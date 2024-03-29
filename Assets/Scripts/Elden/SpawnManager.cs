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
    [SerializeField]
    private GameObject spawnPoint;
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
            yield return new WaitForSeconds(Random.Range(1.2f, 1.5f));
            GameObject car = Instantiate(carPrefabs[(int)CarType.Rider], new Vector3(line.List[Random.Range(0, line.List.Length)].transform.position.x, 6, 0), Quaternion.identity);
            car.transform.SetParent(null);
            car.transform.SetParent(spawnPoint.transform);
            car.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
