using System.Collections.Generic;
using UnityEngine;

namespace Team8.Unemployment.Database
{
    [CreateAssetMenu(fileName = "DecisionData", menuName = "Database/DecisionData")]
    public class DecisionScriptable : ScriptableObject
    {
        public List<DecisionData> decisionList = new List<DecisionData>();
    }
}