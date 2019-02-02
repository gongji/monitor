
using Core.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Server.Command
{
    public class CommandLoader
    {
        private static ILog log = LogManagers.GetLogger(typeof(CommandLoader).FullName);
        public static List<ICommand> Load()
        {

            var outputCommands = new List<ICommand>();
            ICommand command;
            Type type;
            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => 
            t.GetCustomAttributes(typeof(SCommandAttribute), true).Length > 0)).ToArray();
            for (int i = 0, l = types.Length; i < l; i++)
            {
                type = types[i];
                try
                {
                    command = Activator.CreateInstance(type) as ICommand;
                    log.Debug("load command : " +command.Name);
                    outputCommands.Add(command);
                }
                catch (Exception exc)
                {
                    log.Error(new Exception(string.Format("Failed to get commands!"), exc));
                }
            }
            return outputCommands;
        }
    }
}
