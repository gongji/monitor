using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Platform_Comm.Utils
{
    /// <summary>
    /// guid的创建类
    /// </summary>
  public static  class GuidTools
    {
      public static string CreateGuid()
      {
          return System.Guid.NewGuid().ToString("N");
      }
    }
}
