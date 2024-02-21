using Music.Infrastructure.HttpUtil;
using Music.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Music.Infrastructure.Extensions;
using System.Net;
using System.IO;
using System.Reflection;
using System.Collections.Specialized;

namespace Music.MusicApi
{
    /// <summary>
    /// 酷我音乐
    /// </summary>
    public class KWMusicAPI
    {
        public const string API_Classify_Playlist_GetRcmPlayList = "http://www.kuwo.cn/api/www/classify/playlist/getRcmPlayList";
        public const string API_Playlist_PlayListInfo = "http://www.kuwo.cn/api/www/playlist/playListInfo";
        public const string API_Music_MvList = "http://www.kuwo.cn/api/www/music/mvList";
        public const string API_Music_RecommendPlayList = "http://www.kuwo.cn/api/www/rcm/index/playlist";
        public const string API_Bang_MusicList = "http://www.kuwo.cn/api/www/bang/bang/musicList";
        //http://m.kuwo.cn/newh5/singles/songinfoandlrc?musicId
        public const string API_Singles_SongInfoAndLrc = "http://m.kuwo.cn/newh5/singles/songinfoandlrc";


        public static readonly object _lockObject = new object();
        static HttpUtil _httpUtil = null;
        public static HttpUtil HttpUtilInstance
        {
            get
            {
                if (_httpUtil == null)  //先判断实例是否存在，不存在再加锁处理
                {
                    lock (_lockObject)
                    {
                        Dictionary<string, string> header = new Dictionary<string, string>()
                        {
                            { "Cookie","kw_token=N25WESU3O5"},
                            { "CSRF","N25WESU3O5"}
                        };
                        _httpUtil = new HttpUtil(header);
                    }
                }
                return _httpUtil;
            }
        }


        /// <summary>
        /// /
        /// </summary>
        /// <param name="url"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public static async Task<List<SongSheetInfo>> GetSongSheetList(string order = "new", int start = 1, int size = 32)
        {
            List<SongSheetInfo> songSheetList = new List<SongSheetInfo>();
            try
            {
                string getUrl = $"{API_Classify_Playlist_GetRcmPlayList}?pn={start}&rn={size}&order={order}";
                var respondJson = await HttpUtilInstance.Get(getUrl);
                JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(respondJson))["data"]["data"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    SongSheetInfo sheetInfo = new SongSheetInfo();
                    sheetInfo.ID = jArray[i]["id"].ToString();
                    sheetInfo.Pic = jArray[i]["img"].ToString();
                    sheetInfo.Name = jArray[i]["name"].ToString();
                    sheetInfo.Uname = jArray[i]["uname"].ToString();
                    sheetInfo.Total = jArray[i]["total"].ToString();
                    sheetInfo.Listen = jArray[i]["listencnt"].ToString();
                    int num = Convert.ToInt32(sheetInfo.Total);
                    if (num > 99999)
                    {
                        sheetInfo.Total = num / 10000 + "万";
                    }
                    songSheetList.Add(sheetInfo);

                }
                return songSheetList;
            }
            catch (Exception ex)
            {
                return songSheetList;
            }
        }

        /// <summary>
        /// 专属歌单
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <returns></returns>
        public static async Task<List<SongSheetInfo>> GetRecommendPlayList(string id = "rcm", string pn = "1", string rn = "32")
        {
            List<SongSheetInfo> songSheetList = new List<SongSheetInfo>();
            try
            {
                string getUrl = $"{API_Music_RecommendPlayList}?id={id}&pn={pn}&rn={rn}&httpsStatus=1&reqId=1d2472a0-c138-11ec-9a1f-0b908b427ed1";
                var respondJson = await HttpUtilInstance.Get(getUrl);
                JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(respondJson))["data"]["list"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    SongSheetInfo sheetInfo = new SongSheetInfo();
                    sheetInfo.ID = jArray[i]["id"].ToString();
                    sheetInfo.Pic = jArray[i]["img"].ToString();
                    sheetInfo.Name = jArray[i]["name"].ToString();
                    sheetInfo.Uname = jArray[i]["uname"].ToString();
                    sheetInfo.Total = jArray[i]["total"].ToString();
                    sheetInfo.Listen = jArray[i]["listencnt"].ToString();
                    int num = Convert.ToInt32(sheetInfo.Total);
                    if (num > 99999)
                    {
                        sheetInfo.Total = num / 10000 + "万";
                    }
                    songSheetList.Add(sheetInfo);

                }
                return songSheetList;
            }
            catch (Exception ex)
            {
                return songSheetList;
            }
        }

        public static async Task<List<MusicInfo>> GetPlayListInfo(string pid, string pn, string rn, SongSheetInfo songSheetInfo)
        {
            List<MusicInfo> MusicInfoList = new List<MusicInfo>();
            try
            {
                string getUrl = $"{API_Playlist_PlayListInfo}?pid={pid}&pn={pn}&rn={rn}";
                var respondJson = await HttpUtilInstance.Get(getUrl);
				var jTokenMain = ((JObject)JsonConvert.DeserializeObject(respondJson))["data"];
				songSheetInfo.Info = jTokenMain["info"].ToString();
				songSheetInfo.Tag = jTokenMain["tag"].ToString();

				JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(respondJson))["data"]["musicList"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject2 = JObject.Parse(jArray[i].ToString());
                    string songname = jObject2["name"].ToString().Replace("/", "-").Replace("|", " ");
                    string text = jObject2["musicrid"].ToString();
                    string albumname = jObject2["album"].ToString();
                    string albumid = jObject2["albumid"].ToString();
                    string duration = Convert.ToInt64(jObject2["duration"]).ToString();
                    string singer = jObject2["artist"].ToString();
                    string singerid = jObject2["artistid"].ToString();
                    string text2 = text.Replace("MUSIC_", "");
                    string mp3url = "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid=" + text2 + "&format=mp3&response=url";//转换音频地址
                    string lrcurl = "http://m.kuwo.cn/newh5/singles/songinfoandlrc?musicId=" + text2;//歌词
                    bool flag = jObject2.ToString().Contains("albumpic");
                    bool flag2 = jObject2.ToString().Contains("pic");
                    string text3 = "";
                    text3 = ((!flag) ? ((!flag2) ? "" : jObject2["pic"].ToString()) : jObject2["albumpic"].ToString());
                    text3.Replace("120", "500").Replace("300", "500");
                    string mvid = jObject2["rid"].ToString();
                    bool hasLossless = (bool)jObject2["hasLossless"];
                    _ = (bool)jObject2["isListenFee"];
                    bool flag3 = false;
                    flag3 = (int)jObject2["hasmv"] == 1;
                    MusicInfo musicInfo = new MusicInfo();
                    musicInfo.Songid = text;
                    musicInfo.Songname = songname;
                    musicInfo.Singer = singer;
                    musicInfo.Singerid = singerid;
                    musicInfo.Albumname = albumname;
                    musicInfo.Albumid = albumid;
                    musicInfo.Duration = duration;
                    musicInfo.Mvid = mvid;
                    musicInfo.Mp3url = mp3url;
                    musicInfo.Pturl = text3;
                    musicInfo.Lrcurl = lrcurl;
                    musicInfo.HasLossless = hasLossless;
                    musicInfo.Hasmv = flag3;
                    musicInfo.Mvurl = flag3 ? "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid=" + mvid + "&format=mp4&response=url" : "";
                    musicInfo.Fee = false;
                    musicInfo.From = "酷我音乐";
                    MusicInfo musicInfo2 = musicInfo;

                    MusicInfoList.Add(musicInfo);

                }
                return MusicInfoList;
            }
            catch (Exception ex)
            {
                return MusicInfoList;
            }
        }

        public static async Task<List<MvSheetInfo>> GetMvListInfo(string pn="1", string rn="32", string pid = "236682871",string reqId = "c828b740-bb1d-11ec-b19a-0199d95b6f89")
        {
            List<MvSheetInfo> MvInfoList = new List<MvSheetInfo>();
            try
            {
                string getUrl = $"{API_Music_MvList}?pid={pid}&pn={pn}&rn={rn}&httpsStatus=1&reqId={reqId}";
                var respondJson = await HttpUtilInstance.Get(getUrl);
                var jTokenMain = ((JObject)JsonConvert.DeserializeObject(respondJson))["data"];

                JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(respondJson))["data"]["mvlist"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject2 = JObject.Parse(jArray[i].ToString());
                    string name = jObject2["name"].ToString().Replace("/", "-").Replace("|", " ");
                    string id = jObject2["id"].ToString();
                    string duration = jObject2["songTimeMinutes"].ToString();
                    string uname = jObject2["artist"].ToString();
                    string unameid = jObject2["artistid"].ToString();
                    string pic = jObject2["pic"].ToString();
                    string listen = jObject2["mvPlayCnt"].ToString();
                    string source = "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid="+ id + "&format=mp4&response=url";
                    MvSheetInfo musicInfo = new MvSheetInfo();
                    musicInfo.ID = id;
                    musicInfo.Name = name;
                    musicInfo.Uname = uname;
                    musicInfo.UnameId = unameid;
                    musicInfo.Duration = duration;
                    musicInfo.Pic = pic;
                    musicInfo.Listen = listen;
                    musicInfo.Source = source;
                    musicInfo.From = "酷我音乐";
                    MvInfoList.Add(musicInfo);

                }
                return MvInfoList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static async Task<string> GetPlayMusicSource(string url)
        {
            string result = "";
            try
            {
                string getUrl = url;
                var respondJson = await HttpUtilInstance.Get(getUrl);

                return respondJson;
            }
            catch (Exception ex)
            {
                return result;
            }
        }

        /// <summary>
        /// 排行榜
        /// </summary>
        /// <param name="bangId"></param>
        /// <param name="pn"></param>
        /// <param name="rn"></param>
        /// <param name="reqId"></param>
        /// <returns></returns>
        public static async Task<List<MusicInfo>> GetBangMusicList(string bangId, string pn, string rn,string reqId)
        {
            List<MusicInfo> MusicInfoList = new List<MusicInfo>();
            try
            {
                string getUrl = $"{API_Bang_MusicList}?bangId={bangId}&pn={pn}&rn={rn}&httpsStatus=1&reqId={reqId}";
                var respondJson = await HttpUtilInstance.Get(getUrl);
                var jTokenMain = ((JObject)JsonConvert.DeserializeObject(respondJson))["data"];

                JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(respondJson))["data"]["musicList"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject2 = JObject.Parse(jArray[i].ToString());
                    string songname = jObject2["name"].ToString().Replace("/", "-").Replace("|", " ");
                    string text = jObject2["musicrid"].ToString();
                    string albumname = jObject2["album"].ToString();
                    string albumid = jObject2["albumid"].ToString();
                    string duration = Convert.ToInt64(jObject2["duration"]).ToString();
                    string singer = jObject2["artist"].ToString();
                    string singerid = jObject2["artistid"].ToString();
                    string text2 = text.Replace("MUSIC_", "");
                    string mp3url = "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid=" + text2 + "&format=mp3&response=url";//转换音频地址
                    string lrcurl = "http://m.kuwo.cn/newh5/singles/songinfoandlrc?musicId=" + text2;//歌词
                    bool flag = jObject2.ToString().Contains("albumpic");
                    bool flag2 = jObject2.ToString().Contains("pic");
                    string text3 = "";
                    text3 = ((!flag) ? ((!flag2) ? "" : jObject2["pic"].ToString()) : jObject2["albumpic"].ToString());
                    text3.Replace("120", "500").Replace("300", "500");
                    string mvid = jObject2["rid"].ToString();
                    bool hasLossless = (bool)jObject2["hasLossless"];
                    _ = (bool)jObject2["isListenFee"];
                    bool flag3 = false;
                    flag3 = (int)jObject2["hasmv"] == 1;
                    MusicInfo musicInfo = new MusicInfo();
                    musicInfo.Songid = text;
                    musicInfo.Songname = songname;
                    musicInfo.Singer = singer;
                    musicInfo.Singerid = singerid;
                    musicInfo.Albumname = albumname;
                    musicInfo.Albumid = albumid;
                    musicInfo.Duration = duration;
                    musicInfo.Mvid = mvid;
                    musicInfo.Mp3url = mp3url;
                    musicInfo.Pturl = text3;
                    musicInfo.Lrcurl = lrcurl;
                    musicInfo.HasLossless = hasLossless;
                    musicInfo.Hasmv = flag3;
                    musicInfo.Mvurl = flag3 ? "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid=" + mvid + "&format=mp4&response=url" : "";
                    musicInfo.Fee = false;
                    musicInfo.From = "酷我音乐";

                    MusicInfoList.Add(musicInfo);

                }
                return MusicInfoList;
            }
            catch (Exception ex)
            {
                return MusicInfoList;
            }
        }

        /// <summary>
        /// 搜索单曲
        /// </summary>
        /// <param name="searchText"></param>
        public static async Task<List<MusicInfo>> SearchSongList(string searchText,int pn = 1,int count = 30)
        {
            List<MusicInfo> MusicInfoList = new List<MusicInfo>();

            try
            {
                //if (!network.IsConnectedInternet())
                //{
                //    main.message.ShowMessage("无法访问网络，请检查网络是否连接");
                //    return;
                //}
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    return MusicInfoList;
                }
                string jsonText = "";
                await Task.Run(delegate
                {
                    jsonText = SearchSongJson(searchText,pn,count);
                });
                JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(jsonText))["data"]["list"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject2 = JObject.Parse(jArray[i].ToString());
                    string songname = jObject2["name"].ToString().Replace("/", "-").Replace("|", " ");
                    string text = jObject2["musicrid"].ToString();
                    string albumname = jObject2["album"].ToString();
                    string albumid = jObject2["albumid"].ToString();
                    string duration = Convert.ToInt64(jObject2["duration"]).ToString();
                    string singer = jObject2["artist"].ToString();
                    string singerid = jObject2["artistid"].ToString();
                    string text2 = text.Replace("MUSIC_", "");
                    string mp3url = "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid=" + text2 + "&format=mp3&response=url";//转换音频地址
                    string lrcurl = "http://m.kuwo.cn/newh5/singles/songinfoandlrc?musicId=" + text2;//歌词
                    bool flag = jObject2.ToString().Contains("albumpic");
                    bool flag2 = jObject2.ToString().Contains("pic");
                    string text3 = "";
                    text3 = ((!flag) ? ((!flag2) ? "" : jObject2["pic"].ToString()) : jObject2["albumpic"].ToString());
                    text3.Replace("120", "500").Replace("300", "500");
                    string mvid = jObject2["rid"].ToString();
                    bool hasLossless = (bool)jObject2["hasLossless"];
                    _ = (bool)jObject2["isListenFee"];
                    bool flag3 = false;
                    flag3 = (int)jObject2["hasmv"] == 1;
                    MusicInfo musicInfo = new MusicInfo();
                    musicInfo.Songid = text;
                    musicInfo.Songname = songname;
                    musicInfo.Singer = singer;
                    musicInfo.Singerid = singerid;
                    musicInfo.Albumname = albumname;
                    musicInfo.Albumid = albumid;
                    musicInfo.Duration = duration;
                    musicInfo.Mvid = mvid;
                    musicInfo.Mp3url = mp3url;
                    musicInfo.Pturl = text3;
                    musicInfo.Lrcurl = lrcurl;
                    musicInfo.HasLossless = hasLossless;
                    musicInfo.Hasmv = flag3;
                    musicInfo.Mvurl = flag3 ? "http://antiserver.kuwo.cn/anti.s?type=convert_url&rid=" + mvid + "&format=mp4&response=url" : "";
                    musicInfo.Fee = false;
                    musicInfo.From = "酷我音乐";

                    MusicInfoList.Add(musicInfo);

                }
                return MusicInfoList;
            }
            catch (Exception ex)
            {
                return MusicInfoList;
            }
        }
        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            PropertyInfo property = typeof(WebHeaderCollection).GetProperty("InnerCollection", BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                (property.GetValue(header, null) as NameValueCollection)[name] = value;
            }
        }

        public static string SearchSongJson(string searchText,int pn,int count=30)
        {
            string respondJson = null;
            try
            {
                //string url = "http://www.kuwo.cn/api/www/search/searchPlayListBykeyWord?pn=1&rn=" + count + "&key=" + searchText;
                string url = "http://www.kuwo.cn/api/www/search/searchMusicBykeyWord?"+"pn="+pn+"&rn=" + count + "&key=" + searchText + "&reqId=67327430-c3a6-11ec-ab48-713eb75700c1";

                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Method = "GET";
                SetHeaderValue(webRequest.Headers, "csrf", "YHGRC8T7LZR");
                SetHeaderValue(webRequest.Headers, "Referer", "http://www.kuwo.cn/");
                SetHeaderValue(webRequest.Headers, "Cookie", "_ga=GA1.2.281119717.1609679409; Hm_lvt_cdb524f42f0ce19b169a8071123a4797=1614521252,1616906823; Hm_lpvt_cdb524f42f0ce19b169a8071123a4797=1616906823; _gid=GA1.2.1066888924.1616906823; _gat=1; kw_token=YHGRC8T7LZR");
                webRequest.Credentials = CredentialCache.DefaultCredentials;
                WebResponse response = webRequest.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream);
                respondJson = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                response.Close();
                return respondJson;
            }
            catch
            {
                return respondJson;
            }
        }

        public static async Task<List<SongWord>> GetSongWord(string musicId)
        {
            List<SongWord> songWordList = new List<SongWord>();
            try
            {
                string getUrl = $"{API_Singles_SongInfoAndLrc}?musicId={musicId}";
                var respondJson = await HttpUtilInstance.Get(getUrl);

                JArray jArray = JArray.Parse(((JObject)JsonConvert.DeserializeObject(respondJson))["data"]["lrclist"].ToString());
                for (int i = 0; i < jArray.Count; i++)
                {
                    JObject jObject2 = JObject.Parse(jArray[i].ToString());
                    string lineText = jObject2["lineLyric"].ToString();
                    string time = jObject2["time"].ToString();

                    SongWord songWord = new SongWord();
                    songWord.LineLyric = lineText;
                    songWord.Time = time;
                    songWordList.Add(songWord);

                }
                return songWordList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
