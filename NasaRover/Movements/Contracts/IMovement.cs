using NasaRover.Common;

namespace NasaRover.Movements.Contracts
{
	public interface IMovement
	{
		(int X, int Y, OrientationEnum Orientation) Move(int currentX, int currentY, OrientationEnum currentOrientation);
	}
}