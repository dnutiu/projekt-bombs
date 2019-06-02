using System.Collections;
using System.Collections.Generic;

public sealed class BombStatsUtil
{
    
    private static readonly BombStatsUtil instance = new BombStatsUtil();
    const int MAX_POWER = 7;

    public int Power { get; private set; } = 3;

    public float Timer { get; } = 3.0f;

    // Explicit static constructor to tell C# compiler
    // not to mark type as beforefieldinit
    static BombStatsUtil()
    {
    }

    private BombStatsUtil()
    {
    }

    public static BombStatsUtil Instance
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
