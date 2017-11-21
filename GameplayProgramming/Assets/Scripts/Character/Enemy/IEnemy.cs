using UnityEngine;
using System.Collections;


public enum ENUM_Enemy
{
	Null 	= 0,
	Elf		= 1,
	Troll	= 2,
	Ogre	= 3,
	Catpive = 4,
	Max,
}

public abstract class IEnemy : ICharacter
{
	protected ENUM_Enemy m_emEnemyType = ENUM_Enemy.Null;

	public IEnemy()
	{

    }

	public ENUM_Enemy GetEnemyType() 
	{
		return m_emEnemyType;
	}


    public override void UnderAttack(ICharacter Attacker)
    {

        m_Attribute.CalDmgValue(Attacker);

        DoPlayHitSound();
        DoShowHitEffect(); 

        if (m_Attribute.GetNowHP() <= 0)
        {
            Killed();
        }
    }

    //// 執行Visitor
    //public override void RunVisitor(ICharacterVisitor Visitor)
    //{
    //	Visitor.VisitEnemy(this);
    //}

    public abstract void DoPlayHitSound();
	

	public abstract void DoShowHitEffect();
}
