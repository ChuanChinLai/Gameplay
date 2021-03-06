﻿using UnityEngine;
using System.Collections;

public class SoldierRookie : ISoldier
{	
	public SoldierRookie()
	{
		m_emSoldier = ENUM_Soldier.Rookie;
		m_AssetName = "Soldier1";
		m_IconSpriteName = "RookieIcon";
		m_AttrID   = 1;
	}

	public override void DoPlayKilledSound()
	{
		PlaySound( "RookieDeath" );
	}
	
	public override void DoShowKilledEffect()
	{
		PlayEffect( "RookieDeadEffect" );
	}

	public override void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitSoldierRookie(this);
	}

}