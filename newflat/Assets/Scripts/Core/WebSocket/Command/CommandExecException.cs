using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Server.Command
{
    public class CommandExecException : Exception
    {
        public string ErrorCode { get; private set; }
        public object BillData { get; private set; }

        public CommandExecException(string errorCode, string message, object billData = null):base(message) {
            ErrorCode = errorCode;
            BillData = billData;
        }

        public CommandExecException(string message) : this("1", message, null)
        {
            
        }
    }
}
