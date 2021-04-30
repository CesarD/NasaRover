using NasaRover.Movements.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NasaRover.Movements
{
	public class MovementFactory : IMovementFactory
	{
		private readonly Dictionary<TypeInfo, IMovement> _movements;

		public MovementFactory()
		{
			_movements = Assembly.GetExecutingAssembly()
								 .DefinedTypes
								 .Where(type => type.ImplementedInterfaces.Any(i => i == typeof(IMovement)) &&
												type.GetCustomAttributes<MovementInstructionAttribute>().Any())
								 .ToDictionary(x => x, _ => (IMovement) null);
		}
		
		public IMovement GetMovement(char instruction)
		{
			var type = _movements.Keys.SingleOrDefault(x => x.GetCustomAttributes<MovementInstructionAttribute>()
															 .Any(a => a.Instruction == instruction));
			if (type == null)
				return null;

			return _movements[type] ??= Activator.CreateInstance(type) as IMovement;
		}
	}
}