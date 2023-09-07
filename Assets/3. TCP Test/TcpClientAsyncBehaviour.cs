using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(TcpListenClientBehaviour))]
public class TcpClientAsyncBehaviour : MonoBehaviour
{
    TcpClient client;

    [Tooltip("The server's IP address")]
    public string ipAdress = "127.0.0.1";
    
    [Tooltip("The port the service is running on")]
    public int port = 9021;

    IEnumerator Start()
    {
        var listener = GetComponent<TcpListenClientBehaviour>();

        while (!listener.isReady)
            yield return null;

        client = new TcpClient();
        client.Connect(ipAdress, port);

        var msg = Encoding.ASCII.GetBytes("Hello from client");

        client.GetStream().BeginWrite(msg, 0, msg.Length, Sent_Complete, client);
    }

    private void Sent_Complete(IAsyncResult ar)
    {
        if (ar.IsCompleted)
        {
            var client = ar.AsyncState as TcpClient;
            client.GetStream().EndWrite(ar);
        }
    }
}
