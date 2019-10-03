
using Infragistics.Samples.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace InfragisticsDataVisualization.ViewModel
{
    class PivotViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PivotViewModel()
        {
            //Get all data required in the pivot controls
            SalesDataSample salesDataSample = new SalesDataSample();
        }
    }
}
