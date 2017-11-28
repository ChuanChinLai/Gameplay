using UnityEngine;
using System.Collections.Generic;

public class CharacterSystem : IGameSystem
{
	private List<ICharacter> m_Soldiers = new List<ICharacter>();
	private List<ICharacter> m_Enemys = new List<ICharacter>();

	public CharacterSystem(TowerDefenseGame TDGame):base(TDGame)
	{
		Initialize();

		m_TDGame.RegisterGameEvent( ENUM_GameEvent.NewStage , new NewStageObserverSoldierAddMedal(m_TDGame));
	}

	#region Character Manage

	public void AddSoldier(ISoldier theSoldier)
	{
		m_Soldiers.Add( theSoldier );
	}
	
	public void RemoveSoldier(ISoldier theSoldier)
	{
		m_Soldiers.Remove( theSoldier );
	}
	

	public void AddEnemy( IEnemy theEnemy)
	{
		m_Enemys.Add( theEnemy );
	}
	

	public void RemoveEnemy( IEnemy theEnemy)
	{
		m_Enemys.Remove( theEnemy );
	}


	public void RemoveCharacter()
	{
		RemoveCharacter( m_Soldiers, m_Enemys, ENUM_GameEvent.SoldierKilled );
		RemoveCharacter( m_Enemys, m_Soldiers, ENUM_GameEvent.EnemyKilled);
	}

	// 移除角色
	public void RemoveCharacter(List<ICharacter> Characters, List<ICharacter> Opponents, ENUM_GameEvent emEvent)
	{
		// 分別取得可以移除及存活的角色
		List<ICharacter> CanRemoves = new List<ICharacter>();
		foreach( ICharacter Character in Characters)
		{
			// 是否陣亡
			if( Character.IsKilled() == false)
				continue;

			//  是否確認過陣亡事情
			if( Character.CheckKilledEvent()==false)			
				m_TDGame.NotifyGameEvent( emEvent,Character );

			// 是否可以移除
			if( Character.CanRemove())
				CanRemoves.Add (Character);
		}

		// 移除
		foreach( ICharacter CanRemove in CanRemoves)
		{
			// 通知對手移除
			foreach(ICharacter Opponent in Opponents)
				Opponent.RemoveAITarget( CanRemove );

			// 釋放資源並移除
			CanRemove.Release();
			Characters.Remove( CanRemove );
		}
	}

	// Enemy數量
	UnitCountVisitor m_UnitCountVisitor =  new UnitCountVisitor();

	public int GetEnemyCount()
	{
		// 使用Vistiro
		m_UnitCountVisitor.Reset();

		RunVisitor( m_UnitCountVisitor );

		return m_UnitCountVisitor.EnemyCount;

		// 直接取得
		//return m_Enemys.Count;
	}

	// 執行Visitor
	public void RunVisitor(ICharacterVisitor Visitor)
	{
		foreach( ICharacter Character in m_Soldiers)
			Character.RunVisitor( Visitor);
		foreach( ICharacter Character in m_Enemys)
			Character.RunVisitor( Visitor);
	}
	#endregion

	#region Update

	public override void Update()	
	{
		UpdateCharacter();
		UpdateAI();
	}
	
	private void UpdateCharacter()
	{
		foreach( ICharacter Character in m_Soldiers)
			Character.Update();

		foreach( ICharacter Character in m_Enemys)
			Character.Update();
	}
	
	private void UpdateAI()
	{
		UpdateAI(m_Soldiers, m_Enemys );
		UpdateAI(m_Enemys, m_Soldiers );
		
		RemoveCharacter();
	}
	

	private void UpdateAI( List<ICharacter> Characters, List<ICharacter> Targets )
	{
		foreach( ICharacter Character in Characters)
			Character.UpdateAI( Targets );
	}
	
	#endregion
}
