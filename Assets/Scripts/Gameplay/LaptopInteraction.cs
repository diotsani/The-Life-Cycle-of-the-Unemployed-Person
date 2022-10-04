using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class LaptopInteraction : BaseInteraction
    {
        protected override void Start()
        {
            _interactionName = Constants.InteractionName.Laptop;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Laptop);
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