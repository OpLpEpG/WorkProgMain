using CommunityToolkit.Mvvm.Input;
using Connections;
using Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Serialization;
//using WorkProgMain.Controls;
using WorkProgMain.Services;
using WpfDialogs;

namespace WorkProgMain.ViewModels
{
    public class MainVindowVM : VMBase
    {
        /// <summary>        
        /// т.к VM незнает про View создаем статические делегаты
        /// для управления View из VM без дата Binding
        /// делегатам присваивается значение во View
        /// делегаты выполняются из VM
        /// </summary>
        public static Action? ActionHideDockManager { get; set; }
        public static Action? ActionShowDockManager { get; set; }
        public static Action<string>? ActionSaveDockManager { get; set; }
        public static Action<string>? ActionLoadDockManager { get; set; }
        /// <summary>
        ///  меню VM, тулы VM
        /// </summary>        
        public MenuVM MenuVM { get; private set; }
        public ToolBarVM ToolGlyphVM { get; private set; }
        public ToolBarVM ToolTextVM { get; private set; }
        public MainVindowVM(MenuVM m, IToolServer t)
        {
            ToolGlyphVM = new ToolBarVM { ContentID = "ToolGlyph" };
            ToolTextVM = new ToolBarVM { ContentID = "ToolText" };
            t.AddBar(ToolGlyphVM);
            t.AddBar(ToolTextVM);
            MenuVM = m;
        }
        //public ICommand CloseWindowCommand => new RelayCommand<Window>((wnd) => wnd?.Close());
        //public ICommand MaximizeWindowCommand => new RelayCommand(() => CurWindowState = WindowState.Maximized);
        //public ICommand MinimizeWindowCommand => new RelayCommand(() => CurWindowState = WindowState.Minimized);
        //public ICommand RestoreWindowCommand => new RelayCommand(() => CurWindowState = WindowState.Normal);
        //public void OnMainFormLoaded(object sender, RoutedEventArgs e)
        //{
        //    //LayoutsService.eventHandler = DocIsVisibleChanged;
        //   // dockManager.IsVisibleChanged += (s, e) => DocIsVisibleChanged(this, EventArgs.Empty);

        //   // DocIsVisibleChanged(this, EventArgs.Empty);

        //    //if (Properties.Settings.Default.Height > 0 && Properties.Settings.Default.Width > 0)
        //    //{
        //    //    mainForm.Top = Properties.Settings.Default.Top;
        //    //    mainForm.Left = Properties.Settings.Default.Left;
        //    //    mainForm.Height = Properties.Settings.Default.Height;
        //    //    mainForm.Width = Properties.Settings.Default.Width;
        //    //    if (Properties.Settings.Default.Maximized) mainForm.WindowState = WindowState.Maximized;
        //    //    tbMenu.Band = Properties.Settings.Default.ToolBarMenu_Band;
        //    //    tbMenu.BandIndex = Properties.Settings.Default.ToolBarMenu_Index;
        //    //    tbGlyph.Band = Properties.Settings.Default.ToolBarGlyph_Band;
        //    //    tbGlyph.BandIndex = Properties.Settings.Default.ToolBarGlyph_Index;
        //    //    tbTxt.Band = Properties.Settings.Default.ToolBarTxt_Band;
        //    //    tbTxt.BandIndex = Properties.Settings.Default.ToolBarTxt_Index;
        //    //}
        //    //mainForm.Closing += (s, e) =>
        //    //{
        //    //    Properties.Settings.Default.Top = mainForm.RestoreBounds.Top;
        //    //    Properties.Settings.Default.Left = mainForm.RestoreBounds.Left;
        //    //    Properties.Settings.Default.Height = mainForm.RestoreBounds.Height;
        //    //    Properties.Settings.Default.Width = mainForm.RestoreBounds.Width;
        //    //    Properties.Settings.Default.Maximized = mainForm.WindowState == WindowState.Maximized;
        //    //    Properties.Settings.Default.ToolBarMenu_Band = tbMenu.Band;
        //    //    Properties.Settings.Default.ToolBarMenu_Index = tbMenu.BandIndex;
        //    //    Properties.Settings.Default.ToolBarGlyph_Band = tbGlyph.Band;
        //    //    Properties.Settings.Default.ToolBarGlyph_Index = tbGlyph.BandIndex;
        //    //    Properties.Settings.Default.ToolBarTxt_Band = tbTxt.Band;
        //    //    Properties.Settings.Default.ToolBarTxt_Index = tbTxt.BandIndex;
        //    //    Properties.Settings.Default.Save();
        //    //};
        //}
        //public T AddToLayout<T>(bool CanClose = true, bool CanHide = false, bool CanAutoHide = true, bool CanFloat = true, bool CanDockAsTabbedDocument = true)
        //    where T : class
        //{
        //    throw new NotImplementedException();
        //T context = _serviceProvider.GetRequiredService<T>();
        //if (context is DockUserControl) AddToLayout(context);
        //else
        //{
        //    var l = new LayoutAnchorable();
        //    l.Content = context;
        //    l.CanClose = CanClose;
        //    l.CanHide = CanHide;
        //    l.CanAutoHide = CanAutoHide;
        //    l.CanFloat = CanFloat;
        //    l.CanDockAsTabbedDocument = CanDockAsTabbedDocument;
        //    l.AddToLayout(dockManager, AnchorableShowStrategy.Most);
        //    l.Show();
        //}
        //return context;
        //  }
        //public void AddToLayout(object content)
        //{
        //    if (content is DockUserControl u)
        //    {
        //        u.AddToLayout(dockManager, DocIsVisibleChanged);
        //        //var d = new LayoutDocument();
        //        //d.Title = "u.Title";
        //        //d.Content = u;
        //        //mainWindow.AddToLayoutDocument(d);
        //    }
        //}
        //public void DocIsVisibleChanged(object? sender, EventArgs e)
        //{
        //    MenuVM.Hiddens.IsEnable = (dockManager.Layout.Hidden.Count > 0) && dockManager.IsVisible;
        //}
    }
}
