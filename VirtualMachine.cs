using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public VirtualMachine(string program, int memorySize)
		{
			Memory = new byte[memorySize];
			Instructions = program;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			Commands.Add(symbol, execute);
		}

		public Dictionary<char, Action<IVirtualMachine>> Commands = new Dictionary<char, Action<IVirtualMachine>>();
		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }
		public void Run()
		{
			while(InstructionPointer < Instructions.Length)
            {
				var command = Instructions[InstructionPointer];
				if (Commands.Keys.Contains(command))
					Commands[command](this);
				InstructionPointer++;
			}
		}
	}
}