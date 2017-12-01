using UnityEngine;
using System.Collections;

public static class PBDFactory
{
	private static bool   		 m_bLoadFromResource = true;
	private static ICharacterFactory m_CharacterFactory = null;
	private static IAssetFactory 	 m_AssetFactory = null;
	private static IWeaponFactory    m_WeaponFactory = null;
	private static IAttrFactory      m_AttrFactory = null;
	
	// 取得將Unity Asset實作化的工廠
	public static IAssetFactory GetAssetFactory()
	{
		if( m_AssetFactory == null)
		{
			if( m_bLoadFromResource)
				//m_AssetFactory = new ResourceAssetFactory();
				m_AssetFactory = new ResourceAssetProxyFactory(); 
			else
				m_AssetFactory = new RemoteAssetFactory();
		}
		return m_AssetFactory;
	}

	// 遊戲角色工廠
	public static ICharacterFactory GetCharacterFactory()
	{
		if( m_CharacterFactory == null)		
			m_CharacterFactory = new CharacterFactory();
		return m_CharacterFactory;
	}

	// 武器工廠
	public static IWeaponFactory GetWeaponFactory()
	{
		if( m_WeaponFactory == null)		
			m_WeaponFactory = new WeaponFactory();
		return m_WeaponFactory;
	}

	// 數值工廠
	public static IAttrFactory GetAttrFactory()
	{
		if( m_AttrFactory == null)		
			m_AttrFactory = new AttrFactory();
		return m_AttrFactory;
	}	
}
