using MediatR;

namespace NasaRover.Commands
{
	public abstract class CommandBase : IRequest
	{
		protected readonly string _command;

		protected CommandBase(string command)
		{
			_command = command;
		}
	}
}