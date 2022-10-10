using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class DoorInteraction : BaseInteraction
    {
        public delegate void EventName();
        public static event EventName OnFreshFood;
        protected override void Start()
        {
            _interactionName = Constants.Name.Door;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Door);
            base.Start();
        }
        protected override void RequirementDecision(List<Decision> decisionList)
        {
            for (int i = 0; i < decisionList.Count; i++)
            {
                var obj = decisionList[i];
            
                if (obj.DecisionText() == Constants.Requirments.BuyFoodStock)
                {
                    bool set = _playerStatusData.money >= 20;
                    obj.DecisionButton().interactable = set;
                }
            }
        }

        protected override void SpecificDecision(Decision decision)
        {
            if(decision.DecisionText() == Constants.Requirments.Jogging)
            {
                ShowMonologue(Constants.Monologue.JoggingMonolog);
            }
            if (decision.DecisionText() == Constants.Requirments.BuyFoodStock)
            {
                ShowMonologue(Constants.Monologue.BuyFoodMonolog);
                //_interactionController.ResetAllDurability();
                //_playerStatusData.isFresh = true;
                
                OnFreshFood?.Invoke();
            }
        }
    }
}