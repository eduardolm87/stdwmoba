using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarButton : MonoBehaviour
{
    public Image Border;
    public Image Icon;
    public Text Text;


    public KeyCode AssignedKey = KeyCode.Alpha1;

    [HideInInspector]
    public ToolItem tool = null;


    public void Assign(ToolItem tool)
    {
        this.tool = tool;

        if (this.tool == null)
        {
            Icon.sprite = GameManager.Instance.HUD.ToolBox.BlankSlotSprite;
            Text.text = string.Empty;
        }
        else
        {
            Icon.sprite = this.tool.Icon;
            Text.text = KeyCodeShortcutShow();
        }
    }


    string KeyCodeShortcutShow()
    {
        string str = AssignedKey.ToString();
        if (str.StartsWith("Alpha"))
        {
            str = str.Remove(0, 5);
        }

        return str;
    }
}
