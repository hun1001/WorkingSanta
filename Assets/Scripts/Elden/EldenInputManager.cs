using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldenInputManager : MonoSingleton<EldenInputManager>
{
    public event Action<bool> OnSwapScreen;
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
                if (Mathf.Abs(_mouseDownPos.x - Input.mousePosition.x) > 1)
                {
                    OnSwapScreen(Input.mousePosition.x > _mouseDownPos.x ? false : true);
                }
            }
        }
    }
}
