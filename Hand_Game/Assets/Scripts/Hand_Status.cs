using UnityEngine;
using System.Collections;
using Leap;

public class Hand_Status : MonoBehaviour {


	public static bool handsign = false;
	public static bool Fingertip = false;
	// Use this for initialization
	void Start () {
		
	}
	IEnumerator Tip()
	{
		Fingertip = false;
		yield return null;
	}

	// Update is called once per frame
	void Update () {
		
		if (Fingertip) {
			StartCoroutine ("Tip");
		}
	}
}
