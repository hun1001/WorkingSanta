using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parcel : MonoBehaviour
{
    [SerializeField]
    private Sprite[] parcelSprite;

    private Image image;

    private int address;

    void Start()
    {
        image = GetComponent<Image>();
        image.sprite = parcelSprite[Random.Range(0, parcelSprite.Length)];
    }

}
