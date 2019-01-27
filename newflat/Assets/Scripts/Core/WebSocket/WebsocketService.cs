using BestHTTP.WebSocket;
using Br.Core.Server;
using Core.Server.Command;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

public class WebSocketService:MonoSingleton<WebSocketService>  {

    // Use this for initialization

    private WebSocket webSocket;

    private string address = "ws://123.207.167.163:9010/ajaxchattest";

    private CommandsUtils commandsUtils;


    private void Init()
    {
        commandsUtils = new CommandsUtils();
        List<ICommand> loaderList = CommandLoader.Load();
        commandsUtils.RegCommands(loaderList);
    }


    public void StartSocket()
    {
        Init();
        StartConnet();
    }


     public void SendData(string data)
    {
        webSocket.Send(data);
    }

    private void StartConnet()
    {
       string websoketurl = Config.parse("websoketurl");
        // address = websoketurl;

        Debug.Log("webSocket url ="+ websoketurl);
        webSocket = new WebSocket(new Uri(websoketurl));
        webSocket.OnOpen += OnOpen;
        webSocket.OnMessage += OnMessageReceived;
        webSocket.OnClosed += OnClosed;
        webSocket.OnError += OnError;
        webSocket.Open();
    }

    void OnOpen(WebSocket ws)
    {
        Debug.Log("WebSocket OnOpen");
        SendData("hi Server");
    }

    /// <summary>
    /// Called when we received a text message from the server
    /// </summary>
    void OnMessageReceived(WebSocket ws, string message)
    {
        Debug.Log("接收："+message);
        if (message.Equals("|") || string.IsNullOrEmpty(message.Trim()))
        {
            return;
        }
        // Debug.Log("11111111111");
        MessageContent messageContent = CollectionsConvert.ToObject<MessageContent>(message);
        //Debug.Log(messageContent);
        if (messageContent != null)
        {
            // Debug.Log(data);
            commandsUtils.Exec(messageContent);
        }
    }

    /// <summary>
    /// Called when the web socket closed
    /// </summary>
    void OnClosed(WebSocket ws, UInt16 code, string message)
    {
        // Text += string.Format("-WebSocket closed! Code: {0} Message: {1}\n", code, message);

        Debug.Log(message);
        webSocket = null;
    }

    /// <summary>
    /// Called when an error occured on client side
    /// </summary>
    void OnError(WebSocket ws, Exception ex)
    {
        string errorMsg = string.Empty;
#if !UNITY_WEBGL || UNITY_EDITOR
        if (ws.InternalRequest.Response != null)
            errorMsg = string.Format("Status Code from Server: {0} and Message: {1}", ws.InternalRequest.Response.StatusCode, ws.InternalRequest.Response.Message);
#endif

        Debug.Log(ex.Message);

        webSocket = null;
        StartConnet();
    }
}
