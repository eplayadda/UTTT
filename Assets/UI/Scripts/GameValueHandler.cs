using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameConstant
{
	public const int DEFAULT_GAME_AMOUNT = 500;
}

public class GameValueHandler : MonoBehaviour
{
	
	public int gameAmount = GameConstant.DEFAULT_GAME_AMOUNT;

	public Text gameAmountText;
	// Use this for initialization
	void Start ()
	{
		
	}

	public void UpdateGameValue (bool isAdd)
	{
		if (gameAmount >= GameConstant.DEFAULT_GAME_AMOUNT)
			gameAmount = isAdd ? gameAmount += gameAmount : gameAmount -= gameAmount / 2;
		Debug.Log ("gameAmount " + gameAmount);
		gameAmountText.text = System.Convert.ToString (gameAmount);
	}
	

}
