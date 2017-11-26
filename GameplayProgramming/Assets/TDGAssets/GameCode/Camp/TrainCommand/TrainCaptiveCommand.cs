using UnityEngine;
using System.Collections;


public class TrainCaptiveCommand : ITrainCommand
{		
	private TowerDefenseGame m_TDGame = null;
	private ENUM_Enemy 		 m_emEnemy;
	private Vector3 		 m_Position; 
	
	public TrainCaptiveCommand(ENUM_Enemy emEnemy, Vector3 Position, TowerDefenseGame TDGame)
	{
		m_TDGame = TDGame;
		m_emEnemy = emEnemy;
		m_Position = Position;
	}
	
	public override void Execute()
	{
		ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
		IEnemy theEnemy = Factory.CreateEnemy ( m_emEnemy, ENUM_Weapon.Gun , m_Position ,Vector3.zero);

		SoldierCaptive NewSoldier = new SoldierCaptive( theEnemy );
				
		m_TDGame.RemoveEnemy( theEnemy );

		m_TDGame.AddSoldier( NewSoldier );
	}		
}