using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalStats
{
    public static int score;
    public static int health;
    public static int shield;

    public static int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    public static int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public static int Shield
    {
        get
        {
            return shield;
        }
        set
        {
            shield = value;
        }
    }
}