using System;
using System.Collections.Generic;
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
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;


namespace flimoteka
{

    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        SqlConnection sqlConnection;
        DB_connect dB_Connect = new DB_connect();
        public Registration()
        {
            InitializeComponent();
            dB_Connect.openConnection();
        }
        public DatePicker GetAge()
        {
            return DateRR;
        }

        protected PasswordBox GetPassword()
        {
            return Password1;
        }

        protected PasswordBox GetPasswordR()
        {
            return PasswordR;
        }


        protected void Regist_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var date1 = Convert.ToDateTime(DateRR.Text).ToString("yyyy-MM-dd");



                if (Password1.Password.ToString() == PasswordR.Password.ToString() && Password1.Password.Length >= 8 && !string.IsNullOrEmpty(date1) && !string.IsNullOrEmpty(LoginR.Text) && !string.IsNullOrEmpty(Password1.Password.ToString()) && !string.IsNullOrEmpty(Surname.Text) && !string.IsNullOrEmpty(NameR.Text))
                {
                    SqlCommand Command0 = new SqlCommand("INSERT INTO Autorisation(ID_Rules,login,passwd,surname,name,age) VALUES (2,@login,@passwd,@Surname,@Name,@Age) ", dB_Connect.GetConnection());

                    Command0.Parameters.AddWithValue("login", LoginR.Text);
                    Command0.Parameters.AddWithValue("passwd", Password1.Password.ToString());
                    Command0.Parameters.AddWithValue("Surname", Surname.Text);
                    Command0.Parameters.AddWithValue("Name", NameR.Text);
                    Command0.Parameters.AddWithValue("Age", date1);
                    Command0.ExecuteNonQuery();
                    if (Command0 != null)
                    {
                        MessageBoxButton button = MessageBoxButton.OK;
                        MessageBoxImage icon = MessageBoxImage.Question;
                        MessageBoxResult result;
                        result = System.Windows.MessageBox.Show("Вы успешно зарегистрировались", "Успех", button, icon, MessageBoxResult.Yes);

                        dB_Connect.closedConnection();

                        Autorisation form = new Autorisation();
                        form.Show();
                        this.Close();
                    }
                }
                else
                {
                    MessageBoxButton button = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Error;
                    MessageBoxResult result;
                    result = System.Windows.MessageBox.Show("Пароли не совпадают", "Ошибка", button, icon, MessageBoxResult.Yes);
                }
            }

            catch (Exception ex) {

                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = System.Windows.MessageBox.Show("Не все поля заполнены", "Исключение не обработанно", button, icon, MessageBoxResult.Yes);
            
            }
        }
    }
}
