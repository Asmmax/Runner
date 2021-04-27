using UnityEngine;
using Services.Saves;

public class StatsController : MonoBehaviour
{
    private State state;

    [Zenject.Inject]
    public void Init(State state)
    {
        this.state = state;
    }

    private void OnEnable()
    {
        state.Update();
    }

    public void UpdateStatistics()
    {
        state.Update();
    }
}
