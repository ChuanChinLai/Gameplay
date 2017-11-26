using UnityEngine;
using System.Collections;

public abstract class ICharacterBuildParam
{
	public ENUM_Weapon  emWeapon = ENUM_Weapon.Null;
	public ICharacter   NewCharacter = null;		
	public Vector3      SpawnPosition;
	public int          AttrID; 
	public string       AssetName;
	public string       IconSpriteName;
}

public abstract class ICharacterBuilder
{

	public abstract void SetBuildParam( ICharacterBuildParam theParam );

	public abstract void LoadAsset	( int GameObjectID );

	public abstract void AddOnClickScript();

	public abstract void AddWeapon	();

	public abstract void AddAI		();

	public abstract void SetCharacterAttr();

	public abstract void AddCharacterSystem( TowerDefenseGame TDGame ); 
}

