using UnityEngine;
using System.Collections;

public class CharacterFactory : ICharacterFactory
{
	private CharacterBuilderSystem m_BuilderDirector = new CharacterBuilderSystem( TowerDefenseGame.Instance );

	public override ISoldier CreateSoldier( ENUM_Soldier emSoldier, ENUM_Weapon emWeapon, int Lv, Vector3 SpawnPosition)
	{
		SoldierBuildParam SoldierParam = new SoldierBuildParam();

		switch( emSoldier)
		{
		case ENUM_Soldier.Rookie:
			SoldierParam.NewCharacter = new SoldierRookie();
			break;
		case ENUM_Soldier.Sergeant:
			SoldierParam.NewCharacter = new SoldierSergeant();
			break;
		case ENUM_Soldier.Captain:
			SoldierParam.NewCharacter = new SoldierCaptain();
			break;
		default:
            Debug.LogWarning("ERROR");
			return null;
		}

		if( SoldierParam.NewCharacter == null)
			return null;

		SoldierParam.emWeapon = emWeapon;
		SoldierParam.SpawnPosition = SpawnPosition;
		SoldierParam.Lv		  = Lv;


		SoldierBuilder theSoldierBuilder = new SoldierBuilder();
		theSoldierBuilder.SetBuildParam( SoldierParam ); 
		

		m_BuilderDirector.Construct( theSoldierBuilder );
		return SoldierParam.NewCharacter  as ISoldier;
	}
	

	public override IEnemy CreateEnemy( ENUM_Enemy emEnemy, ENUM_Weapon emWeapon, Vector3 SpawnPosition, Vector3 AttackPosition)
	{
		EnemyBuildParam EnemyParam = new EnemyBuildParam();

		switch( emEnemy)
		{
		case ENUM_Enemy.Elf:
			EnemyParam.NewCharacter = new EnemyElf();
			break;
		case ENUM_Enemy.Troll:
			EnemyParam.NewCharacter = new EnemyTroll();
			break;
		case ENUM_Enemy.Ogre:
			EnemyParam.NewCharacter = new EnemyOgre();
			break;
		default:
			Debug.LogWarning("Error");
			return null;
		}

		if( EnemyParam.NewCharacter == null)
			return null;


		EnemyParam.emWeapon = emWeapon;
		EnemyParam.SpawnPosition = SpawnPosition;
		EnemyParam.AttackPosition = AttackPosition;
				

		EnemyBuilder theEnemyBuilder = new EnemyBuilder();
		theEnemyBuilder.SetBuildParam( EnemyParam ); 
		

		m_BuilderDirector.Construct( theEnemyBuilder );
		return EnemyParam.NewCharacter  as IEnemy;
	}

}


