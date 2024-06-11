using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static flimoteka.Autorisation;
using Microsoft.Data.SqlClient;

namespace flimoteka
{

    public class DB_connect
    {
        SqlConnection connectBD = new SqlConnection(@"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User ID = sh1f; Password = 13791379; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False");
        public void openConnection()
        {
            try
            {
                if (connectBD.State == System.Data.ConnectionState.Closed)
                    connectBD.Open();
            }
            catch(Exception ex)
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = System.Windows.MessageBox.Show("Ошибка подключения к серверу", "Ошибка", button, icon, MessageBoxResult.Yes);
            }
        }
        public void closedConnection()
        {
            if (connectBD.State == System.Data.ConnectionState.Open)
                connectBD.Close();
        }
        public SqlConnection GetConnection()
        {
            return connectBD;
        }
    }
}
