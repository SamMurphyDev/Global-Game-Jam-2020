using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public LevelManagement levelManagement;
    public GameObject playerPrefab;
    [Range(-5, 5)]
    public float playerDropHeight = 0;
    public GameObject[] playerList = new GameObject[4];

    void Start()
    {
        // Handle Level Generation
        levelManagement.generateLevel(Guid.NewGuid().ToString());

        //Spawn Players In

        List<int> playerIndexes = new List<int>();
        playerIndexes.Add(1);
        playerIndexes.Add(2);
        playerIndexes.Add(3);
        playerIndexes.Add(4);
        foreach(int index in playerIndexes) {
            GameObject obj = Instantiate(playerPrefab, levelManagement.GetGameObjectSpawnLocation() + new Vector3(0, playerDropHeight, 0), Quaternion.identity);
            string name = "Player " + index;
            obj.GetComponent<PlayerMovement>().setChildTag(name);
            obj.name = name;
            playerList[index - 1] = obj;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
