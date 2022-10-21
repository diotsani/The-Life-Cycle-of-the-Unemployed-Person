using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class RefrigeratorInteraction : BaseInteraction
    {
        [SerializeField] private GameObject _refrigeratorDoor;
        [SerializeField] private Vector3 _openDoor;
        [SerializeField] private Vector3 _closeDoor;
        protected override void OnEnable()
        {
            base.OnEnable();
            DoorInteraction.OnFreshFood += GoodInteraction;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DoorInteraction.OnFreshFood -= GoodInteraction;
        }

        protected override void Start()
        {
            _interactionName = Constants.Name.Refrigerator;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Refrigerator);
            base.Start();
            
            //_dmgParticle = Instantiate(_damagedParticle, _objectPosition);
            //_dmgParticle.gameObject.SetActive(false);
        }

        protected override void Update()
    {
        base.Update();
        if(_isDamaged)return;
        if(_durabilityDay >= _maxDurability)
        {
            CheckCondition();
        }
        
        // if (_isDamaged)
        // {
        //     _dmgParticle.gameObject.SetActive(true);
        // }
        // else
        // {
        //     _dmgParticle.gameObject.SetActive(false);
        //     if(_durabilityDay >= _maxDurability)
        //     {
        //         CheckCondition();
        //     }
        // }
    }

        public override void OnEffect()
        {
            _refrigeratorDoor.transform.DORotate(_openDoor, 1f);
        }

        public override void OffEffect()
        {
            _refrigeratorDoor.transform.DORotate(_closeDoor, 1f);
        }

        protected override void SpecificDecision(Decision decision)
        {
            int rnd = Random.Range(0, 3);
            Debug.Log(rnd);
            
            if (decision.DecisionText() == Constants.Requirments.Eat)
            {
                ShowMonologue(Constants.Monologue.EatMonolog(rnd));
                ShowFeedback(Constants.Feedback.EatFeedback);
                ShowHistory(Constants.History.Eat);
            }
            if(decision.DecisionText() == Constants.Requirments.ThrowFood)
            {
                ShowMonologue(Constants.Monologue.ThrowFoodMonolog(rnd));
                ShowFeedback(Constants.Feedback.ThrowFoodFeedback);
                ShowHistory(Constants.History.ThrowFood);
                _playerStatusData.ResetFood();
                GoodInteraction();
            }

            if (decision.DecisionText() == Constants.Requirments.CheckFoodStock)
            {
                ShowMonologue(Constants.Monologue.FoodStockMonolog(rnd,_playerStatusData.food));
                ShowHistory(Constants.History.CheckFoodStock);
            }
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            if(_isDamaged)return;
            for (int i = 0; i < decisionList.Count; i++)
            {
                var obj = decisionList[i];
                if (obj.DecisionText() == Constants.Requirments.ThrowFood)
                {
                    bool set = _playerStatusData.isFresh;
                    obj.DecisionButton().interactable = !set;
                }
                if (obj.DecisionText() == Constants.Requirments.Eat)
                {
                    bool set = _playerStatusData.food >= 10;
                    obj.DecisionButton().interactable = set;
                }
            }
        }
        protected override void CheckCondition()
        {
            if (!_isDamaged)
            {
                if(_durabilityDay >= _maxDurability)
                {
                    _interactionState = InteractionState.Damaged;
                    DamageInteraction();
                }
            }
        }
        protected override void DamageInteraction()
        {
            _isDamaged = true;
            _playerStatusData.isFresh = false;
            foreach (Decision obj in _decisionList)
            {
                if (obj.DecisionText() == Constants.Requirments.ThrowFood)
                {
                    obj.DecisionButton().interactable = true;
                }
            }
        }
        protected void GoodInteraction()
        {
            ResetDecision();
            ResetDurability();
            _interactionState = InteractionState.Good;
            _playerStatusData.isFresh = true;
            _isDamaged = false;
        }
    }
}