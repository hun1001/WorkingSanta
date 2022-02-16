using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EldenGameManager : MonoSingleton<EldenGameManager>
{
    public Line Line { get { return _line; } }

    [SerializeField] Character _character;
    [SerializeField] Line _line;

    
}
