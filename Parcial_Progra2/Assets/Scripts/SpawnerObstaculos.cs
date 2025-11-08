using UnityEngine;
using System.Collections;

public class SpawnerObstaculos : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject[] obstaculosPrefabs;
    public float intervaloSpawner = 2f;
    public float veloObstaculos = 5f;

    [Header("Posiciones de Spawn")]
    public Vector3 topPosition = new Vector3(15, 2, 0);
    public Vector3 midPosition = new Vector3(15, 0, 0);
    public Vector3 downPosition = new Vector3(15, -1, 0);

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(obstaculosSpawners());
    }

    IEnumerator obstaculosSpawners()
    {
        while (true)
        {
            yield return new WaitForSeconds(intervaloSpawner);

            // Elegir tipo de obstaculo aleatorio (0:Top, 1:Mid, 2:Down)
            int obstacleType = Random.Range(0, 3);
            Vector3 spawnPosition = GetSpawnPosition(obstacleType);

            GameObject obstacle = Instantiate(obstaculosPrefabs[obstacleType], spawnPosition, Quaternion.identity);

            StartCoroutine(moverObstaculos(obstacle));
        }
    }

    Vector3 GetSpawnPosition(int type)
    {
        switch (type)
        {
            case 0: return topPosition;
            case 1: return midPosition;
            case 2: return downPosition;
            default: return midPosition;
        }
    }

    IEnumerator moverObstaculos(GameObject obstacle)
    {
        while (obstacle != null && IsObstacleVisible(obstacle))
        {
            obstacle.transform.Translate(Vector3.left * veloObstaculos * Time.deltaTime);
            yield return null;
        }

        // Destruir cuando salga de pantalla
        if (obstacle != null)
        {
            Destroy(obstacle);
            GameManager.instance.AddScore(1);
        }
    }

    bool IsObstacleVisible(GameObject obstacle)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(obstacle.transform.position);
        return screenPoint.x > -0.1f; // Margen para que desaparezca completamente
    }
}
