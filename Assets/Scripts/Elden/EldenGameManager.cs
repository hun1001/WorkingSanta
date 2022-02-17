using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldenGameManager : MonoSingleton<EldenGameManager>
{
    public Line Line { get { return _line; } }
    public CarSpeed CarSpeed { get { return _carSpeed; } }

    [SerializeField] Character _character;
    [SerializeField] Line _line;    
}
