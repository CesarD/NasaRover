namespace NasaRover.Common.Contracts
{
	public interface IRoversManager
	{
		IRover ActiveRover { get; }
		void AddRover(int x, int y, OrientationEnum orientation);
		void DisposeRover();
	}
}