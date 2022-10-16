using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Team8.Unemployment.Gameplay
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private List<BaseInteraction> _objects;
        [SerializeField] private QuickOutline[] _outlines;

        private void OnEnable()
        {
            Decision.OnClickDecision += SetAllObjects;
            Decision.OnClickInteracted += SetInteracted;
            GameplayFlow.OnEndDay += DeactivatedDecision;
            GameplayFlow.OnChangeDay += ChangeDay;
            GameplayFlow.OnEndGame += DeactivatedDecision;
        }

        private void OnDisable()
        {
            Decision.OnClickDecision -= SetAllObjects;
            Decision.OnClickInteracted -= SetInteracted;
            GameplayFlow.OnEndDay -= DeactivatedDecision;
            GameplayFlow.OnChangeDay -= ChangeDay;
            GameplayFlow.OnEndGame -= DeactivatedDecision;
        }

        private void Start()
        {
            SetOutlines(0);
        }
        
        public void SetOutlines(float value)
        {
            foreach (var outline in _outlines)
            {
                outline.OutlineWidth = value;
            }
        }

        public void SetOffParent()
        {
            foreach (var obj in _objects)
            {
                obj.DecisionParent().SetActive(false);
            }
        }
        public void SetInteracted(bool isInteracted)
        {
            foreach (var obj in _objects)
            {
                obj.isInteracted = isInteracted;
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