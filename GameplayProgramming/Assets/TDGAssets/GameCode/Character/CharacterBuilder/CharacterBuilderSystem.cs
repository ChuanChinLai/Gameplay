using UnityEngine;
using System.Collections.Generic;

public class CharacterBuilderSystem : IGameSystem
{
	private int m_GameObjectID = 0;

	public CharacterBuilderSystem(TowerDefenseGame TDGame):base(TDGame)
	{}

	public override void Initialize()
	{}

	public override void Update()
	{}


	public void Construct(ICharacterBuilder theBuilder)
	{
		theBuilder.LoadAsset( ++m_GameObjectID );
		theBuilder.AddOnClickScript();
		theBuilder.AddWeapon();
		theBuilder.SetCharacterAttr();
		theBuilder.AddAI();

		theBuilder.AddCharacterSystem( m_TDGame );
	}
}
