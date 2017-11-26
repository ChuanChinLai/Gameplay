using UnityEngine;
using System.Collections;

public abstract class IStageData
{
	public abstract void Update();
	public abstract	bool IsFinished();
	public abstract	void Reset();
}
