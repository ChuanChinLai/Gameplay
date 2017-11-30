using UnityEngine;
using System.Collections.Generic;

public abstract class ICharacter
{
	protected string m_Name = "";	
	
	protected GameObject m_GameObject = null;
	protected UnityEngine.AI.NavMeshAgent m_NavAgent = null;

	protected AudioSource  m_Audio	  = null;

	protected string 	m_IconSpriteName = "";
	protected string 	m_AssetName = "";
	protected int		m_AttrID   = 0;		
		
	protected bool m_bKilled = false;		
	protected bool m_bCheckKilled = false;		
	protected float m_RemoveTimer = 1.5f;	
	protected bool m_bCanRemove = false;

	private   IWeapon m_Weapon = null;			
	protected ICharacterAttr m_Attribute = null;
	protected ICharacterAI m_AI = null;


	public ICharacter(){}

	public void SetGameObject( GameObject theGameObject )
	{
		m_GameObject = theGameObject ;
		m_NavAgent = m_GameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
		m_Audio	= m_GameObject.GetComponent<AudioSource>();
	}

	public GameObject GetGameObject()
	{
		return m_GameObject;
	}

	public void Release()
	{
		if( m_GameObject != null)
			GameObject.Destroy( m_GameObject);
	}

	public string GetName()
	{
		return m_Name;
	}

	public string GetAssetName()
	{
		return m_AssetName;
	}

	public string  GetIconSpriteName()
	{
		return m_IconSpriteName ;
	}

	public int GetAttrID()
	{
		return m_AttrID;
	}

	public ICharacterAttr GetCharacterAttr()
	{
		return m_Attribute;
	}
		
	public string GetCharacterName()
	{
		return m_Name;	
	}

	public void Update()
	{
		if( m_bKilled)
		{
			m_RemoveTimer -= Time.deltaTime;
			if( m_RemoveTimer <=0 )
				m_bCanRemove = true;
		}
		
		m_Weapon.Update();
	}

	#region Movement	

	public void MoveTo( Vector3 Position )
	{
		m_NavAgent.SetDestination( Position );
	}

	public void StopMove()
	{
		m_NavAgent.Stop();
	}

	public Vector3 GetPosition()
	{
		return m_GameObject.transform.position;
	}
	#endregion
	
	#region AI

	public void SetAI(ICharacterAI CharacterAI)
	{
		m_AI = CharacterAI;
	}


	public void UpdateAI(List<ICharacter> Targets)
	{
		m_AI.Update(Targets);
	}

	public void RemoveAITarget( ICharacter Targets)
	{
		m_AI.RemoveAITarget(Targets);
	}
	#endregion

	#region Attack

	public void Attack( ICharacter Target)
	{
		SetWeaponAtkPlusValue(m_Attribute.GetAtkPlusValue());

		WeaponAttackTarget( Target);

		m_GameObject.GetComponent<CharacterMovement>().PlayAttackAnim();

		m_GameObject.transform.forward = Target.GetPosition() - GetPosition();
	}

	public abstract void UnderAttack( ICharacter Attacker);
	#endregion

	#region Weapon

	public void SetWeapon(IWeapon Weapon)
	{
		if( m_Weapon != null)
			m_Weapon.Release();

		m_Weapon = Weapon;
		
		m_Weapon.SetOwner(this);
		
		UnityTool.AttachToRefPos( m_GameObject, m_Weapon.GetGameObject(),"weapon-point" ,Vector3.zero);
	}
	
	public IWeapon GetWeapon()
	{
		return m_Weapon;
	}

	protected void SetWeaponAtkPlusValue(int Value)
	{
		m_Weapon.SetAtkPlusValue( Value );
	}

	protected void WeaponAttackTarget( ICharacter Target)
	{
		m_Weapon.Fire( Target );
	}
	
	public int GetAtkValue()
	{
		return m_Weapon.GetAtkValue();
	}

	public float GetAttackRange()
	{
		return m_Weapon.GetAtkRange();
	}		
	#endregion

	#region Killed

	public void Killed()
	{
		if( m_bKilled == true)
			return;

		m_bKilled = true;

		m_bCheckKilled = false;
	}

	public bool IsKilled()
	{
		return m_bKilled; 
	}

	public bool CheckKilledEvent()
	{
		if( m_bCheckKilled)
			return true;

		m_bCheckKilled = true;

		return false;
	}

	public bool CanRemove()
	{
		return m_bCanRemove;
	}
	#endregion

	#region Attribute

	public virtual void SetCharacterAttr( ICharacterAttr CharacterAttr)
	{
		m_Attribute = CharacterAttr;
		m_Attribute.InitAttr ();

		m_NavAgent.speed = m_Attribute.GetMoveSpeed();
		m_Name = m_Attribute.GetAttrName();
	}
	#endregion

	#region Sound

	public void PlaySound(string ClipName)
	{
		IAssetFactory Factory = PBDFactory.GetAssetFactory();
		AudioClip theClip = Factory.LoadAudioClip( ClipName);

		if(theClip == null)
			return;

		m_Audio.clip = theClip;
		m_Audio.Play();
	}

	public void PlayEffect(string EffectName)
	{
		IAssetFactory Factory = PBDFactory.GetAssetFactory();
		GameObject EffectObj = Factory.LoadEffect( EffectName );

		if(EffectObj == null)
			return ;

		UnityTool.Attach( m_GameObject, EffectObj, Vector3.zero); 
	}
	#endregion

	public virtual void RunVisitor(ICharacterVisitor Visitor)
	{
		Visitor.VisitCharacter(this);
	}
} 




