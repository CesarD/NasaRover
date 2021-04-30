using MediatR;
using System;
using System.Linq;
using System.Reflection;

namespace NasaRover.Commands
{
	public static class CommandFactory
	{
		public static IRequest GetCommandInstance(this string command)
		{
			var commandType = Assembly.GetExecutingAssembly()
									  .DefinedTypes
									  .SingleOrDefault(type => type.IsSubclassOf(typeof(CommandBase)) &&
															   !type.IsAbstract &&
															   type.GetCustomAttributes<CommandRegexAttribute>()
																   .Any(x => x.Regex.IsMatch(command)));

			return commandType != null
					   ? Activator.CreateInstance(commandType, command) as IRequest
					   : null;
		}
	}

}