using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    enum CarType{
        Rider,
        Police,
        Hole,
        Bus,
        Car
    }

    [SerializeField]
    private GameObject[] carPrefabs;

    private Line line;

    private void Awake()
    {
        line = EldenGameManager.Instance.Line;
    }

    private void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
            GameObject car = Instantiate(carPrefabs[(int)CarType.Car], new Vector3(line.List[Random.Range(0, line.List.Length)].transform.position.x, 6, 0), Quaternion.identity);
            car.transform.SetParent(null);
        }
    }
}
