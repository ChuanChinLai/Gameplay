using UnityEngine;
using System.Collections.Generic;

// 管理創建出來的角色
public class CharacterSystem : IGameSystem
{
	private List<ICharacter> m_Soldiers = new List<ICharacter>();
	private List<ICharacter> m_Enemys = new List<ICharacter>();

	public CharacterSystem(TowerDefenseGame PBDGame) : base(PBDGame)
	{
		Initialize();

//		m_PBDGame.RegisterGameEvent( ENUM_GameEvent.NewStage , new NewStageObserverSoldierAddMedal(m_PBDGame));
	}

	#region Character Management

	/*
	public void AddSoldier( ISoldier theSoldier)
	{
		m_Soldiers.Add( theSoldier );
	}
	
	
	public void RemoveSoldier( ISoldier theSoldier)
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
    */

	
	public void RemoveCharacter()
	{
		RemoveCharacter( m_Soldiers, m_Enemys, ENUM_GameEvent.SoldierKilled );
		RemoveCharacter( m_Enemys, m_Soldiers, ENUM_GameEvent.EnemyKilled);
	}

	
	public void RemoveCharacter(List<ICharacter> Characters, List<ICharacter> Opponents, ENUM_GameEvent emEvent)
	{
		
		List<ICharacter> CanRemoves = new List<ICharacter>();
		foreach( ICharacter Character in Characters)
		{
			
			if( Character.IsKilled() == false)
				continue;


			if( Character.CheckKilledEvent() == false)			
				m_PBDGame.NotifyGameEvent(emEvent, Character);

			if( Character.CanRemove())
				CanRemoves.Add (Character);
		}

		
		foreach( ICharacter CanRemove in CanRemoves)
		{

			foreach(ICharacter Opponent in Opponents)
//				Opponent.RemoveAITarget( CanRemove );

			CanRemove.Release();
			Characters.Remove( CanRemove );
		}
	}

    /*
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
    */

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



	/*#region 直接取得角色數量的實作方式
	// 取得Soldier數量
	public int GetSoldierCount()
	{
		return m_Soldiers.Count;
	}

	// 取得各Soldier單位數量
	public int GetSoldierCount(ENUM_Soldier emSolider)
	{
		int Count =0;
		foreach(ISoldier pSoldier in m_Soldiers)
		{
			if(pSoldier == null)
				continue;

			if( pSoldier.GetSoldierType() == emSolider)
				Count++;
		}
		return Count;
	}		
	#endregion*/


}
