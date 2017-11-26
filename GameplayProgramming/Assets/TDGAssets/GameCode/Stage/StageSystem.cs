using UnityEngine;
using System.Collections.Generic;

public class StageSystem : IGameSystem
{
	public const int MAX_HEART = 3;
	private int m_NowHeart = MAX_HEART;	
	private int	m_EnemyKilledCount = 0;	

	private int			  m_NowStageLv	 = 1;
	private IStageHandler m_NowStageHandler = null;
	private IStageHandler m_RootStageHandler = null;	
	private List<Vector3> m_SpawnPosition = null;
	private Vector3 	  m_AttackPos = Vector3.zero;
	private bool 		  m_bCreateStage = false;

	public StageSystem(TowerDefenseGame TDGame) : base(TDGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		InitializeStageData();

		m_NowStageHandler = m_RootStageHandler;	

		m_NowStageLv = 1;

		m_TDGame.RegisterGameEvent( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverStageScore(this)); 
	}

	public override void Release ()
	{
		base.Release ();
		m_SpawnPosition.Clear();
		m_SpawnPosition = null;
		m_NowHeart = MAX_HEART;
		m_EnemyKilledCount = 0;
		m_AttackPos = Vector3.zero;
	}
	
	public override void Update()
	{
		m_NowStageHandler.Update();

		if(m_TDGame.GetEnemyCount() ==  0 )
		{
			if( m_NowStageHandler.IsFinished() == false)
				return;


			IStageHandler NewStageData = m_NowStageHandler.CheckStage();

			if( m_NowStageHandler == NewStageData)
				m_NowStageHandler.Reset();
			else			
				m_NowStageHandler = NewStageData;

			NotiyfNewStage();
		}
	}
	
	public void LoseHeart()
	{
		m_NowHeart -= m_NowStageHandler.LoseHeart();
		m_TDGame.ShowHeart( m_NowHeart );
	}

	public void AddEnemyKilledCount()
	{
		m_EnemyKilledCount++;
	}

	public void SetEnemyKilledCount( int KilledCount)
	{
		m_EnemyKilledCount = KilledCount;
	}

	public int GetEnemyKilledCount()
	{
		return m_EnemyKilledCount;
	}
    
	private void NotiyfNewStage()
	{
		m_TDGame.ShowGameMsg("New Stage");
		m_NowStageLv++;

		m_TDGame.ShowNowStageLv(m_NowStageLv);

		m_TDGame.NotifyGameEvent( ENUM_GameEvent.NewStage , m_NowStageLv );

	}
	
	private void InitializeStageData()
	{
		if( m_RootStageHandler!=null)
			return ;

		Vector3 AttackPosition = GetAttackPosition();

		NormalStageData StageData = null;
		IStageScore StageScore = null;
		IStageHandler NewStage = null;

		// L1
		StageData 	= new NormalStageData(3f, GetSpawnPosition(), AttackPosition );
		StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Gun, 3); 
		StageScore 	= new StageScoreEnemyKilledCount(3, this);
		NewStage = new NormalStageHandler(StageScore, StageData );

		m_RootStageHandler = NewStage;

        // L2
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Rifle,3); 
		StageScore 	= new StageScoreEnemyKilledCount(6, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L3
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Rocket,3); 
		StageScore 	= new StageScoreEnemyKilledCount(9, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L4
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Gun,3); 
		StageScore 	= new StageScoreEnemyKilledCount(12, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L5
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Rifle,3); 
		StageScore 	= new StageScoreEnemyKilledCount(15, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L6
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Rocket,3); 
		StageScore 	= new StageScoreEnemyKilledCount(18, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L7
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Gun,3); 
		StageScore 	= new StageScoreEnemyKilledCount(21, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L8
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rifle,3); 
		StageScore 	= new StageScoreEnemyKilledCount(24, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L9
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rocket,3); 
		StageScore 	= new StageScoreEnemyKilledCount(27, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );

        // L10
        StageData = new NormalStageData(3f, GetSpawnPosition(), AttackPosition);
		StageData.AddStageData( ENUM_Enemy.Elf, ENUM_Weapon.Rocket,3); 
		StageData.AddStageData( ENUM_Enemy.Troll, ENUM_Weapon.Rocket,3); 
		StageData.AddStageData( ENUM_Enemy.Ogre, ENUM_Weapon.Rocket,3); 
		StageScore 	= new StageScoreEnemyKilledCount(30, this);
		NewStage = NewStage.SetNextHandler( new NormalStageHandler( StageScore, StageData) );
	}

	private Vector3 GetSpawnPosition()
	{
		if( m_SpawnPosition == null)
		{
			m_SpawnPosition = new List<Vector3>();

			for(int i = 1; i <= 3; ++i)
			{
				string name = string.Format("EnemySpawnPosition{0}", i);

				GameObject tempObj = UnityTool.FindGameObject( name );

				if( tempObj == null)
					continue;

				tempObj.SetActive(false);
				m_SpawnPosition.Add( tempObj.transform.position );
			}
		}

		int index  = UnityEngine.Random.Range(0, m_SpawnPosition.Count -1 );
         
		return m_SpawnPosition[index];
	}

	private Vector3 GetAttackPosition()
	{
		if( m_AttackPos == Vector3.zero)
		{
			GameObject tempObj = UnityTool.FindGameObject("EnemyAttackPosition");

			if( tempObj==null)
				return Vector3.zero;

			tempObj.SetActive(false);

			m_AttackPos = tempObj.transform.position;
		}
		return m_AttackPos;
	}
}
