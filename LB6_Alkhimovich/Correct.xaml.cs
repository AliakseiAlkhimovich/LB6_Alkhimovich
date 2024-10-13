using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace LB6_Alkhimovich
{

    public partial class Correct : Window
    {
        public string CarMarka { get; private set; }
        public string CarModel { get; private set; }
        public int CarYear { get; private set; }
        public string CarColor { get; private set; }
        public Car _car;

        public Correct(Car car)
        {
            InitializeComponent();
            if (car != null)
            {
                _car = car;
                Marka.Text = _car.Marka;
                Model.Text = _car.Model;
                Year.Text = _car.Year.ToString();
                Color.Text = _car.Color;
            }
            else
            { MessageBox.Show("Пожалуйста, выберите автомобиль для редактирования."); this.Close(); }
        }

        public Correct()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                CarMarka = Marka.Text;
                CarModel = Model.Text;
                CarYear = int.Parse(Year.Text);
                CarColor = Color.Text;
                DialogResult = true; // Закрыть окно и вернуть результат
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInputs())
            {
                CarMarka = Marka.Text;
                CarModel = Model.Text;
                CarYear = int.Parse(Year.Text);
                CarColor = Color.Text;
                DialogResult = true; // Закрыть окно и вернуть результат
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Marka.Text))
            {
                MessageBox.Show("Пожалуйста, введите марку автомобиля.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(Model.Text))
            {
                MessageBox.Show("Пожалуйста, введите модель автомобиля.");
                return false;
            }

            if (!int.TryParse(Year.Text, out int year) || Year.Text.Length != 4)
            {
                MessageBox.Show("Пожалуйста, введите год автомобиля в формате 4 цифр.");
                return false;
            }

            return true;
        }
    }

}
