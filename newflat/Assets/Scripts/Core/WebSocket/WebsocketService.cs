using Br.Core.Server;
using Core.Server.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using DG.Tweening;

public class WebsocjetService : MonoSingleton<WebsocjetService> {

    private string url = "ws://echo.websocket.org";
    private WebSocket ws = null;
    private CommandsUtils commandsUtils;

    private void Start()
    {
        commandsUtils = new CommandsUtils();
        List<ICommand> loaderList = CommandLoader.Load();
        commandsUtils.RegCommands(loaderList);
    }
    public void ConnetWebsokcet()
    {
        Debug.Log("start connnet websocket server");
        StopAllCoroutines();
        if(ws!=null)
        {
            ws.Close();
        }
        StartCoroutine(StartWebSocket());
    }
    private IEnumerator StartWebSocket()
    {
        string websoketurl = Config.parse("websoketurl");
       // websoketurl = url;
        if (string.IsNullOrEmpty(websoketurl))
        {
            websoketurl = url;
        }
        Debug.Log("connecting ..."+ websoketurl);
        ws = new WebSocket(new Uri(websoketurl));
        yield return StartCoroutine(ws.Connect());
        ws.SendString("Hi");
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
              
                ConnetWebsokcet();
              
                break;
            }
            //Debug.Log("123");
            yield return 0;
        }
        ws.Close();
    }

    public void SendData(string sendData)
    {
        if (ws != null)
        {
           
            ws.SendString(sendData);
        }

    }

    private void OnMessage(string data)
    {
      //  Debug.Log(data);
        if (data.Equals("|")  || string.IsNullOrEmpty(data.Trim()))
        {
            return;
        }
       // Debug.Log("11111111111");
        MessageContent messageContent = CollectionsConvert.ToObject<MessageContent>(data);
        //Debug.Log(messageContent);
        if(messageContent!=null)
        {
           // Debug.Log(data);
            commandsUtils.Exec(messageContent);
        }
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetTestData();
        }
       // GetTestData();
    }

    private  int i =0;
    private void GetTestData()
    {
        i++;
        SendData("123" +i);


    }


}

