using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Item giveItem;
    public bool destroyOnUse = true;
    public float useDuration = 1;
    private Behaviour halo;
    private int activePlayers = 0;
    // Start is called before the first frame update 
    void Start()
    {
        halo = (Behaviour)GetComponent("Halo");
        halo.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void used() {
        if(this.destroyOnUse) {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            this.activePlayers ++;
            halo.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player") {
            this.activePlayers --;
            if(activePlayers <= 0) {
                halo.enabled = false;
            }
        }
    }
}
