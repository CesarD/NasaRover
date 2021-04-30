using NasaRover.Movements;
using System;
using FluentAssertions;
using Xunit;

namespace NasaRover.Tests
{
	public class MovementFactoryTests
	{
		[Theory]
		[InlineData('F', typeof(ForwardMovement))]
		[InlineData('R', typeof(RotateRightMovement))]
		[InlineData('L', typeof(RotateLeftMovement))]
		public void GetValidMovementsTest(char instruction, Type movementType)
		{
			var factory = new MovementFactory();

			var movement = factory.GetMovement(instruction);

			movement.Should().BeOfType(movementType);
		}

		[Theory]
		[InlineData('A')]
		public void GetNullFromInvalidMovementTest(char instruction)
		{
			var factory = new MovementFactory();

			var movement = factory.GetMovement(instruction);

			movement.Should().BeNull();
		}
	}
}