using Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorkProgMain.ViewModels
{
    public abstract class MenuFile: MenuItemVM
    {
        public string? Title { get; set; }
        public string? InitialDirectory { get; set; }
        public string? Filter { get; set; }
        public bool ValidateNames { get; set; }
        public IList<object>? CustomPlaces { get; set; }
        public bool CheckPathExists { get; set; }
        public bool CheckFileExists { get; set; }
        public bool AddExtension { get; set; }
        public string? DefaultExt { get; set; }
        public Action<string>? OnSelectFileAction = null;
    }
    public class MenuOpenFile: MenuFile
    {
        public bool ReadOnlyChecked { get; set; }
        public bool ShowReadOnly { get; set; }
    }
    public class MenuSaveFile : MenuFile 
    {
        public bool CreatePrompt { get; set; }
        public bool OverwritePrompt { get; set; }
    }

}
