using UnityEngine;
using System.Collections;

// 中士
public class SoldierSergeant : ISoldier
{	
	public SoldierSergeant()
	{
		m_emSoldier = ENUM_Soldier.Sergeant;
		m_AssetName = "Soldier2";
		m_IconSpriteName = "SergeantIcon";
		m_AttrID   = 2;
	}

	public override void DoPlayKilledSound()
	{
		PlaySound( "SergeantDeath" );
	}
	
	public override void DoShowKilledEffect()
	{
		PlayEffect( "SergeantDeadEffect" );
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitSoldierSergeant(this);
	}
}