using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    private float playerX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            if(gameObject.name.Contains("Rider"))
            {
                playerX = collision.gameObject.transform.position.x;
                StartCoroutine(riderMove());
            }
        }
    }

    private IEnumerator riderMove()
    {
        int n = 1;
        float move = 0;
        if(playerX > 0 && gameObject.transform.parent.position.x < 0)
        {
            move = Mathf.Abs(playerX + gameObject.transform.parent.position.x);
            n *= 1;
        }
        else if(playerX < 0 && gameObject.transform.parent.position.x < 0)
        {
            move = Mathf.Abs(playerX - gameObject.transform.parent.position.x);
            n *= -1;
        }
        else if(playerX < 0 && gameObject.transform.parent.position.x > 0)
        {
            move = Mathf.Abs(playerX + gameObject.transform.parent.position.x);
            n *= 1;
        }
        else if (playerX > 0 && gameObject.transform.parent.position.x > 0)
        {
            move = Mathf.Abs(playerX - gameObject.transform.parent.position.x);
            n *= -1;
        }
        move = move * n;
        while (playerX == gameObject.transform.parent.position.x)
        { 
            gameObject.transform.parent.localPosition = new Vector2(gameObject.transform.parent.position.x+move, gameObject.transform.parent.position.y);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
