using System;
using System.Net.Http;

// Token: 0x02000172 RID: 370
[Serializable]
public static class DiscordPoster
{
	// Token: 0x06001406 RID: 5126 RVA: 0x000585AC File Offset: 0x000567AC
	public static async void sendMessage(string username, string content, byte[] file_bytes)
	{
		using (HttpClient httpClient = new HttpClient())
		{
			MainControl.log("Sending feedback!");
			string requestUri = "https://discord.com/api/webhooks/874343240336293948/22mZCnb1EriWbcue4t81WrHMEXo1m1aVC-uZ8wmAM2_WdczXVw0qD_WAwlt8s4L02Lr0";
			MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
			HttpContent content2 = new StringContent(username);
			HttpContent content3 = new StringContent(content);
			multipartFormDataContent.Add(content2, "username");
			multipartFormDataContent.Add(content3, "content");
			if (file_bytes != null)
			{
				multipartFormDataContent.Add(new ByteArrayContent(file_bytes, 0, file_bytes.Length), "Document", "Screenshot.png");
			}
			string text;
			try
			{
				HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(requestUri, multipartFormDataContent);
				HttpResponseMessage response = httpResponseMessage;
				string message = await response.Content.ReadAsStringAsync();
				text = DateTime.Now.ToString() + " message status is: " + response.StatusCode.ToString();
				MainControl.log(message);
				response = null;
			}
			catch (Exception obj)
			{
				MainControl.logError(obj);
				text = DateTime.Now.ToString() + " message failed due to connection issues!";
			}
			MainControl.log(text);
			FeedbackTool.addToFeedbackBuffer(text);
			FeedbackTool.busy = false;
			httpClient.Dispose();
		}
		HttpClient httpClient = null;
	}
}
