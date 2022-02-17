using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Danger : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name.Contains("Hole"))
        {
            image.enabled=true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Contains("Hole"))
        {
            image.enabled = false;
        }
    }
}
