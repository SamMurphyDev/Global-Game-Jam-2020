using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3();
    private Interactable targetInteractable;
    public ParticleSystem particles;

    public GameObject stabilisedChild; 
    public ChargeBar chargeBar;

    private float progress = 0;

    public Item itemLeft;
    public Item itemRight;
    public Item itemTop;

    private ItemSpawner itemSpawner;

    // Start is called before the first frame update 
    void Start()
    {
        itemSpawner = GameObject.FindGameObjectWithTag("ItemSpawner").GetComponent<ItemSpawner>();
    }

    void Update()
    {
        stabilisedChild.transform.rotation = Quaternion.identity;
        if(Input.GetButton("Fire2") && targetInteractable != null) {
            Vector3 aim = targetPosition - gameObject.transform.position;
            if(particles != null) {
                var sh = particles.shape;
                sh.position = aim;
                sh.rotation = Quaternion.LookRotation(new Vector3(0, 1, 0) - aim).eulerAngles;
                if(!particles.isPlaying) {
                    particles.Play();
                }
            }

            progress += Time.deltaTime;
            // if(progress >= targetInteractable.useDuration) {
            if(targetInteractable.isUsed(progress)) {
                targetInteractable.used();
                // receive the item you finished sucking
                pickUpItem(targetInteractable.giveItem);
            }else {
                if(chargeBar != null) {
                    chargeBar.showBar(true);
                    chargeBar.percentage = progress / targetInteractable.useDuration;
                }
            }
        }
        else if(particles.isPlaying) {
            chargeBar.showBar(false);
            particles.Stop();
            progress = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(targetInteractable == null && other.tag == "Interactable") {
            this.targetPosition = other.gameObject.transform.position;
            this.targetInteractable = other.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Interactable") {
            this.targetInteractable = null;
        }
    }

    public void pickUpItem(Item item)
    {
        progress = 0;

        if (itemTop != Item.None){
            itemSpawner.SpawnItem(itemTop, gameObject.transform.position);
        }

        itemTop = item;
    }
}
