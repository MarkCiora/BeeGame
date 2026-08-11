using BeeGame;

namespace BeeGame.UnitTests.Helper;

public class HexPointTests
{
    [Fact]
    public void Length_ForKnownCoordinate_ReturnsHexDistanceFromOrigin()
    {
        var point = new HexPoint(3, -1);

        int distance = point.Length();

        Assert.Equal(3, distance);
    }

    [Fact]
    public void RotatedBy_AfterSixRotations_ReturnsOriginalPoint()
    {
        var original = new HexPoint(3, -2);
        var rotated = original;

        for (int i = 0; i < 6; i++)
        {
            rotated = rotated.RotatedBy(1);
        }

        Assert.Equal(original, rotated);
    }

    [Fact]
    public void GetRing_WithRadiusTwo_ReturnsTwelvePointsAtDistanceTwo()
    {
        var center = new HexPoint(0, 0);

        HexPoint[] ring = center.GetRing(2);

        Assert.Equal(12, ring.Length);
        Assert.All(ring, point => Assert.Equal(2, point.Length()));
        Assert.Equal(12, ring.Distinct().Count());
    }
}
