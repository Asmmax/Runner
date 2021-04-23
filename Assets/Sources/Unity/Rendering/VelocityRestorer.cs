using UnityEngine;

public class VelocityRestorer : MonoBehaviour
{
    [SerializeField]
    private float minMultFactor = 1;
    [SerializeField]
    private float maxMultFactor = 3;
    [SerializeField]
    private float deltaMultFactor = 0.01f;

    private float avgVelocity;
    private float multFactor;
    private Vector2 oldPos;

    private bool isRestored = false;

    private void OnEnable()
    {
        isRestored = false;
    }

    public void Restore()
    {
        isRestored = true;
        multFactor = minMultFactor;
    }

    private void FixedUpdate()
    {
        if (!isRestored)
        {
            avgVelocity = (transform.position.x - oldPos.x) / Time.fixedDeltaTime;
            oldPos = transform.position;
        }
    }

    private void Update()
    {
        if (isRestored)
        {
            transform.Translate(avgVelocity * Time.deltaTime * multFactor, 0, 0);
            multFactor = Mathf.Clamp(multFactor + deltaMultFactor, 0, maxMultFactor);
        }
    }
}
