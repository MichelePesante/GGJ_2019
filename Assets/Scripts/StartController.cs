using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour {

    public int menuSelection;
    public bool oldTriggerHeld;
    public Image PanelImage;
    public bool inCreditsPanel;

    void Update ()
    {
        if (Input.GetAxisRaw("Horizontal_Player1") <= 0)
        {
            oldTriggerHeld = false;
        }

        if (Input.GetAxisRaw("Horizontal_Player1") >= 0.9f && !oldTriggerHeld && !inCreditsPanel)
        {
            oldTriggerHeld = true;
            menuSelection++;
            if (menuSelection > 2)
            {
                menuSelection = 0;
            }
        }

        if (Input.GetButtonDown("Start"))
        {
            if (!inCreditsPanel)
            {
                SwapSceneToLoad();
            }
            else
            {
                PanelImage.gameObject.SetActive(false);
                inCreditsPanel = false;
            }
        }
    }

    public void SwapSceneToLoad()
    {
        switch (menuSelection)
        {
            case 0:
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                break;
            case 1:
                PanelImage.gameObject.SetActive(true);
                inCreditsPanel = true;
                break;
            case 2:
                Application.Quit();
                break;
            default:
                break;
        }
    }
}