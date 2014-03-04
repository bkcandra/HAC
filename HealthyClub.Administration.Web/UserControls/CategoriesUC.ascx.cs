using HealthyClub.Administration.BF;
using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class CategoriesUC : System.Web.UI.UserControl
    {
        public int CurrentCategoryID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnCurrentCategoryID.Value))
                    return Convert.ToInt32(hdnCurrentCategoryID.Value);
                else
                    return 0;
            }
            set
            {
                hdnCurrentCategoryID.Value = value.ToString();

                if (value != 0)
                {
                    divDataView.Visible = true;
                    lblSubCategory.Visible = true;

                }
                else
                {
                    divDataView.Visible = false;
                    lblSubCategory.Visible = !true;

                }
            }
        }

        public string SortExpression
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSortParameter.Value))
                    return hdnSortParameter.Value;
                else
                    return "";
            }
            set
            {
                hdnSortParameter.Value = value;
            }
        }

        //public int AddEditMode
        //{
        //    get
        //    {
        //        if (!string.IsNullOrEmpty(hdnAddEditMode.Value))
        //            return Convert.ToInt32(hdnAddEditMode.Value);
        //        else
        //            return 0;
        //    }
        //    set
        //    {
        //        hdnAddEditMode.Value = value.ToString();
        //    }
        //}

        private void Refresh()
        {
            if (!IsPostBack)
            {
                NavigationUC1.SetNavigation(0);

            }
            SetDataSource();
            GridView1.DataBind();
            SetDDL();
            if (Maxhierarchy())
            {
                divLnkMenu.Visible = false;
                divPopMoveDestination.Visible = false;
            }
            else
            {
                divLnkMenu.Visible = true;
                divPopMoveDestination.Visible = true;
            }
            //RefreshPopUpPage();

        }

        private bool Maxhierarchy()
        {
            if (CurrentCategoryID != 0)
            {
                int hierarchy = new AdministrationDAC().RetrieveCategoryLevel(CurrentCategoryID);
                if (hierarchy >= 2)
                {
                    return true;
                }
                else if (hierarchy == SystemConstants.intError)
                {
                    Response.Redirect("~");
                    return false;
                }
                else return false;
            }
            else return false;
        }

        //private void RefreshPopUpPage()
        //{
        //    txtAddEditName.Text = "";
        //}

        private void SetDataSource()
        {
            GridView1.DataSource = ods;
            ods.TypeName = typeof(AdministrationDAC).FullName;
            ods.SelectParameters.Clear();
            ods.SelectParameters.Add("immediateParentCategoryID", CurrentCategoryID.ToString());
            ods.SelectParameters.Add("sortExpression", SortExpression);
            ods.EnablePaging = GridView1.AllowPaging;
            ods.SelectMethod = "RetrieveSubCategories";
            ods.SelectCountMethod = "RetrieveSubCategoriesCount";
            ods.StartRowIndexParameterName = "startIndex";
            ods.MaximumRowsParameterName = "amount";

        }

        private void SetDDL()
        {
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationDAC().RetrieveAllCategories();
            DropDownList1.Items.Clear();
            foreach (var dr in dt)
            {
                if (dr.ID != CurrentCategoryID && dr.Level1ParentID != CurrentCategoryID && dr.Level2ParentID != CurrentCategoryID)
                {
                    string name = "";
                    if (dr.Level1ParentID != 0 && !dr.IsLevel1ParentNameNull())
                        name += dr.Level1ParentName + " >> ";

                    if (dr.Level2ParentID != 0 && !dr.IsLevel2ParentNameNull())
                        name += dr.Level2ParentName + " >> ";

                    name += dr.Name;
                    ListItem li = new ListItem(name, dr.ID.ToString());
                    DropDownList1.Items.Add(li);
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.CategoryID] != null)
                CurrentCategoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Categories/CategorySetup.aspx?" + SystemConstants.CategoryID + "=0&" + SystemConstants.CategoryRoot + "=" + CurrentCategoryID);
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.CategoryDTDataTable dt = GetSelected();
            foreach (AdministrationEDSC.CategoryDTRow dr in dt)
            {
                bool complete = bfc.DeleteCategories(dr.ID);
                if (complete)
                {
                    divSuccess.Visible = complete;
                    divError.Visible = !complete;
                    lblSuccess.Text = "Selected category(es) has been successfully deleted";
                }
                else
                {
                    divSuccess.Visible = complete;
                    divError.Visible = !complete;
                    lblError.Text = "Cannot delete selected category(es), one or more activities are linked to this category(es). Consider to delete or modify the activities and retry again.";
                }

            }
            Refresh();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RenameCategory" || e.CommandName == "ViewSubCategories" || e.CommandName == "ViewSpecification")
            {
                LinkButton lnkRename = e.CommandSource as LinkButton;
                GridViewRow row = lnkRename.Parent.Parent as GridViewRow;
                HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                LinkButton lnkName = row.FindControl("lnkName") as LinkButton;
                if (e.CommandName == "RenameCategory")
                {
                    Response.Redirect("~/Categories/CategorySetup.aspx?" + SystemConstants.CategoryID + "=" +
                   hdnID.Value);
                    //lblAddEditTitle.Text = "Rename Category";
                    //AddEditMode = 1;
                    //hdnCategoryID.Value = hdnID.Value;
                    //txtAddEditName.Text = lnkName.Text;
                    //ModalPopupExtender2.Show();
                }
                else if (e.CommandName == "ViewSubCategories")
                {
                    CurrentCategoryID = Convert.ToInt32(hdnID.Value);
                    NavigationUC1.CategoryID = CurrentCategoryID;
                    lblNameCategory.Text = new AdministrationDAC().RetrieveCategory(CurrentCategoryID).Name;
                    Refresh();
                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        public void btnMove_Click(object sender, EventArgs e)
        {

            AdministrationBFC bfc = new AdministrationBFC();
            AdministrationEDSC.CategoryDTDataTable dt = GetSelected();
            foreach (AdministrationEDSC.CategoryDTRow dr in dt)
            {
                int destinationID = Convert.ToInt32(DropDownList1.SelectedValue);
                bfc.MoveCategory(dr.ID, destinationID, SystemConstants.DevUser);
            }
            Refresh();
        }

        public void btnCancelPopUp_Click(object sender, EventArgs e)
        {
            // txtAddEditName.Text = "";
        }

        public void btnMove1_Click(object sender, EventArgs e)
        {
            //AdministrationEDSC.CategoryDTRow dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();
            //AdministrationBFC bfc = new AdministrationBFC();

            //dr.Name = txtAddEditName.Text;
            //if (AddEditMode == 0)
            //{
            //    bfc.CreateCategory(CurrentCategoryID, Membership.GetUser().UserName, dr);
            //    Refresh();
            //    lblSucces.Visible = true;
            //    lblSucces.Text = "Data Added, " + DateTime.Now;
            //}
            //else if (AddEditMode == 1)
            //{
            //    int categoryID = Convert.ToInt32(hdnCategoryID.Value);
            //    bfc.UpdateCategory(categoryID, dr.Name, Membership.GetUser().UserName);
            //    AddEditMode = 0;
            //    Refresh();
            //    lblSucces.Visible = true;
            //    lblSucces.Text = "Updated, " + DateTime.Now;
            //}
        }

        public AdministrationEDSC.CategoryDTDataTable GetSelected()
        {
            AdministrationEDSC.CategoryDTDataTable dt = new AdministrationEDSC.CategoryDTDataTable();
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox CheckBox1 = row.FindControl("CheckBox1") as CheckBox;
                    HiddenField hdnID = row.FindControl("hdnID") as HiddenField;
                    var dr = dt.NewCategoryDTRow();
                    if (CheckBox1.Checked)
                    {
                        dr.ID = Convert.ToInt32(hdnID.Value);
                        dt.AddCategoryDTRow(dr);
                    }
                }
            }
            return dt;
        }

        protected void NavigationUC1_RefreshNavigation(int ID)
        {
            CurrentCategoryID = ID;
            Refresh();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Refresh();
        }

        protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
        {
            SortExpression = e.SortExpression;
            SetDataSource();
            GridView1.DataBind();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            txtNameCategory.Text = lblNameCategory.Text;
            lblNameCategory.Visible = false;
            txtNameCategory.Visible = true;
            lnkEdit.Visible = false;
            lnkSaveCategory.Visible = true;
            lnkCancelCategory.Visible = true;
        }

        protected void lnkSaveCategory_Click(object sender, EventArgs e)
        {
            AdministrationEDSC.CategoryDTRow dr = new AdministrationEDSC.CategoryDTDataTable().NewCategoryDTRow();

            dr.ID = CurrentCategoryID;
            dr.Name = txtNameCategory.Text;
            dr.ModifiedBy = Membership.GetUser().UserName;
            dr.ModifiedDateTime = DateTime.Now;
            new AdministrationDAC().UpdateCategoryName(dr);
            NavigationUC1.CategoryID = CurrentCategoryID;
            Refresh();
            lblNameCategory.Text = txtNameCategory.Text;
            SetUpFormView();
        }

        private void SetUpFormView()
        {
            lblNameCategory.Visible = !false;
            txtNameCategory.Visible = !true;
            lnkEdit.Visible = !false;
            lnkSaveCategory.Visible = !true;
            lnkCancelCategory.Visible = !true;
        }

        protected void lnkCancelCategory_Click(object sender, EventArgs e)
        {
            SetUpFormView();
        }
    }
}