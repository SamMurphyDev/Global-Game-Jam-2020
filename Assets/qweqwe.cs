using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class qweqwe : MonoBehaviour
{
    public int player = 1;

    void Start()
    {
        foreach(MaterialSetter setter in GetComponentsInChildren<MaterialSetter>())
        {
            setter.TagSet("Player " + player);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
