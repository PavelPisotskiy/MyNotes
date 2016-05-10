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
    /// Interaction logic for NoteEditorWindow.xaml
    /// </summary>
    public partial class NoteEditorWindow : Window
    {
        public NoteEditorWindow()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfoByIetfLanguageTag(Properties.Settings.Default.CultureName);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.CultureName);
            InitializeComponent();
            this.Language = XmlLanguage.GetLanguage(Properties.Settings.Default.CultureName);           
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //DialogResult = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TitleTextBox_Loaded(object sender, RoutedEventArgs e)
        {
            TitleTextBox.Focus();
            TitleTextBox.SelectionStart = TitleTextBox.Text.Length;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(DialogResult == null)
            {
                DialogResult = false;
            }
            DataContext = null;
        }
    }
}
