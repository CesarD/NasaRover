using Moq;
using NasaRover.Common;
using NasaRover.Common.Contracts;
using NasaRover.Movements.Contracts;
using System;
using FluentAssertions;
using Xunit;

namespace NasaRover.Tests
{
	public class RoverTests
	{
		[Fact]
		public void InitializeRoverWithValidValuesSucceedsTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
			var movementFactoryMock = new Mock<IMovementFactory>();

			var rover = new Rover(mapGridMock.Object, 5, 10, OrientationEnum.N, movementFactoryMock.Object);

			rover.X.Should().Be(5);
			rover.Y.Should().Be(10);
			rover.Orientation.Should().Be(OrientationEnum.N);
		}

		[Fact]
		public void InitializeRoverWithInvalidCoordsThrowsExceptionTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
			var movementFactoryMock = new Mock<IMovementFactory>();

			Action act = () => new Rover(mapGridMock.Object, 5, 10, OrientationEnum.N, movementFactoryMock.Object);

			act.Should().Throw<Exception>();
		}

		[Fact]
		public void ExecuteValidInstructionUpdatesPositionTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
			mapGridMock.Setup(x => x.IsScented(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
			var movementMock = new Mock<IMovement>();
			movementMock.Setup(x => x.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>())).Returns((1, 1, OrientationEnum.N));
			var movementFactoryMock = new Mock<IMovementFactory>();
			movementFactoryMock.Setup(x => x.GetMovement(It.IsAny<char>())).Returns(movementMock.Object);
			var rover = new Rover(mapGridMock.Object, 5, 5, OrientationEnum.S, movementFactoryMock.Object);

			rover.ExecuteInstruction(It.IsAny<char>());

			rover.X.Should().Be(1);
			rover.Y.Should().Be(1);
			rover.Orientation.Should().Be(OrientationEnum.N);
		}

		[Fact]
		public void ExecuteInvalidInstructionDoesNothingTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
			var movementFactoryMock = new Mock<IMovementFactory>();
			movementFactoryMock.Setup(x => x.GetMovement(It.IsAny<char>())).Returns((IMovement)null);
			var rover = new Rover(mapGridMock.Object, 5, 5, OrientationEnum.S, movementFactoryMock.Object);

			rover.ExecuteInstruction(It.IsAny<char>());

			rover.X.Should().Be(5);
			rover.Y.Should().Be(5);
			rover.Orientation.Should().Be(OrientationEnum.S);
		}

		[Fact]
		public void ExecuteInstructionThatLosesRoverMarksLostTrueTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(5, 5)).Returns(true);
			mapGridMock.Setup(x => x.IsValid(7, 13)).Returns(false);
			mapGridMock.Setup(x => x.IsScented(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
			var movementMock = new Mock<IMovement>();
			movementMock.Setup(x => x.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>())).Returns((7, 13, OrientationEnum.N));
			var movementFactoryMock = new Mock<IMovementFactory>();
			movementFactoryMock.Setup(x => x.GetMovement(It.IsAny<char>())).Returns(movementMock.Object);
			var rover = new Rover(mapGridMock.Object, 5, 5, OrientationEnum.N, movementFactoryMock.Object);

			rover.ExecuteInstruction(It.IsAny<char>());

			rover.Lost.Should().BeTrue();
			mapGridMock.Verify(x => x.MarkScent(5, 5), Times.Once);
			rover.X.Should().Be(5);
			rover.Y.Should().Be(5);
			rover.Orientation.Should().Be(OrientationEnum.N);
		}

		[Fact]
		public void ExecuteInstructionThatLosesRoverFromScentedPointDoesNothingTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(5, 5)).Returns(true);
			mapGridMock.Setup(x => x.IsValid(7, 13)).Returns(false);
			mapGridMock.Setup(x => x.IsScented(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
			var movementMock = new Mock<IMovement>();
			movementMock.Setup(x => x.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>())).Returns((7, 13, OrientationEnum.N));
			var movementFactoryMock = new Mock<IMovementFactory>();
			movementFactoryMock.Setup(x => x.GetMovement(It.IsAny<char>())).Returns(movementMock.Object);
			var rover = new Rover(mapGridMock.Object, 5, 5, OrientationEnum.N, movementFactoryMock.Object);

			rover.ExecuteInstruction(It.IsAny<char>());

			rover.Lost.Should().BeFalse();
			mapGridMock.Verify(x => x.MarkScent(5, 5), Times.Never);
			rover.X.Should().Be(5);
			rover.Y.Should().Be(5);
			rover.Orientation.Should().Be(OrientationEnum.N);
		}

		[Fact]
		public void ExecuteInstructionOnLostRoverDoesNothingTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(5, 5)).Returns(true);
			mapGridMock.Setup(x => x.IsValid(7, 13)).Returns(false);
			mapGridMock.Setup(x => x.IsScented(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
			var movementMock = new Mock<IMovement>();
			movementMock.Setup(x => x.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>())).Returns((7, 13, OrientationEnum.N));
			var movementFactoryMock = new Mock<IMovementFactory>();
			movementFactoryMock.Setup(x => x.GetMovement(It.IsAny<char>())).Returns(movementMock.Object);
			var rover = new Rover(mapGridMock.Object, 5, 5, OrientationEnum.N, movementFactoryMock.Object);

			rover.ExecuteInstruction(It.IsAny<char>());

			rover.Lost.Should().BeTrue();

			movementMock.Verify(x => x.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>()), Times.Once);

			rover.ExecuteInstruction(It.IsAny<char>());

			movementMock.Verify(x => x.Move(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>()), Times.Once);
			
		}
	}
}