using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private float timeToWait = 1f;
    [SerializeField] private Animator[] transitions;

    private PlayerPreferences playerPreferences;

    private void Awake()
    {
        playerPreferences = GameObject.FindGameObjectWithTag("PlayerPreferences").GetComponent<PlayerPreferences>();
    }

    private void SavePreferences()
    {
        playerPreferences.SavePlayerPreferences();
    }

    public void ReloadLevel()
    {
        SavePreferences();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
    }

    public void LoadNextLevel()
    {
        SavePreferences();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousLevel()
    {
        SavePreferences();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex - 1));
    }

    public void QuitGame()
    {
        SavePreferences();
        #if !UNITY_EDITOR
        StartCoroutine(QuitApplication());
        #endif
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        GetRandomTransition().SetTrigger("Start");

        yield return new WaitForSeconds(timeToWait);

        SceneManager.LoadScene(levelIndex);
    }

    private IEnumerator QuitApplication()
    {
        GetRandomTransition().SetTrigger("Start");

        yield return new WaitForSeconds(timeToWait);

        Application.Quit();
    }

    private Animator GetRandomTransition()
    {
        return transitions[Random.Range(0, transitions.Length)];
    }
}
