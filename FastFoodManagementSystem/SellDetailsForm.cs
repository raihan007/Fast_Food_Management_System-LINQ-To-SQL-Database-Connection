using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastFoodManagementSystem.Database;

namespace FastFoodManagementSystem
{
    public partial class SellDetailsForm : Form
    {
        private int Sell_Id;
        private DBDataContext _context;
        private Sell _sell;
        
        public SellDetailsForm(int Sell_Id)
        {
            this.Sell_Id = Sell_Id;
            InitializeComponent();
        }

        private void SellDetailsForm_Load(object sender, EventArgs e)
        {
            _context = new DBDataContext();
            _sell = (from sell in _context.Sells
                     join emp in _context.Employees on sell.CreatedBy equals emp.ID
                     where sell.SellId.Equals(this.Sell_Id)
                     select sell).FirstOrDefault(); 
            
            ShowSellData(_sell);
            ShowSellDetailsData();
        }

        private void ShowSellData(Sell sell)
        {
            txtSellId.Text = sell.SellId.ToString();
            txtSellDate.Text = sell.SellDate.ToShortDateString();
            txtTotalCost.Text = sell.TotalCost.ToString();
            txtBilledBy.Text = sell.Employee.Name.ToString();
            txtDiscount.Text = sell.Discount.ToString();
            txtVat.Text = sell.Vat.ToString();
        }

        private void ShowSellDetailsData()
        {
            _context = new DBDataContext();
            SellDetailsGrid.DataSource = (from sell in _context.Sell_Details
                     join pro in _context.Products on sell.Product_ID equals pro.P_Id
                     where sell.Sell_ID.Equals(this.Sell_Id)
                     select new {sell.SD_ID,sell.Product_ID,sell.Product.P_Name,sell.P_Quantity,sell.P_Cost}).ToList();

            SellDetailsGrid.Columns[1].HeaderText = "Product ID";
            SellDetailsGrid.Columns[2].HeaderText = "Product Name";
            SellDetailsGrid.Columns[3].HeaderText = "Quantity";
            SellDetailsGrid.Columns[4].HeaderText = "Cost";
            SellDetailsGrid.Columns[0].Visible = false;
            
        }
    }
}
