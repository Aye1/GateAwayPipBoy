using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MazeGameData : GameData
{
    [SyncVar]
    public Vector3 position;

    public float moveSpeed = 0.5f;

    public override void CreateControls()
    {
        List<MazeControlDirection> directions = new List<MazeControlDirection>() { MazeControlDirection.Left, MazeControlDirection.Up, MazeControlDirection.Down, MazeControlDirection.Right };
        List<MazeGameControlData> controls = new List<MazeGameControlData>();
        foreach(MazeControlDirection direction in directions)
        {
            MazeGameControlData controlData = (MazeGameControlData) GameManager.Instance.CreateControlData(GetGameType());
            controlData.direction = direction;
            controlData.SetMainGameIdentity(netIdentity);
            controls.Add(controlData);
        }
        // TODO: send only to specific players
        GameManager.Instance.SendControlsRoundRobin(controls, CustomNetworkManager.Instance.ConnectedPlayers);
    }

    public override DisplayType GetDisplayType()
    {
        return DisplayType.TabletOnly;
    }

    public override string GetGameName()
    {
        return "3D Maze";
    }

    public override GameType GetGameType()
    {
        return GameType.MazeGame;
    }

    public override void InitGame()
    {
        
    }

    public void SendInput(MazeControlDirection direction)
    {
        Vector3 move = Vector3.zero;
        switch(direction)
        {
            // Up and Down are reversed (Y axis)
            case MazeControlDirection.Down:
                move += Vector3.up;
                break;
            case MazeControlDirection.Up:
                move += Vector3.down;
                break;
            case MazeControlDirection.Left:
                move += Vector3.left;
                break;
            case MazeControlDirection.Right:
                move += Vector3.right;
                break;
        }
        position = Vector3.MoveTowards(position, position + move * moveSpeed, 1.0f);
    }
}
