using Br.Core.Server;
using Core.Server.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WebsocjetService : MonoSingleton<WebsocjetService> {

    private string url = "ws://echo.websocket.org";
    private  WebSocket ws = null;
    private CommandsUtils commandsUtils;

    private void Start()
    {
        commandsUtils = new CommandsUtils();
        List<ICommand> loaderList = CommandLoader.Load();
        commandsUtils.RegCommands(loaderList);
    }

    public IEnumerator StartWebSocket()
    {
        string websoketurl = Config.parse("websoketurl");
        if(string.IsNullOrEmpty(websoketurl))
        {
            websoketurl = url;
        }
        Debug.Log(websoketurl);
        ws = new WebSocket(new Uri(websoketurl));
        yield return StartCoroutine(ws.Connect());
       // w.SendString("Hi there");
        int i = 0;
        while (true)
        {
           
            string reply = ws.RecvString();
            if (reply != null)
            {
                Debug.Log("Received: " + reply);
                OnMessage(reply);
              //  w.SendString( "Hi there" + i++);
            }
            if (ws.error != null)
            {
                Debug.LogError("Error: " + ws.error);
                break;
            }
            //Debug.Log("123");
            yield return 0;
        }
        ws.Close();
    }

    private void SendData(string sendData)
    {
        if(ws!=null)
        {
            Debug.Log(sendData);
            ws.SendString(sendData);
        }
        
    }

    private void OnMessage(string data)
    {
        MessageContent messageContent = CollectionsConvert.ToObject<MessageContent>(data);
        commandsUtils.Exec(messageContent);
    }

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.A))
    //    {
    //        SendData(GetTestData());
    //    }
    //}

    //private string GetTestData()
    //{
    //    MessageContent mc = new MessageContent();
    //    Dictionary<string, object> data = new Dictionary<string, object>();
    //    data.Add("id","123456");
    //    data.Add("state", "alarm");
    //    mc.data = data;
    //    mc.methodName = "alarm";
    //   return  CollectionsConvert.ToJSON(mc);


    //}

    
}

