using System.Collections.Generic;
namespace Core.Server.Command
{
    /// <summary>
    /// 命令
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// 命令名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 执行服务器的请求
        /// </summary>
        /// <param name="data">业务数据</param>
        /// <param name="request">服务器的请求信息</param>
        /// <returns>直接返回业务数据 如果需要返回错误，直接在命令中使用抛出CommandExecException异常</returns>
        object ExecuteCommand(Dictionary<string,object> data);
    }
}