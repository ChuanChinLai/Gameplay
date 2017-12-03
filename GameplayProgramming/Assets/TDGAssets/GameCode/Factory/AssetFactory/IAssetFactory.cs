using UnityEngine;
using System.Collections;


public abstract class IAssetFactory
{

	public abstract GameObject LoadSoldier( string AssetName );


	public abstract GameObject LoadEnemy( string AssetName );


	public abstract GameObject LoadWeapon( string AssetName );


	public abstract GameObject LoadEffect( string AssetName );


	public abstract AudioClip  LoadAudioClip( string ClipName );


	public abstract Sprite	   LoadSprite( string SpriteName );
	
}
