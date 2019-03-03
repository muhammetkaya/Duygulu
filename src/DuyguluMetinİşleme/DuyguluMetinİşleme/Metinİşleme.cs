using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using SimMetricsApi;

namespace DuyguluMetinİşleme
{
    public class Metinİşleme
    {

        List<string> Kelimeler = new List<string>();
        List<int> ASCI = new List<int>();
        List<string> Eslesen = new List<string>();
        List<string> İmlaYapılmıs = new List<string>();
        List<string> StopWordListesi = new List<string>();


        public string AramaAltay(List<string> Sozluk, string Cumle, bool Imla, int Deger1, int Deger2, bool _StopWordAtma)
        {
            string GeriDonusDegeri = "";
            Eslesen.Clear();

            if (_StopWordAtma == true)
                Cumle = StopWordAtma(Cumle);

            if (!(Kelimeler.Count > 0))
                KelimeBulma(Cumle + " ");

            if (Imla == true)
            {
                for (int i = 0; i < Kelimeler.Count; i++)
                {
                    İmlaYapılmıs.Add(OfficeWordImlaDuzeltme(Kelimeler[i]));
                    if (İmlaYapılmıs[i] != "Unspelled")
                        Kelimeler[i] = İmlaYapılmıs[i];
                }
            }
            GeriDonusDegeri = ALTAY(Sozluk, Deger1, Deger2);

            return GeriDonusDegeri;
        }
        public string AramaJaroWinkler(List<string> Sozluk, string Cumle, bool Imla, double BenzerlikOranı, bool _StopWordAtma)
        {
            string GeriDonusDegeri = "";
            Eslesen.Clear();
            ASCI.Clear();


            if (_StopWordAtma == true)
                Cumle = StopWordAtma(Cumle);
            if (!(Kelimeler.Count > 0))
                KelimeBulma(Cumle + " ");


            if (Imla == true)
            {
                for (int i = 0; i < Kelimeler.Count; i++)
                {
                    İmlaYapılmıs.Add(OfficeWordImlaDuzeltme(Kelimeler[i]));
                    if (İmlaYapılmıs[i] != "Unspelled")
                        Kelimeler[i] = İmlaYapılmıs[i];
                }
            }

            GeriDonusDegeri = JAROWINKLER(Sozluk, BenzerlikOranı);

            return GeriDonusDegeri;

        }
        public string AramaNormal(List<string> Sozluk, string Cumle, bool Imla)
        {
            string GeriDonusDegeri = "";
            Eslesen.Clear();
            ASCI.Clear();

            if (!(Kelimeler.Count > 0))
                KelimeBulma(Cumle + " ");


            if (Imla == true)
            {
                for (int i = 0; i < Kelimeler.Count; i++)
                {
                    İmlaYapılmıs.Add(OfficeWordImlaDuzeltme(Kelimeler[i]));
                    if (İmlaYapılmıs[i] != "Unspelled")
                        Kelimeler[i] = İmlaYapılmıs[i];
                }
            }

            GeriDonusDegeri = Normal(Sozluk);
            return GeriDonusDegeri;

        }
        public string AramaJaroWinklerveAltay(List<string> Sozluk, string Cumle, bool Imla, int Deger1, int Deger2, double BenzerlikOranı, bool _StopWordAtma)
        {
            Cumle = Cumle.ToLower();
            Kelimeler.Clear();
            string retval = "", retval1="", retval2, retval3;
            if (_StopWordAtma == true)
                Cumle = StopWordAtma(Cumle);
            if (!(Kelimeler.Count > 0))
                KelimeBulma(Cumle + " ");
            retval1 = AramaNormal(Sozluk, Cumle, Imla);
            retval3 = AramaAltay(Sozluk, Cumle, Imla, Deger1, Deger2, false);
            retval2 = AramaJaroWinkler(Sozluk, Cumle, Imla, BenzerlikOranı, false);
            
            //AYSATUR   PEGASUS HAVAYOLLARI 2013 YILI EGE BÖLGE BIRINCISI  . Siz degerli yolcularimizin ve dostlarimizin... http://t.co/JuFSUvYUB3

            char[] splitChar = new Char[] { ' ', ',', '\n', '.', '?', '!', '\'', '’', ':', ';','#' };
            string[] kelimeler = retval1.Split(splitChar);
            string[] kelimeler2 = retval2.Split(splitChar);

            /*for (int i = 0; i < kelimeler.Length; i++)
                for (int j = 0; j < kelimeler2.Length; j++)
                {
                    if (kelimeler[i] == kelimeler2[j])
                        kelimeler[i] = "";
                }

            for (int i = 0; i < kelimeler.Length; i++)
                retval += kelimeler[i] + " ";
            for (int i = 0; i < kelimeler2.Length; i++)
                retval += kelimeler2[i] + " ";*/
            for (int i = 0; i < Kelimeler.Count; i++)
                retval += Kelimeler[i] + " ";
            

            retval += retval1 + " " + retval2 + " " + retval3 + " ";
            return retval;
        }


        private string JAROWINKLER(List<string> Sozluk, double BenzerlikOranı)
        {
            int sayi;
            string GeriDonusDegeri = "";
            SimMetricsMetricUtilities.JaroWinkler Sim = new SimMetricsMetricUtilities.JaroWinkler();
            double Benzerlik;
            for (int i = 0; i < Kelimeler.Count; i++)
            {
                List<string> temp = new List<string>();
                List<double> temp2 = new List<double>();
                for (int j = 0; j < Sozluk.Count; j++)
                {
                    Benzerlik = Sim.GetSimilarity(Sozluk[j].ToString(), Kelimeler[i].ToString());
                    if (Benzerlik > BenzerlikOranı)
                    {
                        bool[] eşleşme = new bool[Kelimeler[i].Length];
                        char[] cKelime = Kelimeler[i].ToCharArray();
                        char[] cSozluk = Sozluk[j].ToString().ToCharArray();
                        if (cKelime.Length > cSozluk.Length)
                            sayi = cSozluk.Length;
                        else
                            sayi = cKelime.Length;
                        for (int k = 0; k < sayi; k++)
                            if (cKelime[k] == cSozluk[k])
                                eşleşme[k] = true;
                        if ((Sozluk[j].Length < 4) || (Kelimeler[i].Length < 4) && (Sozluk[j].Length == Kelimeler[i].Length))
                        {
                            temp.Add(Sozluk[j]);
                            temp2.Add(Benzerlik);
                            Kelimeler[i] = "";
                        }
                        if ((Kelimeler[i].Length > 3) && (eşleşme[0] == true) && (eşleşme[1] == true) && (eşleşme[2] == true))
                        {
                            temp.Add(Sozluk[j]);
                            temp2.Add(Benzerlik);
                            Kelimeler[i] = "";
                        }
                    }
                }
                double ara;
                if ((temp.Count < 2) && (temp.Count > 0))
                    Eslesen.Add(temp[0]);
                else if (temp.Count > 0)
                {
                    ara = temp2[0];
                    for (int j = 1; j < temp2.Count; j++)
                    {
                        if (ara > temp2[j])
                            ara = temp2[j];
                    }
                    for (int j = 0; j < temp2.Count; j++)
                    {
                        if (temp2[j] == ara)
                            Eslesen.Add(temp[j]);
                    }
                }

            }
            for (int i = 0; i < Eslesen.Count; i++)
            {
                GeriDonusDegeri += Eslesen[i] + " ";
            }
            return GeriDonusDegeri;


        }
        private string ALTAY(List<string> Sozluk, int Deger1, int Deger2)
        {
            string retValue = "";
            string kelimeSozluk = "", kelimeList = "";
            int sayi = 0;
            int eşleşenSayi = 0;
            for (int i = 0; i < Kelimeler.Count; i++)
            {
                kelimeList = Convert.ToString(Kelimeler[i]);
                for (int j = 0; j < Sozluk.Count; j++)
                {
                    kelimeSozluk = Convert.ToString(Sozluk[j]);
                    char[] cKelimeSozluk = kelimeSozluk.ToCharArray();
                    char[] cKelimeList = kelimeList.ToCharArray();
                    Boolean[] eşleşme = new Boolean[kelimeList.Length];
                    if ((cKelimeList.Length <= cKelimeSozluk.Length) && (cKelimeList.Length > 0))
                    {
                        sayi = cKelimeList.Length;
                        for (int k = 0; k < sayi; k++)
                        {
                            if ((cKelimeList[k] == cKelimeSozluk[k]))
                                eşleşme[k] = true;
                            else
                                eşleşme[k] = false;
                        }
                        for (int k = 0; k < cKelimeList.Length; k++)
                        {
                            if (eşleşme[k] == true)
                                eşleşenSayi += 1;

                        } if (((cKelimeList.Length <= 3) && (cKelimeList.Length == cKelimeSozluk.Length)) && (eşleşenSayi == cKelimeList.Length))
                        {
                            Eslesen.Add(kelimeSozluk.ToString());
                            Kelimeler[i] = "";
                            break;
                        }
                        if (((cKelimeList.Length <= Deger1) || (cKelimeSozluk.Length <= Deger1)) && (eşleşenSayi == cKelimeList.Length))
                        {
                            Eslesen.Add(kelimeSozluk.ToString());
                            Kelimeler[i] = "";
                            break;
                        }
                        else if ((cKelimeList.Length > Deger1) && (eşleşenSayi >= (cKelimeList.Length / 2) + Deger2) && (cKelimeSozluk.Length > cKelimeList.Length) && (eşleşme[0] == true) && (eşleşme[1] == true) && (eşleşme[2] == true) && (eşleşme[3] == true))
                        {
                            Eslesen.Add(kelimeSozluk.ToString());
                            Kelimeler[i] = "";
                            break;
                        }
                        else if ((cKelimeList.Length > Deger1) && (eşleşenSayi >= (cKelimeList.Length / 2) + Deger2) && (eşleşme[0] == true) && (eşleşme[1] == true) && (eşleşme[2] == true) && (eşleşme[3] == true))
                        {
                            Eslesen.Add(kelimeSozluk.ToString());
                            Kelimeler[i] = "";
                            break;
                        }
                    }
                    eşleşenSayi = 0;
                }
            }
            for (int i = 0; i < Eslesen.Count; i++)
            {
                retValue += Eslesen[i] + " ";
            }
            return retValue;
        }
        private string Normal(List<string> Sozluk)
        {
            string GeriDonusDegeri = "";
            for (int i = 0; i < Kelimeler.Count; i++)
            {
                for (int j = 0; j < Sozluk.Count; j++)
                {
                    if (Kelimeler[i] == Sozluk[j])
                    {
                        Eslesen.Add(Sozluk[j]);
                        Kelimeler[i] = "";
                        break;
                    }
                }
            }
            for (int i = 0; i < Eslesen.Count; i++)
            {
                GeriDonusDegeri += Eslesen[i] + " ";
            }
            return GeriDonusDegeri;
        }


        private void KelimeBulma(string Cumle)
        {
            char[] splitChar = new Char[] { ' ', ',', '\n', '.', '?', '!', '\'', '’', ':', ';' };
            string[] kelimeler = Cumle.Split(splitChar);
            for (int i = 0; i < kelimeler.Length; i++)
            {
                Kelimeler.Add(kelimeler[i].Trim());
            }
            listDuzenleme();
            listDuzenleme();
        }
        private void StopWordEkleme()
        {
            StreamReader reader = new StreamReader("turkish_stop_words.txt", Encoding.GetEncoding(1254));
            string stopword = reader.ReadLine();
            while (stopword != null)
            {
                StopWordListesi.Add(stopword);
                stopword = reader.ReadLine();
            }
            reader.Close();
        }
        private string StopWordAtma(string Cumle)
        {
            if (Cumle != null)
            {

                string retVal = Cumle;
                Cumle = KarakterAtma(Cumle);
                StopWordEkleme();


                Boolean found = false;
                char[] splitChar = new Char[] { ' ', ',', '\n', '.', '?', '!', '\'', '’', ':', ';' };
                string[] kelimeler = Cumle.Split(splitChar);

                retVal = "";
                for (int j = 0; j < kelimeler.Length; j++)
                {
                    for (int i = 0; i < StopWordListesi.Count; i++)
                    {
                        if (StopWordListesi[i] == kelimeler[j])
                        {
                            found = true;
                            break;
                        }
                    }
                    if (found == false)
                        retVal += kelimeler[j] + " ";
                    else
                    {
                        retVal += "";
                        found = false;
                    }
                }
                return retVal;
            }
            else
                return "";

        }
        private void ASCIKarakterleriEkleme()
        {
            StreamReader reader = new StreamReader("asci.txt", Encoding.GetEncoding(1254));
            string asci = reader.ReadLine();
            while (asci != null)
            {
                ASCI.Add(Convert.ToInt32(Convert.ToChar(asci)));
                asci = reader.ReadLine();
            }
            reader.Close();
        }
        private string KarakterAtma(string Cumle)
        {
            int i;
            Boolean boolASCI = false;
            int ASCIHarf;
            int listasci;
            char[] cMetin = Cumle.ToCharArray();
            for (i = 0; i < cMetin.Length; i++)
            {
                ASCIHarf = Convert.ToInt32(cMetin[i]);
                for (int j = 0; j < ASCI.Count; j++)
                {
                    listasci = ASCI[j];
                    if (listasci == ASCIHarf)
                    {
                        boolASCI = true;
                        break;
                    }
                    else
                        boolASCI = false;
                }
                if (boolASCI == false)
                    cMetin[i] = '\0';

            }
            
            for (i = 0; i < cMetin.Length; i++)
                Cumle += cMetin[i];
            Cumle = Cumle.Replace("\0", "");
            return Cumle;
        }
        private void listDuzenleme()
        {
            string item = "";
            for (int i = 0; i < Kelimeler.Count; i++)
            {
                item = Kelimeler[i].ToString();
                if (item.Length <= 3)
                {
                    Kelimeler.Remove(Kelimeler[i]);
                }
            }
        }
        private string OfficeWordImlaDuzeltme(string Cumle)
        {

            var wordApplication = new Microsoft.Office.Interop.Word.Application() { Visible = false };
            string retVal = "";

            var myDocument = wordApplication.Documents.Open(System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\myDoc.docx");

            List<string> temp = new List<string>();
            var language = wordApplication.Languages[WdLanguageID.wdTurkish];

            // http://support.microsoft.com/kb/292108
            // http://www.delphigroups.info/2/c2/261707.html
            const string custDict = "custom.dic";

            var suggestions = wordApplication.GetSpellingSuggestions(Cumle, custDict, MainDictionary: language.Name);

            foreach (SpellingSuggestion spellingSuggestion in suggestions)
                temp.Add(spellingSuggestion.Name);
            object obj = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            myDocument.Close(ref obj);
            wordApplication.Quit(ref obj);

            if (temp.Count <= 0)
                retVal = "Unspelled";
            else
                retVal = temp[0].ToString();
            return retVal;
        }

    }
}
