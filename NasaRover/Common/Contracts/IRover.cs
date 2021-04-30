namespace NasaRover.Common.Contracts
{
	public interface IRover
	{
		int X { get; }
		int Y { get; }
		OrientationEnum Orientation { get; }
		bool Lost { get; }

		void ExecuteInstruction(char instruction);
	}
}