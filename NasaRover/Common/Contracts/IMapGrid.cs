using System.Collections.Generic;

namespace NasaRover.Common.Contracts
{
	public interface IMapGrid
	{
		int Width { get; }
		int Height { get; }
		IReadOnlyList<(int x, int y)> ScentedPositions { get; }

		void Initialize(int maxX, int maxY);
		bool IsValid(int x, int y);
		void MarkScent(int x, int y);
		bool IsScented(int x, int y);
	}
}