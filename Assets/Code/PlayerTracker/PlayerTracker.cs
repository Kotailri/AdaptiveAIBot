using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public GameObject Player;
    public GameObject Bot;

    // Distance
    private float DistanceTrackingTimer;
    private float CurrDistanceTrackingTimer = 0.0f;

    private int NumDistanceSamples = 0;
    private double SampleDistanceSum = 0.0;

    [Space(5.0f)]
    public double AverageDistance = 0.0f;
    public double CurrentDistance = 0.0f;

    public int PlayerHitsLanded = 0;
    public int PlayerHitsMissed = 0;

    public int BotHitsLanded = 0;
    public int BotHitsMissed = 0;

    public int PlayerHitsDuringWander = 0;

    private void Awake()
    {
        Global.playertracker = this;
        DistanceTrackingTimer = GameConfig.c_DistanceTrackingTimer;
        UpdateAverageDistance();
    }

    private void Update()
    {
        #region Timers
        CurrDistanceTrackingTimer += Time.deltaTime;

        #endregion

        #region Timer Events
        if (CurrDistanceTrackingTimer >= DistanceTrackingTimer)
        {
            CurrDistanceTrackingTimer = 0.0f;
            UpdateAverageDistance();
        }

        #endregion

        CurrentDistance = DistanceBetween();
    }

    #region Distance between Player and Bot
    private double DistanceBetween()
    {
        return Vector2.Distance(Player.transform.position, Bot.transform.position);
    }

    private void UpdateAverageDistance()
    {
        SampleDistanceSum += DistanceBetween(); ;
        NumDistanceSamples++;

        AverageDistance = (SampleDistanceSum / (double) NumDistanceSamples);
    }

    #endregion

    public Vector2 GetPlayerPosition()
    {
        return Player.transform.position;
    }

    public Vector2 GetBotPosition()
    {
        return Bot.transform.position;
    }
    
}
