using System;
using GalaSoft.MvvmLight;

namespace Squirrel.ViewModels
{
    public class SquirrelViewModelBase : ViewModelBase
    {
        public void NotifyPropertyChanged(string propertyName)
        {
            if (!String.IsNullOrEmpty(propertyName) && !String.IsNullOrWhiteSpace(propertyName))
                RaisePropertyChanged(propertyName);
        }
    }
}
