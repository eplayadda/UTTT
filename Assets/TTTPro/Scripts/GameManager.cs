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
    public List<GameObject> inputTrackerBtn = new List<GameObject>();
	void Awake () {
        if (instance == null)
            instance = this;
		//Screen.SetResolution (400,600,false);
	}
    void Start()
    {
//        InputHandler.instance.GameStart();

    }
    public void GameStart() {
        InputHandler.instance.GameStart();
	}
	public void ResetData()
	{
		currState = eGameState.none;
		currTurn = eTurn.one;
		InputHandler.instance.ResetData ();
	}

}
