using NasaRover.Common.Contracts;
using NasaRover.Movements.Contracts;
using System;

namespace NasaRover.Common
{
	public class Rover : IRover
	{
		private readonly IMapGrid _mapGrid;
		private readonly IMovementFactory _movementFactory;

		public Rover(IMapGrid mapGrid, 
					 int startingX, 
					 int startingY, 
					 OrientationEnum startingOrientation,
					 IMovementFactory movementFactory)
		{
			if (!mapGrid.IsValid(startingX, startingY))
				throw new Exception("The starting coordinates are not valid");

			_mapGrid = mapGrid;
			_movementFactory = movementFactory;
			X = startingX;
			Y = startingY;
			Orientation = startingOrientation;
		}

		public int X { get; private set; }
		public int Y { get; private set; }
		public OrientationEnum Orientation { get; private set; }
		public bool Lost { get; private set; }

		public void ExecuteInstruction(char instruction)
		{
			var movement = _movementFactory.GetMovement(instruction);
			if (movement == null || Lost)	//If movement is invalid or the rover is Lost, then ignore everything else and get out
				return;

			var newPosition = movement.Move(X, Y, Orientation);

			//If the next position where it's going is off the map, then it must decide if it ignores it or not
			if (!_mapGrid.IsValid(newPosition.X, newPosition.Y))
			{
				if (_mapGrid.IsScented(X, Y)) //If current position is a scented one, then ignore and get out
					return;
				
				//Otherwise, mark the current position as a scented one, flag the rover as Lost and get out
				_mapGrid.MarkScent(X, Y);
				Lost = true;
				return;
			}

			//Finally, update the current position with the new one
			X = newPosition.X;
			Y = newPosition.Y;
			Orientation = newPosition.Orientation;
		}
		
	}
}