using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public void OnClickButtonSFX(string nameSFX)
    {
        AudioManager.instance.PlaySFX(nameSFX);
    }
}
