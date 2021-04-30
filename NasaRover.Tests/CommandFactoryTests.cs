using FluentAssertions;
using NasaRover.Commands;
using Xunit;

namespace NasaRover.Tests
{
	public class CommandFactoryTests
	{
		[Theory]
		[InlineData(10, 10)]
		[InlineData(2, 4)]
		public void MapSizeReturnsMapGridInitializerCommand(int width, int height)
		{
			var cmd = $"{width} {height}".GetCommandInstance();

			cmd.Should().BeOfType<MapGridInitializerCommand>();
		}

		[Theory]
		[InlineData("12. 21")]
		[InlineData("12 21.")]
		[InlineData("12 21 a")]
		public void BadFormedMapSizeReturnsNull(string command)
		{
			command.GetCommandInstance().Should().BeNull();
		}

		[Theory]
		[InlineData(10, 10, "N")]
		[InlineData(2, 4, "S")]
		public void RoverLocationReturnsNewRoverCommand(int x, int y, string orientation)
		{
			var cmd = $"{x} {y} {orientation}".GetCommandInstance();

			cmd.Should().BeOfType<NewRoverCommand>();
		}

		[Theory]
		[InlineData("12 21 X")]
		[InlineData("12 21. N")]
		[InlineData("12. 21 W")]
		public void BadFormedRoverLocationsReturnsNull(string command)
		{
			command.GetCommandInstance().Should().BeNull();
		}

		[Theory]
		[InlineData("FFRFLF")]
		[InlineData("FFFFRRFFFFLLLFFFF")]
		public void RoverInstructionsReturnRoverInstructionsCommand(string command)
		{
			var cmd = command.GetCommandInstance();

			cmd.Should().BeOfType<RoverInstructionsCommand>();
		}

		[Theory]
		[InlineData("FFFFMF")]
		[InlineData("FFFLRLRFA")]
		[InlineData("ABC")]
		public void BadFormedRoverInstructionsReturnsNull(string command)
		{
			command.GetCommandInstance().Should().BeNull();
		}
	}
}
