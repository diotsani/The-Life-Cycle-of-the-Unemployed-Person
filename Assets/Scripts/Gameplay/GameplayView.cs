using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Global;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [Header("Button")]
        [SerializeField] private Button _retryButton;
        [Header("Dependencies")]
        PlayerStatusData _playerStatusData;
        [SerializeField] DayManager _dayManager;
        
        [Header("Begin Display")]
        [SerializeField] private CanvasGroup _beginDisplay;
        [SerializeField] Image _beginPanel;
        [SerializeField] TMP_Text _beginText;
        
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
        
        [Header("Status Float Display")]
        [SerializeField] private GameObject _statusFloatHeader;
        [SerializeField] private Vector2 _statusPosition;
        [SerializeField] private Vector2 _statusSpacing;
        [SerializeField] private TMP_Text _statusFloatText;
        [SerializeField] private float _statusFloatDuration;
        [SerializeField] private Ease _statusFloatEase;
        private List<TMP_Text> _statusFloatTexts = new List<TMP_Text>();
        private int _amountStatus = 5;

        [Header("End Game Display")]
        [SerializeField] private Image _endGamePanel;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _descriptionText;

        private void OnEnable()
        {
            DayManager.OnShowDay += ShowDay;
            BaseInteraction.OnShowMonologue += ShowMonolog;
            PlayerStatusData.OnStatusChange += ShowStatus;
            GameplayFlow.OnShowEndGame += ShowEndPanel;
            GameplayFlow.OnBeginGame += ShowBegin;
        }

        private void OnDisable()
        {
            DayManager.OnShowDay -= ShowDay;
            BaseInteraction.OnShowMonologue -= ShowMonolog;
            PlayerStatusData.OnStatusChange -= ShowStatus;
            GameplayFlow.OnShowEndGame -= ShowEndPanel;
            GameplayFlow.OnBeginGame -= ShowBegin;
        }

        private void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
            _beginPanel.GetComponent<Button>().onClick.AddListener(ClickBegin);
            _retryButton.onClick.AddListener(ResetGameplay);
            _endGamePanel.GetComponent<Button>().onClick.AddListener(ResetGameplay);
            InitStatusFloat();
            
            _positivePosition = new Vector2(0,_monologPanel.rectTransform.anchoredPosition.y);
            _negativePosition = _positivePosition * -1;
            _monologPanel.rectTransform.anchoredPosition = _negativePosition;
        }

        private void Update()
        {
            if (_isShowingMonolog)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    UnShowMonolog();
                }
            }
        }

        void ResetGameplay()
        {
            _endGamePanel.DOFade(0, 0.5f).From(0)
                .OnComplete(()=>_endGamePanel.gameObject.SetActive(false));
            _playerStatusData.ResetStatus();
            SceneManager.LoadScene("TestGameplay");
        }

        private void ShowBegin()
        {
            // _beginPanel.gameObject.SetActive(true);
            // _beginPanel.DOFade(1, 0.5f).From(0);
            
            _beginDisplay.gameObject.SetActive(true);
            _beginDisplay.DOFade(1, 0.5f).From(0);
        }
        private void ClickBegin()
        {
            // _beginPanel.DOFade(0, 0.5f)
            //     .OnComplete(() => _beginPanel.gameObject.SetActive(false));
            // _beginText.DOFade(0, 0.5f);
            
             _beginDisplay.DOFade(0, 0.5f)
               .OnComplete(() => _beginDisplay.gameObject.SetActive(false));

             _dayManager.ChangeDay(0.5f);
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
            _monologText.text = monolog;
            _monologPanel.gameObject.SetActive(true);
            _monologPanel.rectTransform.DOAnchorPos(_positivePosition, _monologDuration).SetEase(_monologEase);
        }
        private void UnShowMonolog()
        {
            //Vector2 negativePosition = new Vector2(0, _monologPanel.rectTransform.anchoredPosition.y*-1);
            _monologPanel.rectTransform.DOAnchorPos(_negativePosition, _monologDuration)
                .OnComplete(_playerStatusData.NewDay);
            _isShowingMonolog = false;
        }
        private void ShowEndPanel(string title, string description)
        {
            _titleText.text = title;
            _descriptionText.text = description;
            _endGamePanel.gameObject.SetActive(true);
            _endGamePanel.DOFade(1, 0.5f).From(0);
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
                _dayGroup.DOFade(1, delay).From(0);
            }
            else
            {
                _dayText.text = Constants.Status.Day + value.ToString();
                _dayPanel.gameObject.SetActive(true);
                _dayGroup.alpha = 1;
                _dayText.DOFade(1, delay).From(0).SetDelay(delay);
            }
            yield return new WaitForSeconds(1 + delay);
            _playerStatusData.ChangeStatus();
            _dayGroup.DOFade(0, delay)
                .OnComplete(() => _dayPanel.gameObject.SetActive(false));
            //_dayText.DOFade(0, 0.5f);
        }
    }
}