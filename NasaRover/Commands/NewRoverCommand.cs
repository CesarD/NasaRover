using NasaRover.Common;
using System;

namespace NasaRover.Commands
{
	[CommandRegex("\\d+ \\d+ [NSWE]$")]
	public class NewRoverCommand : CommandBase
	{
		public NewRoverCommand(string command) : base(command)
		{
			var parameters = command.Split(' ');
			StartingX = Convert.ToInt32(parameters[0]);
			StartingY = Convert.ToInt32(parameters[1]);
			StartingOrientation = Enum.Parse<OrientationEnum>(parameters[2]);
		}

		protected NewRoverCommand() : base(null) { }

		public virtual int StartingX { get; private set; }
		public virtual int StartingY { get; private set; }
		public virtual OrientationEnum StartingOrientation { get; private set; }
	}
}