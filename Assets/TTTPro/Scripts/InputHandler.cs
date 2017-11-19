using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class InputHandler : NetworkBehaviour  {
    public Text txt;
	// Use this for initialization
    public static InputHandler instance;
    public RectTransform inputTracker;
    public List<RectTransform> inputTrackerPos;
    public List<TicTacToe> tictaktoe = new List<TicTacToe>();
    Vector3 final = Vector3.one;
    public int selectedGrid;
    public int selectedTicTTTgrid;
	public TicTacToe inputTrackerTi;
    public TicTacToe gridParent;
//    public MyNetworkPlayer myNetworkPlayer;
    public enum eInputType
    {
        none,
        grid,
        input
    };
    public eInputType currInputType;
    void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
//        GameStart();
    }

    public void GameStart()
    {
        currInputType = eInputType.grid;
    }
    public void GridSelected(int pGrid)
    {
		if (GameManager.instance.currState != GameManager.eGameState.play)
			return;
        if (GameManager.instance.currGameMode == GameManager.eGameMode.onlineMultiPlayer)
        {
			if (GameManager.instance.currTurn.ToString () == TTTPlayerManager.instace.curPlayer.ToString ()) {
//                myNetworkPlayer.GridSelected(pGrid);
//			GridSelected(pGrid);
				Debug.Log ("input");
				TTTPlayerManager.instace.count = pGrid;
                ConnectionManager.Instance.OnInPutDone(pGrid,0);
				UnetGridSelected (pGrid);
			}
        }
        else if (GameManager.instance.currGameMode == GameManager.eGameMode.offlineMultiplayer)
        {
            UnetGridSelected(pGrid);
        }
        else if (GameManager.instance.currGameMode == GameManager.eGameMode.cpu)
        {
        }
    }

    public void UnetGridSelected(int pGrid)
    {
		Debug.Log ("dfgfdd");

        selectedGrid = pGrid;
        inputTracker.gameObject.SetActive(true);
        OpenInputTracker(pGrid);
        inputTrackerTi.SetDataToInputTracker(tictaktoe[selectedGrid -1].gridData);
        LeanTween.scale(inputTracker, final, .2f);
    }


    public void OnInputTaken(int pGrid)
    {
		if (GameManager.instance.currState != GameManager.eGameState.play)
			return;
        if (GameManager.instance.currGameMode == GameManager.eGameMode.onlineMultiPlayer)
        {
            if (GameManager.instance.currTurn.ToString() == TTTPlayerManager.instace.curPlayer.ToString())
            {
//                myNetworkPlayer.OnInputTaken(pGrid);
                TTTPlayerManager.instace.count = pGrid;
                ConnectionManager.Instance.OnInPutDone(pGrid, 1);

                UnetOnInputTaken(pGrid);
            }
        }
        else if (GameManager.instance.currGameMode == GameManager.eGameMode.offlineMultiplayer)
        {
            UnetOnInputTaken(pGrid);
        }
        else if (GameManager.instance.currGameMode == GameManager.eGameMode.cpu)
        {
        }
    }

    public void UnetOnInputTaken(int pGrid)
    {
		Debug.Log ("Input Recied");
        selectedTicTTTgrid = pGrid;
        tictaktoe[selectedGrid - 1].SetInputData(pGrid);
        LeanTween.scale(inputTracker, new Vector2(.1f,.1f), .2f).setOnComplete(InputTrackerClosed);
    }

    void OpenInputTracker(int pGrid)
    {
        pGrid = pGrid - 1;
        inputTracker.position = inputTrackerPos[pGrid].position;
        float p = pGrid / 3;
        inputTracker.pivot = new Vector2(0.5f*(pGrid % 3),.5f*(2 - p));
    }

    void InputTrackerClosed()
    {
        inputTracker.gameObject.SetActive(false);
        if(GameManager.instance.currTurn == GameManager.eTurn.one)
            GameManager.instance.currTurn = GameManager.eTurn.two;
        else if(GameManager.instance.currTurn == GameManager.eTurn.two)
            GameManager.instance.currTurn = GameManager.eTurn.one;
        SetTTTInterable();
    }

    void InputTrackerCancel()
    {
        inputTracker.gameObject.SetActive(false);
      
    }

    void SetTTTInterable()
    {
        bool isEmptyGrid = tictaktoe[selectedTicTTTgrid - 1].IsIntetactable();

        for (int i = 0; i < tictaktoe.Count; i++)
        {
            tictaktoe[i].SetInterable(true);
            if (isEmptyGrid)
            {
                if (selectedTicTTTgrid == i + 1)
                {
                    tictaktoe[i].SetInterable(true);
                }
                else
                {
                    tictaktoe[i].SetInterable(false);
                }
            }

        }
        if(!isEmptyGrid)
            tictaktoe[selectedTicTTTgrid -1].SetInterable(false);
    }

    public void OnInputTracker_Closed()
    {
        if (GameManager.instance.currGameMode == GameManager.eGameMode.onlineMultiPlayer)
        {
			if (GameManager.instance.currTurn.ToString () == TTTPlayerManager.instace.curPlayer.ToString ()) {
				ConnectionManager.Instance.OnInPutDone(0,2);
				UnetOnInputTracker_Closed();
			}
//                myNetworkPlayer.OnInputTracker_Closed();
        }
        else if (GameManager.instance.currGameMode == GameManager.eGameMode.offlineMultiplayer)
        {
            UnetOnInputTracker_Closed();
        }
        else if (GameManager.instance.currGameMode == GameManager.eGameMode.cpu)
        {
        }

    }

    public void UnetOnInputTracker_Closed()
    {
		if (GameManager.instance.currTurn.ToString () == TTTPlayerManager.instace.curPlayer.ToString ()) {
			LeanTween.scale (inputTracker, new Vector2 (.1f, .1f), .2f).setOnComplete (InputTrackerCancel);
		}
    }

	public void OnGameStartOnServer()
	{
		UIHandler.instance.friendClock.PlayClock ();
		UIHandler.instance.myClock.ResetClock ();
	}

    public void CheckGameOver()
    {
        
    }

	public void ResetData()
	{
		for (int i = 0; i < tictaktoe.Count; i++) {
			tictaktoe [i].ResetData ();
			tictaktoe[i].GetComponent<Button>().interactable = true;
		}
		inputTrackerTi.ResetData ();
		gridParent.ResetData ();
	}
   
}
