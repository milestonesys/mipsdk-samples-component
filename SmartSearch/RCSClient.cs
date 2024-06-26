using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VideoOS.Platform;
using VideoOS.Platform.Login;
using VideoOS.Platform.SDK.Proxy.RecorderServices;

namespace SmartSearch
{
	public class RCSClient
	{
		RecorderCommandService rcs = new RecorderCommandService();

		public RCSClient()
		{
		}

		public Guid StartSearch(Item item, DateTime beginTime, DateTime endTime, int sensitivity, TimeSpan duration, String maskString, int maskHeight, int maskWidth)
		{
			Item recorderItem = item.GetParent();
			String recorderAddress = recorderItem.FQID.ServerId.Uri.ToString();

			String serverUri = String.Format("{0}RecorderCommandService/RecorderCommandService.asmx", recorderAddress);
			rcs.Url = serverUri;

			LoginSettings ls = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite.ServerId.Id);
			TimeDuration timeDuration = new TimeDuration() {MicroSeconds = Convert.ToInt64(duration.TotalMilliseconds*1000)};
			Size size = new Size() {Height = maskHeight, Width = maskWidth};
			ImageMask imageMask = new ImageMask() {Mask = maskString, Size = size};
			return rcs.SmartSearchStart(ls.Token, item.FQID.ObjectId, beginTime, endTime, sensitivity, timeDuration, imageMask, true, new Size(){Width = 320,Height = 200});
		}

		public SmartSearchStatusType GetStatus(Guid searchId)
		{
			LoginSettings ls = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite.ServerId.Id);
			SmartSearchStatus status = rcs.SmartSearchGetStatus(ls.Token, searchId);
			return status.Status;
		}

		public SearchResult GetSearchResult(Guid searchId, bool continueSearch)
		{
			LoginSettings ls = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite.ServerId.Id);
			SmartSearchResult result = rcs.SmartSearchGetResult(ls.Token, searchId, continueSearch);

			return new SearchResult()
			{
				Time = result.ImageTime,
				Resolution = result.MotionAreas.Resolution,
                MotionAreas = result.MotionAreas.Areas
			};
		}

		public void CancelSearch(Guid searchId)
		{
			LoginSettings ls = LoginSettingsCache.GetLoginSettings(EnvironmentManager.Instance.MasterSite.ServerId.Id);
			rcs.SmartSearchCancel(ls.Token, searchId);			
		}
	}

	public class SearchResult
	{
		public DateTime Time { get; set; }
        public Size Resolution { get; set; }
		public MotionAreaInfo[] MotionAreas { get; set; }

	    public override string ToString()
	    {
	        return Time.ToUniversalTime().ToLongTimeString();
	    }
	}
}
