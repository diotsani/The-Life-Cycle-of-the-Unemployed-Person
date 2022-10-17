using System;
using System.Collections;
using Team8.Unemployment.Database;
using Team8.Unemployment.Player;
using Team8.Unemployment.Utility;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team8.Unemployment.Global
{
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    #region Background Music Variabel

    [Header("Background Music")] 
    public bool isPlay;
    public AudioSource sourceBGM;
    public Sound[] BGM;

    private GameObject _popUpBGM;
    private Animator _animBGM;
    private TMP_Text _nameBGM;

    #endregion

    #region Sound Effect Variabel

    [Space(20)] [Header("Sound Effect")]
    public AudioSource sourceSFX;
    public Sound[] SFX;

    #endregion
    
    private void Start()
    {
        PlayBGM();
        SetPlayerPrefsToogleUI();
    }

    #region BGM Method

    public void PlayBGM()
    {
        isPlay = true;
        
        _popUpBGM = GameObject.Find("PopUpBGM");
        _animBGM = _popUpBGM.GetComponent<Animator>();
        _nameBGM = _popUpBGM.GetComponentInChildren<TMP_Text>();

        StartCoroutine(ListBGM());

        IEnumerator ListBGM()
        {
            yield return null;

            while (isPlay)
            {
                Debug.Log("Music Play");
                int random = Random.Range(0, BGM.Length);
                _nameBGM.text = BGM[random].name;
                _animBGM.SetTrigger("PopUp");
                
                yield return new WaitForSeconds(1f);
                
                sourceBGM.clip = BGM[random].clip;
                sourceBGM.volume = BGM[random].volume;
                sourceBGM.pitch = BGM[random].pitch;
                sourceBGM.Play();
                yield return new WaitForSecondsRealtime(sourceBGM.clip.length);
                
                // for (int i = 0; i < BGM.Length; i++)
                // {
                //     _nameBGM.text = BGM[i].name;
                //     _animBGM.SetTrigger("PopUp");
                //
                //     yield return new WaitForSeconds(3f);
                //
                //     sourceBGM.clip = BGM[i].clip;
                //     sourceBGM.volume = BGM[i].volume;
                //     sourceBGM.pitch = BGM[i].pitch;
                //
                //     sourceBGM.Play();
                //
                //     while (sourceBGM.isPlaying) yield return null;
                // }
            }
            while (!isPlay)
            {
                Debug.Log("Music Stop");
            }
        }
    }

    #endregion

    #region SFX Method

    public void PlaySFX(string name)
    {
        Sound sound = Array.Find(SFX, sfx => sfx.name == name);

        if (sound == null)
        {
            Debug.LogWarning($"SFX: <color=red> {name} </color> not found!");
        }

        sourceSFX.volume = sound.volume;
        sourceSFX.pitch = sound.pitch;

        sourceSFX.PlayOneShot(sound.clip);
    }

    #endregion

    #region Toogle UI Method

    void SetPlayerPrefsToogleUI()
    {
        PlayerPrefs.SetString("BGM", sourceBGM.mute.ToString());
        PlayerPrefs.SetString("SFX", sourceSFX.mute.ToString());
    }

    public void ToogleBGM(TMP_Text _textUIBGM)
    {
        sourceBGM.mute = !sourceBGM.mute;
        _textUIBGM.fontStyle = sourceBGM.mute ? FontStyles.Strikethrough : FontStyles.Normal;
        PlayerPrefs.SetString("BGM", sourceBGM.mute.ToString());
    }

    public void ToogleSFX(TMP_Text _textUISFX)
    {
        sourceSFX.mute = !sourceSFX.mute;
        _textUISFX.fontStyle = sourceSFX.mute ? FontStyles.Strikethrough : FontStyles.Normal;
        PlayerPrefs.SetString("SFX", sourceSFX.mute.ToString());
    }

    #endregion
}
}

