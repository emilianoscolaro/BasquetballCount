using BasquetballCount.CustomControlls.CustomDialogs.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace BasquetballCount.ViewModels.BaseViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public INavigation Navigation = null;

        public BaseViewModel()
        {
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string NombrePropiedad = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(NombrePropiedad));
            }
        }
    }
}
