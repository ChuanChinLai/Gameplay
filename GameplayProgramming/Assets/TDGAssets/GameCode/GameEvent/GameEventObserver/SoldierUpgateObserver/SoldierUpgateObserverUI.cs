using UnityEngine;
using System.Collections;


public class SoldierUpgateObserverUI : IGameEventObserver 
{
	private SoldierUpgateSubject m_Subject = null;
	private SoldierInfoUI m_InfoUI = null;

	public SoldierUpgateObserverUI( SoldierInfoUI InfoUI  )
	{
		m_InfoUI = InfoUI;
	}

	public override	void SetSubject( IGameEventSubject Subject )
	{
		m_Subject = Subject as SoldierUpgateSubject;
	}

	public override void Update()
	{
		m_InfoUI.RefreshSoldier( m_Subject.GetSoldier() );
	}

}
