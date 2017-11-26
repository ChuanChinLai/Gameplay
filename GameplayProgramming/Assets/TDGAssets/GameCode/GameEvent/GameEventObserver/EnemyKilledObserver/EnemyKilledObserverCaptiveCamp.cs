using UnityEngine;
using System.Collections;

public class EnemyKilledObserverCaptiveCamp : IGameEventObserver 
{
	private EnemyKilledSubject m_Subject = null;
	private CampSystem m_CampSystem = null;
	
	public EnemyKilledObserverCaptiveCamp(CampSystem  theCampSystem)
	{
		m_CampSystem = theCampSystem;
	}

	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as EnemyKilledSubject;
	}
	
	public override void Update()
	{
		if( m_Subject.GetKilledCount() > 10 ) 
			m_CampSystem.ShowCaptiveCamp();
	}
	
}