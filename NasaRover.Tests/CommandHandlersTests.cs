using Moq;
using NasaRover.Commands;
using NasaRover.Common;
using NasaRover.Common.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NasaRover.Tests
{
	public class CommandHandlersTests
	{
		[Fact]
		public async Task MapGridInitializerCommandCallsMapGridInitializer()
		{
			var mapGridMock = new Mock<IMapGrid>();
			var roversMgrMock = new Mock<IRoversManager>();
			var cmdMock = new Mock<MapGridInitializerCommand>();
			cmdMock.Setup(x => x.Width).Returns(10);
			cmdMock.Setup(x => x.Height).Returns(10);
			var handler = new CommandHandlers(mapGridMock.Object, roversMgrMock.Object);

			await handler.Handle(cmdMock.Object, new CancellationToken());
			
			mapGridMock.Verify(x => x.Initialize(10, 10), Times.Once);
		}

		[Fact]
		public async Task NewRoverCommandCallsAddRoverAndGeneratesNewActiveOne()
		{
			var mapGridMock = new Mock<IMapGrid>();
			var roversMgrMock = new Mock<IRoversManager>();
			var cmdMock = new Mock<NewRoverCommand>();
			cmdMock.Setup(x => x.StartingX).Returns(1);
			cmdMock.Setup(x => x.StartingY).Returns(1);
			cmdMock.Setup(x => x.StartingOrientation).Returns(OrientationEnum.N);
			var handler = new CommandHandlers(mapGridMock.Object, roversMgrMock.Object);

			await handler.Handle(cmdMock.Object, new CancellationToken());

			roversMgrMock.Verify(x => x.AddRover(1, 1, OrientationEnum.N), Times.Once);
		}

		[Fact]
		public async Task RoverInstructionsCommandWithNoActiveRoverDoesNothing()
		{
			var mapGridMock = new Mock<IMapGrid>();
			var roversMgrMock = new Mock<IRoversManager>();
			roversMgrMock.Setup(x => x.ActiveRover).Returns((IRover)null);
			var cmdMock = new Mock<RoverInstructionsCommand>();
			cmdMock.Setup(x => x.Instructions).Returns(It.IsAny<List<char>>());
			var handler = new CommandHandlers(mapGridMock.Object, roversMgrMock.Object);
			var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			await handler.Handle(cmdMock.Object, new CancellationToken());

			roversMgrMock.VerifyGet(x => x.ActiveRover);
			consoleOutput.ToString().Should().Be(string.Empty);
		}

		[Fact]
		public async Task RoverInstructionsCommandWithValidRoverSucceeds()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.Width).Returns(10);
			mapGridMock.Setup(x => x.Height).Returns(10);
			var roversMgrMock = new Mock<IRoversManager>();
			var roverMock = new Mock<IRover>();
			roverMock.Setup(x => x.X).Returns(1);
			roverMock.Setup(x => x.Y).Returns(1);
			roverMock.Setup(x => x.Orientation).Returns(OrientationEnum.N);
			roverMock.Setup(x => x.ExecuteInstruction(It.IsAny<char>())).Verifiable();
			roversMgrMock.Setup(x => x.ActiveRover).Returns(roverMock.Object);
			var cmdMock = new Mock<RoverInstructionsCommand>();
			cmdMock.Setup(x => x.Instructions).Returns(new List<char>("XX"));
			var handler = new CommandHandlers(mapGridMock.Object, roversMgrMock.Object);
			var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			await handler.Handle(cmdMock.Object, new CancellationToken());

			roverMock.Verify(x => x.ExecuteInstruction(It.IsAny<char>()), Times.Exactly(2));
			roverMock.Object.Lost.Should().BeFalse();
			consoleOutput.ToString().Should().Be("1 1 N\r\n");
		}

		[Fact]
		public async Task RoverInstructionsCommandWithLostRoverReportsLost()
		{
			var mapGridMock = new Mock<IMapGrid>();
			mapGridMock.Setup(x => x.Width).Returns(10);
			mapGridMock.Setup(x => x.Height).Returns(10);
			var roversMgrMock = new Mock<IRoversManager>();
			var roverMock = new Mock<IRover>();
			roverMock.Setup(x => x.X).Returns(1);
			roverMock.Setup(x => x.Y).Returns(1);
			roverMock.Setup(x => x.Orientation).Returns(OrientationEnum.N);
			roverMock.Setup(x => x.Lost).Returns(true);
			roverMock.Setup(x => x.ExecuteInstruction(It.IsAny<char>())).Verifiable();
			roversMgrMock.Setup(x => x.ActiveRover).Returns(roverMock.Object);
			var cmdMock = new Mock<RoverInstructionsCommand>();
			cmdMock.Setup(x => x.Instructions).Returns(new List<char>("FF"));
			var handler = new CommandHandlers(mapGridMock.Object, roversMgrMock.Object);
			var consoleOutput = new StringWriter();
			Console.SetOut(consoleOutput);

			await handler.Handle(cmdMock.Object, new CancellationToken());

			roverMock.Verify(x => x.ExecuteInstruction(It.IsAny<char>()), Times.Once);
			roverMock.Object.Lost.Should().BeTrue();
			consoleOutput.ToString().Should().Be("1 1 N LOST\r\n");
		}
	}
}