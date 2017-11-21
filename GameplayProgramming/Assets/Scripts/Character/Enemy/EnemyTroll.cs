using UnityEngine;
using System.Collections;

// 山妖
public class EnemyTroll : IEnemy
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
		//Debug.Log ("EnemyTroll.PlayHitSound");
	}
	
	public override void DoShowHitEffect()
	{
//		PlayEffect( "TrollHitEffect" );
	}

	// 執行Visitor
	//public override void RunVisitor(ICharacterVisitor Visitor)
	//{
	//	Visitor.VisitEnemyTroll(this);
	//}
}
