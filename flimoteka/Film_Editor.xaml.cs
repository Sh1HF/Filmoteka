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
        //Добавление в таблицу фильм
        string SQL_1 = "INSERT INTO Films(name,description,runtime,release_date,budget)" +
            "VALUES (@Name,@Description,@Runtime,@Release,@Budget) SET @idFilm = SCOPE_IDENTITY()";
        //Добавление в смежную таблицу актер фильм
        string SQL_2 = "INSERT INTO S_ActorFilms(ID_Actors,ID_Films) VALUES (@idActors,@idFilms)";
        //Добавление в смежную таблицу страна фильм
        string SQL_3 = "INSERT INTO S_CountryFilms(ID_Country,ID_Films) VALUES (@idCountry,@idFilms)";
        //Добавление в смежную таблицу реитинг фильм
        string SQL_4 = "INSERT INTO S_ReitingFilms(ID_Reitings,ID_Films) VALUES (@idReitings,@idFilms)";
        //Добавление в смежную таблицу жанр фильм
        string SQL_5 = "INSERT INTO S_GenreFilms(ID_Genre,ID_Films) VALUES (@idGenre,@idFilms)";
        //Добавление в смежную таблицу продюссер фильм
        string SQL_6 = "INSERT INTO S_ProdusersFilms(ID_Produsers,ID_Films) VALUES (@idProdusers,@idFilms)";
        //Добавление в смежную таблицу режжисер фильм
        string SQL_7 = "INSERT INTO S_DirectorFilms(ID_Director,ID_Films) VALUES (@idDirector,@idFilms)";

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
