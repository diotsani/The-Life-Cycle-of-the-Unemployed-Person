using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class LaptopInteraction : BaseInteraction
    {
        [SerializeField] private Material _laptopScreen;
        [ColorUsage(true, true)] [SerializeField] private Color _emissionColor;
        [SerializeField] private float _intensity;
        private Color _defaultColor = new Color(22, 22, 22, 255);
        private int _priceRepair = 50;
        [Header("Config Random Durability")]
        [SerializeField] private int _minRandom;
        [SerializeField] private int _maxRandom;
        protected override void Start()
        {
            _interactionName = Constants.Name.Laptop;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Laptop);
            RandomMaxClick(_minRandom, _maxRandom);
            base.Start();

            _dmgParticle = Instantiate(_damagedParticle, _objectPosition);
            _dmgParticle.gameObject.SetActive(false);
        }

        protected override void Update()
        {
            base.Update();
            if (_isDamaged)
            {
                _dmgParticle.gameObject.SetActive(true);
                //_dmgParticle.Play();
            }
            else
            {
                _dmgParticle.gameObject.SetActive(false);
                //_dmgParticle.Stop();
            }
        }

        public override void OnEffect()
        {
            //_laptopScreen.EnableKeyword("_EMISSION");
            //_laptopScreen.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
           // _laptopScreen.SetColor("_EmissionColor", _whiteColor* Mathf.Pow(2, _intensity));
           if(_isDamaged)return;
           _laptopScreen.DOColor(_emissionColor, "_EmissionColor", 1f).From(Color.black);

        }

        public override void OffEffect()
        {
            _laptopScreen.DOColor(Color.black, "_EmissionColor", 1f);
        }

        protected override void SpecificDecision(Decision decision)
        {
            int rnd = Random.Range(0, 3);
            if (decision.DecisionText() == Constants.Requirments.ApplyJob)
            {
                ShowMonologue(Constants.Monologue.ApplyJobMonolog(rnd));
                ShowFeedback(Constants.Feedback.ApplyJobFeedback);
                ShowHistory(Constants.History.ApplyJob);
                _playerStatusData.isApplyJob = true;
                decision.DecisionObject().SetActive(!_playerStatusData.isApplyJob);
            }
            if (decision.DecisionText() == Constants.Requirments.TakeCourse)
            {
                ShowMonologue(Constants.Monologue.TakeCourseMonolog(rnd));
                ShowFeedback(Constants.Feedback.TakeCourseFeedback);
                ShowHistory(Constants.History.TakeCourse);
            }
            if (decision.DecisionText() == Constants.Requirments.PlayGame)
            {
                ShowMonologue(Constants.Monologue.PlayGameMonolog(rnd));
                
                ShowFeedback(Constants.Feedback.PlayGameFeedback);
                ShowHistory(Constants.History.PlayGame);
            }
            if (decision.DecisionText() == Constants.Requirments.CheckMail)
            {
                ShowHistory(Constants.History.CheckMail);
                if(!_playerStatusData.isApplied)
                {
                    ShowMonologue(Constants.Monologue.CheckMailMonolog_1);
                }
                else if(_playerStatusData.isApplied && _playerStatusData.skill < 100)
                {
                    ShowMonologue(Constants.Monologue.CheckMailMonolog_2);
                }
            }
            if (decision.DecisionText() == Constants.Requirments.Repair)
            {
                ShowMonologue(Constants.Monologue.RepairLaptopMonolog(rnd));
                ShowFeedback(Constants.Feedback.RepairLaptopFeedback);
                ShowHistory(Constants.History.RepairLaptop);
                ResetDecision();
                ResetAmountClick();
                RandomMaxClick(_minRandom, _maxRandom);
                _interactionState = InteractionState.Good;
                _isDamaged = false;
            }
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            SetRepairDecision(_priceRepair);
            if(_isDamaged)return;
            for (int i = 0; i < decisionList.Count; i++)
            {
                var obj = decisionList[i];
                if (obj.DecisionText() == Constants.Requirments.ApplyJob)
                {
                    bool set = _playerStatusData.isApplyJob;
                    obj.DecisionObject().SetActive(!set);
                    obj.DecisionButton().interactable = !set;
                }
                if (obj.DecisionText() == Constants.Requirments.TakeCourse)
                {
                    bool set = _playerStatusData.stress <= _playerStatusData.minStress;
                    obj.DecisionButton().interactable = set;
                }
            }
        }
        protected override void CheckCondition()
        {
            if(_amountClicked >= _maxClicked)
            {
                _interactionState = InteractionState.Damaged;
                DamageInteraction();
            }
        }

        protected override void DamageInteraction()
        {
            _isDamaged = true;
            foreach (Decision obj in _decisionList)
            {
                obj.DecisionButton().interactable = false;
                if (obj.DecisionText() == Constants.Requirments.Repair)
                {
                    obj.DecisionObject().SetActive(true);
                    obj.DecisionButton().interactable = true;
                }
            }
        }
    }
}