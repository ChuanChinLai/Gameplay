using UnityEngine;
using System.Collections;

public class WeaponGun : IWeapon 
{
	public WeaponGun()
	{
		m_emWeaponType = ENUM_Weapon.Gun;
	}

	protected override void DoShowBulletEffect( ICharacter theTarget )
	{
		ShowBulletEffect(theTarget.GetPosition(),0.03f,0.2f);
	}
	
	protected override void DoShowSoundEffect()
	{
		ShowSoundEffect("GunShot");
	}
}
