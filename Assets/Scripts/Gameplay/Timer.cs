using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
    public class Timer : MonoBehaviour
    {
        public int TotalTime = 20;

        private DateTime startTime;

        private bool timerOn = false;

        public Image Semaphor;

        //public TextMeshProUGUI CountDown;
        public TextMeshProUGUI TimeRemaining;

        public Slider TimeSlider;
        public TextMeshProUGUI FinishText;
        public TextMeshProUGUI ScoreText;

        public PlayerSlingshotInput playerInput;

        private int previousTime;

        private void Start()
        {
            FinishText.gameObject.SetActive(false);
            ScoreText.gameObject.SetActive(false);
            TimeSlider.maxValue = TotalTime;
            TimeSlider.minValue = 0;
            TimeSlider.value = TotalTime;
            TimeRemaining.text = TotalTime.ToString();
            var sequence = DOTween.Sequence();
            var semaphorTransform = Semaphor.transform;
            //var timeTransform = CountDown.transform;
            semaphorTransform.localScale = Vector3.one * 5;
            Semaphor.color = Color.red;
            sequence.Append(semaphorTransform.DOScale(0, 0.8f));
            sequence.AppendCallback(() => { semaphorTransform.localScale = Vector3.one * 5; });
            sequence.Append(semaphorTransform.DOScale(0, 1));
            sequence.AppendCallback(() => { semaphorTransform.localScale = Vector3.one * 5; });
            sequence.Append(semaphorTransform.DOScale(0, 1));
            sequence.AppendCallback(() =>
            {
                semaphorTransform.localScale = Vector3.one * 5;
                Semaphor.color = Color.green;
                //Semaphor.DOFade(0, 1);
            });
            sequence.Append(Semaphor.DOFade(0, 1));
            sequence.Append(semaphorTransform.DOScale(0, 1));
            sequence.AppendCallback(() =>
            {
                semaphorTransform.gameObject.SetActive(false);
                startTime = DateTime.Now;
                previousTime = TotalTime;
                timerOn = true;
                playerInput.enabled = true;
            });
            sequence.Play();
        }

        private void Update()
        {
            if (!timerOn) return;
            var timeRemaining = TotalTime - (int) (DateTime.Now - startTime).TotalSeconds;

            if (timeRemaining <= 0)
            {
                timerOn = false;
                timeRemaining = 0;
                playerInput.enabled = false;
            }

            if (timeRemaining < previousTime)
            {
                previousTime = timeRemaining;
                TimeRemaining.text = timeRemaining.ToString();
                TimeSlider.value = timeRemaining;
            }
        }
    }
}