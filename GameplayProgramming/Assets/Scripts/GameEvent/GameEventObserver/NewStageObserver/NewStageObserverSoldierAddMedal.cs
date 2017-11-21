using UnityEngine;
using System.Collections;

public class NewStageObserverSoldierAddMedal : IGameEventObserver 
{
	private NewStageSubject m_Subject = null;
	private TowerDefenseGame m_PBDGame = null;
	
	public NewStageObserverSoldierAddMedal(TowerDefenseGame PBDGame)
	{
		m_PBDGame = PBDGame;
	}
	
	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as NewStageSubject;
	}
	
	public override void Update()
	{
//		SoldierAddMedalVisitor theAddMedalVisitor = new SoldierAddMedalVisitor(m_PBDGame); 
//		m_PBDGame.RunCharacterVisitor( theAddMedalVisitor );
	}
	
}