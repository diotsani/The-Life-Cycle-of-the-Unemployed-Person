using System.Collections.Generic;
using Team8.Unemployment.Database;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class BookInteraction : BaseInteraction
    {
        protected override void Start()
        {
            _interactionName = Constants.Name.BookShelf;
            _decisionScriptable = Resources.Load<DecisionScriptable>(Constants.Path.BookShelf);
            base.Start();
        }

        protected override void SpecificDecision(Decision decision)
        {
            if (decision.DecisionText() == Constants.Requirments.ReadBook)
            {
                ShowMonologue(Constants.Monologue.ReadBookMonolog);
                ShowHistory(Constants.History.ReadBook);
            }
            if (decision.DecisionText() == Constants.Requirments.Sell)
            {
                ShowMonologue(Constants.Monologue.SellBookMonolog);
                ShowHistory(Constants.History.SellBook);
            }
            if (decision.DecisionText() == Constants.Requirments.CheckBookShelf)
            {
                ShowMonologue(Constants.Monologue.BookStockMonolog(_playerStatusData.book));
                ShowHistory(Constants.History.CheckBookShelf);
            }
        }

        protected override void RequirementDecision(List<Decision> decisionList)
        {
            foreach (var obj in decisionList)
            {
                bool set = _playerStatusData.book > 0;
                DecisionParent().SetActive(set);
                this.gameObject.SetActive(set);
            }
        }
    }
}