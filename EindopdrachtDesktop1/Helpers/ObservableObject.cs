﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MenuNavigatie.Helpers
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        #region implementatie INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
