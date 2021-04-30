using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NasaRover.Commands;
using NasaRover.Common;
using NasaRover.Common.Contracts;
using NasaRover.Movements;
using NasaRover.Movements.Contracts;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NasaRover
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			await new ServiceCollection().AddMediatR(Assembly.GetExecutingAssembly())
										 .AddSingleton<IMapGrid, MapGrid>()
										 .AddSingleton<IMovementFactory, MovementFactory>()
										 .AddSingleton<IRoversManager, RoversManager>()
										 .AddSingleton<IApp, App>()
										 .BuildServiceProvider()
										 .GetRequiredService<IApp>()
										 .RunAsync();
		}
	}
	
	public class App : IApp
	{
		private readonly IMediator _mediator;

		public App(IMediator mediator)
		{
			_mediator = mediator;
		}

		public async Task RunAsync()
		{
			Console.WriteLine("Start entering parameters...");
			Console.WriteLine(string.Empty);

			while (true)
			{
				var command = Console.ReadLine()?.ToUpperInvariant();
				if (string.IsNullOrWhiteSpace(command))
					break;

				var cmd = command.GetCommandInstance();
				if (cmd == null)
					continue;
				await _mediator.Send(cmd);
			}
			
			Console.WriteLine("Press ENTER to exit");
			Console.ReadLine();
		}
	}

	public interface IApp
	{
		Task RunAsync();
	}
}
