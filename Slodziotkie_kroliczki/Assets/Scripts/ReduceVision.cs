using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReduceVision : Przeszkoda_A {


	public override IEnumerator Effect()
	{
		Debug.Log ("Zaciemniam wizje");
		GameManager.Instance.playerStatus [1] = true;
		GameManager.Instance.dirt.gameObject.SetActive (true);
		yield return new WaitForSeconds (GameManager.Instance.VisionReductionDuration);
		GameManager.Instance.dirt.gameObject.SetActive (false);
		Debug.Log ("Koniec zaciemniania");
	}
}
