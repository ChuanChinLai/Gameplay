using UnityEngine;
using System.Collections.Generic;


public enum ENUM_GameEvent
{
	Null  			= 0,
	EnemyKilled 	= 1,
	SoldierKilled	= 2,
	SoldierUpgate	= 3,
	NewStage		= 4,
}


public class GameEventSystem : IGameSystem
{
	private Dictionary< ENUM_GameEvent, IGameEventSubject> m_GameEvents = new Dictionary< ENUM_GameEvent, IGameEventSubject>(); 

	public GameEventSystem(TowerDefenseGame PBDGame) : base(PBDGame)
	{
		Initialize();
	}
		
	
	public override void Release()
	{
		m_GameEvents.Clear();
	}
		

	public void RegisterObserver(ENUM_GameEvent emGameEvnet, IGameEventObserver Observer)
	{
		IGameEventSubject Subject = GetGameEventSubject( emGameEvnet );
		if( Subject!=null)
		{
			Subject.Attach( Observer );
			Observer.SetSubject( Subject );
		}
	}

	
	private IGameEventSubject GetGameEventSubject( ENUM_GameEvent emGameEvnet )
	{
		
		if( m_GameEvents.ContainsKey( emGameEvnet ))
			return m_GameEvents[emGameEvnet];

		
		IGameEventSubject pSujbect= null;
		switch( emGameEvnet )
		{
            /*
		case ENUM_GameEvent.EnemyKilled:
			pSujbect = new EnemyKilledSubject();
			break;
		case ENUM_GameEvent.SoldierKilled:
			pSujbect = new SoldierKilledSubject();
			break;
		case ENUM_GameEvent.SoldierUpgate:
			pSujbect = new SoldierUpgateSubject();
			break;
		case ENUM_GameEvent.NewStage:
			pSujbect = new NewStageSubject();
			break;
            */
		default:
			Debug.LogWarning("還沒有針對["+emGameEvnet+"]指定要產生的Subject類別");
			return null;
		}

		// 加入後並回傳
		m_GameEvents.Add (emGameEvnet, pSujbect );
		return pSujbect;
	}

	
	public void NotifySubject( ENUM_GameEvent emGameEvnet, System.Object Param)
	{
		
		if( m_GameEvents.ContainsKey( emGameEvnet ) == false)
			return;
		//Debug.Log("SubjectAddCount["+emGameEvnet+"]");
		m_GameEvents[emGameEvnet].SetParam( Param );
	}
	
}
