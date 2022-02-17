using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class Character : MonoBehaviour
{
    private int _currentLine = 0;
    private float _speed;

    private void Awake()
    {
        EldenInputManager.Instance.OnSwapScreenV += OnSwapScreenV;
        EldenInputManager.Instance.OnSwapScreenH += OnSwapScreenH;
    }

    private void OnSwapScreenH(bool isUp)
    {
        if (isUp)
        {
            _speed += 0.5f;
        }
        else
        {   
            _speed -= 0.5f;
        }
    }

    private void OnSwapScreenV(bool isLeft)
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

        transform.DOMoveX(EldenGameManager.Instance.Line.List[_currentLine].transform.position.x, 0.5f);
    }

    private void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + _speed/100);
        
        if (gameObject.transform.position.y > Screen.height / 2)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, Screen.height / 2);
        }
        else if (gameObject.transform.position.y < -Screen.height / 2)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -Screen.height / 2);
        }

        if (gameObject.transform.position.x > Screen.width / 2)
        {
            gameObject.transform.position = new Vector2(Screen.width / 2, gameObject.transform.position.y);
        }
        else if (gameObject.transform.position.x < -Screen.width / 2)
        {
            gameObject.transform.position = new Vector2(-Screen.width / 2, gameObject.transform.position.y);
        }
    }
}