using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace flimoteka
{

    /// <summary>
    /// Логика взаимодействия для Film_Editor.xaml
    /// </summary>
    public partial class Film_Editor : Window
    {
        SqlConnection sqlConnection;
        DB_connect dB_Connect = new DB_connect();
        //Добавление в таблицу фильм
        string SQL_1 = "INSERT INTO Films(name,description,runtime,release_date,budget) VALUES (@Name,@Description,@Runtime,@Release,@Budget); SET @id=SCOPE_IDENTITY()";
        //Добавление в смежную таблицу актер фильм
        string SQL_2 = "INSERT INTO S_ActorFilms(ID_Actors,ID_Films) VALUES (@idActors,@idFilms)";
        //Добавление в смежную таблицу страна фильм
        string SQL_3 = "INSERT INTO S_CountryFilms(ID_Country,ID_Films) VALUES (@idCountry,@idFilms)";
        //Добавление в смежную таблицу реитинг фильм
        string SQL_4 = "INSERT INTO S_ReitingFilms(ID_Reitings,ID_Films) VALUES (@idReitings,@idFilms)";
        //Добавление в смежную таблицу жанр фильм
        string SQL_5 = "INSERT INTO S_GenreFilms(ID_Genre,ID_Films) VALUES (@idGenre,@idFilms)";
        //Добавление в смежную таблицу продюссер фильм
        string SQL_6 = "INSERT INTO S_ProdusersFilms(ID_Produsers,ID_Films) VALUES (@idProdusers,@idFilms)";
        //Добавление в смежную таблицу режжисер фильм
        string SQL_7 = "INSERT INTO S_DirectorFilms(ID_Director,ID_Films) VALUES (@idDirector,@idFilms)";
        //Добавление в таблицу рецензии
        string SQL_8 = "INSERT INTO Reitings(reitingsName, reitingScore, recense) VALUES('Оценка', @ScoreR, @recense) SET @idScore = SCOPE_IDENTITY()";

        // Выборка 
        string SQL_9 = "SELECT countryName FROM Country";
        string SQL_10 = "SELECT name + ' ' + first_name as first_name1 FROM Actors";
        string SQL_11 = "SELECT * FROM Genre";
        string SQL_12 = "SELECT surname + ' ' + name AS DirectorFull FROM Director";
        string SQL_13 = "SELECT name + ' ' + first_name AS ProducerFull FROM Produsers";
        string SQL_14 = "SELECT * FROM Pravoobladatel";

        public Film_Editor()
        {
            InitializeComponent();
            dB_Connect.openConnection();
            SqlCommand countryR = new SqlCommand(SQL_9, dB_Connect.GetConnection());

            SqlDataAdapter readerCountry = new SqlDataAdapter(countryR);
            SqlDataReader dttables = countryR.ExecuteReader();

            while (dttables.Read())
            {
                {
                    coutry.Items.Add(dttables["countryName"]);
                }
            }
            dttables.Close();
            SqlCommand actorsR = new SqlCommand(SQL_10, dB_Connect.GetConnection());

            SqlDataAdapter readerActors= new SqlDataAdapter(actorsR);

            SqlDataReader dttables1 = actorsR.ExecuteReader();

            while (dttables1.Read())
            {
                {
                    Actors_1.Items.Add(dttables1["first_name1"]);
                }
            }
            dttables1.Close();
            SqlCommand genreR = new SqlCommand(SQL_11, dB_Connect.GetConnection());

            SqlDataAdapter readerGenre = new SqlDataAdapter(genreR);

            SqlDataReader dttables2 = genreR.ExecuteReader();

            while (dttables2.Read())
            {
                {
                    genre_1.Items.Add(dttables2["genreName"]);
                }
            }
            dttables2.Close();
            SqlCommand directorR = new SqlCommand(SQL_12, dB_Connect.GetConnection());

            SqlDataAdapter readerDirector = new SqlDataAdapter(directorR);

            SqlDataReader dttables3 = directorR.ExecuteReader();

            while (dttables3.Read())
            {
                {
                    Director_1.Items.Add(dttables3["DirectorFull"]);
                }
            }
            dttables3.Close();
            SqlCommand producerR = new SqlCommand(SQL_13, dB_Connect.GetConnection());

            SqlDataAdapter readerProducer = new SqlDataAdapter(producerR);

            SqlDataReader dttables4 = producerR.ExecuteReader();

            while (dttables4.Read())
            {
                {
                    producer.Items.Add(dttables4["ProducerFull"]);
                }
            }
            dttables4.Close();
            SqlCommand pravoobladatelR = new SqlCommand(SQL_14, dB_Connect.GetConnection());

            SqlDataAdapter readerPravoobladatel = new SqlDataAdapter(pravoobladatelR);

            SqlDataReader dttables5 = pravoobladatelR.ExecuteReader();

            while (dttables5.Read())
            {
                {
                    pravoobladatel.Items.Add(dttables5["pravoobladatelName"]);
                }
            }
            dttables5.Close();
        }
        /*
        public class Pravoobladatel
        {
            public int IdPravoobladatel { get; set; }
            public string PravoobladatelN { get; set; }
        }
        public class Produsers
        {
            public int IdProdusers { get; set; }
            public string ProdusersN { get; set; }
        }
        public class Director
        {
            public int IdDirector { get; set; }
            public string DirectorN { get; set; }
        }
        public class Genre
        {
            public int IdGenre { get; set; }
            public string GenreN { get; set; }
        }
        public class Actors
        {
            public int IdActors { get; set; }
            public string ActorsN { get; set; }
        }
        public class Country
        {
            public int IdCountry { get; set; }
            public string CountryN { get; set; }
        }
        */

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void AddRows_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection;
            DB_connect dB_Connect = new DB_connect();

            try
            {
                dB_Connect.openConnection();

                SqlCommand Command0 = new SqlCommand(SQL_1, dB_Connect.GetConnection());
                Command0.Parameters.AddWithValue("Name", Name_1.Text);
                Command0.Parameters.AddWithValue("Description", Desctiption_1.Text);
                Command0.Parameters.AddWithValue("Runtime", Chronometrazh.Text);
                Command0.Parameters.AddWithValue("Release", Year_1.Text);
                Command0.Parameters.AddWithValue("Budget", Budget_1.Text);

                SqlParameter idParam = new SqlParameter
                {
                    ParameterName = "@id",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output // параметр выходной
                };
                Command0.Parameters.Add(idParam);
                Command0.ExecuteReader();
                if (Command0 != null)
                {

                }
            }

            catch(Exception ex)
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = System.Windows.MessageBox.Show($"{ex}", "Ошибка", button, icon, MessageBoxResult.Yes);
            }
        }

        private void coutry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}