using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using BCUtility;
using System.IO;
using System.Transactions;
using System.Linq;
using System.Reflection;


namespace HealthyClub.Administration.Web.Rewards
{
    public partial class OCR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void storename_Click(object sender, EventArgs e)
        {
            string appdir = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = appdir + "/First Names.txt";
            string name = txtname.Text;
            using (FileStream aFile = new FileStream(filePath, FileMode.Append, FileAccess.Write))
            using (StreamWriter sw = new StreamWriter(aFile))
            {
                sw.WriteLine(name);
                txtExtractedText.Text = "Name added";
            }


        }
        protected void btnExtractText_Click(object sender, EventArgs e)
        {
            string connect = "Provider=Microsoft.Jet.OleDb.4.0;Data Source=|DataDirectory|Medical History.mdb";
            string query = "Select First_Name,Last_Name from FormDataTable";
            string first;
            string last;
            List<string> content = new List<string>();
            using (OleDbConnection conn = new OleDbConnection(connect))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {

                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            first = reader[0].ToString();
                            last = reader[1].ToString();
                            content.Add(first + "==>" + last);
                        }
                    }

                }
            }
            txtExtractedText.Text = String.Join(Environment.NewLine, content); ;



        }

    }
}