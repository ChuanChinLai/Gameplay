using UnityEngine;
using System.Collections;

public class TowerDefenseGame
{
	// Singleton
	private static TowerDefenseGame _instance;

	public static TowerDefenseGame Instance
	{
		get
		{
			if (_instance == null)			
				_instance = new TowerDefenseGame();
			return _instance;
		}
	}


	private bool m_bGameOver = false;
	
	private GameEventSystem m_GameEventSystem = null;	 
	private CampSystem m_CampSystem	 = null; 			
	private StageSystem m_StageSystem = null; 			 
	private CharacterSystem m_CharacterSystem = null; 	 
	private APSystem m_ApSystem = null; 				 
	private AchievementSystem m_AchievementSystem = null;

	private CampInfoUI m_CampInfoUI = null;				
	private SoldierInfoUI m_SoldierInfoUI = null;		 
	private GameStateInfoUI m_GameStateInfoUI = null;	 
	private GamePauseUI m_GamePauseUI = null;
		
	private TowerDefenseGame()
	{

    }

	public void Initinal()
	{
		m_bGameOver = false;

		m_GameEventSystem = new GameEventSystem(this);	
		m_CampSystem = new CampSystem(this);			
		m_StageSystem = new StageSystem(this);			
		m_CharacterSystem = new CharacterSystem(this); 	
		m_ApSystem = new APSystem(this);				
		m_AchievementSystem = new AchievementSystem(this); 

		m_CampInfoUI = new CampInfoUI(this); 			
		m_SoldierInfoUI = new SoldierInfoUI(this); 											
		m_GameStateInfoUI = new GameStateInfoUI(this); 
		m_GamePauseUI = new GamePauseUI (this);

		EnemyAI.SetStageSystem( m_StageSystem );

		LoadData();

		ResigerGameEvent();
	}


	private void ResigerGameEvent()
	{
		m_GameEventSystem.RegisterObserver( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverUI(this));
	}

	public void Release()
	{

		m_GameEventSystem.Release();
		m_CampSystem.Release();
		m_StageSystem.Release();
		m_CharacterSystem.Release();
		m_ApSystem.Release();
		m_AchievementSystem.Release();

		m_CampInfoUI.Release();
		m_SoldierInfoUI.Release();
		m_GameStateInfoUI.Release();
		m_GamePauseUI.Release();
		UITool.ReleaseCanvas();

		SaveData();
	}

	public void Update()
	{
		InputProcess();

		m_GameEventSystem.Update();
		m_CampSystem.Update();
		m_StageSystem.Update();
		m_CharacterSystem.Update();
		m_ApSystem.Update();
		m_AchievementSystem.Update();

		m_CampInfoUI.Update();
		m_SoldierInfoUI.Update();
		m_GameStateInfoUI.Update();
		m_GamePauseUI.Update();
	}

	private void InputProcess()
	{
		if(Input.GetMouseButtonUp( 0 ) == false)
			return ;
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);		
		
		foreach (RaycastHit hit in hits)
		{
			CampOnClick CampClickScript = hit.transform.gameObject.GetComponent<CampOnClick>();

			if( CampClickScript != null )
			{
				CampClickScript.OnClick();
				return;
			}
			
			SoldierOnClick SoldierClickScript = hit.transform.gameObject.GetComponent<SoldierOnClick>();
			if( SoldierClickScript != null )
			{
				SoldierClickScript.OnClick();
				return ;
			}
		}
	}
	
	public bool ThisGameIsOver()
	{
		return m_bGameOver;
	}

	public void ChangeToMainMenu()
	{
		m_bGameOver = true;
	}

	public void AddSoldier( ISoldier theSoldier)
	{
		if( m_CharacterSystem !=null)
			m_CharacterSystem.AddSoldier( theSoldier );
	}

	public void RemoveSoldier( ISoldier theSoldier)
	{
		if( m_CharacterSystem !=null)
			m_CharacterSystem.RemoveSoldier( theSoldier );
	}
	
	public void AddEnemy( IEnemy theEnemy)
	{
		if( m_CharacterSystem !=null)
			m_CharacterSystem.AddEnemy( theEnemy );
	}

	public void RemoveEnemy( IEnemy theEnemy)
	{
		if( m_CharacterSystem !=null)
			m_CharacterSystem.RemoveEnemy( theEnemy );
	}

	public int GetEnemyCount()
	{
		if( m_CharacterSystem != null)
			return m_CharacterSystem.GetEnemyCount();

		return 0;
	}

	public void AddEnemyKilledCount()
	{
		m_StageSystem.AddEnemyKilledCount();
	}

	public void RunCharacterVisitor(ICharacterVisitor Visitor)
	{
		m_CharacterSystem.RunVisitor( Visitor );
	}

	public void RegisterGameEvent( ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
	{
		m_GameEventSystem.RegisterObserver( emGameEvent , Observer );
	}

	public void NotifyGameEvent( ENUM_GameEvent emGameEvent, System.Object Param )
	{
		m_GameEventSystem.NotifySubject( emGameEvent, Param);
	}

	public void ShowCampInfo( ICamp Camp )
	{
		m_CampInfoUI.ShowInfo( Camp );
		m_SoldierInfoUI.Hide();
	}

	public void ShowSoldierInfo( ISoldier Soldier )
	{
		m_SoldierInfoUI.ShowInfo( Soldier );
		m_CampInfoUI.Hide();
	}

	public void APChange( int NowAP)
	{
		m_GameStateInfoUI.ShowAP( NowAP);
	}

	public bool CostAP( int ApValue)
	{
		return m_ApSystem.CostAP( ApValue );
	}

	public void ShowNowStageLv( int Lv)
	{
		m_GameStateInfoUI.ShowNowStageLv(Lv);
	}

	public void ShowGameMsg( string Msg)
	{
		m_GameStateInfoUI.ShowMsg( Msg );
	}

	public void ShowHeart(int Value)
	{
		m_GameStateInfoUI.ShowHeart( Value);
		ShowGameMsg("You are under attack!!!");
	}

	public void GamePause()
	{
		if( m_GamePauseUI.IsVisible()==false)
			m_GamePauseUI.ShowGamePause( m_AchievementSystem.CreateSaveData() );
		else
			m_GamePauseUI.Hide();
	}

	private void SaveData()
	{
		AchievementSaveData SaveData = m_AchievementSystem.CreateSaveData();
		SaveData.SaveData();
	}

	private AchievementSaveData LoadData()
	{
		AchievementSaveData OldData = new AchievementSaveData();
		OldData.LoadData();
		m_AchievementSystem.SetSaveData( OldData );
		return OldData;
	}
	
	/*#region 直接取得角色數量的實作方式
	// 目前Soldier數量
	public int GetSoldierCount()
	{
		if( m_CharacterSystem !=null)
			return m_CharacterSystem.GetSoldierCount();
		return 0;
	}

	// 目前Soldier數量
	public int GetSoldierCount( ENUM_Soldier emSoldier)
	{
		if( m_CharacterSystem !=null)
			return m_CharacterSystem.GetSoldierCount(emSoldier);
		return 0;
	}	
	#endregion*/

}
