﻿using DataTransferObjects;
using LogicLayer;
using LogicLayerInterfaces;
using PresentationUtilityCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPFPresentationLayer.InventoryPages
{
    /// <summary>
    /// Creator: Brandyn T. Coverdill
    /// Created: 2020/04/08
    /// Approver: 
    /// Approver: 
    ///
    /// The Code-behind file logic for the View Item Report view.
    /// </summary>
    public partial class ViewItemReports : Page
    {

        private IItemReportManager _itemReportManager;

        public ViewItemReports()
        {
            InitializeComponent();
            _itemReportManager = new ItemReportManager();
        }

        /// <summary>
        /// Creator: Brandyn T. Coverdill
        /// Created: 2020/04/08
        /// Approver: 
        /// Approver:  
        ///
        /// Method that generates the columns for the view item report datagrid.
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgViewItemReport_AutoGeneratedColumns(object sender, EventArgs e)
        {
            dgViewItemReport.Columns.RemoveAt(0);
            dgViewItemReport.Columns.RemoveAt(4);
            dgViewItemReport.Columns.RemoveAt(4);
            dgViewItemReport.Columns.RemoveAt(4);
            dgViewItemReport.Columns.RemoveAt(4);
            dgViewItemReport.Columns.RemoveAt(4);
            dgViewItemReport.Columns[0].DisplayIndex = 3;
            dgViewItemReport.Columns[0].Header = "Reported";
            dgViewItemReport.Columns[1].Header = "Item ID";
            dgViewItemReport.Columns[2].Header = "Item Name";
            dgViewItemReport.Columns[3].Header = "Amount of Items Damaged/Missing";

            foreach (var column in this.dgViewItemReport.Columns)
            {
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        /// <summary>
        /// Creator: Brandyn T. Coverdill
        /// Created: 2020/04/08
        /// Approver: 
        /// Approver:  
        ///
        /// When Datagrid is loaded, put items into the grid.
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgViewItemReport_Loaded(object sender, RoutedEventArgs e)
        {
            dgViewItemReport.ItemsSource = _itemReportManager.retrieveItemReports();
        }

        /// <summary>
        /// Creator: Brandyn T. Coverdill
        /// Created: 2020/04/08
        /// Approver: 
        /// Approver:  
        ///
        /// Event that navigates to the EditItemReports screen.
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewItemReport_Click(object sender, RoutedEventArgs e)
        {
            ItemReport itemReport = (ItemReport)dgViewItemReport.SelectedItem;
            if (dgViewItemReport.SelectedItem != null)
            {
                this.NavigationService?.Navigate(new EditItemReports(itemReport));
            }
            else
            {
                "Please pick an item report to view.".ErrorMessage();
            }
        }

        /// <summary>
        /// Creator: Brandyn T. Coverdill
        /// Created: 2020/04/08
        /// Approver: 
        /// Approver:  
        ///
        /// This event deletes an Item Report (Damaged/Missing Items from shelf).
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteItemReport_Click(object sender, RoutedEventArgs e)
        {
            ItemReport itemReport = (ItemReport)dgViewItemReport.SelectedItem;
            if (dgViewItemReport.SelectedItem != null)
            {
                _itemReportManager.deleteItemReport(itemReport.ItemID, itemReport.ItemQuantity, itemReport.Report);
                dgViewItemReport.ItemsSource = _itemReportManager.retrieveItemReports();
            }
            else
            {
                "Please pick an item report to delete.".ErrorMessage();
            }
        }

        /// <summary>
        /// Creator: Brandyn T. Coverdill
        /// Created: 2020/04/08
        /// Approver: 
        /// Approver:  
        ///
        /// This event searches the Item Report List by Item Name.
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearchItem_Click(object sender, RoutedEventArgs e)
        {
            if (txtSearchItem.Text.Trim() == "")
            {
                dgViewItemReport.ItemsSource = _itemReportManager.retrieveItemReports();
            }
            else
            {
                populateViewItemReport();
            }
        }

        /// <summary>
        /// Creator: Brandyn T. Coverdill
        /// Created: 2020/04/08
        /// Approver: 
        /// Approver:  
        ///
        /// This method populates the Datagrid by the Search Field. (Empty shows all Item Reports).
        /// </summary>
        ///
        /// <remarks>
        /// Updated By: 
        /// Updated: 
        /// Update:
        /// </remarks>
        private void populateViewItemReport()
        {
            // Save text from the text area.
            string searchedName = txtSearchItem.Text.ToString();

            // Get a list of all the item reports in the database.
            List<ItemReport> itemReportsForSearch = new List<ItemReport>();
            itemReportsForSearch = _itemReportManager.retrieveItemReports();

            // Search through the Item Names which contain the text entered by the user.
            dgViewItemReport.ItemsSource = itemReportsForSearch.Where(r => r.ItemName.ToLower().Contains(searchedName.ToLower()));
        }
    }
}
