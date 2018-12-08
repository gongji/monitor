using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskQueue : ITaskQueue,IDisposable
{
    //private Queue _queue;
    private List<ITask> _taskList;
    private MonoBehaviour _mono;
    private int _curTaskIndex = 0;
    private float _curProgress = 0;
    private float _tempProgress = 0;
    private float _prevProgress = 0;
    //和当前任务索引一起判断进度的计算
    private int _progressIndex = 0;
    private float _progress = 0;
    private int _taskTotalCount = 0;
    private object _data;

    public int CurrentTaskCount
    {
        get
        {
            return _taskTotalCount - _curTaskIndex;
        }
    }

    public int CurrentTaskIndex
    {
        get
        {
            return _curTaskIndex;
        }
    }

    public object Data
    {
        get
        {
            return _data;
        }
    }

    public string ID
    {
        get
        {
            return "task queue";
        }
    }

    public Action<string> OnError { get; set; }

    public Action OnFinish { get; set; }

    public Action OnStart { get; set; }

    public float Progress
    {
        get
        {
           
            //仅用任务完成的数量来作为进度
            return _taskTotalCount > 0 ? (Mathf.Clamp(_curTaskIndex, 0, _taskTotalCount) / (_taskTotalCount * 1.0f)) : 0;
        }
    }

    public int TaskCount
    {
        get
        {
            if (_taskList != null) _taskTotalCount = _taskList.Count;
            return _taskTotalCount;
        }
    }


    public void Add(ITask task)
    {
        if (_taskList != null)
        {
            _taskList.Add(task);
        }
    }

    public void Clear()
    {
        //MonoObject.Instance.onUpdateEvent -= Test;
        if (_taskList != null)
        {
            _taskList.Clear();
            GC.Collect();
        }
    }

    public void Remove(ITask task)
    {
        if (_taskList != null)
        {
            _taskList.Remove(task);
        }
    }

    public void StartTask()
    {
        if (OnStart != null) OnStart();
        _taskTotalCount = _taskList.Count;
        //Debug.Log("Task COunt " + _taskTotalCount);
        _mono.StartCoroutine(StartTaskQueue());

    }

    public TaskQueue(MonoBehaviour mono)
    {
        this._mono = mono;
        this._taskList = new List<ITask>();
    }

    public void SetData(object data)
    {
        _data = data;
    }

    private IEnumerator StartTaskQueue()
    {
        //MonoObject.Instance.onUpdateEvent += Test;
        yield return 0;
        if (_curTaskIndex < _taskTotalCount)
        {
            ITask task = _taskList[_curTaskIndex];
            yield return task.Excute();
            //执行完一个任务，开始下一个任务
           // Debug.Log("执行完一个任务，开始下一个任务" + _curTaskIndex);
            _curTaskIndex += 1;
            //递归

            PublicMonoSingle.Instance.StartCoroutine(StartTaskQueue());
        }
        else
        {
            if (OnFinish != null) OnFinish();
        }
    }


    public void StopTask()
    {
        PublicMonoSingle.Instance.StopAllCoroutines();
    }

    #region IDisposable Support
    private bool disposedValue = false; // 要检测冗余调用

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: 释放托管状态(托管对象)。
                this._mono = null;
                this._taskList = null;
               // Debug.Log(GetType() + "释放托管对象");
            }

            // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
            // TODO: 将大型字段设置为 null。

            disposedValue = true;
        }
    }

    // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
    ~TaskQueue()
    {
        // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        Dispose(true);
    }

    // 添加此代码以正确实现可处置模式。
    public void Dispose()
    {
        // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        Dispose(true);
        // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
         GC.SuppressFinalize(this);
    }

    #endregion
}
