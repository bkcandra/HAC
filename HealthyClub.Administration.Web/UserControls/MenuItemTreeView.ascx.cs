using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class MenuItemTreeView : System.Web.UI.UserControl
    {
        public delegate void MenuItemHandler(int menuItemID);
        public event MenuItemHandler TreeNodeClicked;

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void Refresh(int Menutype)
        {
            LoadTree(Menutype);
            TreeView1.ExpandAll();
        }

        private void LoadTree(int menutype)
        {
            TreeView1.Nodes.Clear();
            AdministrationDAC dac = new AdministrationDAC();
            var menus = dac.RetrieveMenuExplorers(menutype);


            TreeNode newNode = new TreeNode("Home", "0");
            TreeView1.Nodes.Add(newNode);
            BuildMenuItemTree(null, 1, 0, menus, 0);
            //newNode = new TreeNode("Contact Us", "999");
            //TreeView1.Nodes.Add(newNode);

        }

        private void BuildMenuItemTree(TreeNode node, int level, int parentID, AdministrationEDSC.v_MenuDTDataTable menuItemDts, int count)
        {

            count++;

            //if (count == menuItemDts.Count())
            //    return;



            IEnumerable<AdministrationEDSC.v_MenuDTRow> filteredMenuItem = null;

            switch (level)
            {
                case 1: filteredMenuItem = from m in menuItemDts
                                           where m.ParentMenuID == parentID
                                           select m;
                    break;
                case 2:
                    filteredMenuItem = from m in menuItemDts
                                       where m.ParentMenuID == parentID
                                       select m;
                    break;
            }

            foreach (var dr in filteredMenuItem)
            {
                TreeNode newNode = new TreeNode(dr.LinkText, dr.ID.ToString());

                if (node == null)
                    TreeView1.Nodes.Add(newNode);
                else
                    node.ChildNodes.Add(newNode);

                if (dr.ParentMenuID == 0)
                {
                    level = 2;
                }
                else
                {
                    level = 1;
                }
                BuildMenuItemTree(newNode, level, dr.ID, menuItemDts, count);

            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

            if (TreeNodeClicked != null)
            {
                TreeNodeClicked(Convert.ToInt32(TreeView1.SelectedNode.Value));
            }
        }
    }
}