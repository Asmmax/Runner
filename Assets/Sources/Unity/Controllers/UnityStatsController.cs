using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Saves;

public class UnityStatsController : MonoBehaviour
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
