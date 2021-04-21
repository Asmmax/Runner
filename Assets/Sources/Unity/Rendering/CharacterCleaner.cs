using UnityEngine;

public class CharacterCleaner : CleanBehaviour
{
    private float avgVelocity;
    private float maxVelocity;
    private Vector2 oldPos;

    private bool isClean = false;

    public override void ClearByWin()
    {
        isClean = true;
    }

    public override void ClearByLose()
    {
    }

    private void FixedUpdate()
    {
        if (!isClean)
        {
            avgVelocity = (transform.position.x - oldPos.x) / Time.fixedDeltaTime;
            oldPos = transform.position;
            maxVelocity = 3 * avgVelocity;
        }
    }

    private void Update()
    {
        if (isClean)
        {
            transform.Translate(avgVelocity * Time.deltaTime, 0, 0);
            avgVelocity = Mathf.Clamp(avgVelocity + 0.05f, 0, maxVelocity);
        }
    }
}
