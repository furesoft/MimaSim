using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.MIMA.Components;
using System.Diagnostics;

namespace MimaTest;

[TestClass]
public class BusTests
{
    [TestMethod]
    public void Bus_Connect_Should_Pass()
    {
        var cpuBus = new Bus();
        var registerBus = new Bus();

        cpuBus.Connect(registerBus);

        cpuBus.Subscribe(debug);
        registerBus.Subscribe(debug);

        cpuBus.Send("hello from cpu");

        registerBus.Send("hello from register");
    }

    private void debug(object value)
    {
        Debug.WriteLine(value);
    }
}