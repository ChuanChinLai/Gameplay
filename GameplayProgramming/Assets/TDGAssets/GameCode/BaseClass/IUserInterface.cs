using UnityEngine;
using System.Collections;

public abstract class IUserInterface
{
	protected TowerDefenseGame  m_TDGame = null;
	protected GameObject	 	m_RootUI = null;
	private bool 				m_bActive = true;

	public IUserInterface( TowerDefenseGame TDGame )
	{
		m_TDGame = TDGame;
	}

	public bool IsVisible()
	{
		return m_bActive;
	}

	public virtual void Show()
	{
		m_RootUI.SetActive(true);
		m_bActive = true;
	}

	public virtual void Hide()
	{
		m_RootUI.SetActive(false);
		m_bActive = false;
	}

	public virtual void Initialize(){}
	public virtual void Release(){}
	public virtual void Update(){}

}
