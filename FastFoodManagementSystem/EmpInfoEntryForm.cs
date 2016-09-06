﻿using System;
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
    public partial class EmpInfoEntryForm : Form
    {
        private int emp_Id;
        private Employee _employee;
        private DBDataContext _context;
        private Admin _admin;
        public EmpInfoEntryForm(Admin _admin,int emp_Id)
        {
            this._admin = _admin;
            this.emp_Id = emp_Id;
            InitializeComponent();
            cmbType.SelectedIndex = 0;
            cmbGender.SelectedIndex = 0;
        }

        private void EmpInfoEntryForm_Load(object sender, EventArgs e)
        {
            if (this.emp_Id != 0)
            {
                btnAction.Text = "Update";
                _context = new DBDataContext();
                _employee = _context.Employees.FirstOrDefault(emp => emp.ID.Equals(_employee.ID));
                ShowEmpData(_employee);
            }
            else
            {
                btnAction.Text = "Save";
            }
  
        }

        private void ShowEmpData(Employee employee)
        {
            txtId.Text = _employee.ID.ToString();
            txtName.Text = _employee.Name;
            txtAddress.Text = _employee.Address;
            cmbGender.SelectedIndex = cmbGender.Items.IndexOf(_employee.Gender);
            txtEmail.Text = _employee.Email;
            txtPhone.Text = _employee.Phone;
            txtBirthdate.Value = _employee.Birthdate;
            txtNIDno.Text = _employee.NID;
            txtUsername.Text = _employee.Username;
            txtPassword.Text = _employee.Password;
            txtSalary.Text = _employee.Salary.ToString();
            txtJobRole.Text = _employee.Role;
            cmbType.SelectedIndex = cmbType.Items.IndexOf(_employee.Type);
        }

        private void btnAction_Click(object sender, EventArgs e)
        {
            if (btnAction.Text == "Save")
            {
                try
                {
                    Employee emp = new Employee()
                    {
                        Name = txtName.Text,
                        Address = txtAddress.Text,
                        Gender = Convert.ToString(cmbGender.SelectedItem),
                        Email = txtEmail.Text,
                        Phone = txtPhone.Text,
                        Birthdate = txtBirthdate.Value,
                        NID = txtNIDno.Text,
                        Username = txtUsername.Text,
                        Password = txtPassword.Text,
                        Role = txtJobRole.Text,
                        Salary = Convert.ToInt32(txtSalary.Text),
                        Type = Convert.ToString(cmbType.SelectedItem)
                    };

                    _context = new DBDataContext();
                    _context.Employees.InsertOnSubmit(emp);
                    _context.SubmitChanges();
                    MessageBox.Show("Successfully Saved.");
                    _admin.ShowAllEmployeesData();
                    _admin.EmpInfoTabState();
                    this.Dispose();
                    
                }
                catch (Exception)
                {
                    MessageBox.Show("Failed to Save.");
                }
            }
            else
            {
                DialogResult result1 = MessageBox.Show("Are You Want To Update This?", "Confirm!", MessageBoxButtons.YesNo);
                if (result1 == DialogResult.Yes)
                {
                    try
                    {
                        int id = Convert.ToInt32(txtId.Text);
                        _context = new DBDataContext();

                        Employee emp = _context.Employees.FirstOrDefault(em => em.ID.Equals(id));

                        emp.Name = txtName.Text;
                        emp.Address = txtAddress.Text;
                        emp.Gender = Convert.ToString(cmbGender.SelectedItem);
                        emp.Email = txtEmail.Text;
                        emp.Phone = txtPhone.Text;
                        emp.Birthdate = txtBirthdate.Value;
                        emp.NID = txtNIDno.Text;
                        emp.Username = txtUsername.Text;
                        emp.Password = txtPassword.Text;
                        emp.Role = txtJobRole.Text;
                        emp.Salary = Convert.ToInt32(txtSalary.Text);
                        emp.Type = Convert.ToString(cmbType.SelectedItem);

                        _context.SubmitChanges();

                        MessageBox.Show("Successfully Updated.");
                        _admin.ShowAllEmployeesData();
                        _admin.EmpInfoTabState();
                        this.Dispose();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Failed to Update.");
                    } 
                }
            }
        }
    }
}
