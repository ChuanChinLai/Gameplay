using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoldierInfoUI : IUserInterface
{
	private ISoldier m_Soldier = null;

	private Image  m_Icon = null;
	private Text   m_NameTxt = null;
	private Text   m_HPTxt = null;
	private Text   m_LvTxt = null;
	private Text   m_AtkTxt = null;
	private Text   m_AtkRangeTxt = null;
	private Text   m_SpeedTxt = null;
	private Slider m_HPSlider = null;

	public SoldierInfoUI( TowerDefenseGame TDGame ):base(TDGame)
	{
		Initialize();
	}
		
	public override void Initialize()
	{
		m_RootUI = UITool.FindUIGameObject( "SoldierInfoUI" );

		m_Icon = UITool.GetUIComponent<Image>(m_RootUI, "SoldierIcon");

		m_NameTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierNameText");

		m_HPTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierHPText");

		m_LvTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierLvText");

		m_AtkTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierAtkText");

		m_AtkRangeTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierAtkRangeText");

		m_SpeedTxt = UITool.GetUIComponent<Text>(m_RootUI, "SoldierSpeedText");

		m_HPSlider = UITool.GetUIComponent<Slider>(m_RootUI, "SoldierSlider");	


		m_TDGame.RegisterGameEvent( ENUM_GameEvent.SoldierKilled, new SoldierKilledObserverUI( this ));
		m_TDGame.RegisterGameEvent( ENUM_GameEvent.SoldierUpgate, new SoldierUpgateObserverUI( this ));

		Hide();
	}

	// Hide
	public override void Hide ()
	{
		base.Hide ();
		m_Soldier = null;
	}

	public void ShowInfo(ISoldier Soldier)
	{

		m_Soldier = Soldier;

		if( m_Soldier == null || m_Soldier.IsKilled())
		{
			Hide ();
			return ;
		}
		Show ();

		// Icon
		IAssetFactory Factory = PBDFactory.GetAssetFactory();
		m_Icon.sprite = Factory.LoadSprite( m_Soldier.GetIconSpriteName());

		m_NameTxt.text =  m_Soldier.GetName();

		m_LvTxt.text =string.Format("LV:{0}", m_Soldier.GetSoldierValue().GetSoldierLv());

		m_AtkTxt.text = string.Format( "ATK:{0}",m_Soldier.GetWeapon().GetAtkValue());

		m_AtkRangeTxt.text = string.Format( "RNG:{0}",m_Soldier.GetWeapon().GetAtkRange());

		m_SpeedTxt.text = string.Format("SPD:{0}", m_Soldier.GetSoldierValue().GetMoveSpeed());;

		RefreshHPInfo();
	}

	public void RefreshSoldier( ISoldier Soldier  )
	{
		if( Soldier == null)
		{
			m_Soldier = null;
			Hide ();
		}

		if( m_Soldier != Soldier)
			return;

		ShowInfo( Soldier );
	}

	private void RefreshHPInfo()
	{
		int NowHP = m_Soldier.GetSoldierValue().GetNowHP();
		int MaxHP = m_Soldier.GetSoldierValue().GetMaxHP();

		m_HPTxt.text = string.Format("HP({0}/{1})", NowHP, MaxHP);

		m_HPSlider.maxValue = MaxHP;
		m_HPSlider.minValue = 0;
		m_HPSlider.value = NowHP;
	}


	public override void Update ()
	{
		base.Update ();		

		if(m_Soldier==null)
			return ;

		if(m_Soldier.IsKilled())
		{
			m_Soldier = null;
			Hide ();
			return ;
		}
		
		RefreshHPInfo();
	}
}

