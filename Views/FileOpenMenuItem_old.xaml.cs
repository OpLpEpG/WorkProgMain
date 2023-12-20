using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;
using Microsoft.Win32;
using WorkProgMain.ViewModels;

namespace WorkProgMain.Views
{
    /// <summary>
    /// Логика взаимодействия для FileOpenMenuItem.xaml
    /// </summary>
    public partial class FileOpenMenuItem : MenuItem
    {
        public FileOpenMenuItem()
        {
            InitializeComponent();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if(openFileDialog.ShowDialog() == true) 
            {
                (this.DataContext as MenuOpenFile)?.OnSelectFileAction?.Invoke(openFileDialog.FileName);
            }

        }
    }
}
