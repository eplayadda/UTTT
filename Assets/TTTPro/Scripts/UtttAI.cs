using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtttAI : MonoBehaviour 
{
	public static UtttAI instance;
	InputHandler inputHandler;
	void Awake()
	{
		if (instance == null)
			instance = this;
	}

	void Start()
	{
		inputHandler = InputHandler.instance;
	}

	public int GridSelecteByAI()
	{
		bool isEmptyGrid = false;
		int pSelectedGrid = 0;
		if(inputHandler.selectedTicTTTgrid > 0)
			 isEmptyGrid = inputHandler.tictaktoe[inputHandler.selectedTicTTTgrid - 1].IsIntetactable();
		if (!isEmptyGrid) {
			List <int> freeData = new List<int> ();
		
			for (int i = 0; i < inputHandler.inputTrackerTi.gridData.Count; i++) {
				if (inputHandler.inputTrackerTi.gridData [i] == 0) {
					freeData.Add (i+1);
				}
			}
			pSelectedGrid = freeData [Random.Range (0, freeData.Count)];
		} else {
			pSelectedGrid = inputHandler.selectedTicTTTgrid ;
		}
		return pSelectedGrid;
	}

	public int InputSelecteByAI(int pGrid)
	{
		List <int> freeData = new List<int> ();
		List <int> allData = inputHandler.tictaktoe [pGrid - 1].gridData;
		for (int i = 0; i < allData.Count; i++) {
			if (allData [i] == 0) {
				freeData.Add (i+1);
			}
		}
		int pSelectedInput = freeData[Random.Range(0,freeData.Count)];
		return pSelectedInput;
	}
}
