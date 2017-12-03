using UnityEngine;
using System.Collections;

public class ResourceAssetFactory : IAssetFactory 
{
	public const string SoldierPath = "Characters/Soldier/";
	public const string EnemyPath = "Characters/Enemy/";
	public const string WeaponPath = "Weapons/";
	public const string EffectPath = "Effects/";
	public const string AudioPath  = "Audios/";
	public const string SpritePath = "Sprites/";


	public override GameObject LoadSoldier( string AssetName )
	{	
		return InstantiateGameObject( SoldierPath + AssetName );
	}
	

	public override GameObject LoadEnemy( string AssetName )
	{
		return InstantiateGameObject( EnemyPath + AssetName  );
	}


	public override GameObject LoadWeapon( string AssetName )
	{
		return InstantiateGameObject( WeaponPath +  AssetName );
	}


	public override GameObject LoadEffect( string AssetName )
	{
		return InstantiateGameObject( EffectPath + AssetName);
	}


	public override AudioClip  LoadAudioClip(string ClipName)
	{
		UnityEngine.Object res = LoadGameObjectFromResourcePath(AudioPath + ClipName );
		if(res==null)
			return null;
		return res as AudioClip;
	}


	public override Sprite LoadSprite(string SpriteName)
	{
		return Resources.Load(SpritePath + SpriteName,typeof(Sprite)) as Sprite;
	}


	private GameObject InstantiateGameObject( string AssetName )
	{
		UnityEngine.Object res = LoadGameObjectFromResourcePath( AssetName );

		if(res == null)
			return null;

		return  UnityEngine.Object.Instantiate(res) as GameObject;
	}


	public UnityEngine.Object LoadGameObjectFromResourcePath( string AssetPath)
	{
		UnityEngine.Object res = Resources.Load(AssetPath);
		if( res == null)
		{
			Debug.LogWarning("Error");
			return null;
		}		
		return res;
	}
}
