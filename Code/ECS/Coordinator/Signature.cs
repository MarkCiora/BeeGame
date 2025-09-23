public struct Signature
{
    public ulong bits;

    public void Set(int componentType, bool value)
    {
        if (value)
            bits |= 1UL << componentType;
        else
            bits &= ~(1UL << componentType);
    }

    public bool Has(int componentType)
    {
        return (bits & (1UL << componentType)) != 0;
    }

    public void Reset()
    {
        bits = 0;
    }
}