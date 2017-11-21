using UnityEngine;
using System.Collections;

public class EnemyOgre  : IEnemy
{
	public EnemyOgre()
	{
		m_emEnemyType = ENUM_Enemy.Ogre;
		m_AssetName = "Enemy3";
		m_AttrID   = 3;
		m_IconSpriteName = "OgreIcon";
	}
	

	public override void DoPlayHitSound()
	{
		//Debug.Log ("EnemyOgre.PlayHitSound");
	}
	
	public override void DoShowHitEffect()
	{
//		PlayEffect( "OgreHitEffect" );
	}

	//public override void RunVisitor(ICharacterVisitor Visitor)
	//{
	//	Visitor.VisitEnemyOgre(this);
	//}
}
