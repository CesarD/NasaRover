using NasaRover.Common;
using NasaRover.Movements.Contracts;

namespace NasaRover.Movements
{
	[MovementInstruction('L')]
	public class RotateLeftMovement : IMovement
	{
		public (int X, int Y, OrientationEnum Orientation) Move(int currentX, int currentY, OrientationEnum currentOrientation)
		{
			return currentOrientation switch
				   {
					   OrientationEnum.N => (currentX, currentY, OrientationEnum.W),
					   OrientationEnum.S => (currentX, currentY, OrientationEnum.E),
					   OrientationEnum.W => (currentX, currentY, OrientationEnum.S),
					   OrientationEnum.E => (currentX, currentY, OrientationEnum.N),
					   _ => (currentX, currentY, currentOrientation)
				   };
		}
	}
}