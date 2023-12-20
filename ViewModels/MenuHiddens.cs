using CommunityToolkit.Mvvm.Input;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WorkProgMain.ViewModels
{
    public class MenuHidden : CommandMenuItemVM { }


    public class MenuHiddens: OnSubMenuOpenMenuItemVM
    {
        static MenuHiddens? Instance;

        static MenuHiddens()
        {
            MenuTemplateSelector.Dictionary.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri("pack://application:,,,/Views/MenusResource.xaml")
            });

        }
        public MenuHiddens() 
        {
            Instance = this;
          //  IconSource = "pack://application:,,,/Images/DockPane.PNG";
            Priority = 20;
            Header = Properties.Resources.m_Hidden;
            ContentID = RootMenusID.NHidden;

            VMBaseForms.OnVisibleChanged += s => IsEnable = DockManagerVM.Instance!.Hiddens.Count() > 0;
                                       
            Items.Add(
                        new MenuHidden
                        {
                            Header = Properties.Resources.m_ShowAll,
                            Priority = 0,
                            Command = new RelayCommand(() =>
                            {
                                for (int i = 2; i < Instance!.Items.Count; i++)
                                    (Instance.Items[i] as CommandMenuItemVM)?.Command?.Execute(null);
                            })
                        });
            Items.Add(  new Separator { Priority = 0 });

            OnSubMenuAction = () =>
            {
                while (Instance!.Items.Count > 2) Instance.Items.RemoveAt(Instance.Items.Count - 1);

                foreach (var a in DockManagerVM.Instance!.Hiddens)
                //foreach (var a in dockManager.Layout.Hidden)
                {
                    Instance.Items.Add(new MenuHidden
                    {
                        Header = a.Title,
                        Priority = 100,
                        //Command = new RelayCommand(() => a.Show()),
                        Command = new RelayCommand(() => a.IsVisible = true),
                    });
                }
            };
        }
    }
}
