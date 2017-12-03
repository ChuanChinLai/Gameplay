using UnityEngine;
using System.Collections;

public class WeaponRifle : IWeapon 
{
	public WeaponRifle()
	{
		m_emWeaponType = ENUM_Weapon.Rifle;
	}
	
	protected override void DoShowBulletEffect( ICharacter theTarget )
	{
		ShowBulletEffect(theTarget.GetPosition(),0.5f,0.2f);
	}
	
	protected override void DoShowSoundEffect()
	{
		ShowSoundEffect("RifleShot");
	}

}
