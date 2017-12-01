using UnityEngine;
using System.Collections;

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

		m_TDGame.NotifyGameEvent( ENUM_GameEvent.SoldierUpgate, Soldier); 
	}
}
