﻿using DataTransferObjects;
using LogicLayer;
using PresentationUtilityCode;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WPFPresentationLayer.RecruitingPages
{
    /// <summary>
    /// Interaction logic for ListCompleteRequests.xaml
    /// </summary>
    public partial class ListCompleteRequests : Page
    {
        private IRequestManager _requestManager;
        private List<DepartmentRequest> _doneRequests;
        private PetUniverseUser _user;
        private List<string> _userDepartmentIDs;
        public ListCompleteRequests(PetUniverseUser user)
        {
            _user = user;
            _requestManager = new RequestManager();
            populateDepartments();
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
        private void populateDepartments()
        {
            try
            {
                _userDepartmentIDs = _requestManager.RetrieveAllDepartmentIDsByUserID(this._user.PUUserID);
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
        /// Load Method to retrieve a list of Completed Requests when the DoneRequestTab loads, and bind 
        /// that list to the datagrid.
        /// </summary>
        /// <remarks>
        /// Updater:
        /// Updated:
        /// Update:
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
        /// Updater:
        /// Updated:
        /// Update:
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
    }
}
