using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Global;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [Header("Dependencies")]
        PlayerStatusData _playerStatusData;
        [SerializeField] DayManager _dayManager;
        [SerializeField] private GameplayFlow _gameplayFlow;
        
        [Header("Begin Display")]
        [SerializeField] private CanvasGroup _beginDisplay;
        [SerializeField] Image _beginPanel;
        [SerializeField] TMP_Text _beginDateText;
        [SerializeField] TMP_Text _beginClickText;

        [Header("HUD")]
        [SerializeField] private Image _skillBar;
        [SerializeField] private Image _healthBar;
        [SerializeField] private Image _stressBar;
        [SerializeField] private TMP_Text _actionText;
        [SerializeField] private TMP_Text _guideText;
        [SerializeField] private float _guideDelay;
        private float _maxProgress = 100;
        private float _barDelay = 1f;
        
        [Header("Pause Display")]
        [SerializeField] private CanvasGroup _pausePanel;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _quitButton;
        private bool _isCoolDownPause;
        private float _coolDownPauseTime = 0.6f;
        
        [Header("Day Display")]
        [SerializeField] private CanvasGroup _dayGroup;
        [SerializeField] private Image _dayPanel;
        [SerializeField] private TMP_Text _dayText;
        
        [Header("Monolog Display")]
        [SerializeField] private Image _monologPanel;
        [SerializeField] private TMP_Text _monologText;
        [SerializeField] private float _monologDuration;
        [SerializeField] private Ease _monologEase;
        private Vector2 _negativePosition;
        private Vector2 _positivePosition;
        private bool _isShowingMonolog;
        
        [Header("Feedback Display")]
        [SerializeField] private CanvasGroup _feedbackPanel;
        [SerializeField] private TMP_Text _feedbackText;
        [SerializeField] private float _feedbackDuration;
        [SerializeField] private float _feedbackDelay;
        [SerializeField] private Ease _feedbackEase;
        
        [Header("Status Float Display")]
        [SerializeField] private GameObject _statusFloatHeader;
        [SerializeField] private TMP_Text _statusFloatText;
        private List<TMP_Text> _statusFloatTexts = new List<TMP_Text>();
        private int _amountStatus = 5;

        [Header("End Game Display")]
        [SerializeField] private CanvasGroup _endGamePanel;
        [SerializeField] private TMP_Text _titleEndGameText;
        [SerializeField] private TMP_Text _descriptionEndGameText;

        private void OnEnable()
        {
            DayManager.OnShowDay += ShowDay;
            BaseInteraction.OnShowMonologue += ShowMonolog;
            BaseInteraction.OnShowFeedback += ShowFeedback;
            //PlayerStatusData.OnStatusChange += ShowStatus;
            GameplayFlow.OnShowEndGame += ShowEndPanel;
            GameplayFlow.OnBeginGame += ShowBegin;
        }

        private void OnDisable()
        {
            DayManager.OnShowDay -= ShowDay;
            BaseInteraction.OnShowMonologue -= ShowMonolog;
            BaseInteraction.OnShowFeedback -= ShowFeedback;
            //PlayerStatusData.OnStatusChange -= ShowStatus;
            GameplayFlow.OnShowEndGame -= ShowEndPanel;
            GameplayFlow.OnBeginGame -= ShowBegin;
        }

        private void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
            
            _beginPanel.GetComponent<Button>().onClick.AddListener(ClickBegin);
            _beginDateText.text = DateTime.Now.ToString("dd/MM/yyyy");
            
            _endGamePanel.GetComponent<Button>().onClick.AddListener(ResetGameplay);
            _endGamePanel.GetComponent<Button>().interactable = false;
            //InitStatusFloat();
            InitPause();
            
            _positivePosition = new Vector2(0,_monologPanel.rectTransform.anchoredPosition.y);
            _negativePosition = _positivePosition * -1;
            _monologPanel.rectTransform.anchoredPosition = _negativePosition;
        }

        private void Update()
        {
            ProgressBar();
            _actionText.text = Constants.Status.Action + _playerStatusData.action;
            
            if (_isShowingMonolog)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    UnShowMonolog();
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(_isCoolDownPause)return;
                if (Time.timeScale == 0)
                {
                    ShowPause("Resume");
                    StartCoroutine(DelayOnPause(_coolDownPauseTime));
                }
                else
                {
                    ShowPause("Pause");
                    StartCoroutine(DelayOnPause(_coolDownPauseTime));
                }
            }
        }
        void ProgressBar()
        {
            float currentSkill = _playerStatusData.skill;
            float currentHealth = _playerStatusData.health;
            float currentStress = _playerStatusData.stress;
            _skillBar.DOFillAmount(currentSkill / _maxProgress, _barDelay);
            _healthBar.DOFillAmount(currentHealth / _maxProgress,_barDelay);
            _stressBar.DOFillAmount(currentStress/_maxProgress, _barDelay);
        }

        void ResetGameplay()
        {
            _endGamePanel.DOFade(0, 0.5f).From(0)
                .OnComplete(()=>_endGamePanel.gameObject.SetActive(false));
            _playerStatusData.ResetStatus();
            SceneManager.LoadScene(Constants.Scene.Home);
        }

        void QuitGameplay()
        {
            _pausePanel.DOFade(0, 0.5f).From(0)
                .OnComplete(()=>_pausePanel.gameObject.SetActive(false));
           _playerStatusData.ResetStatus();
           SceneManager.LoadScene(Constants.Scene.Home);
        }

        private void ShowBegin()
        {
            _beginDisplay.gameObject.SetActive(true);
            _beginDisplay.DOFade(1, 0.5f).From(0);
        }
        private void ClickBegin()
        {
            _beginPanel.GetComponent<Button>().interactable = false;
            _beginDisplay.DOFade(0, 0.5f)
               .OnComplete(() => _beginDisplay.gameObject.SetActive(false));

             _dayManager.ChangeDay(0.5f);
        }

        private void InitPause()
        {
            _resumeButton.onClick.AddListener(()=> ShowPause("Resume"));
            _quitButton.onClick.AddListener(QuitGameplay);
        }
        private void ShowPause(string message)
        {
            if(message == "Pause")
            {
                Debug.Log(message);
                _pausePanel.gameObject.SetActive(true);
                _pausePanel.DOFade(1, 0.5f).From(0)
                    .OnComplete(()=>Time.timeScale = 0);
                _playerStatusData.isPlayGame = false;
            }
            else
            {
                Time.timeScale = 1;
                Debug.Log(message);
                _pausePanel.DOFade(0, 0.5f).From(1)
                    .OnComplete(() => _pausePanel.gameObject.SetActive(false));
                _playerStatusData.isPlayGame = true;
            }
        }
        private IEnumerator DelayOnPause(float delay)
        {
            _isCoolDownPause = true;
            yield return new WaitForSecondsRealtime(delay);
            _isCoolDownPause = false;
        }

        private void ShowDay(int value,float delay)
        {
            // _dayText.text = Constants.Status.Day + value.ToString();
            // _dayPanel.SetActive(true);
            StartCoroutine(ShowDayDelay(value,delay));
        }

        private void ShowMonolog(string monolog)
        {
            _playerStatusData.isNewDay = false;
            _isShowingMonolog = true;
            _gameplayFlow.isWaitMonolog = true;
            
            _monologText.text = monolog;
            _monologPanel.gameObject.SetActive(true);
            _monologPanel.rectTransform.DOAnchorPos(_positivePosition, _monologDuration).SetEase(_monologEase);
        }
        private void UnShowMonolog()
        {
            //Vector2 negativePosition = new Vector2(0, _monologPanel.rectTransform.anchoredPosition.y*-1);
            _playerStatusData.isNewDay = true;
            _monologPanel.rectTransform.DOAnchorPos(_negativePosition, _monologDuration);
                //.OnComplete(_playerStatusData.NewDay);
            _isShowingMonolog = false;
            _gameplayFlow.isWaitMonolog = false;
        }
        private void ShowFeedback(string feedback)
        {
            _feedbackText.text = feedback;
            _feedbackPanel.gameObject.SetActive(true);
            _feedbackPanel.DOFade(1, _feedbackDuration/2).From(0).SetDelay(_feedbackDelay).SetEase(_feedbackEase)
                .OnComplete(()=>_feedbackPanel.DOFade(0, _feedbackDuration/2).From(1).SetDelay(_feedbackDelay*2)
                    .OnComplete(()=>_feedbackPanel.gameObject.SetActive(false)));
        }
        private void ShowEndPanel(string title, string description)
        {
            _titleEndGameText.text = title;
            _descriptionEndGameText.text = description;
            _endGamePanel.gameObject.SetActive(true);
            _endGamePanel.DOFade(1, 1f).From(0).SetDelay(0.5f)
                .OnComplete(()=> SetEndGameButton());
        }
        public TMP_Text EndText()
        {
            return _descriptionEndGameText;
        }
        void SetEndGameButton()
        {
            _endGamePanel.GetComponent<Button>().interactable = true;
        }
        private void InitStatusFloat()
        {
            for (int i = 0; i < _amountStatus; i++)
            {
                TMP_Text text = Instantiate(_statusFloatText, _statusFloatHeader.transform);
                _statusFloatTexts.Add(text);
                text.gameObject.SetActive(false);
            }
        }
        private void ShowStatus(string name, int value)
        {
            TMP_Text text = GetStatusText();
            if(text != null)
            {
                text.gameObject.SetActive(true);
                text.text = $"{name} {value.ToString("+#;-#;0")}";
                text.transform.SetAsLastSibling();
                text.DOFade(1,1f)
                    .From(0)
                    .OnComplete(() => text.DOFade(0,0.5f).SetDelay(2f)
                            .OnComplete(() => text.gameObject.SetActive(false)));
                //StartCoroutine(AfterActive(text.gameObject, 3f));
            }
        }
        private TMP_Text GetStatusText()
        {
            foreach (var text in _statusFloatTexts)
            {
                if (!text.gameObject.activeInHierarchy)
                {
                    return text;
                }
            }
            return null;
        }
        private IEnumerator ShowDayDelay(int value,float delay)
        {
            if (value > 1)
            {
                _dayText.text = Constants.Status.Day + value.ToString();
                _dayPanel.gameObject.SetActive(true);
                _dayGroup.alpha = 1;
                _dayGroup.DOFade(1, delay).From(0).SetDelay(delay);
            }
            else
            {
                // First Day
                _guideText.gameObject.SetActive(true);
                _guideText.alpha = 0;
                _dayText.text = Constants.Status.Day + value.ToString();
                _dayPanel.gameObject.SetActive(true);
                _dayGroup.alpha = 1;
                _dayText.DOFade(1, delay).From(0).SetDelay(delay)
                    .OnComplete(()=>_guideText.DOFade(1,0.6f).SetDelay(_guideDelay)
                        .OnComplete(()=>_guideText.DOFade(0,0.6f).SetDelay(0.4f)
                            .OnComplete(()=>_guideText.gameObject.SetActive(false))));
            }
            yield return new WaitForSeconds(1 + delay);
            _playerStatusData.ChangeStatus();
            _dayGroup.DOFade(0, delay)
                .OnComplete(() => _dayPanel.gameObject.SetActive(false));
            yield return new WaitForSeconds(delay);
            _gameplayFlow.CheckEndGame();
            _playerStatusData.isPlayGame = true;
            
        }
    }
}