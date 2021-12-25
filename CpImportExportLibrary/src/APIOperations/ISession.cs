namespace CpImportExportLibrary.src.ApiOperations
{
    public interface ISession
    {
        string Password { get; }
        string Sid { get; }
        string Url { get; }
        string Username { get; }

        dynamic ApiCall(string command, string payload);
        void Login();
        void Logout();
        dynamic Publish();
    }
}