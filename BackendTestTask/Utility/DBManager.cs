using System;
using System.Collections.Generic;
using Npgsql;
using BackendTestTask.Utility;

namespace BackendTestTask
{
    public static class DBManager
    {
        private static string Host = "";
        private static string User = "";
        private static string DBname = "";
        private static string Password = "";
        private static string Port = "";

        private static NpgsqlConnection myConnection;

        private static string connectionString = String.Format("Server={0};Username={1};Database={2};Port={3};Password={4};SSLMode=Prefer",
                    Host,
                    User,
                    DBname,
                    Port,
                    Password
                    );
        private static void OpenConnection()
        {
            myConnection = new NpgsqlConnection(connectionString);

            try
            {
                myConnection.Open();
            }
            catch (Exception)
            {
                AppLogger.GetInstance().Error("Error establishing a database connection");
                throw new Exception();
            }
        }

        private static void CloseConnection()
        {
            myConnection.Close();
        }



        public static void SaveStatisticsToDB(IEnumerable<dynamic> uniqueWords)
        {
            OpenConnection();
            AppLogger.GetInstance().Info("Trying to save statistics to database.");

            try
            {
                foreach (var item in uniqueWords)
                {
                    using (var command = new NpgsqlCommand("INSERT INTO words (word, frequency, website_id) VALUES (@n1, @q1, (SELECT MAX(id) from websites))", myConnection))
                    {
                        command.Parameters.AddWithValue("n1", item.Word);
                        command.Parameters.AddWithValue("q1", item.Frequency);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                AppLogger.GetInstance().Error(ex.Message);
                throw;
            }

            AppLogger.GetInstance().Info("Statistics has been saved to database successefully.");
            Console.WriteLine("Статистика успешно сохранилась в базу данных.");

            CloseConnection();
        }


        public static void SaveWebsiteToDB(string url)
        {
            AppLogger.GetInstance().Info("Trying to save website to database.");

            OpenConnection();
            try
            {
                using (var command = new NpgsqlCommand("INSERT INTO websites (name) VALUES (@n1)", myConnection))
                {
                    command.Parameters.AddWithValue("n1", url);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {

                AppLogger.GetInstance().Error(ex.Message);
                throw;
            }

            AppLogger.GetInstance().Info("Website has been saved to database successefully.");

            CloseConnection();
        }
    }
}
