using UnityEngine;

public class MazeGameView : GameView
{

#pragma warning disable 0649
    [SerializeField] public Material waveMaterial;
#pragma warning restore 0649

    public MazeGameData MazeData
    {
        get { return (MazeGameData)GameData; }
    }

    protected override void OnGameStatusChanged(GameStatus newStatus)
    {
        //TODO
    }

    private void Update()
    {
        Vector3 position = MazeData.position;
        waveMaterial.SetVector("_PlayerPos", position);
    }
}
