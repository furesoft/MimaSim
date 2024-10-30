using System;
using System.Diagnostics;
using MimaSim.Core;
using MimaSim.MIMA.Components;
using MimaSim.MIMA.Components.Network;
using NATS.Net;

namespace MimaSim.Desktop.Core;

public class NetworkService : INetworkService
{
    private NatsClient _client;
    private string _subject;

    public async void Init()
    {
        _client = new NatsClient("demo.nats.io");

        _subject = $"mimasim.networks.{CPU.Instance.NIC.NetworkAddress}.>";
        /*await foreach (var msg in _client.SubscribeAsync<byte[]>(subject: _subject))
        {
            Debug.WriteLine($"Received: {msg.Subject}: {Frame.Unpack(msg.Data!)}");
        }*/
    }

    public async void Send(Frame frame)
    {
        await _client.PublishAsync(subject: _subject, data: frame.Pack());
    }

    public void Receive(Action<Frame> action)
    {
        throw new NotImplementedException();
    }
}