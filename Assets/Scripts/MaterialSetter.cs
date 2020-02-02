using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    public Material player1;
    public Material player2;
    public Material player3;
    public Material player4;

    public void TagSet(string tag) {
        switch(tag) {
            case "Player 1":
                GetComponent<SkinnedMeshRenderer>().materials.SetValue(player1, 0);
                break;
            case "Player 2":
                GetComponent<SkinnedMeshRenderer>().materials.SetValue(player2, 0);
                break;
            case "Player 3":
                GetComponent<SkinnedMeshRenderer>().materials.SetValue(player3, 0);
                break;
            case "Player 4":
                GetComponent<SkinnedMeshRenderer>().materials.SetValue(player4, 0);
                break;
        }
    }
}
