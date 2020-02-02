using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject[] littleClaws;
    public GameObject[] littleArm;
    public GameObject[] littleBlade;

    public GameObject[] bigClaws;
    public GameObject[] bigArm;
    public GameObject[] bigBlade;
    public Text winText;
    public GameObject solarPanel;
    private float charge;
    private int playerNumber;

    // Start is called before the first frame update 
    void Start()
    {
        itemSpawner = GameObject.FindGameObjectWithTag("GameController").GetComponent<ItemSpawner>();
        winText = GameObject.FindGameObjectWithTag("HUD").GetComponent<Text>();
        movement = GetComponent<PlayerMovement>();
    }

    public void tagSetup(int playerNumber) {
        
        string tag = "P" + playerNumber;
        this.playerNumber = playerNumber;
        GameObject hud = GameObject.FindGameObjectWithTag(tag + " HUD");
        Debug.Log(tag);
        playerChargeBar = hud.GetComponentInChildren<ChargeBar>();
        playerChargeBar.percentage = 0;
        Debug.Log("showing...");
        playerChargeBar.showBar(true);
        Debug.Log("SHOWING BAR FOR PLAYER " + tag);
    }

    void Update()
    {
        stabilisedChild.transform.rotation = Quaternion.identity;
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

        if(items.ContainsValue(Item.Crown)) {
            charge += Time.deltaTime;
            if(charge >= 15 && !winText.enabled) {
                winText.text = "Player " + playerNumber + " Wins!";
                winText.enabled = true;
            }
            playerChargeBar.percentage = charge / 15;
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

    private void setStatusOfPlayerParts(bool status, Item item) {
        switch(item) {
            case Item.Claw:
                foreach(GameObject obj in littleClaws) {
                    obj.SetActive(status);
                }
                break;
            case Item.Sword:
                foreach(GameObject obj in littleBlade) {
                    obj.SetActive(status);
                }
                break;
            case Item.Fist:
                foreach(GameObject obj in littleArm) {
                    obj.SetActive(status);
                }
                break;
            case Item.Crown:
                //solarPanel.SetActive(status);
                break;
            case Item.Big_Claw:
                foreach(GameObject obj in bigClaws) {
                    obj.SetActive(status);
                }
                break;
            case Item.Big_Sword:
                foreach(GameObject obj in bigBlade) {
                    obj.SetActive(status);
                }
                break;
            case Item.Big_Fist:
                foreach(GameObject obj in bigArm) {
                    obj.SetActive(status);
                }
                break;
        }
    }

    private void dropItem(ItemSlot slot) {
        itemSpawner.SpawnItem(items[slot], gameObject.transform.position);
        setStatusOfPlayerParts(false, items[slot]);
        items.Remove(slot);
    }

    public void pickUpItem(Item item, ItemSlot slot)
    {
        // we MUST reset please
        progress = 0;

        if (items.ContainsKey(slot))
        {
            itemSpawner.SpawnItem(items[slot], gameObject.transform.position);
        }

        items[slot] = item;

        setStatusOfPlayerParts(true, item);
    }

    private void FixedUpdate() {
        speed = rb.velocity.magnitude;
    }
}
