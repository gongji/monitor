
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IEventListener
{
    bool HandleEvent(string eventName, IDictionary<string, object> dictionary);
}

public class EventMgr : Singleton<EventMgr>
{
    public bool LimitQueueProcesing = false;
    public float QueueProcessTime = 0.0f;

    private Hashtable m_listenerTable = new Hashtable();
    private Queue m_eventQueue = new Queue();

    private struct EventInfo
    {
        public string name;
        public IDictionary<string, object> dictionary;
        //public object data2;
        //public object data3;
        public EventInfo(string p1, IDictionary<string, object> dictionary)
        {
            name = p1;
            //data1 = p2;
            //data2 = p3;
            //data3 = p4;
            this.dictionary = dictionary;
        }
    }

    //Add a listener to the event manager that will receive any events of the supplied event name.
    public bool AddListener(IEventListener listener, string eventName)
    {
        if (listener == null || eventName == null)
        {
            Debug.Log("Event Manager: AddListener failed due to no listener or event name specified.");
            return false;
        }

        if (!m_listenerTable.ContainsKey(eventName))
            m_listenerTable.Add(eventName, new ArrayList());

        ArrayList listenerList = m_listenerTable[eventName] as ArrayList;
        if (listenerList.Contains(listener))
        {
            Debug.Log("Event Manager: Listener: " + listener.GetType().ToString() + " is already in list for event: " + eventName);
            return false; //listener already in list
        }
       // Debug.Log("Event Manager: Listener: " + listener.GetType().ToString() + " is add ok, event: " + eventName);
        listenerList.Add(listener);
        return true;
    }

    //Remove a listener from the subscribed to event.
    public bool RemoveListener(IEventListener listener, string eventName)
    {
        if (!m_listenerTable.ContainsKey(eventName))
            return false;

        ArrayList listenerList = m_listenerTable[eventName] as ArrayList;
        if (!listenerList.Contains(listener))
            return false;

        listenerList.Remove(listener);
        return true;
    }

    private static object locker = new object();
    //Trigger the event instantly, this should only be used in specific circumstances,
    //the QueueEvent function is usually fast enough for the vast majority of uses.
    public bool SendEvent(string eventName, IDictionary<string, object> dictionary)
    {
        lock (locker)
        {

            SendEventCornite(eventName, dictionary);
        }
//        if (!m_listenerTable.ContainsKey(eventName))
//        {
//            Debug.Log("Event Manager: Event \"" + eventName + "\" triggered has no listeners!");
//            return false; //No listeners for event so ignore it
//        }
//
//        ArrayList listenerList = m_listenerTable[eventName] as ArrayList;
//        ArrayList cloneList = listenerList.Clone() as ArrayList;
//        foreach (IEventListener listener in cloneList)
//        {
//            if (listener.HandleEvent(eventName, dictionary))
//                return true; //Event consumed.
//        }
        return true;
    }

    private void SendEventCornite(string eventName, IDictionary<string, object> dictionary)
    {
       
        foreach (string temp in m_listenerTable.Keys)
        {
            if (temp.StartsWith(eventName))
            {
                ArrayList listenerList = m_listenerTable[temp] as ArrayList;
                ArrayList cloneList = listenerList.Clone() as ArrayList;
                foreach (IEventListener listener in cloneList)
                {
                    listener.HandleEvent(temp, dictionary);
                   // yield return 1f;
                }
            }
        }
    }
    //Inserts the event into the current queue.
    public bool PushEvent(string eventName, IDictionary<string, object> dictionary)
    {
        if (!m_listenerTable.ContainsKey(eventName))
        {
            Debug.Log("EventManager: QueueEvent failed due to no listeners for event: " + eventName);
            return false;
        }

        m_eventQueue.Enqueue(new EventInfo(eventName, dictionary));
        return true;
    }

    //Every update cycle the queue is processed, if the queue processing is limited,
    //a maximum processing time per update can be set after which the events will have
    //to be processed next update loop.
    void Update()
    {
        float timer = 0.0f;
        while (m_eventQueue.Count > 0)
        {
            if (LimitQueueProcesing)
            {
                if (timer > QueueProcessTime)
                    return;
            }

            EventInfo evt = (EventInfo)m_eventQueue.Dequeue();
            if (!SendEvent(evt.name, evt.dictionary))
            if (LimitQueueProcesing)
                timer += Time.deltaTime;
        }
    }

    public void OnApplicationQuit()
    {
        m_listenerTable.Clear();
        m_eventQueue.Clear();
    }

    void Destroy()
    {
        m_listenerTable.Clear();
        m_eventQueue.Clear();
    }
}
