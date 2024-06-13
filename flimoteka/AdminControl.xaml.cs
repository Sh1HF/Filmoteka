using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net;
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
using TabControl = System.Windows.Controls.TabControl;
using TabItem = System.Windows.Controls.TabItem;

namespace flimoteka
{
    /// <summary>
    /// Логика взаимодействия для AdminControl.xaml
    /// </summary>
    public partial class AdminControl : Window
    {
        SqlConnection sqlConnection;

        DB_connect dB_Connect = new DB_connect();

        ObservableCollection<string> columns = new ObservableCollection<string>();

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

            dB_Connect.closedConnection();

        }

        private void LoadSubs(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dB_Connect.GetConnection().ConnectionString == null)
                {
                    dB_Connect.GetConnection().ConnectionString = @"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User ID = sh1f; Password = 13791379; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False";
                    dB_Connect.openConnection();

                    SqlCommand subs = new SqlCommand($"SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules\r\nJOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement where (abonnementName LIKE '%{subscribename.Text}%' OR rulesPrivilegies LIKE '%{subscribename.Text}%' OR DateAbonement LIKE '%{subscribename.Text}%' OR login LIKE '%{subscribename.Text}%' OR surname LIKE '%{subscribename.Text}%' OR name LIKE '%{subscribename.Text}%' OR age LIKE '%{subscribename.Text}%');", dB_Connect.GetConnection());

                    DataTable dt = new DataTable("tables");

                    SqlDataAdapter film_reader = new SqlDataAdapter(subs);
                    film_reader.Fill(dt);
                    AutorisationGrid.ItemsSource = dt.DefaultView;

                    dB_Connect.closedConnection();
                }
                else {
                    dB_Connect.openConnection();

                    SqlCommand subs = new SqlCommand($"SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules\r\nJOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement where (abonnementName LIKE '%{subscribename.Text}%' OR rulesPrivilegies LIKE '%{subscribename.Text}%' OR DateAbonement LIKE '%{subscribename.Text}%' OR login LIKE '%{subscribename.Text}%' OR surname LIKE '%{subscribename.Text}%' OR name LIKE '%{subscribename.Text}%' OR age LIKE '%{subscribename.Text}%');", dB_Connect.GetConnection());

                    DataTable dt = new DataTable("tables");

                    SqlDataAdapter film_reader = new SqlDataAdapter(subs);
                    film_reader.Fill(dt);
                    AutorisationGrid.ItemsSource = dt.DefaultView;

                    dB_Connect.closedConnection();
                }
            }
            catch (Exception exc)
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = System.Windows.MessageBox.Show(exc.Message, "Не удалось применить поиск", button, icon, MessageBoxResult.Yes);
            }

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
                dB_Connect.openConnection();

                string tableitem = table_choice.SelectedItem.ToString();

                tab2namlbl.Content += " "  + tableitem;

                SqlCommand table = new SqlCommand(String.Format("select * from {0}", tableitem), dB_Connect.GetConnection());

                dt = new DataTable("table");

                SqlDataAdapter table_reader = new SqlDataAdapter(table);
                table_reader.Fill(dt);
                tablesgrid.ItemsSource = dt.DefaultView;

                foreach (DataGridColumn column in tablesgrid.Columns)
                {
                    if (!(column.Header.ToString().Contains("id")) && (!column.Header.ToString().Contains("ID")))
                    {
                        columns.Add(column.Header.ToString());
                    }

                }
                dB_Connect.closedConnection();
            }
        }

        private void ComboBox_DropDownOpened(object sender, EventArgs e)
        {
            table_choice.SelectedItem = null;
        }

        private void Search_tables(object sender, RoutedEventArgs e)
        {
            if (catalog_search.Text != null)
            {
                dB_Connect.openConnection();

                string tableitem = table_choice.SelectedItem.ToString();

                string catalog_query = $"SELECT * FROM {tableitem} WHERE(";
                foreach (var item in columns)
                {
                    catalog_query += item + $" LIKE '%{catalog_search.Text}%'" + " OR ";
                }
                catalog_query = catalog_query.TrimEnd(new char[] {' ', 'O', 'R' }) + ")";

                SqlCommand catalog = new SqlCommand(catalog_query, dB_Connect.GetConnection());

                DataTable tables = new DataTable("tables");

                SqlDataAdapter film_reader = new SqlDataAdapter(catalog);
                film_reader.Fill(tables);
                tablesgrid.ItemsSource = tables.DefaultView;

                dB_Connect.closedConnection();
            }
            else
            {
                System.Windows.MessageBox.Show("Не заполнено поле поиска");
            }
            
        }

        private void AutorisationGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (!(e.Row == null) && !(e.Row.IsNewItem == true))
            {
                var cell = (DataRowView)AutorisationGrid.SelectedValue;

                string lastname = AutorisationGrid.ColumnFromDisplayIndex(4).Header.ToString();
                string firstname = AutorisationGrid.ColumnFromDisplayIndex(5).Header.ToString();

                string fnvalue = cell.Row.ItemArray[5].ToString();
                string lnvalue = cell.Row.ItemArray[4].ToString();

                string column = e.Column.Header.ToString();

                var unchanged = cell.Row.ItemArray[e.Column.DisplayIndex];

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
                if (!value.Equals(unchanged) && !value.Equals(cell.Row.ItemArray.First().ToString()))
                {
                        dB_Connect.closedConnection();
                        dB_Connect.openConnection();

                        SqlCommand updatevalue = new SqlCommand($"UPDATE Autorisation SET {column} = '{value}' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules JOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement WHERE surname = '{lnvalue}' AND name = '{fnvalue}'", dB_Connect.GetConnection());

                        updatevalue.ExecuteNonQuery();

                        dB_Connect.closedConnection();
                }
                else
                {
                    System.Windows.MessageBox.Show("Поле осталось неизменным");
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

                string column = e.Column.Header.ToString();

                var value = (e.EditingElement as System.Windows.Controls.TextBox).Text;

                var unchanged = cell.Row.ItemArray[e.Column.DisplayIndex];

                if (!value.Equals(unchanged) && !value.Equals(cell.Row.ItemArray.First())) 
                {
                        dB_Connect.openConnection();

                        SqlCommand updatevalue = new SqlCommand($"UPDATE {table_choice.SelectedItem.ToString()} SET {column} = '{value}' WHERE {indexcolumn} = {index}", dB_Connect.GetConnection());

                        updatevalue.ExecuteNonQuery();

                        dB_Connect.closedConnection();
                }

            }
            else 
            {
                e.Cancel = true;
            }
            
        }

        private void AutorisationGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            //нереализованный метод добавления строки
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

        private void Refresh_btn_Click(object sender, RoutedEventArgs e)
        {

            try {
                AutorisationGrid.ItemsSource = null;
                AutorisationGrid.Items.Clear();
                AutorisationGrid.Columns.Clear();
                AutorisationGrid.Items.Refresh();


                if (dB_Connect.GetConnection().ConnectionString == null)
                {
                    dB_Connect.GetConnection().ConnectionString = @"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User ID = sh1f; Password = 13791379; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False";
                    dB_Connect.openConnection();

                    SqlCommand refreshsubs = new SqlCommand("SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules JOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement", dB_Connect.GetConnection());

                    SqlDataAdapter refreshreader = new SqlDataAdapter(refreshsubs);
                    DataTable refreshfilms = new DataTable("films");
                    refreshreader.Fill(refreshfilms);
                    AutorisationGrid.ItemsSource = refreshfilms.DefaultView;
                }
                else {
                    dB_Connect.GetConnection().ConnectionString = @"Data Source = dyuhahome.ddns.net,1381; Initial Catalog = FilmotekaDB; User ID = sh1f; Password = 13791379; Encrypt = False; Trust Server Certificate = True; Application Intent = ReadWrite; Multi Subnet Failover = False";
                    dB_Connect.openConnection();

                    SqlCommand refreshsubs = new SqlCommand("SELECT abonnementName as 'Абонемент',rulesPrivilegies as 'Права',DateAbonement as 'Дата абонемента',login as 'Логин',surname as 'Фамилия',name as 'Имя',age as 'Дата рождения' FROM Autorisation JOIN Rules ON Rules.ID_Rules = Autorisation.ID_Rules JOIN Abonnement ON Abonnement.ID_Abonnement = Autorisation.ID_Abonement", dB_Connect.GetConnection());

                    SqlDataAdapter refreshreader = new SqlDataAdapter(refreshsubs);
                    DataTable refreshfilms = new DataTable("films");
                    refreshreader.Fill(refreshfilms);
                    AutorisationGrid.ItemsSource = refreshfilms.DefaultView;
                }

                dB_Connect.closedConnection();

            }
            catch (Exception exc)
            {
                MessageBoxButton button = MessageBoxButton.OK;
                MessageBoxImage icon = MessageBoxImage.Error;
                MessageBoxResult result;
                result = System.Windows.MessageBox.Show(exc.Message,"Не удалось обновить таблицу",button, icon, MessageBoxResult.Yes);
            }

        }


        private void LoadPageInTab(TabItem tabItem, Uri pageUri)
        {
            Frame frame = FindVisualChild<Frame>(tabItem);
            if (frame != null)
            {
                frame.Source = pageUri;
            }
        }

        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                TabItem selectedTab = (TabItem)tabcontrol.SelectedItem;
                if (selectedTab != null & selectedTab == editload) 
                {
                    Film_Editor form = new Film_Editor();
                    form.Show();
                    //LoadPageInTab(editload, new Uri("Film_Editor.xaml", UriKind.Relative));
                }
            }
        }
    }
}
