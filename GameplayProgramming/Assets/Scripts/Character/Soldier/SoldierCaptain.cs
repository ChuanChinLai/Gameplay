using UnityEngine;
using System.Collections;

// 上尉
public class SoldierCaptain : ISoldier
{	
	public SoldierCaptain()
	{
		m_emSoldier = ENUM_Soldier.Captain;
		m_AssetName = "Soldier3";
		m_IconSpriteName = "CaptainIcon";
		m_AttrID   = 3;
	}


	public override void DoPlayKilledSound()
	{
//		PlaySound( "CaptainDeath" );
	}
	
	public override void DoShowKilledEffect()
	{
//		PlayEffect( "CaptainDeadEffect" );
	}


	//public override void RunVisitor(ICharacterVisitor Visitor)
	//{
	//	Visitor.VisitSoldierCaptain(this);
	//}

}