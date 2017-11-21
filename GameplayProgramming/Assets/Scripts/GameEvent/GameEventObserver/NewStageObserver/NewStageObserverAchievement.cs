using UnityEngine;
using System.Collections;

public class NewStageObserverAchievement : IGameEventObserver 
{
	private NewStageSubject m_Subject = null;
	private AchievementSystem m_AchievementSystem = null;
	
	public NewStageObserverAchievement(AchievementSystem  AchievementSystem)
	{
		m_AchievementSystem = AchievementSystem;
	}

	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as NewStageSubject;
	}
	
	public override void Update()
	{
		m_AchievementSystem.SetNowStageLevel( m_Subject.GetStageCount() );
	}
	
}