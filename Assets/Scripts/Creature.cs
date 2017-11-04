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

    //Tool to interact with
    ToolItem targetTool = null;



    void Update()
    {
        if (!IsBusy)
        {
            if (targetInteractable != null)
            {
                if (Vector3.Distance(transform.position, targetInteractable.transform.position) < GetInteractDistanceToTarget())
                {
                    Stop();

                    if (targetInteractable.Tags.Contains(Interactable.InteractableTags.Targeteable))
                    {
                        //if the target is a creature targeteable by a tool, let the tool manage it
                        UseToolOnCreature(targetInteractable.GetComponent<Creature>(), GetSelectedTool());
                    }
                    else
                    {
                        //If the target is a passive item, let it manage it
                        targetInteractable.Interact(this);
                    }
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

    ToolItem GetSelectedTool()
    {
        if (IsPlayer)
        {
            return GameManager.Instance.Player.SelectedTool;
        }

        //Depending on the AI, this should change to a default AI attack
        return null;
    }

    float GetInteractDistanceToTarget()
    {
        if (targetTool == null)
        {
            return targetInteractable.InteractDistance;
        }
        else
        {
            return targetTool.DistanceToInteract;
        }
    }


    public void CancelInteraction()
    {
        targetInteractable = null;
        targetTool = null;
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

    public void InteractWithObject(Interactable target, ToolItem tool)
    {
        targetInteractable = target;
        if (target.Tags.Contains(Interactable.InteractableTags.Targeteable))
        {
            targetTool = tool;
        }
        else
            targetTool = null;
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

    public virtual void UseToolOnCreature(Creature target, ToolItem tool)
    {
        tool.UseOnCreature(this, target);
    }


    void OnTriggerEnter(Collider other)
    {
        Hitbox hbx = other.GetComponent<Hitbox>();
        if (hbx != null)
        {
            if (hbx.Owner != this)
            {
                OnHitboxEnter(hbx);
            }
        }
    }

    void OnHitboxEnter(Hitbox hbx)
    {
        Debug.Log("Touch hitbox!");
    }





    public virtual void ShootHitbox(Vector3 pointClicked)
    {
        if (IsBusy)
            return;

        Hitbox hbx = Hitbox.CreateHitbox(transform.position + (transform.forward * 0.5f));

        hbx.Owner = this;


        float hitboxSpeed = 5;
        float duration = 2;

        hbx.Rigidbody.velocity = transform.forward * hitboxSpeed;
        if (duration > 0)
        {
            hbx.DestroyAfterSeconds(duration);
        }

    }
}
