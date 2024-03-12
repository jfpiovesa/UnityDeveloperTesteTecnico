using System;

public static class PlayerStatickStatus
{
    public static Action<int> CoinsPlayer { get; set; }

    public static Action<int> LevelPlayer { get; set; }


    public static void SetCoins(int value)
    {
        CoinsPlayer?.Invoke(value);
    }
    public static void SetLevel(int value)
    {
        LevelPlayer?.Invoke(value);
    }
}
