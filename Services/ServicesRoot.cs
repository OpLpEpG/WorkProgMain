using Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WorkProgMain.ViewModels;
using WorkProgMain.Views;

namespace WorkProgMain.Services
{
    internal static class ServicesRoot
    {
        public static void Register(IServiceCollection services, GlobalSettings opt) 
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<MainVindowVM>();

            services.AddSingleton<IFormsServer>(sp => DockManagerVM.Instance!);

            services.AddSingleton<MenuVM>();
            services.AddTransient<MenuItemVM>();
            services.AddTransient<CommandMenuItemVM>();
            services.AddTransient<OnSubMenuOpenMenuItemVM>();

            services.AddTransient<ProjectsExplorerVM>();
            services.AddTransient<IMenuItemClient, ProjectsExplorerMenuFactory>();
            services.AddTransient<IFormsRegistrator, FormsRegistrator<ProjectsExplorerVM>>();

            if (opt.Logging.Box.Error)
            {
                services.AddTransient<ExceptLogVM>();
                services.AddTransient<IMenuItemClient, ExceptLogMenuFactory>();
                services.AddTransient<IFormsRegistrator, FormsRegistrator<ExceptLogVM>>();
            }
            if (opt.Logging.Box.Trace)
            {
                services.AddTransient<TraceLogVM>();
                services.AddTransient<IMenuItemClient, TraceLogMenuFactory>();
                services.AddTransient<IFormsRegistrator, FormsRegistrator<TraceLogVM>>();
            }
            if (opt.Logging.Box.Info)
            {
                services.AddTransient<InfoLogVM>();
                services.AddTransient<IMenuItemClient, InfoLogMenuFactory>();
                services.AddTransient<IFormsRegistrator, FormsRegistrator<InfoLogVM>>();
            }
            // services.AddTransient<IStaticContentClient,ProjectsExplorerFactory>();
        }
    }
}
