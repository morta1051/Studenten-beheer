using MenuNavigatie.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuNavigatie.Model
{
    public class UserMessage : ObservableObject
    {
        #region
        private string _text = string.Empty;
        #endregion

        #region properties
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged(); }
        }
        #endregion


    }




}
