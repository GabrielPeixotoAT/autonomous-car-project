using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    bool fullScreen;
    
    int current;

    public Dropdown resolutionInput, fullScreenInput, qualityInput;

    void Start()
    {
        fullScreen = Screen.fullScreen;

        current = Screen.width;

        switch (current)
        {
            case 800:
                resolutionInput.value = 0;
                break;
            case 1280:
                resolutionInput.value = 1;
                break;
            case 1366:
                resolutionInput.value = 2;
                break;
            case 1920:
                resolutionInput.value = 3;
                break;
        }

        if (fullScreen)
        {
            fullScreenInput.value = 0;
        }
        else
        {
            fullScreenInput.value = 1;
        }

        qualityInput.value = QualitySettings.GetQualityLevel();
    }

    public void SetScreenResolution(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(800, 600, fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280, 720, fullScreen);
                break;
            case 2:
                Screen.SetResolution(1366, 768, fullScreen);
                break;
            case 3:
                Screen.SetResolution(1920, 1080, fullScreen);
                break;
        }
    }

    public void SetFullScreen()
    {
        if (fullScreenInput.value == 0)
        {
            fullScreen = true;
        }
       else
       {
            fullScreen = false;
       }
        Screen.fullScreen = fullScreen;
    }

    public void SetGraphicQuality(int index)
    {
        QualitySettings.SetQualityLevel(index, true);
    }

    public void CloseMenu()
    {
        Destroy(gameObject);
    }
}
