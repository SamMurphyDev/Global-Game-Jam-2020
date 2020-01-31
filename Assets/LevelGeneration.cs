using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    
    public GameObject levelObj;
    public Transform environment;
    public int levelXSize = 10;
    public int levelYSize = 10;

    public int droppers = 3;

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
            // numbers.Add(newRandom);
        } while(numbers.Count < total);

        return numbers.ToArray();
    }

    void Start()
    {
        string seed = "112433abcc120b474d189a6979247624";

        for(int ii = 0; ii < levelYSize; ii++) {
            int[] tilesLocationsCoordX = NumberSpread(Random.Range((int)(levelXSize * 0.25f), levelXSize), levelXSize);

            foreach(int locationX in tilesLocationsCoordX) {
                GameObject obj = Instantiate(levelObj, environment);
                obj.name = "Tile " + locationX + ", " + ii;
                obj.transform.position = new Vector3(locationX, 0, ii);
            }
        }
    }
}
