using System;
using System.Drawing;
using System.Windows.Forms;

namespace Задание02
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int rows = Convert.ToInt32(textBox2.Text);
                int columns = Convert.ToInt32(textBox1.Text);

                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();

                if (columns >= 3 && columns <= 10)
                {
                    textBox1.BackColor = SystemColors.Window;
                    textBox2.BackColor = SystemColors.Window;

                    SetRowsAndColumnsDGV(rows, columns);
                    SetRatings(rows, columns);
                    CalculateAverageRatings(rows, columns);
                }
                else
                {
                    textBox1.BackColor = Color.Red;
                    MessageBox.Show(
                        "Количество судей вне указанного диапазона",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch
            {
                textBox1.BackColor = Color.Red;
                textBox2.BackColor = Color.Red;

                MessageBox.Show(
                    "Неверный формат ввода" + Environment.NewLine + "НЕОБХОДИМО: целое число",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.BackColor = SystemColors.Window;

            textBox2.Text = "";
            textBox2.BackColor = SystemColors.Window;

            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textBox1.BackColor = SystemColors.Window;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textBox2.BackColor = SystemColors.Window;
        }

        double[,] ratingsArray;
        double[] averageRatingsArray;

        int columnsWidth = 60;

        private void SetRowsAndColumnsDGV (int rows, int columns)
        {
            dataGridView1.RowCount = rows;
            dataGridView1.ColumnCount = columns;

            dataGridView2.RowCount = rows;
            dataGridView2.ColumnCount = 1;

            DataGridViewColumn column = dataGridView2.Columns[0];
            column.Width = columnsWidth;
        }

        private void SetRatings(int rows, int columns)
        {
            var rnd = new Random();

            ratingsArray = new double[rows, columns];

            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    ratingsArray[i, j] = Math.Round(rnd.NextDouble(0.0, 10.0), 1);

                    DataGridViewColumn column = dataGridView1.Columns[j];
                    column.Width = columnsWidth;

                    dataGridView1.Rows[i].Cells[j].Value = ratingsArray[i, j].ToString();
                }
            }
        }

        private void CalculateAverageRatings(int rows, int columns)
        {
            averageRatingsArray = new double[rows];

            for (int i = 0; i < rows; i++)
            {
                double min = ratingsArray[i, 0];
                double max = ratingsArray[i, 0];

                for (int j = 0; j < columns; j++)
                {
                    if (ratingsArray[i, j] < min)
                        min = ratingsArray[i, j];

                    if (ratingsArray[i, j] > max)
                        max = ratingsArray[i, j];

                    averageRatingsArray[i] += ratingsArray[i, j]; // суммирование оценок одного фигуриста
                }

                // вычисление средней оценки для одного фигуриста
                averageRatingsArray[i] = Math.Round(
                    (averageRatingsArray[i] - min - max)
                    / (columns - 2), 1);


                dataGridView2.Rows[i].Cells[0].Value = averageRatingsArray[i].ToString();
            }
        }
    }

    public static class RandomExtensions
    {
        public static double NextDouble(this Random random, double minValue, double maxValue)
        {
            return random.NextDouble() * (maxValue - minValue) + minValue;
        }
    }
}
