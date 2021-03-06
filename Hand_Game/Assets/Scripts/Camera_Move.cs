using UnityEngine;
using System.Collections;

public class Camera_Move : MonoBehaviour {

	public float speed = 5f;
	public Vector3 pos;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		pos = new Vector3 (transform.position.x + 2, transform.position.y + transform.position.z);

		float amtMove = speed * Time.smoothDeltaTime;

		float key1 = Input.GetAxis("Horizontal");
		transform.Translate(Vector3.right * amtMove * key1 , Space.World);

		float key2 = Input.GetAxis("Vertical");
		transform.Translate(Vector3.forward * amtMove * key2 , Space.World);

		if (Input.GetKey ("q")) {
			transform.Rotate (Vector3.down * 30 * Time.deltaTime,Space.World);
		}

		if (Input.GetKey ("e")) {
			transform.Rotate (Vector3.up * 30 * Time.deltaTime,Space.World);
		}
	}
}
