using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SampleSceneManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (SharedData.player1Joined)
        {
            Debug.Log("Player 1 Joined");
        }
        else
        {
            GameObject.Destroy(GameObject.FindWithTag("Player 1").transform.parent.gameObject);
        }

        if (SharedData.player2Joined)
        {
            Debug.Log("Player 2 Joined");
        }
        else
        {
            GameObject.Destroy(GameObject.FindWithTag("Player 2").transform.parent.gameObject);
        }

        if (SharedData.player3Joined)
        {
            Debug.Log("Player 3 Joined");
        }
        else
        {
            GameObject.Destroy(GameObject.FindWithTag("Player 3").transform.parent.gameObject);
        }

        if (SharedData.player4Joined)
        {
            Debug.Log("Player 4 Joined");
        }
        else
        {
            GameObject.Destroy(GameObject.FindWithTag("Player 4").transform.parent.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Start"))
        {
            SharedData.Reset();
            Debug.Log("Loading StartScreen");
            SceneManager.LoadScene("StartScreen", LoadSceneMode.Single);
        }
    }
}
