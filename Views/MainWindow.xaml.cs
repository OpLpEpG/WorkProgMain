using Core;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Navigation;
using WorkProgMain.ViewModels;
using WpfDialogs;
using Xceed.Wpf.AvalonDock;
using Xceed.Wpf.AvalonDock.Controls;
using Xceed.Wpf.AvalonDock.Layout;
using Xceed.Wpf.AvalonDock.Layout.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace WorkProgMain
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window//, IMainWindow
    {
        #region IMainWindow
        //public Grid GridRoot => gridRoot;
        //public ToolBarTray ToolBarTray => toolBarTry;
        //public ToolBar ToolBarMenu => toolBarMenu;
        //public ToolBar ToolBarSpeedButtons => toolBarButtonGliph;
        //public ToolBar ToolBarButtons => toolBarButtonText;
        //public DockingManager DockManager => dockManager;
        //public StatusBar StatusBar => sb;
        //public Window Window => this;
        //public LayoutDocumentPane DocumentPane => layRootDock;
        //public object? FindMenu(string name)=> menu.FindName(name);
        //public T AddToLayout<T>(bool CanClose = true, bool CanHide = false, bool CanAutoHide = true, bool CanFloat = true, bool CanDockAsTabbedDocument = true)
        // where T : class => ((MainVindowVM)DataContext).AddToLayout<T>(CanClose, CanHide, CanAutoHide, CanFloat, CanDockAsTabbedDocument);
        //public void AddToLayout(object content) => ((MainVindowVM)DataContext).AddToLayout(content);
        //public void AddToLayoutDocument(object content) => layRootDock.Children.Add((LayoutDocument)content);
        #endregion
        public MainWindow(MainVindowVM vm)
        {
            InitializeComponent();
            DataContext = vm;
            MainVindowVM.ActionSaveDockManager = s =>
            {
                var serializer = new XmlLayoutSerializer(dockManager);
                serializer.Serialize(s);
            };
            MainVindowVM.ActionLoadDockManager = s =>
            {
                var serializer = new XmlLayoutSerializer(dockManager);
                var fs = VMBase.ServiceProvider.GetRequiredService<IFormsServer>();

                serializer.LayoutSerializationCallback += (s, args) =>
                    args.Content = fs.AddOrGet(args.Model.ContentId);

                serializer.Deserialize(s);

            };
            MainVindowVM.ActionHideDockManager = () =>
            {
                if (gridRoot.Children.Contains(dockManager))
                    gridRoot.Children.Remove(dockManager);
            };
            MainVindowVM.ActionShowDockManager = () =>
            {
                if (!gridRoot.Children.Contains(dockManager))
                    gridRoot.Children.Add(dockManager);
            };
            //VMBaseForms.OnClose += (o) =>
            //{
            //    var lc = dockManager.Layout.Descendents().OfType<LayoutContent>().FirstOrDefault(lc => lc.Content == o);
            //    if (lc != null)
            //    {
            //        //if (!lc.CanClose && lc is LayoutAnchorable la)
            //        //{
            //        //    //lc.CanClose = true;
            //        //}
            //        lc.Close();
            //        //dockManager.RemoveViewFromLogicalChild(lc);
            //    }
            //};
            VMBaseForms.OnFocus += (o) =>
            {
                var w = dockManager.FloatingWindows.FirstOrDefault(
                    w => w.Model.Descendents().OfType<LayoutContent>().FirstOrDefault(
                        lc => lc.Content == o) != null);
                w?.Focus();
            };
        }

        private void WindowMain_Loaded(object sender, RoutedEventArgs e)
        {
            if (Properties.Settings.Default.Height > 0 && Properties.Settings.Default.Width > 0)
            {
                WindowMain.Top = Properties.Settings.Default.Top;
                WindowMain.Left = Properties.Settings.Default.Left;
                WindowMain.Height = Properties.Settings.Default.Height;
                WindowMain.Width = Properties.Settings.Default.Width;
                if (Properties.Settings.Default.Maximized) WindowMain.WindowState = WindowState.Maximized;
                toolBarMenu.Band = Properties.Settings.Default.ToolBarMenu_Band;
                toolBarMenu.BandIndex = Properties.Settings.Default.ToolBarMenu_Index;
                toolBarButtonGliph.Band = Properties.Settings.Default.ToolBarGlyph_Band;
                toolBarButtonGliph.BandIndex = Properties.Settings.Default.ToolBarGlyph_Index;
                toolBarButtonText.Band = Properties.Settings.Default.ToolBarTxt_Band;
                toolBarButtonText.BandIndex = Properties.Settings.Default.ToolBarTxt_Index;
            }
        }

        private void WindowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.Top = WindowMain.RestoreBounds.Top;
            Properties.Settings.Default.Left = WindowMain.RestoreBounds.Left;
            Properties.Settings.Default.Height = WindowMain.RestoreBounds.Height;
            Properties.Settings.Default.Width = WindowMain.RestoreBounds.Width;
            Properties.Settings.Default.Maximized = WindowMain.WindowState == WindowState.Maximized;
            Properties.Settings.Default.ToolBarMenu_Band = toolBarMenu.Band;
            Properties.Settings.Default.ToolBarMenu_Index = toolBarMenu.BandIndex;
            Properties.Settings.Default.ToolBarGlyph_Band = toolBarButtonGliph.Band;
            Properties.Settings.Default.ToolBarGlyph_Index = toolBarButtonGliph.BandIndex;
            Properties.Settings.Default.ToolBarTxt_Band = toolBarButtonText.Band;
            Properties.Settings.Default.ToolBarTxt_Index = toolBarButtonText.BandIndex;
            Properties.Settings.Default.Save();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowMain.WindowState = WindowState.Minimized;
        }

        private void RestoreButton_Click(object sender, RoutedEventArgs e)
        {
            WindowMain.WindowState = WindowState.Normal;
        }

        private void MaximizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowMain.WindowState = WindowState.Maximized;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
