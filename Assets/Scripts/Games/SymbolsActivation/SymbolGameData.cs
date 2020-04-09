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

    public delegate void SymbolSent(SymbolGameData data, char symbol);
    public static SymbolSent OnSymbolSent;

    public override void InitGame()
    {
        CreateResult();
        status = GameStatus.Started;
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
            status = GameStatus.Started;
        }
        else
        {
            currentText += symbol;
            if (currentText.Length == result.Length)
            {
                if (currentText == result)
                {
                    status = GameStatus.Won;
                }
                else
                {
                    status = GameStatus.Failed;
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
        //CreatePartialResults(repartition);
    }

    private void CreatePartialResults(Dictionary<GameControlData, Player> repartition)
    {
        
    }

    private string CreateResultWithoutCharacters(List<char> charsToRemove)
    {
        string res = result;
        foreach(char charToRemove in charsToRemove)
        {
            res = res.Replace(charToRemove, '_');
        }
        return res;
    }
}
