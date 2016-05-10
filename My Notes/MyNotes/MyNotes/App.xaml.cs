using MyNotes.Helpers;
using MyNotes.Model;
using MyNotes.MyNotesAuthorizationService;
using MyNotes.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace MyNotes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ServiceLocator.Instance.Register<INotesManager>(new NotesManager());
            ServiceLocator.Instance.Register<IMessageService>(new MessageService());
            ServiceLocator.Instance.Register<IWindowManager>(new WindowManager());
            

            string email = MyNotes.Properties.Settings.Default.Email;
            string password = MyNotes.Properties.Settings.Default.Password;
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                try
                {
                    AuthorizationClient client = new AuthorizationClient();
                    IAuthorization authorization = client.ChannelFactory.CreateChannel();

                    int id = authorization.SignIn(email, password);
                    if (id == 0)
                    {
                        AuthorizationWindow window = new AuthorizationWindow();
                        window.Show();
                    }
                    else
                    {
                        ServiceLocator.Instance.Resolve<INotesManager>().UserId = id;
                        MainWindow mainWindow = new MainWindow();
                        Application.Current.MainWindow = mainWindow;
                        mainWindow.Show();

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(AuthorizationWindow))
                            {
                                window.Close(); break;
                            }
                        }
                    }

                }
                catch (CommunicationException)
                {
                    MainWindow mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                    mainWindow.Show();

                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(AuthorizationWindow))
                        {
                            window.Close(); break;
                        }
                    }
                }
            }
            else
            {
                AuthorizationWindow window = new AuthorizationWindow();
                window.Show();
            }
        }
    }
}
