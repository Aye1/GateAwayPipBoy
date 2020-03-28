using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamesProvider : MonoBehaviour
{
    public List<GameView> games;
    public Transform gamesHolder;
    public static GamesProvider Instance { get; private set; }

    public SymbolGameView symbolGameView;

#pragma warning disable 0649
    [SerializeField] private SymbolControlView _symbolActivatorUI;
    //TODO: add main symbol game view
#pragma warning restore 0649

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        } else
        {
            Destroy(gameObject);
        }
    }

    public void CreateGame(GameData data)
    {
        if (data is SymbolGameData)
        {
            SymbolGameView createdGame = Instantiate(symbolGameView, Vector3.zero, Quaternion.identity, gamesHolder);
            createdGame.gameData = data;
            createdGame.transform.localPosition = Vector3.zero;
        }
        // TODO: remove someday
        else if (games.Any())
        {
            int selectedIndex = 0;
            GameView createdGame = Instantiate(games[selectedIndex], Vector3.zero, Quaternion.identity, gamesHolder);
            createdGame.gameData = data;
            createdGame.transform.localPosition = Vector3.zero;
        }
    }

    public void CreateSymbolControl(SymbolControlData data)
    {
        Transform parentTransform = FindObjectOfType<SymbolGameView>().GetSymbolsControlHolder();
        SymbolControlView createdSymbolActivator = Instantiate(_symbolActivatorUI, Vector3.zero, Quaternion.identity, parentTransform);
        createdSymbolActivator.SymbolData = data;
        createdSymbolActivator.transform.localPosition = Vector3.zero;
    }
}
