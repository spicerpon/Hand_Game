using UnityEngine;
using System.Collections;

public class Cat_AI : MonoBehaviour {

	Animation anim;

	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	public AnimationClip runAnimation;
	public AnimationClip jumpPoseAnimation;

	public float walkMaxAnimationSpeed = 0.75f;
	public float trotMaxAnimationSpeed = 1.0f;
	public float runMaxAnimationSpeed = 1.0f;
	public float jumpAnimationSpeed = 1.15f;
	public float landAnimationSpeed = 1.0f;

	NavMeshAgent Navi;
	Transform Player;

	Vector3 moveDirection;
	float moveSpeed;
	float maxSpeed = 8f;
	float minSpeed = 3f;
	float random_v;
	float random_h;

	public int closeness; //고양이 친밀도

	public enum CharacterState {
		Idle = 0,
		Walking = 1,
		Following = 2,
		Running = 3,
		Danger = 4,
		Curious = 5
	}
	public CharacterState State; //고양이의 상태 부여

	// Use this for initialization
	void Start () {
		Navi = GetComponent<NavMeshAgent> ();

		anim = GetComponent<Animation> ();
		anim.AddClip (idleAnimation, "Idle");
		anim.AddClip (walkAnimation, "Walk");
		anim.AddClip (runAnimation, "Run");
		anim.AddClip (jumpPoseAnimation, "Jump");
	
		closeness = 0;
		InvokeRepeating ("MoveRandom", 0.0f, 5f); //일정한 빈도로 랜덤한 고양이 움직임 발생 
	}

	IEnumerator Escape()
	{
		moveDirection *= -1;
		transform.rotation = Quaternion.LookRotation(moveDirection);
		moveSpeed = maxSpeed;
		closeness++; //친밀도 상승
		State = CharacterState.Running;
		anim.CrossFade ("Run");
		yield return new WaitForSeconds(2f);
		State = CharacterState.Idle;
		MoveRandom ();
		yield return null;
	}

	void MoveRandom()
	{
		if (State == CharacterState.Danger || State == CharacterState.Following || State == CharacterState.Curious || Hand_Status.Fingertip) {
			return;

			//고양이가 특수한 상태일 때는 랜덤 무브를 실행하지 않음
		} 
	
		moveSpeed = Random.Range (minSpeed, maxSpeed);

		if (moveSpeed > 6) {
			State = CharacterState.Running;
			anim.CrossFade ("Run");
		}
		else{
			State = CharacterState.Walking;
			anim.CrossFade ("Walk");
		}

		int temp = Random.Range (0, 2);

		if (temp == 0) {
			random_v = 1;
		} else
			random_v = -1;

		temp = Random.Range (0, 2);

		if (temp == 0) {
			random_h = 1;
		} else
			random_h = -1;
		
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right = new Vector3(forward.z, 0, -forward.x);

		float v  = random_v;
		float h  = random_h;

		Vector3 targetDirection = h * right + v * forward;
		moveDirection = targetDirection.normalized;

		transform.rotation = Quaternion.LookRotation(moveDirection);

		temp = Random.Range (0, 4);

		if (temp == 3) {
			State = CharacterState.Idle;
			anim.CrossFade ("Idle");
		}
			
	}
	
	// Update is called once per frame
	void Update () {


		if (State == CharacterState.Danger) {
			StartCoroutine ("Escape"); 

			//친밀도가 낮을때 사람을 발견했을 시 실행
		}

		if (State == CharacterState.Curious) {
			GetComponent<Animation>().Play("Ithcing");
			moveDirection *= -1;
			transform.rotation = Quaternion.LookRotation(moveDirection);
			State = CharacterState.Idle;

			//시야에 박스가 있을때는 잠깐 멈춰서서 고민하는 상태로 전환
		}

		var move = moveSpeed / 10f * Time.deltaTime;

		if (State != CharacterState.Idle && State != CharacterState.Following && State != CharacterState.Curious)
			transform.position += moveDirection * move;

		if (Hand_Status.Fingertip && closeness >= 5) 
		{
			Debug.Log ("FingerTip!");
			State = CharacterState.Idle;
			anim.CrossFade ("Idle");
			Player = GameObject.FindWithTag("Player").transform;
			transform.LookAt (Player);

			//손가락으로 신호를 주면 사람쪽을 쳐다보게 하는 상태
		}


		if (State == CharacterState.Following)
		{
			Debug.Log ("Following");
			Player = GameObject.FindWithTag("Player").transform;

			Navi.destination = Player.position;

			float distance = Vector3.Distance (transform.position, Player.transform.position);
		
			if (distance < 1.5f) {
				State = CharacterState.Idle;
				anim.CrossFade ("Idle");
				Navi.ResetPath (); //목적지에 도달하면 기본 상태로 변경
			}
			else
				anim.CrossFade ("Run");

			//사람이 손짓햇을때 쫓아가는 상태
		}

	

}

}