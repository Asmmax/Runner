using Interactors;
using Core.Data;
using System.Collections.Generic;

public class TestLevelGateway : ILevelGateway
{
    private IDictionary<int, Level> levels = new Dictionary<int, Level>();
    public Level GetLevelStats(int level)
    {
        if (!levels.ContainsKey(level))
        {
            levels.Add(level, new Level(level));
        }

        return levels[level];
    }

    public void PutLevelStats(int level, Level stats)
    {
        levels[level] = stats;
    }
}
