using UnityEngine;
using System.Collections;

public class Cat_Sight : MonoBehaviour {
	   
	private Transform playerTrans;
	private Vector3 rayDirection;
	private int FieldOfView = 60;
	private int ViewDistance = 3;

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
					print("Human Detected!");
					if (cat.closeness < 5) {
						cat.State = Cat_AI.CharacterState.Danger;
					}
									
					if (cat.closeness >= 5 && Hand_Status.handsign) {
						print("Following!");
						cat.State = Cat_AI.CharacterState.Following;
					}

				}	
			
			}
		}
	}

	void DetectBox()
	{
		RaycastHit[] hits;
		hits = Physics.RaycastAll (transform.position, transform.forward, 1f);

		for(int a = 0; a < hits.Length; a++)
		{
			if (hits [a].collider.tag == "Box") 
			{
				print("Curious!");
				transform.LookAt (hits[a].collider.transform);
				cat.State = Cat_AI.CharacterState.Curious;
			}
		}

	}
		
	// Update is called once per frame
	void Update () {
		DetectAspect ();
		DetectBox ();
	}
}
