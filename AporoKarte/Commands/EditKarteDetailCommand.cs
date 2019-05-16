using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using AporoKarte.ViewModels;

namespace AporoKarte.Commands
{
    public class EditKarteDetailCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            MessageBox.Show(parameter.ToString());
        }
    }
}
