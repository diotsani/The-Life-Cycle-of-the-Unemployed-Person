using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class WalletInteraction : BaseInteraction
    {
        protected override void Start()
        {
            _interactionName = Constants.Name.Wallet;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Wallet);
            base.Start();
        }
        protected override void SpecificDecision(Decision decision)
        {
            if (decision.DecisionText() == Constants.Requirments.CheckMoney)
            {
                ShowMonologue(Constants.Monologue.CheckMoneyMonolog(_playerStatusData.money));
            }
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            
        }
    }
}