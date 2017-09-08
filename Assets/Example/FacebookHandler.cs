using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.UI;

public class FacebookHandler : MonoBehaviour
{
	public GameObject FriendPrefab;
	public Transform parentObject;
	//public GameObject panel;
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
			Debug.Log (result.RawResult);
			var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
			// Print current access token's User ID
			ConnectionManager.Instance.myID = aToken.UserId;
			Debug.Log (aToken.UserId);
			GetFriends ();
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
		if (FB.IsLoggedIn) {
			FB.API ("me?fields=id,name,friends.limit(20){first_name,picture}", HttpMethod.GET, this.GetFreindCallback);
		} else {
			Login ();
		}

	}

	void GetFreindCallback (IResult result)
	{
		string resposne = result.RawResult;
		Debug.Log (resposne);
		var data = (Dictionary<string, object>)result.ResultDictionary;
		var tagData = data ["friends"] as Dictionary<string,object>;
		var resultData = tagData ["data"] as List<object>;
		//Debug.Log (tagData ["first_name"].ToString ());
		for (int i = 0; i < resultData.Count; i++) {
			var resultValue = resultData [i] as Dictionary<string, object>;
			var picture = resultValue ["picture"] as Dictionary<string ,object>;
			var picData = picture ["data"] as Dictionary<string,object>;
			string url = picData ["url"].ToString ();
			Debug.Log ("url : " + url);
			GameObject g = Instantiate (FriendPrefab) as GameObject;
			g.SetActive (true);
			g.transform.SetParent (parentObject);
			g.transform.localScale = Vector3.one;
			g.transform.position = Vector3.zero;
			g.GetComponent<FriendsDetails> ().Name.text = resultValue ["first_name"].ToString ();
			Button btn = g.GetComponentInChildren<Button> ();
			Debug.Log (resultValue ["first_name"].ToString () + "  , " + resultValue ["id"].ToString ());
			string id = resultValue ["id"].ToString ();
			AddListener (btn, id);
			if (!string.IsNullOrEmpty (id)) {
				FB.API ("https" + "://graph.facebook.com/" + id + "/picture?width=128&height=128", HttpMethod.GET, delegate(IGraphResult avatarResult) {
					if (avatarResult.Error != null) {
						Debug.Log (avatarResult.Error);
					} else {

						g.GetComponent<FriendsDetails> ().ProfilePic.sprite = Sprite.Create (avatarResult.Texture, new Rect (0, 0, 128, 128), new Vector2 (0.5f, 0.5f));
						;
					}
				});
			}
		}
	}

	private void AddListener (Button btn, string fbID)
	{
		btn.onClick.AddListener (() => SetFriendsId (fbID));
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

	public void SetFriendsId (string id)
	{
		ConnectionManager.Instance.friedID = id;
		Debug.Log ("SetFriendsId : " + id);
		UIHandler.instance.ShowTableInfo ();
		UIHandler.instance.DeactiveFriendsList ();
	}

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
