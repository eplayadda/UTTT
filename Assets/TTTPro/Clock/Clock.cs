using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{

	public Image nidle;

	public enum eClockState
	{
		none,
		play,
		pause
	}

	public eClockState currentClockStart;

	float minimum = 1.0F;
	float maximum = 0.0F;
	float t = 0.0f;
	public float speed = .1f;
	public float fillValue;

	void Start ()
	{
	
	}

	void Update ()
	{
		ClockData ();
	}


	public void PlayClock ()
	{
		currentClockStart = eClockState.play;
	}


	public void PauseClock ()
	{
		currentClockStart = eClockState.pause;
	}


	public void ResetClock ()
	{
		currentClockStart = eClockState.pause;
		float minimum = 1.0F;
		float maximum = 0.0F;
		t = 0f;
		fillValue = 1f;
		nidle.fillAmount = fillValue;
	}


	void ClockData ()
	{
		switch (currentClockStart) {
		case eClockState.play:
			{
				fillValue = Mathf.Lerp (minimum, maximum, t);
				t += speed * Time.deltaTime;
				nidle.fillAmount = fillValue;
//				Debug.Log ("Time :: "+t);
//				if (t >= 1)
//					StartCoroutine ("OnTimeUp");
			}
			break;
		case eClockState.pause:
			{

			}
			break;
		}
	}

	IEnumerator OnTimeUp()
	{
		currentClockStart = eClockState.none;
		if (TTTPlayerManager.instace.curPlayer == TTTPlayerManager.ePlayer.one && GameManager.instance.currTurn == GameManager.eTurn.one) {
			int a = UtttAI.instance.GridSelecteByAI ();
			Debug.Log ("a1"+a);
			yield return new WaitForSeconds (1f);
			InputHandler.instance.GridSelected (a);
			a = UtttAI.instance.InputSelecteByAI (a);
			Debug.Log ("a2"+a);
			yield return new WaitForSeconds (1f);
			InputHandler.instance.OnInputTaken (a);

			Debug.Log ("Time Up 1");
		} else if (TTTPlayerManager.instace.curPlayer == TTTPlayerManager.ePlayer.two && GameManager.instance.currTurn == GameManager.eTurn.two) {
			Debug.Log ("Time Up 2");
			int a = UtttAI.instance.GridSelecteByAI ();
			Debug.Log ("a1"+a);
			yield return new  WaitForSeconds (1f);
			InputHandler.instance.GridSelected (a);
			a = UtttAI.instance.InputSelecteByAI (a);
			Debug.Log ("a1"+a);
			yield return new WaitForSeconds (1f);
			InputHandler.instance.OnInputTaken (a);
		}
	}
}
