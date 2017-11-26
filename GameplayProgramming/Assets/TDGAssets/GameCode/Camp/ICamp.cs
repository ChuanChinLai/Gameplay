using UnityEngine;
using System.Collections.Generic;


public abstract class ICamp 
{
	protected GameObject m_GameObject = null;
	protected string m_Name = "Null";
	protected string m_IconSpriteName = ""; 
	protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;

	protected List<ITrainCommand> m_TrainCommands = new List<ITrainCommand>();
	protected float	m_CommandTimer = 0;	
	protected float m_TrainCoolDown = 0;

	protected ITrainCost m_TrainCost = null;

	protected TowerDefenseGame m_TDGame = null;

	public ICamp(GameObject GameObj, float TrainCoolDown, string Name, string IconSprite)
	{
		m_GameObject = GameObj;
		m_TrainCoolDown = TrainCoolDown;
		m_CommandTimer = m_TrainCoolDown;
		m_Name = Name;
		m_IconSpriteName = IconSprite;
		m_TrainCost = new TrainCost();
	}


	public void SetTowerDefenseGame(TowerDefenseGame TDGame) 
	{
		m_TDGame = TDGame;
	}


	public ENUM_Soldier GetSoldierType()
	{
		return m_emSoldier;
	}
	

	protected void AddTrainCommand( ITrainCommand Command )
	{
		m_TrainCommands.Add( Command );
	}


	public void RemoveLastTrainCommand()
	{
		if( m_TrainCommands.Count == 0 )
			return ;

		m_TrainCommands.RemoveAt( m_TrainCommands.Count - 1 );
	}


	public int GetTrainCommandCount()
	{
		return m_TrainCommands.Count;
	}


	public void RunCommand()
	{

		if( m_TrainCommands.Count == 0 )
			return;

		m_CommandTimer -= Time.deltaTime;

		if( m_CommandTimer > 0)
			return;

		m_CommandTimer = m_TrainCoolDown;

		m_TrainCommands[0].Execute();

		m_TrainCommands.RemoveAt( 0 );
	}
	

	public virtual int GetLevel()
	{
		return 0;
	}


	public virtual int GetLevelUpCost()
    {
        return 0;
    }

	public virtual void LevelUp()
    {

    }
	
	public virtual ENUM_Weapon GetWeaponType()
	{
		return ENUM_Weapon.Null;
	}


	public virtual int GetWeaponLevelUpCost()
    {
        return 0;
    }


	public virtual void WeaponLevelUp()
    {

    }


	public int GetOnTrainCount()
	{
		return m_TrainCommands.Count;
	}


	public float GetTrainTimer()
	{
		return m_CommandTimer;
	}


	public string GetName()
	{
		return m_Name;
	}


	public string GetIconSpriteName()
	{
		return m_IconSpriteName;
	}


	public void SetVisible(bool bValue)
	{
		m_GameObject.SetActive(bValue);
	}


	public bool GetVisible()
	{
		return m_GameObject.activeSelf;
	}

	public abstract int GetTrainCost();

	public abstract void Train();
}
