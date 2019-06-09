using System.Collections;
using System.Collections.Generic;

public sealed class BombStatsManager
{
    
    private static readonly BombStatsManager instance = new BombStatsManager();
    const int MAX_POWER = 7;

    public int Power { get; private set; } = 3;

    public float Timer { get; } = 3.0f;

    public float ExplosionDuration { get; } = 0.55f;

    private BombStatsManager()
    {
    }

    public static BombStatsManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void increasePower()
    {
        if(Power <= MAX_POWER)
        {
            Power++;
        }
    }

}
