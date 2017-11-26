using UnityEngine;
using System.Collections;

public class NewStageObserverSoldierAddMedal : IGameEventObserver 
{
	private NewStageSubject m_Subject = null;
	private TowerDefenseGame m_TDGame = null;
	
	public NewStageObserverSoldierAddMedal(TowerDefenseGame  TDGame)
	{
		m_TDGame = TDGame;
	}
	
	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as NewStageSubject;
	}
	
	public override void Update()
	{
		SoldierAddMedalVisitor theAddMedalVisitor = new SoldierAddMedalVisitor(m_TDGame); 
		m_TDGame.RunCharacterVisitor( theAddMedalVisitor );
	}
    
}