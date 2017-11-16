using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
	public static UIHandler instance;
	public GameObject gamePlayUI;
	public GameObject menuUI;
	public GameObject loadng;
	public GameObject requestPanel;
	public GameObject tableInfo;

	public GameObject hud;
	public GameObject FriendsList;
	public Text loginTxt;
	public string requestID;

	public Text gameOverTxt;
	public GameObject gameOverPnl;
    
	public Clock myClock;
	public Clock friendClock;


	public Button startGamePlay;
	public GameObject addFriendInGamePlay;
	public GameObject frndPic;
	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
			instance = this;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void StartGame ()
	{
		gamePlayUI.SetActive (true);
		menuUI.SetActive (false);
		GameManager.instance.GameStart ();
	}

	public void PlayerIDSelecte(int a)
	{
		if (a == 1) {
			ConnectionManager.Instance.myID = "1";
			ConnectionManager.Instance.friedID = "2";
		} else {
			ConnectionManager.Instance.myID = "2";
			ConnectionManager.Instance.friedID = "1";
		}
	}

	public void OnLoginClicked ()
	{
		loginTxt.text = "Loading....";
		ConnectionManager.Instance.MakeConnection ();
	}

	public void SignalRConnectionDone ()
	{
		loadng.SetActive (false);
		menuUI.SetActive (true);
	}

	public void MenuPage()
	{
		Debug.Log ("Replay");
		menuUI.SetActive (true);
		tableInfo.SetActive (false);
		loadng.SetActive (false);
		gamePlayUI.SetActive(false);
		loadng.SetActive(false);
		requestPanel.SetActive(false);
		tableInfo.SetActive(false);
		hud.SetActive (false);
		gameOverPnl.SetActive (false);
		startGamePlay.transform.parent.gameObject.SetActive (true);
		startGamePlay.interactable = false;

	}

	public void CreateTable ()
	{
		ConnectionManager.Instance.OnSendRequest (requestID);
	}

	public void ShowTableInfo ()
	{
		tableInfo.SetActive (true);
		loadng.SetActive (false);
		menuUI.SetActive (false);
	}

	public void OnCancelTableInfo ()
	{
		tableInfo.SetActive (false);
		loadng.SetActive (false);
		menuUI.SetActive (true);
	}

	public void AcceptChallange (bool isAcc)
	{
		if (isAcc) {
			UIHandler.instance.gamePlayUI.SetActive (true);
			UIHandler.instance.menuUI.SetActive (false);
			UIHandler.instance.loadng.SetActive (false);
			UIHandler.instance.hud.SetActive (true);
			frndPic.SetActive (true);
			startGamePlay.transform.parent.gameObject.SetActive (false);
			addFriendInGamePlay.SetActive (false);
	
			TTTPlayerManager.instace.curPlayer = TTTPlayerManager.ePlayer.two;
			requestPanel.SetActive (false);
			GameManager.instance.currGameMode = GameManager.eGameMode.onlineMultiPlayer;
			GameManager.instance.currState = GameManager.eGameState.play;
			ConnectionManager.Instance.IacceptChallage ();
		}
	}


	public void OpponentAcptedChallage ()
	{
		startGamePlay.interactable = true;
		frndPic.SetActive (true);
		addFriendInGamePlay.SetActive (false);
	}

	public void StartGamePlay ()
	{
		startGamePlay.transform.parent.gameObject.SetActive (false);
		GameManager.instance.currGameMode = GameManager.eGameMode.onlineMultiPlayer;
		GameManager.instance.currState = GameManager.eGameState.play;
		ConnectionManager.Instance.OnServerGameStart ();
		UIHandler.instance.friendClock.ResetClock ();
		UIHandler.instance.myClock.PlayClock ();
	}

	public void OnGameOver (int isWin)
	{
		gameOverPnl.SetActive (true);
		if (TTTPlayerManager.instace.curPlayer == TTTPlayerManager.ePlayer.one) {
			if (isWin == 1) {
				gameOverTxt.text = "You Win";
			} else if (isWin == 2) {
				gameOverTxt.text = "You Lose";

			} else if (isWin == 3) {
				gameOverTxt.text = "Draw";

			}
		} else if (TTTPlayerManager.instace.curPlayer == TTTPlayerManager.ePlayer.two) {
			if (isWin == 1) {
				gameOverTxt.text = "You Los";
			} else if (isWin == 2) {
				gameOverTxt.text = "You win";

			} else if (isWin == 3) {
				gameOverTxt.text = "Draw";

			}
		}
	}

	public void Replay()
	{
		//Application.LoadLevel (0);
		myClock.ResetClock();
		friendClock.ResetClock ();
		GameManager.instance.ResetData();
		MenuPage ();
	}

	public void DeactiveFriendsList ()
	{
		FriendsList.SetActive (false);
	}

	public void ActiveFriendsList ()
	{
		FriendsList.SetActive (true);
		UsersFriendsController.Instance.DisplayUserFriends ();
	}
}
