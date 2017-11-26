using UnityEngine;
using System.Collections;

public class SoldierKilledObserverAchievement : IGameEventObserver 
{
	private SoldierKilledSubject m_Subject = null;

	private AchievementSystem m_AchievementSystem = null;
	
	public SoldierKilledObserverAchievement(AchievementSystem  AchievementSystem)
	{
		m_AchievementSystem = AchievementSystem;
	}

	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as SoldierKilledSubject;
	}
	
	public override void Update()
	{
		m_AchievementSystem.AddSoldierKilledCount();
	}
	
}