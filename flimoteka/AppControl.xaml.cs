using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для AppControl.xaml
    /// </summary>
    public partial class AppControl : Window
    {
        public AppControl()
        {
            InitializeComponent();
        }

        private void Button_FilmAddAsync(object sender, RoutedEventArgs e)
        {
            Film_Editor form = new Film_Editor();
            form.Show();
            this.Hide();
        }

        private void Button_RowsAsync(object sender, RoutedEventArgs e)
        {
            Rows_editor form = new Rows_editor();
            form.Show();
            this.Hide();
        }

        private void Button_AdminAsync(object sender, RoutedEventArgs e)
        {
            Admin_panel form = new Admin_panel();
            form.Show();
            this.Hide();
        }

        private void Button_ExitAsync(object sender, RoutedEventArgs e)
        {
            Autorisation form = new Autorisation();
            form.Show();
            this.Hide();
        }
    }
}
