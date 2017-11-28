using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class GameStateInfoUI : IUserInterface
{
	private Text m_MsgText = null;
	private Text m_APText = null;
	private Text m_NowStageLvText = null;
	private Text m_SoldierCountText = null;
	private Text m_EnemyCountText = null;
	
	private GameObject m_GameOverObj = null;
	private Slider m_ApSlider = null;
	private List<Image> m_HeartImages = null; 
	
	private const float MESSAGE_TIMER = 2f;
	private float m_MsgTimer = 0;

	private UnitCountVisitor m_UnitCountVisitor = new UnitCountVisitor();

	//  
	public GameStateInfoUI( TowerDefenseGame TDGame ):base(TDGame)
	{
		Initialize();
	}

	//
	public override void Initialize()
	{
		m_RootUI = UITool.FindUIGameObject( "GameStateInfo" );


		m_MsgText = UITool.GetUIComponent<Text>(m_RootUI,"NotifyText");
		m_MsgText.text = "";

		m_NowStageLvText = UITool.GetUIComponent<Text>(m_RootUI,"NowStageLvText");
		ShowNowStageLv(1);

		m_APText = UITool.GetUIComponent<Text>(m_RootUI,"APText");
		m_ApSlider = UITool.GetUIComponent<Slider>(m_RootUI,"APSlider");
		m_ApSlider.maxValue = APSystem.MAX_AP;
		m_ApSlider.minValue = 0;
		ShowAP( APSystem.MAX_AP );

		m_SoldierCountText = UITool.GetUIComponent<Text>(m_RootUI,"SoldierCountText");;
		m_EnemyCountText = UITool.GetUIComponent<Text>(m_RootUI,"EnemyCountText");;

		// Heart
		m_HeartImages = new List<Image>();
		for(int i=1;i<=StageSystem.MAX_HEART;++i)
		{
			string name = string.Format("Heart{0}",i);
			m_HeartImages.Add ( UITool.GetUIComponent<Image>(m_RootUI,name));
		}
		ShowHeart( StageSystem.MAX_HEART );


		Button btn  = UITool.GetUIComponent<Button>(m_RootUI, "ContinueBtn");
		btn.onClick.AddListener( ()=> OnContinueBtnClick() );

		m_GameOverObj = UnityTool.FindChildGameObject( m_RootUI,"GameOver");
		m_GameOverObj.SetActive(false);

		btn  = UITool.GetUIComponent<Button>(m_RootUI, "PauseBtn");
		btn.onClick.AddListener( ()=> OnPauseBtnClick() );
	}

	public override void Release ()
	{
		base.Release ();
		m_HeartImages = null;
		Time.timeScale = 1;
	}

	//
	public override void Update ()
	{
		base.Update ();

		m_UnitCountVisitor.Reset();
		m_TDGame.RunCharacterVisitor(m_UnitCountVisitor);


		m_SoldierCountText.text = string.Format("SoldierCount:{0}", m_UnitCountVisitor.GetUnitCount( ENUM_Soldier.Null ));
		m_EnemyCountText.text = string.Format("EnemyCount:{0}", m_UnitCountVisitor.GetUnitCount( ENUM_Enemy.Null ));

		if(m_MsgTimer <=0)
			return;

		m_MsgTimer -= Time.deltaTime;

		if(m_MsgTimer > 0)
			return;

		m_MsgTimer = 0;
		m_MsgText.text = "";
	}

	
	public void ShowAP( int Value)
	{
		m_ApSlider.value = Value;
		m_APText.text = Value.ToString();
	}

	
	public void ShowHeart(int Value)
	{
		int i=0;

		for(;i < Value; ++i)
			m_HeartImages[i].enabled = true;

		for(; i<StageSystem.MAX_HEART; ++i)
			m_HeartImages[i].enabled = false;

		if( Value <= 0 )
			ShowGameOver();
	}


	public void ShowNowStageLv( int Lv)
	{
		m_NowStageLvText.text = string.Format("Stage:{0}",Lv);
	}


	private void ShowGameOver()
	{
		m_GameOverObj.SetActive(true);
		Time.timeScale = 0;
	}

	// Continue
	private void OnContinueBtnClick()
	{
		Time.timeScale = 1;
		m_TDGame.ChangeToMainMenu();
	}

	// Pause
	private void OnPauseBtnClick()
	{
		m_TDGame.GamePause();
	}

	// 
	public void ShowMsg(string Msg)
	{
		if( m_MsgTimer > 0)
			m_MsgText.text = m_MsgText.text + ","+ Msg;
		else 
			m_MsgText.text = Msg;
		m_MsgTimer = MESSAGE_TIMER;
	}
}
