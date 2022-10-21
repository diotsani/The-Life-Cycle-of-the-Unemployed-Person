using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class BookInteraction : BaseInteraction
    {
        [SerializeField] private List<GameObject> _bookList;
        [SerializeField] private Collider _bookCollider;
        protected override void Start()
        {
            _interactionName = Constants.Name.BookShelf;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.BookShelf);
            base.Start();
        }

        protected override void SpecificDecision(Decision decision)
        {
            int rnd = Random.Range(0, 3);
            Debug.Log(rnd);
            if (decision.DecisionText() == Constants.Requirments.ReadBook)
            {
                ShowMonologue(Constants.Monologue.ReadBookMonolog(rnd));
                
                ShowFeedback(Constants.Feedback.ReadBookFeedback);
                ShowHistory(Constants.History.ReadBook);
            }
            if (decision.DecisionText() == Constants.Requirments.Sell)
            {
                ShowMonologue(Constants.Monologue.SellBookMonolog(rnd));
                
                ShowFeedback(Constants.Feedback.SellBookFeedback);
                ShowHistory(Constants.History.SellBook);
                
                _bookList[0].SetActive(false);
                _bookList.RemoveAt(0);
            }
            if (decision.DecisionText() == Constants.Requirments.CheckBookShelf)
            {
                ShowMonologue(Constants.Monologue.BookStockMonolog(rnd,_playerStatusData.book));
                ShowHistory(Constants.History.CheckBookShelf);
            }
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            foreach (var obj in decisionList)
            {
                bool set = _playerStatusData.book > 0;
                if(_playerStatusData.book <= 0)
                {
                    _bookCollider.enabled = false;
                    DecisionParent().SetActive(false);
                }
            }
        }
    }
}