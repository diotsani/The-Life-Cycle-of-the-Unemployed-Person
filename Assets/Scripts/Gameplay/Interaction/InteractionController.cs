using System;
using System.Collections.Generic;
using Team8.Unemployment.Global;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

namespace Team8.Unemployment.Gameplay
{
    public class InteractionController : MonoBehaviour
    {
        [SerializeField] private List<BaseInteraction> _objects;
        [SerializeField] private QuickOutline[] _outlines;
        
        public BaseInteraction currentInteraction;
        public BaseInteraction selectedInteraction;

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

        private void Update()
        {
            Selected();
            GetOutline();
        }

        private void GetOutline()
        {
            LayerMask _mask = LayerMask.GetMask("Interaction");
            LayerMask _default = LayerMask.GetMask("Default");
            RaycastHit _hit;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if(!PlayerStatusData.Instance.isPlayGame)return;
            if (Physics.Raycast(_ray, out _hit, _mask))
            {
                currentInteraction = _hit.collider.GetComponentInParent<BaseInteraction>();
                if (currentInteraction != null)
                {
                    currentInteraction.SetOutline(1);
                    if (Input.GetMouseButtonDown(0))
                    {
                        selectedInteraction = currentInteraction;
                    }
                }
                else 
                {
                    SetOutlines(0);
                    Selected();
                }
            }
        }

        private void Selected()
        {
            if(selectedInteraction != null)
            {
                selectedInteraction.SetOutline(1);
                if (Input.GetMouseButtonDown(0))
                {
                    selectedInteraction.SetOutline(0);
                    selectedInteraction = null;
                }
            }
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