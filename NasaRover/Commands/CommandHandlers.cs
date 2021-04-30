using MediatR;
using NasaRover.Common.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NasaRover.Commands
{
	public class CommandHandlers : IRequestHandler<MapGridInitializerCommand>,
								   IRequestHandler<NewRoverCommand>,
								   IRequestHandler<RoverInstructionsCommand>
	{
		private readonly IMapGrid _mapGrid;
		private readonly IRoversManager _roversManager;

		public CommandHandlers(IMapGrid mapGrid, IRoversManager roversManager)
		{
			_mapGrid = mapGrid;
			_roversManager = roversManager;
		}

		public Task<Unit> Handle(MapGridInitializerCommand request, CancellationToken cancellationToken)
		{
			_mapGrid.Initialize(request.Width, request.Height);

			return Task.FromResult(Unit.Value);
		}

		public Task<Unit> Handle(NewRoverCommand request, CancellationToken cancellationToken)
		{
			_roversManager.AddRover(request.StartingX, request.StartingY, request.StartingOrientation);

			return Task.FromResult(Unit.Value);
		}

		public Task<Unit> Handle(RoverInstructionsCommand request, CancellationToken cancellationToken)
		{
			if (_roversManager.ActiveRover != null)
			{
				var rover = _roversManager.ActiveRover;
				foreach (var instruction in request.Instructions)
				{
					rover.ExecuteInstruction(instruction);
					if (rover.Lost)
						break;
				}

				Console.WriteLine($"{rover.X} {rover.Y} {rover.Orientation}{(rover.Lost ? " LOST" : string.Empty)}");

				_roversManager.DisposeRover();
			}

			return Task.FromResult(Unit.Value);
		}
	}
}