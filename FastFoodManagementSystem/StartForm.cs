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
    public partial class StartForm : Form
    {
        private Employee _employees;

        private DBDataContext context;
        public StartForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String username = textBox1.Text;
            String password = textBox2.Text;
            context = new DBDataContext();

            _employees =
                context.Employees.SingleOrDefault(
                    emp => emp.Email == username || emp.Username == username && emp.Password == password);

            if (_employees != null)
            {
                //_employees = new Employee()
                //{
                //    Id = logger.ID,
                //    Name = logger.Name,
                //    Address = logger.Address,
                //    Gender = logger.Gender,
                //    Email = logger.Email,
                //    Phone = logger.Phone,
                //    Birthdate = logger.Birthdate,
                //    NID = logger.NID,
                //    Username = logger.Username,
                //    Password = logger.Password,
                //    Role = logger.Role,
                //    Salary = logger.Salary,
                //    Type = logger.Type
                //};

                if (_employees.Type == "Manager")
                {
                    this.Hide();
                    Admin _admin = new Admin(_employees);
                    _admin.Show();
                }
                else
                {
                    this.Hide();
                    EmployeeForm _employeeForm = new EmployeeForm(_employees);
                    _employeeForm.Show();
                }
                
            }
            else
            {
                MessageBox.Show("Wrong Username or Password!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AllProduct _allProduct = new AllProduct();
            _allProduct.ShowDialog();
        }

        private void StartForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
