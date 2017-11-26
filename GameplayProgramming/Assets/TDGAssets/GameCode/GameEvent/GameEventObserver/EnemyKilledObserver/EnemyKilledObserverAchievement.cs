using UnityEngine;
using System.Collections;

public class EnemyKilledObserverAchievement : IGameEventObserver 
{
	private EnemyKilledSubject m_Subject = null;
	private AchievementSystem m_AchievementSystem = null;
	
	public EnemyKilledObserverAchievement(AchievementSystem  AchievementSystem)
	{
		m_AchievementSystem = AchievementSystem;
	}

	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as EnemyKilledSubject;
	}
	
	public override void Update()
	{
		//Debug.Log("EnemyKilledObserverAchievement.Update: Count["+ m_Subject.GetKilledCount() +"]");
		m_AchievementSystem.AddEnemyKilledCount();
	}
	
}