using UnityEngine;
using System.Collections;

public abstract class IStageHandler
{
	protected IStageHandler m_NextHandler = null;
	protected IStageData	m_StatgeData  = null;
	protected IStageScore   m_StageScore  = null;

	public IStageHandler SetNextHandler(IStageHandler NextHandler)
	{
		m_NextHandler = NextHandler;
		return m_NextHandler;
	}

	public abstract IStageHandler CheckStage();
	public abstract void Update();
	public abstract void Reset();
	public abstract bool IsFinished();
	public abstract int  LoseHeart();
}
