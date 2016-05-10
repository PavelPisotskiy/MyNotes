using System.Windows;

namespace MyNotes.Helpers
{
    public interface IMessageService
    {
        void ShowMessage(string message);
        void ShowExclamation(string exclamation);
        void ShowError(string error);
        MessageBoxResult ShowQuestion(string question, MessageBoxButton button); 
    }
}
