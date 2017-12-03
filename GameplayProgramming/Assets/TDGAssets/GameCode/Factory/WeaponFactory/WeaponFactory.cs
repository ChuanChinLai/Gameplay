using UnityEngine;
using System.Collections;

public class WeaponFactory : IWeaponFactory 
{
	public WeaponFactory()
	{

	}

	public override IWeapon CreateWeapon( ENUM_Weapon emWeapon)
	{
		IWeapon pWeapon = null;
		string	AssetName = "";
		int		AttrID = 0;

		switch( emWeapon )
		{
		case ENUM_Weapon.Gun:
			pWeapon = new WeaponGun();
			AssetName = "WeaponGun";
			AttrID	= 1;
			break;
		case ENUM_Weapon.Rifle:
			pWeapon = new WeaponRifle();
			AssetName = "WeaponRifle";
			AttrID	= 2;
			break;
		case ENUM_Weapon.Rocket:
			pWeapon = new WeaponRocket();
			AssetName = "WeaponRocket";
			AttrID	= 3;
			break;		
		default:
			Debug.LogWarning("ERROR");
			return null;
		}

		IAssetFactory AssetFactory = PBDFactory.GetAssetFactory();
		GameObject WeaponGameObjet = AssetFactory.LoadWeapon( AssetName );
		pWeapon.SetGameObject( WeaponGameObjet );

		IAttrFactory theAttrFactory = PBDFactory.GetAttrFactory();
		WeaponAttr theWeaponAttr = theAttrFactory.GetWeaponAttr( AttrID ); 

		pWeapon.SetWeaponAttr( theWeaponAttr );

		return pWeapon;
	}
	
}
