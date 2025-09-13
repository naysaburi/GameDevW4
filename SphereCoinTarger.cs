using UnityEngine;

public class SphereCoinTarget : MonoBehaviour
{
    [HideInInspector] public SphereSpawner spawner;
    private ScoreManager scoreManager;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tambah skor
            if (scoreManager != null)
            {
                scoreManager.AddScore(1);
            }

            // Beritahu spawner coin ini diambil
            if (spawner != null)
            {
                spawner.CoinCollected(gameObject);
            }

            // Hapus coin dengan delay agar tidak error MissingReference
            Destroy(gameObject, 0.01f);
        }
    }
}
