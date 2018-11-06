using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface  ITaskQueue : ITaskProcess
{
    void Add(ITask task);
    void Remove(ITask task);
    void Clear();
    void StartTask();
    void StopTask();

    int TaskCount { get; }
    int CurrentTaskCount { get; }
    int CurrentTaskIndex { get; }
}
