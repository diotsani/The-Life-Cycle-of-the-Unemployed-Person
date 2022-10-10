using System;
using System.Collections;
using System.Collections.Generic;
using Team8.Unemployment.Database;
using Team8.Unemployment.Global;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Team8.Unemployment.Gameplay
{
    public enum InteractionState
    {
        Good,
        Damaged
    }
    public abstract class BaseInteraction : MonoBehaviour,IInteractable
    {
        public delegate void EventParameter(string monologue);
        public static event EventParameter OnShowMonologue;
        
        [Header("Dependencies")]
        [SerializeField] protected InteractionController _interactionController;
        protected PlayerStatusData _playerStatusData;
        [SerializeField] protected string _interactionName;
        
        [Header("Decision")]
        protected DecisionScriptable _decisionScriptable;
        protected Decision _decisionPrefab;
        protected List<Decision> _decisionList;
        [SerializeField] protected GameObject _decisionParent;
        
        [Header("Config")]
        [SerializeField] protected int _amountClicked;
        [SerializeField] protected int _maxClicked;
        [SerializeField] protected int _durabilityDay;
        protected int _maxDurability = 3;
        
        [Header("Position")]
        [SerializeField] protected Transform _objectPosition;
        protected Vector3 _position = new Vector3(-0.3f,0.3f,0f);
        
        [Header("Condition")]
        [SerializeField] protected InteractionState _interactionState;
        public bool isInteracted;
        public bool isClicked;
        [SerializeField] protected bool _isDamaged;

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
            RequirementDecision(_decisionList);
            ClickSpecificDecision(_decisionList);
        }

        protected void Update()
        {
            Vector3 parentPosition = Camera.main.WorldToScreenPoint(_objectPosition.position+ _position);
            _decisionParent.transform.position = parentPosition;

            if (isClicked)
            {
                RequirementDecision(_decisionList);
                CheckCondition();
                isClicked = false;
            }
        }

        private void InitDecision()
        {
            Vector3 parentPosition = Camera.main.WorldToScreenPoint(_objectPosition.position+ _position);
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
            //_decisionParent.SetActive(false); // dont delete this line
        }
        public void AddAmountClick()
        {
            _amountClicked++;
        }
        public void ResetAmountClick()
        {
            _amountClicked = 0;
        }
        public void AddDurability()
        {
            _durabilityDay++;
        }
        public void ResetDurability()
        {
            _durabilityDay = 0;
        }
        protected virtual void ClickSpecificDecision(List<Decision> decisionList)
        {
            for (int i = 0; i < decisionList.Count; i++)
            {
                Decision decision = decisionList[i];
                decision.LockButton().onClick.AddListener(()=> ShowMonologue(Constants.Monologue.LockRepairMonolog));
                decision.DecisionButton().onClick.AddListener(()=> SpecificDecision(decision));
            }
        }
        protected abstract void SpecificDecision(Decision decision);
        protected abstract void RequirementDecision(List<Decision> decisionList);

        protected virtual void CheckCondition()
        {
            //Check Condition for interaction state in Laptop, Handphone, Refrigerator
        }
        protected virtual void DamageInteraction()
        {
            //Damage Interaction in Laptop, Handphone, Refrigerator
        }
        protected virtual void ShowMonologue(string monologue)
        {
            OnShowMonologue?.Invoke(monologue);
        }

        protected virtual void ResetDecision()
        {
            foreach (Decision obj in _decisionList)
            {
                obj.DecisionObject().SetActive(true);
                obj.DecisionButton().interactable = true;
            }
        }
        protected virtual void SetRepairDecision(int value)
        {
            foreach (Decision obj in _decisionList)
            {
                if (obj.DecisionText() == Constants.Requirments.Repair)
                {
                    bool set = _playerStatusData.money >= value;
                    obj.LockButton().gameObject.SetActive(!set);
                    obj.DecisionObject().SetActive(_isDamaged);
                }
            }
        }
        public void DeactivateDecision()
        {
            foreach (Decision obj in _decisionList)
            {
                obj.DecisionButton().interactable = false;
            }
        }
        public void RandomMaxClick(int min, int max)
        {
            _maxClicked = Random.Range(min, max);
        }

        public string GetName()
        {
            return _interactionName;
        }
        public GameObject DecisionParent()
        {
            return _decisionParent;
        }
        public void OnInteraction(bool status)
        {
            _decisionParent.SetActive(status);
        }
        public Vector3 TargetPostision()
        {
            return _objectPosition.position;
        }
    }
}