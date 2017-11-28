using UnityEngine;
using System.Collections;


public class NormalStageHandler : IStageHandler 
{
	public NormalStageHandler(IStageScore StateScore, IStageData StageData )
	{
		m_StageScore  = StateScore;
		m_StatgeData  = StageData;
	}
		
	public override IStageHandler CheckStage()
	{
		if( m_StageScore.CheckScore() == false)
			return this;

		if(m_NextHandler == null)
			return this;		

		return m_NextHandler.CheckStage();
	}
	
	public override void Update()
	{
		m_StatgeData.Update();
	}

	public override void Reset()
	{
		m_StatgeData.Reset();
	}

	public override bool IsFinished()
	{
		return m_StatgeData.IsFinished();
	}


	public override int  LoseHeart()
	{
		return 1;
	}
}
