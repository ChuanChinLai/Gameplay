using UnityEngine;
using System;
using System.Collections;


public class GameLoop : MonoBehaviour 
{

    SceneStateController m_SceneStateController = new SceneStateController();


	void Awake()
	{

		GameObject.DontDestroyOnLoad( this.gameObject );		 

		UnityEngine.Random.InitState((int)DateTime.Now.Ticks);
	}

	// Use this for initialization
	void Start () 
	{
		m_SceneStateController.SetState(new StartState(m_SceneStateController), "");
	}

	// Update is called once per frame
	void Update () 
	{
		m_SceneStateController.StateUpdate();	
	}
}
