using UnityEngine;
using System.Collections;


public class CaptiveCamp : ICamp
{
//	private GameObject m_GameObject = null;
	private ENUM_Enemy m_emEnemy = ENUM_Enemy.Null;
	private Vector3 m_Position;


	public CaptiveCamp(GameObject theGameObject, ENUM_Enemy emEnemy, string CampName, string IconSprite ,float TrainCoolDown,Vector3 Position) : base(theGameObject, TrainCoolDown,CampName, IconSprite )
	{
		m_emSoldier = ENUM_Soldier.Captive;
		m_emEnemy = emEnemy;			
		m_Position = Position;
	}

	public override int GetTrainCost()
	{
		return 10;
	}


	public override void Train()
	{
		TrainCaptiveCommand NewCommand = new TrainCaptiveCommand( m_emEnemy, m_Position, m_TDGame);  
		AddTrainCommand( NewCommand );
	}


}
