using MyNotes.Helpers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MyNotes.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfoByIetfLanguageTag(Properties.Settings.Default.CultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.CultureName);
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage(Properties.Settings.Default.CultureName);
        }

        private RelayCommand selectSearchTBCommand;

        public ICommand SelectSearchTBCommand
        {
            get
            {
                if (selectSearchTBCommand == null)
                    selectSearchTBCommand = new RelayCommand(SelectSearchTBCommandExecute);
                return selectSearchTBCommand;
            }
        }

        private void SelectSearchTBCommandExecute(object obj)
        {
            searchTB.Focus();
        }
    }
}
