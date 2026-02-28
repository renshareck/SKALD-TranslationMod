using System;
using System.Text;
using AOT;
using Steamworks;
using UnityEngine;

// Token: 0x0200016E RID: 366
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060013EA RID: 5098 RVA: 0x00057DB2 File Offset: 0x00055FB2
	protected static SteamManager Instance
	{
		get
		{
			if (SteamManager.s_instance == null)
			{
				return new GameObject("SteamManager").AddComponent<SteamManager>();
			}
			return SteamManager.s_instance;
		}
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060013EB RID: 5099 RVA: 0x00057DD6 File Offset: 0x00055FD6
	public static bool Initialized
	{
		get
		{
			return SteamManager.Instance.m_bInitialized;
		}
	}

	// Token: 0x060013EC RID: 5100 RVA: 0x00057DE2 File Offset: 0x00055FE2
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	protected static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
		Debug.LogWarning(pchDebugText);
	}

	// Token: 0x060013ED RID: 5101 RVA: 0x00057DEA File Offset: 0x00055FEA
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
		SteamManager.s_EverInitialized = false;
		SteamManager.s_instance = null;
	}

	// Token: 0x060013EE RID: 5102 RVA: 0x00057DF8 File Offset: 0x00055FF8
	protected virtual void Awake()
	{
		if (SteamManager.s_instance != null)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		SteamManager.s_instance = this;
		if (SteamManager.s_EverInitialized)
		{
			throw new Exception("Tried to Initialize the SteamAPI twice in one session!");
		}
		Object.DontDestroyOnLoad(base.gameObject);
		if (!Packsize.Test())
		{
			Debug.LogError("[Steamworks.NET] Packsize Test returned false, the wrong version of Steamworks.NET is being run in this platform.", this);
		}
		if (!DllCheck.Test())
		{
			Debug.LogError("[Steamworks.NET] DllCheck Test returned false, One or more of the Steamworks binaries seems to be the wrong version.", this);
		}
		try
		{
			if (SteamAPI.RestartAppIfNecessary((AppId_t)this.AppID))
			{
				Debug.Log("[Steamworks.NET] Shutting down because RestartAppIfNecessary returned true. Steam will restart the application.");
				Application.Quit();
				return;
			}
		}
		catch (DllNotFoundException ex)
		{
			string str = "[Steamworks.NET] Could not load [lib]steam_api.dll/so/dylib. It's likely not in the correct location. Refer to the README for more details.\n";
			DllNotFoundException ex2 = ex;
			Debug.LogError(str + ((ex2 != null) ? ex2.ToString() : null), this);
			Application.Quit();
			return;
		}
		this.m_bInitialized = SteamAPI.Init();
		if (!this.m_bInitialized)
		{
			Debug.LogError("[Steamworks.NET] SteamAPI_Init() failed. Refer to Valve's documentation or the comment above this line for more information.", this);
			return;
		}
		if (this.m_bInitialized)
		{
			Debug.Log(string.Format("[Steamworks.NET] Steam Initialized successfully with AppID: {0}", SteamUtils.GetAppID()));
		}
		SteamManager.s_EverInitialized = true;
	}

	// Token: 0x060013EF RID: 5103 RVA: 0x00057F08 File Offset: 0x00056108
	protected virtual void OnEnable()
	{
		if (SteamManager.s_instance == null)
		{
			SteamManager.s_instance = this;
		}
		if (!this.m_bInitialized)
		{
			return;
		}
		if (this.m_SteamAPIWarningMessageHook == null)
		{
			this.m_SteamAPIWarningMessageHook = new SteamAPIWarningMessageHook_t(SteamManager.SteamAPIDebugTextHook);
			SteamClient.SetWarningMessageHook(this.m_SteamAPIWarningMessageHook);
		}
	}

	// Token: 0x060013F0 RID: 5104 RVA: 0x00057F56 File Offset: 0x00056156
	protected virtual void OnDestroy()
	{
		if (SteamManager.s_instance != this)
		{
			return;
		}
		SteamManager.s_instance = null;
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.Shutdown();
	}

	// Token: 0x060013F1 RID: 5105 RVA: 0x00057F7A File Offset: 0x0005617A
	protected virtual void Update()
	{
		if (!this.m_bInitialized)
		{
			return;
		}
		SteamAPI.RunCallbacks();
	}

	// Token: 0x040004F5 RID: 1269
	[SerializeField]
	private uint AppID;

	// Token: 0x040004F6 RID: 1270
	protected static bool s_EverInitialized;

	// Token: 0x040004F7 RID: 1271
	protected static SteamManager s_instance;

	// Token: 0x040004F8 RID: 1272
	protected bool m_bInitialized;

	// Token: 0x040004F9 RID: 1273
	protected SteamAPIWarningMessageHook_t m_SteamAPIWarningMessageHook;
}
