using HealthyClub.Administration.DA;
using HealthyClub.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HealthyClub.Administration.Web.Report
{
    public partial class Provider : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string attachment = "attachment; filename=Provider.csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("content-disposition", attachment);
            HttpContext.Current.Response.ContentType = "text/csv";
            HttpContext.Current.Response.AddHeader("Pragma", "public");

            var providerDT = new AdministrationDAC().RetrieveProviders();
            var stateDT = new AdministrationDAC().RetrieveStates();
            //Exclude some info
            providerDT.PrimaryKey = null;
            providerDT.Columns.Remove("MiddleName");
            providerDT.Columns.Remove("UserID");
            providerDT.Columns.Remove("Activity");
            providerDT.Columns.Remove("EntityKey");
            providerDT.Columns.Remove("EntityState");
            providerDT.Columns.Remove("ProviderSettingsReference");
            providerDT.Columns.Remove("ProviderSettings");
            providerDT.Columns.Remove("UsersReference");
            providerDT.Columns.Remove("ActivityRewards");
            providerDT.Columns.Remove("ProviderLog");
            providerDT.Columns.Remove("Reward");
            providerDT.Columns.Remove("Users");
            providerDT.Columns.Remove("AccountDeletion");




            StringBuilder sb = new StringBuilder();

            if (providerDT.Columns.Count != 0)
            {
                foreach (DataColumn column in providerDT.Columns)
                {
                    sb.Append(column.ColumnName + ',');
                }
                sb.Append("\r\n");
                foreach (DataRow row in providerDT.Rows)
                {
                    foreach (DataColumn column in providerDT.Columns)
                    {

                        if (row[column].ToString().Contains(',') == true)
                        {
                            row[column] = row[column].ToString().Replace(",", "");
                        }
                        if (column.ColumnName == "Title")
                        {
                            if (row[column].ToString().Equals(((int)SystemConstants.userTitle.Dr).ToString()))
                                sb.Append(SystemConstants.userTitle.Dr.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.userTitle.Miss).ToString()))
                                sb.Append(SystemConstants.userTitle.Miss.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.userTitle.Mr).ToString()))
                                sb.Append(SystemConstants.userTitle.Mr.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.userTitle.Mrs).ToString()))
                                sb.Append(SystemConstants.userTitle.Mrs.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.userTitle.Ms).ToString()))
                                sb.Append(SystemConstants.userTitle.Ms.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.userTitle.Rev).ToString()))
                                sb.Append(SystemConstants.userTitle.Rev.ToString() + ',');
                            else
                                sb.Append("" + ',');
                        }
                        else if (column.ColumnName == "StateID")
                        {
                            sb.Append(stateDT.Where(x => x.ID.ToString() == row[column].ToString()).Select(y => y.StateName).FirstOrDefault() + ',');
                        }
                        else if (column.ColumnName == "Aggreement")
                        {
                            if (row[column].ToString().Equals(true.ToString()))
                                sb.Append("Yes" + ',');
                            else if (row[column].ToString().Equals(false.ToString()))
                                sb.Append("No" + ',');
                            else
                                sb.Append(row[column].ToString() + ',');
                        }
                        else if (column.ColumnName == "PreferredContact")
                        {
                            if (row[column].ToString().Equals(((int)SystemConstants.PreferedContact.Brochure).ToString()))
                                sb.Append(SystemConstants.PreferedContact.Brochure.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.PreferedContact.Email).ToString()))
                                sb.Append(SystemConstants.PreferedContact.Email.ToString() + ',');
                            else if (row[column].ToString().Equals(((int)SystemConstants.PreferedContact.Phone).ToString()))
                                sb.Append(SystemConstants.PreferedContact.Phone.ToString() + ',');
                            else
                                sb.Append(row[column].ToString() + ',');
                        }
                        else
                            sb.Append(row[column].ToString() + ',');
                    }
                    sb.Append("\r\n");
                }
            }



            HttpContext.Current.Response.Write(sb.ToString());
            HttpContext.Current.Response.End();
        }
    }
}