using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 任务进程信息
/// </summary>
public interface ITaskProcess
{
    /// <summary>
    /// 任务开始
    /// </summary>
    Action OnStart { get; set; }
    /// <summary>
    /// （成功）任务结束
    /// </summary>
    Action OnFinish { get; set; }
    /// <summary>
    /// 任务失败
    /// </summary>
    Action<string> OnError { get; set; }
    /// <summary>
    /// 数据
    /// </summary>
    object Data { get; }
    /// <summary>
    /// 任务进度
    /// </summary>
    float Progress { get; }
    /// <summary>
    /// 任务id
    /// </summary>
    string ID { get; }
}
