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

	
	private bool m_IsGameOver = false;
	
	// GameSystem
	private GameEventSystem m_GameEventSystem = null;	 

    /*
	private CampSystem m_CampSystem	 = null; 			 
	private StageSystem m_StageSystem = null; 			 
	private CharacterSystem m_CharacterSystem = null; 	 
	private APSystem m_ApSystem = null; 				 
	private AchievementSystem m_AchievementSystem = null;


	// UI
	private CampInfoUI m_CampInfoUI = null;				 
	private SoldierInfoUI m_SoldierInfoUI = null;		 
	private GameStateInfoUI m_GameStateInfoUI = null;
	private GamePauseUI m_GamePauseUI = null;			 
		*/

	private TowerDefenseGame()
	{}

	// Init
	public void Initinal()
	{

        m_IsGameOver = false;
		
		m_GameEventSystem = new GameEventSystem(this);

        /*
		m_CampSystem = new CampSystem(this);			
		m_StageSystem = new StageSystem(this);			
		m_CharacterSystem = new CharacterSystem(this); 	
		m_ApSystem = new APSystem(this);				
		m_AchievementSystem = new AchievementSystem(this); 

		
		m_CampInfoUI = new CampInfoUI(this); 			
		m_SoldierInfoUI = new SoldierInfoUI(this); 										
		m_GameStateInfoUI = new GameStateInfoUI(this); 	
		m_GamePauseUI = new GamePauseUI (this);			


		// 注入到其它系統
		EnemyAI.SetStageSystem( m_StageSystem );

		// 載入存檔
		LoadData();

		// 註冊遊戲事件系統
                */
        ResigerGameEvent();
	}


	private void ResigerGameEvent()
	{
//		m_GameEventSystem.RegisterObserver( ENUM_GameEvent.EnemyKilled, new EnemyKilledObserverUI(this));

		// Combo
		/*ComboObserver theComboObserver =new ComboObserver(this);
		m_GameEventSystem.RegisterObserver( ENUM_GameEvent.EnemyKilled, theComboObserver);
		m_GameEventSystem.RegisterObserver( ENUM_GameEvent.SoldierKilled, theComboObserver);*/

	}

	// Release
	public void Release()
	{
		
		m_GameEventSystem.Release();

        //m_CampSystem.Release();
        //m_StageSystem.Release();
        //m_CharacterSystem.Release();
        //m_ApSystem.Release();
        //m_AchievementSystem.Release();


        //m_CampInfoUI.Release();
        //m_SoldierInfoUI.Release();
        //m_GameStateInfoUI.Release();
        //m_GamePauseUI.Release();
        //UITool.ReleaseCanvas();


  //      SaveData();
	}

	
	public void Update()
	{
		// Player Input
		InputProcess();

		
		m_GameEventSystem.Update();
		//m_CampSystem.Update();
		//m_StageSystem.Update();
		//m_CharacterSystem.Update();
		//m_ApSystem.Update();
		//m_AchievementSystem.Update();

		
		//m_CampInfoUI.Update();
		//m_SoldierInfoUI.Update();
		//m_GameStateInfoUI.Update();
		//m_GamePauseUI.Update();
	}

	
	private void InputProcess()
	{
		// mouse left
		if(Input.GetMouseButtonUp( 0 ) == false)
			return ;
		

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit[] hits = Physics.RaycastAll(ray);

        // 走訪每一個被Hit到的GameObject
        foreach (RaycastHit hit in hits)
        {
            //// 是否有兵營點擊
            //CampOnClick CampClickScript = hit.transform.gameObject.GetComponent<CampOnClick>();
            //if (CampClickScript != null)
            //{
            //    CampClickScript.OnClick();
            //    return;
            //}

            //// 是否有角色點擊
            //SoldierOnClick SoldierClickScript = hit.transform.gameObject.GetComponent<SoldierOnClick>();
            //if (SoldierClickScript != null)
            //{
            //    SoldierClickScript.OnClick();
            //    return;
            //}
        }
    }
	

	public bool ThisGameIsOver()
	{
		return m_IsGameOver;
	}


	public void ChangeToMainMenu()
	{
        m_IsGameOver = true;
	}

    /*
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
		if( m_CharacterSystem !=null)
			return m_CharacterSystem.GetEnemyCount();
		return 0;
	}

	// 增加敵人陣亡數量(不透過GameEventSystem呼叫) 
	public void AddEnemyKilledCount()
	{
		m_StageSystem.AddEnemyKilledCount();
	}

	// 執行角色系統的Visitor
	public void RunCharacterVisitor(ICharacterVisitor Visitor)
	{
		m_CharacterSystem.RunVisitor( Visitor );
	}
    */

    #region Game Event
    // 註冊遊戲事件
    public void RegisterGameEvent( ENUM_GameEvent emGameEvent, IGameEventObserver Observer)
	{
		m_GameEventSystem.RegisterObserver( emGameEvent , Observer );
	}

	// 通知遊戲事件
	public void NotifyGameEvent( ENUM_GameEvent emGameEvent, System.Object Param )
	{
		m_GameEventSystem.NotifySubject( emGameEvent, Param);
	}
    #endregion

    /*
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

	// 通知AP更動
	public void APChange( int NowAP)
	{
		m_GameStateInfoUI.ShowAP( NowAP);
	}

	// 花費AP
	public bool CostAP( int ApValue)
	{
		return m_ApSystem.CostAP( ApValue );
	}

	// 顯示關卡
	public void ShowNowStageLv(int Lv)
	{
		m_GameStateInfoUI.ShowNowStageLv(Lv);
	}

	//  遊戲訊息
	public void ShowGameMsg( string Msg)
	{
		m_GameStateInfoUI.ShowMsg( Msg );
	}

	// 顯示Heart
	public void ShowHeart(int Value)
	{
		m_GameStateInfoUI.ShowHeart( Value);
		ShowGameMsg("陣營被攻擊");
	}

	// 顯示暫停
	public void GamePause()
	{
		if( m_GamePauseUI.IsVisible()==false)
			m_GamePauseUI.ShowGamePause( m_AchievementSystem.CreateSaveData() );
		else
			m_GamePauseUI.Hide();
	}

	// 存檔
	private void SaveData()
	{
		AchievementSaveData SaveData = m_AchievementSystem.CreateSaveData();
		SaveData.SaveData();
	}

	// 取回存檔
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
