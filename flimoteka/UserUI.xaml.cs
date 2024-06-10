using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        public UserUI()
        {
            InitializeComponent();
            dB_Connect.openConnection();

            SqlCommand films = new SqlCommand("SELECT name as 'Название',runtime as 'Длительность',release_date as 'Год',Reitings.reitingScore as 'Оценка фильма' FROM Films JOIN S_ReitingFilms ON Films.ID_Films = S_ReitingFilms.ID_Films JOIN Reitings ON S_ReitingFilms.ID_Reitings = Reitings.ID_Reitings", dB_Connect.GetConnection());

            SqlDataAdapter film_reader = new SqlDataAdapter(films);
            DataTable dt = new DataTable("films");
            film_reader.Fill(dt);
            FilmList.ItemsSource = dt.DefaultView;
        }
    }
}
