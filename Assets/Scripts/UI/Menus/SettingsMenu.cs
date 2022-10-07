using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    bool fullScreen = true;

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
        fullScreen = !fullScreen;
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
