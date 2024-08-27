using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public TMP_Dropdown Resolutions;

    Resolution[] resolutions;

    private void Start()
    {
        Screen.SetResolution(800, 1000, FullScreenMode.MaximizedWindow);
        /*
        int currentResIndex = 0;

        resolutions = Screen.resolutions;

        Resolutions.ClearOptions();

        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = Screen.width + " x " + Screen.height;
            options.Add(option);

            if ((Screen.width == Screen.currentResolution.width) && (Screen.height == Screen.currentResolution.height))
            {
                currentResIndex = i;
            }
        }
        Resolutions.AddOptions(options);
        Resolutions.value = currentResIndex;
        Resolutions.RefreshShownValue();
        */
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetRes(int Res)
    {
        Resolution Resolution = resolutions[Res];

        Screen.SetResolution(800, 1000, FullScreenMode.MaximizedWindow);
    }
}