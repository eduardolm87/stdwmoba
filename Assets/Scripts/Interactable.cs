using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
	Outline outline;

	void Awake()
	{
		outline = GetComponent<Outline>();
	}

	void Start()
	{
		outline.enabled = false;
	}



	void OnMouseEnter()
	{
		outline.enabled = true;
	}

	void OnMouseExit()
	{
		outline.enabled = false;
	}
}
