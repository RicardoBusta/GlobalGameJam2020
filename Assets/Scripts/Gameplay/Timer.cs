using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    public class Timer : MonoBehaviour
    {
        public int TotalTime = 20;

        private DateTime startTime;

        private bool timerOn = false;

        public TextMeshProUGUI CountDown;
        public TextMeshProUGUI TimeRemaining;

        private int previousTime;

        private void Start()
        {
            TimeRemaining.text = TotalTime.ToString();
            var sequence = DOTween.Sequence();
            var timeTransform = CountDown.transform;
            CountDown.text = "3";
            timeTransform.localScale = Vector3.one;
            sequence.Append(timeTransform.DOScale(0, 1));
            sequence.AppendCallback(() =>
            {
                CountDown.text = "2";
                timeTransform.localScale = Vector3.one;
            });
            sequence.Append(timeTransform.DOScale(0, 1));
            sequence.AppendCallback(() =>
            {
                CountDown.text = "1";
                timeTransform.localScale = Vector3.one;
            });
            sequence.Append(timeTransform.DOScale(0, 1));
            sequence.AppendCallback(() =>
            {
                CountDown.text = "GO!!";
                timeTransform.localScale = Vector3.one;
            });
            sequence.Append(timeTransform.DOScale(0, 1));
            sequence.AppendCallback(() =>
            {
                timeTransform.gameObject.SetActive(false);
                startTime = DateTime.Now;
                previousTime = TotalTime;
                timerOn = true;
            });
        }

        private void Update()
        {
            if (!timerOn) return;
            var timeRemaining = TotalTime - (int) (DateTime.Now - startTime).TotalSeconds;

            if (timeRemaining <= 0)
            {
                timerOn = false;
                timeRemaining = 0;
            }

            if (timeRemaining < previousTime)
            {
                previousTime = timeRemaining;
                TimeRemaining.text = timeRemaining.ToString();
            }
        }
    }
}