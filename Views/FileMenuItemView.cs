using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorkProgMain.ViewModels;
using Xceed.Wpf.AvalonDock.Controls;

namespace WorkProgMain.Views
{
    public abstract class FileMenuItemView : MenuItem
    {
        public FileDialog? FileDialog {get; set;}
        public FileMenuItemView()
        {
            Click += (o, e) =>
            {
                SetupDialog();
                if (FileDialog?.ShowDialog() == true)
                {
                    (this.DataContext as MenuFile)?.OnSelectFileAction?.Invoke(FileDialog.FileName);
                }
            };
        }
        protected virtual void SetupDialog()
        {
            if (FileDialog != null && DataContext is MenuFile mf)
            {
                FileDialog.Title = mf.Title ?? FileDialog.Title;
                FileDialog.InitialDirectory = mf.InitialDirectory ?? FileDialog.InitialDirectory;
                FileDialog.AddExtension = mf.AddExtension;
                FileDialog.CheckFileExists = mf.CheckFileExists;
                FileDialog.CheckPathExists = mf.CheckPathExists;
                FileDialog.DefaultExt = mf.DefaultExt ?? FileDialog.DefaultExt;
                FileDialog.Filter = mf.Filter ?? FileDialog.Filter;
                FileDialog.ValidateNames = mf.ValidateNames;
                if (mf.CustomPlaces != null)
                {
                    FileDialog.CustomPlaces = Array.ConvertAll(mf.CustomPlaces.ToArray(), (o) =>
                    {
                        if (o is string s) return new FileDialogCustomPlace(s);
                        else if (o is Guid g) return new FileDialogCustomPlace(g);
                        else return new FileDialogCustomPlace("no name");
                    });
                }
            }
        }
    }

    public class FileOpenMenuItemView : FileMenuItemView
    {
        protected override void SetupDialog()
        {
            this.FileDialog = new OpenFileDialog();
            base.SetupDialog();
            if (DataContext is MenuOpenFile m && FileDialog is OpenFileDialog f)
            {
                f.ReadOnlyChecked = m.ReadOnlyChecked;
                f.ShowReadOnly = m.ShowReadOnly;
            }
        }
    }
    public class FileSaveMenuItemView : FileMenuItemView
    {
        protected override void SetupDialog()
        {
            this.FileDialog = new SaveFileDialog();
            base.SetupDialog();
            if (DataContext is MenuSaveFile m && FileDialog is SaveFileDialog f)
            {
                f.CreatePrompt = m.CreatePrompt;
                f.OverwritePrompt = m.OverwritePrompt;
            }
        }
    }
}
