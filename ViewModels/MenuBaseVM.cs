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
        static MenuVM()
        {
            RootMenus.Items.AddRange(new[]
            { 
                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Create, Properties.Resources.nfile_Create, 100),
                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Open, Properties.Resources.nfile_Open, 101),
                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Add, Properties.Resources.nfile_Add, 300),
                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Close, Properties.Resources.m_File, 500),
                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_CloseProject, Properties.Resources.m_File, 500),

                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Last_Projects, Properties.Resources.nfile_last_Projects, 700),
                new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Last_File, Properties.Resources.nfile_last_Files, 701),
            });
        }
        public ObservableCollection<MenuItemVM> RootItems => MenuServer.Items;
        public MenuItemVM FileMenu { get; private set; }
        public MenuItemVM ShowWindow { get; private set; }
        public MenuItemVM Hiddens { get; private set; }
        public MenuVM(IMenuItemServer menuItemServer) 
        {
           // var mainvm = ServiceProvider.GetRequiredService<MainVindowVM>();
            //Items = new ObservableCollection<MenuItemVM>();
            RootItems.Add(FileMenu = new MenuItemVM
            {
                // IconSource = "pack://application:,,,/Images/DockBottom.PNG",
                ContentID = RootMenusID.NFile,
                Header = Properties.Resources.m_File,
                Items =
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
                        new MenuSaveFile
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
                        new MenuOpenFile
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

                      //  new MenuItemVM{Priority=999},
                        new CommandMenuItemVM
                        {
                            Priority =10000,
                            //InputGestureText="Ctrl+X",
                            Header="E_xit",
                            Command = new RelayCommand(() => Application.Current.Shutdown())
                        },
                    },
            });
            menuItemServer.UpdateSeparatorGroup(FileMenu);

            RootItems.Add(ShowWindow = new MenuItemVM
            {
                ContentID = RootMenusID.NShow,
                Header = Properties.Resources.m_Show,
                Items =
                    {

                    }
            });
            RootItems.Add(Hiddens = new MenuHiddens());
            //RootItems.Add(Hiddens = new HiddenVM
            //{
            //    IconSource = "pack://application:,,,/Images/DockPane.PNG",
            //    Header = Properties.Resources.m_Hidden,
            //    ContentID = RootMenusID.NHidden,
            //    OnSubMenuAction = () =>
            //    {
            //        while (Hiddens!.Items.Count > 2) Hiddens.Items.RemoveAt(Hiddens.Items.Count - 1);
            //        foreach (var a in dockManager.Layout.Hidden)
            //        {
            //            Hiddens.Items.Add(new CommandMenuItemVM
            //            {
            //                Header = a.Title,
            //                Priority = 100,
            //                Command = new RelayCommand(() => a.Show()),
            //            });
            //        }
            //    },
            //    Items =
            //    {
            //        new CommandMenuItemVM
            //        {
            //            Header = Properties.Resources.m_ShowAll,
            //            Priority = 0,
            //            Command = new RelayCommand(()=>
            //            {
            //                for(int i = 2; i < Hiddens!.Items.Count; i++)
            //                    (Hiddens.Items[i] as CommandMenuItemVM)?.Command?.Execute(null);
            //            }),
            //        },
            //        new SeparatorVM(){Priority = 0},
            //    }
            //});
            // menuItemServer.UpdateSeparatorGroup(Hiddens);


            //var view = new OnSubMenuOpenMenuItemVM { Header = "_Test Views" };

            //view.OnSubMenuAction = () => view.Header = view.Header + "1";

            //var sview1 = new MenuItemVM { 
            //    Header = "sViews1",
            //    IconSource = "pack://application:,,,/Images/DockBottom.PNG",
            //};
            //var sview2 = new MenuItemVM { Header = "sViews2", };
            //var sview3 = new CommandMenuItemVM
            //{
            //    Header = "exit",
            //    Priority = 1000,
            //    IconSource = "pack://application:,,,/Images/DockBottom.PNG",
            //    Command = new RelayCommand(() => Environment.Exit(0))
            //};
            //view.Items.Add(sview1);
            //view.Items.Add(sview2);
            //view.Items.Add(sview3);
            //menuItemServer.UpdateSeparatorGroup(view);
            //RootItems.Add(view);
        }
    }
}
