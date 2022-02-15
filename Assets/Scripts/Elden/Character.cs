using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private void Awake()
    {
        EldenInputManager.Instance.OnSwapScreen += OnSwapScreen;
    }

    private void OnSwapScreen(float angle)
    {
        
    }
}