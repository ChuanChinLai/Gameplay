using UnityEngine;
using System.Collections;

public enum ENUM_Weapon
{
	Null 	= 0,
	Gun 	= 1,
	Rifle	= 2,	
	Rocket	= 3,	
	Max	,
}

public abstract class IWeapon
{
	protected ENUM_Weapon m_emWeaponType = ENUM_Weapon.Null;

	protected int		   m_AtkPlusValue = 0;		  	
	//protected int 	   m_Atk = 0; 					
	//protected float 	   m_Range= 0.0f;				
	protected WeaponAttr m_WeaponAttr = null;		  	

	protected GameObject  m_GameObject = null;			
	protected ICharacter  m_WeaponOwner = null;			

	protected float			 m_EffectDisplayTime = 0;
	protected ParticleSystem m_Particles;                    
	protected LineRenderer   m_Line;                           
	protected AudioSource 	 m_Audio;                           
	protected Light 		 m_Light;                                 
	
	public IWeapon(){}
	
	public ENUM_Weapon GetWeaponType()
	{
		return  m_emWeaponType;
	}


	public void SetGameObject( GameObject theGameObject )
	{
		m_GameObject = theGameObject ;

		SetupEffect();
	}

	public GameObject GetGameObject()
	{
		return m_GameObject;
	}

	public void SetOwner( ICharacter Owner )
	{
		m_WeaponOwner = Owner;
	}

	public void SetWeaponAttr(WeaponAttr theWeaponAttr)
	{
        m_WeaponAttr = theWeaponAttr;
	}

	public void SetAtkPlusValue(int Value)
	{
		m_AtkPlusValue = Value;
	}

	public int GetAtkValue()
	{
		return m_WeaponAttr.Atk + m_AtkPlusValue;
	}

	public float GetAtkRange()
	{
		return m_WeaponAttr.AtkRange;
	}

	public void Release()
	{
		if( m_GameObject != null)
			GameObject.Destroy( m_GameObject);
	}

	public void Update()
	{
		if( m_EffectDisplayTime > 0 )
		{
			m_EffectDisplayTime -= Time.deltaTime;

			if(m_EffectDisplayTime <= 0)
				DisableEffect();
		}
	}


	protected void SetupEffect()
	{
		GameObject EffectObj = UnityTool.FindChildGameObject( m_GameObject ,"Effect");

		m_Line = EffectObj.GetComponent <LineRenderer> ();
		m_Particles = EffectObj.GetComponent<ParticleSystem> ();
		m_Audio = EffectObj.GetComponent<AudioSource> ();
		m_Light = EffectObj.GetComponent<Light> ();
	}

	protected void DisableEffect()
	{
		if(m_Line!=null)
			m_Line.enabled = false;

		if(m_Light!=null)
			m_Light.enabled = false;
	}


	protected void ShowBulletEffect(Vector3 TargetPosition, float LineWidth,float DisplayTime)
	{
		if( m_Line ==null)
			return ;
		m_Line.enabled = true;
		m_Line.SetWidth( LineWidth,LineWidth);
		m_Line.SetPosition(0,m_GameObject.transform.position);
		m_Line.SetPosition(1,TargetPosition);
		m_EffectDisplayTime = DisplayTime;
	}


	protected void ShowShootEffect()
	{
		if( m_Particles != null)
		{
			m_Particles.Stop ();
			m_Particles.Play ();		
		}

		if( m_Light !=null)
			m_Line.enabled = true;
	}


	protected void ShowSoundEffect(string ClipName)
	{
		if(m_Audio == null)
			return;

//		IAssetFactory Factory = PBDFactory.GetAssetFactory();
		//AudioClip theClip = Factory.LoadAudioClip( ClipName);
		//if(theClip == null)
		//	return ;
		//m_Audio.clip = theClip;
		//m_Audio.Play();
	}


	public void Fire( ICharacter theTarget )
	{
		ShowShootEffect();

		DoShowBulletEffect( theTarget );

		DoShowSoundEffect();
		
		theTarget.UnderAttack( m_WeaponOwner );
	}
	
	protected abstract void DoShowBulletEffect( ICharacter theTarget );

	protected abstract void DoShowSoundEffect();
}

