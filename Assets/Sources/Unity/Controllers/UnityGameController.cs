using UnityEngine;
using UnityEngine.Events;
using Interactors;
using Zenject;
using Saves;

public class UnityGameController : MonoBehaviour
{
    private GameController gameController;
    private int targetLevel;

    [SerializeField]
    private UnityEvent gameOver;
    [SerializeField]
    private UnityEvent gameWin;

    [Inject]
    public void Init(GameController gameController)
    {
        this.gameController = gameController;
        this.gameController.AddLoseCallback(Lose);
        this.gameController.AddWinCallback(Win);
    }

    public void SetTargetlevel(int level)
    {
        targetLevel = level;
    }

    public void NextLevel()
    {
        targetLevel++;
    }

    public void Play()
    {
        gameController.Play(targetLevel);
    }

    public void Retry()
    {
        gameController.Stop();
        gameController.Play(targetLevel);
    }

    public void Stop()
    {
        gameController.Stop();
    }

    public void Pause()
    {
        gameController.Pause();
    }

    public void Resume()
    {
        gameController.Resume();
    }

    private void Update()
    {
        gameController.Update();
    }

    private void Win()
    {
        gameWin.Invoke();
    }

    private void Lose()
    {
        gameOver.Invoke();
    }
}
