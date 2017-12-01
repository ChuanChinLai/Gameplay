using UnityEngine;
using System.Collections;

public class UnitCountVisitor : ICharacterVisitor 
{

	public int CharacterCount = 0;
	public int SoldierCount = 0;
	public int SoldierRookieCount = 0;
	public int SoldierSergeantCount = 0;
	public int SoldierCaptainCount = 0;
	public int SoldierCaptiveCount = 0;
	public int EnemyCount = 0;
	public int EnemyElfCount = 0;
	public int EnemyTrollCount = 0;
	public int EnemyOgreCount = 0;
		
	public override void VisitCharacter(ICharacter Character)
	{
		base.VisitCharacter(Character);
		CharacterCount++;
	}
	
	public override void VisitSoldier(ISoldier Soldier)
	{
		base.VisitSoldier(Soldier);
		SoldierCount++;
	}
	
	public override void VisitSoldierRookie(SoldierRookie Rookie)
	{
		base.VisitSoldierRookie(Rookie);
		SoldierRookieCount++;
	}
	
	public override void VisitSoldierSergeant(SoldierSergeant Sergeant)
	{
		base.VisitSoldierSergeant(Sergeant);
		SoldierSergeantCount++;
	}
	
	public override void VisitSoldierCaptain(SoldierCaptain Captain)
	{
		base.VisitSoldierCaptain(Captain);
		SoldierCaptainCount++;
	}
	
	public override void VisitSoldierCaptive(SoldierCaptive Captive)
	{
		base.VisitSoldierCaptive(Captive);
		SoldierCaptiveCount++;
	}
	
	public override void VisitEnemy(IEnemy Enemy)
	{
		base.VisitEnemy(Enemy);
		EnemyCount++;
	}
	
	public override void VisitEnemyElf(EnemyElf Elf)
	{
		base.VisitEnemyElf(Elf);
		EnemyElfCount++;
	}
	
	public override void VisitEnemyTroll(EnemyTroll Troll)
	{
		base.VisitEnemyTroll(Troll);
		EnemyTrollCount++;
	}
	
	public override void VisitEnemyOgre(EnemyOgre Ogre)
	{
		base.VisitEnemyOgre(Ogre);
		EnemyOgreCount++;
	}

	public void Reset()
	{
		CharacterCount = 0;
		SoldierCount = 0;
		SoldierRookieCount = 0;
		SoldierSergeantCount = 0;
		SoldierCaptainCount = 0;
		SoldierCaptiveCount = 0;
		EnemyCount = 0;
		EnemyElfCount = 0;
		EnemyTrollCount = 0;
		EnemyOgreCount = 0;	
	}

	public int GetUnitCount( ENUM_Soldier emSoldier)
	{
		switch( emSoldier)
		{
		case ENUM_Soldier.Null:
			return SoldierCount;
		case ENUM_Soldier.Rookie:
			return SoldierRookieCount;
		case ENUM_Soldier.Sergeant:
			return SoldierSergeantCount;
		case ENUM_Soldier.Captain:
			return SoldierCaptainCount;
		case ENUM_Soldier.Captive:
			return SoldierCaptiveCount;
		default: 
			break;
		}
		return 0;
	}
	
	public int GetUnitCount( ENUM_Enemy emEnemy)
	{
		switch( emEnemy)
		{
		case ENUM_Enemy.Null:
			return EnemyCount;
		case ENUM_Enemy.Elf:
			return EnemyElfCount;
		case ENUM_Enemy.Troll:
			return EnemyTrollCount;
		case ENUM_Enemy.Ogre:
			return EnemyOgreCount;
		default: 
			break;
		}
		return 0;
	}

}
