using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // These are true by default in SharedData to make testing easier.
        SharedData.player1Joined = false;
        SharedData.player2Joined = false;
        SharedData.player3Joined = false;
        SharedData.player4Joined = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("X P1"))
        {
            Debug.Log("P1 Joined");
            SharedData.player1Joined = true;
            GameObject.FindGameObjectWithTag("P1 Selected").GetComponent<UnityEngine.UI.Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        }

        if (Input.GetButton("X P2"))
        {
            SharedData.player2Joined = true;
            GameObject.FindGameObjectWithTag("P2 Selected").GetComponent<UnityEngine.UI.Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        }

        if (Input.GetButton("X P3"))
        {
            SharedData.player3Joined = true;
            GameObject.FindGameObjectWithTag("P3 Selected").GetComponent<UnityEngine.UI.Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        }

        if (Input.GetButton("X P4"))
        {
            SharedData.player4Joined = true;
            GameObject.FindGameObjectWithTag("P4 Selected").GetComponent<UnityEngine.UI.Image>().rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 200);
        }

        if (Input.GetButton("Start"))
        {
            if (SharedData.player1Joined || SharedData.player2Joined || SharedData.player3Joined || SharedData.player4Joined)
            {
                Debug.Log("Loading Main Game Scene");
                SceneManager.LoadScene(1);
            }
            else
            {
                Debug.Log("At least one player has to join the game.");
            }
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
