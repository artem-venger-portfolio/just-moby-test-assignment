namespace BlockTower
{
    public interface IProjectLogger
    {
        public void LogInfo(string message);
        public void LogWarning(string message);
        public void LogError(string message);
    }
}