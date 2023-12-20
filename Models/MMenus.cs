using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkProgMain.Models
{
    public static class MMenus
    {
        public static void CreateMenusStructure()
        {
            RootMenus.Items.AddRange(new[]
            {
                new rootMenu(RootMenusID.ROOT, RootMenusID.NFile, Properties.Resources.m_File, 0),
                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Create, Properties.Resources.nfile_Create, 100),
                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Open, Properties.Resources.nfile_Open, 101),
                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Add, Properties.Resources.nfile_Add, 300),

                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Close, Properties.Resources.m_File, 500),
                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_CloseProject, Properties.Resources.m_File, 500),

                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Last_Projects, Properties.Resources.nfile_last_Projects, 700),
                    new rootMenu(RootMenusID.NFile, RootMenusID.NFile_Last_File, Properties.Resources.nfile_last_Files, 701),

                new rootMenu(RootMenusID.ROOT, RootMenusID.NShow, Properties.Resources.m_Show, 10),
                    new rootMenu(RootMenusID.NShow, RootMenusID.NDebugs, Properties.Resources.m_Debugs, 100),
            });

        }
    }
}
