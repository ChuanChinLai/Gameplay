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
		// 取得Soldier的數值
		IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
		int AttrID = m_BuildParam.NewCharacter.GetAttrID();
		SoldierAttr theSoldierAttr = theAttrFactory.GetSoldierAttr( AttrID ); 

		// 設定
		theSoldierAttr.SetAttStrategy( new SoldierAttrStrategy() );

		// 設定等級
		theSoldierAttr.SetSoldierLv( m_BuildParam.Lv );
		
		// 設定給角色
		m_BuildParam.NewCharacter.SetCharacterAttr( theSoldierAttr );
	}

	// 加入AI
	public override void AddAI()
	{
		SoldierAI theAI = new SoldierAI( m_BuildParam.NewCharacter );
		m_BuildParam.NewCharacter.SetAI( theAI );
	}

	// 加入管理器
	public override void AddCharacterSystem( TowerDefenseGame TDGame )
	{
		TDGame.AddSoldier( m_BuildParam.NewCharacter as ISoldier );
	}
}