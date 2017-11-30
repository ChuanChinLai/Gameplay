using UnityEngine;
using System.Collections;

public class EnemyCaptive : IEnemy
{
	private ISoldier m_Captive = null;

	public EnemyCaptive( ISoldier theSoldier, Vector3 AttackPos)
	{
		m_emEnemyType = ENUM_Enemy.Catpive;
		m_Captive = theSoldier;

		SetGameObject( theSoldier.GetGameObject() );

		EnemyAttr tempAttr = new EnemyAttr();
		SetCharacterAttr( tempAttr );

		SetWeapon( theSoldier.GetWeapon() );

		m_AI = new EnemyAI( this, AttackPos );
		m_AI.ChangeAIState( new IdleAIState() );
	}

	public override void DoPlayHitSound()
	{
		m_Captive.DoPlayKilledSound();
	}
	
	public override void DoShowHitEffect()
	{
		m_Captive.DoShowKilledEffect();
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		//
	}

}
