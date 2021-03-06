﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OOP_5
{
    public partial class Form1 : Form
    {
        public List<Category> lCategory = new List<Category>();
        public Basket B = new Basket();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            new SQL().createTableWithContents();
            lCategory = new DataIO().loadCategory();
            updateListBox1();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateListBox2();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedMeal = lCategory[listBox1.SelectedIndex].lMeal[listBox2.SelectedIndex];

            try { pictureBox1.Image = Image.FromFile("img\\" + selectedMeal.getImagePath()); }
            catch { pictureBox1.Image = Image.FromFile("img\\failed.png"); }

            textBox1.Text = selectedMeal.getDescription();
            label4.Text = string.Format("Kaina: {0}€", selectedMeal.getPrice());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedMeal = lCategory[listBox1.SelectedIndex].lMeal[listBox2.SelectedIndex];

            bool exists = false;

            for (int i = 0; i < B.lMeal.Count; i++)
            {
                if (selectedMeal.getName() == B.lMeal[i].getName())
                {
                    B.lMeal[i].setCount(B.lMeal[i].getCount() + 1);
                    exists = true;
                }
            }

            if (!exists)
                B.lMeal.Add(selectedMeal);

            updateListBox3();

            updateLabel3();
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            //Kad žymėt nebūtų galima..
            //https://stackoverflow.com/questions/2800896/how-to-make-a-textbox-non-selectable-using-c-sharp
            ActiveControl = label1;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            B.lMeal = new List<Meal>();
            updateListBox3();
            label3.Text = "Užsakymo suma: - €";
        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        #region Update

        public void updateListBox1()
        {
            listBox1.Items.Clear();

            for (int i = 0; i < lCategory.Count; i++)
                listBox1.Items.Add(lCategory[i].getName());
        }

        public void updateListBox2()
        {
            listBox2.Items.Clear();

            for (int i = 0; i < lCategory[listBox1.SelectedIndex].lMeal.Count; i++)
                listBox2.Items.Add(lCategory[listBox1.SelectedIndex].lMeal[i].getName());
        }

        public void updateListBox3()
        {
            listBox3.Items.Clear();

            for (int i = 0; i < B.lMeal.Count; i++)
                listBox3.Items.Add(B.lMeal[i].getName() + " | x" + B.lMeal[i].getCount());
        }

        public void updateLabel3()
        {
            double sum = 0;

            for (int i = 0; i < B.lMeal.Count; i++)
                sum += B.lMeal[i].getPrice() * B.lMeal[i].getCount();

            label3.Text = string.Format("Užsakymo suma: {0}€", sum);
        }

        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                new DataIO().saveToTXT(this, B);
                new SQL().addNewOrder(B);
                MessageBox.Show("Užsakymas sėkmingai pridėtas!");
            }
            catch
            {
                MessageBox.Show("Įvyko klaida pridedant užsakymą!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new pAdmin(this).Show();
        }

        public void reloadData()
        {
            lCategory = new DataIO().loadCategory();

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            pictureBox1.Image = null;
            pictureBox1.InitialImage = null;

            textBox1.Text = null;
            label4.Text = "Kaina: - €";
            label3.Text = "Užsakymo suma: - €";

            updateListBox1();
        }
    }
}
