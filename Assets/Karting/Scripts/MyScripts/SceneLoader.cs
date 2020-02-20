using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// for this simple game
// 0 corresponds to main menu index
// 1 corresponds to race index
public class SceneLoader : MonoBehaviour
{
    // inspector
    [Tooltip("Wait time since the button is pressed until begin scene load starts.")]
    [SerializeField] float buttonWaitTime = 0.5f;
    [Tooltip("Button sound audio clip.")]
    [SerializeField] AudioClip buttonSound = null;
    [Tooltip("Button sound volume.")]
    [SerializeField] [Range(0,1)] float buttonSoundVolume = 1f;

    // refs
    enum buttonAction { LoadMainMenu, LoadGame, Exit};

    // ---------------------------

    // button methods 
    public void LoadMainMenu()
    {
        StartCoroutine(SceneLoadRoutine(buttonAction.LoadMainMenu));
    }

    public void StartGame()
    {
        StartCoroutine(SceneLoadRoutine(buttonAction.LoadGame));
    }

    public void Exit()
    {
        StartCoroutine(SceneLoadRoutine(buttonAction.Exit));
    }

    // ---------------------------

    // all routines play button sound and wait some time
    // then they do an action depending on the calling method
    IEnumerator SceneLoadRoutine(buttonAction action)
    {
        // play button sound
        AudioSource.PlayClipAtPoint(buttonSound, Camera.main.transform.position, buttonSoundVolume);

        yield return new WaitForSeconds(buttonWaitTime);
        switch (action)
        {
            case buttonAction.LoadMainMenu:
            {
                SceneManager.LoadScene(0);
                break;
            }
            case buttonAction.LoadGame:
            {
                SceneManager.LoadScene(1);
                break;
            }
            case buttonAction.Exit:
            {
                Application.Quit();
                break;
            }
        }
    }
}
