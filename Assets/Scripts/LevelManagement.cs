using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagement : MonoBehaviour
{
    
    public Camera mainCamera;
    public GameObject levelObj;
    public GameObject sciFiBarrel;
    [Range(0,1)]
    public float barrelSpawnProbability = 0.1f;
    public Transform environment;
    public int levelXSize = 10;
    public int levelYSize = 10;
    [Range(0,1)]
    public float minRowFill = 0.25f;
    public int smoothingPasses = 2;
    [Range(0, 8)]
    public int smoothIntensity = 4;

    public float squaredOffset = 0;

    Vector2Int[] neighbouringTiles = {
        new Vector2Int(-1, -1), 
        new Vector2Int(1, -1),
        new Vector2Int(0, -1), 
        new Vector2Int(-1, 1), 
        new Vector2Int(1, 1), 
        new Vector2Int(0, 1), 
        new Vector2Int(-1, 0),
        new Vector2Int(1, 0)};

    [HideInInspector]
    public GameObject[,] map;

    List<Vector3> spawnLocations = new List<Vector3>();

    [Range(-5, 5)]
    public float playerDropHeight = 0;

    int[] NumberSpread(int total, int max) {
        if(total > max) {
            throw new System.Exception("Total needs to be less than Max");
        }

        List<int> numbers = new List<int>();
        
        do {
            int newRandom = Random.Range(0, max - 1);
            if(!numbers.Contains(newRandom)) {
                numbers.Add(newRandom);
            }
        } while(numbers.Count < total);

        numbers.Sort();

        return numbers.ToArray();
    }

    public Vector3 GetGameObjectSpawnLocation() {
        return spawnLocations[(int)Mathf.Floor(Random.Range(0, spawnLocations.Count))] + new Vector3(0, playerDropHeight, 0);
    }

    public void generateLevel (string seed) {
        // string seed = "112433abcc120b474d189a6979247624";

        Random.InitState(seed.GetHashCode());

        map = new GameObject[levelYSize, levelXSize];

        for(int ii = 0; ii < levelYSize; ii++) {
            int[] tilesLocationsCoordX = NumberSpread(Random.Range((int)(levelXSize * minRowFill), levelXSize), levelXSize);

            foreach(int locationX in tilesLocationsCoordX) {
                GameObject obj = Instantiate(levelObj, environment);
                obj.name = "Tile " + locationX + ", " + ii;
                map[ii, locationX] = obj;
                obj.transform.position = new Vector3(locationX * squaredOffset, 0, ii * squaredOffset);
                Tile tile = obj.GetComponent<Tile>();
                tile.Position = new Vector2Int(locationX, ii);

                if (Random.value > 1.0f - barrelSpawnProbability)
                {
                    // Put a barrel in the middle of the tile.
                    Instantiate(sciFiBarrel, obj.transform);
                }
            }
        }

        for(int iii = 0; iii < smoothingPasses; iii++) {
            int c = 0;
            for(int i = 0; i < levelYSize; i++) {
                for(int ii = 0; ii < levelXSize; ii++) {
                    if(map[i, ii] == null) {
                        continue;
                    }

                    int totalNeighbouringTiles = 0;
                    Vector2Int pos = new Vector2Int(ii, i);

                    foreach(Vector2Int neighbourTile in neighbouringTiles) {
                        Vector2Int checkPos = pos + neighbourTile;
                        
                        if(checkPos.x < 0 || checkPos.y < 0 || checkPos.x >= levelXSize || checkPos.y >= levelYSize) {
                            continue;
                        }

                        bool tileExists = map[checkPos.y, checkPos.x] != null;
                        totalNeighbouringTiles += tileExists  ? 1 : 0;
                        map[i, ii].GetComponent<Tile>().setDirectionTile(neighbourTile, tileExists);
                    }

                    if(totalNeighbouringTiles <= smoothIntensity) {
                        c++;
                        Destroy(map[i, ii]);
                        map[i, ii] = null;
                    }
                }
            }
        }

        Vector3 max = Vector3.zero;
        Vector3 min = Vector3.zero;
        for(int i = 0; i < levelYSize; i++) {
            for(int ii = 0; ii < levelXSize; ii++) {
                GameObject obj = map[i, ii];
                if (obj != null) {
                    spawnLocations.Add(obj.transform.position + new Vector3(0, 5, 0));

                    if(obj.transform.position.z > max.z) {
                        max.z = obj.transform.position.z;
                    }

                    if(obj.transform.position.z < min.z) {
                        min.z = obj.transform.position.z;
                    }
                }
            }    
        }

        Vector3 cameraMove = (max - min) / 2;
        mainCamera.transform.position = cameraMove - new Vector3(25, -25, 0);
        Instantiate(levelObj, cameraMove, Quaternion.identity);
    }

    void Start()
    {
        
    }
}
