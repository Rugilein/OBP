using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace OOP_5
{
    public partial class pAdmin : Form
    {
        bool hasLoggedIn = false;

        public List<Category> lCategory = new List<Category>();

        Form1 F;

        public pAdmin(Form1 _F)
        {
            InitializeComponent();
            F = _F;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string input = Interaction.InputBox("Įveskite naujos kategorijos pavadinimą:", "Nauja kategorija", "", -1, -1);

            if (!string.IsNullOrEmpty(input))
            {
                new SQL().addNewCategory(input);
                F.reloadData();
            }
            else
                MessageBox.Show("Reikia įvesti naujos kategorijos pavadinimą!");
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox1.Items.Clear();

            //for (int i = 0; i < lCategory[comboBox1.SelectedIndex].lMeal.Count; i++)
            //    comboBox2.Items.Add(lCategory[comboBox1.SelectedIndex].lMeal[i].getName());
        }

        private void pAdmin_Load(object sender, EventArgs e)
        {
            Size = new System.Drawing.Size(704, 362);

            panel2.Location = new Point(180, 20);

            if (hasLoggedIn)
            {
                panel2.Hide();

                panel1.Show();
            }
            else
                panel1.Hide();


            lCategory = new DataIO().loadCategory();

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox5.Items.Clear();

            for (int i = 0; i < lCategory.Count; i++)
            {
                comboBox1.Items.Add(lCategory[i].getName());
                comboBox2.Items.Add(lCategory[i].getName());
                comboBox5.Items.Add(lCategory[i].getName());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox1.Text))
                MessageBox.Show("Privalai nurodyti kategoriją!");
            else if (string.IsNullOrEmpty(textBox1.Text))
                MessageBox.Show("Privalai nurodyti pavadinimą!");
            else if (string.IsNullOrEmpty(textBox2.Text))
                MessageBox.Show("Privalai nurodyti paveikslėlį!");
            else if (string.IsNullOrEmpty(textBox3.Text))
                MessageBox.Show("Privalai nurodyti aprašymą!");
            else
            {
                new SQL().addNewFood(comboBox1.SelectedIndex + 1, textBox1.Text, float.Parse(numericUpDown1.Value.ToString()), textBox1.Text, textBox1.Text);
                F.reloadData();
                MessageBox.Show("Naujas patiekalas sėkmingai pridėtas!");
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox3.Items.Clear();

            for (int i = 0; i < lCategory[comboBox2.SelectedIndex].lMeal.Count; i++)
                comboBox3.Items.Add(lCategory[comboBox2.SelectedIndex].lMeal[i].getName());
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            var foodInfo = lCategory[comboBox2.SelectedIndex].lMeal[comboBox3.SelectedIndex];

            numericUpDown2.Value = decimal.Parse(foodInfo.getPrice().ToString());

            textBox5.Text = foodInfo.getImagePath();

            textBox4.Text = foodInfo.getDescription();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox2.Text))
                MessageBox.Show("Reikia pasirinkti kateogriją!");
            if (string.IsNullOrEmpty(comboBox3.Text))
                MessageBox.Show("Reikia pasirinkti patiekalą!");
            else
            {
                var cMeal = lCategory[comboBox2.SelectedIndex].lMeal[comboBox3.SelectedIndex];

                int cMealID = cMeal.getMealID();

                new SQL().updateFood(Convert.ToDouble(numericUpDown2.Value), textBox5.Text, textBox4.Text, cMealID);

                F.reloadData();

                MessageBox.Show("Sėkmingai paredaguota!");
            }
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox4.Items.Clear();

            for (int i = 0; i < lCategory[comboBox5.SelectedIndex].lMeal.Count; i++)
                comboBox4.Items.Add(lCategory[comboBox5.SelectedIndex].lMeal[i].getName());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(comboBox5.Text))
                MessageBox.Show("Reikia pasirinkti kateogriją!");
            if (string.IsNullOrEmpty(comboBox4.Text))
                MessageBox.Show("Reikia pasirinkti patiekalą!");
            else
            {
                var cMeal = lCategory[comboBox5.SelectedIndex].lMeal[comboBox4.SelectedIndex];

                int cMealID = cMeal.getMealID();

                new SQL().deleteFood(cMealID);

                F.reloadData();

                MessageBox.Show("Įrašas sėkmingai ištrintas!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (new SQL().bUserExists(textBox6.Text, textBox7.Text))
            {
                hasLoggedIn = true;

                panel2.Hide();

                panel1.Show();
            }
        }
    }
}
