using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersFriendsController : MonoBehaviour
{
	public static UsersFriendsController Instance;

	public FacebookHandler facebookController;


	void Awake ()
	{
		if (Instance == null)
			Instance = this;
	}
	// Use this for initialization
	public void DisplayUserFriends ()
	{
		facebookController.GetFriends ();
	}
	

}
