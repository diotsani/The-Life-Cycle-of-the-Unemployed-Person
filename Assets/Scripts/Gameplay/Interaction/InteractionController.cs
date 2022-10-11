using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Team8.Unemployment.Gameplay
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private List<BaseInteraction> _objects;

        private void OnEnable()
        {
            Decision.OnClickDecision += SetAllObjects;
            Decision.OnClickDecision += SetOffInteracted;
            GameplayFlow.OnEndDay += DeactivatedDecision;
            GameplayFlow.OnChangeDay += ChangeDay;
            GameplayFlow.OnEndGame += DeactivatedDecision;
        }

        private void OnDisable()
        {
            Decision.OnClickDecision -= SetAllObjects;
            Decision.OnClickDecision -= SetOffInteracted;
            GameplayFlow.OnEndDay -= DeactivatedDecision;
            GameplayFlow.OnChangeDay -= ChangeDay;
            GameplayFlow.OnEndGame -= DeactivatedDecision;
        }
        public void SetOffParent()
        {
            foreach (var obj in _objects)
            {
                obj.DecisionParent().SetActive(false);
            }
        }
        public void SetOffInteracted()
        {
            foreach (var obj in _objects)
            {
                obj.isInteracted = false;
            }
        }
        private void SetAllObjects()
        {
            foreach (var obj in _objects)
            {
                obj.isClicked = true;
            }
        }

        private void ChangeDay()
        {
            ReactivatedDecision();
            foreach (var obj in _objects)
            {
                obj.AddDurability();
            }
        }
        public void ResetAllDurability()
        {
            foreach (var obj in _objects)
            {
                obj.ResetDurability();
                Debug.Log("ResetDurability");
            }
        }
        private void DeactivatedDecision()
        {
            foreach (var obj in _objects)
            {
                obj.DeactivateDecision();
            }
        }
        private void ReactivatedDecision()
        {
            foreach (var obj in _objects)
            {
                obj.ReactivateDecision();
            }
        }
        
    }
}