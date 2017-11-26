using UnityEngine;
using System.Collections;

public class SoldierCamp : ICamp
{
	const int MAX_LV = 3;	
	ENUM_Weapon	 m_emWeapon = ENUM_Weapon.Gun;
	int	m_Lv = 1;								
	Vector3 m_Position;							

	public SoldierCamp(GameObject theGameObject, ENUM_Soldier emSoldier, string CampName, string IconSprite ,float TrainCoolDown,Vector3 Position) : base(theGameObject, TrainCoolDown, CampName, IconSprite)
	{
		m_emSoldier = emSoldier;			
		m_Position = Position;
	}

	public override int GetLevel()
	{
		return m_Lv;
	}

	public override int GetLevelUpCost()
	{ 
		if( m_Lv >= MAX_LV)
			return 0;

		return 100;
	}

	public override void LevelUp()
	{
		m_Lv++;
		m_Lv = Mathf.Min( m_Lv , MAX_LV);
	}
	

	public override ENUM_Weapon GetWeaponType()
	{
		return m_emWeapon;
	}


	public override int GetWeaponLevelUpCost()
	{ 
		if( (m_emWeapon + 1) >= ENUM_Weapon.Max )
			return 0;

		return 100;
	}
	

	public override void WeaponLevelUp()
	{
		m_emWeapon++;

		if( m_emWeapon >= ENUM_Weapon.Max)
			m_emWeapon--;
	}


	public override int GetTrainCost()
	{
		return m_TrainCost.GetTrainCost( m_emSoldier, m_Lv, m_emWeapon) ;
	}


	public override void Train()
	{
		TrainSoldierCommand NewCommand = new TrainSoldierCommand( m_emSoldier, m_emWeapon, m_Lv, m_Position);  
		AddTrainCommand( NewCommand );
	}
}
