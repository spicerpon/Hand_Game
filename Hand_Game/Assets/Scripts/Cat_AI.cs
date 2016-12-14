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

	enum CharacterState {
		Idle = 0,
		Walking = 1,
		Trotting = 2,
		Running = 3,
		Jumping = 4,
	}
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();

		anim.AddClip (idleAnimation, "Idle");
		anim.AddClip (walkAnimation, "Walk");
		anim.AddClip (runAnimation, "Run");
		anim.AddClip (jumpPoseAnimation, "Jump");
	}
	
	// Update is called once per frame
	void Update () {
		
		//anim.CrossFade ("Run");
	
	}
}
