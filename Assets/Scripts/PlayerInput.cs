using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float distance = 50f;


    public int ActionMouseButton = 1;

    public int ToolUseMouseButton = 0;


    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(ActionMouseButton))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, distance))
            {
                if (hit.collider.CompareTag("WalkSurface"))
                {
                    GameManager.Instance.Player.MoveToPoint(hit.point);
                }
            }
        }
        /*
    else if (Input.GetMouseButtonDown(ToolUseMouseButton))
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance))
        {
            Vector3 clickedPoint = hit.point;
            clickedPoint.y = GameManager.Instance.Player.transform.position.y;

            GameManager.Instance.Player.UseTool(clickedPoint);
        }
    }
         * */
    }




    public void InteractWithInteractable(Interactable target)
    {
        GameManager.Instance.Player.InteractWithObject(target, GameManager.Instance.Player.SelectedTool);
    }
}
