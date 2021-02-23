using Microsoft.VisualStudio.TestTools.UnitTesting;
using MimaSim.MIMA;

namespace MimaTest
{
    [TestClass]
    public class EmitterTests
    {
        [TestMethod]
        public void EmitInstruction_Should_Pass()
        {
            var emitter = new ByteCodeEmitter();

            emitter.EmitInstruction(OpCodes.LOAD, 42);
            emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, Registers.X);

            emitter.EmitInstruction(OpCodes.LOAD, 8);
            emitter.EmitInstruction(OpCodes.MOV_REG_REG, Registers.Accumulator, Registers.Y);

            emitter.EmitInstruction(OpCodes.ADD);

            var raw = emitter.ToArray();

            var toTest = new byte[] {
                0x04, 0x2A, 0x00,
                0x40, 0x01, 0x02,
                0x04, 0x08, 0x00,
                0x40, 0x01, 0x03,
                0x08,
            };

            Assert.AreEqual(string.Join(' ', raw), string.Join(' ', toTest));
        }
    }
}