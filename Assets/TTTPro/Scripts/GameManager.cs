using System.Collections;
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
	public enum eGameState
	{
		none,
		play,
		pause,
		gameOver
	}
	public eGameState currState;
    public eGameMode currGameMode;
    public eTurn currTurn;
    public static GameManager instance;
    public Sprite[] marker;
	void Awake () {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad (this.gameObject);

		} else {
			DestroyImmediate(this.gameObject);
			return;
		}

		Screen.SetResolution (400,600,false);
	}
    void Start()
    {
//        InputHandler.instance.GameStart();

    }
    public void GameStart() {
        InputHandler.instance.GameStart();
	}

}
