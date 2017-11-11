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



}
