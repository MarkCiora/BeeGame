using BeeGame;

namespace BeeGame.UnitTests.ECS;

public class SignatureTests
{
    [Fact]
    public void Set_WhenEnabledAndDisabled_UpdatesOnlySelectedBit()
    {
        var signature = new Signature();

        signature.Set(2, true);
        signature.Set(5, true);
        signature.Set(2, false);

        Assert.False(signature.Has(2));
        Assert.True(signature.Has(5));
    }

    [Fact]
    public void Reset_AfterComponentsAreSet_ClearsAllBits()
    {
        var signature = new Signature();
        signature.Set(1, true);
        signature.Set(7, true);

        signature.Reset();

        Assert.Equal(0UL, signature.bits);
    }
}
