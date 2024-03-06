using System;

/// <summary>
/// Stores all game data and notifies about changes
/// </summary>
public static class PlayerStats
{
    public static event Action<int> OnMoneyChanged;
    public static event Action<int> OnLevelChanged;
    public static event Action<int> OnExpiriencePointsChanged;

    private static int _money = 0;
    private static int _level = 1;
    private static int _expiriencePoints = 0;
    private static int _experiencePointsToNewLevel = 10;

    public static int Money
    {
        get => _money;
        set
        {
            _money = value;
            OnMoneyChanged?.Invoke(_money);
        }
    }

    public static int Level
    {
        get => _level;
        set
        {
            _level = value;
            BaseAttackDamage *= 2;
            MaxHealth += 20;
            OnLevelChanged?.Invoke(_level);
        }
    }

    public static int ExpiriencePoints
     {
        get => _expiriencePoints;
        set
        {
            _expiriencePoints = value;

            while(_expiriencePoints >= _experiencePointsToNewLevel)
            {
                _expiriencePoints -= _experiencePointsToNewLevel;
                _experiencePointsToNewLevel = (int)(_experiencePointsToNewLevel * 1.5);
                Level++;
            }

            OnExpiriencePointsChanged?.Invoke(_expiriencePoints);
        }
    }

    public static int BaseAttackDamage { get; set; } = 5;
    public static int MaxHealth { get; set; } = 50;
}
