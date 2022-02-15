using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldenInputManager : MonoSingleton<EldenGameManager>
{
    public event Action<float> OnSwapScreen;
    private bool _isMouseDown = false;
    private Vector2 _mouseDownPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseDown = true;
            _mouseDownPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isMouseDown = false;
            if (OnSwapScreen != null)
            {
                OnSwapScreen.Invoke(Vector2.Angle(_mouseDownPos, Input.mousePosition));
            }
        }
    }
}
