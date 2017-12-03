using UnityEngine;
using System.Collections;

public class AchievementSystem : IGameSystem 
{
	private AchievementSaveData m_LastSaveData = null; 

	private int m_EnemyKilledCount = 0;
	private int m_SoldierKilledCount = 0;
	private int m_StageLv =  0;

	public AchievementSystem(TowerDefenseGame TDGame):base(TDGame)
	{
		Initialize();
	}

	// 
	public override void Initialize ()
	{
		base.Initialize ();

		m_TDGame.RegisterGameEvent( ENUM_GameEvent.EnemyKilled	 , new EnemyKilledObserverAchievement(this));
		m_TDGame.RegisterGameEvent( ENUM_GameEvent.SoldierKilled, new SoldierKilledObserverAchievement(this));
		m_TDGame.RegisterGameEvent( ENUM_GameEvent.NewStage	 , new NewStageObserverAchievement(this));
	}


	public void AddEnemyKilledCount()
	{
		m_EnemyKilledCount++;
	}


	public void AddSoldierKilledCount()
	{
		m_SoldierKilledCount++;
	}


	public void SetNowStageLevel( int NowStageLevel )
	{
		m_StageLv = NowStageLevel;
	}
	

	public AchievementSaveData CreateSaveData()
	{
		AchievementSaveData SaveData = new AchievementSaveData();

		SaveData.EnemyKilledCount 	= Mathf.Max (m_EnemyKilledCount,m_LastSaveData.EnemyKilledCount);
		SaveData.SoldierKilledCount = Mathf.Max (m_SoldierKilledCount,m_LastSaveData.SoldierKilledCount);
		SaveData.StageLv 			= Mathf.Max (m_StageLv,m_LastSaveData.StageLv);

		return SaveData;
	}

	public void SetSaveData( AchievementSaveData SaveData)
	{
		m_LastSaveData = SaveData;
	}
}
