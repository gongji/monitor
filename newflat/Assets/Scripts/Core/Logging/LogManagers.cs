namespace Core.Common.Logging
{
    public class LogManagers
    {
        public static ILog GetLogger(string name) {
            return GetFactoryLogger().GetLog(name);
        }

        public static ILogFactory GetFactoryLogger()
        {
            return new DebugLogFactory();
        }
    }
}
