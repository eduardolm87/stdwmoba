using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	public float MovementSpeed = 5;





	public Rigidbody rigidbody;
	public Animator animator;




	bool movingTowardsPoint = false;
	bool InteractingWithSomething = false;
	Vector3 targetPoint = Vector3.zero;


	void Update()
	{
		Interact();

		if (!InteractingWithSomething)
		{
			if (movingTowardsPoint)
			{
				MovingTowardsPointRoutine();
			}
		}

	

		AnimationControl();
	}







	public void MoveToPoint(Vector3 point)
	{
		targetPoint = point;
		movingTowardsPoint = true;
	}

	public void Stop()
	{
		rigidbody.velocity = Vector3.zero;
	}

	void MovingTowardsPointRoutine()
	{
		if (Vector3.Distance(transform.position, targetPoint) > 0.1f)
		{
			Vector3 dir = (targetPoint - transform.position).normalized;
			rigidbody.velocity = dir * MovementSpeed;

			Vector3 LookAtPoint = targetPoint;
			LookAtPoint.y = transform.position.y;

			transform.LookAt(LookAtPoint);
		}
		else
		{
			movingTowardsPoint = false;
			Stop();
		}

	}

	void Interact()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			InteractingWithSomething = !InteractingWithSomething;
			if (InteractingWithSomething)
			{
				Stop();
			}
		}
	}


	void AnimationControl()
	{
		animator.SetFloat("WalkingSpd", Mathf.Abs(rigidbody.velocity.magnitude));

		animator.SetBool("Interact", InteractingWithSomething);
	}

}
