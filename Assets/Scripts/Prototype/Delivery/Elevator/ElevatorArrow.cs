using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorArrow : MonoBehaviour
{
    void Start()
    {
        EventManager.StartListening("ElevatorUp", ElevatorUp);
        EventManager.StartListening("ElevatorDown", ElevatorDown);
    }

    private void ElevatorUp()
    {

    }

    private void ElevatorDown()
    {

    }
}
