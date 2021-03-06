using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			for (char ch = 'A'; ch < 'Z' + 1; ch++)
			{
				var symbol = ch;
				vm.RegisterCommand(symbol, b => { b.Memory[b.MemoryPointer] = (Byte)symbol; });
				vm.RegisterCommand(Char.ToLower(symbol), b => { b.Memory[b.MemoryPointer] = (Byte)Char.ToLower(symbol); });
			}
			for (char i = '0'; i < '9' + 1; i++)
			{
				var value = i;
				vm.RegisterCommand(value, b => { b.Memory[b.MemoryPointer] = (Byte)value; });
			}
			vm.RegisterCommand('.', b => { write(Convert.ToChar(b.Memory[b.MemoryPointer])); });
			vm.RegisterCommand('+', b =>
			{
				if (b.Memory[b.MemoryPointer] == 255)
					b.Memory[b.MemoryPointer] = 0;
				else
					b.Memory[b.MemoryPointer]++; 
			});
			vm.RegisterCommand('-', b => 
			{
				if (b.Memory[b.MemoryPointer] == 0)
					b.Memory[b.MemoryPointer] = 255;
				else
					b.Memory[b.MemoryPointer]--; });
			vm.RegisterCommand(',', b => 
			{
				int readedValue = read();
				if (readedValue != -1)
					b.Memory[b.MemoryPointer] = (byte)readedValue;
			});
			vm.RegisterCommand('>', b => 
			{
				if (b.MemoryPointer + 1 == b.Memory.Length)
					b.MemoryPointer = 0;
				else
					b.MemoryPointer++;
			});
			vm.RegisterCommand('<', b => 
			{
				if (b.MemoryPointer - 1 == -1)
					b.MemoryPointer = b.Memory.Length - 1;
				else
					b.MemoryPointer--;
			});
		}
	}
}