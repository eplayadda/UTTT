using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class TTTPlayerManager :MonoBehaviour  {
    public enum ePlayer
    {
        none,
        one,
        two
    }

    public ePlayer curPlayer;

    public static TTTPlayerManager instace;
//    [SyncVar]
    public int count;
	// Use this for initialization
	void Awake () {
        if (instace == null)
            instace = this;
	}

	public void ResetData()
	{
		curPlayer = ePlayer.none;
		count =0;
	}
}
