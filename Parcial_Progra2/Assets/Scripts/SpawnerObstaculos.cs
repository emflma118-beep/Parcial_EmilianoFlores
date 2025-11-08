using UnityEngine;
using System.Collections;

public class SpawnerObstaculos : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject[] obstaclePrefabs; // 0:Top, 1:Mid, 2:Down
    public float spawnInterval = 2f;
    public float obstacleSpeed = 5f;

    [Header("Posiciones de Spawn")]
    public Vector3 topPosition = new Vector3(15, 2, 0);
    public Vector3 midPosition = new Vector3(15, 0, 0);
    public Vector3 downPosition = new Vector3(15, -1, 0);

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Elegir tipo aleatorio (0:Top, 1:Mid, 2:Down)
            int obstacleType = Random.Range(0, 3);
            Vector3 spawnPosition = GetSpawnPosition(obstacleType);

            // Instanciar obstáculo
            GameObject obstacle = Instantiate(obstaclePrefabs[obstacleType], spawnPosition, Quaternion.identity);

            // Mover obstáculo
            StartCoroutine(MoveObstacle(obstacle));
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

    IEnumerator MoveObstacle(GameObject obstacle)
    {
        while (obstacle != null && IsObstacleVisible(obstacle))
        {
            obstacle.transform.Translate(Vector3.left * obstacleSpeed * Time.deltaTime);
            yield return null;
        }

        // Destruir cuando salga de pantalla
        if (obstacle != null)
        {
            Destroy(obstacle);
            // Añadir punto al pasar obstáculo
            GameManager.instance.AddScore(1);
        }
    }

    bool IsObstacleVisible(GameObject obstacle)
    {
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(obstacle.transform.position);
        return screenPoint.x > -0.1f; // Margen para que desaparezca completamente
    }
}
