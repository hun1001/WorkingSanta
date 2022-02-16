using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpeed : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private void Update()
    {
        gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - speed/100);
    }
}
