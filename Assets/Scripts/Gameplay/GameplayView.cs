using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Team8.Unemployment.Gameplay
{
    public class GameplayView : MonoBehaviour
    {
        [Header("Day Display")]
        [SerializeField] private TMP_Text _dayText;
        [SerializeField] private GameObject _dayPanel;

        private void OnEnable()
        {
            DayManager.OnShowDay += ShowDay;
        }

        private void OnDisable()
        {
            DayManager.OnShowDay -= ShowDay;
        }

        private void ShowDay(int value)
        {
            _dayText.text = Constants.Status.Day + value.ToString();
            _dayPanel.SetActive(true);
            StartCoroutine(AfterActive(_dayPanel,2f));
        }
        private IEnumerator AfterActive(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);
        }
    }
}