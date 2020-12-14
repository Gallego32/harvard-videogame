using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicsMenu : MonoBehaviour
{
    public List<Button> buttons;

    public ColorBlock notSelected;

    public GameObject fullScreenButton;

    void Start()
    {
        int quality;
        Button selectedButton;
        quality = QualitySettings.GetQualityLevel();

        if (quality == 2) selectedButton = buttons[1];
        else if (quality == 1) selectedButton = buttons[2];
        else selectedButton = buttons[0];
        
        SelectButton(selectedButton);
    }

    void Update()
    {
        if (Screen.fullScreen)
            fullScreenButton.SetActive(true);
        else
            fullScreenButton.SetActive(false);
    }

    public void SelectButton(Button button)
    {
        button.colors = ColorBlock.defaultColorBlock;

        foreach(Button btn in buttons)
            if (btn != button)
                btn.colors = notSelected;
    }
    
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
}
