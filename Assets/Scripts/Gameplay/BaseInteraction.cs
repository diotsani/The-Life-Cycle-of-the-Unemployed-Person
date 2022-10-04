using System;
using System.Collections;
using System.Collections.Generic;
using Team8.Unemployment.Database;
using Team8.Unemployment.Global;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public enum InteractionState
    {
        Good,
        Damaged
    }
    public abstract class BaseInteraction : MonoBehaviour,IInteractable
    {
        public delegate void EventName(string monologue);
        public static event EventName OnShowMonologue;
        
        protected PlayerStatusData _playerStatusData;
        [SerializeField] protected string _interactionName;
        
        [Header("Decision")]
        protected DecisionScriptable _decisionScriptable;
        protected Decision _decisionPrefab;
        protected List<Decision> _decisionList;
        [SerializeField] protected GameObject _decisionParent;
        
        [Header("Config")]
        [SerializeField] protected int _amountClicked;
        [SerializeField] protected int _dailyClicks;
        [SerializeField] protected int _consecutiveDay;

        protected virtual void OnEnable()
        {
            
        }

        protected virtual void OnDisable()
        {
            
        }

        protected virtual void Start()
        {
            _playerStatusData = PlayerStatusData.Instance;
            _decisionList = new List<Decision>();
            _decisionPrefab = Resources.Load<Decision>("Prefabs/DecisionButton");
            InitDecision();
            ClickSpecificDecision(_decisionList);
        }

        private void InitDecision()
        {
            Vector3 pos = new Vector3(0,1,0);
            Vector3 parentPosition = Camera.main.WorldToScreenPoint(transform.position+ pos);
            _decisionParent.transform.position = parentPosition;
            
            for (int i = 0; i < _decisionScriptable.decisionList.Count; i++)
            {
                int index = i;
                string getName = _decisionScriptable.decisionList[index].decisionName;
                int getSkill = _decisionScriptable.decisionList[index].skillCost;
                int getStress = _decisionScriptable.decisionList[index].stressCost;
                int getHealth = _decisionScriptable.decisionList[index].healthCost;
                int getMoney = _decisionScriptable.decisionList[index].moneyCost;
                int getAction = _decisionScriptable.decisionList[index].actionCost;
                int getBook = _decisionScriptable.decisionList[index].bookCost;
                int getFood = _decisionScriptable.decisionList[index].foodCost;
                
                Decision decision = Instantiate(_decisionPrefab, _decisionParent.transform);
                _decisionList.Add(decision);
                decision.Init(getName, getSkill, getStress, getHealth, getMoney, getAction, getBook, getFood);
                decision.OnClick(decision,this,_playerStatusData);
            }
        }
        public void AddClick()
        {
            _amountClicked++;
            _dailyClicks++;
        }
        public void ResetAmountClick()
        {
            _amountClicked = 0;
        }
        public void ResetDailyClicks()
        {
            _dailyClicks = 0;
        }
        public void ResetConsecutiveDay()
        {
            _consecutiveDay = 0;
        }
        protected virtual void ClickSpecificDecision(List<Decision> decisionList)
        {
            for (int i = 0; i < decisionList.Count; i++)
            {
                Decision decision = decisionList[i];
                decision.DecisionButton().onClick.AddListener(()=> SpecificDecision(decision));
            }
        }
        protected abstract void SpecificDecision(Decision decision);
        protected abstract void RequirmentDecision(List<Decision> decisionList);
        protected virtual void ShowMonologue(string monologue)
        {
            OnShowMonologue?.Invoke(monologue);
        }
        public void DeactivateDecision()
        {
            foreach (Decision objs in _decisionList)
            {
                objs.DecisionButton().interactable = false;
            }
        }
        public void OnInteraction(bool status)
        {
            _decisionParent.SetActive(status);
        }
    }
}