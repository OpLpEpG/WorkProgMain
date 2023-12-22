using CommunityToolkit.Mvvm.Input;
using Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorkProgMain.ViewModels
{
    public class ProjectsExplorerMenuFactory : IMenuItemClient
    {
        IServiceProvider _serviceProvider = VMBase.ServiceProvider;

        private void ShowPE() => VMBaseForms.CreateAndShow(nameof(ProjectsExplorerVM));
        void IMenuItemClient.AddStaticMenus(IMenuItemServer _menuItemServer)
        {
            IToolServer toolServer = _serviceProvider.GetRequiredService<IToolServer>();
            toolServer.Add("ToolGlyph", new [] 
            {
                new ToolButton
                {
                    ToolTip = new ToolTip { Content = $"{Properties.Resources.m_Show} {Properties.Resources.tProjectExplorer}" },
                    ContentID = "CidShowProjectsExplorer",
                    IconSource = "pack://application:,,,/Images/Project.PNG",
                    Priority = -1000,
                    Command = new RelayCommand(ShowPE)
                },
                new ToolButton
                {
                    ToolTip = new ToolTip { Content = $"{Properties.Resources.nProject_New}..." },
                    ContentID = "CidShowProjectsExplorer1",
                    IconSource = "pack://application:,,,/Images/NewProject.png",
                    Priority = -500,
                    Command = new RelayCommand(ShowPE)
                },
                //new ToolButton
                //{
                //    ToolTip = new ToolTip { Content = "Show Projec Explorer" },
                //    ContentID = "CidShowProjectsExplorer",
                //    IconSource = "pack://application:,,,/Images/Project.PNG",
                //    Priority = 10000,
                //    Command = new RelayCommand(ShowPE)
                //},
                //new ToolButton
                //{
                //    ToolTip = new ToolTip { Content = "Show Projec Explorer" },
                //    ContentID = "CidShowProjectsExplorer",
                //    IconSource = "pack://application:,,,/Images/Project.PNG",
                //    Priority = 10000,
                //    Command = new RelayCommand(ShowPE)
                //},
                //new ToolButton
                //{
                //    ToolTip = new ToolTip { Content = "Show Projec Explorer" },
                //    ContentID = "CidShowProjectsExplorer",
                //    IconSource = "pack://application:,,,/Images/Project.PNG",
                //    Priority = 20000,
                //    Command = new RelayCommand(ShowPE)
                //},
                //new ToolButton
                //{
                //    ToolTip = new ToolTip { Content = "Show Projec Explorer" },
                //    ContentID = "CidShowProjectsExplorer",
                //    IconSource = "pack://application:,,,/Images/Project.PNG",
                //    Priority = 20000,
                //    Command = new RelayCommand(ShowPE)
                //}

            });
            _menuItemServer.Add(RootMenusID.NShow, new[] {
                new CommandMenuItemVM
                {
                    ContentID = "CidShowProjectsExplorer",
                    Header = Properties.Resources.tProjectExplorer,
                    IconSource = "pack://application:,,,/Images/Project.PNG",
                    Priority = 0,
                    Command = new RelayCommand(ShowPE)
                },
            });
            _menuItemServer.Add(RootMenusID.NFile_Create, new MenuItemVM[] {
                new MenuOpenFileVM
                {
                    ContentID = "FOP",
                    Header = Properties.Resources.nfile_Open,
                    IconSource = "pack://application:,,,/Images/Project.PNG",
                    Title = Properties.Resources.nfile_Open,
                    Filter = "Text documents (.txt)|*.txt|Any documents (.doc)|*.doc|Any (*)|*",
                    DefaultExt = ".txt",
                    CustomPlaces = new object[] 
                    {
                        @"C:\Users\Public\Documents\Горизонт\WorkProg\Projects\",
                        @"C:\XE\Projects\Device2\_exe\Debug\Метрология\",
                        @"G:\Мой диск\mtr\",
                        new Guid("FDD39AD0-238F-46AF-ADB4-6C85480369C7"),//документы
                        new Guid("1777F761-68AD-4D8A-87BD-30B759FA33DD"),//избрвнное
                    }
                },
                new CommandMenuItemVM
                {
                    ContentID = "CidShowProjectsExplorer",
                    Header = Properties.Resources.nProject_New,
                    IconSource = "pack://application:,,,/Images/NewProject.PNG",
                    Priority = -1000,
                    Command = new RelayCommand(ShowPE)
                },
            });
        }
    }
    //public class ProjectsExplorerFactory //: IStaticContentClient
    //{
    //    private readonly IServiceProvider _serviceProvider;
    //    public ProjectsExplorerFactory(IServiceProvider sp) => _serviceProvider = sp;
    //    public object Content => _serviceProvider.GetRequiredService<ProjectsExplorer>();
    //    public ShowStrategy? AnchorableShowStrategy => ShowStrategy.Left;        
    //}
    public class ProjectsExplorerVM : ToolVM
    {

        public ProjectsExplorerVM()
        {
            Title = Properties.Resources.tProjectExplorer;
            IconSource = new Uri("pack://application:,,,/Images/Project16.png");
            CanFloat = false;
            CanDockAsTabbedDocument = false;
            CanClose = false;
            ShowStrategy = Core.ShowStrategy.Left;  
            ContentID = nameof(ProjectsExplorerVM);
            ToolTip = Properties.Resources.tProjectExplorer;
        }
    }
}
