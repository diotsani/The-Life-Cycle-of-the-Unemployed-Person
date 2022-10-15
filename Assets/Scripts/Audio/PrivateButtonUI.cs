using System;
using UnityEngine;
using TMPro;

public class PrivateButtonUI : MonoBehaviour
{
    public TMP_Text textBGM, textSFX;

    private void Start()
    {
        CheckPlayerPrefsButton();
    }

    #region SetToogleUI

    void CheckPlayerPrefsButton()
    {
        textBGM.fontStyle = Boolean.Parse(PlayerPrefs.GetString("BGM")) ? FontStyles.Strikethrough : FontStyles.Normal;
        textSFX.fontStyle = Boolean.Parse(PlayerPrefs.GetString("SFX")) ? FontStyles.Strikethrough : FontStyles.Normal;
    }
    
    public void ToogleBGMUI()
    {
        textBGM.fontStyle = Boolean.Parse(PlayerPrefs.GetString("BGM")) ? FontStyles.Strikethrough : FontStyles.Normal;
        AudioManager.instance.ToogleBGM(textBGM);
    }
    
    public void ToogleSFXUI()
    {
        textSFX.fontStyle = Boolean.Parse(PlayerPrefs.GetString("SFX")) ? FontStyles.Strikethrough : FontStyles.Normal;
        AudioManager.instance.ToogleSFX(textSFX);
    }

    #endregion
}
