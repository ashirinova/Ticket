using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Ticket
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] cities = { "Bakı", "Gəncə", "Sumqayıt", "Şəki", "Qəbələ", "Lənkəran", "Xaçmaz", "Naxçıvan" };

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.AddRange(cities);
            comboBox2.Items.AddRange(cities);
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
            if (comboBox2.Items.Count > 1) comboBox2.SelectedIndex = 1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult d = MessageBox.Show("Proqramdan çıxış edilsinmi?", "Bildiriş", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            string t = comboBox1.Text;
            comboBox1.Text = comboBox2.Text;
            comboBox2.Text = t;
        }
        int n = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(comboBox1.Text) ||
                string.IsNullOrWhiteSpace(comboBox2.Text) ||
                !maskedTextBox1.MaskCompleted ||
                string.IsNullOrWhiteSpace(textBox1.Text) ||
                string.IsNullOrWhiteSpace(textBox2.Text) ||
                string.IsNullOrWhiteSpace(textBox3.Text) ||
                !maskedTextBox2.MaskCompleted ||
                string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Xahiş olunur bütün xanaları tam doldurun!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (comboBox1.Text == comboBox2.Text)
            {
                MessageBox.Show("Eyni şəhərlərə gediş yoxdur!", "Məlumat", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            n++;
            listBox1.Items.Add(n.ToString() + ") " + comboBox1.Text + " " + comboBox2.Text + " "
                + maskedTextBox1.Text + " " + textBox1.Text);
            string fin = textBox3.Text.Trim();
            if (fin.Length != 7 || !Regex.IsMatch(fin, @"[a-zA-Z]") || !Regex.IsMatch(fin, @"[0-9]"))
            {
                MessageBox.Show("FİN kod dəqiq 7 simvoldan ibarət olmalı və tərkibində həm hərf, həm də rəqəm olmalıdır!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string email = textBox4.Text.Trim();
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Düzgün e-poçt (email) ünvanı daxil edin!", "Xəta", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            YenidenNomrele();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                YenidenNomrele();
            }
            else
            {
                MessageBox.Show("Lütfən silmək istədiyiniz bileti siyahıdan seçin!", "Xəbərdarlıq", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void YenidenNomrele()
        {
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string item = listBox1.Items[i].ToString();
                if (item.Contains(") "))
                {
                    item = item.Substring(item.IndexOf(") ") + 2);
                }
                listBox1.Items[i] = $"{i + 1}) {item}";
            }
        }
    }
}