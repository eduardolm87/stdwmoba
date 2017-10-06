using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
	public float MovementSpeed = 5;





	public Rigidbody rigidbody;
	public Animator animator;


	protected bool isplayer = false;
	public bool IsPlayer
	{
		get { return isplayer; }
	}


	public enum CreatureStatus
	{
		Idle,
		Walking,
		Busy,
		Stun,
		Dead
	}

	public bool IsBusy
	{
		get
		{
			switch (status)
			{
			case CreatureStatus.Busy:
			case CreatureStatus.Stun:
			case CreatureStatus.Dead:
				return true;

			default:
				return false;
			}
		}
	}






	//General status
	[HideInInspector]
	public CreatureStatus status = CreatureStatus.Idle;

	//Interacting
	[HideInInspector]
	public Interactable targetInteractable = null;

	//Moving to point in the map (when Walking)
	Vector3 targetPoint = Vector3.zero;




	void Update()
	{
		if (!IsBusy)
		{
			if (targetInteractable != null)
			{
				if (Vector3.Distance(transform.position, targetInteractable.transform.position) < targetInteractable.InteractDistance)
				{
					Stop();
					targetInteractable.Interact(this);
				}
				else
				{
					MoveToPoint(targetInteractable.transform.position);
				}
			}

			if (status == CreatureStatus.Walking)
			{
				MovingTowardsPointRoutine();
			}
		}

		AnimationControl();
	}



	public void CancelInteraction()
	{
		targetInteractable = null;
		targetPoint = transform.position;
		status = CreatureStatus.Idle;
		GameManager.Instance.HUD.TaskProgressBarObject.SetActive(false);
	}

	public void MoveToPoint(Vector3 point)
	{
		if (IsBusy)
		{
			if (status == CreatureStatus.Busy)
			{
				CancelInteraction();
			}
			else
			{
				return;
			}
		}

		targetPoint = point;
		status = CreatureStatus.Walking;
	}

	public void InteractWithObject(Interactable target)
	{
		targetInteractable = target;
	}

	public void Stop()
	{
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
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
			status = CreatureStatus.Idle;
			Stop();
		}

	}


	void AnimationControl()
	{
		animator.SetFloat("WalkingSpd", Mathf.Abs(rigidbody.velocity.magnitude));
		animator.SetBool("Interact", status == CreatureStatus.Busy);
	}

}
