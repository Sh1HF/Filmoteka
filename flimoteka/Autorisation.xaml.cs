using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
        SqlConnection sqlConnection;
        DB_connect dB_Connect = new DB_connect();
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

            dB_Connect.openConnection();
            if (!string.IsNullOrEmpty(loginBTH.Text) && !string.IsNullOrWhiteSpace(passwordBTH.Password.ToString()))
            {
                SqlCommand Command0 = new SqlCommand("SELECT * from [Autorisation] WHERE login=@login and passwd=@passwd", dB_Connect.GetConnection());

                Command0.Parameters.AddWithValue("login", loginBTH.Text);
                Command0.Parameters.AddWithValue("passwd", passwordBTH.Password.ToString());

                SqlDataReader DatR;

                DatR = await Command0.ExecuteReaderAsync();

                if (DatR.HasRows == true)
                {
                    await DatR.ReadAsync();

                    if (Convert.ToInt32(DatR["ID_Rules"]) == 1)
                    {
                        AdminControl form = new AdminControl();
                        this.Hide();
                        form.Show();
                    }
                    else
                    {
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Error;
                        MessageBoxResult result;
                        result = System.Windows.MessageBox.Show("Вы не админ окно пока в разработке", "Ошибка", button, icon, MessageBoxResult.Yes);
                    }

                    DatR.Close();
                }
                else
                {
                    Message_box.Visibility = Visibility.Visible;
                    Message_box.Content = "Заполните данные";
                }

            }
        }
            private void loginBTH_TextChanged(object sender, TextChangedEventArgs e)
            {

            }

            private void OnClosing(object sender, CancelEventArgs e)
            {
                e.Cancel = true;
                base.OnClosing(e);
            }

            private void Button_Click(object sender, RoutedEventArgs e)
            {
                Registration form = new Registration();

                this.Hide();
                form.Show();
            }
        }
    }
