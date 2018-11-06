
using Core.Common.Logging;
using System.Collections.Generic;

namespace Core.Server.Command
{

    public class UndefinedCommand : ICommand
    {
        private static ILog log = LogManagers.GetLogger(typeof(UndefinedCommand).FullName);
        public string Name { get { return "undefined"; } }

        public object ExecuteCommand(Dictionary<string,object> data)
        {
           
            throw new CommandExecException("1", "未找到命令:" + Name);
        }
    }
}
