using UnityEngine;
using System.Collections.Generic;

public class AttrFactory : IAttrFactory
{
	private Dictionary<int, BaseAttr>  		m_SoldierAttrDB = null;
	private Dictionary<int, EnemyBaseAttr> 	m_EnemyAttrDB 	= null;
	private Dictionary<int, WeaponAttr> 	m_WeaponAttrDB 	= null;
	private Dictionary<int, AdditionalAttr> m_AdditionalAttrDB=null;
	
	public AttrFactory()
	{
		InitSoldierAttr();
		InitEnemyAttr();
		InitWeaponAttr();
		InitAdditionalAttr();
	}

	private void InitSoldierAttr()
	{
		m_SoldierAttrDB = new Dictionary<int, BaseAttr>();

		m_SoldierAttrDB.Add (  1, new CharacterBaseAttr(10, 3.0f, "新兵")); 
		m_SoldierAttrDB.Add (  2, new CharacterBaseAttr(20, 3.2f, "中士")); 
		m_SoldierAttrDB.Add (  3, new CharacterBaseAttr(30, 3.4f, "上尉")); 
	}

	private void InitEnemyAttr()
	{
		m_EnemyAttrDB 	= new Dictionary<int,EnemyBaseAttr>();

		m_EnemyAttrDB.Add (  1, new EnemyBaseAttr(5, 3.0f,"精靈",10) );
		m_EnemyAttrDB.Add (  2, new EnemyBaseAttr(15,3.1f,"山妖",20) ); 
		m_EnemyAttrDB.Add (  3, new EnemyBaseAttr(20,3.3f,"怪物",40) );
	}

	// 建立所有Weapon的數值
	private void InitWeaponAttr()
	{
		m_WeaponAttrDB 	= new Dictionary<int,WeaponAttr>();

		m_WeaponAttrDB.Add ( 1, new WeaponAttr( 2, 4 ,"短槍") );
		m_WeaponAttrDB.Add ( 2, new WeaponAttr( 4, 7, "長槍") );
		m_WeaponAttrDB.Add ( 3, new WeaponAttr( 8, 10,"火箭筒") );
	}

	private void InitAdditionalAttr()
	{
		m_AdditionalAttrDB = new Dictionary<int,AdditionalAttr>();

		m_AdditionalAttrDB.Add ( 11, new AdditionalAttr( 3, 0, "勇士")); 
		m_AdditionalAttrDB.Add ( 12, new AdditionalAttr( 5, 0, "猛將")); 
		m_AdditionalAttrDB.Add ( 13, new AdditionalAttr(10, 0, "英雄")); 
		
		m_AdditionalAttrDB.Add ( 21, new AdditionalAttr( 5, 1, "◇")); 	
		m_AdditionalAttrDB.Add ( 22, new AdditionalAttr( 5, 1, "☆")); 	
		m_AdditionalAttrDB.Add ( 23, new AdditionalAttr( 5, 1, "★")); 
	}

	

	public override SoldierAttr GetSoldierAttr( int AttrID )
	{
		if( m_SoldierAttrDB.ContainsKey( AttrID ) == false)
		{
			Debug.LogWarning("GetSoldierAttr:AttrID["+AttrID+"] not exist");
			return null;
		}

		SoldierAttr NewAttr = new SoldierAttr();

        NewAttr.SetSoldierAttr(m_SoldierAttrDB[AttrID]);

        return NewAttr;
	}


	public override SoldierAttr GetEliteSoldierAttr(ENUM_AttrDecorator emType,int AttrID, SoldierAttr theSoldierAttr)
	{

		AdditionalAttr theAdditionalAttr =  GetAdditionalAttr( AttrID );
		if( theAdditionalAttr == null)
		{
			Debug.LogWarning("GetEliteSoldierAttr:["+ AttrID + "]not exist");
			return theSoldierAttr;
		}

		BaseAttrDecorator theAttrDecorator = null;
		switch( emType)
		{
		case ENUM_AttrDecorator.Prefix:
			theAttrDecorator = new PrefixBaseAttr();
			break;
		case ENUM_AttrDecorator.Suffix:
			theAttrDecorator = new SuffixBaseAttr();
			break;
		}
		if(theAttrDecorator==null)
		{
			Debug.LogWarning("GetEliteSoldierAttr:無法針對["+emType+"]產生裝飾者");
			return theSoldierAttr;
		}

		theAttrDecorator.SetComponent( theSoldierAttr.GetBaseAttr());
		theAttrDecorator.SetAdditionalAttr( theAdditionalAttr );

		theSoldierAttr.SetBaseAttr( theAttrDecorator );
		return theSoldierAttr;
	}


	public override EnemyAttr GetEnemyAttr( int AttrID )
	{
		if( m_EnemyAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetEnemyAttr:AttrID["+AttrID+ "]not exist");
			return null;
		}
		
		EnemyAttr NewAttr = new EnemyAttr();
		NewAttr.SetEnemyAttr( m_EnemyAttrDB[AttrID]);		
		return NewAttr;
	}
	
	public override WeaponAttr GetWeaponAttr( int AttrID )
	{
		if( m_WeaponAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetWeaponAttr:AttrID["+AttrID+ "]not exist");
			return null;
		}

		return m_WeaponAttrDB[AttrID];
	}

	public override AdditionalAttr GetAdditionalAttr( int AttrID )
	{
		if( m_AdditionalAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetAdditionalAttr:AttrID["+AttrID+ "]not exist");
			return null;
		}

		return m_AdditionalAttrDB[AttrID];
	}
	
}