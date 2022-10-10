using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class LaptopInteraction : BaseInteraction
    {
        private int _priceRepair = 50;
        private int _minRandom = 3;
        private int _maxRandom = 6;
        protected override void Start()
        {
            _interactionName = Constants.Name.Laptop;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Laptop);
            RandomMaxClick(_minRandom, _maxRandom);
            base.Start();
        }
        protected override void SpecificDecision(Decision decision)
        {
            if (decision.DecisionText() == Constants.Requirments.ApplyJob)
            {
                ShowMonologue(Constants.Monologue.ApplyJobMonolog);
                _playerStatusData.isApplyJob = true;
                decision.DecisionObject().SetActive(!_playerStatusData.isApplyJob);
            }
            if (decision.DecisionText() == Constants.Requirments.TakeCourse)
            {
                ShowMonologue(Constants.Monologue.TakeCourseMonolog);
            }
            if (decision.DecisionText() == Constants.Requirments.PlayGame)
            {
                ShowMonologue(Constants.Monologue.PlayGameMonolog);
            }
            if (decision.DecisionText() == Constants.Requirments.CheckMail)
            {
                if(!_playerStatusData.isApplied)
                {
                    ShowMonologue(Constants.Monologue.CheckMailMonolog_1);
                }
                else if(_playerStatusData.isApplied && _playerStatusData.skill < 100)
                {
                    ShowMonologue(Constants.Monologue.CheckMailMonolog_2);
                }
            }
            if (decision.DecisionText() == Constants.Requirments.Repair)
            {
                ShowMonologue(Constants.Monologue.RepairLaptopMonolog);
                ResetDecision();
                ResetAmountClick();
                RandomMaxClick(_minRandom, _maxRandom);
                _interactionState = InteractionState.Good;
                _isDamaged = false;
            }
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            SetRepairDecision(_priceRepair);
            if(_isDamaged)return;
            for (int i = 0; i < decisionList.Count; i++)
            {
                var obj = decisionList[i];
                if (obj.DecisionText() == Constants.Requirments.ApplyJob)
                {
                    bool set = _playerStatusData.isApplyJob;
                    obj.DecisionObject().SetActive(!set);
                    obj.DecisionButton().interactable = !set;
                }
                if (obj.DecisionText() == Constants.Requirments.TakeCourse)
                {
                    bool set = _playerStatusData.stress < 50;
                    obj.DecisionButton().interactable = set;
                }
            }
        }
        protected override void CheckCondition()
        {
            if(_amountClicked >= _maxClicked)
            {
                _interactionState = InteractionState.Damaged;
                DamageInteraction();
            }
        }

        protected override void DamageInteraction()
        {
            _isDamaged = true;
            foreach (Decision obj in _decisionList)
            {
                obj.DecisionButton().interactable = false;
                if (obj.DecisionText() == Constants.Requirments.Repair)
                {
                    obj.DecisionObject().SetActive(true);
                    obj.DecisionButton().interactable = true;
                }
            }
        }
    }
}