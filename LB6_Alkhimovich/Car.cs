using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace LB6_Alkhimovich
{
    public class Car
    {
        public int CarId { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }

        private static string connString;

        static Car()
        {
            // Получение строки подключения из файла конфигурации
            connString = ConfigurationManager.ConnectionStrings["CarConnection"].ConnectionString;
        }

        public override string ToString()
        {
            return String.Format("CarId={0} - Marka: {1} - Model: {2} - Year: {3} - Color: {4}", CarId, Marka, Model, Year, Color);
        }

        public static IEnumerable<Car> GetAllCars()
        {
            var commandString = "SELECT CarId, Marka, Model, Year, Color FROM [Table]";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand getAllCommand = new SqlCommand(commandString, connection);
                connection.Open();
                using (var reader = getAllCommand.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var carId = reader.GetInt32(0);
                            var marka = reader.GetString(1);
                            var model = reader.GetString(2);
                            var year = reader.GetInt32(3);
                            var color = reader.IsDBNull(4) ? null : reader.GetString(4);

                            var car = new Car
                            {
                                CarId = carId,
                                Marka = marka,
                                Model = model,
                                Year = year,
                                Color = color
                            };
                            yield return car;
                        }
                    }
                }
            }
        }

        public void Insert()
        {
            var commandString = "INSERT INTO [Table] (Marka, Model, Year, Color) VALUES (@marka, @model, @year, @color)";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand insertCommand = new SqlCommand(commandString, connection);

                insertCommand.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("@marka", Marka),
                    new SqlParameter("@model", Model),
                    new SqlParameter("@year", Year),
                    new SqlParameter("@color", (object)Color ?? DBNull.Value),
                });

                connection.Open();
                insertCommand.ExecuteNonQuery();
            }
        }

        public static Car GetCar(int id)
        {
            foreach (var car in GetAllCars())
            {
                if (car.CarId == id)
                    return car;
            }
            return null;
        }

        public void Update()
        {
            var commandString = "UPDATE [Table] SET Marka=@marka, Model=@model, Year=@year, Color=@color WHERE CarId=@id";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand updateCommand = new SqlCommand(commandString, connection);

                updateCommand.Parameters.AddRange(new SqlParameter[] {
                    new SqlParameter("marka", Marka),
                    new SqlParameter("model", Model),
                    new SqlParameter("year", Year),
                    new SqlParameter("color", (object)Color ?? DBNull.Value),
                    new SqlParameter("id", CarId),
                });

                connection.Open();
                updateCommand.ExecuteNonQuery();
            }
        }

        public static void Delete(int id)
        {
            var commandString = "DELETE FROM [Table] WHERE CarId=@id";
            using (SqlConnection connection = new SqlConnection(connString))
            {
                SqlCommand deleteCommand = new SqlCommand(commandString, connection);
                deleteCommand.Parameters.AddWithValue("id", id);
                connection.Open();
                deleteCommand.ExecuteNonQuery();
            }
        }
    }
}