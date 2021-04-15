using UnityEngine;
using Interactors;
using Zenject;

public class UnityGameController : MonoBehaviour
{
    private PauseInteractor pauseInteractor;
    private PlayInteractor playInteractor;

    [Inject]
    public void Init(PauseInteractor pauseInteractor, PlayInteractor playInteractor)
    {
        this.playInteractor = playInteractor;
        this.pauseInteractor = pauseInteractor;
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
}
