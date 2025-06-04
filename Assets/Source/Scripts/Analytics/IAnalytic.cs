namespace Source.Scripts.Analytics
{
    public interface IAnalytic
    {
        void Login();
        void CompleteLevel();
        void LoseLevel();
        void LogEvent(string str);
    }
}