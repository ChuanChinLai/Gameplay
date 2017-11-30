using UnityEngine;
using System.Collections;

public class EnemyElf  : IEnemy
{
	public EnemyElf()
	{
		m_emEnemyType = ENUM_Enemy.Elf;
		m_AssetName = "Enemy1";
		m_AttrID   = 1;
		m_IconSpriteName = "ElfIcon";
	}
	
	public override void DoPlayHitSound()
	{

	}
	
	public override void DoShowHitEffect()
	{
		PlayEffect( "ElfHitEffect" );
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitEnemyElf(this);
	}
}
