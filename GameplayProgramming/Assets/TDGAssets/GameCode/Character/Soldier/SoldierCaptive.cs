using UnityEngine;
using System.Collections;

public class SoldierCaptive : ISoldier 
{
	private IEnemy m_Captive = null;

	public SoldierCaptive( IEnemy theEnemy)
	{
		m_emSoldier = ENUM_Soldier.Captive;
		m_Captive = theEnemy;

		SetGameObject( theEnemy.GetGameObject() );

		SoldierAttr tempAttr = new SoldierAttr();
		tempAttr.SetSoldierAttr( theEnemy.GetCharacterAttr().GetBaseAttr() );
		tempAttr.SetAttStrategy( theEnemy.GetCharacterAttr().GetAttStrategy());
		tempAttr.SetSoldierLv( 1 );
		SetCharacterAttr( tempAttr );

		SetWeapon( theEnemy.GetWeapon() );

		m_AI = new SoldierAI( this );
		m_AI.ChangeAIState( new IdleAIState() );
	}

	public override void DoPlayKilledSound()
	{
		m_Captive.DoPlayHitSound();
	}
	
	public override void DoShowKilledEffect()
	{
		m_Captive.DoShowHitEffect();
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitSoldierCaptive(this);
	}

}
