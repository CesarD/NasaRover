using FluentAssertions;
using NasaRover.Commands;
using NasaRover.Movements;
using Xunit;

namespace NasaRover.Tests
{
	public class AttributesTests
	{
		[Fact]
		public void CommandRegexAttributeInitializesRegexCorrectly()
		{
			var attr = new CommandRegexAttribute("A");
			var match = attr.Regex.Match("A");

			match.Success.Should().BeTrue();
			match.Length.Should().Be(1);
			match.Value.Should().Be("A");
		}

		[Fact]
		public void MovementInstructionAttributeInitializesInstructionCorrectly()
		{
			var attr = new MovementInstructionAttribute('A');

			attr.Instruction.Should().Be('A');
		}
	}
}