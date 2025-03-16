namespace BlockTower
{
    public interface IProjectLogger
    {
        public void LogInfo(string message);
        void LogInfo(string message, params string[] tags);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}