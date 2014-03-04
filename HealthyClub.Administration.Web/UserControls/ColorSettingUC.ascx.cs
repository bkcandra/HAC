using HealthyClub.Administration.DA;
using HealthyClub.Administration.EDS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.UserControls
{
    public partial class ColorSettingUC : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Refresh();
            }
        }

        private void Refresh()
        {
            SetMaster();
        }

        private void SetMaster()
        {
            var dr = new AdministrationDAC().RetrieveWebImage();
            if (dr != null)
            {
                if (!dr.IsHeaderStatusColorNull())
                    txtHeaderStatusColor.Text = dr.HeaderStatusColor;
                else
                    txtHeaderStatusColor.Text = "FFFFFF";
                if (!dr.IsHeaderBackgroundColorNull())
                    txtheaderBackgroundColor.Text = dr.HeaderBackgroundColor;
                else
                    txtheaderBackgroundColor.Text = "FFFFFF";
                if (!dr.IsBodyOuterColorNull())
                    txtBodyOuterColor.Text = dr.BodyOuterColor;
                else
                    txtBodyOuterColor.Text = "FFFFFF";
                if (!dr.IsFooterColorNull())
                    txtFooter.Text = dr.FooterColor;
                else
                    txtFooter.Text = "FFFFFF";
                if (!dr.IsTableHeadingColorNull())
                    txtTableHeading.Text = dr.TableHeadingColor;
                else
                    txtTableHeading.Text = "FFFFFF";
                if (!dr.IsSearchBarBackgroundColorNull())
                    txtSearchBar.Text = dr.SearchBarBackgroundColor;
                else
                    txtSearchBar.Text = "FFFFFF";
                if (!dr.IsActivityDetailTabColorNull())
                    txtActivityTab.Text = dr.ActivityDetailTabColor;
                else
                    txtActivityTab.Text = "FFFFFF";
            }
        }

        protected void lnkPopupColor1_Click(object sender, EventArgs e)
        {

        }

        protected void lnkSaveMenuColor_Click(object sender, EventArgs e)
        {
            var dr = new AdministrationEDSC.WebConfigurationDTDataTable().NewWebConfigurationDTRow();

            if (!string.IsNullOrEmpty(txtFooter.Text))
                dr.FooterColor = txtFooter.Text;
            else
                dr.FooterColor = "FFFFFF";
            if (!string.IsNullOrEmpty(txtHeaderStatusColor.Text))
                dr.HeaderStatusColor = txtHeaderStatusColor.Text;
            else
                dr.HeaderStatusColor = "FFFFFF";
            if (!string.IsNullOrEmpty(txtBodyOuterColor.Text))
                dr.BodyOuterColor = txtBodyOuterColor.Text;
            else
                dr.BodyOuterColor = "FFFFFF";
            if (!string.IsNullOrEmpty(txtFooter.Text))
                dr.FooterColor = txtFooter.Text;
            else
                dr.FooterColor = "FFFFFF";

            if (!string.IsNullOrEmpty(txtTableHeading.Text))
                dr.TableHeadingColor = txtTableHeading.Text;
            else
                dr.TableHeadingColor = "FFFFFF";
            if (!string.IsNullOrEmpty(txtSearchBar.Text))
                dr.SearchBarBackgroundColor = txtSearchBar.Text;
            else
                dr.SearchBarBackgroundColor = "FFFFFF";
            if (!string.IsNullOrEmpty(txtActivityTab.Text))
                dr.ActivityDetailTabColor = txtActivityTab.Text;
            else
                dr.ActivityDetailTabColor = "FFFFFF";

            dr.ModifiedBy = Membership.GetUser().UserName;
            dr.ModifiedDatetime = DateTime.Now;
            new AdministrationDAC().UpdateWebConfigurationColor(dr);
            lblSaved1.Visible = true;
            lblSaved1.Text = "-Saved- ," + DateTime.Now;

            try
            {
                //CssServiceReference.CustomerWebSettingServiceSoapClient cssClient = new CssServiceReference.CustomerWebSettingServiceSoapClient();
                //PvdServiceReference.ProviderWebSettingServiceSoapClient pvdClient = new PvdServiceReference.ProviderWebSettingServiceSoapClient();
                //cssClient.ApplyCss();
                //pvdClient.ApplyCss();
            }
            catch { }
        }
    }
}