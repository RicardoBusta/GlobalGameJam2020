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
        public Text FinishText;
        public Text ScoreText;

        public PlayerSlingshotInput playerInput;

        public Canvas GameOverScreen;

        private int previousTime;

        public CanvasGroup PieGraphic;
        public Image PieFill;
        public TextMeshProUGUI ScorePercentage;

        public ScoreCalculator Score;

        private void Start()
        {
            PieGraphic.gameObject.SetActive(false);
            GameOverScreen.gameObject.SetActive(false);
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
                timeRemaining = 0;
                GameOver();
            }

            if (timeRemaining < previousTime)
            {
                previousTime = timeRemaining;
                TimeRemaining.text = timeRemaining.ToString();
                TimeSlider.value = timeRemaining;
            }
        }

        private void GameOver()
        {
            timerOn = false;
            playerInput.enabled = false;

            var gameOverSequence = DOTween.Sequence();
            gameOverSequence.AppendCallback(() =>
            {
                PieGraphic.gameObject.SetActive(true);
                PieGraphic.alpha = 0;
                PieFill.fillAmount = 0;
                ScorePercentage.text = "0%";
                Score.UpdateValue();
            });
            gameOverSequence.Append(PieGraphic.DOFade(1, 1));
            gameOverSequence.Append(DOVirtual.Float(0f, Score.FinalScore, 2f, v =>
            {
                var i = Mathf.RoundToInt(v);
                PieFill.fillAmount = i/100f;
                ScorePercentage.text = $"{i}%";
            }));
            gameOverSequence.AppendCallback(() => { GameOverScreen.gameObject.SetActive(true); });
            gameOverSequence.Play();
        }
    }
}