using UnityEngine;
using System.Collections;

public class EnemyKilledSubject : IGameEventSubject
{
	private	int	m_KilledCount = 0;

	private IEnemy m_Enemy = null;

	public EnemyKilledSubject()
	{
    }

	public IEnemy GetEnemy()
	{
		return m_Enemy;
	}


	public int GetKilledCount()
	{
		return m_KilledCount;
	}

	public override void SetParam( System.Object Param )
	{
		base.SetParam(Param);
		m_Enemy = Param as IEnemy;
		m_KilledCount++;

		Notify();
	}
}
