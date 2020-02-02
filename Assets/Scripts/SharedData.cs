using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedData
{
    public static bool player1Joined = false;
    public static bool player2Joined = false;
    public static bool player3Joined = false;
    public static bool player4Joined = false;

    public static void Reset()
    {
        player1Joined = false;
        player2Joined = false;
        player3Joined = false;
        player4Joined = false;
    }
}
