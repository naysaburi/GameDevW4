using UnityEngine;
using System.Collections.Generic;

public class SphereSpawner : MonoBehaviour
{
    public GameObject spherePrefab;   
    public int initialCount = 10;   
    public float spawnRange = 15f;    
    public float groundY = 0.5f;     

    private List<GameObject> coins = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < initialCount; i++)
        {
            SpawnRandom();
        }
    }

    public void SpawnRandom()
    {
        
        float angle = Random.Range(0f, Mathf.PI * 2);

        
        float r = Mathf.Sqrt(Random.Range(0f, 1f)) * spawnRange;

        float x = Mathf.Cos(angle) * r;
        float z = Mathf.Sin(angle) * r;

        Vector3 spawnPos = new Vector3(x, groundY, z);

        GameObject coin = Instantiate(spherePrefab, spawnPos, Quaternion.identity);

        // Hubungkan ke CoinTarget
        SphereCoinTarget coinTarget = coin.GetComponent<SphereCoinTarget>();
        if (coinTarget != null)
        {
            coinTarget.spawner = this;
        }

        coins.Add(coin);
    }

    public void CoinCollected(GameObject coin)
    {
        if (coin != null && coins.Contains(coin))
        {
            coins.Remove(coin);
        }

        
        SpawnRandom();
    }

    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(Vector3.zero, spawnRange);
    }
}
