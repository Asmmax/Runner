using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Interactors;

public class UnityStatusController : MonoBehaviour
{
    private StatusInteractor statusInteractor;

    [Zenject.Inject]
    public void Init(StatusInteractor statusInteractor)
    {
        this.statusInteractor = statusInteractor;
    }

    public void ShowLevelInfo(int level)
    {
        statusInteractor.ShowStatistics(level);
    }
}
