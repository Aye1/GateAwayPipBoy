using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameInfoPanel : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _statusText;
#pragma warning restore 0649

    public GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameData != null)
        {
            _nameText.text = gameData.gameName;
            _statusText.text = gameData.status.ToString();
        }
        else
        {
            _nameText.text = "No game launched";
            _statusText.text = "";
        }
    }
}
