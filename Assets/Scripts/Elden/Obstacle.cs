using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] Danger Danger1;
    [SerializeField] Danger Danger2;
    [SerializeField] Danger Danger3;

    private float _coolDown = 0;
    
    private void Start()
    {
        _coolDown = 5;
    }
}
