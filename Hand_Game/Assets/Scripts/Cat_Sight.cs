using UnityEngine;
using System.Collections;

public class Cat_Sight : MonoBehaviour {
	   
	private Transform playerTrans;
	private Vector3 rayDirection;
	private int FieldOfView = 45;
	private int ViewDistance = 2;

	Cat_AI cat;

	// Use this for initialization
	void Start () {
		cat = GetComponent<Cat_AI> ();
		playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void DetectAspect()
	{
		RaycastHit hit;

		rayDirection = playerTrans.position - transform.position;

		if((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
		{
			if(Physics.Raycast(transform.position, rayDirection, out hit, ViewDistance))
			{
				
				if(hit.collider.tag == "Player")
				{
					print("사람 발견!!!");
					cat.State = Cat_AI.CharacterState.Danger;
				}									
			}

		}
	}



	// Update is called once per frame
	void Update () {
		DetectAspect ();
	}
}
