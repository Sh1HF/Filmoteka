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

            SqlCommand films = new SqlCommand("SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules\r\nJOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement", dB_Connect.GetConnection());

            films.Parameters.AddWithValue("filmname", filmname.Text);

            SqlDataAdapter film_reader = new SqlDataAdapter(films);
            DataTable dt = new DataTable("films");
            film_reader.Fill(dt);
            AutorisationGrid.ItemsSource = dt.DefaultView;

        }

        private void TabItem_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }


        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AutorisationGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AutorisationGrid_ColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {

        }
    }
}
