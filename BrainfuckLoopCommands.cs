using System.Collections.Generic;

namespace func.brainfuck
{
	public class BrainfuckLoopCommands
	{
		public static void RegisterTo(IVirtualMachine vm)
		{
			var loopStack = new Stack<int>();
			var loopTable = new Dictionary<int, int>();


			for (int i = 0; i < vm.Instructions.Length; i++)
			{
				var instruction = vm.Instructions[i];

				if (instruction.Equals('['))
					loopStack.Push(i);
				else if (instruction.Equals(']'))
				{
					var openBracket = loopStack.Pop();
					loopTable[openBracket] = i;
					loopTable[i] = openBracket;
				}
			}

			vm.RegisterCommand('[', b =>
			{
				if (b.Memory[b.MemoryPointer] == 0)
					b.InstructionPointer = loopTable[b.InstructionPointer];
			});

			vm.RegisterCommand(']', b => 
			{
				if (b.Memory[b.MemoryPointer] != 0)
					b.InstructionPointer = loopTable[b.InstructionPointer];
			});
		}
	}
}