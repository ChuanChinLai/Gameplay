using UnityEngine;
using System.Collections;

// 增加Solder勳章
public class SoldierAddMedalVisitor : ICharacterVisitor 
{
	TowerDefenseGame m_TDGame = null;

	public SoldierAddMedalVisitor( TowerDefenseGame TDGame)
	{
		m_TDGame = TDGame;
	}

	public override void VisitSoldier(ISoldier Soldier)
	{
		base.VisitSoldier( Soldier);
		Soldier.AddMedal();

		// 遊戲事件
		m_TDGame.NotifyGameEvent( ENUM_GameEvent.SoldierUpgate, Soldier); 
	}
}
