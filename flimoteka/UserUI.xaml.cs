using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
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

namespace flimoteka
{
    /// <summary>
    /// Логика взаимодействия для UserUI.xaml
    /// </summary>
    public partial class UserUI : Window
    {
        SqlConnection sqlConnection;
        DB_connect dB_Connect = new DB_connect();


        //Переменные для записи значений
        int countryId = 0;
        string countryName = "";
        int directorId = 0;
        string directorName = "";
        int rankingID = 0;
        double rankingNum;
        string rankingmovie= "";
        int yearId = 0;
        int yearNum;
        int budgetId = 0;
        double budgetNum;

        string SQL_1 = "SELECT * FROM Country";
        string SQL_2 = "SELECT surname + ' ' + name AS DirectorFull,ID_Director FROM Director";
        string SQL_3 = "SELECT ID_Reitings, reitingScore, recense FROM Reitings";
        string SQL_4 = "SELECT ID_Films,name,release_date,budget FROM Films";
    

        public UserUI()
        {
            InitializeComponent();
            dB_Connect.openConnection();

            SqlCommand films = new SqlCommand("SELECT name as 'Название',runtime as 'Длительность',release_date as 'Год',Reitings.reitingScore as 'Оценка фильма' FROM Films JOIN S_ReitingFilms ON Films.ID_Films = S_ReitingFilms.ID_Films JOIN Reitings ON S_ReitingFilms.ID_Reitings = Reitings.ID_Reitings", dB_Connect.GetConnection());

            SqlDataAdapter film_reader = new SqlDataAdapter(films);
            DataTable dt = new DataTable("films");
            film_reader.Fill(dt);
            FilmList.ItemsSource = dt.DefaultView;


            // Загрузка данных в combobox страна
            SqlCommand countryR = new SqlCommand(SQL_1, dB_Connect.GetConnection());
            SqlDataAdapter readerCountry = new SqlDataAdapter(countryR);
            SqlDataReader dttables = countryR.ExecuteReader();

            while (dttables.Read())
            {
                countryId = (int)dttables["ID_Country"];
                countryName = (string)dttables["countryName"];
                country_filter.Items.Add(new KeyValuePair<int, string>(countryId, countryName));
            }
            dttables.Close();


            // Загрузка данный в combobox Режисер
            SqlCommand director = new SqlCommand(SQL_2, dB_Connect.GetConnection());
            SqlDataAdapter readerDirector = new SqlDataAdapter(director);
            SqlDataReader dttables1 = director.ExecuteReader();

            while (dttables1.Read())
            {
                {
                    directorId = (int)dttables1["ID_Director"];
                    directorName = (string)dttables1["DirectorFull"];
                    director_filter.Items.Add(new KeyValuePair<int, string>(directorId, directorName));
                }
            }
            dttables1.Close();


            // Загрузка данный в combobox рейтинг
            SqlCommand ranking = new SqlCommand(SQL_3, dB_Connect.GetConnection());
            SqlDataAdapter readerRanking = new SqlDataAdapter(ranking);
            SqlDataReader dttables2 = ranking.ExecuteReader();

            while (dttables2.Read())
            {
                {
                    rankingID = (int)dttables2["ID_Reitings"];
                    rankingNum = (double)dttables2["reitingScore"];
                    rankingmovie = (string)dttables2["recense"];
                    rank_filter.Items.Add(new KeyValuePair<int, double>(rankingID, rankingNum));

                }
            }
            dttables2.Close();

            // Загрузка данный в combobox год
            SqlCommand year = new SqlCommand(SQL_4, dB_Connect.GetConnection());
            SqlDataAdapter readeryear = new SqlDataAdapter(year);
            SqlDataReader dttables3 = year.ExecuteReader();

            while (dttables3.Read())
            {
                {
                    yearId = (int)dttables3["ID_Films"];
                    yearNum = (int)dttables3["release_date"];
                    year_filter.Items.Add(new KeyValuePair<int, int>(yearId, yearNum));

                }
            }
            dttables3.Close();

            // Загрузка данный в combobox бюджет
            SqlCommand budget = new SqlCommand(SQL_4, dB_Connect.GetConnection());
            SqlDataAdapter readerbudget = new SqlDataAdapter(budget);
            SqlDataReader dttables4 = budget.ExecuteReader();

            while (dttables4.Read())
            {
                {
                    budgetId = (int)dttables4["ID_Films"];
                    budgetNum = (double)dttables4["budget"];
                    budget_filter.Items.Add(new KeyValuePair<int, double>(budgetId, budgetNum));

                }
            }
            dttables4.Close();
        }

        private void LoadMovies_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand films = new SqlCommand($"SELECT name as 'Название',runtime as 'Длительность',release_date as 'Год',Reitings.reitingScore as 'Оценка фильма' FROM Films JOIN S_ReitingFilms ON Films.ID_Films = S_ReitingFilms.ID_Films JOIN Reitings ON S_ReitingFilms.ID_Reitings = Reitings.ID_Reitings where name LIKE '%{filmname.Text}%'", dB_Connect.GetConnection());

            DataTable filmssearch = new DataTable("filmssearch");

            SqlDataAdapter film_reader = new SqlDataAdapter(films);
            film_reader.Fill(filmssearch);
            FilmList.ItemsSource = filmssearch.DefaultView;
        }

        private void country_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void director_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void rank_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void year_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void budget_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void apply_filter_btn_Click(object sender, RoutedEventArgs e)
        {
            if (country_filter.SelectedItem != null)
            {
                string country_sel = country_filter.SelectedItem.ToString();
                //string director_sel = director_filter.SelectedItem.ToString();
                //string ranking_sel = rank_filter.SelectedItem.ToString();
                //string year_sel = year_filter.SelectedItem.ToString();
                //string budget_sel = budget_filter.SelectedItem.ToString();

                FilmList.Items.Filter(country_sel);
            }
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
