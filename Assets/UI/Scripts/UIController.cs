using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController
{
	
	public void ActiveUIObject (GameObject obj)
	{
		obj.SetActive (true);
	}

	public void DeactiveUIObject (GameObject obj)
	{
		obj.SetActive (false);	
	}


}
