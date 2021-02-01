﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utility;

namespace Daily_Account
{
    public partial class AddPurchase : Form
    {
        public AddPurchase()
        {
            InitializeComponent();
        }

        private void InvoiceTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(InvoiceTypeComboBox.SelectedIndex == 1)
            {
                GSTNO_TextBox.Text = "N/A";
                GSTNO_TextBox.ReadOnly = true;
                GSTPer_ComboBox.SelectedIndex = 0;
                GSTPer_ComboBox.Enabled = false;
            }
            else
            {
                GSTNO_TextBox.Clear();
                GSTNO_TextBox.ReadOnly = false;
                GSTPer_ComboBox.Enabled = true;
                GSTPer_ComboBox.SelectedIndex = -1;
            }
        }

        private void AddPurchaseButton_Click(object sender, EventArgs e)
        {
            if(ValidateForm())
            {
                
                MessageBox.Show("Record Added Successfully");
            }
        }

        private bool ValidateForm()
        {
            if(InvoiceTypeComboBox.SelectedIndex == -1)
            {
                ShowErrorMessage("Please Select Any Invoise Type", "Validation Error");
                return false;
            }
            else if(BillNo_TextBox.Text.Trim()==string.Empty)
            {
                ShowErrorMessage("Please Enter Bill Number", "Validation Error");
                return false;
            }
            else if(Supplier_ComboBox.SelectedIndex == -1)
            {
                ShowErrorMessage("Please Select Any Supplier Name", "Validation Error");
                return false;
            }
            else if(GSTNO_TextBox.Text.Trim()==string.Empty)
            {
                ShowErrorMessage("Plase Enter GST Number", "Validation Error");
                return false;
            }
            else if(ItemPriceTextBox.Text.Trim()==string.Empty)
            {
                ShowErrorMessage("Please Enter The Price", "Validation Error");
                return false;
            }
            else if(Item_ComboBox.SelectedIndex == -1)
            {
                ShowErrorMessage("Please Select Any Item", "Validation Error");
                return false;
            }
            else if(GSTPer_ComboBox.Text == string.Empty)
            {
                ShowErrorMessage("Please Select The GST Persent", "Validation Error");
                return false;
            }
            return true;
        }

        private void ShowErrorMessage(string v1, string v2)
        {
            MessageBox.Show(v1, v2, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ItemPriceTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (!char.IsControl(e.KeyChar)) && (!char.IsPunctuation(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void TotalAmountTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsNumber(e.KeyChar) && (!char.IsControl(e.KeyChar)) && (!char.IsPunctuation(e.KeyChar)))
            {
                e.Handled = true;
            }
        }

        private void AddItem_CircleButton_Click(object sender, EventArgs e)
        {
            AddPriceForm APF = new AddPriceForm();
            APF.ShowDialog();
        }

        private void AddPurchase_Load(object sender, EventArgs e)
        {
            LoadAllComboBoxes();
        }

        private void LoadAllComboBoxes()
        {
            Item_ComboBox.DataSource = GetAllItems();
            Item_ComboBox.DisplayMember = "Item_Name";
            Item_ComboBox.SelectedIndex = -1;
        }

        private DataTable Dt_Items;
        private DataTable GetAllItems()
        {
            Dt_Items = new DataTable();
            string connString = DBCofiguration.ConnectionString;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                using (SqlCommand cmd = new SqlCommand("usp_GetAllItems", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Dt_Items.Load(reader);
                }
            }
                return Dt_Items;
        }

        private void RefreshCircleButton_Click(object sender, EventArgs e)
        {
            LoadAllComboBoxes();
        }

        private void AddSuplier_CircleButton_Click(object sender, EventArgs e)
        {
            ADDSupplierForm ADD = new ADDSupplierForm();
            ADD.ShowDialog();
        }
    }
}
