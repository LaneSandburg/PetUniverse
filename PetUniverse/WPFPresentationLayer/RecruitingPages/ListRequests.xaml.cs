﻿using DataTransferObjects;
using LogicLayer;
using LogicLayerInterfaces;
using PresentationUtilityCode;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFPresentationLayer.RecruitingPages
{
    /// <summary>
    /// Interaction logic for ListRequests.xaml
    /// </summary>
    public partial class ListRequests : Page
    {
        private IRequestManager _requestManager;
        private IDepartmentManager _departmentManager;
        private List<DepartmentRequest> _newRequests;
        private List<DepartmentRequest> _activeRequests;
        private List<DepartmentRequest> _doneRequests;
        private PetUniverseUser _user;
        private List<string> _departmentIDs; //Unsure if needed in listview or just Details for creation and edit
        private List<string> _requestTypes;  //Unsure if needed in listview or just Details for creation and edit
        private List<string> _userDepartmentIDs;
        private List<string[]> _employeeNames;

        public ListRequests(PetUniverseUser user)
        {
            _user = user;
            _requestManager = new RequestManager();
            _departmentManager = new DepartmentManager();
            PopulateDepartments();
            PopulateEmployeeNames();
            InitializeComponent();
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/22
        /// Approver: Derek Taylor
        ///
        /// Helper method to populate an employee user's departments, based on their userID
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
        ///
        /// </remarks>
        private void PopulateDepartments()
        {
            try
            {
                _userDepartmentIDs = _requestManager.RetrieveAllDepartmentIDsByUserID(_user.PUUserID);
                _departmentIDs = _departmentManager.RetrieveAllDepartments().DepartmentIDFilter();
                _requestTypes = _requestManager.RetriveAllRequestTypes();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/03/10
        /// Approver: Derek Taylor
        ///
        /// Load Method to retrieve a list of Employee names and associated employee numbers.  This allows the DepartmentRequestVM to 
        /// display appropriate employee names instead of just their numbers.
        /// </summary>
        /// <remarks>
        /// Updater:  
        /// Updated: 
        /// Update: 
        ///
        /// </remarks>
        private void PopulateEmployeeNames()
        {
            try
            {
                _employeeNames = _requestManager.RetrieveEmployeeNames();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/13
        /// Approver: Derek Taylor
        ///
        /// Load Method to retrieve a list of New Requests when the NewRequestTab loads, and bind 
        /// that list to the datagrid.
        /// </summary>
        /// <remarks>
        /// Updater: Ryan Morganti  
        /// Updated: 2020/03/05
        /// Update: Centralized all request tabs into one Page.
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgNewRequestList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user.IsEmployee())
                {
                    _newRequests = _requestManager.RetrieveNewRequestsByDepartmentID(_userDepartmentIDs);
                    dgNewRequestList.ItemsSource = _newRequests.DistinctRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/13
        /// Approver: Derek Taylor
        ///
        /// Method for filtering which columns to fill once the DataGrid for NewRequests is given a source
        /// </summary>
        /// <remarks>
        /// Updater: Ryan Morganti  
        /// Updated: 2020/03/05
        /// Update: Centralized all request tabs into one Page.
        ///
        /// </remarks>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgNewRequestList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            //dgNewRequestList.Columns.RemoveAt(13);
            dgNewRequestList.Columns.RemoveAt(11);
            dgNewRequestList.Columns.RemoveAt(10);
            dgNewRequestList.Columns.RemoveAt(9);
            dgNewRequestList.Columns.RemoveAt(6);
            dgNewRequestList.Columns.RemoveAt(5);
            dgNewRequestList.Columns.RemoveAt(4);
            dgNewRequestList.Columns.RemoveAt(3);
            dgNewRequestList.Columns.RemoveAt(0);
            dgNewRequestList.Columns[3].DisplayIndex = 4;
            dgNewRequestList.Columns[3].Header = "Topic";
            dgNewRequestList.Columns[0].DisplayIndex = 1;
            dgNewRequestList.Columns[0].Header = "Requested By";
            dgNewRequestList.Columns[2].DisplayIndex = 0;
            dgNewRequestList.Columns[2].Header = "Request";
            dgNewRequestList.Columns[1].DisplayIndex = 2;
            dgNewRequestList.Columns[1].Header = "Requested Of";
            dgNewRequestList.Columns[4].DisplayIndex = 3;
            dgNewRequestList.Columns[4].Header = "Date";
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/13
        /// Approver: Derek Taylor
        ///
        /// Load Method to retrieve a list of Active Requests when the ActiveRequestTab loads, and bind 
        /// that list to the datagrid.
        /// </summary>
        /// <remarks>
        /// Updater: Ryan Morganti  
        /// Updated: 2020/03/05
        /// Update: Centralized all request tabs into one Page.
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgActiveRequestList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgActiveRequestList.ItemsSource == null)
                {
                    _activeRequests = _requestManager.RetrieveActiveRequestsByDepartmentID(_userDepartmentIDs);
                    dgActiveRequestList.ItemsSource = _activeRequests.DistinctRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/13
        /// Approver: Derek Taylor
        ///
        /// Method for filtering which columns to fill once the DataGrid for ActiveRequests is given a source
        /// </summary>
        /// <remarks>
        /// Updater: Ryan Morganti  
        /// Updated: 2020/03/05
        /// Update: Centralized all request tabs into one Page.
        ///
        /// </remarks>
        /// /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgActiveRequestList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            //dgActiveRequestList.Columns.RemoveAt(13);
            dgActiveRequestList.Columns.RemoveAt(11);
            dgActiveRequestList.Columns.RemoveAt(10);
            dgActiveRequestList.Columns.RemoveAt(9);
            dgActiveRequestList.Columns.RemoveAt(6);
            dgActiveRequestList.Columns.RemoveAt(5);
            dgActiveRequestList.Columns.RemoveAt(4);
            dgActiveRequestList.Columns.RemoveAt(3);
            dgActiveRequestList.Columns.RemoveAt(0);
            dgActiveRequestList.Columns[3].DisplayIndex = 4;
            dgActiveRequestList.Columns[3].Header = "Topic";
            dgActiveRequestList.Columns[0].DisplayIndex = 1;
            dgActiveRequestList.Columns[0].Header = "Requested By";
            dgActiveRequestList.Columns[2].DisplayIndex = 0;
            dgActiveRequestList.Columns[2].Header = "Request";
            dgActiveRequestList.Columns[1].DisplayIndex = 2;
            dgActiveRequestList.Columns[1].Header = "Requested Of";
            dgActiveRequestList.Columns[4].DisplayIndex = 3;
            dgActiveRequestList.Columns[4].Header = "Date";
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/13
        /// Approver: Derek Taylor
        ///
        /// Load Method to retrieve a list of Completed Requests when the DoneRequestTab loads, and bind 
        /// that list to the datagrid.
        /// </summary>
        /// <remarks>
        /// Updater: Ryan Morganti  
        /// Updated: 2020/03/05
        /// Update: Centralized all request tabs into one Page.
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgDoneRequestList_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgDoneRequestList.ItemsSource == null)
                {
                    _doneRequests = _requestManager.RetrieveCompleteRequestsByDepartmentID(_userDepartmentIDs);
                    dgDoneRequestList.ItemsSource = _doneRequests.DistinctRequests();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.InnerException.Message);
            }
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/02/13
        /// Approver: Derek Taylor
        ///
        /// Method for filtering which columns to fill once the DataGrid for DoneRequests is given a source
        /// </summary>
        /// <remarks>
        /// Updater: Ryan Morganti  
        /// Updated: 2020/03/05
        /// Update: Centralized all request tabs into one Page.
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgDoneRequestList_AutoGeneratedColumns(object sender, EventArgs e)
        {
            //dgDoneRequestList.Columns.RemoveAt(13);
            dgDoneRequestList.Columns.RemoveAt(11);
            dgDoneRequestList.Columns.RemoveAt(10);
            dgDoneRequestList.Columns.RemoveAt(9);
            dgDoneRequestList.Columns.RemoveAt(6);
            dgDoneRequestList.Columns.RemoveAt(5);
            dgDoneRequestList.Columns.RemoveAt(4);
            dgDoneRequestList.Columns.RemoveAt(3);
            dgDoneRequestList.Columns.RemoveAt(0);
            dgDoneRequestList.Columns[3].DisplayIndex = 4;
            dgDoneRequestList.Columns[3].Header = "Topic";
            dgDoneRequestList.Columns[0].DisplayIndex = 1;
            dgDoneRequestList.Columns[0].Header = "Requested By";
            dgDoneRequestList.Columns[2].DisplayIndex = 0;
            dgDoneRequestList.Columns[2].Header = "Request";
            dgDoneRequestList.Columns[1].DisplayIndex = 2;
            dgDoneRequestList.Columns[1].Header = "Requested Of";
            dgDoneRequestList.Columns[4].DisplayIndex = 3;
            dgDoneRequestList.Columns[4].Header = "Date";
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/03/05
        /// Approver: Derek Taylor
        ///
        /// Event Method for opening a select New request into a detailed view.
        /// </summary>
        /// <remarks>
        /// Updater:  
        /// Updated: 
        /// Update: 
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgNewRequestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DepartmentRequestVM requestVM = new DepartmentRequestVM((DepartmentRequest)dgNewRequestList.SelectedItem);
            requestVM.PopulateUserNames(_employeeNames);
            this.NavigationService?.Navigate(frDeptRequestDetails.Content
                = new DepartmentRequestDetails(_requestManager, _user, requestVM, _departmentIDs, _requestTypes, _employeeNames));
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/03/10
        /// Approver: Derek Taylor
        ///
        /// Event Method for opening a select Active request into a detailed view.
        /// </summary>
        /// <remarks>
        /// Updater:  
        /// Updated: 
        /// Update: 
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgActiveRequestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DepartmentRequestVM requestVM = new DepartmentRequestVM((DepartmentRequest)dgActiveRequestList.SelectedItem);
            requestVM.PopulateUserNames(_employeeNames);
            this.NavigationService?.Navigate(frDeptRequestDetails.Content
                = new DepartmentRequestDetails(_requestManager, _user, requestVM, _departmentIDs, _requestTypes, _employeeNames));
        }

        /// <summary>
        /// Creator: Ryan Morganti
        /// Created: 2020/03/10
        /// Approver: Derek Taylor
        ///
        /// Event Method for opening a select Completed request into a detailed view.
        /// </summary>
        /// <remarks>
        /// Updater:  
        /// Updated: 
        /// Update: 
        ///
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgDoneRequestList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DepartmentRequestVM requestVM = new DepartmentRequestVM((DepartmentRequest)dgDoneRequestList.SelectedItem);
            requestVM.PopulateUserNames(_employeeNames);
            this.NavigationService?.Navigate(frDeptRequestDetails.Content
                = new DepartmentRequestDetails(_requestManager, _user, requestVM, _departmentIDs, _requestTypes, _employeeNames));
        }
    }
}
