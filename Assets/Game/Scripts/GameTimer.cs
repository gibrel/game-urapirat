using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    private PlayerPreferences playerPreferences;
    private float gameTime;

    public float GameTime => gameTime;
    public float LackingGameTime => gameTime - Time.time;

    public bool TimeHasRunOut => LackingGameTime <= 0;

    private void Awake()
    {
        playerPreferences = GameObject.FindGameObjectWithTag("PlayerPreferences").GetComponent<PlayerPreferences>();
    }


    private void Start()
    {
        gameTime = playerPreferences.GameTime * 60f;

        if (gameTime is not >= 60f or not <= 180f)
        {
            Debug.LogError("Could not load game time as expected");
            gameTime = 60f;
        }
    }

}
