using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("X P1"))
        {
            SharedData.player1Joined = true;
            // TODO: Update the UI.
        }

        if (Input.GetButton("X P2"))
        {
            SharedData.player2Joined = true;
            // TODO: Update the UI.
        }

        if (Input.GetButton("X P3"))
        {
            SharedData.player3Joined = true;
            // TODO: Update the UI.
        }

        if (Input.GetButton("X P4"))
        {
            SharedData.player4Joined = true;
            // TODO: Update the UI.
        }

        if (Input.GetButton("Start"))
        {
            if (SharedData.player1Joined || SharedData.player2Joined || SharedData.player3Joined || SharedData.player4Joined)
            {
                Debug.Log("Loading SampleScene");
                SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
            }
            else
            {
                Debug.Log("At least one player has to join the game.");
            }
        }
    }
}
