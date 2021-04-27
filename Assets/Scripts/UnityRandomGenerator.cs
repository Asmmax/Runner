using Unity.Mathematics;
using Core.Level;
public class UnityRandomGenerator : IRandomGenerator
{
    private Random random;

    UnityRandomGenerator()
    {
        Reset(0);
    }

    public int GenInt(int maxValue)
    {
        return random.NextInt(maxValue+1);
    }

    public float GenUniformFloat()
    {
        return random.NextFloat();
    }

    public void Reset(int seed)
    {
        random = new Random((uint)(seed + int.MaxValue));
    }
}
