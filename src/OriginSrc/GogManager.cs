using System;
using Galaxy.Api;
using Gog;
using UnityEngine;

// Token: 0x0200016D RID: 365
public class GogManager : MonoBehaviour
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000578EF File Offset: 0x00055AEF
	public GalaxyID MyGalaxyID
	{
		get
		{
			return GogManager.myGalaxyID;
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060013D5 RID: 5077 RVA: 0x000578F6 File Offset: 0x00055AF6
	public bool GalaxyFullyInitialized
	{
		get
		{
			return this.galaxyFullyInitialized;
		}
	}

	// Token: 0x060013D6 RID: 5078 RVA: 0x000578FE File Offset: 0x00055AFE
	private void Awake()
	{
		Object.Destroy(base.gameObject);
	}

	// Token: 0x060013D7 RID: 5079 RVA: 0x0005790B File Offset: 0x00055B0B
	private void OnEnable()
	{
		this.Init();
		this.ListenersInit();
		this.SignInGalaxy();
	}

	// Token: 0x060013D8 RID: 5080 RVA: 0x0005791F File Offset: 0x00055B1F
	private void Update()
	{
		GalaxyInstance.ProcessData();
	}

	// Token: 0x060013D9 RID: 5081 RVA: 0x00057926 File Offset: 0x00055B26
	private void OnDisable()
	{
		this.ShutdownAllFeatureClasses();
		this.ListenersDispose();
	}

	// Token: 0x060013DA RID: 5082 RVA: 0x00057934 File Offset: 0x00055B34
	private void OnApplicationQuit()
	{
		GalaxyInstance.Shutdown(true);
		GogManager.Instance = null;
		Object.Destroy(this);
	}

	// Token: 0x060013DB RID: 5083 RVA: 0x00057948 File Offset: 0x00055B48
	private void ListenersInit()
	{
		Listener.Create<GogManager.AuthenticationListener>(ref this.authListener);
		Listener.Create<GogManager.GogServicesConnectionStateListener>(ref this.gogServicesConnectionStateListener);
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x00057960 File Offset: 0x00055B60
	private void ListenersDispose()
	{
		Listener.Dispose<GogManager.AuthenticationListener>(ref this.authListener);
		Listener.Dispose<GogManager.GogServicesConnectionStateListener>(ref this.gogServicesConnectionStateListener);
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x00057978 File Offset: 0x00055B78
	public void StartStatsAndAchievements()
	{
		if (this.StatsAndAchievements == null)
		{
			this.StatsAndAchievements = base.gameObject.AddComponent<StatsAndAchievements>();
		}
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x00057999 File Offset: 0x00055B99
	public void ShutdownStatsAndAchievements()
	{
		if (this.StatsAndAchievements != null)
		{
			Object.Destroy(this.StatsAndAchievements);
		}
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x000579B4 File Offset: 0x00055BB4
	public void ShutdownAllFeatureClasses()
	{
		this.ShutdownStatsAndAchievements();
	}

	// Token: 0x060013E0 RID: 5088 RVA: 0x000579BC File Offset: 0x00055BBC
	private void Init()
	{
		InitParams initParams = new InitParams(this.clientID, this.clientSecret);
		Debug.Log("Initializing GalaxyPeer instance...");
		try
		{
			GalaxyInstance.Init(initParams);
			this.galaxyFullyInitialized = true;
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "Init failed for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
			this.galaxyFullyInitialized = false;
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x00057A2C File Offset: 0x00055C2C
	public void SignInGalaxy()
	{
		Debug.Log("Signing user in using Galaxy client...");
		try
		{
			GalaxyInstance.User().SignInGalaxy();
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "SignInGalaxy failed for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x00057A80 File Offset: 0x00055C80
	public void SignInCredentials(string username, string password)
	{
		Debug.Log("Signing user in using credentials...");
		try
		{
			GalaxyInstance.User().SignInCredentials(username, password);
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "SignInCredentials failed for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
		}
	}

	// Token: 0x060013E3 RID: 5091 RVA: 0x00057AD4 File Offset: 0x00055CD4
	public void SignOut()
	{
		Debug.Log("Singing user out...");
		try
		{
			GalaxyInstance.User().SignOut();
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "SignOut failed for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
		}
	}

	// Token: 0x060013E4 RID: 5092 RVA: 0x00057B28 File Offset: 0x00055D28
	public bool IsSignedIn(bool silent = false)
	{
		bool result = false;
		if (!silent)
		{
			Debug.Log("Checking SignedIn status...");
		}
		try
		{
			result = GalaxyInstance.User().SignedIn();
			if (!silent)
			{
				Debug.Log("User SignedIn: " + result.ToString());
			}
		}
		catch (GalaxyInstance.Error error)
		{
			if (!silent)
			{
				string str = "Could not check user signed in status for reason ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
			}
		}
		return result;
	}

	// Token: 0x060013E5 RID: 5093 RVA: 0x00057BA0 File Offset: 0x00055DA0
	public bool IsLoggedOn(bool silent = false)
	{
		bool result = false;
		if (!silent)
		{
			Debug.Log("Checking LoggedOn status...");
		}
		try
		{
			result = GalaxyInstance.User().IsLoggedOn();
			if (!silent)
			{
				Debug.Log("User logged on: " + result.ToString());
			}
		}
		catch (GalaxyInstance.Error error)
		{
			if (!silent)
			{
				string str = "Could not check user logged on status for reason ";
				GalaxyInstance.Error error2 = error;
				Debug.LogWarning(str + ((error2 != null) ? error2.ToString() : null));
			}
		}
		return result;
	}

	// Token: 0x060013E6 RID: 5094 RVA: 0x00057C18 File Offset: 0x00055E18
	public bool IsDlcInstalled(ulong productID)
	{
		bool result = false;
		Debug.Log("Checking is DLC " + productID.ToString() + " installed");
		try
		{
			result = GalaxyInstance.Apps().IsDlcInstalled(productID);
			Debug.Log("DLC " + productID.ToString() + " installed " + result.ToString());
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "Could not check is DLC ";
			string str2 = productID.ToString();
			string str3 = " installed for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.LogWarning(str + str2 + str3 + ((error2 != null) ? error2.ToString() : null));
		}
		return result;
	}

	// Token: 0x060013E7 RID: 5095 RVA: 0x00057CB0 File Offset: 0x00055EB0
	public string GetCurrentGameLanguage()
	{
		string text = null;
		Debug.Log("Checking current game language");
		try
		{
			text = GalaxyInstance.Apps().GetCurrentGameLanguage();
			Debug.Log("Current game language is " + text);
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "Could not check current game language for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.Log(str + ((error2 != null) ? error2.ToString() : null));
		}
		return text;
	}

	// Token: 0x060013E8 RID: 5096 RVA: 0x00057D18 File Offset: 0x00055F18
	public void ShowOverlayWithWebPage(string url)
	{
		Debug.Log("Opening overlay with web page " + url);
		try
		{
			GalaxyInstance.Utils().ShowOverlayWithWebPage(url);
			Debug.Log("Opened overlay with web page " + url);
		}
		catch (GalaxyInstance.Error error)
		{
			string str = "Could not open overlay with web page ";
			string str2 = " for reason ";
			GalaxyInstance.Error error2 = error;
			Debug.Log(str + url + str2 + ((error2 != null) ? error2.ToString() : null));
		}
	}

	// Token: 0x040004EC RID: 1260
	private readonly string clientID = "54264136932118121";

	// Token: 0x040004ED RID: 1261
	private readonly string clientSecret = "24fb3f926a60b42999a9402da6f0a108cac2aebce6b0bc328f25072f77ff8874";

	// Token: 0x040004EE RID: 1262
	public static GogManager Instance;

	// Token: 0x040004EF RID: 1263
	public StatsAndAchievements StatsAndAchievements;

	// Token: 0x040004F0 RID: 1264
	private static GalaxyID myGalaxyID;

	// Token: 0x040004F1 RID: 1265
	private bool galaxyFullyInitialized;

	// Token: 0x040004F2 RID: 1266
	public string[] achievementsList = new string[0];

	// Token: 0x040004F3 RID: 1267
	public GogManager.AuthenticationListener authListener;

	// Token: 0x040004F4 RID: 1268
	public GogManager.GogServicesConnectionStateListener gogServicesConnectionStateListener;

	// Token: 0x020002AF RID: 687
	public class AuthenticationListener : GlobalAuthListener
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x000752F0 File Offset: 0x000734F0
		public override void OnAuthSuccess()
		{
			GogManager.myGalaxyID = GalaxyInstance.User().GetGalaxyID();
			string str = "Successfully signed in as user: ";
			GalaxyID myGalaxyID = GogManager.myGalaxyID;
			Debug.Log(str + ((myGalaxyID != null) ? myGalaxyID.ToString() : null));
			GogManager.Instance.StartStatsAndAchievements();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0007532B File Offset: 0x0007352B
		public override void OnAuthFailure(IAuthListener.FailureReason failureReason)
		{
			Debug.LogWarning("Failed to sign in for reason " + failureReason.ToString());
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x00075349 File Offset: 0x00073549
		public override void OnAuthLost()
		{
			Debug.LogWarning("Authorization lost");
		}
	}

	// Token: 0x020002B0 RID: 688
	public class GogServicesConnectionStateListener : GlobalGogServicesConnectionStateListener
	{
		// Token: 0x06001B26 RID: 6950 RVA: 0x0007535D File Offset: 0x0007355D
		public override void OnConnectionStateChange(GogServicesConnectionState connected)
		{
			Debug.Log("Connection state to GOG services changed to " + connected.ToString());
		}
	}
}
