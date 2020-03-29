using UnityEngine;
using System.Collections;
using Mirror;

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
        gameName = "Symbol Game";
        CreateResult();
        status = GameStatus.Started;
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
}
