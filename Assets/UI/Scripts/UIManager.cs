using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
	private static UIManager _instance;

	public static UIManager Instance {
		get {
			if (_instance == null)
				_instance = GameObject.FindObjectOfType<UIManager> ();
			return _instance;
		}

	}

	public UIController uiController;

	public GameObject MainMenu;
	public GameObject FriendsListPanel;
	public GameObject InviableFriendsPanel;
	public GameObject SettingsPanel;
	public GameObject CreateGamePlayPanel;
	public GameObject SelectGameplayPanel;
	public GameObject ExitPanel;
	public GameObject CreateTablePanel;
	public GameObject StorePanel;
	public GameObject UserProfile;

	public StoreManager storeManager;

	void Awake ()
	{
		uiController = new UIController ();	
	}

	//	Use this for initialization
	void Start ()
	{
		
	}

	public void OnClickFrinds ()
	{
		uiController.ActiveUIObject (FriendsListPanel);
		UIHandler.instance.ActiveFriendsList ();
	}

	public void OnClickSetting ()
	{
		uiController.ActiveUIObject (SettingsPanel);
	}

	public void OnClickMode ()
	{
		uiController.ActiveUIObject (CreateGamePlayPanel);
	}

	public void OnClickCreateTable ()
	{
		uiController.ActiveUIObject (CreateTablePanel);
		uiController.ActiveUIObject (UIHandler.instance.gamePlayUI);
		uiController.DeactiveUIObject (CreateGamePlayPanel);
		UIHandler.instance.CreateTable ();
	}

	public void OnClickUserProfile ()
	{
		uiController.ActiveUIObject (UserProfile);
	}

	public void OnClickBuyCoin ()
	{
		uiController.ActiveUIObject (StorePanel);
	}

	public void OnClickInviteFriends ()
	{
		Debug.Log ("OnClickInviteFriends");
		uiController.ActiveUIObject (InviableFriendsPanel);
		UIHandler.instance.ActiveFriendsList ();
	}

	public void OnCloseFriendsList ()
	{
		uiController.DeactiveUIObject (FriendsListPanel);
	}

	public void OnCloseSettings ()
	{
		uiController.DeactiveUIObject (SettingsPanel);
	}

	public void OnCloseCreateGamePlayPanel ()
	{
		uiController.DeactiveUIObject (CreateGamePlayPanel);
	}

	public void OnCloseCreateTable ()
	{
		uiController.DeactiveUIObject (CreateTablePanel);
	}

	public void OnCloseUserProfile ()
	{
		uiController.DeactiveUIObject (UserProfile);
	}

	public void OnCloseStore ()
	{
		uiController.DeactiveUIObject (StorePanel);
	}

	public void OnCloseInvitableFriends ()
	{
		uiController.DeactiveUIObject (InviableFriendsPanel);
	}

}
