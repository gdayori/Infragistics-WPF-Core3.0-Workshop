using Infragistics.Samples.Data.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace InfragisticsDataVisualization.ViewModel
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public ICommand OpenDashboard { get; set; }
        public ICommand OpenBiWindow { get; set; }

        public MainWindowViewModel()
        {

            // Get sales data to be bound to grid
            SalesDataSample salesDataSample = new SalesDataSample();
            salesRecords = salesDataSample.SalesData;

            // Commands
            OpenDashboard = new OpenDashboardCommand();
            OpenBiWindow = new OpenPivotCommand();
        }

        private ObservableCollection<Sale> salesRecords;
        public ObservableCollection<Sale> SalesRecords
        {
            get { return salesRecords; }
        }
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class OpenDashboardCommand : ICommand
    {
        public OpenDashboardCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var newWindow = new Dashboard();
            newWindow.Show();
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public class OpenPivotCommand : ICommand
    {
        public OpenPivotCommand()
        {
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var newWindow = new Pivot();
            newWindow.Show();
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
