using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SharedData
{
    // All true initially so you can run the Sam scene on its own.
    public static bool player1Joined = true;
    public static bool player2Joined = true;
    public static bool player3Joined = true;
    public static bool player4Joined = true;

    public static void Reset()
    {
        Debug.Log("Resetting shared data");
        player1Joined = false;
        player2Joined = false;
        player3Joined = false;
        player4Joined = false;
    }
}
