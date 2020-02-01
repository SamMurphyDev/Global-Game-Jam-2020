using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnManager : MonoBehaviour
{
    private LevelGeneration levelGen;

    void Start() {
        levelGen = GetComponent<LevelGeneration>();
    }

    Vector3 findMostOuterPosition(Vector2Int outerDirection) {
        Vector2Int startingPos = new Vector2Int(outerDirection.x == -1 ? 0 : levelGen.levelXSize - 1, outerDirection.y == -1 ? 0 : levelGen.levelYSize - 1)
    }

    public void moveToPlayerSpawn(int playerIndex, GameObject player) {

    }
}
