using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateScriptableObject : MonoBehaviour
{
	[MenuItem ("Assets/Create/Store Object")]
	public static void CreateMyAsset ()
	{
		StoreManager asset = ScriptableObject.CreateInstance<StoreManager> ();

		AssetDatabase.CreateAsset (asset, "Assets/StoreObject.asset");
		AssetDatabase.SaveAssets ();

		EditorUtility.FocusProjectWindow ();

		Selection.activeObject = asset;
	}
}
