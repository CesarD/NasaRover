using System;

namespace NasaRover.Commands
{
	[CommandRegex("\\d+ \\d+$")]
	public class MapGridInitializerCommand : CommandBase
	{
		public MapGridInitializerCommand(string command) : base(command)
		{
			var parameters = command.Split(" ");
			Width = Convert.ToInt32(parameters[0]);
			Height = Convert.ToInt32(parameters[1]);
		}

		protected MapGridInitializerCommand() : base(null) { }

		public virtual int Width { get; private set; }
		public virtual int Height { get; private set; }
	}
}