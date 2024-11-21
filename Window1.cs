using System;
using Wisej.Web;
using System.Data;
using System.Data.SQLite;
using System.Drawing;

namespace WisejWebApplication2
{
    public partial class Window1 : Form
    {
        private TabControl tabControl;
        private TabPage organizationsTab;
        private TabPage staffTab;
        private DataGridView organizationsGrid;
        private DataGridView staffGrid;
        private Button addOrganizationBtn;
        private Button editOrganizationBtn;
        private Button addStaffBtn;
        private Button editStaffBtn;

        public Window1()
        {
            InitializeComponent();
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            this.Size = new System.Drawing.Size(800, 600);

            tabControl = new TabControl();
            tabControl.Dock = DockStyle.Fill;

            organizationsTab = new TabPage("Organizations");
            staffTab = new TabPage("Staff");

            organizationsGrid = new DataGridView();
            organizationsGrid.Dock = DockStyle.Fill;

            staffGrid = new DataGridView();
            staffGrid.Dock = DockStyle.Fill;

            addOrganizationBtn = new Button { Text = "Add Organization" };
            editOrganizationBtn = new Button { Text = "Edit Organization" };
            addStaffBtn = new Button { Text = "Add Staff" };
            editStaffBtn = new Button { Text = "Edit Staff" };

            var orgButtonPanel = new FlowLayoutPanel();
            orgButtonPanel.Dock = DockStyle.Top;
            orgButtonPanel.Controls.AddRange(new Control[] { addOrganizationBtn, editOrganizationBtn });

            var staffButtonPanel = new FlowLayoutPanel();
            staffButtonPanel.Dock = DockStyle.Top;
            staffButtonPanel.Controls.AddRange(new Control[] { addStaffBtn, editStaffBtn });

            organizationsTab.Controls.Add(organizationsGrid);
            organizationsTab.Controls.Add(orgButtonPanel);

            staffTab.Controls.Add(staffGrid);
            staffTab.Controls.Add(staffButtonPanel);

            tabControl.TabPages.Add(organizationsTab);
            tabControl.TabPages.Add(staffTab);

            this.Controls.Add(tabControl);

            // Add event handlers
            addOrganizationBtn.Click += AddOrganizationBtn_Click;
            editOrganizationBtn.Click += EditOrganizationBtn_Click;
            addStaffBtn.Click += AddStaffBtn_Click;
            editStaffBtn.Click += EditStaffBtn_Click;

            // Style grids
            StyleGrid(organizationsGrid, Color.AliceBlue, Color.LightCyan, Color.Navy);
            StyleGrid(staffGrid, Color.Lavender, Color.MistyRose, Color.DarkGreen);
        }

        private void StyleGrid(DataGridView grid, Color defaultColor, Color alternateColor, Color headerColor)
        {
            grid.DefaultCellStyle.BackColor = defaultColor;
            grid.DefaultCellStyle.ForeColor = Color.Black;
            grid.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font(grid.Font, FontStyle.Bold);
        }

        private void LoadData()
        {
            // Load Organizations
            using (var connection = new SQLiteConnection(Database.connectionString))
            {
                connection.Open();

                // Clear existing data and insert sample organizations
                var clearOrgQuery = "DELETE FROM Organizations;";
                using (var clearCmd = new SQLiteCommand(clearOrgQuery, connection))
                {
                    clearCmd.ExecuteNonQuery();
                }

                var insertOrgQuery = @"
                    INSERT INTO Organizations (Name, Street, Zip, City, Country) VALUES 
                    ('Tech Innovations Inc.', '123 Silicon Valley Rd', '94000', 'San Francisco', 'USA'),
                    ('Global Solutions Ltd.', '45 Business Park', '10115', 'Berlin', 'Germany'),
                    ('Creative Minds Co.', '789 Innovation Street', '75001', 'Paris', 'France');";
                using (var insertCmd = new SQLiteCommand(insertOrgQuery, connection))
                {
                    insertCmd.ExecuteNonQuery();
                }

                // Load Organizations
                var query = "SELECT * FROM Organizations;";
                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    organizationsGrid.DataSource = table;
                }
            }

            // Load Staff
            using (var connection = new SQLiteConnection(Database.connectionString))
            {
                connection.Open();

                // Clear existing data and insert sample staff
                var clearStaffQuery = "DELETE FROM Staff;";
                using (var clearCmd = new SQLiteCommand(clearStaffQuery, connection))
                {
                    clearCmd.ExecuteNonQuery();
                }

                var insertStaffQuery = @"
                    INSERT INTO Staff (OrganizationId, Title, FirstName, LastName, Phone, Email) VALUES 
                    (1, 'CEO', 'John', 'Doe', '+1-555-1234', 'john.doe@techinnovations.com'),
                    (1, 'CTO', 'Jane', 'Smith', '+1-555-5678', 'jane.smith@techinnovations.com'),
                    (2, 'Project Manager', 'Hans', 'Mueller', '+49-30-9876', 'hans.mueller@globalsolutions.de'),
                    (2, 'Senior Developer', 'Anna', 'Schmidt', '+49-30-5432', 'anna.schmidt@globalsolutions.de'),
                    (3, 'Creative Director', 'Pierre', 'Dubois', '+33-1-2345', 'pierre.dubois@creativeminds.fr'),
                    (3, 'Marketing Lead', 'Marie', 'Laurent', '+33-1-6789', 'marie.laurent@creativeminds.fr');";
                using (var insertCmd = new SQLiteCommand(insertStaffQuery, connection))
                {
                    insertCmd.ExecuteNonQuery();
                }

                // Load Staff with Organization Name
                var query = @"
                    SELECT 
                        Staff.Id, 
                        Organizations.Name AS OrganizationName, 
                        Staff.Title, 
                        Staff.FirstName, 
                        Staff.LastName, 
                        Staff.Phone, 
                        Staff.Email 
                    FROM Staff 
                    LEFT JOIN Organizations ON Staff.OrganizationId = Organizations.Id;";

                using (var adapter = new SQLiteDataAdapter(query, connection))
                {
                    var table = new DataTable();
                    adapter.Fill(table);
                    staffGrid.DataSource = table;
                }
            }
        }

        // Implement event handlers for add/edit buttons
        private void AddOrganizationBtn_Click(object sender, EventArgs e)
        {
            // Implement add organization logic
        }

        private void EditOrganizationBtn_Click(object sender, EventArgs e)
        {
            // Implement edit organization logic
        }

        private void AddStaffBtn_Click(object sender, EventArgs e)
        {
            // Implement add staff logic
        }

        private void EditStaffBtn_Click(object sender, EventArgs e)
        {
            // Implement edit staff logic
        }
    }
}