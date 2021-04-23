using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField]
    float characterOffset = -7;

    [SerializeField]
    float startInitPos = -10;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void OnEnable()
    {
        transform.position = new Vector2(startInitPos, 0);
        UpdatePosition();
    }

    private void Start()
    {
        UpdatePosition();
    }

    void Update()
    {
        UpdatePosition();
    }
    void UpdatePosition()
    {
        mainCamera.transform.position = new Vector3(transform.position.x - characterOffset, transform.position.y, mainCamera.transform.position.z);
    }
}
