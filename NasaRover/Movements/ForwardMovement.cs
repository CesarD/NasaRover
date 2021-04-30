using NasaRover.Common;
using NasaRover.Movements.Contracts;

namespace NasaRover.Movements
{
	[MovementInstruction('F')]
	public class ForwardMovement : IMovement
	{
		public (int X, int Y, OrientationEnum Orientation) Move(int currentX, int currentY, OrientationEnum orientation)
		{
			return orientation switch
				   {
					   OrientationEnum.N => (currentX, currentY + 1, orientation),
					   OrientationEnum.S => (currentX, currentY - 1, orientation),
					   OrientationEnum.W => (currentX - 1, currentY, orientation),
					   OrientationEnum.E => (currentX + 1, currentY, orientation),
					   _ => (currentX, currentY, orientation)
				   };
		}
	}
}