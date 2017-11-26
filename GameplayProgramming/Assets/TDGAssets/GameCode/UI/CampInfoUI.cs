using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class CampInfoUI : IUserInterface
{
	private ICamp m_Camp = null; 

	private Button m_LevelUpBtn = null;
	private Button m_WeaponLvUpBtn = null;
	private Button m_TrainBtn = null;
	private Button m_CancelBtn = null;
	private Text m_AliveCountTxt = null;
	private Text m_CampLvTxt = null;
	private Text m_WeaponLvTxt = null;
	private Text m_TrainCostText = null;
	private Text m_TrainTimerText= null;
	private Text m_OnTrainCountTxt = null;
	private Text m_CampNameTxt = null;
	private Image m_CampImage = null; 

	private UnitCountVisitor m_UnitCountVisitor = new UnitCountVisitor();

	public CampInfoUI( TowerDefenseGame TDGame ):base(TDGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_RootUI = UITool.FindUIGameObject( "CampInfoUI" );


		m_CampNameTxt = UITool.GetUIComponent<Text>(m_RootUI, "CampNameText");

		m_CampImage = UITool.GetUIComponent<Image>(m_RootUI, "CampIcon");

		m_AliveCountTxt = UITool.GetUIComponent<Text>(m_RootUI, "AliveCountText");				

		m_CampLvTxt = UITool.GetUIComponent<Text>(m_RootUI, "CampLevelText");

		m_WeaponLvTxt = UITool.GetUIComponent<Text>(m_RootUI, "WeaponLevelText");

		m_OnTrainCountTxt = UITool.GetUIComponent<Text>(m_RootUI, "OnTrainCountText");

		m_TrainCostText = UITool.GetUIComponent<Text>(m_RootUI, "TrainCostText");

		m_TrainTimerText = UITool.GetUIComponent<Text>(m_RootUI, "TrainTimerText");


		m_LevelUpBtn = UITool.GetUIComponent<Button>(m_RootUI, "CampLevelUpBtn");
		m_LevelUpBtn.onClick.AddListener( ()=> OnLevelUpBtnClick() );

		m_WeaponLvUpBtn = UITool.GetUIComponent<Button>(m_RootUI, "WeaponLevelUpBtn");
		m_WeaponLvUpBtn.onClick.AddListener( ()=> OnWeaponLevelUpBtnClick() );

		m_TrainBtn = UITool.GetUIComponent<Button>(m_RootUI, "TrainSoldierBtn");
		m_TrainBtn.onClick.AddListener( ()=> OnTrainBtnClick() );

		m_CancelBtn = UITool.GetUIComponent<Button>(m_RootUI, "CancelTrainBtn");
		m_CancelBtn.onClick.AddListener( ()=> OnCancelBtnClick() );

		Hide();
	}


	public void ShowInfo(ICamp Camp)
	{
		//Debug.Log("顯示兵營資訊");
		Show ();
		m_Camp = Camp;


		m_CampNameTxt.text = m_Camp.GetName();

		m_TrainCostText.text = string.Format("AP:{0}",m_Camp.GetTrainCost());

		ShowOnTrainInfo();

		IAssetFactory Factory = PBDFactory.GetAssetFactory();
		m_CampImage.sprite = Factory.LoadSprite( m_Camp.GetIconSpriteName());

		if( m_Camp.GetLevel() <= 0 )
			EnableLevelInfo(false);
		else
		{
			EnableLevelInfo(true);
			m_CampLvTxt.text = string.Format("Level:" + m_Camp.GetLevel());
			ShowWeaponLv();
		}			
	}


	private void ShowWeaponLv()
	{
		string WeaponName = "";
		switch(m_Camp.GetWeaponType())
		{
		case ENUM_Weapon.Gun:
			WeaponName = "Gun";
			break;
		case ENUM_Weapon.Rifle:
			WeaponName = "Rifle";
			break;
		case ENUM_Weapon.Rocket:
			WeaponName = "Rocket";
			break;
		default:
			WeaponName = "None";
			break;
		}
		m_WeaponLvTxt.text = string.Format("Weapon LV:{0}",WeaponName);

	}


	private void ShowOnTrainInfo()
	{
		if( m_Camp == null) return;

		int Count = m_Camp.GetOnTrainCount();

		m_OnTrainCountTxt.text = string.Format("Train Count:" + Count);

		if(Count > 0)
			m_TrainTimerText.text = string.Format("Time:{0:0.00}", m_Camp.GetTrainTimer());
		else
			m_TrainTimerText.text = "";

		m_UnitCountVisitor.Reset();
		m_TDGame.RunCharacterVisitor( m_UnitCountVisitor );
		int UnitCount = m_UnitCountVisitor.GetUnitCount( m_Camp.GetSoldierType());
		m_AliveCountTxt.text = string.Format( "Alive Count:{0}",UnitCount );
	}

	public override void Update ()
	{
		ShowOnTrainInfo();
	}


	private void EnableLevelInfo(bool Value)
	{
		m_CampLvTxt.enabled = Value;
		m_WeaponLvTxt.enabled = Value;
		m_LevelUpBtn.gameObject.SetActive(Value);
		m_WeaponLvUpBtn.gameObject.SetActive( Value);
	}
	

	private void OnLevelUpBtnClick()
	{
		int Cost = m_Camp.GetLevelUpCost();
		if( CheckRule( Cost > 0 , "Max Level")==false )
			return ;

		string Msg = string.Format("Need AP: {0}", Cost);

		if( CheckRule(  m_TDGame.CostAP(Cost), Msg ) == false)
			return ;

		m_Camp.LevelUp();
		ShowInfo( m_Camp );
	}

	private void OnWeaponLevelUpBtnClick()
	{
		int Cost = m_Camp.GetWeaponLevelUpCost();
		if( CheckRule( Cost > 0 , "Max Level") ==false )		
			return ;

		string Msg = string.Format("Need AP: {0}", Cost);
		if( CheckRule( m_TDGame.CostAP(Cost), Msg ) ==false)
			return ;
		
		m_Camp.WeaponLevelUp();
		ShowInfo( m_Camp );
	}

	private void OnTrainBtnClick()
	{
		int Cost = m_Camp.GetTrainCost();

		if( CheckRule( Cost > 0 ,"Can't training" ) == false )		
			return;

		string Msg = string.Format("Need AP: {0}", Cost);

		if( CheckRule( m_TDGame.CostAP(Cost), Msg ) ==false)
			return;

		m_Camp.Train();
		ShowInfo( m_Camp );
	}

	private void OnCancelBtnClick()
	{

		m_Camp.RemoveLastTrainCommand();
		ShowInfo( m_Camp );
	}


	private bool CheckRule( bool bValue, string NotifyMsg )
	{
		if( bValue == false)
			m_TDGame.ShowGameMsg( NotifyMsg );			
		return bValue;
	}

}
