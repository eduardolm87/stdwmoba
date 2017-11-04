using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDToolbox : MonoBehaviour
{
    public List<ToolbarButton> Slots = new List<ToolbarButton>();

    public Sprite BlankSlotSprite;


    void Start()
    {
        RefreshFromPlayer();
    }

    public void RefreshFromPlayer()
    {
        if (GameManager.Instance.Player == null)
        {
            return;
        }

        for (int i = 0; i < GameManager.Instance.Player.Tools.Count; i++)
        {
            ToolItem tool = GameManager.Instance.Player.Tools[i];
            Slots[i].Assign(tool);
            // Debug.Log("Slot " + i + " is " + Slots[i].tool.name);
        }


        int nToolsInPlayer = GameManager.Instance.Player.Tools.Count;
        for (int i = nToolsInPlayer; i < Slots.Count; i++)
        {
            Slots[i].Assign(null);
            //Debug.Log("Slot " + i + " is " + "empty");

        }
    }
}
