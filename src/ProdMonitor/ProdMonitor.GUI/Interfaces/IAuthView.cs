
namespace ProdMonitor.GUI.Interfaces
{
    public interface IAuthView
    {
        string Username { get; }
        string Password { get; }

        event EventHandler LoginClicked;

        void Show();

        void ShowMessage(string message);
    }
}
