using MyNotes.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyNotes.Helpers
{
    class MessageService : IMessageService
    {
        public void ShowError(string error)
        {
            MessageBox.Show(error, LocalizeResource.ErrorCaption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void ShowExclamation(string exclamation)
        {
            MessageBox.Show(exclamation, LocalizeResource.ExclamationCaption, MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, LocalizeResource.MessageCaption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public MessageBoxResult ShowQuestion(string question, MessageBoxButton button)
        {
            return MessageBox.Show(question, LocalizeResource.QuestionCaption, button, MessageBoxImage.Question);
        }
    }
}
