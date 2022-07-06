using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteAlways]
public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private Transform SpawnEnemy;
    [SerializeField] private List<Transform> Points;

    [SerializeField] private GameObject Enemy;
    [Range(1f, 10f)]
    [SerializeField] private float spawnTimer = 2f;
    private float timer;

    //private delegate Transform SpawnHere() = spawnHere;


    private void Start()
    {
        timer = spawnTimer;
        foreach(Transform child in SpawnEnemy)
        {
            Points.Add(child);
        }
    }

    private void Update()
    {
        timer -= Time.deltaTime;   
        if(timer <= 0)
        {
            Spawn();
            timer = spawnTimer;
        }
    }

    private void Spawn()
    {
        GameObject enemy = Instantiate(Enemy, SpawnHere().position, SpawnHere().rotation);


    }


    private Transform SpawnHere()
    {
        int choosePoint = Random.Range(0,Points.Count);

        return Points[choosePoint];
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        Gizmos.color = Color.magenta;
        foreach(Transform child in Points)
        {
            Gizmos.DrawWireSphere(child.position, 0.5f);
        }
    }
}
