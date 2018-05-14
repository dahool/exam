using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using service;
using models;

namespace WindowsFormsApp2
{
    
    public partial class Form1 : Form
    {

        private Building building;

        public Form1()
        {
            InitializeComponent();
            building = BuildingFactory.create();
            building.ResultNotificationEvent += ResultFound;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();   
            this.building.search(textBox1.Text);
        }

        private void ResultFound(object sender, Owner o)
        {
            listView1.Items.Add(o.name + ", " + o.lastName);
        }
    }
}
