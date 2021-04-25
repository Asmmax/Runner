using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class BackController : MonoBehaviour
{
    [SerializeField]
    private UnityEvent backClick;



    void Update()
    {
        bool backPressed = Input.GetButtonDown("Back");
        if (backPressed)
            backClick.Invoke();
    }
}
