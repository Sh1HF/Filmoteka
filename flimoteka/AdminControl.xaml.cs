using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
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
using static MaterialDesignThemes.Wpf.Theme;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace flimoteka
{
    /// <summary>
    /// Логика взаимодействия для AdminControl.xaml
    /// </summary>
    public partial class AdminControl : Window
    {
        SqlConnection sqlConnection;

        DB_connect dB_Connect = new DB_connect();

        string columnName;

        public AdminControl()
        {
            InitializeComponent();

            dB_Connect.openConnection();

            SqlCommand users = new SqlCommand("SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules\r\nJOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement", dB_Connect.GetConnection());

            SqlDataAdapter userreader = new SqlDataAdapter(users);
            DataTable dtfilms = new DataTable("films");
            userreader.Fill(dtfilms);
            AutorisationGrid.ItemsSource = dtfilms.DefaultView;

            SqlCommand tables = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", dB_Connect.GetConnection());

            SqlDataAdapter tablereader = new SqlDataAdapter(tables);
            DataTable dttables = new DataTable("tables");
            tablereader.Fill(dttables);

            for (int i = 0; i < dttables.Rows.Count; i++)
            {
                table_choice.Items.Add(dttables.Rows[i]["TABLE_NAME"]);
            }


        }


        private void LoadMovies(object sender, RoutedEventArgs e)
        {

            SqlCommand films = new SqlCommand("Select * from [Films] where name=@name;", dB_Connect.GetConnection());

            films.Parameters.AddWithValue("name", filmname.Text);

            DataTable dt = new DataTable("tables");

            SqlDataAdapter film_reader = new SqlDataAdapter(films);
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

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (table_choice.SelectedItem != null)
            {
                string tableitem = table_choice.SelectedItem.ToString();

                SqlCommand table = new SqlCommand(String.Format("select * from {0}", tableitem), dB_Connect.GetConnection());

                DataTable dt = new DataTable("table");

                SqlDataAdapter table_reader = new SqlDataAdapter(table);
                table_reader.Fill(dt);
                tablesgrid.ItemsSource = dt.DefaultView;

                foreach (DataGridColumn column in tablesgrid.Columns)
                {
                    if (!(column.Header.ToString().Contains("id")) && (!column.Header.ToString().Contains("ID"))){ 
                    columnName = column.Header.ToString();
                    }

                }
            }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            table_choice.SelectedItem = null;
        }

        private void Search_tables(object sender, RoutedEventArgs e)
        {
            string tableitem = table_choice.SelectedItem.ToString();

            SqlCommand catalog = new SqlCommand($"select * from {tableitem} where {columnName} LIKE '%{catalog_search.Text}%'", dB_Connect.GetConnection());

            DataTable dt = new DataTable("tables");

            SqlDataAdapter film_reader = new SqlDataAdapter(catalog);
            film_reader.Fill(dt);
            tablesgrid.ItemsSource = dt.DefaultView;
        }

        private void tablesgrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            string tableitem = tablesgrid.SelectedItem.ToString();
            if (tableitem != null)
            {

            }
        }
    }
}
