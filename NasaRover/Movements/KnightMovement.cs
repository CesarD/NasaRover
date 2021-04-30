using NasaRover.Common;
using NasaRover.Movements.Contracts;

namespace NasaRover.Movements
{
	[MovementInstruction('K')]
	public class KnightMovement : IMovement
	{
		public (int X, int Y, OrientationEnum Orientation) Move(int currentX, int currentY, OrientationEnum currentOrientation)
		{
			return currentOrientation switch
				   {
					   OrientationEnum.N => (currentX + 1, currentY + 2, currentOrientation),
					   OrientationEnum.S => (currentX - 1, currentY - 2, currentOrientation),
					   OrientationEnum.W => (currentX - 2, currentY + 1, currentOrientation),
					   OrientationEnum.E => (currentX + 2, currentY - 1, currentOrientation),
					   _ => (currentX, currentY, currentOrientation)
				   };
		}
	}
}