using System;
using System.IO;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class MainForm : Form
    {
        // Inventory file path
        private string inventoryFile = "inventory.txt";
        private int selectedItemIndex;
        private double itemPrice;
        private bool itemInStock;
        private DateTime lastUpdate;

        // Array to hold inventory items
        private string[] inventoryItems;

        public MainForm()
        {
            InitializeComponent();
            LoadInventory();
        }

        // Loads inventory items from a file into the ListBox
        private void LoadInventory()
        {
            try
            {
                // Check if inventory file exists
                if (File.Exists(inventoryFile))
                {
                    inventoryItems = File.ReadAllLines(inventoryFile);
                    listBoxInventory.Items.Clear();

                    // Loop to add items to ListBox
                    foreach (string item in inventoryItems)
                        listBoxInventory.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions when reading the file
                MessageBox.Show($"Error loading inventory: {ex.Message}");
            }
        }

        // Opens AddItemForm to add a new item
        private void btnAddItem_Click(object sender, EventArgs e)
        {
            AddItemForm addForm = new AddItemForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // Add new item from second form to ListBox
                listBoxInventory.Items.Add(addForm.GetItemDetails());
                SaveInventory();
            }
        }

        // Removes the selected item from inventory
        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            // Ensure an item is selected before removal
            if (listBoxInventory.SelectedIndex >= 0)
            {
                listBoxInventory.Items.RemoveAt(listBoxInventory.SelectedIndex);
                SaveInventory();
            }
            else
            {
                MessageBox.Show("Please select an item to remove.");
            }
        }

        // Refreshes the inventory list from the file
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        // Saves the current inventory items to a file
        private void SaveInventory()
        {
            try
            {
                // Copy items from ListBox to array
                string[] items = new string[listBoxInventory.Items.Count];
                listBoxInventory.Items.CopyTo(items, 0);
                File.WriteAllLines(inventoryFile, items);

                // Update the last update time
                lastUpdate = DateTime.Now;
            }
            catch (Exception ex)
            {
                // Handle exceptions when saving to the file
                MessageBox.Show($"Error saving inventory: {ex.Message}");
            }
        }

        // Shows details of the selected item
        private void listBoxInventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxInventory.SelectedItem != null)
            {
                selectedItemIndex = listBoxInventory.SelectedIndex;
                MessageBox.Show($"Selected Item: {listBoxInventory.SelectedItem}");
            }
        }
    }
}