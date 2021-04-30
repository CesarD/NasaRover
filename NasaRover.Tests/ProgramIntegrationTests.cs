using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NasaRover.Tests
{
	public class ProgramIntegrationTests
	{
		[Fact]
		public async Task RunSampleInputResultsInSampleOutput()
		{
			var commands = new[]
						   {
							   "5 3", 
							   "1 1 E", 
							   "RFRFRFRF", 
							   "3 2 N", 
							   "FRRFLLFFRRFLL", 
							   "0 3 W", 
							   "LLFFFLFLFL"
						   };
			var consoleInput = new StringReader(string.Join(Environment.NewLine, commands));
			var consoleOutput = new StringWriter();

			Console.SetIn(consoleInput);
			Console.SetOut(consoleOutput);
			
			await Program.Main(null);
			
			const string expectedOutput = "Start entering parameters...\r\n\r\n1 1 E\r\n3 3 N LOST\r\n2 3 S\r\nPress ENTER to exit\r\n";

			consoleOutput.ToString().Should().Be(expectedOutput);
		}
	}
}