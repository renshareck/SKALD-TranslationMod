using System;

namespace Gog
{
	// Token: 0x0200019C RID: 412
	public static class Listener
	{
		// Token: 0x06001573 RID: 5491 RVA: 0x00060782 File Offset: 0x0005E982
		public static void Create<T>(ref T listener) where T : new()
		{
			if (listener == null)
			{
				listener = Activator.CreateInstance<T>();
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0006079C File Offset: 0x0005E99C
		public static void Dispose<T>(ref T listener) where T : IDisposable
		{
			if (listener != null)
			{
				listener.Dispose();
			}
			listener = default(T);
		}
	}
}
