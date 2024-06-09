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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace flimoteka
{
    /// <summary>
    /// Логика взаимодействия для AdminControl.xaml
    /// </summary>
    public partial class AdminControl : Window
    {
        SqlConnection sqlConnection;

        DB_connect dB_Connect = new DB_connect();
        public AdminControl()
        {
            InitializeComponent();

            dB_Connect.openConnection();

            SqlCommand films = new SqlCommand("Select * from [films] where filmname=@filmname;", dB_Connect.GetConnection());

            films.Parameters.AddWithValue("filmname", filmname.Text);

            SqlDataAdapter film_reader = new SqlDataAdapter(films);
            DataTable dt = new DataTable("films");
            film_reader.Fill(dt);
            FilmGrid.ItemsSource = dt.DefaultView;

        }

        private void TabItem_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }


        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }
    }
}
