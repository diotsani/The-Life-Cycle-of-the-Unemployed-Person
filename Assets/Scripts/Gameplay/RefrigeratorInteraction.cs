using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class RefrigeratorInteraction : BaseInteraction
    {
        protected override void Start()
        {
            _interactionName = Constants.InteractionName.Refrigerator;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Refrigerator);
            base.Start();
        }

        protected override void SpecificDecision(Decision decision)
        {
            
        }

        protected override void RequirmentDecision(List<Decision> decisionList)
        {
            
        }
    }
}