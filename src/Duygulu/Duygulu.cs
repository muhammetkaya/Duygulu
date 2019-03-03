using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using LinqToTwitter;
using System.ComponentModel;
using DuyguluMetinİşleme;

namespace Duygulu
{
    public class Duygulu
    {
        string ConsumerKey = "Fqa5DjXkUlB1HY7VWzoNjA";
        string ConsumerSecret = "o113z7LQIoaiEpvbZXXRtPWxsDmTnKFK9WZZejfA";
        string AccessToken = "330333367-lUcRAbLeAMNyJWFfkJcylcByt2HTgHEiIby6ANvM";
        string AccessTokenSecret = "sqXweAdL0oAGW76o9cL5PXE06RorwzZUc6WBCNUcLqU6z";
        twitterdbEntities1 contex = new twitterdbEntities1();
        MySqlDataAdapter mySQLadapter;
        Metinİşleme MetinIsleme = new Metinİşleme();

        private DataTable Oku(string Query)
        {
            MySqlConnection connention = new MySqlConnection("server=localhost;userid=root;database=twitterdb");
            connention.Open();

            DataTable dt = new DataTable();
            MySqlDataAdapter mySQLadapter = new MySqlDataAdapter(Query, connention);
            mySQLadapter.Fill(dt);
            connention.Close();
            return dt;
        }
        public DataTable TabloyaCevir<T>(IList<T> data) // list nesnesini datatable a çevirme fonksiyonu
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        public string ConvertEncoding(string metin)
        {

            byte[] bytes = Encoding.Default.GetBytes(metin);
            metin = Encoding.Default.GetString(bytes);
            return metin;
        }
        public DataTable TweetAra(string AramaMetni, DateTime SonTatih)
        {

            DataTable Tablo = new DataTable();
            bool isRetweet = false;
            string url = "";
            for (int i = 0; i < 15; i++)
                Tablo.Columns.Add();
            var auth = new SingleUserAuthorizer
            {
                Credentials = new SingleUserInMemoryCredentials
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    TwitterAccessToken = AccessToken,
                    TwitterAccessTokenSecret = AccessTokenSecret
                }
            };
            TwitterContext twitterContex = new TwitterContext(auth);

            var srch = (from search in twitterContex.Search    // Kriterler................
                        where search.Type == SearchType.Search &&
                              search.Until == SonTatih &&
                              search.Query == AramaMetni &&
                              search.SearchLanguage == "tr" &&
                              search.Count == 100
                        select search).SingleOrDefault();

            foreach (var Tweet in srch.Statuses)
            {
                if (Tweet.Entities.MediaEntities.Count > 0)
                    url = Tweet.Entities.MediaEntities[0].MediaUrl;
                else
                    url = "";
                if (Tweet.Text.Substring(0, 4) == "RT @")
                    isRetweet = true;
                else
                    isRetweet = false;
                Tablo.Rows.Add(
                    Tweet.StatusID,
                    Tweet.CreatedAt,
                    Tweet.Coordinates.Latitude.ToString().Replace(',', '.') + "," + Tweet.Coordinates.Longitude.ToString().Replace(',', '.'),
                    Tweet.Text,
                    Tweet.User.Identifier.UserID,
                    Tweet.User.Name,
                    Tweet.User.Identifier.ScreenName,
                    Tweet.User.Description,
                    Tweet.User.FollowersCount,
                    Tweet.User.FriendsCount,
                    isRetweet,
                    Tweet.RetweetCount,
                    Tweet.FavoriteCount,
                    url);
            }
            return Tablo;
        }
        public DataTable TwitterdanKaydet(string AramaMetni, DateTime SonTarih, int n, int DuyguluKullaiciID)//******************
        {
            DataTable dtKullanici = new DataTable();
            sbyte yetki = sbyte.Parse(Oku("SELECT `Authorize` FROM `appuser` WHERE `Id`="+DuyguluKullaiciID.ToString()).Rows[0][0].ToString());

            List<string> dictionary = new List<string>();
            DataTable dt = new DataTable();
            dt = Oku("SELECT * FROM wordtable");

            for (int i = 0; i < dt.Rows.Count; i++)
                dictionary.Add(dt.Rows[i][1].ToString());

            DataTable dtsonuc = new DataTable();
            dtsonuc.Columns.Add("Kullanıcı");
            dtsonuc.Columns.Add("Tweet");
            DataTable table = new DataTable();
            string url = "";
            bool isRetweet = false;
            string UserIds = "", MaxId = "0";


            var auth = new SingleUserAuthorizer
            {
                Credentials = new SingleUserInMemoryCredentials
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    TwitterAccessToken = AccessToken,
                    TwitterAccessTokenSecret = AccessTokenSecret
                }
            };
            TwitterContext twitterContex = new TwitterContext(auth);
            searchtable satir = new searchtable
            {
                SearchText = AramaMetni,
                SearchDate = DateTime.Now,
                Authorize = yetki,
                UserId = DuyguluKullaiciID,
            };
            contex.searchtable.Add(satir);
            contex.SaveChanges();
            long SearchId;
            for (int i = 0; i < n; i++)
            {
                SearchId = contex.searchtable.Local[0].SearchId;
                var srch = (from search in twitterContex.Search
                            where search.Type == SearchType.Search &&
                                  search.MaxID == ulong.Parse(MaxId) &&
                                   search.Until == SonTarih &&
                                   search.SearchLanguage == "tr" &&
                                  search.Query == AramaMetni &&
                                  search.Count == 100
                            select search).SingleOrDefault();
                // Gelen tweetlerin analizden geçip dönen sonucun kullanıcı ve arama tablolarına kaydı
                DataTable dtAnaliz = new DataTable();
                dtAnaliz = Analiz(TabloyaCevir(srch.Statuses), DuyguluKullaiciID);
                int iyi = 0, kotu = 0;
                for (int j = 0; j < dtAnaliz.Rows.Count; j++)
                {
                    if (int.Parse(dtAnaliz.Rows[j]["Fark"].ToString()) > 0)
                        iyi++;
                    if (int.Parse(dtAnaliz.Rows[j]["Fark"].ToString()) < 0)
                        kotu++;
                }

                searchtable serarch = (from u in contex.searchtable where u.SearchId == SearchId select u).SingleOrDefault();
                serarch.Positiv = iyi;
                serarch.Negativ = kotu;
                appuser user = (from u in contex.appuser where u.Id == DuyguluKullaiciID select u).SingleOrDefault();
                user.Positiv = iyi + user.Positiv;
                user.Negativ = kotu + user.Negativ;

                contex.SaveChanges();


                foreach (var Tweet in srch.Statuses)
                {
                    if (Tweet.Entities.MediaEntities.Count > 0)
                        url = Tweet.Entities.MediaEntities[0].MediaUrl;
                    else
                        url = "";
                    if (Tweet.Text.Substring(0, 4) == "RT @")
                        isRetweet = true;
                    else
                        isRetweet = false;
                    var sorgu = from alanlar in contex.tweettable
                                where alanlar.TweetID == Tweet.StatusID
                                select alanlar.Text;
                    table = TabloyaCevir(sorgu.ToList());

                    if (table.Rows.Count <= 0) // AYNI KİŞİNİN AYNI TWEETİNİ  FİLTRELEME
                    {
                        decimal UserId = long.Parse(Tweet.User.Identifier.UserID);
                        var sorgu2 = from alanlar in contex.usertable
                                     where alanlar.UserId == UserId
                                     select alanlar.UserName;
                        table = TabloyaCevir(sorgu2.ToList());
                        if (table.Rows.Count <= 0 && UserIds.IndexOf(Tweet.User.Identifier.UserID) == -1)   // AYNI KİŞİYİ FİLTRELEME
                        {
                            UserIds = UserIds + Tweet.User.Identifier.UserID + " ";
                            usertable satir3 = new usertable
                            {
                                UserId = long.Parse(Tweet.User.Identifier.UserID),
                                UserName = Tweet.User.Name,
                                UserScreenName = Tweet.User.Identifier.ScreenName,
                                USerProfileText = Tweet.User.Description,
                                UserFollowerCount = Tweet.User.FollowersCount,
                                UserFollowingCount = Tweet.User.FriendsCount,
                            };
                            contex.usertable.Add(satir3);
                        }
                        tweettable satir2 = new tweettable
                        {
                            TweetID = Tweet.StatusID,
                            SearchId = SearchId,
                            CreatedDate = Tweet.CreatedAt,
                            GeoCode = Tweet.Coordinates.Latitude.ToString().Replace(',', '.') + "," + Tweet.Coordinates.Longitude.ToString().Replace(',', '.'),
                            Text = ConvertEncoding(Tweet.Text),
                            //Text2 = processing.main(dictionary, Tweet.Text.Replace("'", " ").Replace("\"", " ")),
                            UserId = long.Parse(Tweet.User.Identifier.UserID),
                            HasPhoto = url,
                            IsReweet = isRetweet,
                            ReTweetCount = Tweet.RetweetCount,
                            FavLike = Tweet.FavoriteCount,
                            
                        };
                        contex.tweettable.Add(satir2);
                    }
                    MaxId = Tweet.StatusID;

                    //Sonuc tablosu doldur
                    dtsonuc.Rows.Add(Tweet.User.Name,Tweet.Text);
                }
                contex.SaveChanges();
            }
            UserIds = "";
            return dtsonuc;

        }

        public DataTable TTGetir(int WoeId)
        {
            DataTable Table = new DataTable();
            Table.Columns.Add();
            var auth = new SingleUserAuthorizer
            {
                Credentials = new SingleUserInMemoryCredentials
                {
                    ConsumerKey = ConsumerKey,
                    ConsumerSecret = ConsumerSecret,
                    TwitterAccessToken = AccessToken,
                    TwitterAccessTokenSecret = AccessTokenSecret
                }
            };
            TwitterContext twitterContex = new TwitterContext(auth);

            var trends = (from trnd in twitterContex.Trends
                          where trnd.Type == TrendType.Place &&
                                trnd.WoeID == WoeId // something other than 1
                          select trnd).ToList();
            Table.Columns[0].ColumnName = trends.First().Locations.First().Name;
            trends.ForEach(trnd => Table.Rows.Add(trnd.Name));
            return Table;
        }
       
        public DataTable VeriTabanindaAra(string AramaMetni, int DuyguluKullaniciID, bool OzelAramaSonuclari)//************************************************
        {
            string Query = "";

            if (OzelAramaSonuclari == false)
                Query = "SELECT tweettable.*, usertable.* FROM  usertable  INNER JOIN (searchtable INNER JOIN tweettable ON searchtable.SearchId = tweettable.SearchId) ON usertable.UserId = tweettable.UserId Where searchtable.Authorize=2 AND MATCH(`Text`) AGAINST('" + AramaMetni.Replace("'", " ") + "'IN BOOLEAN MODE) ";
            if (OzelAramaSonuclari == true)
                Query = "SELECT tweettable.*, usertable.* FROM appuser INNER JOIN (searchtable INNER JOIN tweettable ON searchtable.SearchId = tweettable.SearchId) ON appuser.Id = searchtable.UserId INNER JOIN usertable ON usertable.UserId = tweettable.UserId WHERE  (((appuser.Id)=" + DuyguluKullaniciID.ToString() + ")) AND  MATCH(`Text`) AGAINST('" + AramaMetni.Replace("'", " ") + "'IN BOOLEAN MODE) ";
            return Oku(Query);
        }
        public DataTable Analiz(DataTable table, int DuyguluKullaniciID)
        {
            int i = 0;
            bool StopWord;
            string Query = "";
            DataTable dtUser = new DataTable();
            dtUser = Oku("SELECT * FROM appuser WHERE Id=" + DuyguluKullaniciID.ToString());

            List<string> dictionary = new List<string>();
            DataTable dt = new DataTable();
            dt = Oku("SELECT * FROM wordtable WHERE UserId=0 OR UserId=" + DuyguluKullaniciID.ToString());
            for (i = 0; i < dt.Rows.Count; i++)
                dictionary.Add(dt.Rows[i][1].ToString());


            table.Columns.Add("İyi");
            table.Columns.Add("Kötü");
            table.Columns.Add("Fark");
            bool spelling;
            int p = 0, n = 0;
            for (i = 0; i < table.Rows.Count; i++)
            {
                if (dtUser.Rows[0]["Preprocessing"].ToString() == "1")
                {
                    if (dtUser.Rows[0]["Spelling"].ToString() == "0")
                        spelling = false;
                    else
                        spelling = true;

                    if (dtUser.Rows[0]["StopWord"].ToString() == "0")
                        StopWord = false;
                    else
                        StopWord = true;
                    Query = "SELECT * FROM wordtable WHERE (UserId=0 OR UserId=" + DuyguluKullaniciID.ToString() + ") AND MATCH(`Word`) AGAINST ('" + MetinIsleme.AramaJaroWinklerveAltay(dictionary, table.Rows[i]["Text"].ToString().Replace("'", " ").Replace("\"", " ").Replace("#","").ToLower(), spelling, int.Parse(dtUser.Rows[0]["Stemming1"].ToString()), int.Parse(dtUser.Rows[0]["Stemming2"].ToString()), double.Parse(dtUser.Rows[0]["Similarity"].ToString()) / 100, StopWord) + "')";
  
                    // Query = "SELECT * FROM wordtable WHERE (UserId=0 OR UserId=" + DuyguluKullaniciID.ToString() + ") AND MATCH(`Word`) AGAINST ('" + processing.StopWordAtarakv3(dictionary, table.Rows[i]["Text"].ToString().Replace("'", " ").Replace("\"", " "), spelling, int.Parse(dtUser.Rows[0]["Stemming1"].ToString()), int.Parse(dtUser.Rows[0]["Stemming2"].ToString()), 0.9) + "')";
                }
                else
                    Query = "SELECT * FROM wordtable WHERE (UserId=0 OR UserId=" + DuyguluKullaniciID.ToString() + ") AND MATCH(`Word`) AGAINST ('" + table.Rows[i]["Text"].ToString().Replace("'", " ").Replace("\"", " ") + "')";
                DataTable table2 = new DataTable();
                table2 = Oku(Query);
                for (int ii = 0; ii < table2.Rows.Count; ii++)
                {
                    p += int.Parse(table2.Rows[ii][2].ToString());
                    n += int.Parse(table2.Rows[ii][3].ToString());
                }

                table.Rows[i]["İyi"] = p.ToString();
                table.Rows[i]["Kötü"] = n.ToString();
                table.Rows[i]["Fark"] = (p - n).ToString();
                p = 0;
                n = 0;

            }

            return table;
        }
        public string Hazirla(string metin, int DuyguluKullaniciID)
        {
            int i = 0;
            bool StopWord=false;
            DataTable dtUser = new DataTable();
            dtUser = Oku("SELECT * FROM appuser WHERE Id=" + DuyguluKullaniciID.ToString());

            List<string> dictionary = new List<string>();
            DataTable dt = new DataTable();
            dt = Oku("SELECT * FROM wordtable WHERE UserId=0 OR UserId=" + DuyguluKullaniciID.ToString());
            for (i = 0; i < dt.Rows.Count; i++)
                dictionary.Add(dt.Rows[i][1].ToString());

            bool spelling=false;
            if (dtUser.Rows[0]["Preprocessing"].ToString() == "1")
            {
                if (dtUser.Rows[0]["Spelling"].ToString() == "0")
                    spelling = false;
                else
                    spelling = true;

                if (dtUser.Rows[0]["StopWord"].ToString() == "0")
                    StopWord = false;
                else
                    StopWord = true;
            }
            metin = MetinIsleme.AramaJaroWinklerveAltay(dictionary, metin.Replace("'", " ").Replace("\"", " ").ToLower(), spelling, int.Parse(dtUser.Rows[0]["Stemming1"].ToString()), int.Parse(dtUser.Rows[0]["Stemming2"].ToString()), double.Parse(dtUser.Rows[0]["Similarity"].ToString())/100, StopWord);
            return metin.Replace("#"," ");
        }

        public DataTable TekAnaliz(string metin, int DuyguluKullaniciID)
        {
            DataTable dtUser = new DataTable();
            DataTable table2 = new DataTable();
            dtUser = Oku("SELECT * FROM appuser WHERE Id=" + DuyguluKullaniciID.ToString());



            if (dtUser.Rows[0]["Preprocessing"].ToString() == "1")
                table2 = Oku("SELECT * FROM wordtable WHERE (UserId=0 OR UserId=" + DuyguluKullaniciID.ToString() + ") AND MATCH(`Word`) AGAINST ('" + Hazirla(metin, DuyguluKullaniciID) + "')");
            else
                table2 = Oku("SELECT * FROM wordtable WHERE (UserId=0 OR UserId=" + DuyguluKullaniciID.ToString() + ") AND MATCH(`Word`) AGAINST ('" + metin+ "')");
            return table2;
        }
    }
}
