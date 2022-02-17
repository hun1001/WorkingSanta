using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldenInputManager : MonoSingleton<EldenInputManager>
{
    public event Action<bool> OnSwapScreenH;
    public event Action<bool> OnSwapScreenV;
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
            if (OnSwapScreenV != null)
            {
                if (Mathf.Abs(_mouseDownPos.x - Input.mousePosition.x) > 250)
                {
                    OnSwapScreenV(!(Input.mousePosition.x > _mouseDownPos.x));
                }
            }

            if (OnSwapScreenH != null)
            {
                if (Mathf.Abs(_mouseDownPos.y - Input.mousePosition.y) > 250)
                {
                    OnSwapScreenH(Input.mousePosition.y > _mouseDownPos.y);
                }
            }
        }
    }
}
