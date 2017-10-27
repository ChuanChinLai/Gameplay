using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SceneStateController
{
	private ISceneState m_State;
	private bool 	m_bRunBegin = false;
	
	public SceneStateController()
	{}


	public void SetState(ISceneState State, string LoadSceneName)
	{
		//Debug.Log ("SetState:"+State.ToString());
		m_bRunBegin = false;

		LoadScene( LoadSceneName );

		if( m_State != null )
			m_State.StateEnd();

		m_State = State;
	}


	private void LoadScene(string LoadSceneName)
	{
		if( LoadSceneName == null || LoadSceneName.Length == 0 )
			return ;

        SceneManager.LoadScene(LoadSceneName);
//		Application.LoadLevel( LoadSceneName );
	}


	public void StateUpdate()
	{
 //       AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(m_State.StateName);

		if (Application.isLoadingLevel)
			return;

		// call state init()
		if( m_State != null && m_bRunBegin == false)
		{
			m_State.StateBegin();
			m_bRunBegin = true;
		}

        //call state update()
		if( m_State != null)
			m_State.StateUpdate();
	}
}
