using System.Collections.Generic;
namespace Core.Server.Command
{
    public interface ICommand
    {
     
        string Name { get; }

        
        object ExecuteCommand(string data);
    }
}