using UnityEngine;
using System.Collections;

public class Camera_Move : MonoBehaviour {

	public float speed = 5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		float amtMove = speed * Time.smoothDeltaTime;

		float key1 = Input.GetAxis("Horizontal");
		transform.Translate(Vector3.right * amtMove * key1 , Space.World);

		float key2 = Input.GetAxis("Vertical");
		transform.Translate(Vector3.forward * amtMove * key2 , Space.World);
	}
}
