using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public abstract class BaseInteraction : MonoBehaviour,IInteractable
    {
        [SerializeField] protected string _interactionName;
        [SerializeField] protected GameObject _decisionParent;
    
        public void OnInteraction(bool status)
        {
            _decisionParent.SetActive(status);
        }
    }
}