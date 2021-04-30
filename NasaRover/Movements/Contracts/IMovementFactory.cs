namespace NasaRover.Movements.Contracts
{
	public interface IMovementFactory
	{
		IMovement GetMovement(char instruction);
	}
}