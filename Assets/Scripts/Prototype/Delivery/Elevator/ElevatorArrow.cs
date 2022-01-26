using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorArrow : MonoBehaviour
{
    [SerializeField]
    private Sprite arrowUp;

    [SerializeField]
    private Sprite arrowDown;

    private Image image;

    private 
    void Start()
    {
        image = GetComponent<Image>();
        EventManager.StartListening("ElevatorUp", ElevatorUp);
        EventManager.StartListening("ElevatorDown", ElevatorDown);
        EventManager.StartListening("ElevatorStop", ElevatorStop);
    }

    private void ElevatorStop()
    {
        image.sprite = null;
    }

    private void ElevatorUp()
    {
        image.sprite = arrowUp;
    }

    private void ElevatorDown()
    {
        image.sprite = arrowDown;
    }
}
