using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BattleState : ISceneState
{
	public BattleState(SceneStateController Controller):base(Controller)
	{
		this.StateName = "BattleState";
	}

	public override void StateBegin()
	{
		TowerDefenseGame.Instance.Initinal();
	}

	public override void StateEnd()
	{
		TowerDefenseGame.Instance.Release();
	}
			
	public override void StateUpdate()
	{	
		TowerDefenseGame.Instance.Update();

		if( TowerDefenseGame.Instance.ThisGameIsOver())
			m_Controller.SetState(new MainMenuState(m_Controller), "MainMenuScene" );
	}
}
