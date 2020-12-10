using System;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Windows.Forms;
using System.Data;

namespace NetWork.Server.Window
{
    public partial class UserManagerForm : Form
    {
        public static DataTable dataTable = new DataTable();

        public UserManagerForm()
        {
            InitializeComponent();
            dataGridViewClientManager.DataSource = dataTable;
        }

        public static void CheckXML()
        {
            if (!File.Exists("ManagedClients.xml"))
            {
                Server.managedClientsXML = new XDocument(new XDeclaration("1.0", "utf-8", "yes"), new XComment("All managed Clients are here"),
                    new XElement("Clients"));

                Server.managedClientsXML.Save("ManagedClients.xml");

                Server.managedClientsXML = XDocument.Load("ManagedClients.xml");
            }
            else
            if (Server.managedClientsXML == null)
                Server.managedClientsXML = XDocument.Load("ManagedClients.xml");
        }

        public static void UpdateDataTable()
        {
            CheckXML();

            if (dataTable.Columns.Count <= 0)
            {
                dataTable.Columns.Add("Name", typeof(string));
                dataTable.Columns.Add("Address", typeof(string));
                dataTable.Columns.Add("Band", typeof(bool));
                dataTable.Columns.Add("Reason", typeof(string));
            }

            if (File.Exists("ManagedClients.xml"))
            {
                if (dataTable.Rows.Count <= 0)
                {
                    foreach (var client in Server.managedClientsXML.Descendants("Client"))
                    {
                        dataTable.Rows.Add(client.Attribute("Name").Value, client.Attribute("Address").Value, client.Attribute("Band").Value, client.Attribute("Reason").Value);
                    }
                }
                else
                {
                    dataTable.Clear();

                    foreach (var client in Server.managedClientsXML.Descendants("Client"))
                    {
                        dataTable.Rows.Add(client.Attribute("Name").Value, client.Attribute("Address").Value, client.Attribute("Band").Value, client.Attribute("Reason").Value);
                    }
                }
            }
            else
            {
                dataTable.Clear();
            }
        }

        private void DataGridViewClientManager_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ClientManagerWindow_Load(object sender, EventArgs e)
        {
            UpdateDataTable();
        }
    }
}
