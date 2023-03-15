using StudentManager.Core;
using StudentManager.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StudentManager.MVVM.ViewModel
{
    internal class HomeViewModel
    {
        public RelayCommand CloseWindowCommand { get; set; }
        
        public HomeViewModel()
        {
            CloseWindowCommand = new RelayCommand(o =>
            {
                if (o is IClosable window)
                    window.Close();
            });
        }
    }
}
