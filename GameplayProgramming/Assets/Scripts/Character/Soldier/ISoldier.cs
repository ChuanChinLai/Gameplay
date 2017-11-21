using UnityEngine;
using System.Collections;


public enum ENUM_Soldier
{
	Null = 0,
	Rookie	= 1,	
	Sergeant= 2,	
	Captain = 3,	
	Captive	= 4, 
	Max,
}

public abstract class ISoldier : ICharacter
{
	protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;
	protected int		   m_MedalCount	= 0; 				
	protected const int	   MAX_MEDAL = 3; 					
	protected const int    MEDAL_VALUE_ID = 20;	

	public ISoldier()
	{

	}

	public ENUM_Soldier GetSoldierType()
	{
		return m_emSoldier;
	}

    public SoldierAttr GetSoldierValue()
    {
        return m_Attribute as SoldierAttr;
    }


    public override void UnderAttack(ICharacter Attacker)
    {

        m_Attribute.CalDmgValue(Attacker);

        if (m_Attribute.GetNowHP() <= 0)
        {
            DoPlayKilledSound();    
            DoShowKilledEffect();
            Killed();         
        }
    }


    public virtual void AddMedal()
    {
        if (m_MedalCount >= MAX_MEDAL)
            return;


        m_MedalCount++;

        int AttrID = m_MedalCount + MEDAL_VALUE_ID;

 //       IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();

 //       SoldierAttr SufAttr = theAttrFactory.GetEliteSoldierAttr(ENUM_AttrDecorator.Suffix, AttrID, m_Attribute as SoldierAttr);
 //       SetCharacterAttr(SufAttr);
    }

    //public override void RunVisitor(ICharacterVisitor Visitor)
    //{
    //    Visitor.VisitSoldier(this);
    //}

    public abstract void DoPlayKilledSound();

	public abstract void DoShowKilledEffect();	
}