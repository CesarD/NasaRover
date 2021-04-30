using FluentAssertions;
using NasaRover.Commands;
using Xunit;

namespace NasaRover.Tests
{
	public class CommandsTests
	{
		[Fact]
		public void MapSizeCommandInitializesMapGridInitializerCommandCorrectly()
		{
			var cmd = new MapGridInitializerCommand("5 40");

			cmd.Width.Should().Be(5);
			cmd.Height.Should().Be(40);
		}

		[Fact]
		public void RoverLocationCommandInitializesNewRoverCommandCorrectly()
		{
			var cmd = new NewRoverCommand("5 10 N");
			
			cmd.StartingX.Should().Be(5);
			cmd.StartingY.Should().Be(10);
			cmd.StartingOrientation.ToString().Should().Be("N");
		}

		[Fact]
		public void RoverInstructionsInitilizeRoverInstructionsCommandCorrectly()
		{
			var cmd = new RoverInstructionsCommand("FFRFLF");

			cmd.Instructions.Should().SatisfyRespectively(c => c.Should().Be('F'),
														  c => c.Should().Be('F'),
														  c => c.Should().Be('R'),
														  c => c.Should().Be('F'),
														  c => c.Should().Be('L'),
														  c => c.Should().Be('F'));
		}
	}
}