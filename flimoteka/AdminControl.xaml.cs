using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
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

        DataTable dt;

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

        private void LoadSubs(object sender, RoutedEventArgs e)
        {
            SqlCommand subs = new SqlCommand("SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules\r\nJOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement where abonnementName=@abonnementName;", dB_Connect.GetConnection());

            subs.Parameters.AddWithValue("abonnementName", subscribename.Text);

            DataTable dt = new DataTable("tables");

            SqlDataAdapter film_reader = new SqlDataAdapter(subs);
            film_reader.Fill(dt);
            AutorisationGrid.ItemsSource = dt.DefaultView;
        }


        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (table_choice.SelectedItem != null)
            {
                string tableitem = table_choice.SelectedItem.ToString();

                SqlCommand table = new SqlCommand(String.Format("select * from {0}", tableitem), dB_Connect.GetConnection());

                dt = new DataTable("table");

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

            DataTable tables = new DataTable("tables");

            SqlDataAdapter film_reader = new SqlDataAdapter(catalog);
            film_reader.Fill(tables);
            tablesgrid.ItemsSource = tables.DefaultView;
        }

        private void AutorisationGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!(e.Row == null) && !(e.Row.IsNewItem == true))
            {
                var cell = (DataRowView)AutorisationGrid.SelectedValue;

                //int index = (int)cell.Row.ItemArray. First();

                string lastname = AutorisationGrid.ColumnFromDisplayIndex(4).Header.ToString();
                string firstname = AutorisationGrid.ColumnFromDisplayIndex(5).Header.ToString();

                string fnvalue = cell.Row.ItemArray[5].ToString();
                string lnvalue = cell.Row.ItemArray[4].ToString();
                



                //string column = AutorisationGrid.CurrentCell.Column.Header.ToString();
                string column = e.Column.Header.ToString();

                switch (column)
                {
                    case "Абонемент": 
                        column = "abonnementName";
                        break;
                    case "rulesPrivilegies":
                        column = "rulesPrivilegies";
                        break;
                    case "Дата абонемента":
                        column = "DateAbonement";
                        break;
                    case "Логин":
                        column = "login";
                        break;
                    case "Фамилия":
                        column = "surname";
                        break;
                    case "Имя":
                        column = "name";
                        break;
                    case "Дата рождения":
                        column = "age";
                        break;
                    default:
                        break;
                }

                var value = (e.EditingElement as System.Windows.Controls.TextBox).Text;

                //var unchanged = cell.Row.ItemArray.Last().ToString();

                using (dB_Connect.GetConnection())
                {
                    if (dB_Connect.GetConnection().State == System.Data.ConnectionState.Open)
                    {
                        dB_Connect.openConnection();
                    }

                    SqlCommand updatevalue = new SqlCommand($"UPDATE Autorisation SET {column} = '{value}' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules JOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement WHERE surname = '{lnvalue}' AND name = '{fnvalue}'", dB_Connect.GetConnection());

                    updatevalue.ExecuteNonQuery();

                }

            }
            else
            {
                e.Cancel = true;
            }
        }

        private void tablesgrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {

            if (!(e.Row == null) && !(e.Row.IsNewItem == true))
            {
                var cell = (DataRowView)tablesgrid.SelectedValue;

                int index = (int)cell.Row.ItemArray.First();

                string indexcolumn = tablesgrid.ColumnFromDisplayIndex(0).Header.ToString();

                string column = tablesgrid.CurrentCell.Column.Header.ToString();

                var value = (e.EditingElement as System.Windows.Controls.TextBox).Text;

                var unchanged = cell.Row.ItemArray.Last().ToString();

                if (value != null && !value.Equals(unchanged) && !value.Equals(index.ToString())) 
                {
                    using (dB_Connect.GetConnection())
                    {
                        if (dB_Connect.GetConnection().State == System.Data.ConnectionState.Open)
                        {
                            dB_Connect.openConnection();
                        }

                        SqlCommand updatevalue = new SqlCommand($"UPDATE {table_choice.SelectedItem.ToString()} SET {column} = '{value}' WHERE {indexcolumn} = {index}", dB_Connect.GetConnection());

                        updatevalue.ExecuteNonQuery();

                    }
                }

            }
            else 
            {
                e.Cancel = true;
            }
            
        }

        private void AutorisationGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {

        }

        private void Tablesgrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            /* Не реализованно (добавление в форме data grid)
            if (!(e.Row == null) && e.Row.IsNewItem == true)
            {
                DataTable row = e.Row.Item as DataTable;

                List<string> columns = new List<string>();

                foreach (DataGridColumn item in tablesgrid.Columns)
                {
                    columns.Add(item.Header.ToString());
                }

                using (dB_Connect.GetConnection())
                {
                    if (dB_Connect.GetConnection().State == System.Data.ConnectionState.Open)
                    {
                        dB_Connect.openConnection();
                    }

                    SqlCommand updatevalue = new SqlCommand($"INSERT INTO {table_choice.SelectedItem.ToString()} ({columns}) VALUES ({row})", dB_Connect.GetConnection());

                    updatevalue.ExecuteNonQuery();

                }*/
            }

        private void AddMovie_btn_Click(object sender, RoutedEventArgs e)
        {
            Film_Editor form = new Film_Editor();
            form.Show();
        }
    }
}
