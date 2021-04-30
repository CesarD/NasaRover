using System.Collections.Generic;
using System.Linq;

namespace NasaRover.Commands
{
	[CommandRegex("^[FKRL]+$")]
	public class RoverInstructionsCommand : CommandBase
	{
		public RoverInstructionsCommand(string command) : base(command)
		{
			_instructions = command.ToList();
		}

		protected RoverInstructionsCommand() : base(null) { }

		private readonly List<char> _instructions = new ();
		public virtual IReadOnlyList<char> Instructions => _instructions;
	}
}