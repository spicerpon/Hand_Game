using UnityEngine;
using System.Collections;
using Leap;

public class Hand_Status : MonoBehaviour {


	public static bool handsign = false; //따라오라는 손짓
	public static bool Fingertip = false; //쳐다보라는 손짓
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
