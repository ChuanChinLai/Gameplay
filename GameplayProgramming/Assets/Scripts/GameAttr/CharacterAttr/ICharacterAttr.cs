using UnityEngine;
using System.Collections;


public abstract class ICharacterAttr
{
	protected BaseAttr m_BaseAttr = null;
	protected int m_NowHP = 0;		
	protected IAttrStrategy m_AttrStrategy = null;

	public ICharacterAttr(){}

	
	public void SetBaseAttr( BaseAttr BaseAttr )
	{
		m_BaseAttr = BaseAttr;
	}

	
	public BaseAttr GetBaseAttr()
	{
		return m_BaseAttr;
	}
	
	
	public void SetAttStrategy(IAttrStrategy theAttrStrategy)
	{
		m_AttrStrategy = theAttrStrategy;
	}

	
	public IAttrStrategy GetAttStrategy()
	{
		return m_AttrStrategy;
	}

	
	public int GetNowHP()
	{
		return m_NowHP;
	}

	
	public virtual int GetMaxHP()
	{
		return m_BaseAttr.GetMaxHP();
	}

	public void FullNowHP()
	{
		m_NowHP = GetMaxHP();
	}
		
	public virtual float GetMoveSpeed()
	{
		return m_BaseAttr.GetMoveSpeed();
	}
		
	public virtual string GetAttrName()
	{
		return m_BaseAttr.GetAttrName();
	}

	public virtual void InitAttr()
	{
		m_AttrStrategy.InitAttr( this ); 
		FullNowHP();
	}

	public int GetAtkPlusValue()
	{
		return m_AttrStrategy.GetAtkPlusValue( this );
	}


	//public void CalDmgValue( ICharacter Attacker )
	//{
 //       // 取得武器功擊力
 //       int AtkValue = Attacker.GetAtkValue();

 //       // 減傷
 //       AtkValue -= m_AttrStrategy.GetDmgDescValue(this);

 //       // 扣去傷害
 //       m_NowHP -= AtkValue;
 //   }

}
