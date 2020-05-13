using Mirror;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class SymbolGameData : GameData
{
    public int numberOfSymbols = 8;

    [SyncVar]
    public string result;

    [SyncVar]
    public string currentText;
                                    
    public string clientPartialResult = "12345678";
    private List<string> partialResults;

    public delegate void SymbolSent(SymbolGameData data, char symbol);
    public static SymbolSent OnSymbolSent;

    public override void InitGame()
    {
        CreateResult();
        SetStatus(GameStatus.Started);
    }

    public override GameType GetGameType()
    {
        return GameType.SymbolGame;
    }

    public override string GetGameName()
    {
        return "Symbols Game";
    }

    public override DisplayType GetDisplayType()
    {
        return DisplayType.TabletAndPhone;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        // We can't send the partial result until the object is created on the client side
        // Only objects with this client authority can call commands, that's why we go through the Player
        MirrorHelpers.GetClientLocalPlayer(netIdentity).AskForSymbolPartialResult();
    }

    private void CreateResult()
    {
        result = "";
        string possibleChars = GameManager.allSymbols;
        for(int i = 0; i < numberOfSymbols; i++)
        {
            char symbol = CreateSymbol(possibleChars);
            result += symbol;
            // We don't want twice the same character
            string toRemove = symbol.ToString();
            possibleChars = possibleChars.Replace(toRemove, string.Empty);
        }
    }

    private char CreateSymbol(string possibleChars)
    {
        int index = Alea.GetInt(0, possibleChars.Length);
        return possibleChars[index];
    }

    public void AddSymbol(char symbol)
    {
        // Go back to empty state
        if (currentText.Length == result.Length)
        {
            currentText = "";
            SetStatus(GameStatus.Started);
        }
        else
        {
            currentText += symbol;
            if (currentText.Length == result.Length)
            {
                if (currentText == result)
                {
                    SetStatus(GameStatus.Won);
                }
                else
                {
                    SetStatus(GameStatus.Failed);
                }
            }
        }
    }

    public override void CreateControls()
    {
        List<SymbolControlData> controls = new List<SymbolControlData>();
        // Shuffling the characters order
        List<char> possibleSymbolsShuffled = result.ToCharArray().OrderBy(x => Guid.NewGuid()).ToList();

        while (possibleSymbolsShuffled.Count > 0)
        {
            char nextChar = possibleSymbolsShuffled[0];
            possibleSymbolsShuffled.RemoveAt(0);
            SymbolControlData data = (SymbolControlData)GameManager.Instance.CreateControlData(GameType.SymbolGame);
            data.SetMainGameIdentity(netIdentity);
            data.symbol = nextChar;
            controls.Add(data);
        }

        Dictionary<GameControlData, Player> repartition = GameManager.Instance.SendControlsRoundRobin(controls, playerIdentities);
        CreatePartialResults();
    }

    private void CreatePartialResults()
    {
        string charsToDistribute = result;
        int nbPlayers = playerIdentities.Count;
        partialResults = new List<string>();

        for (int i = 0; i < nbPlayers; i++)
        {
            partialResults.Add(CreateEmptyResult());
        }

        int k = 0;
        while(charsToDistribute.Length > 0)
        {
            int removedIndex = Alea.GetInt(0, charsToDistribute.Length);
            char symbol = charsToDistribute[removedIndex];
            charsToDistribute = charsToDistribute.Remove(removedIndex, 1);
            int realPos = result.IndexOf(symbol);
            int id = k % nbPlayers;
            partialResults[id] = ReplaceCharAtPos(partialResults[id], realPos, symbol);
            k++;
        }
    }

    private string CreateEmptyResult()
    {
        string res = "";
        for(int i = 0; i < result.Length; i++)
        {
            res += "_";
        }
        return res;
    }

    private string ReplaceCharAtPos(string original, int pos, char newChar)
    {
        char[] charArray = original.ToCharArray();
        charArray[pos] = newChar;
        return new string(charArray);
    }

    public void SendPartialResult(NetworkIdentity identity)
    {
        if (partialResults.Count > 0) {
            string result = partialResults[0];
            partialResults.RemoveAt(0);
            TargetSendPartialResult(identity.connectionToClient, result);
        } else
        {
            Debug.LogError("No partial result remaining - too many players requested");
        }
    }

    [TargetRpc]
    public void TargetSendPartialResult(NetworkConnection target, string result)
    {
        Debug.Log("Target RPC");
        clientPartialResult = result;
    }

    [Command]
    public void CmdAskPartialResult(NetworkIdentity identity)
    {
        SendPartialResult(identity);
    }
}
