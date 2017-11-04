using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;
using System.Linq;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    List<Outline> outlines = new List<Outline>();
    public float InteractDistance = 2;
    public float InteractDelayTime = 3;
    public string TaskName = "Waiting";
    public List<InteractableTags> Tags = new List<InteractableTags>();


    public enum InteractableTags
    {
        Task,
        Targeteable
    }



    [HideInInspector]
    public float CurrentInteractElapsedTime = -1;

    bool alreadyStartedInteraction = false;

    void Awake()
    {
        outlines = GetComponentsInChildren<Outline>().ToList();
    }

    void Start()
    {
        outlines.ForEach(x => x.enabled = false);
    }



    protected virtual void OnMouseEnter()
    {
        outlines.ForEach(x => x.enabled = true);
    }

    protected virtual void OnMouseExit()
    {
        outlines.ForEach(x => x.enabled = false);
    }

    protected virtual void OnMouseUpAsButton()
    {
        GameManager.Instance.PlayerInput.InteractWithInteractable(this);
    }

    public void Interact(Creature creature)
    {
        if (Tags.Contains(InteractableTags.Task))
        {
            Interact_Task(creature);
        }
    }


    public void Interact_Task(Creature creature)
    {
        if (alreadyStartedInteraction)
            return;

        CurrentInteractElapsedTime = Time.time;
        alreadyStartedInteraction = true;
        creature.status = Creature.CreatureStatus.Busy;
        creature.StartCoroutine(InteractCoroutine(creature));

    }

    public virtual IEnumerator InteractCoroutine(Creature creature)
    {
        bool InteractionCompleted = false;

        while (!InteractionCompleted && creature.status == Creature.CreatureStatus.Busy)
        {
            float progressTime = Time.time - CurrentInteractElapsedTime;
            float progressPercent = progressTime / InteractDelayTime;
            int progressPercentInt = Mathf.Clamp(Mathf.FloorToInt(progressPercent * 100), 0, 100);

            if (creature.IsPlayer)
            {
                GameManager.Instance.HUD.TaskProgressBar(progressPercent, TaskName + " (" + progressPercentInt + "%)");
            }

            if (InteractDelayTime <= 0 || progressTime > InteractDelayTime)
            {
                InteractionCompleted = true;
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }


        if (InteractionCompleted)
        {
            yield return StartCoroutine(InteractionComplete(creature));
        }

        alreadyStartedInteraction = false;
        creature.CancelInteraction();
    }

    public virtual IEnumerator InteractionComplete(Creature creature)
    {
        Debug.Log(creature.name + " completed the interaction!");
        yield return new WaitForSeconds(0.1f);
    }
}
