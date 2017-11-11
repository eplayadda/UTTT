using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "StoreManger", menuName = "Store")]
public class StoreManager : ScriptableObject
{
	public Coin[] CoinList;

}

[System.Serializable]
public class Coin
{
	public string CoinValue;
	public string CoinCount;
}
