namespace LoggerMikroservis.Manager
{
    public interface ILoggerManager
    {
        void LogDebug(string message);

        void LogError(Exception ex, string message);

        void LogInfo(string message);

        void LogWarn(string message);
    }
}
