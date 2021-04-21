using UnityEngine;

public class CameraCleaner : CleanBehaviour
{
    
    private float avgVelocity;
    private Vector2 oldPos;

    private bool isClean = false;

    public override void ClearByWin()
    {
        isClean = true;
    }

    public override void ClearByLose()
    {
        isClean = true;
    }

    private void FixedUpdate()
    {
        if (!isClean)
        {
            avgVelocity = (transform.position.x - oldPos.x) / Time.fixedDeltaTime;
            oldPos = transform.position;
        }
    }

    private void Update()
    {
        if (isClean)
        {
            transform.Translate(avgVelocity * Time.deltaTime / 2, 0, 0);
        }
    }
}
