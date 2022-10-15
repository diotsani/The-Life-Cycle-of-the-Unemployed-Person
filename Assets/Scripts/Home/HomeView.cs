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

        private void Start()
        {
            _newGameButton.onClick.AddListener(OnNewGame);
            _creditButton.onClick.AddListener(OnCredit);
            _quitButton.onClick.AddListener(OnQuit);
            _exitCreditButton.onClick.AddListener(OnExitCredit);
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
            
            _creditPanel.DOFade(1, 0.5f).From(0);
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
    }
}