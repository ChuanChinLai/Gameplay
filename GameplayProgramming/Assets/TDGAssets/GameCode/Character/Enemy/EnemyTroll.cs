using UnityEngine;
using System.Collections;

public class EnemyTroll  : IEnemy
{
	public EnemyTroll()
	{
		m_emEnemyType = ENUM_Enemy.Troll;
		m_AssetName = "Enemy3";
		m_AttrID   = 3;
		m_IconSpriteName = "OgreIcon";
	}
	
	public override void DoPlayHitSound()
	{

	}
	
	public override void DoShowHitEffect()
	{
		PlayEffect( "TrollHitEffect" );
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitEnemyTroll(this);
	}
}
