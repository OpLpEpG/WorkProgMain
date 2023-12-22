using CommunityToolkit.Mvvm.Input;
using Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using WorkProgMain.Models;
using WpfDialogs;

namespace WorkProgMain.ViewModels
{
    //    <!--<c:PriorityMenu Priority = "0" Name="NFile" Header="{x:Static res:Resources.m_File}">
    //    <c:PriorityMenu Priority = "11" Header="_Save" Command="{Binding DocLayoutSaveCommand}"/>
    //    <c:PriorityMenu Priority = "12" Header="_Restore" Command="{Binding DocLayoutRestoreCommand}"/>
    //    <c:PriorityMenu Priority = "13" Header="_Load" Command="{Binding DocLayoutLoadCommand}"/>
    //    <c:PriorityMenu Priority = "14" Header="_UnLoad" Command="{Binding DocLayoutUnloadCommand}"/>
    //    <c:PrioritySeparator Priority = "1000" />
    //    -->< !--< a:MenuItemEx Header = "_Avalon" /> -->< !--
    //    < c:PriorityMenu Priority = "1111" Header="E_xit" Command="{Binding CloseWindowCommand}" 
    //              CommandParameter="{Binding ElementName=WindowMain}"/>
    //</c:PriorityMenu>
    //<c:PriorityMenu Priority = "1" Name="NShow" Header="{x:Static res:Resources.m_Show}"/>
    //<c:PriorityMenu Priority = "2" Name="NHidden" Header="{x:Static res:Resources.m_Hidden}">
    //    <i:Interaction.Triggers>
    //        <i:EventTrigger EventName = "SubmenuOpened" >
    //            < i:InvokeCommandAction Command = "{Binding HiddenSubmenuOpenedCommand}"
    //                                   CommandParameter="{Binding ElementName=NHidden}"/>
    //        </i:EventTrigger>
    //    </i:Interaction.Triggers>
    //    <c:PriorityMenu Priority = "0" Name="NShowAll" Header="{x:Static res:Resources.m_ShowAll}"
    //              Command="{Binding ShowAllHiddenCommand}"
    //              CommandParameter="{Binding ElementName=NHidden}"/>
    //    <c:PrioritySeparator Priority = "100" Name="NShowAllSeparator"/>
    //</c:PriorityMenu>
    //<c:PriorityMenu Priority = "11" Name="NControll" Header="{x:Static res:Resources.m_Controll}" Visibility="Collapsed"/>
    //<c:PriorityMenu Priority = "12" Name="NMetrology" Header="{x:Static res:Resources.m_Metrology}" Visibility="Collapsed"/>-->
    //public class HiddenVM: OnSubMenuOpenMenuItemVM { }
    public class MenuVM : VMBase//, IMenuItemServer
    {
        public ObservableCollection<PriorityItem> RootItems => MenuServer.Items;
        public MenuVM(IMenuItemServer menuItemServer) 
        {
            MMenus.CreateMenusStructure();

            menuItemServer.Add(RootMenusID.NFile, new MenuItemVM[]
            {
                        #region TEST Menus
                        new CommandMenuItemVM
                        {
                            Priority=1190,
                            Header="HIde Dock Manager",
                            Command= new RelayCommand(() =>
                                MainVindowVM.ActionHideDockManager?.Invoke())
                        },
                        new CommandMenuItemVM
                        {
                            Priority=1199,
                            Header="Show Dock Manager",
                            Command= new RelayCommand(() =>
                                MainVindowVM.ActionShowDockManager?.Invoke())
                        },
                        new MenuSaveFileVM
                        {
                            Priority=3199,
                            Header="Save Dock Manager",
                            Title = Properties.Resources.nfile_Create,
                            Filter = "Text documents (.xml)|*.xml|Any documents (.doc)|*.doc|Any (*)|*",
                            DefaultExt = ".xml",
                            OverwritePrompt = true,
                            CreatePrompt = true,
                            CustomPlaces = new object[]
                            {
                                @"C:\Users\Public\Documents\Горизонт\WorkProg\Projects\",
                                @"C:\XE\Projects\Device2\_exe\Debug\Метрология\",
                                @"G:\Мой диск\mtr\",
                                new Guid("FDD39AD0-238F-46AF-ADB4-6C85480369C7"),//документы
                                new Guid("1777F761-68AD-4D8A-87BD-30B759FA33DD"),//избрвнное
                            },
                            OnSelectFileAction = f =>
                                MainVindowVM.ActionSaveDockManager?.Invoke(f)                            
                            //{
                            //    //var all = dockManager.Layout.Descendents().OfType<LayoutContent>().ToArray();
                            //    //foreach (var n in all)
                            //    //{
                            //    //    XmlSerializer writer = new XmlSerializer(typeof(FormBase));
                            //    //    //null,
                            //    //    //AbstractConnection.GetConnectionTypes(), null, null);
                            //    //    StringWriter s = new StringWriter();
                            //    //    writer.Serialize(s, n.Content);

                            //    //    n.ContentId = s.ToString();
                            //    //}
                            //    var serializer = new XmlLayoutSerializer(dockManager);
                            //    using (var stream = new StreamWriter(string.Format(@".\AvalonDock_{0}.xml", 1)))
                            //        serializer.Serialize(stream);
                            //})
                        },
                        new MenuOpenFileVM
                        {
                            Priority=3199,
                            Header="Restore Dock Manager",
                            Title = Properties.Resources.nfile_Open,
                            Filter = "Text documents (.xml)|*.xml|Any documents (.doc)|*.doc|Any (*)|*",
                            DefaultExt = ".xml",
                            CustomPlaces = new object[]
                            {
                                @"C:\Users\Public\Documents\Горизонт\WorkProg\Projects\",
                                @"C:\XE\Projects\Device2\_exe\Debug\Метрология\",
                                @"G:\Мой диск\mtr\",
                                new Guid("FDD39AD0-238F-46AF-ADB4-6C85480369C7"),//документы
                                new Guid("1777F761-68AD-4D8A-87BD-30B759FA33DD"),//избрвнное
                            },
                            OnSelectFileAction = f =>
                            {
                                DockManagerVM.Clear();
                                MainVindowVM.ActionLoadDockManager?.Invoke(f);
                                VMBaseForms.OnVisibleChange(this);
                            },
                        },
                        #endregion

                        new CommandMenuItemVM
                        {
                            Priority =10000,
                            //InputGestureText="Ctrl+X",
                            Header="E_xit",
                            Command = new RelayCommand(() => Application.Current.Shutdown())
                        },

            });
            menuItemServer.Add(RootMenusID.ROOT, new[]
            {
                new MenuHiddens()
            });

        }
    }
}
