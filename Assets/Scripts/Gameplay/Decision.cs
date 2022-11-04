using Team8.Unemployment.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Team8.Unemployment.Gameplay
{
    public class Decision : MonoBehaviour
    {
        public delegate void EventName();
        public static event EventName OnClickDecision;
        public delegate void EventInteracted(bool isClick);
        public static event EventInteracted OnClickInteracted;
        
        [Header("Button")]
        [SerializeField] private string decisionText;
        [SerializeField] private Button _decisionButton;
        [SerializeField] private Button _lockButton;
        [SerializeField] private TMP_Text _buttonText;

        [Header("Config")]
        private int _skillCost;
        private int _stressCost;
        private int _healthCost;
        public int _moneyCost { get; private set; }
        private int _actionCost;
        private int _bookCost;
        public int _foodCost { get; private set; }

        public void Init(string getName,int getSkill, int getStress, int getHealth, int getMoney, int getAction, int getBook, int getFood)
        {
            this.name = getName;
            decisionText = getName;
            _buttonText.text = getName;
            
            _skillCost = getSkill;
            _stressCost = getStress;
            _healthCost = getHealth;
            _moneyCost = getMoney;
            _actionCost = getAction;
            _bookCost = getBook;
            _foodCost = getFood;
        }

        public void OnClick(Decision getDecision, BaseInteraction getBase, PlayerStatusData getPlayer)
        {
            getDecision.DecisionButton().onClick.AddListener(()=>DecisionChoose(getBase,getPlayer));
            _lockButton.onClick.AddListener(SetLockButton);
        }

        private void DecisionChoose(BaseInteraction getBase,PlayerStatusData getPlayer)
        {
            AudioManager.Instance.PlaySFX("ButtonClick");
            OnClickDecision?.Invoke();
            OnClickInteracted?.Invoke(false);
            
            getPlayer.SkillCost(_skillCost);
            getPlayer.StressCost(_stressCost);
            getPlayer.HealthCost(_healthCost);
            getPlayer.MoneyCost(_moneyCost);
            getPlayer.ActionCost(_actionCost);
            getPlayer.BookCost(_bookCost);
            getPlayer.FoodCost(_foodCost);
            
            getBase.DecisionParent().SetActive(false);
            getBase.AddAmountClick();
        }
        public string DecisionText()
        {
            return decisionText;
        }
        public GameObject DecisionObject()
        {
            return this.gameObject;
        }
        public Button DecisionButton()
        {
            return _decisionButton;
        }
        public Button LockButton()
        {
            return _lockButton;
        }
        void SetLockButton()
        {
            Debug.Log(name + " is locked");
        }
        public int FoodCost()
        {
            return _foodCost;
        }
    }
}