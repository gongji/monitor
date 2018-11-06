using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask : ITaskProcess
{
    /// <summary>
    /// 执行任务
    /// </summary>
    /// <returns></returns>
    IEnumerator Excute();
}
