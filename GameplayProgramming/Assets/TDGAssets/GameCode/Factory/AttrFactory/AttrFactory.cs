using UnityEngine;
using System.Collections.Generic;

public class AttrFactory : IAttrFactory
{
	private Dictionary<int,BaseAttr>  		m_SoldierAttrDB = null;
	private Dictionary<int,EnemyBaseAttr> 	m_EnemyAttrDB 	= null;
	private Dictionary<int,WeaponAttr> 		m_WeaponAttrDB 	= null;
	private Dictionary<int,AdditionalAttr>  m_AdditionalAttrDB=null;
	
	public AttrFactory()
	{
		InitSoldierAttr();
		InitEnemyAttr();
		InitWeaponAttr();
		InitAdditionalAttr();
	}

	// 建立所有Soldier的數值
	private void InitSoldierAttr()
	{
		m_SoldierAttrDB = new Dictionary<int,BaseAttr>();

		m_SoldierAttrDB.Add (  1, new CharacterBaseAttr(10, 3.0f, "Rookie")); 
		m_SoldierAttrDB.Add (  2, new CharacterBaseAttr(20, 3.2f, "Sergeant")); 
		m_SoldierAttrDB.Add (  3, new CharacterBaseAttr(30, 3.4f, "Captain")); 
	}

	private void InitEnemyAttr()
	{
		m_EnemyAttrDB 	= new Dictionary<int,EnemyBaseAttr>();

		m_EnemyAttrDB.Add (  1, new EnemyBaseAttr(5, 3.0f, "Elf", 10) );
		m_EnemyAttrDB.Add (  2, new EnemyBaseAttr(15,3.1f, "Troll", 20) ); 
		m_EnemyAttrDB.Add (  3, new EnemyBaseAttr(20,3.3f, "Ogre", 40) );
	}

	private void InitWeaponAttr()
	{
		m_WeaponAttrDB 	= new Dictionary<int,WeaponAttr>();

		m_WeaponAttrDB.Add ( 1, new WeaponAttr( 2, 4, "Gun") );
		m_WeaponAttrDB.Add ( 2, new WeaponAttr( 4, 7, "Rifle") );
		m_WeaponAttrDB.Add ( 3, new WeaponAttr( 8, 10, "Rocket") );
	}


	private void InitAdditionalAttr()
	{
		m_AdditionalAttrDB = new Dictionary<int,AdditionalAttr>();

		m_AdditionalAttrDB.Add ( 11, new AdditionalAttr( 3, 0, "Super")); 
		m_AdditionalAttrDB.Add ( 12, new AdditionalAttr( 5, 0, "Hyper")); 
		m_AdditionalAttrDB.Add ( 13, new AdditionalAttr(10, 0, "Ultra")); 
		
		m_AdditionalAttrDB.Add ( 21, new AdditionalAttr( 5, 1, "◇")); 	
		m_AdditionalAttrDB.Add ( 22, new AdditionalAttr( 5, 1, "☆")); 	
		m_AdditionalAttrDB.Add ( 23, new AdditionalAttr( 5, 1, "★")); 
	}

	
	public override SoldierAttr GetSoldierAttr( int AttrID )
	{
		if( m_SoldierAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetSoldierAttr:AttrID["+AttrID+"]數值不存在");
			return null;
		}


		SoldierAttr NewAttr = new SoldierAttr();
        NewAttr.SetSoldierAttr(m_SoldierAttrDB[AttrID]);
        return NewAttr;
	}


	public override SoldierAttr GetEliteSoldierAttr(ENUM_AttrDecorator emType, int AttrID, SoldierAttr theSoldierAttr)
	{
		AdditionalAttr theAdditionalAttr =  GetAdditionalAttr( AttrID );
		if( theAdditionalAttr == null)
		{
			Debug.LogWarning("GetEliteSoldierAttr:加乘數值["+AttrID+"]不存在");
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

	// 取得Enemy的數值,傳入外部參數CritRate
	public override EnemyAttr GetEnemyAttr( int AttrID )
	{
		if( m_EnemyAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetEnemyAttr:AttrID["+AttrID+"]數值不存在");
			return null;
		}
		
		// 產生數物件並設定共用的數值資料
		EnemyAttr NewAttr = new EnemyAttr();
		NewAttr.SetEnemyAttr( m_EnemyAttrDB[AttrID]);		
		return NewAttr;
	}
	
	// 取得武器的數值
	public override WeaponAttr GetWeaponAttr( int AttrID )
	{
		if( m_WeaponAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetWeaponAttr:AttrID["+AttrID+"]數值不存在");
			return null;
		}
		// 直接回傳共用的武器數值
		return m_WeaponAttrDB[AttrID];
	}

	// 取得加乘用的數值
	public override AdditionalAttr GetAdditionalAttr( int AttrID )
	{
		if( m_AdditionalAttrDB.ContainsKey( AttrID )==false)
		{
			Debug.LogWarning("GetAdditionalAttr:AttrID["+AttrID+"]數值不存在");
			return null;
		}

		// 直接回傳加乘用的數值
		return m_AdditionalAttrDB[AttrID];
	}
	
}