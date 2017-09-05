using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class SSTest : MonoBehaviour
{
	public GameObject FriendPrefab;
	public Transform parentObject;
	public GameObject panel;
	// Use this for initialization
	void Start ()
	{
		if (!FB.IsInitialized) {
			FB.Init (onInitComplete, OnHideUnity);
		} else {
			// Already initialized, signal an app activation App Event
			FB.ActivateApp ();
           
		}
	}
	
	// Update is called once per frame
	void onInitComplete ()
	{
		if (FB.IsInitialized) {
			// Signal an app activation App Event
			FB.ActivateApp ();
		} else {
			Debug.Log ("Failed to Initialize the Facebook SDK");
		}

	}

	private void OnHideUnity (bool isGameShown)
	{
		if (!isGameShown) {
			// Pause the game - we will need to hide
			Time.timeScale = 0;
		} else {
			// Resume the game - we're getting focus again
			Time.timeScale = 1;
		}
	}

	public void Login ()
	{
		var perms = new List<string> () { "public_profile", "email", "user_friends" };
		FB.LogInWithReadPermissions (perms, AuthCallback);
		FB.LogInWithPublishPermissions (new List<string> (){ "publish_actions" }, AuthCallback);
	}

	private void AuthCallback (ILoginResult result)
	{
		if (FB.IsLoggedIn) {
			// AccessToken class will have session details
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			Debug.Log (aToken.UserId);
			// Print current access token's granted permissions
			foreach (string perm in aToken.Permissions) {
				Debug.Log (perm);
			}
		} else {
			Debug.Log ("User cancelled login");
		}
	}

	public void GetFriends ()
	{
		FB.API ("me?fields=id,name,friends.limit(10){first_name}", HttpMethod.GET, this.GetFreindCallback);

	}

	void GetFreindCallback (IResult result)
	{
		panel.SetActive (false);
		string resposne = result.RawResult;
		Debug.Log (resposne);
		var data = (Dictionary<string, object>)result.ResultDictionary;
		var tagData = data ["friends"] as Dictionary<string,object>;
		var resultData = tagData ["data"] as List<object>;
		//Debug.Log (tagData ["first_name"].ToString ());
		for (int i = 0; i < resultData.Count; i++) {
			var resultValue = resultData [i] as Dictionary<string, object>;
			GameObject g = Instantiate (FriendPrefab) as GameObject;
			g.SetActive (true);
			g.transform.SetParent (parentObject);
			g.transform.localScale = Vector3.one;
			g.transform.position = Vector3.zero;
			g.GetComponent<Property> ().firendsName.text = resultValue ["first_name"].ToString ();
			Debug.Log (resultValue ["first_name"].ToString () + "  , " + resultValue ["id"].ToString ());
		}
	}

	//	private void OnGUI ()
	//	{
	//		if (GUI.Button (new Rect (100, 100, 100, 50), "Login")) {
	//			Login ();
	//		}
	//
	//		if (GUI.Button (new Rect (100, 200, 100, 50), "GetFriends")) {
	//			GetFriends ();
	//		}
	//
	//		if (GUI.Button (new Rect (100, 300, 100, 50), "Invite")) {
	//			AppRequest ();
	//		}
	//	}

	public void AppRequest ()
	{
		if (FB.IsLoggedIn)
			FB.Mobile.AppInvite (new System.Uri ("https://me.fb/34552008254503"), null, this.RequestCallBack);
	}

	void RequestCallBack (IAppInviteResult result)
	{
		Debug.Log (result.RawResult);
	}
}
