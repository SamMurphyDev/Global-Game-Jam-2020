using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject crown;
    public GameObject fist;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnItem(Item item, Vector3 position)
    {
        Debug.Log("SPAWNING: " + item.ToString());
        GameObject gameObject = null;
        switch(item){
            case Item.Crown:
                Debug.Log("Crown");
                gameObject = crown;
                break;
            case Item.Fist:
                Debug.Log("Fist");
                gameObject = fist;
                break;
        }
        Debug.Log(gameObject != null);
        if (gameObject != null){
            Debug.Log("object? " + gameObject.ToString());
            Instantiate(gameObject, position, Quaternion.identity);
        }
    }
}
