using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HealthyClub.Customer.EDS;
using HealthyClub.Customer.DA;
using HealthyClub.Utility;
using System.Collections;
using System.Drawing;
using HealthyClub.Customer.BF;

namespace HealthyClub.Web.UserControls
{
    public partial class RewardSidebar : System.Web.UI.UserControl
    {
        public delegate void SidebarFilter(int categoryID, int ageFrom, int ageTo, string RewardType, bool Filtered);
        public event SidebarFilter PushFilter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Refresh();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (Request.QueryString[SystemConstants.CategoryID] != null)
                CategoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);
            if (Request.QueryString[SystemConstants.Filtered] != null)
                Filtered = Convert.ToBoolean(Request.QueryString[SystemConstants.Filtered]);
            if (Request.QueryString[SystemConstants.RewType] != null)
                RewardType = Request.QueryString[SystemConstants.RewType];
            if (Request.QueryString[SystemConstants.AgeFrom] != null)
                ageFrom = Convert.ToInt32(Request.QueryString[SystemConstants.AgeFrom]);
            if (Request.QueryString[SystemConstants.AgeTo] != null)
                ageTo = Convert.ToInt32(Request.QueryString[SystemConstants.AgeTo]);
        }
    
          
        private void Refresh()
        {
           
            
            initStartingRef();
        
            ShowFilter();

        }

  

        private void setFilter()
        {
            
            if (!string.IsNullOrEmpty(categories.SelectedItem.Value))
                CategoryID = Convert.ToInt32(categories.SelectedItem.Value);

            if (!string.IsNullOrEmpty(RewardType) && !String.IsNullOrEmpty(getRewardType()))
                RewardType += "|" + getRewardType();

            if (txtAgeFrom.Text == "" && txtAgeTo.Text != "")
            {
                ageFrom = 1;
                ageTo = Convert.ToInt32(txtAgeTo.Text);
            
            }
            else if (txtAgeFrom.Text != "" && txtAgeTo.Text == "")
            {
                ageFrom = Convert.ToInt32(txtAgeFrom.Text);
                ageTo = 1000000000;
            }


            if (!string.IsNullOrEmpty(txtAgeFrom.Text) &&  (!string.IsNullOrEmpty(txtAgeTo.Text)))
            {
                ageFrom = Convert.ToInt32(txtAgeFrom.Text);
                ageTo = Convert.ToInt32(txtAgeTo.Text);
            }
           
            
        }


        protected void lnkResetAge_Click(object sender, EventArgs e)
        {
            ageFrom = 0;
            ageTo = 99;
            txtAgeFrom.Text = txtAgeTo.Text = "";

            setFilter();
            ShowFilter();
            if (PushFilter != null)
                if (dtFrom == SystemConstants.nodate && dtTo == SystemConstants.nodate )
                    PushFilter(CategoryID, ageFrom, ageTo, RewardType, false);
                else
                    PushFilter(CategoryID, ageFrom, ageTo,RewardType, true);
        }


        private string getRewardType()
        {
            string selected = "";
            string separator = "|";
            
                if (chkinternal.Checked)
                {
                    if (!String.IsNullOrEmpty(selected))
                        selected += separator;
                    selected += "1";
                }
                if (chkexternal.Checked)
                {
                    if (!String.IsNullOrEmpty(selected))
                        selected += separator;
                    selected += "2";
                }
               


            return selected;
        }


        private void ShowFilter()
        {
            if(ageFrom == 1 && ageTo != 99)
            {
                txtAgeFrom.Text = "0";
                txtAgeTo.Text = ageTo.ToString();
                lnkResetAge.Visible = true;
            
            }
            else if(ageFrom != 0 && ageTo == 1000000000)
            {
                txtAgeFrom.Text = ageFrom.ToString();
                txtAgeTo.Text = "Infinity";
                lnkResetAge.Visible = true;
            
            }
            else if (ageFrom != 0 && ageTo != 99)
            {
                lnkResetAge.Visible = true;
              
                txtAgeFrom.Text = ageFrom.ToString();
                txtAgeTo.Text = ageTo.ToString();
                if (ageFrom > ageTo)
                {
                    lnkResetAge.Text = "Please Enter valid maximum and minimum Reward Points";
                    txtAgeFrom.Text = "";
                    txtAgeTo.Text = "";
                }
            }
            else
            {
                lnkResetAge.Visible = false;
            }

            string[] valarr = RewardType.Split('|');

            if (RewardType != "0")
            {
                    if (valarr.Contains("1"))
                    {
                        chkinternal.Checked = true;
                    }
                    if (valarr.Contains("2"))
                    {
                        chkexternal.Checked = true;
                    }
                 
            }
            
        }
    


        private void initStartingRef()
        {
            StartingRef = CategoryID;

           
        }

       
        protected void Apply_Click(object sender, EventArgs e)
        {
            RewardType = "";

            
            setFilter();
            ShowFilter();
            if (PushFilter != null)
            {
                PushFilter(CategoryID, ageFrom, ageTo, RewardType, true);
            }
        }

        public bool ShowAllCategoryListing
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnShowListingAllCategory.Value))
                    return Convert.ToBoolean(hdnShowListingAllCategory.Value);
                else return true;
            }
            set
            {
                hdnShowListingAllCategory.Value = value.ToString();
            }
        }

        public bool ShowCategoryListingTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnShowListingTitle.Value))
                    return Convert.ToBoolean(hdnShowListingTitle.Value);
                else return true;
            }
            set
            {
                hdnShowListingTitle.Value = value.ToString();
            }
        }

        public bool ShowTitle
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnShowTitle.Value))
                    return Convert.ToBoolean(hdnShowTitle.Value);
                else return true;
            }
            set
            {
                hdnShowTitle.Value = value.ToString();
            }
        }
        public string RewardType
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnRewardType.Value))
                    return hdnRewardType.Value.ToString();
                else return "0";
            }
            set
            {
                hdnRewardType.Value = value.ToString();
            }
        }

        public bool Filtered
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFiltered.Value))
                    return Convert.ToBoolean(hdnFiltered.Value);
                else return false;
            }
            set
            {
                hdnFiltered.Value = value.ToString();
            }
        }

        

        public int StartingRef
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnStartingRef.Value))
                    return Convert.ToInt32(hdnStartingRef.Value);
                else return 1;
            }
            set
            {
                if (Request.QueryString[SystemConstants.CategoryID] != null)
                {
                    CategoryID = Convert.ToInt32(Request.QueryString[SystemConstants.CategoryID]);
                }

                if (Request.QueryString[SystemConstants.ProviderID] != null)
                {
                    ProviderID = Convert.ToInt32(Request.QueryString[SystemConstants.ProviderID]);

                }
                /*
                if (CategoryID == 0)
                    hdnStartingRef.Value = value.ToString();
                else
                {
                    var cat = new CustomerDAC().RetrieveCategory(CategoryID);
                    hdnStartingRef.Value = cat.Level1ParentID.ToString();
                }*/

                //keep load all category nodes
                hdnStartingRef.Value = "0";
                CustomerEDSC.v_CategoryExplorerDTDataTable categoryDT = new CustomerDAC().RetrieveCategories(StartingRef);
                CustomerEDSC.v_CategoryExplorerDTRow categoryDR = new CustomerDAC().RetrieveCategory(StartingRef);


                if (ShowAllCategoryListing)
                {

                    ListItem selectedListItem = categories.Items.FindByValue("allcategories");
                    if (selectedListItem != null)
                    {
                        selectedListItem.Selected = true;

                    }

                }
                LoadList(categoryDR);
              
            }
        }

        public int CategoryID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnCategoryID.Value))
                    return Convert.ToInt32(hdnCategoryID.Value);
                else return 0;
            }
            set
            {
                hdnCategoryID.Value = value.ToString();
            }
        }


        public bool filterError
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnFilterError.Value))
                    return Convert.ToBoolean(hdnFilterError.Value);
                else return false;
            }
            set
            {
                hdnFilterError.Value = value.ToString();
            }
        }

        public int ageFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAgeFrom.Value))
                    return Convert.ToInt32(hdnAgeFrom.Value);
                else return 0;
            }
            set
            {
                hdnAgeFrom.Value = value.ToString();
            }
        }

        public int ageTo
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnAgeTo.Value))
                    return Convert.ToInt32(hdnAgeTo.Value);
                else return 99;
            }
            set
            {
                hdnAgeTo.Value = value.ToString();
            }
        }

        public int ShowLevel
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnShowLevel.Value))
                    return Convert.ToInt32(hdnShowLevel.Value);
                else return 1;
            }
            set
            {
                hdnShowLevel.Value = value.ToString();
            }
        }

        public int ProviderID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnProvider.Value))
                    return Convert.ToInt32(hdnProvider.Value);
                else return 0;
            }
            set
            {
                hdnProvider.Value = value.ToString();
            }
        }

        public int TagID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTagID.Value))
                    return Convert.ToInt32(hdnTagID.Value);
                else return 0;
            }
            set
            {
                hdnTagID.Value = value.ToString();
            }
        }

        public DateTime dtFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnDTFrom.Value))
                    return Convert.ToDateTime(hdnDTFrom.Value);
                else return SystemConstants.nodate;
            }
            set
            {
                hdnDTFrom.Value = value.ToString();
            }
        }

        public DateTime dtTo
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnDTTo.Value))
                    return Convert.ToDateTime(hdnDTTo.Value);
                else return SystemConstants.nodate;
            }
            set
            {
                hdnDTTo.Value = value.ToString();
            }
        }

        private List<string> GetExpandedList()
        {
            string sessionName = "CategoryList_Section_" + CategoryID;

            if (Session[sessionName] == null)
            {
                Session[sessionName] = new List<string>();
            }

            return Session[sessionName] as List<string>;
        }

        private bool CheckIfStateExpanded(string treeValue)
        {
            var list = GetExpandedList();

            return list.Contains(treeValue);
        }

        private void RemoveFromExpandState(string treeValue)
        {
            var list = GetExpandedList();

            if (list.Contains(treeValue))
                list.Remove(treeValue);
        }

        private void ResetExpandState()
        {
            var list = GetExpandedList();
            list.Clear();
        }

        private void AddToExpandState(string treeNodeValue)
        {
            var list = GetExpandedList();

            if (!list.Contains(treeNodeValue))
                list.Add(treeNodeValue);
        }

        private void LoadList(CustomerEDSC.v_CategoryExplorerDTRow categoryDR)
        {
            


            if (categoryDR != null)
            {
                //create root
                if (ShowCategoryListingTitle)
                {
                    //string url = "~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + categoryDR.ID.ToString();
                    //node = new TreeNode(categoryDR.Name, categoryDR.ID.ToString(), null, null, null);
                   ListItem list1 = new ListItem(categoryDR.Name, categoryDR.ID.ToString());
                   
                   categories.Items.Add(list1);

                    if (categoryDR.ID == Convert.ToInt32(Session[SystemConstants.s_CurrentCategorySelected]))
                        list1.Selected = true;
                   
                }
            }

            
        }

    
        protected void categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            setFilter();
            ShowFilter();
            if (PushFilter != null)
            {
                PushFilter(CategoryID, ageFrom, ageTo, RewardType, true);
            }

            //Session[SystemConstants.s_CurrentCategorySelected] = Convert.ToInt32(TreeView1.SelectedNode.Value);
            //Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + TreeView1.SelectedNode.Value);
        }


    public static class Alert1
    {

        /// <summary>
        /// Shows a client-side JavaScript alert in the browser.
        /// </summary>
        /// <param name="message">The message to appear in the alert.</param>
        public static void Show(string message)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">Alert1('" + cleanMessage + "');</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Alert1), "alert", script);
            }
        }

    }
    }
}