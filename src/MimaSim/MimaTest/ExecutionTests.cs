using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.MIMA;
using MimaSim.MIMA.Components;

namespace MimaTest;

[TestClass]
public class ExecutionTests
{
    [TestMethod]
    public void TestRaw()
    {
        var program = new byte[] { 0x04, 0x2A, 00, 0x40, 0x01, 0x02, 0x04, 0x02, 00, 0x40, 0x01, 0x03, 0x18 };

        CPU.Instance.Program = program;

        while (CPU.Instance.GetRegister(Registers.IAR) < program.Length)
        {
            CPU.Instance.Step();
        }
    }
}