using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private bool once = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(gameObject.name.Contains("Rider")&&!once)
            {
                transform.parent.DOMoveX(collision.gameObject.transform.position.x, 0.5f);
                once = true;
            }
        }
    }
}
