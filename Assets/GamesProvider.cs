using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamesProvider : MonoBehaviour
{
    public List<FakeGame> games;
    public Transform gamesHolder;
    public static GamesProvider Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void CreateGame(GameData data)
    {
        if(games.Any())
        {
            int selectedIndex = 0;
            FakeGame createdGame = Instantiate(games[selectedIndex], Vector3.zero, Quaternion.identity, gamesHolder);
            createdGame.gameData = data;
            createdGame.transform.localPosition = Vector3.zero;
        }
    }
}
