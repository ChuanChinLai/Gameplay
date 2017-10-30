using UnityEngine;
using System.Collections;


public abstract class BaseAttr
{			
	public abstract int 	GetMaxHP();
	public abstract float 	GetMoveSpeed();
	public abstract string 	GetAttrName();
}


public class CharacterBaseAttr : BaseAttr
{
	private int 	m_MaxHP;		
	private float  	m_MoveSpeed;	
	private string 	m_AttrName;			

	public CharacterBaseAttr(int MaxHP,float MoveSpeed, string AttrName)
	{
		m_MaxHP = MaxHP;
		m_MoveSpeed = MoveSpeed;
		m_AttrName = AttrName;
	}

	public override int GetMaxHP()
	{
		return m_MaxHP;
	}

	public override float GetMoveSpeed()
	{
		return m_MoveSpeed;
	}

	public override string GetAttrName()
	{
		return m_AttrName;
	}
}
