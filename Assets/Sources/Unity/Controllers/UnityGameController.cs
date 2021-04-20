using UnityEngine;
using UnityEngine.Events;
using Interactors;
using Zenject;

public class UnityGameController : MonoBehaviour
{
    private PauseInteractor pauseInteractor;
    private PlayInteractor playInteractor;

    [SerializeField]
    private UnityEvent gameOver;
    [SerializeField]
    private UnityEvent gameWin;

    [Inject]
    public void Init(PauseInteractor pauseInteractor, PlayInteractor playInteractor)
    {
        this.playInteractor = playInteractor;
        this.pauseInteractor = pauseInteractor;
        this.playInteractor.AddLoseCallback(Lose);
        this.playInteractor.AddWinCallback(Win);
    }

    public void Play(int level)
    {
        playInteractor.Play(level);
    }

    public void Retry()
    {
        playInteractor.Retry();
    }

    public void Stop()
    {
        playInteractor.Stop();
    }

    public void Pause()
    {
        pauseInteractor.Pause();
    }

    public void Resume()
    {
        pauseInteractor.Resume();
    }

    private void Update()
    {
        playInteractor.Update();
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
