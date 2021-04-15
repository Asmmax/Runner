using UnityEngine;
using Core.Game;

public class UnityTimeController : ITimeController
{
    private bool isLocked = false;
    public float DeltaTime
    {
        get
        {
            if (isLocked) return 0;
            return Time.deltaTime;
        }
    }

    public void Lock()
    {
        isLocked = true;
    }

    public void Unlock()
    {
        isLocked = false;
    }
}
