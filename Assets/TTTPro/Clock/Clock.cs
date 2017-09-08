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
			}
			break;
		case eClockState.pause:
			{

			}
			break;
		}
	}
}
