using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;
using System.Collections;
using Core;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Intrinsics.X86;
using TextBlockLogging;

//namespace WorkProgMain.Views.Pane
//{
//    class PanesTemplateSelector : DataTemplateSelector
//    {
//        public override System.Windows.DataTemplate SelectTemplate(object item, System.Windows.DependencyObject container)
//        {
//            var name = item == null ? null : item.GetType().Name;
//            if (name != null && FormResource.Dictionary.Contains(name+"Template"))
//            {
//                return (DataTemplate)FormResource.Dictionary[name + "Template"];
//            }
//            return base.SelectTemplate(item, container);
//        }
//    }
//}

