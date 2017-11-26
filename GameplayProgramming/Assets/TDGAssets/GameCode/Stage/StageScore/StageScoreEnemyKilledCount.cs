using UnityEngine;
using System.Collections;


public class StageScoreEnemyKilledCount :  IStageScore
{
	private int m_EnemyKilledCount = 0;
	private StageSystem m_StageSystem = null;
	
	public StageScoreEnemyKilledCount( int KilledCount, StageSystem theStageSystem)
	{
		m_EnemyKilledCount = KilledCount;
		m_StageSystem = theStageSystem;
	}

	public override bool CheckScore()
	{
		return ( m_StageSystem.GetEnemyKilledCount() >=  m_EnemyKilledCount);
	}
}
