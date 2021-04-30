using FluentAssertions;
using NasaRover.Common;
using Xunit;

namespace NasaRover.Tests
{
	public class MapGridTests
	{
		[Fact]
		public void MapGridInitialStateTest()
		{
			var mapGrid = new MapGrid();

			mapGrid.Width.Should().Be(0);
			mapGrid.Height.Should().Be(0);
			mapGrid.ScentedPositions.Should().BeEmpty();
		}

		[Fact]
		public void InitializeTest()
		{
			var mapGrid = new MapGrid();

			mapGrid.Initialize(10, 20);

			mapGrid.Width.Should().Be(11);
			mapGrid.Height.Should().Be(21);
			mapGrid.ScentedPositions.Should().BeEmpty();
		}

		[Fact]
		public void IsValidTest()
		{
			var mapGrid = new MapGrid();

			mapGrid.Initialize(10, 20);

			mapGrid.IsValid(5, 8).Should().BeTrue();
			mapGrid.IsValid(15, 30).Should().BeFalse();
		}

		[Fact]
		public void MarkScentWithValidValueSavesOneTest()
		{
			var mapGrid = new MapGrid();
			mapGrid.Initialize(10, 20);

			mapGrid.MarkScent(5, 10);

			mapGrid.ScentedPositions.Should().SatisfyRespectively(tuple => tuple.Should().Be((5, 10)));
		}

		[Fact]
		public void MarkScentWithInvalidValueSavesNoneTest()
		{
			var mapGrid = new MapGrid();
			mapGrid.Initialize(10, 20);

			mapGrid.MarkScent(15, 25);

			mapGrid.ScentedPositions.Should().BeEmpty();
		}

		[Fact]
		public void MarkScentWithRepeatedValueKeepsOnlyOneTest()
		{
			var mapGrid = new MapGrid();
			mapGrid.Initialize(10, 20);

			mapGrid.MarkScent(5, 10);

			mapGrid.MarkScent(5, 10);

			mapGrid.ScentedPositions.Should().HaveCount(1).And.SatisfyRespectively(tuple => tuple.Should().Be((5, 10)));
		}

		[Fact]
		public void IsScentedTrueIfScentedValueExistsTest()
		{
			var mapGrid = new MapGrid();
			mapGrid.Initialize(10, 20);

			mapGrid.MarkScent(5, 10);

			mapGrid.IsScented(5, 10).Should().BeTrue();
		}

		[Fact]
		public void IsScentedFalseIfScentedValueDoesntExistTest()
		{
			var mapGrid = new MapGrid();
			mapGrid.Initialize(10, 20);

			mapGrid.MarkScent(5, 10);

			mapGrid.IsScented(1, 1).Should().BeFalse();
		}
	}
}