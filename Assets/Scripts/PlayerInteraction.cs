using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3();
    private Interactable targetInteractable;
    public ParticleSystem particles;

    public GameObject stabilisedChild;
    public ChargeBar useProgressBar;

    private float progress = 0;
    public float dropThreshold = 12;

    public Item itemHands;
    public Item itemHead;
    public Dictionary<ItemSlot, Item> items = new Dictionary<ItemSlot, Item>();
    private ItemSpawner itemSpawner;
    private float speed;
    public Rigidbody rb;
    private PlayerMovement movement;

    private ChargeBar playerChargeBar;

    // Start is called before the first frame update 
    void Start()
    {
        Debug.Log("movement");
        itemSpawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemSpawner>();
        Debug.Log("movement");

        movement = GetComponent<PlayerMovement>();
        Debug.Log(movement);
    }

    public void tagSetup(string tag) {
        GameObject hud = GameObject.FindGameObjectWithTag(tag + " HUD");
        playerChargeBar = hud.GetComponentInChildren<ChargeBar>();
        playerChargeBar.percentage = 0.5F;
        playerChargeBar.showBar(true);
    }

    void Update()
    {
        stabilisedChild.transform.rotation = Quaternion.Euler(0, 90, 0);
        if (Input.GetAxisRaw(movement.interactButtonAxis) == 1 && targetInteractable != null)
        {
            Vector3 aim = targetPosition - gameObject.transform.position;
            if (particles != null)
            {
                var sh = particles.shape;
                sh.position = aim;
                sh.rotation = Quaternion.LookRotation(new Vector3(0, 1, 0) - aim).eulerAngles;
                if (!particles.isPlaying)
                {
                    particles.Play();
                }
            }

            progress += Time.deltaTime;
            // if(progress >= targetInteractable.useDuration) {
            if (targetInteractable.isUsed(progress))
            {
                targetInteractable.used();
                // receive the item you finished sucking
                pickUpItem(targetInteractable.giveItem, targetInteractable.itemSlot);
            }
            else
            {
                if (useProgressBar != null)
                {
                    useProgressBar.showBar(true);
                    useProgressBar.percentage = progress / targetInteractable.useDuration;
                }
            }
        }
        else if (particles.isPlaying)
        {
            useProgressBar.showBar(false);
            particles.Stop();
            progress = 0;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (targetInteractable == null && other.tag == "Interactable")
        {
            this.targetPosition = other.gameObject.transform.position;
            this.targetInteractable = other.gameObject.GetComponent<Interactable>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Interactable")
        {
            this.targetInteractable = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            Debug.Log(collision.relativeVelocity.magnitude);
            Debug.Log(speed);
            if((collision.relativeVelocity.magnitude - speed) > dropThreshold) {
                if(items.ContainsKey(ItemSlot.Head)) {
                    dropItem(ItemSlot.Head);
                } else if(items.ContainsKey(ItemSlot.Hands)) {
                    dropItem(ItemSlot.Hands);
                }
            }
        }
    }

    private void dropItem(ItemSlot slot) {
        itemSpawner.SpawnItem(items[slot], gameObject.transform.position);
        items.Remove(slot);
    }

    public void pickUpItem(Item item, ItemSlot slot)
    {
        // we MUST reset please
        progress = 0;

        if(item == Item.Crown) {
            playerChargeBar.percentage = 1;
        }

        if (items.ContainsKey(slot))
        {
            itemSpawner.SpawnItem(items[slot], gameObject.transform.position);
        }

        items[slot] = item;
    }

    private void FixedUpdate() {
        speed = rb.velocity.magnitude;
    }
}
