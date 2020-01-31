using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    
    public GameObject levelObj;
    public Transform environment;
    public int levelXSize = 10;
    public int levelYSize = 10;

    void Start()
    {
        for(int i = 0; i < levelXSize; i++) {
            for(int ii = 0; ii < levelYSize; ii++) {
                GameObject obj = Instantiate(levelObj, environment);
                obj.transform.position = new Vector3(i, 0, ii);
                obj.name = "Level Section - " + i + ", " + ii;
            }   
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
