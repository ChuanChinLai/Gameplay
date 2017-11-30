using UnityEngine;
using System.Collections;


public class BossStageHandler : NormalStageHandler 
{
	public BossStageHandler(IStageScore StateScore, IStageData StageData ):base(StateScore,StageData) 
	{}

	public override int  LoseHeart()
	{
		return StageSystem.MAX_HEART;
	}
}

