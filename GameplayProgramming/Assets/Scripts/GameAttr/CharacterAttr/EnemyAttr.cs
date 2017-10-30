using UnityEngine;
using System.Collections;

public class EnemyBaseAttr : CharacterBaseAttr
{
    public int m_InitCritRate;

    public EnemyBaseAttr(int MaxHP, float MoveSpeed, string AttrName, int CritRate) : base(MaxHP, MoveSpeed, AttrName)
    {
        m_InitCritRate = CritRate;
    }

    public virtual int GetInitCritRate()
    {
        return m_InitCritRate;
    }
}


public class EnemyAttr : ICharacterAttr
{
	protected int m_CritRate = 0;

	public EnemyAttr()
	{}

	public void SetEnemyAttr(EnemyBaseAttr EnemyBaseAttr)
	{
		
		base.SetBaseAttr( EnemyBaseAttr );

		
		m_CritRate = EnemyBaseAttr.GetInitCritRate();
	}
	
	
	public int GetCritRate()
	{
		return m_CritRate;
	}

	
	public void CutdownCritRate()
	{
		m_CritRate -= m_CritRate / 2;
	}

}
