﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

	public float distance = 50f;

	void FixedUpdate()
	{    
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, distance))
			{
				GameManager.Instance.Player.MoveToPoint(hit.point);

			}    
		}    
	}
}