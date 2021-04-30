namespace NasaRover.Commands
{
	[CommandRegex("^GS+$")]
	public class GetScentedPositionsCommand : CommandBase
	{
		public GetScentedPositionsCommand(string command) : base(command)
		{
		}

		protected GetScentedPositionsCommand() : base(null) { }
	}
}