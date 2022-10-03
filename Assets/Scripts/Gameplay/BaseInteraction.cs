using System.Collections;
using System.Collections.Generic;
using Team8.Unemployment.Global;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public abstract class BaseInteraction : MonoBehaviour,IInteractable
    {
        PlayerStatusData playerStatusData;
        [SerializeField] protected string _interactionName;
        [SerializeField] protected GameObject _decisionParent;
    
        public void OnInteraction(bool status)
        {
            _decisionParent.SetActive(status);
        }
    }
}