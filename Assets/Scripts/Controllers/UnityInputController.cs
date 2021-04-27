using UnityEngine;
using Core.Game;
using Zenject;

public class UnityInputController : ITickable, IInputController
{
    private float oldValue = 0;
    private float currentPos = 0;
    private bool isLocked = false;

    public float Vertical
    {
        get
        {
            return currentPos;
        }
    }

    public void Reset()
    {
        oldValue = 0;
        currentPos = 0;
    }

    public void Tick()
    {
        if (isLocked) return;

        float vertical = Input.GetAxis("Vertical") * 0.5f;
        if (vertical != oldValue)
        {
            currentPos = Mathf.Clamp(currentPos + vertical, -1, 1);
            oldValue = vertical;
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
