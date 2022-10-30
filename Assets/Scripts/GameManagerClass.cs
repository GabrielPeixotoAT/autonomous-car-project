using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerClass : MonoBehaviour
{
    public GameObject pauseMenu;

    public static bool isPaused;

    GameObject pauseMenuClone;

    void Start() {
        Time.timeScale = 1;
    }

    void Update()
    {
        VerifyPauseAction();
    }

    void VerifyPauseAction()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            pauseMenuClone = (GameObject) Instantiate(pauseMenu, gameObject.transform);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Destroy(pauseMenuClone);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isPaused = false;
        }
    }
}
