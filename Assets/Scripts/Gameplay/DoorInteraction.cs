using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class DoorInteraction : BaseInteraction
    {
        protected override void Start()
        {
            _interactionName = Constants.Name.Door;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Door);
            base.Start();
        }
        protected override void RequirementDecision(List<Decision> decisionList)
        {
            
        }

        protected override void SpecificDecision(Decision decision)
        {
            if (decision.DecisionText() == Constants.Requirments.BuyFood)
            {
                ShowMonologue(Constants.Monologue.BuyFoodMonolog);
                _interactionController.ResetAllDurability();
                _playerStatusData.isFresh = true;
            }
        }
    }
}