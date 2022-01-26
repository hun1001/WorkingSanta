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

    void Start()
    {
        EventManager.StartListening("ElevatorUp", ElevatorUp);
        EventManager.StartListening("ElevatorDown", ElevatorDown);
        EventManager.StartListening("ElevatorStop", ElevatorStop);
    }

    private void ElevatorStop()
    {
        image = GetComponent<Image>();
        image.sprite = null;
    }

    private void ElevatorUp()
    {
        image = GetComponent<Image>();
        image.sprite = arrowUp;
    }

    private void ElevatorDown()
    {
        image = GetComponent<Image>();
        image.sprite = arrowDown;
    }

    private void OnDestroy()
    {
        EventManager.StopListening("ElevatorUp", ElevatorUp);
        EventManager.StopListening("ElevatorDown", ElevatorDown);
        EventManager.StopListening("ElevatorStop", ElevatorStop);
    }
}
