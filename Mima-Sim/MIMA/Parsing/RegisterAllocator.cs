using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MimaSim.MIMA.Parsing
{
    public class RegisterAllocator
    {
        private bool IsXUsed = false;

        public Registers Allocate()
        {
            if (IsXUsed)
            {
                IsXUsed = false;
                return Registers.Y;
            }
            else
            {
                IsXUsed = true;
                return Registers.X;
            }
        }
    }
}