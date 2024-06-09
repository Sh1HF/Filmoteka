using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для Film_Editor.xaml
    /// </summary>
    public partial class Film_Editor : Window
    {
        public Film_Editor()
        {
            InitializeComponent();
        }

        private void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            base.OnClosing(e);
        }
    }


}
