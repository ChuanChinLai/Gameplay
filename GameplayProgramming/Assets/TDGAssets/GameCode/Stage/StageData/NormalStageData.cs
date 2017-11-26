using UnityEngine;
using System.Collections.Generic;


public class NormalStageData : IStageData 
{
	private float m_CoolDown = 0;
	private float m_MaxCoolDown = 0;
	private Vector3 m_SpawnPosition = Vector3.zero;
	private Vector3 m_AttackPosition = Vector3.zero;
	private bool 	m_AllEnemyBorn = false;
	

	class StageData
	{
		public ENUM_Enemy emEnemy = ENUM_Enemy.Null;
		public ENUM_Weapon emWeapon = ENUM_Weapon.Null;
		public bool bBorn = false;
		public StageData( ENUM_Enemy emEnemy, ENUM_Weapon emWeapon )
		{
			this.emEnemy = emEnemy;
			this.emWeapon= emWeapon;
		}
	}


	private List<StageData> m_StageData = new List<StageData>(); 
	
	public NormalStageData(float CoolDown ,Vector3 SpawnPosition, Vector3 AttackPosition)
	{
		m_MaxCoolDown = CoolDown;
		m_CoolDown = m_MaxCoolDown;
		m_SpawnPosition = SpawnPosition;
		m_AttackPosition = AttackPosition;
	}


	public void AddStageData( ENUM_Enemy emEnemy, ENUM_Weapon emWeapon,int Count)
	{
		for(int i = 0; i < Count; ++i)
			m_StageData.Add ( new StageData(emEnemy, emWeapon));
	}

	public override	void Reset()
	{
		foreach( StageData pData in m_StageData)
			pData.bBorn = false;		

		m_AllEnemyBorn = false;
	}

	public override void Update()
	{
		if( m_StageData.Count == 0)
			return;

		m_CoolDown -= Time.deltaTime;

		if( m_CoolDown > 0)
			return;

		m_CoolDown = m_MaxCoolDown;

		StageData theNewEnemy = GetEnemy();

		if(theNewEnemy == null)
			return;

		ICharacterFactory Factory = PBDFactory.GetCharacterFactory();
		Factory.CreateEnemy( theNewEnemy.emEnemy, theNewEnemy.emWeapon, m_SpawnPosition, m_AttackPosition);
	}

	private StageData GetEnemy()
	{
		foreach( StageData pData in m_StageData)
		{
			if(pData.bBorn == false)
			{
				pData.bBorn = true;
				return pData;
			}
		}
		m_AllEnemyBorn = true;
		return null;
	}

	public override	bool IsFinished()
	{
		return m_AllEnemyBorn;
	}
}
