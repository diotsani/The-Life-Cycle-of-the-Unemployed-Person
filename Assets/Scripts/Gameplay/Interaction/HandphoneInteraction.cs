using System.Collections.Generic;
using DG.Tweening;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class HandphoneInteraction : BaseInteraction
    {
        [SerializeField] private Material _handphoneScreen;
        [ColorUsage(true, true)] [SerializeField] private Color _emissionColor;
        private int _priceRepair = 25;
        private int _minRandom = 6;
        private int _maxRandom = 9;
        protected override void Start()
        {
            _interactionName = Constants.Name.Handphone;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.Handphone);
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
           // _dmgParticle.Play();
        }
        else
        {
            _dmgParticle.gameObject.SetActive(false);
            //_dmgParticle.Stop();
        }
    }
        public override void OnEffect()
        {
           // _handphoneScreen.EnableKeyword("_EMISSION");
            //_handphoneScreen.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            //_laptopScreen.SetColor("_EmissionColor", Color.white);
            if(_isDamaged)return;
            _handphoneScreen.DOColor(_emissionColor, "_EmissionColor", 1f).From(Color.black);
        }

        public override void OffEffect()
        {
            _handphoneScreen.DOColor(Color.black, "_EmissionColor", 1f);
        }
        protected override void SpecificDecision(Decision decision)
        {
            int rnd = Random.Range(0, 3);
            Debug.Log(rnd);
            
            if (decision.DecisionText() == Constants.Requirments.SocialMedia)
            {
                int rndStress = Random.Range(-10, 12 + 1);
                _playerStatusData.StressCost(rndStress);
                if (rndStress > 1)
                {
                    ShowMonologue(Constants.Monologue.SocialMediaMonolog_2);
                }
                else
                {
                    ShowMonologue(Constants.Monologue.SocialMediaMonolog_1);
                }
                ShowFeedback(Constants.Feedback.SocialMediaFeedback);
            }
            if (decision.DecisionText() == Constants.Requirments.JobInfo)
            {
                if (_playerStatusData.skill > 50)
                {
                    ShowMonologue(Constants.Monologue.JobInfoMonolog_1);
                }
                else
                {
                    ShowMonologue(Constants.Monologue.JobInfoMonolog_2);
                }
                
                ShowHistory(Constants.History.JobInfo);
            }
            if (decision.DecisionText() == Constants.Requirments.Sell)
            {
                ShowMonologue(Constants.Monologue.SellHandphoneMonolog(rnd));
                ShowFeedback(Constants.Feedback.SellHandphoneFeedback);
                ShowHistory(Constants.History.SellHandphone);

                DecisionParent().SetActive(false);
                this.gameObject.SetActive(false);
            }
            if (decision.DecisionText() == Constants.Requirments.Repair)
            {
                ShowMonologue(Constants.Monologue.RepairHandphoneMonolog(rnd));
                ShowFeedback(Constants.Feedback.RepairHandphoneFeedback);
                ShowHistory(Constants.History.RepairHandphone);
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
                Decision obj = decisionList[i];
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