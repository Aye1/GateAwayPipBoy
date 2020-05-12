using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingBalanceBar : MonoBehaviour
{
    [Header("Image references")]
    public Image bar;
    public Image goalImage;

    [Header("Game settings")]
    public int maxHeight = 100;
    public float currentHeight = 80; // When == 100, bar is entirely filled

    public int GoalMin 
    {
        get { return _goalMin; }
        set 
        {
            if (value != _goalMin)
            {
                _goalMin = value;
                resizeAndPositionGoalImage();
            }
        }
    }
    public int GoalMax
    {
        get { return _goalMax; }
        set
        {
            if (value != _goalMax)
            {
                _goalMax = value;
                resizeAndPositionGoalImage();
            }
        }
    }


    private Color successColor = Color.green, progressColor = Color.red;
    private int _goalMin = 60, _goalMax = 80;

    void Start()
    {
        resizeAndPositionGoalImage();
    }

    void Update()
    {
        Vector3 targetScale = new Vector3(1, currentHeight / (float)maxHeight, 1);
        bar.transform.localScale = targetScale;

        if (currentHeight >= GoalMin && currentHeight <= GoalMax)
            bar.color = successColor;
        else
            bar.color = progressColor;
    }

    void resizeAndPositionGoalImage()
    {
        Rect fullRect = GetComponent<RectTransform>().rect;
        float goalHeight = (GoalMax - GoalMin) * 0.01f * fullRect.height;

        goalImage.rectTransform.sizeDelta = new Vector2(fullRect.width, goalHeight);

        float goalXPosition = goalImage.rectTransform.localPosition.x;
        float goalYPosition = (GoalMin + GoalMax) * 0.5f * 0.01f * fullRect.height - fullRect.height / 2;

        goalImage.rectTransform.localPosition = new Vector3(goalXPosition, goalYPosition, 0);
    }
}
