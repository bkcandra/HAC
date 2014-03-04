using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class CategoriesSetupUC : System.Web.UI.UserControl
    {
        public SystemConstants.FormMode Mode
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAddEditMode.Value))
                {
                    return (SystemConstants.FormMode)Convert.ToInt32(hdnAddEditMode.Value);
                }
                else
                {
                    return SystemConstants.FormMode.New;
                }
            }
            set
            {
                hdnAddEditMode.Value = Convert.ToInt32(value).ToString();

                if (value == SystemConstants.FormMode.New)
                {
                    lblAddEditTitle.Text = "Add New Category";
                }
                else if (value == SystemConstants.FormMode.Edit)
                {
                    lblAddEditTitle.Text = "Edit Category";
                }
            }
        }

        public int CategoryRoot { get; set; }

        public AdministrationEDSC.CategoryDTRow GetData()
        {
            AdministrationEDSC.CategoryDTRow dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();

            if (!string.IsNullOrEmpty(hdnCategoryID.Value))
                dr.ID = Convert.ToInt32(hdnCategoryID.Value);
            dr.Name = txtAddEditName.Text;
            //  dr.Description = txtAddEditDescription.Text;
            return dr;
        }

        //public void SetData(AdministrationEDSC.BrandDTRow dr)
        //{
        //    hdnCategoryID.Value = dr.ID.ToString();

        //    if (!dr.IsNameNull())
        //    {
        //        txtAddEditName.Text = dr.Name;
        //       // lblAddEditName.Text = dr.Name;
        //    }

        //    //if (!dr.IsDescriptionNull())
        //    //{
        //    //   // txtAddEditDescription.Text = dr.Description;
        //    //   // lblAddEditDescription.Text = dr.Description;
        //    //}
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Mode == SystemConstants.FormMode.New)
                    SetDDL();
            }
        }

        protected void Page_init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.CategoryRoot] != null)
                CategoryRoot = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryRoot]);
        }

        private void SetDDL()
        {
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveAllCategories();
            dropDownParent.Items.Clear();

            ListItem li = new ListItem("Root", "0");
            dropDownParent.Items.Add(li);

            foreach (var dr in dt)
            {
                string name = "";
                if (dr.Level1ParentID != 0 && !dr.IsLevel1ParentNameNull())
                    name += dr.Level1ParentName + " >> ";

                if (dr.Level2ParentID != 0 && !dr.IsLevel2ParentNameNull())
                    name += dr.Level2ParentName + " >> ";

                name += dr.Name;
                li = new ListItem(name, dr.ID.ToString());
                dropDownParent.Items.Add(li);
            }
            if (CategoryRoot != 0)
                dropDownParent.SelectedValue = CategoryRoot.ToString();
        }

        internal void SetData(AdministrationEDSC.CategoryDTRow dr)
        {
            SetDDL();
            hdnCategoryID.Value = dr.ID.ToString();

            if (!dr.IsNameNull())
            {
                txtAddEditName.Text = dr.Name;
                lblAddEditName.Text = dr.Name;
            }

            if (Mode == SystemConstants.FormMode.Edit)
            {
                AdministrationEDSC.CategoryDTDataTable dt = new AdministrationEDSC.CategoryDTDataTable();
                dt = new AdministrationDAC().RetrieveAllCategories();

                int counter = 1;
                int parentID = 0;
                if (dr.Level2ParentID != 0)
                {
                    parentID = dr.Level2ParentID;
                }
                else if (dr.Level1ParentID != 0)
                {
                    parentID = dr.Level1ParentID;
                }

                bool find = false;
                foreach (AdministrationEDSC.CategoryDTRow brandDR in dt.Rows)
                {
                    if (brandDR.ID == parentID)
                    {
                        find = true;
                        break;
                    }
                    counter++;
                }
                if (find == false)
                    counter = 0;
                dropDownParent.SelectedIndex = counter;
                lblParent.Text = dropDownParent.SelectedItem.ToString();
            }
        }

        protected void lnkOk1_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.CategoryDTRow dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();
            dr = GetData();
            int parentID = Convert.ToInt32(dropDownParent.SelectedValue);

            string userName = SystemConstants.DevUser;
            //Membership.GetUser().UserName;

            AdministrationBFC bfc = new AdministrationBFC();

            if (Mode == SystemConstants.FormMode.New)
            {
                bfc.CreateCategory(parentID, userName, dr);
            }
            else if (Mode == SystemConstants.FormMode.Edit)
            {
                bfc.UpdateCategory(dr.ID, dr.Name, parentID, userName);
            }

            lblAddEditName.Text = txtAddEditName.Text;
            lblParent.Text = dropDownParent.SelectedItem.ToString();

            SetFormView();

        }

        private void SetFormView()
        {
            lblAddEditName.Visible = true;
            txtAddEditName.Visible = false;
            lblParent.Visible = true;
            dropDownParent.Visible = false;
            lnkOk1.Visible = false;
            lnkCancel1.Visible = false;
            lnkEdit.Visible = true;
            lnkCreateAnother.Visible = true;
            lnkBackToList.Visible = true;
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            if (Mode != SystemConstants.FormMode.Edit)
            {
                Mode = SystemConstants.FormMode.Edit;
                hdnCategoryID.Value = new AdministrationDAC().RetrieveLastCategoryID();
            }

            SetFormEdit();
        }

        private void SetFormEdit()
        {
            lblAddEditName.Visible = !true;
            txtAddEditName.Visible = !false;
            lblParent.Visible = !true;
            dropDownParent.Visible = !false;
            lnkOk1.Visible = !false;
            lnkCancel1.Visible = !false;
            lnkEdit.Visible = !true;
            lnkCreateAnother.Visible = !true;
            txtAddEditName.Text = lblAddEditName.Text;
            dropDownParent.SelectedIndex = 0;
            lnkBackToList.Visible = !true;
        }

        protected void lnkCreateAnother_Click(object sender, EventArgs e)
        {
            ClearField();

            SetFormEdit();
            SetDDL();

            Mode = SystemConstants.FormMode.New;
        }

        private void ClearField()
        {
            lblAddEditName.Text = "";
            txtAddEditName.Text = "";
            lblAddEditName.Text = "";
            lblParent.Text = "";



        }

        protected void lnkCancel1_Click(object sender, EventArgs e)
        {
            if (Mode == SystemConstants.FormMode.New)
                Response.Redirect("~/Categories/Default.aspx?" + SystemConstants.CategoryID + "=" + CategoryRoot);
            else if (Mode == SystemConstants.FormMode.Edit)
                SetFormView();
        }

        protected void lnkBackToList_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Categories/Default.aspx?" + SystemConstants.CategoryID + "=" + CategoryRoot);
        }
    }
}