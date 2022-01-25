using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParcelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject parcelPrefab;

    [SerializeField]
    private Transform spawnPoint;


    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            GameObject parcel = Instantiate(parcelPrefab, spawnPoint);
        }
    }
}
