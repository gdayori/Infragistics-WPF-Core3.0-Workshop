
using Infragistics.Olap.FlatData;
using Infragistics.Samples.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace InfragisticsDataVisualization.ViewModel
{
    class PivotViewModel : INotifyPropertyChanged
    {
        public PivotViewModel()
        {
            // Get all data required in the pivot controls
            SalesDataSample salesDataSample = new SalesDataSample();
            // Added ↓↓↓
            var flatDataSource = new Infragistics.Olap.FlatData.FlatDataSource();
            flatDataSource.ItemsSource = salesDataSample.SalesData;
            salesFlatDataSource = flatDataSource;
            // Added ↑↑↑
        }

        // Added ↓↓↓
        //Flat DataSource to be bound to pivot controls
        private FlatDataSource salesFlatDataSource;
        public FlatDataSource SalesFlatDataSource
        {
            get { return salesFlatDataSource; }
        }
        // Added ↑↑↑

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
