namespace Core.Common.Logging
{
    /// <summary>
    /// Console log factory
    /// </summary>
    public class DebugLogFactory : ILogFactory
    {
        /// <summary>
        /// Gets the log by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public ILog GetLog(string name)
        {
            return new DebugLog(name);
        }
    }
}
