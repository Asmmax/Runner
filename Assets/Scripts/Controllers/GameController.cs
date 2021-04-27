using UnityEngine;
using UnityEngine.Events;
using Services.Saves;
using Core.Game;
using Core.Data;

public interface IConverterGateway
{
    IEntityConverter GetConverterFor(int level);
}

public class GameController : MonoBehaviour, IGameController, IScoreView
{
    [SerializeField]
    private UnityEvent gameOver;
    [SerializeField]
    private UnityEvent gameWin;

    private int targetLevel;

    bool isPlayed = false;

    int curLevel = 0;
    int curScore = 0;
    IConverterGateway converterGateway;
    ILevelGateway levelGateway;
    IInputController inputController;
    ITimeController timeController;

    GameModel targetModel;

    public GameModel TargetModel
    {
        set
        {
            targetModel = value;
            targetModel.GameController = this;
        }
    }

    [Zenject.Inject]
    public void Init(IConverterGateway converterGateway,
        ILevelGateway levelGateway,
        ITimeController timeController,
        IInputController inputController)
    {
        this.converterGateway = converterGateway;
        this.levelGateway = levelGateway;
        this.inputController = inputController;
        this.timeController = timeController;
    }

    private void Awake()
    {
        inputController.Lock();
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
        Play(targetLevel);
    }

    public void Retry()
    {
        Stop();
        Play(targetLevel);
    }

    public void Play(int level)
    {
        if (isPlayed) return;
        isPlayed = true;

        inputController.Unlock();

        curLevel = level;
        IEntityConverter entityConverter = converterGateway.GetConverterFor(level);
        targetModel.EntityConverter = entityConverter;

        targetModel.Initialize();
    }

    public void Pause()
    {
        timeController.Lock();
        inputController.Lock();
    }

    public void Resume()
    {
        timeController.Unlock();
        inputController.Unlock();
    }

    public void Update()
    {
        if (targetModel != null && targetModel.IsValid())
        {
            targetModel.Update();
        }
    }

    public void Stop()
    {
        if (!isPlayed) return;
        isPlayed = false;

        StopImpl();
    }

    private void StopImpl()
    {
        targetModel.Dispose();
        inputController.Reset();
        inputController.Lock();
        curScore = 0;
    }

    public void Win()
    {
        if (!isPlayed) return;
        isPlayed = false;

        Level stats = levelGateway.GetOrCreateLevelStats(curLevel);
        stats.PutNewScore(curScore);
        stats.Complate();
        levelGateway.PutLevelStats(stats);
        StopImpl();
        gameWin.Invoke();
    }

    public void Lose()
    {
        if (!isPlayed) return;
        isPlayed = false;

        Level stats = levelGateway.GetOrCreateLevelStats(curLevel);
        stats.PutNewScore(curScore);
        levelGateway.PutLevelStats(stats);
        StopImpl();
        gameOver.Invoke();
    }

    void IScoreView.Update(int points)
    {
        curScore = points;
    }
}