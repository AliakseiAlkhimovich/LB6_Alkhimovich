using System.Collections.ObjectModel;
using System.Windows;

namespace LB6_Alkhimovich
{
    public partial class MainWindow : Window
    {
        ObservableCollection<Car> Cars;

        public MainWindow()
        {
            Cars = new ObservableCollection<Car>();
            InitializeComponent();
            lBox.DataContext = Cars;
        }

        void FillData()
        {
            Cars.Clear();
            foreach (var item in Car.GetAllCars())
            {
                Cars.Add(item);
            }
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            FillData();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var correctWindow = new Correct();
            correctWindow.Title = "Добавить новый автомобиль";
            correctWindow.EditButton.Visibility = Visibility.Collapsed;
            if (correctWindow.ShowDialog() == true) // Открыть окно и дождаться его закрытия
            {
                var car = new Car()
                {
                    Marka = correctWindow.CarMarka,
                    Model = correctWindow.CarModel,
                    Year = correctWindow.CarYear,
                    Color = correctWindow.CarColor
                };

                car.Insert();
                FillData();
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный автомобиль из списка
            var car = (Car)lBox.SelectedItem;
            // Открываем окно редактирования, передавая ему выбранный автомобиль
            var correctWindow = new Correct(car);
            correctWindow.Title = "Редактировать автомобиль";
            correctWindow.AddButton.Visibility = Visibility.Collapsed;

            if (car != null)
            {
                // Открыть окно и дождаться его закрытия
                if (correctWindow.ShowDialog() == true)
                {
                    // Обновляем данные автомобиля
                    car.Marka = correctWindow.CarMarka;
                    car.Model = correctWindow.CarModel;
                    car.Year = correctWindow.CarYear;
                    car.Color = correctWindow.CarColor;

                    car.Update();
                    FillData(); 
                }
            }
            else
            {
                //MessageBox.Show("Пожалуйста, выберите автомобиль для редактирования.");
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var car = (Car)lBox.SelectedItem;
            if (car != null)
            {
                Car.Delete(car.CarId);
                FillData();
            }
        }
    }
}