using UnityEngine;

public class FrustrumCleaner : MonoBehaviour
{
    private CleanBehaviour cleanBehaviour;

    private void Awake()
    {
        cleanBehaviour = GetComponent<CleanBehaviour>();
    }

    public void ClearForward()
    {
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(cameraHeight * mainCamera.aspect, cameraHeight);
        Vector2 cameraPos = mainCamera.transform.position;

        Vector2 pos = transform.position;

        if (pos.x > cameraPos.x + cameraSize.x / 2)
        {
            cleanBehaviour.CallAfterClean();
        }
    }
}
