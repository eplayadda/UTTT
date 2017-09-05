using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicTacToe : MonoBehaviour {
    public List<Image> grids = new List<Image>();
    public List<int> gridData = new List<int>();
    public GameObject marker;
    public TicTacToe parentTTT;
    public enum eTTTStatus
    {
        none,
        playerTwo,
        playerOne,
        draw
    };
    public eTTTStatus currTTTStatus;

	void Start () 
    {
	}

    public void CheckWinLogic()
    {
        int winPlayer = 0;
            //---------------
            // Row Game over
        if (gridData[0] != 0 && gridData[0] == gridData[1] && gridData[1] == gridData[2])
        {
            winPlayer = gridData[0];
        }
        else if (gridData[3] != 0 && gridData[3] == gridData[4] && gridData[4] == gridData[5])
        {
            winPlayer = gridData[3];
        }
        else if (gridData[6] != 0 && gridData[6] == gridData[7] && gridData[7] == gridData[8])
        {
            winPlayer = gridData[6];
        }

            //---------------
            // Collum Game over

        else if (gridData[0] != 0 && gridData[0] == gridData[3] && gridData[3] == gridData[6])
        {
            winPlayer = gridData[0];
        }
        else if (gridData[1] != 0 && gridData[1] == gridData[4] && gridData[4] == gridData[7])
        {
            winPlayer = gridData[1];    
        }
        else if (gridData[2] != 0 && gridData[2] == gridData[5] && gridData[5] == gridData[8])
        {
            winPlayer = gridData[2];    

        }
            //---------------
            // Digonal Game over

        else if (gridData[0] != 0 && gridData[0] == gridData[4] && gridData[4] == gridData[8])
        {
            winPlayer = gridData[0];    

        }
        else if (gridData[2] != 0 && gridData[2] == gridData[4] && gridData[4] == gridData[6])
        {
            winPlayer = gridData[2];    
        }
        else if(!IsIntetactable())
        {
            winPlayer = 3;
        }

        currTTTStatus =(eTTTStatus) winPlayer;
        if (winPlayer != 0)
        {
            SetMarkerImage(winPlayer);
//            parentTTT.SetInputData(winPlayer);

        }
//        if(parentTTT != null)
//        parentTTT.CheckWinLogic();
    }

    void SetMarkerImage(int index)
    {
		marker.GetComponent<Animator>().enabled = false;
        marker.GetComponent<Image>().sprite = GameManager.instance.marker[index];
    }

    public void SetInterable(bool pStatus)
    {
        GetComponent<Button>().interactable = pStatus;
        if (pStatus)
        {
			if(currTTTStatus == eTTTStatus.none)
            marker.GetComponent<Animator>().enabled = true;
			else
				marker.GetComponent<Animator>().enabled = false;

        }
        else
        {
            if (currTTTStatus == eTTTStatus.none)
            {
                marker.GetComponent<Animator>().enabled = false;
                marker.GetComponent<Image>().color = new Color(0,0,0,0);
            }

        }
    }
    public void SetInputData(int pGrid)
    {
        if (GameManager.instance.currGameMode == GameManager.eGameMode.onlineMultiPlayer)
        {
            if(GameManager.instance.currTurn == GameManager.eTurn.one)
                gridData[pGrid - 1] = 1;
            else if(GameManager.instance.currTurn == GameManager.eTurn.two)
                gridData[pGrid - 1] = 2;

        }
        SetMoveDataInUI();
        CheckWinLogic();
    }

    void SetMoveDataInUI()
    {
        for (int i = 0; i < gridData.Count; i++)
        {
             grids[i].sprite = GameManager.instance.marker[0];
        }
        for (int i = 0; i < gridData.Count; i++)
        {
            if (gridData[i] == 1)
            {
                grids[i].sprite = GameManager.instance.marker[1];
            }
            else if (gridData[i] == 2)
            {
                grids[i].sprite = GameManager.instance.marker[2];

            }
        }
    }

    public void SetDataToInputTracker(List<int> pData)
    {
        gridData = pData;
        for (int i = 0; i < pData.Count; i++)
        {
            if (pData[i] == 0)
            {
                GameManager.instance.inputTrackerBtn[i].GetComponent<Button>().interactable = true;
            }
            else
            {
                GameManager.instance.inputTrackerBtn[i].GetComponent<Button>().interactable = false;

            }
        }
        SetMoveDataInUI();
    }
	
    public bool IsIntetactable()
    {
        bool isInter = false;
        if (currTTTStatus == eTTTStatus.none)
        {
            for (int i = 0; i < gridData.Count; i++)
            {
                if (gridData[i] == 0)
                {
                    isInter = true;
                    break;
                }
            }
        }
        return isInter;
    }

}
