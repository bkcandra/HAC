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


namespace HealthyClub.Web.UserControls
{
    public partial class ActivityListSidebar : System.Web.UI.UserControl
    {
        public delegate void SidebarFilter(int categoryID, int ageFrom, int ageTo, string sinitddlsububurbID, DateTime from, DateTime to, TimeSpan tmFrom, TimeSpan tmTo, ListItemCollection filteredDays, bool Filtered);
        public event SidebarFilter ApplyFilter;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                Refresh();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            //Catch Day filter
            //filteredDays = new ListItemCollection();
            //if (Request.QueryString[SystemConstants.MondayisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.MondayisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Monday.ToString(), DayOfWeek.Monday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.TuesdayisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.TuesdayisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Tuesday.ToString(), DayOfWeek.Tuesday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.WedisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.WedisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Wednesday.ToString(), DayOfWeek.Wednesday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.ThurisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.ThurisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Thursday.ToString(), DayOfWeek.Thursday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.FriisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.FriisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Friday.ToString(), DayOfWeek.Friday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.SatisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.SatisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Saturday.ToString(), DayOfWeek.Saturday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.SunisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.SunisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(DayOfWeek.Sunday.ToString(), DayOfWeek.Sunday.ToString());
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = false;
            //    }
            //}
            //if (Request.QueryString[SystemConstants.AnyisFiltered] != null)
            //{
            //    if (Convert.ToInt32(Request.QueryString[SystemConstants.AnyisFiltered]) == 1)
            //    {
            //        ListItem day = new ListItem(SystemConstants.AnyisFiltered, SystemConstants.AnyisFiltered);
            //        filteredDays.Add(day);
            //        chkAnyday.Checked = true;
            //    }
            //}
        }

        private void Refresh()
        {
            initDDLTime();
            initDDLSuburbs();
            initStartingRef();
            setFilteredDays();
            ShowFilter();

        }

        private void ShowFilter()
        {
            //if (ageFrom != 0 || ageTo != 99)
            //{
            //    imgbtnResetAge.Visible = true;
            //    txtAgeFrom.Text = ageFrom.ToString();
            //    txtAgeTo.Text = ageTo.ToString();
            //}
            //else
            //{
            //    imgbtnResetAge.Visible = false;
            //}



            if (!chkAnyday.Checked)
                foreach (ListItem day in filteredDays)
                {
                    if (day.Text == DayOfWeek.Monday.ToString())
                    {
                        chkMonday.Checked = true;
                    }
                    else if (day.Text == DayOfWeek.Tuesday.ToString())
                    {
                        chkTuesday.Checked = true;
                    }
                    else if (day.Text == DayOfWeek.Wednesday.ToString())
                    {
                        chkWebnesday.Checked = true;
                    }
                    else if (day.Text == DayOfWeek.Thursday.ToString())
                    {
                        chkThursday.Checked = true;
                    }
                    else if (day.Text == DayOfWeek.Friday.ToString())
                    {
                        chkFriday.Checked = true;
                    }
                    else if (day.Text == DayOfWeek.Saturday.ToString())
                    {
                        chkSaturday.Checked = true;
                    }
                    else if (day.Text == DayOfWeek.Sunday.ToString())
                    {
                        chkSunday.Checked = true;
                    }
                }


            if ((dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate) || (tmFrom != SystemConstants.nodate.TimeOfDay && tmTo != SystemConstants.nodate.TimeOfDay))
            {
                imgbtnResetDate.Visible = false;
                if (dtFrom != SystemConstants.nodate && dtTo != SystemConstants.nodate)
                {
                    if (dtFrom.Date != SystemConstants.nodate.Date)
                        txtCalendarFrom.Text = dtFrom.Date.ToShortDateString();
                    if (dtTo.Date != SystemConstants.nodate.Date)
                        txtCalendarTo.Text = dtTo.Date.ToShortDateString();

                    imgbtnResetDate.Visible = true;
                }

                if (tmFrom != SystemConstants.nodate.TimeOfDay && tmTo != SystemConstants.nodate.TimeOfDay)
                {
                    imgbtnResetDate.Visible = true;
                    if (tmFrom != SystemConstants.nodate.Date.TimeOfDay)
                        ddlTimeStart.SelectedItem.Text = tmFrom.Hours.ToString("00") + ":" + tmFrom.Minutes.ToString("00");
                    if (tmTo != SystemConstants.nodate.Date.TimeOfDay)
                        ddlTimeEnds.SelectedItem.Text = tmTo.Hours.ToString("00") + ":" + tmTo.Minutes.ToString("00");

                }
            }







            string[] valarr = SuburbID.Split('|');

            if (SuburbID != "0")
            {
                foreach (ListItem item in DropDownCheckBoxes1.Items)
                {
                    if (valarr.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
                imgbtnLocation.Visible = true;
            }
            else
                imgbtnLocation.Visible = false;
        }

        private void initDDLTime()
        {
            ddlTimeStart.Items.Add("Start Time");
            ddlTimeEnds.Items.Add("End   Time");
            //ddlFrom
            for (int hour = 1; hour <= 24; hour++)
            {
                for (int minutes = 1; minutes <= 2; minutes++)
                {
                    if (minutes == 1)
                        ddlTimeStart.Items.Add((hour - 1).ToString("00") + ":00");
                    else if (minutes == 2)
                        ddlTimeStart.Items.Add((hour - 1).ToString("00") + ":30");
                }
                //ddlTimeStart.SelectedIndex = 1;
            }
            //ddlTo
            for (int hour2 = 1; hour2 <= 24; hour2++)
            {
                for (int minutes2 = 1; minutes2 <= 2; minutes2++)
                {
                    if (minutes2 == 1)
                        ddlTimeEnds.Items.Add((hour2 - 1).ToString("00") + ":00");
                    else if (minutes2 == 2)
                        ddlTimeEnds.Items.Add((hour2 - 1).ToString("00") + ":30");
                }
            }
            //ddlTimeEnds.SelectedIndex = 2;
        }

        private void initStartingRef()
        {
            StartingRef = CategoryID;
        }

        private void initDDLSuburbs()
        {
            CustomerEDSC.v_SuburbExplorerDTDataTable dt = new CustomerDAC().RetrieveSuburbs();

            CustomerEDSC.v_SuburbExplorerDTDataTable dtSub = new CustomerDAC().RetrieveSuburbs();

            ListItemCollection subList = new ListItemCollection();


            DropDownCheckBoxes1.DataSource = dtSub;
            DropDownCheckBoxes1.DataTextField = "Name";
            DropDownCheckBoxes1.DataValueField = "ID";

            DropDownCheckBoxes1.DataBind();


        }

        protected void Apply_Click(object sender, EventArgs e)
        {
            SuburbID = "";
            if (filteredDays != null)
                filteredDays.Clear();
            setFilter();
            //ShowFilter();

            if (ApplyFilter != null)
            {
                ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, tmFrom, tmTo, filteredDays, true);
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

        public ListItemCollection filteredDays { get; set; }

        public int StartingRef
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnStartingRef.Value))
                    return Convert.ToInt32(hdnStartingRef.Value);
                else return 0;
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
                TreeView1.Nodes.Clear();

                if (ShowAllCategoryListing)
                {
                    string url = "~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + 0;
                    TreeNode AllCat = new TreeNode("All Categories", "0", null, url, null);

                    TreeView1.Nodes.Add(AllCat);
                    AllCat.Selected = true;
                }
                LoadTree(null, categoryDR, categoryDT);
                if (CategoryID == 0)
                {

                    TreeView1.CollapseAll();
                }
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

        public string SuburbID
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnSuburbID.Value))
                    return hdnSuburbID.Value.ToString();
                else return "0";
            }
            set
            {
                hdnSuburbID.Value = value.ToString();
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

        public TimeSpan tmFrom
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTmFrom.Value))
                    return Convert.ToDateTime(hdnTmFrom.Value).TimeOfDay;
                else return SystemConstants.nodate.TimeOfDay;
            }
            set
            {
                hdnTmFrom.Value = value.ToString();
            }
        }

        public TimeSpan tmTo
        {
            get
            {
                if (!string.IsNullOrEmpty(hdnTmTo.Value))
                    return Convert.ToDateTime(hdnTmTo.Value).TimeOfDay;
                else return SystemConstants.nodate.TimeOfDay;
            }
            set
            {
                hdnTmTo.Value = value.ToString();
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

        private void LoadTree(TreeNode node, CustomerEDSC.v_CategoryExplorerDTRow categoryDR, CustomerEDSC.v_CategoryExplorerDTDataTable categoryDT)
        {
            IEnumerable<CustomerEDSC.v_CategoryExplorerDTRow> list = null;

            if (node == null && categoryDR != null)
            {
                //create root
                if (ShowCategoryListingTitle)
                {
                    //string url = "~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + categoryDR.ID.ToString();
                    node = new TreeNode(categoryDR.Name, categoryDR.ID.ToString(), null, null, null);
                    node.ImageUrl = "";
                    TreeView1.Nodes.Add(node);

                    if (categoryDR.ID == Convert.ToInt32(Session[SystemConstants.s_CurrentCategorySelected]))
                        node.Selected = true;
                    else
                    {
                        if (node.Depth > ShowLevel && !IsPostBack)
                            node.Collapse();
                    }
                }
            }

            if (categoryDR == null)
            {
                list = from b in categoryDT
                       where b.Level == 0
                       select b;
            }
            else if (categoryDR.Level == 0)
            {
                list = from b in categoryDT
                       where b.Level == 1 && b.Level1ParentID == categoryDR.ID
                       select b;
            }
            else if (categoryDR.Level == 1)
            {
                list = from b in categoryDT
                       where b.Level == 2 && b.Level1ParentID == categoryDR.Level1ParentID && b.Level2ParentID == categoryDR.ID
                       select b;
            }
            else if (categoryDR.Level == 2)
            {
                list = from b in categoryDT
                       where b.Level == 2 && b.Level1ParentID == categoryDR.Level1ParentID && b.Level2ParentID == categoryDR.ID
                       select b;
            }

            CustomerEDSC.v_CategoryExplorerDTRow drwait = new CustomerEDSC.v_CategoryExplorerDTDataTable().Newv_CategoryExplorerDTRow();
            drwait.Name = "";
            foreach (var dr in list)
            {
                //string url = "~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + dr.ID;
                TreeNode childNode;
                if (dr.ID == CategoryID)
                    childNode = new TreeNode("<font color='#17438C'>" + dr.Name + "</font>", dr.ID.ToString(), null, null, null);
                else if (dr.Name == "Other")
                {
                    drwait.ID = dr.ID;
                    drwait.Name = dr.Name;
                    drwait.Level = dr.Level;
                    continue;
                }
                else
                    childNode = new TreeNode(dr.Name, dr.ID.ToString(), null, null, null);

                if (dr.Level != 0)
                    childNode.ImageUrl = "~/Content/StyleImages/point.png";
                else
                    childNode.Collapse();

                if (node == null)
                    TreeView1.Nodes.Add(childNode);
                else
                    node.ChildNodes.Add(childNode);

                if (dr.ID == CategoryID)
                {
                    childNode.Selected = true;

                    childNode.Expand();
                    AddToExpandState(childNode.Value);

                    var tempNode = childNode.Parent;
                    int x = childNode.Depth;
                    for (; x > 0; x--)
                    {
                        if (tempNode != null)
                        {
                            tempNode.Expand();
                            AddToExpandState(tempNode.Value);
                            tempNode = tempNode.Parent;
                        }
                    }

                }
                else
                {
                    if (!CheckIfStateExpanded(childNode.Value))
                    {
                        if (childNode.Depth > ShowLevel && !IsPostBack)
                        {
                            childNode.Collapse();
                            RemoveFromExpandState(childNode.Value);
                        }
                        else
                        {
                            AddToExpandState(childNode.Value);
                        }
                    }
                    else
                    {
                        childNode.Expand();
                        AddToExpandState(childNode.Value);
                    }
                }

                LoadTree(childNode, dr, categoryDT);
            }
            if (!string.IsNullOrEmpty(drwait.Name))
            {
                //string url = "~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + dr.ID;
                TreeNode childNode;
                if (drwait.ID == CategoryID)
                    childNode = new TreeNode("<font color='#17438C'>" + drwait.Name + "</font>", drwait.ID.ToString(), null, null, null);

                else
                    childNode = new TreeNode(drwait.Name, drwait.ID.ToString(), null, null, null);

                if (drwait.Level != 0)
                    childNode.ImageUrl = "~/Content/StyleImages/point.png";
                else
                    childNode.Collapse();

                if (node == null)
                    TreeView1.Nodes.Add(childNode);
                else
                    node.ChildNodes.Add(childNode);

                if (drwait.ID == CategoryID)
                {
                    childNode.Selected = true;

                    childNode.Expand();
                    AddToExpandState(childNode.Value);

                    var tempNode = childNode.Parent;
                    int x = childNode.Depth;
                    for (; x > 0; x--)
                    {
                        if (tempNode != null)
                        {
                            tempNode.Expand();
                            AddToExpandState(tempNode.Value);
                            tempNode = tempNode.Parent;
                        }
                    }

                }
                else
                {
                    if (!CheckIfStateExpanded(childNode.Value))
                    {
                        if (childNode.Depth > ShowLevel && !IsPostBack)
                        {
                            childNode.Collapse();
                            RemoveFromExpandState(childNode.Value);
                        }
                        else
                        {
                            AddToExpandState(childNode.Value);
                        }
                    }
                    else
                    {
                        childNode.Expand();
                        AddToExpandState(childNode.Value);
                    }
                }

                LoadTree(childNode, drwait, categoryDT);
            }

        }

        private string getSelectedSuburb()
        {
            string selected = "";
            string separator = "|";
            foreach (ListItem item in DropDownCheckBoxes1.Items)
            {
                if (item.Selected)
                {
                    if (!String.IsNullOrEmpty(selected))
                        selected += separator;
                    selected += item.Value;
                }
            }


            return selected;
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            setFilter();
            ShowFilter();
            if (ApplyFilter != null)
            {
                ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, tmFrom, tmTo, filteredDays, true);
            }

            //Session[SystemConstants.s_CurrentCategorySelected] = Convert.ToInt32(TreeView1.SelectedNode.Value);
            //Response.Redirect("~/Activities/Default.aspx?" + SystemConstants.CategoryID + "=" + TreeView1.SelectedNode.Value);
        }

        private void setFilteredDays()
        {
            foreach (ListItem day in filteredDays)
            {
                if (day.Text == DayOfWeek.Monday.ToString())
                {
                    chkMonday.Checked = true;
                    tabDays.Visible = true;
                }
                else if (day.Text == DayOfWeek.Tuesday.ToString())
                {
                    chkTuesday.Checked = true; tabDays.Visible = true;
                }
                else if (day.Text == DayOfWeek.Wednesday.ToString())
                {
                    chkWebnesday.Checked = true; tabDays.Visible = true;
                }
                else if (day.Text == DayOfWeek.Thursday.ToString())
                {
                    chkThursday.Checked = true; tabDays.Visible = true;
                }
                else if (day.Text == DayOfWeek.Friday.ToString())
                {
                    chkFriday.Checked = true; tabDays.Visible = true;
                }
                else if (day.Text == DayOfWeek.Saturday.ToString())
                {
                    chkSaturday.Checked = true; tabDays.Visible = true;
                }
                else if (day.Text == DayOfWeek.Sunday.ToString())
                {
                    chkSunday.Checked = true; tabDays.Visible = true;
                }
                else if (day.Text == SystemConstants.AnyisFiltered)
                {
                    chkAnyday.Checked = true;
                }

            }
            if (chkAnyday.Checked)
                tabDays.Visible = false;
        }

        private void setFilter()
        {
            TimeSpan tsparse = DateTime.Now.TimeOfDay;
            DateTime dtparse = DateTime.Now;
            if (!string.IsNullOrEmpty(TreeView1.SelectedNode.Value))
                CategoryID = Convert.ToInt32(TreeView1.SelectedNode.Value);

            if (!String.IsNullOrEmpty(SuburbID) && !String.IsNullOrEmpty(getSelectedSuburb()))
                SuburbID += "|" + getSelectedSuburb();

            if (!DateTime.TryParse(txtCalendarFrom.Text, out dtparse))
                txtCalendarFrom.Text = "";

            if (!DateTime.TryParse(txtCalendarTo.Text, out dtparse))
                txtCalendarTo.Text = "";

            if (!string.IsNullOrEmpty(txtCalendarFrom.Text) && txtCalendarFrom.Text != "__/__/____")
                dtFrom = Convert.ToDateTime(txtCalendarFrom.Text + " " + "00:00");

            if (!string.IsNullOrEmpty(txtCalendarTo.Text) && txtCalendarTo.Text != "__/__/____")
                dtTo = Convert.ToDateTime(txtCalendarTo.Text + " " + "00:00");

            if (ddlTimeStart.SelectedIndex != 0)
                tmFrom = Convert.ToDateTime(ddlTimeStart.SelectedItem.Text).TimeOfDay;

            if (ddlTimeEnds.SelectedIndex != 0)
                tmTo = Convert.ToDateTime(ddlTimeEnds.SelectedItem.Text).TimeOfDay;


            if (dtFrom > dtTo)
            {
                filterError = true;
                dtFrom = dtTo = SystemConstants.nodate;
                Alert.Show("Calendar From must be lower than Calendar To");
            }
            if (ddlTimeStart.SelectedIndex > ddlTimeEnds.SelectedIndex)
            {
                filterError = true;
                ddlTimeStart.SelectedIndex = ddlTimeEnds.SelectedIndex = 0;
                Alert.Show("Time Start must be lower than Time Finish");
            }
            if (ageFrom > ageTo)
            {
                filterError = true;
                ageFrom = 0;
                ageTo = 99;
                Alert.Show("Age ");
            }

            ListItemCollection Days = new ListItemCollection();

            if (chkAnyday.Checked)
            {

                ListItem heldDay = new ListItem(SystemConstants.AnyisFiltered, SystemConstants.AnyisFiltered);
                Days.Add(heldDay);
            }
            if (!chkAnyday.Checked)
            {
                int numofday = 0;

                if (chkMonday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Monday.ToString(), DayOfWeek.Monday.ToString());
                    Days.Add(heldDay);
                }
                if (chkTuesday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Tuesday.ToString(), DayOfWeek.Tuesday.ToString());
                    Days.Add(heldDay);
                }
                if (chkWebnesday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Wednesday.ToString(), DayOfWeek.Wednesday.ToString());
                    Days.Add(heldDay);
                }
                if (chkThursday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Thursday.ToString(), DayOfWeek.Thursday.ToString());
                    Days.Add(heldDay);
                }
                if (chkFriday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Friday.ToString(), DayOfWeek.Friday.ToString());
                    Days.Add(heldDay);
                }
                if (chkSaturday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Saturday.ToString(), DayOfWeek.Saturday.ToString());
                    Days.Add(heldDay);
                }
                if (chkSunday.Checked)
                {
                    numofday++;
                    ListItem heldDay = new ListItem(DayOfWeek.Sunday.ToString(), DayOfWeek.Sunday.ToString());
                    Days.Add(heldDay);
                }
                if (Days.Count != 0)
                    imgbtnResetDate.Visible = true;
            }
            else
            {
                imgbtnResetDate.Visible = false;
            }
            filteredDays = Days;

        }

        protected void imgbtnResetDate_Click(object sender, ImageClickEventArgs e)
        {
            dtFrom = dtTo = SystemConstants.nodate;
            txtCalendarFrom.Text = txtCalendarTo.Text = "";
            ddlTimeStart.SelectedIndex = ddlTimeEnds.SelectedIndex = 0;
            //day reset
            chkAnyday.Checked = true; chkMonday.Checked = chkTuesday.Checked = chkWebnesday.Checked = chkThursday.Checked = chkFriday.Checked = chkSaturday.Checked = chkSunday.Checked = true;
            filteredDays.Clear();

            setFilter();
            ShowFilter();
            if (ApplyFilter != null)
                if (ageFrom == 0 && ageTo == 99 && string.IsNullOrEmpty(getSelectedSuburb()))
                    ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, tmFrom, tmTo, filteredDays, false);
                else
                    ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, tmFrom, tmTo, filteredDays, true);
        }

        //protected void imgbtnResetAge_Click(object sender, ImageClickEventArgs e)
        //{
        //    ageFrom = 0;
        //    ageTo = 99;
        //    txtAgeFrom.Text = txtAgeTo.Text = "";

        //    setFilter();
        //    ShowFilter();
        //    if (ApplyFilter != null)
        //        if (dtFrom == SystemConstants.nodate && dtTo == SystemConstants.nodate && getSelectedSuburb() == "0")
        //            ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, filteredDays, false);
        //        else
        //            ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, filteredDays, true);
        //}

        protected void imgbtnResetSuburb_Click(object sender, ImageClickEventArgs e)
        {
            SuburbID = "0";
            foreach (ListItem item in DropDownCheckBoxes1.Items)
            {
                item.Selected = false;
            }
            setFilter();
            ShowFilter();
            if (ApplyFilter != null)
                if (dtFrom == SystemConstants.nodate && dtTo == SystemConstants.nodate && ageFrom == 0 && ageTo == 99)
                    ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, tmFrom, tmTo, filteredDays, false);
                else
                    ApplyFilter(CategoryID, ageFrom, ageTo, SuburbID, dtFrom, dtTo, tmFrom, tmTo, filteredDays, true);
        }

        protected void chkAnyday_CheckedChanged(object sender, EventArgs e)
        {
            chkMonday.Checked = chkTuesday.Checked = chkWebnesday.Checked = chkThursday.Checked = chkFriday.Checked = chkSaturday.Checked = chkSunday.Checked = chkAnyday.Checked;
            tabDays.Visible = !chkAnyday.Checked;
        }

        private void Checkday()
        {
            if (!chkMonday.Checked || !chkTuesday.Checked || !chkWebnesday.Checked || !chkThursday.Checked || !chkFriday.Checked || !chkSaturday.Checked || !chkSunday.Checked)
                chkAnyday.Checked = false;
            else
                chkAnyday.Checked = true;
        }

        protected void chkMonday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

        protected void chkTuesday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

        protected void chkWebnesday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

        protected void chkThursday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

        protected void chkFriday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

        protected void chkSaturday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

        protected void chkSunday_CheckedChanged(object sender, EventArgs e)
        {
            Checkday();
        }

    }

    public static class Alert
    {

        /// <summary>
        /// Shows a client-side JavaScript alert in the browser.
        /// </summary>
        /// <param name="message">The message to appear in the alert.</param>
        public static void Show(string message)
        {
            // Cleans the message to allow single quotation marks
            string cleanMessage = message.Replace("'", "\\'");
            string script = "<script type=\"text/javascript\">alert('" + cleanMessage + "');</script>";

            // Gets the executing web page
            Page page = HttpContext.Current.CurrentHandler as Page;

            // Checks if the handler is a Page and that the script isn't allready on the Page
            if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("alert"))
            {
                page.ClientScript.RegisterClientScriptBlock(typeof(Alert), "alert", script);
            }
        }

    }
}