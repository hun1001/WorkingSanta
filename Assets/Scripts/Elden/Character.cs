using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    private int _currentLine = 0;

    private void Awake()
    {
        EldenInputManager.Instance.OnSwapScreen += OnSwapScreen;
    }

    private void OnSwapScreen(bool isLeft)
    {
        if (isLeft)
        {
            if (_currentLine > 0)
            {
                _currentLine--;
            }
        }
        else
        {
            if (_currentLine < 3)
            {
                _currentLine++;
            }
        }

        Vector2 temp = transform.position;
        temp.x = EldenGameManager.Instance.Line.List[_currentLine].transform.position.x;
        transform.DOMove(temp, 0.5f);
    }
}