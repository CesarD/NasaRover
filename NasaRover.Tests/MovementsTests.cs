using FluentAssertions;
using NasaRover.Common;
using NasaRover.Movements;
using Xunit;

namespace NasaRover.Tests
{
	public class MovementsTests
	{
		[Theory]
		[InlineData(1, 1, OrientationEnum.N, 1, 2, OrientationEnum.N)]
		[InlineData(1, 1, OrientationEnum.S, 1, 0, OrientationEnum.S)]
		[InlineData(1, 1, OrientationEnum.W, 0, 1, OrientationEnum.W)]
		[InlineData(1, 1, OrientationEnum.E, 2, 1, OrientationEnum.E)]
		[InlineData(1, 1, (OrientationEnum)(-1), 1, 1, (OrientationEnum)(-1))]
		public void ForwardMovementTest(int startX, int startY, OrientationEnum startOrientation, int expectedX, int expectedY, OrientationEnum expectedOrientation)
		{
			var movement = new ForwardMovement();

			var (x, y, orientation) = movement.Move(startX, startY, startOrientation);

			x.Should().Be(expectedX);
			y.Should().Be(expectedY);
			orientation.Should().Be(expectedOrientation);
		}
		
		[Theory]
		[InlineData(1, 1, OrientationEnum.N, 1, 1, OrientationEnum.E)]
		[InlineData(1, 1, OrientationEnum.S, 1, 1, OrientationEnum.W)]
		[InlineData(1, 1, OrientationEnum.W, 1, 1, OrientationEnum.N)]
		[InlineData(1, 1, OrientationEnum.E, 1, 1, OrientationEnum.S)]
		[InlineData(1, 1, (OrientationEnum)(-1), 1, 1, (OrientationEnum)(-1))]
		public void RotateRightMovementTest(int startX, int startY, OrientationEnum startOrientation, int expectedX, int expectedY, OrientationEnum expectedOrientation)
		{
			var movement = new RotateRightMovement();

			var (x, y, orientation) = movement.Move(startX, startY, startOrientation);

			x.Should().Be(expectedX);
			y.Should().Be(expectedY);
			orientation.Should().Be(expectedOrientation);
		}

		[Theory]
		[InlineData(1, 1, OrientationEnum.N, 1, 1, OrientationEnum.W)]
		[InlineData(1, 1, OrientationEnum.S, 1, 1, OrientationEnum.E)]
		[InlineData(1, 1, OrientationEnum.W, 1, 1, OrientationEnum.S)]
		[InlineData(1, 1, OrientationEnum.E, 1, 1, OrientationEnum.N)]
		[InlineData(1, 1, (OrientationEnum)(-1), 1, 1, (OrientationEnum)(-1))]
		public void RotateLeftMovementTest(int startX, int startY, OrientationEnum startOrientation, int expectedX, int expectedY, OrientationEnum expectedOrientation)
		{
			var movement = new RotateLeftMovement();

			var (x, y, orientation) = movement.Move(startX, startY, startOrientation);

			x.Should().Be(expectedX);
			y.Should().Be(expectedY);
			orientation.Should().Be(expectedOrientation);
		}
	}
}