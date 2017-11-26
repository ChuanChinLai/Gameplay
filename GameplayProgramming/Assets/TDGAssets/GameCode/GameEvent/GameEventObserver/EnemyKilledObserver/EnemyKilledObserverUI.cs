using UnityEngine;
using System.Collections;

public class EnemyKilledObserverUI : IGameEventObserver 
{
	private EnemyKilledSubject m_Subject = null;
	private TowerDefenseGame m_TDGame = null;

	public EnemyKilledObserverUI(TowerDefenseGame TDGame  )
	{
		m_TDGame = TDGame;
	}

	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as EnemyKilledSubject;
	}

	public override void Update()
	{
		//Debug.Log("EnemyKilledObserverUI.Update: Count["+ m_Subject.GetKilledCount() +"]");
		if(m_TDGame != null)
			m_TDGame.ShowGameMsg("Enemy Killed");
	}

}
