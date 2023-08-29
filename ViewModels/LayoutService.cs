using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using Xceed.Wpf.AvalonDock.Layout;

namespace WorkProgMain.ViewModels
{
    //public class LayoutService : VMBase, ILayoutServer
    //{
    //    MainVindowVM _mv;
    //    public LayoutService(MainVindowVM mv)
    //    {
    //        _mv = mv;            
    //    }
    //    void ILayoutServer.AddToLayout(IEnumerable<IContentClient> items)
    //    {
    //        foreach(var contentclient in items) 
    //        {
    //            if (contentclient.Content is LayoutContent c && c.Content is FrameworkElement e
    //                && e.Width != double.NaN && e.Height != double.NaN)
    //            {

    //                c.FloatingWidth = e.Width;
    //                c.FloatingHeight = e.Height;
    //                c.FloatingTop = (SystemParameters.PrimaryScreenHeight - e.Height) / 2;
    //                c.FloatingLeft = (SystemParameters.PrimaryScreenWidth - e.Width) / 2;
    //                if (c is LayoutAnchorable la)
    //                {
    //                    la.AutoHideHeight = e.Height;
    //                    la.AutoHideWidth = e.Width;
    //                }
    //                e.Width = double.NaN;
    //                e.Height = double.NaN;
    //            }
    //            if (contentclient.Content is LayoutAnchorable a)
    //            {
    //                a.IsVisibleChanged += _mv.DocIsVisibleChanged;
    //                a.AddToLayout(dockManager, contentclient.anchorableShowStrategy ?? AnchorableShowStrategy.Most);                    
    //            }
    //            else if (contentclient is LayoutDocument d)
    //            {
    //                documentPane.Children.Add(d);
    //            }
    //            else
    //            {
    //                var ah = new LayoutAnchorable();
    //                ah.Content = contentclient;
    //                ah.Title = "Noname";
    //                ah.AddToLayout(dockManager, AnchorableShowStrategy.Most);
    //            }
    //        } 
    //    }
    //}
}
