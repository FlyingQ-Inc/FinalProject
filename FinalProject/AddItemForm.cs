using System;
using System.Windows.Forms;

namespace FinalProject
{
    public partial class AddItemForm : Form
    {
        public AddItemForm()
        {
            InitializeComponent();
        }

        // Retrieves item details entered by the user
        public string GetItemDetails()
        {
            string itemName = txtItemName.Text;
            double price;

            // Validates the price input; sets to 0.0 if invalid
            bool isPriceValid = double.TryParse(txtPrice.Text, out price);
            if (!isPriceValid)
                price = 0.0;

            // Determines stock status based on checkbox
            bool inStock = chkInStock.Checked;
            string stockStatus = inStock ? "In Stock" : "Out of Stock";

            // Returns a formatted string with item details
            return $"{itemName} | ${price:F2} | {stockStatus} | Added: {DateTime.Now:d}";
        }

        // Handles the Submit button click event
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Ensures item name is not empty before submitting
            if (!string.IsNullOrWhiteSpace(txtItemName.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Item name cannot be empty.");
            }
        }

        // Handles the Cancel button click event
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
