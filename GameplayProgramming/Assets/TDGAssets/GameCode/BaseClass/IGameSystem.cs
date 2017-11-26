using UnityEngine;
using System.Collections;

public abstract class IGameSystem
{
	protected TowerDefenseGame m_TDGame = null;

	public IGameSystem( TowerDefenseGame TDGame )
	{
		m_TDGame = TDGame;
	}

	public virtual void Initialize(){}
	public virtual void Release(){}
	public virtual void Update(){}

}
