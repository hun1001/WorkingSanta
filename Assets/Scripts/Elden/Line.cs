using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] Transform _line1;
    [SerializeField] Transform _line2;
    [SerializeField] Transform _line3;
    [SerializeField] Transform _line4;

    public Transform[] List { get { return new Transform[] { _line1, _line2, _line3, _line4 }; } }
}
