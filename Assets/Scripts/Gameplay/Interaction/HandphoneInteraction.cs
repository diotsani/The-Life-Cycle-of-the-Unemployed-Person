using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class HandphoneInteraction : BaseInteraction
    {
        private int _priceRepair = 25;
        private int _minRandom = 6;
        private int _maxRandom = 9;
        protected override void Start()
        {
            _interactionName = Constants.Name.Handphone;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Handphone);
            RandomMaxClick(_minRandom, _maxRandom);
            base.Start();
        }
        protected override void SpecificDecision(Decision decision)
        {
            if (decision.DecisionText() == Constants.Requirments.SocialMedia)
            {
                if (_playerStatusData.skill > 50)
                {
                    ShowMonologue(Constants.Monologue.SocialMediaMonolog_1);
                }
                else
                {
                    ShowMonologue(Constants.Monologue.SocialMediaMonolog_2);
                }
                
                ShowHistory(Constants.History.SocialMedia);
            }
            if (decision.DecisionText() == Constants.Requirments.Sell)
            {
                ShowMonologue(Constants.Monologue.SellHandphoneMonolog);
                ShowFeedback(Constants.Feedback.SellHandphoneFeedback);
                ShowHistory(Constants.History.SellHandphone);

                DecisionParent().SetActive(false);
                this.gameObject.SetActive(false);
            }
            if (decision.DecisionText() == Constants.Requirments.Repair)
            {
                ShowMonologue(Constants.Monologue.RepairHandphoneMonolog);
                ShowFeedback(Constants.Feedback.RepairHandphoneFeedback);
                ShowHistory(Constants.History.RepairHandphone);
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
                Decision obj = decisionList[i];
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