using UnityEngine;
using System.Collections;

public class EnemyBuildParam : ICharacterBuildParam
{
	public Vector3		AttackPosition = Vector3.zero;

	public EnemyBuildParam()
	{

	}
}

public class EnemyBuilder : ICharacterBuilder
{
	private EnemyBuildParam m_BuildParam = null;

	public override void SetBuildParam( ICharacterBuildParam theParam )
	{
		m_BuildParam = theParam as EnemyBuildParam;	
	}

	public override void LoadAsset( int GameObjectID )
	{
		IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
		GameObject EnemyGameObject = AssetFactory.LoadEnemy( m_BuildParam.NewCharacter.GetAssetName() );
		EnemyGameObject.transform.position = m_BuildParam.SpawnPosition;
		EnemyGameObject.gameObject.name = string.Format("Enemy[{0}]",GameObjectID);
		m_BuildParam.NewCharacter.SetGameObject( EnemyGameObject );
	}

	public override void AddOnClickScript()
	{

	}

	public override void AddWeapon()
	{
		IWeaponFactory  WeaponFactory = PBDFactory.GetWeaponFactory();
		IWeapon Weapon = WeaponFactory.CreateWeapon( m_BuildParam.emWeapon ); 
		m_BuildParam.NewCharacter.SetWeapon( Weapon );
	}
	

	public override void SetCharacterAttr()
	{
		IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
		int AttrID = m_BuildParam.NewCharacter.GetAttrID();
		EnemyAttr theEnemyAttr = theAttrFactory.GetEnemyAttr( AttrID ); 

		theEnemyAttr.SetAttStrategy( new EnemyAttrStrategy() );

        m_BuildParam.NewCharacter.SetCharacterAttr( theEnemyAttr );
	}

	public override void AddAI()
	{
		EnemyAI theAI = new EnemyAI( m_BuildParam.NewCharacter, m_BuildParam.AttackPosition );
		m_BuildParam.NewCharacter.SetAI( theAI);
	}

	public override void AddCharacterSystem( TowerDefenseGame TDGame)
	{
		TDGame.AddEnemy( m_BuildParam.NewCharacter as IEnemy );
	}
}
