using NasaRover.Common.Contracts;
using NasaRover.Movements.Contracts;

namespace NasaRover.Common
{
	public class RoversManager : IRoversManager
	{
		private readonly IMapGrid _mapGrid;
		private readonly IMovementFactory _movementFactory;

		public RoversManager(IMapGrid mapGrid, IMovementFactory movementFactory)
		{
			_mapGrid = mapGrid;
			_movementFactory = movementFactory;
		}
		
		public IRover ActiveRover { get; private set; }

		public void AddRover(int x, int y, OrientationEnum orientation)
		{
			var rover = new Rover(_mapGrid, x, y, orientation, _movementFactory);
			ActiveRover = rover;
		}

		public void DisposeRover()
		{
			ActiveRover = null;
		}
	}
}