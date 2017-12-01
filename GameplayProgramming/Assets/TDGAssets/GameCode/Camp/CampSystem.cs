using UnityEngine;
using System.Collections.Generic;

public class CampSystem : IGameSystem
{
	private Dictionary<ENUM_Soldier, ICamp> m_SoldierCamps = new Dictionary<ENUM_Soldier,ICamp>(); 
	private Dictionary<ENUM_Enemy , ICamp> m_CaptiveCamps = new Dictionary<ENUM_Enemy,ICamp>(); 

	public CampSystem(TowerDefenseGame TDGame) : base(TDGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_SoldierCamps.Add ( ENUM_Soldier.Rookie, SoldierCampFactory( ENUM_Soldier.Rookie ));
		m_SoldierCamps.Add ( ENUM_Soldier.Sergeant, SoldierCampFactory( ENUM_Soldier.Sergeant ));
		m_SoldierCamps.Add ( ENUM_Soldier.Captain, SoldierCampFactory( ENUM_Soldier.Captain ));

		m_CaptiveCamps.Add ( ENUM_Enemy.Elf, CaptiveCampFactory( ENUM_Enemy.Elf ));

		m_TDGame.RegisterGameEvent( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverCaptiveCamp(this));
	}


	public override void Update()
	{
		foreach( SoldierCamp Camp in m_SoldierCamps.Values )
			Camp.RunCommand();

		foreach( CaptiveCamp Camp in m_CaptiveCamps.Values )
			Camp.RunCommand();
	}
	

	private SoldierCamp SoldierCampFactory( ENUM_Soldier emSoldier )
	{
		string GameObjectName = "SoldierCamp_";
		float CoolDown = 0;
		string CampName = "";
		string IconSprite = "";
		switch( emSoldier )
		{
		case ENUM_Soldier.Rookie:
			GameObjectName += "Rookie";
			CoolDown = 3;
			CampName = "RookieCamp";
			IconSprite = "RookieCamp";
			break;
		case ENUM_Soldier.Sergeant:
			GameObjectName += "Sergeant";
			CoolDown = 4;
			CampName = "SergeantCamp";
			IconSprite = "SergeantCamp";
			break;
		case ENUM_Soldier.Captain:
			GameObjectName += "Captain";
			CoolDown = 5;
			CampName = "CaptainCamp";
			IconSprite = "CaptainCamp";
			break;
		default:
			Debug.Log("ERROR");
			break;				
		}

		GameObject theGameObject = UnityTool.FindGameObject( GameObjectName );

		Vector3 TrainPoint = GetTrainPoint( GameObjectName );

		SoldierCamp NewCamp = new SoldierCamp(theGameObject, emSoldier, CampName, IconSprite, CoolDown, TrainPoint); 
		NewCamp.SetTowerDefenseGame( m_TDGame );

		AddCampScript(theGameObject, NewCamp);

		return NewCamp;
	}

	public void ShowCaptiveCamp()
	{
		if( m_CaptiveCamps[ENUM_Enemy.Elf].GetVisible() == false)
		{
			m_CaptiveCamps[ENUM_Enemy.Elf].SetVisible(true);
			m_TDGame.ShowGameMsg("Get CaptiveCamp");
		}
	}

	private CaptiveCamp CaptiveCampFactory( ENUM_Enemy emEnemy )
	{
		string GameObjectName = "CaptiveCamp_";
		float CoolDown = 0;
		string CampName = "";
		string IconSprite = "";

		switch( emEnemy )
		{
		case ENUM_Enemy.Elf :
			GameObjectName += "Elf";
			CoolDown = 3;
			CampName = "Elf CaptiveCamp";
			IconSprite = "CaptiveCamp";
			break;		
		default:
			Debug.Log("ERROR");
			break;				
		}

		GameObject theGameObject = UnityTool.FindGameObject( GameObjectName );
				
		Vector3 TrainPoint = GetTrainPoint( GameObjectName );

		CaptiveCamp NewCamp = new CaptiveCamp(theGameObject, emEnemy, CampName, IconSprite, CoolDown, TrainPoint); 
		NewCamp.SetTowerDefenseGame( m_TDGame );

		AddCampScript(theGameObject, NewCamp);

		NewCamp.SetVisible(false);

		return NewCamp;
	}

	private Vector3 GetTrainPoint(string GameObjectName )
	{
		GameObject theCamp = UnityTool.FindGameObject( GameObjectName );
		GameObject theTrainPoint = UnityTool.FindChildGameObject( theCamp, "TrainPoint" );

		theTrainPoint.SetActive(false);

		return theTrainPoint.transform.position;
	}

	private void AddCampScript(GameObject theGameObject, ICamp Camp)
	{
		CampOnClick CampScript = theGameObject.AddComponent<CampOnClick>();
		CampScript.theCamp = Camp;
	}
	
	public void UTTrainSoldier( ENUM_Soldier emSoldier ) 
	{
		if( m_SoldierCamps.ContainsKey( emSoldier ))
			m_SoldierCamps[emSoldier].Train();
	}	
}
