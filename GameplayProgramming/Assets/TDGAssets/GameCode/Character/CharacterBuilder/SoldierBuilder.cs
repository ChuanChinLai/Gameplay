using UnityEngine;
using System.Collections;

public class SoldierBuildParam : ICharacterBuildParam
{
	public int 			Lv = 0;

	public SoldierBuildParam()
	{

	}
}

public class SoldierBuilder : ICharacterBuilder
{
	private SoldierBuildParam m_BuildParam = null;

	public override void SetBuildParam( ICharacterBuildParam theParam )
	{
		m_BuildParam = theParam as SoldierBuildParam;	
	}
	
	public override void LoadAsset( int GameObjectID )
	{
		IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
		GameObject SoldierGameObject = AssetFactory.LoadSoldier( m_BuildParam.NewCharacter.GetAssetName() );
		SoldierGameObject.transform.position = m_BuildParam.SpawnPosition;
		SoldierGameObject.gameObject.name = string.Format("Soldier[{0}]",GameObjectID);
		m_BuildParam.NewCharacter.SetGameObject( SoldierGameObject );
	}

	public override void AddOnClickScript()
	{
		SoldierOnClick Script = m_BuildParam.NewCharacter.GetGameObject().AddComponent<SoldierOnClick>();
		Script.Solder = m_BuildParam.NewCharacter as ISoldier;
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
		SoldierAttr theSoldierAttr = theAttrFactory.GetSoldierAttr( AttrID ); 

		theSoldierAttr.SetAttStrategy( new SoldierAttrStrategy() );
		theSoldierAttr.SetSoldierLv( m_BuildParam.Lv );
		m_BuildParam.NewCharacter.SetCharacterAttr( theSoldierAttr );
	}

	public override void AddAI()
	{
		SoldierAI theAI = new SoldierAI( m_BuildParam.NewCharacter );
		m_BuildParam.NewCharacter.SetAI( theAI );
	}

	public override void AddCharacterSystem( TowerDefenseGame TDGame )
	{
		TDGame.AddSoldier( m_BuildParam.NewCharacter as ISoldier );
	}
}