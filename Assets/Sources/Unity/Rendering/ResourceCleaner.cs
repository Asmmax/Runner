using UnityEngine;

public class ResourceCleaner : CleanBehaviour
{

    public override void ClearByLose()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(cameraHeight * mainCamera.aspect, cameraHeight);
        Vector2 cameraPos = mainCamera.transform.position;

        Vector2 pos = transform.position;

        if (pos.x > cameraPos.x + cameraSize.x / 2)
        {
            afterClean(ownID, gameObject);
        }
    }

    public override void ClearByWin()
    {

    }
}
