using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    /// Логика взаимодействия для Autorisation.xaml
    /// </summary>
    public partial class Autorisation : Window
    {
        public Autorisation()
        {
            InitializeComponent();
        }

        public TextBox GetLogin()
        {
            return loginBTH;
        }

        public PasswordBox GetPassword()
        {
            return passwordBTH;
        }

        private async void Button_LoginAsync(object sender, RoutedEventArgs e)
        {
            //DB_connect.Connect();

            SqlConnection sqlConnection;
            string connectstring;

            connectstring = @"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User ID = sh1f; Password = 13791379; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False";
            sqlConnection = new SqlConnection(connectstring);


            await sqlConnection.OpenAsync();

            if (!string.IsNullOrEmpty(loginBTH.Text) && !string.IsNullOrWhiteSpace(passwordBTH.Password.ToString()))
            {
                SqlCommand Command0 = new SqlCommand("SELECT * from [Autorisation] WHERE login=@login and passwd=@passwd", sqlConnection);

                Command0.Parameters.AddWithValue("login", loginBTH.Text);
                Command0.Parameters.AddWithValue("passwd", passwordBTH.Password.ToString());

                SqlDataReader DatR;

                DatR = await Command0.ExecuteReaderAsync();

                if (DatR.HasRows == true)
                {
                    await DatR.ReadAsync();

                    if (Convert.ToInt32(DatR["ID_Autorisation"]) == 1)
                    {
                        AppControl form = new AppControl();
                        form.Show();
                        this.Hide();
                    }
                    /*else
                    {
                        Form3 form2 = new Form3();
                        fomr2.Show();
                        this.Hide();
                    }*/

                    DatR.Close();

                }

            }
            else
            {
                Message_box.Visibility = Visibility.Visible;
                Message_box.Content = "Заполните данные";
            }
        }

        private void loginBTH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
