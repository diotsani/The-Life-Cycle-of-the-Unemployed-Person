using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class HandphoneInteraction : BaseInteraction
    {
        protected override void Start()
        {
            _interactionName = Constants.Name.Handphone;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Handphone);
            base.Start();
        }
        protected override void SpecificDecision(Decision decision)
        {
            
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            
        }
    }
}