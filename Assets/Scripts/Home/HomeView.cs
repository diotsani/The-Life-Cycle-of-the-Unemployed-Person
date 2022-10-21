using System;
using DG.Tweening;
using Team8.Unemployment.Global;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team8.Unemployment.Home
{
    public class HomeView : MonoBehaviour
    {
        [Header("Home Button")]
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _creditButton;
        [SerializeField] private Button _quitButton;
        
        [Header("Home Display")]
        [SerializeField] private CanvasGroup _logoPanel;
        [SerializeField] private CanvasGroup _buttonPanel;
        
        [Header("Credit Display")]
        [SerializeField] private CanvasGroup _creditPanel;
        [SerializeField] private Button _exitCreditButton;

        [Header("Day")] 
        [SerializeField,Range(0,24)] private float _currentTime;

        [SerializeField] private GameObject[] _dayCycle;
        [SerializeField] private GameObject[] _spotlight;

        private void Start()
        {
            Time.timeScale = 1;
            _newGameButton.onClick.RemoveAllListeners();
            _creditButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
            _exitCreditButton.onClick.RemoveAllListeners();
            
            _newGameButton.onClick.AddListener(OnNewGame);
            _creditButton.onClick.AddListener(OnCredit);
            _quitButton.onClick.AddListener(OnQuit);
            _exitCreditButton.onClick.AddListener(OnExitCredit);
        }

        private void Update()
        {
            DaySystem();
        }

        private void OnNewGame()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            SceneManager.LoadScene("TestGameplay");
        }
        private void OnCredit()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            _creditPanel.gameObject.SetActive(true);

            _creditPanel.DOFade(1, 0.5f);
            _logoPanel.DOFade(0, 0.5f).From(1).OnComplete((() => _logoPanel.gameObject.SetActive(false)));
            _buttonPanel.DOFade(0, 0.5f).From(1).OnComplete((() => _buttonPanel.gameObject.SetActive(false)));
            
        }
        private void OnExitCredit()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            
            _creditPanel.DOFade(0, 0.5f).From(1).OnComplete((() => _creditPanel.gameObject.SetActive(false)));
            
            _logoPanel.gameObject.SetActive(true);
            _buttonPanel.gameObject.SetActive(true);
            _logoPanel.DOFade(1, 0.5f).From(0);
            _buttonPanel.DOFade(1, 0.5f).From(0);
        }
        private void OnQuit()
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            Application.Quit();
        }

        private void DaySystem()
        {
            _currentTime = DateTime.Now.Hour;
            if (_currentTime > 5 && _currentTime < 9)
            {
                //Morning
                SetDayCycle(Constants.DaySystem.Morning,Constants.DaySystem.MorningSpotlight);
            }
            else if (_currentTime > 9 && _currentTime < 14)
            {
                //Noon
                SetDayCycle(Constants.DaySystem.Noon,Constants.DaySystem.MorningSpotlight);
            }
            else if (_currentTime > 14 && _currentTime < 18)
            {
                //Afternoon
                SetDayCycle(Constants.DaySystem.Afternoon,Constants.DaySystem.AfternoonSpotlight);
            }
            else if (_currentTime > 0 && _currentTime < 5 || _currentTime > 18)
            {
                //Night
                SetDayCycle(Constants.DaySystem.Night,Constants.DaySystem.NightSpotlight);
            }
        }

        private void SetDayCycle(string dayName, string spotlightName)
        {
            foreach (GameObject day in _dayCycle)
            {
                day.SetActive(false);
                if (day.name == dayName)
                {
                    day.SetActive(true);
                }
            }

            foreach (GameObject spot in _spotlight)
            {
                spot.SetActive(false);
                if (spot.name == spotlightName)
                {
                    spot.SetActive(true);
                }
            }
        }
    }
}