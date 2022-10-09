using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Team8.Unemployment.Gameplay;
using Team8.Unemployment.Player;
using TMPro;
using UnityEngine;

namespace Team8.Unemployment.Utility
{
    public class Tooltip : MonoBehaviour
    {

        #region Public Variable

        [Header("Dependencies")]
        public LayerMask interactMask;
        public PlayerController player;
        public Vector3 target;
        
        [Header("Tooltip UI")]
        public GameObject tooltip;
        public TMP_Text tooltipText;

        #endregion

        #region Private Variable

        private Ray _ray;
        private RaycastHit _hit;
        private bool _hitInfo;
        private IInteractable _interactable;

        #endregion

        private void Update()
        {
            InitRay();
            InitCurrentPos();
        }

        void InitRay()
        {
            _hitInfo = false;
            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        void InitCurrentPos()
        {
            if (Physics.Raycast(_ray, out _hit, interactMask))
            {
                _interactable = _hit.collider.GetComponent<IInteractable>();

                if (_interactable != null)
                {
                    _hitInfo = true;
                    
                    TooltipUI(_hitInfo);

                    if (Input.GetMouseButtonDown(0))
                    {
                        target = _interactable.TargetPostision();
                        player.LookAtTarget(target);
                        player.playerStatus = PlayerController.PlayerStatus.walk;
                    }
                }
                
                TooltipUI(_hitInfo);
            }
        }

        public void TooltipUI(bool status)
        {
            tooltip.SetActive(status);
            tooltip.transform.position = Input.mousePosition;
            tooltipText.text = _interactable != null ? _interactable.GetName() : String.Empty;
        }
    }
}


