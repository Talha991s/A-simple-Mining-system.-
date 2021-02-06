using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo 
{
    private static int score = 0;
    private static int scans = 6;
    private static int extractions = 3;
    private static int tipIndex = 0;
    private static bool scanning = true;
    private static bool canExtract = true;

    public delegate void EndGame();
    public static event EndGame GameOver;

    private static string[] tips =
    {
        "You can use the toggle at the bottm right to switch between extraction and scan mode.",                   //0
        "Scanning exposes the surrounding tiles as well.",                                                      //1
        "When you extract you get the points of the tile, but it reduces the value of the surrounding tiles.",  //2
        "You're out of scans, you can only extract now!",                                                       //3
        "You're out of extractions! That's the game."                                                           //4
    };

    public static int Score
    {
        get { return score; }
        set { score = value; }
    }

    public static int Scans
    {
        get { return scans; }
        set
        {
            TipIndex = 1;
            scans = value;
            if (scans <= 0)
            {
                TipIndex = 3;
                Scanning = false;
            }
        }
    }

    public static bool Scanning
    {
        get { return scanning; }
        set
        {
            scanning = scans > 0 ? value : false;
        }
    }

    public static int Extractions
    {
        get { return extractions; }
        set
        {
            TipIndex = 2;
            extractions = extractions > 0 ? value : 0;
            canExtract = extractions > 0;
        }
    }

    public static bool CanExtract
    {
        get { return canExtract; }
    }

    public static string Tip
    {
        get { return tips[tipIndex]; }
    }

    public static int TipIndex
    {
        get { return tipIndex; }
        set { tipIndex = Mathf.Clamp(value, tipIndex, tips.Length - 1); }
    }

    public static void EndTheGame()
    {
        if (GameOver != null)
        {
            GameOver();
        }
    }
}
