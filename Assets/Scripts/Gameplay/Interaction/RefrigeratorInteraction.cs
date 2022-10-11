﻿using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class RefrigeratorInteraction : BaseInteraction
    {
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
        }

        protected override void SpecificDecision(Decision decision)
        {
            if (decision.DecisionText() == Constants.Requirments.Eat)
            {
                ShowMonologue(Constants.Monologue.EatMonolog);
                ShowHistory(Constants.History.Eat);
            }
            if(decision.DecisionText() == Constants.Requirments.ThrowFood)
            {
                ShowMonologue(Constants.Monologue.ThrowFoodMonolog);
                ShowHistory(Constants.History.ThrowFood);
                _playerStatusData.ResetFood();
                GoodInteraction();
            }

            if (decision.DecisionText() == Constants.Requirments.CheckFoodStock)
            {
                ShowMonologue(Constants.Monologue.FoodStockMonolog(_playerStatusData.food));
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