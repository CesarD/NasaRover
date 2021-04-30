using NasaRover.Common;
using NasaRover.Movements.Contracts;

namespace NasaRover.Movements
{
	[MovementInstruction('R')]
	public class RotateRightMovement : IMovement
	{
		public (int X, int Y, OrientationEnum Orientation) Move(int currentX, int currentY, OrientationEnum currentOrientation)
		{
			return currentOrientation switch
				   {
					   OrientationEnum.N => (currentX, currentY, OrientationEnum.E),
					   OrientationEnum.S => (currentX, currentY, OrientationEnum.W),
					   OrientationEnum.W => (currentX, currentY, OrientationEnum.N),
					   OrientationEnum.E => (currentX, currentY, OrientationEnum.S),
					   _ => (currentX, currentY, currentOrientation)
				   };
		}
	}
}