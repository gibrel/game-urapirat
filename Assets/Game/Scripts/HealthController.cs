using System.Collections;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private int health;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private float timeToDie = 1f;
    [SerializeField] private bool timeMode = false;

    private SpawnPointManager spawnPointManager;
    private GameTimer gameTimer;
    private PlayerPoints playerPoints;

    public int MaxHealth { get { return maxHealth; } }
    public int Health { get { return health; } }

    private void Awake()
    {
        var gameController = GameObject.FindGameObjectWithTag("GameController");
        spawnPointManager = gameController.GetComponent<SpawnPointManager>();
        gameTimer = gameController.GetComponent<GameTimer>();
        playerPoints = gameController.GetComponent<PlayerPoints>();
    }

    private void Start()
    {
        if (timeMode)
        {
            maxHealth = (int)gameTimer.GameTime;
            healthBar.SetMaximunHealth(maxHealth);
        }
        else
        {
            health = maxHealth;
            healthBar.SetMaximunHealth(maxHealth);
        }
    }

    private void Update()
    {
        if (timeMode)
        {
            health = (int)gameTimer.LackingGameTime;
            healthBar.SetHealth(health);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!timeMode && !gameTimer.TimeHasRunOut)
        {
            health -= damage;
            if (health <= 0)
            {
                health = 0;
                StartCoroutine(SelfDestruct());
            }
            healthBar.SetHealth(health);

            if (!transform.CompareTag("Player"))
            {
                playerPoints.AddPoints(damage);
            }
        }
    }

    private IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(timeToDie);
        if (!gameObject.CompareTag("Player"))
        {
            spawnPointManager.DecreaseObjects();
        }
        Destroy(gameObject);
    }
}
