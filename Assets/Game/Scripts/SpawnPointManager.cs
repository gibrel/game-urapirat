using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnObjects;
    [SerializeField] private int simultaneousObjectsAllowed = 10;
    [SerializeField] private float minDistanceToSpawn = 20f;
    [SerializeField] private float timeBetweenSpawns = 5f;

    private PlayerPreferences playerPreferences;
    private Transform playerPosition;
    private GameObject[] spawnPoints;
    private int currentObjects = 0;
    private float timeFromLastSpawn = 0;

    public int CurrentObjectsQuantity { get => currentObjects; }

    public void DecreaseObjects()
    {
        currentObjects--;
        if (currentObjects < 0) currentObjects = 0;
    }

    private void Awake()
    {
        spawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        playerPreferences = GameObject.FindGameObjectWithTag("PlayerPreferences").GetComponent<PlayerPreferences>();
    }

    private void Start()
    {
        int spwnFrq = playerPreferences.SpawnFreq;

        if (spwnFrq < 3 || spwnFrq > 10)
        {
            Debug.LogError("Could not load spawn frequency as expected");
            return;
        }

        timeBetweenSpawns = spwnFrq;
    }

    private void Update()
    {
        if (Time.time > timeFromLastSpawn
            && currentObjects < simultaneousObjectsAllowed
            && spawnObjects.Length > 0
            && spawnPoints.Length > 0)
        {
            SpawnObject();
        }
    }

    private void SpawnObject()
    {
        GameObject spawn = SelectSpawn();

        if (spawn == null) return;

        Vector3 position = SelectSpawnPointPosition();

        if (Mathf.Abs(position.magnitude) >= Mathf.Abs((Vector3.one * 9999).magnitude)) return;

        Instantiate(spawn, position, Quaternion.identity);

        currentObjects++;

        timeFromLastSpawn = Time.time + timeBetweenSpawns;
    }

    private Vector3 SelectSpawnPointPosition()
    {
        Vector3 closestAllowedPoint = Vector3.one * 9999;

        foreach (GameObject spawnPoint in spawnPoints)
        {
            Vector3 position = spawnPoint.transform.position;

            float distanceFromPlayer = Mathf.Abs((playerPosition.position - position).magnitude);

            if (distanceFromPlayer >= minDistanceToSpawn
                && Mathf.Abs(position.magnitude) < Mathf.Abs(closestAllowedPoint.magnitude))
            {
                closestAllowedPoint = position;
            }
        }

        return closestAllowedPoint;
    }

    private GameObject SelectSpawn()
    {
        int random = Random.Range(0, spawnObjects.Length);
        return spawnObjects[random];
    }
}
