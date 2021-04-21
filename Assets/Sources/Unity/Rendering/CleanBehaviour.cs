using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CleanBehaviour : MonoBehaviour
{
    public delegate void AfterClean(int id, GameObject gameObject);

    protected AfterClean afterClean;
    protected int ownID;

    public void SubscribeAfterClean(int ownID, AfterClean callback)
    {
        this.ownID = ownID;
        afterClean = callback;
    }

    public abstract void ClearByWin();
    public abstract void ClearByLose();
}
