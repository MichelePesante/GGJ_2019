using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour {

    public int menuSelection;
    public bool oldRightTriggerHeld;
    public bool OldLeftTriggerHeld;
    public Image PanelImage;
    public bool inCreditsPanel;
    public List<Light> lights;

    private AudioSource source;
    public AudioClip chickClip;

    private void Awake() {
        source = GetComponent<AudioSource>();
    }

    void Update ()
    {
        if (Input.GetAxisRaw("Horizontal_Player1") <= 0)
        {
            oldRightTriggerHeld = false;
        }

        if (Input.GetAxisRaw("Horizontal_Player1") >= 0)
        {
            OldLeftTriggerHeld = false;
        }

        if (Input.GetAxisRaw("Horizontal_Player1") >= 0.9f && !oldRightTriggerHeld && !inCreditsPanel)
        {
            oldRightTriggerHeld = true;
            menuSelection++;
            if (menuSelection > 2)
            {
                menuSelection = 0;
            }
            ResetAllLights();
            lights[menuSelection].enabled = true;

            playChick();
        }

        if (Input.GetAxisRaw("Horizontal_Player1") <= -0.9f && !OldLeftTriggerHeld && !inCreditsPanel)
        {
            OldLeftTriggerHeld = true;
            menuSelection--;
            if (menuSelection < 0)
            {
                menuSelection = 2;
            }
            ResetAllLights();
            lights[menuSelection].enabled = true;

            playChick();
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

            playChick();
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

    public void ResetAllLights()
    {
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }

    private void playChick() {
        source.clip = chickClip;
        source.Play();
    }
}