using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CleanBehaviour : MonoBehaviour
{
    public delegate void AfterClean(int id);

    [SerializeField]
    private UnityEvent clearByWin;
    [SerializeField]
    private UnityEvent clearByLose;

    private AfterClean afterClean;
    private int ownID;

    public void SubscribeAfterClean(int ownID, AfterClean callback)
    {
        this.ownID = ownID;
        afterClean = callback;
    }

    public void CallAfterClean()
    {
        afterClean(ownID);
    }

    public void ClearByWin() { clearByWin.Invoke(); }
    public void ClearByLose() { clearByLose.Invoke(); }
}
