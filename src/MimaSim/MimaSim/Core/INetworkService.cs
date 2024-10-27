using System;
using MimaSim.MIMA.Components.Network;

namespace MimaSim.Core;

public interface INetworkService
{
    void Init();

    void Send(Frame frame);
    void Receive(Action<Frame> action);
}
