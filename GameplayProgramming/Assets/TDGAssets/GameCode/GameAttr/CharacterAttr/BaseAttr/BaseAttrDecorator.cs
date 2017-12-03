using UnityEngine;
using System.Collections.Generic;


public class AdditionalAttr
{
	private int 	m_Strength;
	private int  	m_Agility;	
	private string 	m_Name;		
	
	public AdditionalAttr(int Strength, int Agility, string Name)
	{
		m_Strength = Strength;
		m_Agility = Agility;
		m_Name = Name;
	}
	
	public int GetStrength()
	{
		return m_Strength;
	}
	
	public int GetAgility()
	{
		return m_Agility;
	}
	
	public string GetName()
	{
		return m_Name;
	}
}

public abstract class BaseAttrDecorator : BaseAttr
{
	protected BaseAttr 			m_Component; 		
	protected AdditionalAttr 	m_AdditionialAttr;
	
	public void SetComponent(BaseAttr theComponent)
	{
		m_Component = theComponent;
	}

	public void SetAdditionalAttr(AdditionalAttr theAdditionalAttr)
	{
		m_AdditionialAttr = theAdditionalAttr;
	}

	public override int GetMaxHP()
	{
		return m_Component.GetMaxHP();
	}
	
	public override float GetMoveSpeed()
	{
		return m_Component.GetMoveSpeed();
	}
	
	public override string GetAttrName()
	{
		return m_Component.GetAttrName();
	}
}


public enum ENUM_AttrDecorator
{
	Prefix,
	Suffix,
}

public class PrefixBaseAttr : BaseAttrDecorator
{
	public PrefixBaseAttr()
	{}
	
	public override int GetMaxHP()
	{
		return m_AdditionialAttr.GetStrength() + m_Component.GetMaxHP();
	}
	
	public override float GetMoveSpeed()
	{
		return m_AdditionialAttr.GetAgility()*0.2f + m_Component.GetMoveSpeed();
	}
	
	public override string GetAttrName()
	{
		return m_AdditionialAttr.GetName() + m_Component.GetAttrName();
	}	
}


public class SuffixBaseAttr : BaseAttrDecorator
{
	public SuffixBaseAttr()
	{}
	
	public override int GetMaxHP()
	{
		return m_Component.GetMaxHP() + m_AdditionialAttr.GetStrength();
	}
	
	public override float GetMoveSpeed()
	{
		return m_Component.GetMoveSpeed() + m_AdditionialAttr.GetAgility()*0.2f;
	}
	
	public override string GetAttrName()
	{
		return m_Component.GetAttrName() + m_AdditionialAttr.GetName();
	}	
}


public class StrengthenBaseAttr : BaseAttrDecorator
{
	protected List<AdditionalAttr> 	m_AdditionialAttrs;

	public StrengthenBaseAttr()
	{}
	
	public override int GetMaxHP()
	{
		int MaxHP = m_Component.GetMaxHP();

		foreach(AdditionalAttr theAttr in m_AdditionialAttrs)
			MaxHP += theAttr.GetStrength();

		return MaxHP;
	}
	
	public override float GetMoveSpeed()
	{
		float MoveSpeed = m_Component.GetMoveSpeed();

		foreach(AdditionalAttr theAttr in m_AdditionialAttrs)
			MoveSpeed += theAttr.GetAgility()*0.2f;

		return MoveSpeed;
	}
	
	public override string GetAttrName()
	{ 
		return "Strengthen" + m_Component.GetAttrName();
	}	
}
