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
