using NasaRover.Common.Contracts;
using System.Collections.Generic;

namespace NasaRover.Common
{
	public class MapGrid : IMapGrid
	{
		public int Width { get; private set; }
		public int Height { get; private set; }

		private readonly List<(int x, int y)> _scentedPositions = new ();
		public IReadOnlyList<(int x, int y)> ScentedPositions => _scentedPositions;

		public void Initialize(int maxX, int maxY)
		{
			Width = maxX + 1;
			Height = maxY + 1;
		}

		public bool IsValid(int x, int y) => x >= 0 && x < Width && y >= 0 && y < Height;

		public void MarkScent(int x, int y)
		{
			//Only allow to scent a position if it's a valid one and it doesn't exist already in the list
			if (IsValid(x, y) && !IsScented(x, y))
				_scentedPositions.Add((x, y));
		}

		public bool IsScented(int x, int y) => _scentedPositions.Contains((x, y));
	}
}