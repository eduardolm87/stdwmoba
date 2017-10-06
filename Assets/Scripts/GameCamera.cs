using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	Vector3 OriginalOffset;
	Quaternion OriginalRotation;

	void Start()
	{
		OriginalOffset = transform.localPosition;	
		OriginalRotation = transform.localRotation;
	}

	void Update()
	{
		Transform player = GameManager.Instance.Player.transform;

		transform.position = player.position;

		transform.position += OriginalOffset;


	}
}
