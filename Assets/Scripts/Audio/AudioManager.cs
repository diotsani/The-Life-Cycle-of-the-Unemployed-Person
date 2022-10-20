using System;
using System.Collections;
using Team8.Unemployment.Database;
using Team8.Unemployment.Player;
using Team8.Unemployment.Utility;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace Team8.Unemployment.Global
{
    public class AudioManager : SingletonMonoBehaviour<AudioManager>
    {
    [Header("Background Music")] 
    public bool isPlay;
    public AudioSource sourceBGM;
    public Sound[] BGM;
        private string sceneName;
    [SerializeField] private TMP_Text _BgmHome;
    [SerializeField] private TMP_Text _BgmGameplay;
        [SerializeField] private float _delay;


    [Space(20)] [Header("Sound Effect")]
    public AudioSource sourceSFX;
    public Sound[] SFX;


    
        private void Start()
    {
        PlayBGM();
        SetPlayerPrefsToogleUI();
    }
        private void Update()
        {
            sceneName = SceneManager.GetActiveScene().name;
        }


        public void PlayBGM()
    {
        isPlay = true;
        

        StartCoroutine(ListBGM());

        IEnumerator ListBGM()
        {
            yield return null;

            while (isPlay)
            {
                Debug.Log("Music Play");
                int random = Random.Range(0, BGM.Length);

                    if(sceneName == Constants.Scene.Home)
                    {
                        _BgmHome.text = BGM[random].name;
                        _BgmHome.gameObject.SetActive(true);
                        _BgmHome.DOFade(1, _delay).From(0)
                            .OnComplete(()=>_BgmHome.DOFade(0,_delay))
                            .OnComplete(()=> _BgmHome.gameObject.SetActive(false));
                    }
                    else if(sceneName == Constants.Scene.Gameplay)
                    {
                        _BgmGameplay.text = BGM[random].name;
                        _BgmGameplay.gameObject.SetActive(true);
                        _BgmGameplay.DOFade(1, _delay).From(0)
                            .OnComplete(()=>_BgmGameplay.DOFade(0,_delay))
                            .OnComplete(() => _BgmGameplay.gameObject.SetActive(false));
                    }
                
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

    }
}

