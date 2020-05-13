using UnityEngine;
using System.Collections;

public abstract class GameView : MonoBehaviour
{
    private GameData _gameData;
    public GameData GameData
    {
        get { return _gameData; }
        set
        {
            if (value != _gameData && value != null)
            {
                _gameData = value;
                _gameData.OnStatusChanged += OnGameStatusChanged;
                _gameData.OnGameExit += DestroyView;
            }
        }
    }

    protected abstract void OnGameStatusChanged(GameStatus newStatus);

    private void OnDestroy()
    {
        if(_gameData != null)
        {
            _gameData.OnStatusChanged -= OnGameStatusChanged;
            _gameData.OnGameExit -= DestroyView;
        }
    }

    public void DestroyView()
    {
        GamesViewsManager.Instance.ClearAllViews();
    }
}
