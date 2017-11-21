using UnityEngine;
using System.Collections;

public class WeaponRocket : IWeapon 
{
	public WeaponRocket()
	{
		m_emWeaponType = ENUM_Weapon.Rocket;
	}

	protected override void DoShowBulletEffect( ICharacter theTarget)
	{
		ShowBulletEffect(theTarget.GetPosition(),0.8f,0.5f);
	}
	
	protected override void DoShowSoundEffect()
	{
		ShowSoundEffect("RocketShot");
	}
}
