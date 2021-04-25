using Saves;
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

    public void PutLevelStats(Level stats)
    {
        if (stats != null) return;

        if (!levels.ContainsKey(stats.Id))
        {
            levels.Add(stats.Id, stats);
        }
        else
        {
            levels[stats.Id] = stats;
        }
    }

    public void ResetStats()
    {
        levels.Clear();
    }
}
