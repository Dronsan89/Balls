using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BallsFall
{
    public class UIController : MonoBehaviour
    {
        public event Action OnStartClick;
        public event Action OnPauseClick;
        public event Action OnRestartClick;
        public event Action OnProceedClick;

        [SerializeField] private Slider hpBar;
        [SerializeField] private GameObject introScreen;
        [SerializeField] private GameObject gameScreen;
        [SerializeField] private GameObject failScreen;
        [SerializeField] private GameObject pauseScreen;
        [Header("Texts")]
        [SerializeField] private Text rewardText;
        [SerializeField] private Text speedText;
        [Header("Buttons")]
        [SerializeField] private Button startButton;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button proceedButton;

        private GameObject currentScreen;

        private void Awake()
        {
            startButton.onClick.AddListener(() => Game());
            pauseButton.onClick.AddListener(() => Pause());
            restartButton.onClick.AddListener(() => Restart());
            proceedButton.onClick.AddListener(() => Proceed());
        }

        public void SetHpBar(Vector2 health) => hpBar.value = health.x / health.y;

        public void SetReward(int reward) => rewardText.text = "Reward: " + reward;

        public void Intro()
        {
            currentScreen = introScreen;
            currentScreen.SetActive(true);
        }

        public void Game()
        {
            SetActiveScreen(gameScreen);
            OnStartClick?.Invoke();
        }

        public void Pause()
        {
            SetActiveScreen(pauseScreen);
            OnPauseClick?.Invoke();
        }

        public void Restart()
        {
            SetActiveScreen(gameScreen);
            OnRestartClick?.Invoke();
        }

        public void Proceed()
        {
            SetActiveScreen(gameScreen);
            OnProceedClick?.Invoke();
        }

        public void Fail()
        {
            SetActiveScreen(failScreen);
        }

        public void SpeedUp(float speed)
        {
            speedText.gameObject.SetActive(true);
            speedText.text = "SPEED UP! " + speed;
            StartCoroutine(CloseTextSpeedUp());
        }

        IEnumerator CloseTextSpeedUp()
        {
            yield return new WaitForSeconds(1);
            speedText.gameObject.SetActive(false);
        }

        private void SetActiveScreen(GameObject screen)
        {
            currentScreen.SetActive(false);
            currentScreen = screen;
            currentScreen.SetActive(true);
        }
    }
}
