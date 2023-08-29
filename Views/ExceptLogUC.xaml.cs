using Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
using TextBlockLogging;
using WorkProgMain.ViewModels;
using WpfDialogs;

namespace WorkProgMain.Views
{
    /// <summary>
    /// Логика взаимодействия для ExceptLogUC.xaml
    /// </summary>
    public partial class TextLogUC : UserControl 
    {
        public static LogTextBlock GetLogTextBlock(TextLogVM vm)
        {
            ILogTextBlockService s = VMBase.ServiceProvider.GetRequiredService<ILogTextBlockService>();

            var t = s.GetLogTextBlock(vm.ContentID!);

            if (t == null)
            {
                t = new LogTextBlock();
                s.SetLogTextBlock(vm.ContentID!, t);
            }
            BindingOperations.ClearBinding(t, LogTextBlock.FreezeProperty);
            Binding binding = new Binding();
            binding.Source = vm;
            binding.Path = new PropertyPath("Freeze");
            binding.Mode = BindingMode.OneWay;
            t.SetBinding(LogTextBlock.FreezeProperty, binding);
            vm.OnClear += t.Inlines.Clear;

            return t;
        }
        public TextLogUC()
        {
            InitializeComponent();            
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            sve.Content = GetLogTextBlock((TextLogVM)DataContext);
        }
        private void Button_Click_Err(object sender, RoutedEventArgs e)
        {
            var l = VMBase.ServiceProvider.GetRequiredService<ILogger<TextLogUC>>();
            l.LogInformation("LogInformation");
            l.LogTrace("msg {}", sender);
            throw new NotImplementedException();    
        }
        private void mFreeze1_Click(object sender, RoutedEventArgs e)
        {
            TextLogVM vm = (TextLogVM)DataContext;
            vm.Freeze = !vm.Freeze;
        }

        private void mClose_Click(object sender, RoutedEventArgs e)
        {
            TextLogVM vm = (TextLogVM)DataContext;
             vm.Close();
        }
    }       
}
