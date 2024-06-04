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

    public static class DB_connect
    {
        static SqlConnection sqlConnection;

        public static async void Connect()
        {


            string connectstring;


            //connectstring = @"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User Id = sh1f; Password = 13791379;TrustServerCertificate=True";

            connectstring = @"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User ID = sh1f; Password = 13791379; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False";
            sqlConnection = new SqlConnection(connectstring);

            //sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["conStrSheebu"].ConnectionString);

            await sqlConnection.OpenAsync();


        }

        public static SqlConnection sqlconn()
        {
            return sqlConnection;
        }


        /*public void Login_BD(string connectstring)
        {
            string login = Autorisation.GetLogin();

            if (!string.IsNullOrEmpty(Authorization.GetLogin().Text) && !string.IsNullOrWhiteSpace(textBox1.Text) &&
              !string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrWhiteSpace(textBox2.Text))
            {

            }
        
        
        }*/

    }
}
