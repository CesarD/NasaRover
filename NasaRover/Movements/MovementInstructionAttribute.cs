using System;

namespace NasaRover.Movements
{
	public class MovementInstructionAttribute : Attribute
	{
		public MovementInstructionAttribute(char instruction)
		{
			Instruction = instruction;
		}
		
		public char Instruction { get; private set; }
	}
}