using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBalanceBar : MonoBehaviour
{
   
    public Image bar;

    public int maxHeight = 100;
    public float currentHeight = 0; // When == 100, bar is entirely filled
    public float decreaseSpeed = 1;

    void Start()
    {

    }

    void Update()
    {
        currentHeight = Math.Max(0, currentHeight - Time.deltaTime * decreaseSpeed);
        Vector3 targetScale = new Vector3(1, currentHeight / (float)maxHeight, 1);
        bar.transform.localScale = targetScale;
        // bar.transform.localScale = Vector3.MoveTowards(bar.transform.localScale, targetScale, 1);
    }

}
