using BallsFall.Settings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BallsFall
{
    enum GameState
    {
        intro,
        game,
        fail,
        pause,
        restart,
        proceed
    }

    [RequireComponent(typeof(EventSystem))]
    public class Main : MonoBehaviour
    {
        [SerializeField] private GameSetting gameSetting;
        [SerializeField] private UIController uiController;
        [SerializeField] private BallsCreator ballsCreator;
        [SerializeField] private ProgressTimer progressTimer;

        private RepositoryRewards repositoryRewards;
        private Health health;

        private void Awake()
        {
            Init();

            Subscriptions();
        }

        private void Init()
        {
            health = new Health(gameSetting);
            repositoryRewards = new RepositoryRewards();

            ballsCreator.Init(gameSetting, health, repositoryRewards);
            progressTimer.Init(gameSetting);
        }

        private void Subscriptions()
        {
            ballsCreator.OnDestroyBall += DataDestroySelector;
            health.OnChangeHealth += (health) => { uiController.SetHpBar(new Vector2(health.Item1, health.Item2)); };
            health.OnDead += () => { SetState(GameState.fail); };
            repositoryRewards.OnChangeRewards += uiController.SetReward;
            progressTimer.OnSpeedUp += uiController.SpeedUp;

            uiController.OnStartClick += () => { SetState(GameState.game); };
            uiController.OnRestartClick += () => { SetState(GameState.restart); };
            uiController.OnPauseClick += () => { SetState(GameState.pause); };
            uiController.OnProceedClick += () => { SetState(GameState.proceed); };
        }

        private void Start() => SetState(GameState.intro);

        private void SetState(GameState state)
        {
            switch (state)
            {
                case GameState.intro:
                    IntroGame();
                    break;
                case GameState.game:
                    StartGame();
                    break;
                case GameState.fail:
                    FailGame();
                    break;
                case GameState.pause:
                    PauseGame();
                    break;
                case GameState.restart:
                    RestartGame();
                    break;
                case GameState.proceed:
                    ProceedGame();
                    break;
            }
        }

        private void DataDestroySelector(DestroyModel model, IDestroyBall destroyBall) => destroyBall.DestroyBall(model);

        #region State game
        private void IntroGame() => uiController.Intro();

        private void StartGame()
        {
            Time.timeScale = 1;
            ballsCreator.Enable();
            progressTimer.IsEnable = true;
            progressTimer.ResetGame();
        }

        private void FailGame()
        {
            ballsCreator.Disable();
            uiController.Fail();
            ballsCreator.DestroyAllBall();
            progressTimer.IsEnable = false;
        }

        private void PauseGame() => Time.timeScale = 0;

        private void RestartGame()
        {
            Time.timeScale = 1;
            ballsCreator.DestroyAllBall();
            repositoryRewards.ResetCurrentReward();
            health.ResetGame();
            SetState(GameState.game);
        }

        private void ProceedGame() => Time.timeScale = 1;
        #endregion
    }
}
