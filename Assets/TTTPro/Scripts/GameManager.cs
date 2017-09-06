﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum eGameMode
    {
        none,
        cpu,
        onlineMultiPlayer,
        offlineMultiplayer
    }

    public enum eTurn
    {
        none,
        one,
        two
    }
    public eGameMode currGameMode;
    public eTurn currTurn;
    public static GameManager instance;
    public Sprite[] marker;
    public List<GameObject> inputTrackerBtn = new List<GameObject>();
	void Awake () {
        if (instance == null)
            instance = this;
	}
    void Start()
    {
//        InputHandler.instance.GameStart();

    }
    public void GameStart() {
        InputHandler.instance.GameStart();
	}

}