using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;

	void Awake()
	{
		Instance = this; 
	}


	public PlayerInput PlayerInput;


	public Player Player;
}
