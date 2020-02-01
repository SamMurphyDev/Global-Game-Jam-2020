using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelManagement levelManagement;
    public GameObject playerPrefab;
    public GameObject[] playerList = new GameObject[4];

    public struct AxisMapping {
        string horizontalAxis;
        string verticalAxis;
    }

    public AxisMapping[] playerControls;

    void Start()
    {
        // Handle Level Generation
        levelManagement.generateLevel("Sam");

        //Spawn Players In

        List<int> playerIndexes = new List<int>();
        foreach(int index in playerIndexes) {
            GameObject obj = Instantiate(playerPrefab, levelManagement.GetGameObjectSpawnLocation(), Quaternion.identity);
            obj.GetComponent<PlayerMovement>().
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
