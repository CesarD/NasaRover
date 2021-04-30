using FluentAssertions;
using Moq;
using NasaRover.Common;
using NasaRover.Common.Contracts;
using NasaRover.Movements.Contracts;
using Xunit;

namespace NasaRover.Tests
{
	public class RoversManagerTests
	{
		[Fact]
		public void AddRoverSetsActiveRoverTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
			var movementFactoryMock = new Mock<IMovementFactory>();
			var roversMgr = new RoversManager(mapGridMock.Object, movementFactoryMock.Object);
			
			roversMgr.AddRover(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>());

			roversMgr.ActiveRover.Should().NotBeNull();
		}

		[Fact]
		public void DisposeRoverSetsActiveRoverNullTest()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.IsValid(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
			var movementFactoryMock = new Mock<IMovementFactory>();
			var roversMgr = new RoversManager(mapGridMock.Object, movementFactoryMock.Object);

			roversMgr.AddRover(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<OrientationEnum>());

			roversMgr.ActiveRover.Should().NotBeNull();

			roversMgr.DisposeRover();

			roversMgr.ActiveRover.Should().BeNull();
		}
	}
}