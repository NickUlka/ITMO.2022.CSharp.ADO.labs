using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;

namespace ITMO._2022.CSharp.ADO.labs._1
{
    public partial class Form1 : Form

    {
        OleDbConnection connection = new OleDbConnection();
            //string testConnect = @"Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Northwind;Data Source=LAPTOP-7LV3KHNE\SQLEXPRESS";
        
        private void connection_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            connectionToolStripMenuItem.Enabled =
                (e.CurrentState == ConnectionState.Closed);
            connectionCloseToolStripMenuItem.Enabled =
                (e.CurrentState == ConnectionState.Open);
        }

        
        public Form1()
        {
            InitializeComponent();
        }

        static string GetConnectionStringByName(string name)
        {
            string returnValue = null;
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[name];
            if (settings != null)
                returnValue = settings.ConnectionString;
            return returnValue;
        }
        string testConnect = GetConnectionStringByName("DBConnect.NorthwindConnectionString");

        private void connectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.connection.StateChange += new System.Data.StateChangeEventHandler(this.connection_StateChange);
                if (connection.State != ConnectionState.Open)
                {
                    connection.ConnectionString = testConnect;
                    connection.Open();
                    MessageBox.Show("Соединение с базой данных выполнено успешно");
                }
                else
                    MessageBox.Show("Соединение с базой данных уже установлено");
            }
            catch (OleDbException XcpSQL)
            {
                foreach (OleDbError se in XcpSQL.Errors)
                {
                    MessageBox.Show(se.Message,
                    "SQL Error code " + se.NativeError,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            }

            catch (Exception Xcp)
            {
                MessageBox.Show(Xcp.Message, "Unexpected Exception",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void connectionCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.connection.StateChange += new System.Data.StateChangeEventHandler(this.connection_StateChange);
            if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    MessageBox.Show("Соединение с базой данных закрыто");
                }
                else
                    MessageBox.Show("Соединение с базой данных уже закрыто");     
        }

        private void connectionListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConnectionStringSettingsCollection settings =
          ConfigurationManager.ConnectionStrings;
            if (settings != null)
            {
                foreach (ConnectionStringSettings cs in settings)
                {
                    MessageBox.Show("name = " + cs.Name);
                    MessageBox.Show("providerName = " + cs.ProviderName);
                    MessageBox.Show("connectionString = " + cs.ConnectionString);
                }
            }

        }


    }
}
