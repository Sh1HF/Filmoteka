using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.IO;
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
        /// <Старый_код>
        /*
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
        string SQL_8 = "INSERT INTO Reitings(reitingsName, reitingScore, recense) VALUES('Оценка', @ScoreR, @recense); SET @id1=SCOPE_IDENTITY()";
         */
        /// </Старый_код>

        //Множественное добавление в БД
        string SQL_1M = "BEGIN TRANSACTION;\r\n" +
            "\r\nDECLARE @id INT;\r\n" +
            "\r\nDECLARE @id1 INT;\r\n" +
            "\r\nINSERT INTO Films (name, description, runtime, release_date, budget)\r\nVALUES (@Name, @Description, @Runtime, @Release, @Budget);\r\n" +
            "\r\nSET @id = SCOPE_IDENTITY();\r\n" +
            "\r\nINSERT INTO S_ActorFilms (ID_Actors, ID_Films)\r\nVALUES (@idActors, @id);\r\n" +
            "\r\nINSERT INTO S_CountryFilms (ID_Country, ID_Films)\r\nVALUES (@idCountry, @id);\r\n" +
            "\r\nINSERT INTO S_GenreFilms (ID_Genre, ID_Films)\r\nVALUES (@idGenre, @id);\r\n" +
            "\r\nINSERT INTO S_ProdusersFilms (ID_Produsers, ID_Films)\r\nVALUES (@idProdusers, @id);\r\n" +
            "\r\nINSERT INTO S_DirectorFilms (ID_Director, ID_Films)\r\nVALUES (@idDirector, @id);\r\n" +
            "\r\nINSERT INTO Reitings(reitingsName, reitingScore, recense) VALUES('Оценка', @ScoreR, @recense); SET @id1=SCOPE_IDENTITY();\r\n" +
            "\r\nINSERT INTO S_ReitingFilms (ID_Reitings, ID_Films)\r\nVALUES (@id1, @id);\r\n" +
            "\r\nINSERT INTO S_PravoonladatelFilms(ID_Pravoobladatel,ID_Films) VALUES (@PravoobladatelID,@id);" +
            "\r\nCOMMIT;";
        // Выборка 
        string SQL_2 = "SELECT * FROM Country";
        string SQL_3 = "SELECT ID_Actors, name + ' ' + first_name as first_name1 FROM Actors";
        string SQL_4 = "SELECT * FROM Genre";
        string SQL_5 = "SELECT surname + ' ' + name AS DirectorFull,ID_Director FROM Director";
        string SQL_6 = "SELECT ID_Produsers, name + ' ' + first_name AS ProducerFull FROM Produsers";
        string SQL_7 = "SELECT * FROM Pravoobladatel";


        //Переменные для записи значений
        int countryId = 0;
        string countryName = "";
        int actorsId = 0;
        string actorsName = "";
        int genreId = 0;
        string genreName = "";
        int directorId = 0;
        string directorName = "";
        int produserId = 0;
        string produserName = "";
        int ravoobladatelId = 0;
        string ravoobladatelName = "";

        public Film_Editor()
        {
            InitializeComponent();
            dB_Connect.openConnection();

            // Загрузка данный в Страна
            SqlCommand countryR = new SqlCommand(SQL_2, dB_Connect.GetConnection());
            SqlDataAdapter readerCountry = new SqlDataAdapter(countryR);
            SqlDataReader dttables = countryR.ExecuteReader();

            while (dttables.Read())
            {
                countryId = (int)dttables["ID_Country"];
                countryName = (string)dttables["countryName"];
                coutry.Items.Add(new KeyValuePair<int, string>(countryId, countryName));
            }
            dttables.Close();

            // Загрузка данный в Актеры
            SqlCommand actorsR = new SqlCommand(SQL_3, dB_Connect.GetConnection());
            SqlDataAdapter readerActors = new SqlDataAdapter(actorsR);
            SqlDataReader dttables1 = actorsR.ExecuteReader();

            while (dttables1.Read())
            {
                {
                    actorsId = (int)dttables1["ID_Actors"];
                    actorsName = (string)dttables1["first_name1"];
                    Actors_1.Items.Add(new KeyValuePair<int, string>(actorsId, actorsName));
                }
            }
            dttables1.Close();

            // Загрузка данный в Жанр
            SqlCommand genreR = new SqlCommand(SQL_4, dB_Connect.GetConnection());
            SqlDataAdapter readerGenre = new SqlDataAdapter(genreR);
            SqlDataReader dttables2 = genreR.ExecuteReader();

            while (dttables2.Read())
            {
                {
                    genreId = (int)dttables2["ID_Genre"];
                    genreName = (string)dttables2["genreName"];
                    genre_1.Items.Add(new KeyValuePair<int, string>(genreId, genreName));
                }
            }
            dttables2.Close();

            // Загрузка данный в Директор
            SqlCommand directorR = new SqlCommand(SQL_5, dB_Connect.GetConnection());
            SqlDataAdapter readerDirector = new SqlDataAdapter(directorR);
            SqlDataReader dttables3 = directorR.ExecuteReader();

            while (dttables3.Read())
            {
                {
                    directorId = (int)dttables3["ID_Director"];
                    directorName = (string)dttables3["DirectorFull"];
                    Director_1.Items.Add(new KeyValuePair<int, string>(directorId, directorName));
                }
            }
            dttables3.Close();

            // Загрузка данный в Продюссер
            SqlCommand producerR = new SqlCommand(SQL_6, dB_Connect.GetConnection());
            SqlDataAdapter readerProducer = new SqlDataAdapter(producerR);
            SqlDataReader dttables4 = producerR.ExecuteReader();

            while (dttables4.Read())
            {
                {
                    produserId = (int)dttables4["ID_Produsers"];
                    produserName = (string)dttables4["ProducerFull"];
                    producer.Items.Add(new KeyValuePair<int, string>(produserId, produserName));

                }
            }
            dttables4.Close();

            // Загрузка данный в Правообладатель
            SqlCommand pravoobladatelR = new SqlCommand(SQL_7, dB_Connect.GetConnection());
            SqlDataAdapter readerPravoobladatel = new SqlDataAdapter(pravoobladatelR);
            SqlDataReader dttables5 = pravoobladatelR.ExecuteReader();

            while (dttables5.Read())
            {
                {
                    ravoobladatelId = (int)dttables5["ID_Pravoobladatel"];
                    ravoobladatelName = (string)dttables5["pravoobladatelName"];
                    pravoobladatel.Items.Add(new KeyValuePair<int, string>(ravoobladatelId, ravoobladatelName));

                }
            }
            dttables5.Close();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        //Получение ID с Комбобокс 


        private void coutry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (coutry.SelectedItem is KeyValuePair<int, string> selectedCountry)
            {
                countryId = selectedCountry.Key;
                countryName = selectedCountry.Value;
            }
        }

        private void pravoobladatel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pravoobladatel.SelectedItem is KeyValuePair<int, string> selectedpravoobladatel)
            {
                ravoobladatelId = selectedpravoobladatel.Key;
                ravoobladatelName = selectedpravoobladatel.Value;
            }
        }

        private void genre_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (genre_1.SelectedItem is KeyValuePair<int, string> selectedgenre)
            {
                genreId = selectedgenre.Key;
                genreName = selectedgenre.Value;
            }
        }

        private void Actors_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Actors_1.SelectedItem is KeyValuePair<int, string> selectedactors)
            {
                actorsId = selectedactors.Key;
                actorsName = selectedactors.Value;
            }
        }

        private void producer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (producer.SelectedItem is KeyValuePair<int, string> selectedproduser)
            {
                produserId = selectedproduser.Key;
                produserName = selectedproduser.Value;
            }
        }

        private void Director_1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Director_1.SelectedItem is KeyValuePair<int, string> selecteddirector)
            {
                directorId = selecteddirector.Key;
                directorName = selecteddirector.Value;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------
        //Действие на нажатие кнопки Создать
        private void AddRows_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnection;
            DB_connect dB_Connect = new DB_connect();
            dB_Connect.openConnection();

            SqlCommand Command0 = new SqlCommand(SQL_1M, dB_Connect.GetConnection());
            Command0.Parameters.AddWithValue("Name", Name_1.Text);
            Command0.Parameters.AddWithValue("Description", Desctiption_1.Text);
            Command0.Parameters.AddWithValue("Runtime", Chronometrazh.Text);
            Command0.Parameters.AddWithValue("Release", Year_1.Text);
            Command0.Parameters.AddWithValue("Budget", Budget_1.Text);
            Command0.Parameters.AddWithValue("idActors", actorsId);
            Command0.Parameters.AddWithValue("idCountry", countryId);
            Command0.Parameters.AddWithValue("idGenre", genreId);
            Command0.Parameters.AddWithValue("idProdusers", produserId);
            Command0.Parameters.AddWithValue("idDirector", directorId);
            Command0.Parameters.AddWithValue("ScoreR", Reitings_1.Text);
            Command0.Parameters.AddWithValue("recense", Name_1.Text);
            Command0.Parameters.AddWithValue("PravoobladatelID", ravoobladatelId);

            if (pravoobladatel.SelectedIndex > -1 && Director_1.SelectedIndex > -1 && producer.SelectedIndex > -1 && Actors_1.SelectedIndex > -1 && genre_1.SelectedIndex > -1 && coutry.SelectedIndex > -1 && !string.IsNullOrEmpty(Name_1.Text) && !string.IsNullOrEmpty(Desctiption_1.Text) && !string.IsNullOrEmpty(Chronometrazh.Text) && !string.IsNullOrEmpty(Year_1.Text) && !string.IsNullOrEmpty(Budget_1.Text) && !string.IsNullOrEmpty(Reitings_1.Text) && !string.IsNullOrEmpty(Name_1.Text))
            {
                Command0.ExecuteReader();
                if (Command0 != null)
                {
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Information;
                    MessageBoxResult result;
                    result = System.Windows.MessageBox.Show("Запись успешно добавлена", "Успех", button, icon, MessageBoxResult.Yes);
                }
            }
            else
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = System.Windows.MessageBox.Show("Введены не все данные в форме", "Ошибка заполнения БД", button, icon, MessageBoxResult.Yes);
            }
        }
        /*
        SqlParameter idParam = new SqlParameter
        {
            ParameterName = "@id",
            SqlDbType = SqlDbType.Int,
            Direction = ParameterDirection.Output // параметр выходной
        };
        Command0.Parameters.Add(idParam);
        SqlDataAdapter readerResult = new SqlDataAdapter(Command0);
        SqlDataReader dttables10 = Command0.ExecuteReader();
        dttables10.Close();
        if (Command0 != null)
        {
            using (SqlCommand Command1 = new SqlCommand(SQL_2, dB_Connect.GetConnection()))
            {
                Command1.Parameters.AddWithValue("idActors", actorsId);
                Command1.Parameters.AddWithValue("idFilms", idParam);
                Command1.ExecuteReader();

            };

            using (SqlCommand Command2 = new SqlCommand(SQL_3, dB_Connect.GetConnection()))
            {
                Command2.Parameters.AddWithValue("idCountry", countryId);
                Command2.Parameters.AddWithValue("idFilms", idParam);
                Command2.ExecuteReader();
            };


            using (SqlCommand Command3 = new SqlCommand(SQL_5, dB_Connect.GetConnection())) 
            {
            Command3.Parameters.AddWithValue("idGenre", genreId);
            Command3.Parameters.AddWithValue("idFilms", idParam);
            Command3.ExecuteReader(); 
            }

            using(SqlCommand Command4 = new SqlCommand(SQL_6, dB_Connect.GetConnection())){
                Command4.Parameters.AddWithValue("idProdusers", produserId);
                Command4.Parameters.AddWithValue("idFilms", idParam);
                Command4.ExecuteNonQuery();
            };


            using (SqlCommand Command5 = new SqlCommand(SQL_7, dB_Connect.GetConnection()))
            {
                Command5.Parameters.AddWithValue("idDirector", directorId);
                Command5.Parameters.AddWithValue("idFilms", idParam);
                Command5.ExecuteNonQuery();
            }
            SqlCommand Command6 = new SqlCommand(SQL_8, dB_Connect.GetConnection());
            SqlDataAdapter readerScoreRES = new SqlDataAdapter(Command6);
            SqlDataReader dttables11 = Command6.ExecuteReader();
            Command6.Parameters.AddWithValue("ScoreR", Reitings_1.Text);
                Command6.Parameters.AddWithValue("recense", Name_1.Text);
                SqlParameter idScore = new SqlParameter
                {
                    ParameterName = "@id1",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Output // параметр выходной
                };
                Command6.Parameters.Add(idScore);
                dttables11.Close();
            SqlCommand Command7 = new SqlCommand(SQL_4, dB_Connect.GetConnection());
            Command7.Parameters.AddWithValue("idReitings", idScore);
            Command7.Parameters.AddWithValue("idFilms", idParam);
            Command7.ExecuteNonQuery();
            */
    }
}