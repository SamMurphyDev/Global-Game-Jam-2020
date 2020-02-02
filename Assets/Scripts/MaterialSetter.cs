using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    public Material player1;
    public Material player2;
    public Material player3;
    public Material player4;
    public Material player1Second;
    public Material player2Second;
    public Material player3Second;
    public Material player4Second;

    public void TagSet(string tag) {
        switch(tag) {
            case "Player 1":
                SkinnedMeshRenderer renderer = GetComponent<SkinnedMeshRenderer>();
                if(renderer == null)
                {
                    GetComponent<MeshRenderer>().materials = new Material[2] { player1, player1Second };
                } else
                {
                    renderer.materials = new Material[2] { player1, player1Second };
                }
                break;
            case "Player 2":
                SkinnedMeshRenderer renderer1 = GetComponent<SkinnedMeshRenderer>();
                if (renderer1 == null)
                {
                    GetComponent<MeshRenderer>().materials = new Material[2] { player2, player2Second };
                }
                else
                {
                    renderer1.materials = new Material[2] { player2, player2Second };
                }
                break;
            case "Player 3":
                SkinnedMeshRenderer renderer2 = GetComponent<SkinnedMeshRenderer>();
                if (renderer2 == null)
                {
                    GetComponent<MeshRenderer>().materials = new Material[2] { player3, player3Second };
                }
                else
                {
                    renderer2.materials = new Material[2] { player3, player3Second };
                }
                break;
            case "Player 4":
                SkinnedMeshRenderer renderer3 = GetComponent<SkinnedMeshRenderer>();
                if (renderer3 == null)
                {
                    GetComponent<MeshRenderer>().materials = new Material[2] { player4, player4Second };
                }
                else
                {
                    renderer3.materials = new Material[2] { player4, player4Second };
                }
                break;
        }
    }
}
