using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Player : Creature
{
    public List<ToolItem> Tools = new List<ToolItem>();

    [HideInInspector]
    public int SelectedToolIndex = -1;

    [HideInInspector]
    public ToolItem SelectedTool { get { return Tools[SelectedToolIndex]; } }

    void Start()
    {
        isplayer = true;
        SelectTool(0);

        Debug.Log("My selected tool is " + SelectedTool.name);
    }


    public void SelectTool(int index)
    {
        SelectedToolIndex = index;
    }

    public void SelectTool(string name)
    {
        int i = Tools.FindIndex(x => x.name == name);
        if (i < 0)
        {
            Debug.LogError("Error selecting tool index " + i);
            return;
        }

        SelectTool(i);
    }




}
