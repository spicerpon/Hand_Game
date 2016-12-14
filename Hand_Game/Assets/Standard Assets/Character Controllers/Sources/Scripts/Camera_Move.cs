using UnityEngine;
using System.Collections;

public class Camera_Move : MonoBehaviour {

	public float moveSpeed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");

		transform.Translate (0f, 0f, h * moveSpeed * Time.deltaTime);

	}
	

}
