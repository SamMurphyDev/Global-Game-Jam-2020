using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Vector3 targetPosition = new Vector3();
    private Interactable targetInteractable;
    private ParticleSystem particles;

    private float progress = 0;

    // Start is called before the first frame update 
    void Start()
    {
        particles = gameObject.GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if(Input.GetButton("Fire2") && targetInteractable != null) {
            Vector3 aim = targetPosition - gameObject.transform.position;
            if(particles != null) {
                var sh = particles.shape;
                sh.rotation = Quaternion.LookRotation(aim).eulerAngles - gameObject.transform.rotation.eulerAngles;
                if(!particles.isPlaying) {
                    particles.Play();
                }
            }
            progress += Time.fixedDeltaTime;
            if(progress >= targetInteractable.useDuration) {
                targetInteractable.used();
            }

        }
        else if(particles.isPlaying) {
            particles.Stop();
            progress = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Interactable") {
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
}
