using Discord;
using Discord.Rest;
using Discord.WebSocket;
using ProcessHacker.Native.Api;
using ProcessHacker.Native.Objects;
using ProcessHacker.Native.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silent_Scanner
{
    public delegate void SearchError(string message);

    public delegate void SearchFinished();

    public delegate void SearchProgressChanged(string progress);

    /// <summary>
    /// Defines a generic process memory searcher with status events.
    /// </summary>
    public interface ISearcher
    {
        event SearchError SearchError;

        event SearchFinished SearchFinished;

        event SearchProgressChanged SearchProgressChanged;

        Dictionary<string, object> Params { get; }
        int PID { get; }
        List<string[]> Results { get; }

        void Search();
    }

    public partial class Form1 : Form
    {
        public static bool explorersearched = false;
        public static string[] explorerstrings = GetExplorerStrings();
        public static bool javawsearched = false;
        public static string[] javawstrings = GetJavawStrings();
        public static List<string> searchstringsexplorer = new List<string>();
        public static List<string> searchstringsjavaw = new List<string>();
        public int currentplace = 0;
        public bool finishedsearch;
        private DiscordSocketClient _client;
        private int currentnumber = 0;
        private int currentnumbere = 0;
        private string currentstat = "Currently not searching";
        private string[] disks;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
<<<<<<< HEAD
=======
        public static bool explorersearched = false;
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
        private List<FileInfo> files;
        private string[,] finalresults = new string[10, 10000];
        //  List<int> listainte = new List<int>();
        //   List<IntPtr> listeeea = new List<IntPtr>();
        private string ipaddress;
<<<<<<< HEAD
=======

        public static bool javawsearched = false;
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
        private string laineee;

        // Process currentprocess;
        private string lastfile;

        private bool messagesent = false;
        private string[] minecraftnames;
        private string minecraftnamesjoin;
        private string possiblecheatsonlauncher;
        private string[] results = new string[1];
        private List<int> scanner = new List<int>();

        //  List<string> finalresultslist = new List<string>();
        private string[] searchplaces = new string[] { @"C:\Users\" + Environment.UserName + @"\Desktop", @"C:\Users\" + Environment.UserName + @"\AppData\Roaming", @"C:\Users\" + Environment.UserName + @"\Documents", @"C:\Users\" + Environment.UserName + @"\Downloads", @"C:\Users\" + Environment.UserName + @"\OneDrive", @"C:\Users\" + Environment.UserName + @"\videos", @"C:\Users\" + Environment.UserName + @"\images", @"C:\$Recycle.Bin", @"C:\Users\" + Environment.UserName + @"\recent", @"C:\Windows\Prefetch", @"C:\Program Files\AutoHotkey", null, null, null, null, null };

        // int totalexplorersearch = 0; 0 0
        //  int stringsexplorerfound: = 0; 0 2
        //    int currentexplorersearch = 0; 0 1
        //    int currentjavawsearch = 0;  1 1
        //    int totaljavawsearch = 0;  1 0
        //    int stringsjavawfound: = 0; 1 2
        //    int stringssvchostfound: = 0; 2 2
        //     int currentsvchostsearch = 0; 2 1
        //     int totalsvchost = 0; 2 0
        private int[,] stringsearch = new int[5, 5];

        private TimeSpan timing;
        private BackgroundWorker worker;
        private BackgroundWorker workerfour;
        private BackgroundWorker workersearch;
        private BackgroundWorker workersvchost;
        private BackgroundWorker workerthree;
        private BackgroundWorker workertimer;
        private BackgroundWorker workertoo;
        /* List<int> listaint = new List<int>();
         List<IntPtr> listeea = new List<IntPtr>();*/
        // List<string> SearchValues = new List<string>();
        //  List<IntPtr> PossibleAddresses = new List<IntPtr>();
        /*bool hasLockedOn;
        IntPtr processPointer;*/

        public Form1()
        {
            InitializeComponent();
            //  this.textBox3.DragDrop += new System.EventHandler(this.textBox3_TextChanged);
            // this.DragDrop += new DragEventHandler(Form1_DragDrop);
            //openFileDialog1.InitialDirectory = @"C:\";
            //  Dumpmemory("explorer");
            try
            {
                ipaddress = new WebClient().DownloadString("https://api.ipify.org");
            }
            catch { }
            List<string> list = new List<string>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.TotalSize <= 17179860387)
                {
                    list.Add(drive.Name);
                }
            }
            disks = list.ToArray();
            for (int f = 0; f < disks.Length; f++)
            {
                searchplaces[f + 11] = disks[f];
            }

            string hostName = Dns.GetHostName();
            IPHostEntry hostEntry = Dns.GetHostEntry(hostName);
            IPAddress[] addr = hostEntry.AddressList;
            var ip = addr.Where(x => x.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                         .FirstOrDefault();
            ipaddress = ip.ToString() ?? "";
            string ln;
            try
            {
                if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version"))
                {
                    label1.Text = "Silent scanner " + File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version");
                }
            }
            catch { }
            possiblecheatsonlauncher = null;
            List<string> liste = new List<string>();
            List<string> lista = new List<string>();
            List<string> possiblecheatslauncherlist = new List<string>();
            int counter = 0;
            StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\launcher_profiles.json");
<<<<<<< HEAD

=======
            string[] explorerstrings;
            try
            {
                explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));
                //  StreamWriter fileeee = new StreamWriter(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", true);
                //    for (int iwannagillmaself = 0; iwannagillmaself < explorerstrings.Length; iwannagillmaself++)
                //    {
                //        fileeee.Write(explorerstrings[iwannagillmaself]);
                //  }
                //fileeee.Close();
            }
            catch
            {
                StreamReader fileeee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                explorerstrings = fileeee.ReadToEnd().Replace("\n", "").Split('\r');
                fileeee.Close();
            }
            //Searcher.Searchboi("explorer", "");
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
            while ((ln = file.ReadLine()) != null)
            {
                ln = ln.Replace(",", "");
                ln = ln.Replace("\"", "");
                ln = ln.Replace(":", "");
                ln = ln.Replace("}", "");

                ln = ln.Replace(" ", "");
                ln = ln.Replace("{", "");

                if (ln.Contains("displayName"))
                {
                    laineee = ln;
                    laineee = laineee.Replace("displayName", "");
                    liste.Add(laineee);
                }
                for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                {
                    if (ln.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                    {
                        possiblecheatslauncherlist.Add(" Possible cheat on launcher: " + ln + " ");
<<<<<<< HEAD
=======



>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                    }
                }
                counter++;
            }

            file.Close();

         //   minecraftnames = liste.ToArray();
         //   possiblecheatsonlauncher = String.Join(", ", possiblecheatslauncherlist.ToArray());
          //  minecraftnamesjoin = String.Join("-", minecraftnames);
            StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\usercache.json");

            while ((ln = filee.ReadLine()) != null)
            {
                string[] lnsplitops = ln.Split('\"');
                for (int gaemer = 0; gaemer < lnsplitops.Length; gaemer++)
                {
                    if (lnsplitops[gaemer].Contains("name"))
                    {
                        if (!liste.Contains(lnsplitops[gaemer + 2]))
                        {
                            laineee = lnsplitops[gaemer + 2];
                            laineee = laineee.Replace("\"", "");
                            liste.Add(laineee);
                        }
                    }
                }
                counter++;
            }

            filee.Close();

            minecraftnames = liste.ToArray();
            possiblecheatsonlauncher = String.Join(", ", possiblecheatslauncherlist.ToArray());
            minecraftnamesjoin = String.Join("-", minecraftnames);

            ChangeLabelnine("Usernames: " + String.Join(", ", minecraftnames));
            ChangeLabelnine("Usernames: " + String.Join(", ", minecraftnames));
        }

        public static string[] GetExplorerStrings()
        {
            string[] explorerstrings;
            try
            {
                explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));
            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                explorerstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }
            return explorerstrings;
        }

        public static string[] GetJavawStrings()
        {
            string[] javawstrings;
            try
            {
                javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));
            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                javawstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }
            return javawstrings;
        }

        public void AddProgress(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(AddProgress), new object[] { value });
                    return;
                }

                progressBar1.Value = +Convert.ToInt32(value);
            }
            catch { }
        }

        public void AppendTextBoxtoo(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(AppendTextBoxtoo), new object[] { value });
                    return;
                }

                textBox2.AppendText(Environment.NewLine + Environment.NewLine + value);
            }
            catch { }
        }

        public void ChangeLabel(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(ChangeLabel), new object[] { value });
                    return;
                }

                label6.Text = value;
            }
            catch { }
        }

        public void ChangeLabelnine(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(ChangeLabelnine), new object[] { value });
                    return;
                }

                label9.Text = value;
            }
            catch { }
        }

        public void ChangeLabelseven(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(ChangeLabelseven), new object[] { value });
                    return;
                }

                label7.Text = value;
            }
            catch { }
        }

        public void ChangeLabelten(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(ChangeLabelten), new object[] { value });
                    return;
                }

                label10.Text = value;
            }
            catch { }
        }

        public void ChangeLabeltoo(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(ChangeLabeltoo), new object[] { value });
                    return;
                }

                label8.Text = value;
            }
            catch { }
        }

        public void ChangeLabeltwo(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(ChangeLabeltwo), new object[] { value });
                    return;
                }

                label2.Text = value;
            }
            catch { }
        }

        public void Changetimer(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(Changetimer), new object[] { value });
                    return;
                }

                label11.Text = value;
            }
            catch { }
        }

        public string fixtime(string text)
        {
            try
            {
                Convert.ToDateTime(text);
                string[] time = text.Split(':');
                for (int i = 0; time.Length > i; i++)
                {
                    if (Convert.ToInt32(time[i]) < 10)
                    {
                        time[i] = 0 + time[i];
                    }
                }
                return string.Join(":", time);
            }
            catch { }
            return text;
        }

        public List<FileInfo> GetFiles()
        {
            try
            {
                var di = new DirectoryInfo(searchplaces[currentplace]);//"C:\");

                var directories = di.GetDirectories();
                var files = new List<FileInfo>();

                foreach (var directoryInfo in directories)
                {
                    try
                    {
                        GetFilesFromDirectory(directoryInfo.FullName, files, results);
                    }
                    catch
                    {
                    }
                }
            }
            catch { }
            return files;
        }

        public void GetFilesFromDirectory(string directory, List<FileInfo> files, string[] results)
        {
            // finishedsearch = false;
<<<<<<< HEAD

=======
            string[] explorerstrings;
            try
            {
                explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));

            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                explorerstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
            var di = new DirectoryInfo(directory);
            var fs = di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);

            List<string> list = new List<string>();
            files.AddRange(fs);
            for (int i = 0; files.Count > i; i++)
            {
                for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                {
                    string ln = files[i].FullName.ToLower();
                    if (ln.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                    {
                        list.Add(files[i].FullName);
<<<<<<< HEAD
                        results = list.ToArray();
                    }
=======

                    }

                    results = list.ToArray();
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                }
            }
            var directories = di.GetDirectories();
            try
            {
                if (results[results.Length - 1] != null && lastfile != results[results.Length - 1])
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    AppendTextBoxtoo("£filesearch$" + time + " " + results[results.Length - 1]);// + @"\" + files[files.Count - 1].ToString(), 2);
                    lastfile = results[results.Length - 1];
                }
            }
            catch { }
            foreach (var directoryInfo in directories)
            {
                try
                {
                    //     writetobox(directoryInfo.FullName.ToString(), 1);

                    //        allfiles[Convert.ToInt32(files)] = fs.ToString();

                    GetFilesFromDirectory(directoryInfo.FullName, files, results);
                }
                catch
                {
                }
                files.Clear();
            }
        }

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            var token = "discord bot token";
            await _client.LoginAsync(Discord.TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += Client_Ready;
            await Task.Delay(-1);
            return;
        }

        public void Parseresults()
        {
            try
            {
                string[] results = textBox2.Text.Split('£');
                int allstuff = 0;
                for (int i = 0; results.Length > i; i++)
                {
<<<<<<< HEAD
                    if (results[i].Contains("javawstrings$"))
                    {
                        results[i] = results[i].Replace("javawstrings$", "");
                        finalresults[0, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("explorerstrings$"))
                    {
                        results[i] = results[i].Replace("explorerstrings$", "");
=======
                    if (results[i].Contains("javawstrings"))
                    {
                        results[i] = results[i].Replace("javawstrings", "");
                        finalresults[0, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("explorerstrings"))
                    {
                        results[i] = results[i].Replace("explorerstrings", "");
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                        finalresults[1, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("versionsjson$"))
                    {
                        results[i] = results[i].Replace("versionsjson$", "");
                        finalresults[2, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("filesearch$"))
                    {
                        results[i] = results[i].Replace("filesearch$", "");
                        finalresults[3, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("minecraft*.log$"))
                    {
                        results[i] = results[i].Replace("minecraft*.log$", "");
                        finalresults[4, allstuff] = results[i];
                        allstuff++;
                    }
                    else
                    {
                        finalresults[5, allstuff] = results[i];
                        allstuff++;
                    }
                    //minecraft *.log
                    //versionsjson
                    //javawstrings
                    //explorerstrings
                    //filesearch
                }
                try
                {
                    List<string> filecont = new List<string>();
                    filecont.Add("Username: " + Environment.UserName + "  Ip Address: " + ipaddress + "  Nickname(s): " + String.Join(", ", minecraftnames));
                    filecont.Add(possiblecheatsonlauncher + " Result: ");
                    for (int a = 0; 10 > a; a++)
                    {
                        for (int e = 0; allstuff > e; e++)// length of finalresult string[]
                        {
                            if (finalresults[a, e] != null)
                            {
                                filecont.Add(finalresults[a, e]);
                                //  filecont.Add(null);
                            }
                        }
                        filecont.Add(null);
                    }
                    string[] filecontent;
                    filecontent = filecont.ToArray();
                    File.WriteAllLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\result.txt", filecontent);
                   Process.Start(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\result.txt");

                }
                catch { }
            }
            catch { }
        }

        public void worker_secondthreadsearch()//object sender, DoWorkEventArgs e)
        {
            try
            {
                string ln;
                int counteer = 0;
                List<string> possiblecheatslogfile = new List<string>();
<<<<<<< HEAD

                try
                {
                    /*  try
                      {
                          explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                          File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));
                      }
                      catch
                      {
                          StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                          explorerstrings = file.ReadToEnd().Split('\r');
                          file.Close();
                      }*/
=======
              

                try
                {
                    string[] explorerstrings;
                    try
                    {
                        explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                        File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));


                    }
                    catch
                    {
                        StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                        explorerstrings = file.ReadToEnd().Split('\r');
                        file.Close();
                    }
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                    Searcher.Searchboi("explorer", "");

                    //   if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer"))
                    //      {
                    //           StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                    stringsearch[0, 0] = searchstringsexplorer.Count;//Convert.ToInt32(new FileInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Length);
                                                                     // while ((ln = filee.ReadLine()) != null)
                    string[] danked = new string[searchstringsexplorer.Count];
                    for (int eeed = 0; eeed < searchstringsexplorer.Count; eeed++)
                    {
                        danked[eeed] = searchstringsexplorer.ToArray()[eeed];
                    }
                    for (int helpme = 0; helpme < searchstringsexplorer.Count; helpme++)
                    {
                        ln = danked[helpme].ToLower();
                        for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                        {
                            if (ln.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£explorerstrings$" + time + " Possible Explorer string found: " + danked[helpme]);
<<<<<<< HEAD
=======
                       
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                            }
                        }
                        ChangeLabeltoo(stringsearch[0, 1] + @"\" + stringsearch[0, 0]);
                        stringsearch[0, 1]++;
                        // counteer++;// HERE BOI
<<<<<<< HEAD
=======
                     
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                    }
                    //   }
                }
                catch { }
            }
            catch { }
            explorersearched = true;
            AddProgress("10");
        }

        public void worker_timer(object sender, DoWorkEventArgs e)
        {
            Stopwatch counter = new Stopwatch();
            counter.Start();
            while (!messagesent)
            {
<<<<<<< HEAD
                if (!explorersearched && Searcher.searchprogress != "")
                {
=======
                if (javawsearched || explorersearched)
                { 
                    if (Searcher.searchprogress != "")
                         { 
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                    switch (currentnumber)
                    {
                       
                        case 0:
                            //      currentstat = currentstatarray[0];
                            ChangeLabeltoo(Searcher.searchprogress);
                        currentnumbere++;
                        break;

                        case 1:
                            //      currentstat = currentstatarray[0];
                            ChangeLabeltoo(Searcher.searchprogress + ".");
                        currentnumbere++;
                        break;

                        case 2:
                            //       currentstat = currentstatarray[0];
                            ChangeLabeltoo(Searcher.searchprogress + "..");
                        currentnumbere++;
                        break;

                        case 3:
                            ChangeLabeltoo(Searcher.searchprogress + "...");
                        currentnumbere = 0;
                        break;
                    }
                }
                if (Searcher.searchprogressjavaw != "")
                    switch (currentnumber)
                    {
                        case 0:
                            //      currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw);
                            currentnumbere++;
                            break;

                        case 1:
                            //      currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw + ".");
                            currentnumbere++;
                            break;

                        case 2:
                            //       currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw + "..");
                            currentnumbere++;
                            break;

                        case 3:
                            ChangeLabeltoo(Searcher.searchprogress + "...");
                            currentnumbere = 0;
                            break;
                    }
                }
                else
                {
                    ChangeLabeltoo("Explorer string search finished");
                }
                if (!javawsearched && Searcher.searchprogressjavaw != "")
                {
                    switch (currentnumber)
                    {
                        case 0:
                            //      currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw);
                            currentnumbere++;
                            break;

                        case 1:
                            //      currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw + ".");
                            currentnumbere++;
                            break;

                        case 2:
                            //       currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw + "..");
                            currentnumbere++;
                            break;

                        case 3:
                            ChangeLabel(Searcher.searchprogressjavaw + "...");
                            currentnumbere = 0;
                            break;
                    }
                }
                else
                {
                    ChangeLabel("Javaw string search finished");
                }
                if (counter.Elapsed.TotalSeconds != timing.TotalSeconds)
                {
                    timing = counter.Elapsed;
                    string timeelapsed = fixtime(timing.Minutes.ToString() + ":" + timing.Seconds.ToString());
                    Changetimer(timeelapsed);
                    Thread.Sleep(500);
                    //  string[] currentstatarray;// = currentstat.Split('.');
                    //   currentstatarray = currentstat.Split('.');
                    switch (currentnumber)
                    {
                        case 0:
                            //      currentstat = currentstatarray[0];
                            ChangeLabeltwo(currentstat);
                            currentnumber++;
                            break;

                        case 1:
                            //      currentstat = currentstatarray[0];
                            ChangeLabeltwo(currentstat + ".");
                            currentnumber++;
                            break;

                        case 2:
                            //       currentstat = currentstatarray[0];
                            ChangeLabeltwo(currentstat + "..");
                            currentnumber++;
                            break;

                        case 3:
                            //  currentstat = currentstatarray[0];
                            ChangeLabeltwo(currentstat + "...");
                            currentnumber = 0;
                            break;
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            utilities utillitiespage = new utilities();
            //    Hide();
            utillitiespage.Show();
            //     Close();
        }

        private async Task Client_Ready()
        {
            try
            {
                ulong idsv = 0;
                ulong usernaem = 0;
                ulong category = 0;
                var guild = _client.GetGuild(0);
                RestTextChannel channelel = null;
                SocketGuildUser _user = null;

                try
                {
                    _user = guild.GetUser(usernaem) as SocketGuildUser;
                }
                catch
                {
                }

                SocketGuildChannel[] xeker = guild.Channels.ToArray();

                ulong channelid = 0;
                for (int gaee = 0; guild.Channels.Count > gaee; gaee++)
                {
                    if (xeker[gaee].Name.StartsWith(minecraftnamesjoin.ToLower()))
                    {
                        channelid = guild.GetTextChannel(xeker[gaee].Id).Id;
                        break;
                    }
                }
                if (channelid == 0)
                {
                    channelel = await _user.Guild.CreateTextChannelAsync(String.Join(", ", minecraftnames), prop => prop.CategoryId = category);
                    channelid = channelel.Id;
                }
                if (("Username: " + Environment.UserName + "  Ip Address: " + ipaddress + "  Nickname(s): " + String.Join(", ", minecraftnames), possiblecheatsonlauncher + "   Result: " + textBox2.Text).ToString().Length < 1900)
                {
                    var eb = new EmbedBuilder();
                    eb.AddField("Username: " + Environment.UserName + "  Ip Address: " + ipaddress + "  Nickname(s): " + String.Join(", ", minecraftnames), possiblecheatsonlauncher + "   Result: " + textBox2.Text, false);
                    eb.WithColor(Discord.Color.Red);
                    await _client.GetGuild(idsv).GetTextChannel(channelid).SendMessageAsync("", false, eb.Build());
                }
                else
                {
                    var eb = new EmbedBuilder();
                    eb.AddField("Username: " + Environment.UserName + "  Ip Address: " + ipaddress + "  Nickname(s): " + String.Join(", ", minecraftnames), possiblecheatsonlauncher + "   Result: ", false);
                    eb.WithColor(Discord.Color.Red);

                    await _client.GetGuild(idsv).GetTextChannel(channelid).SendMessageAsync("", false, eb.Build());
                    await _client.GetGuild(idsv).GetTextChannel(channelid).SendFileAsync(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\result.txt", "");
                    File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\result.txt");
                }
            }
            catch { }

            messagesent = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void FormMain_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        /*        public void searchdatboi(string value)
                {
                    try
                    {
                        if (InvokeRequired)
                        {
                            this.Invoke(new Action<string>(searchdatboi), new object[] { value });
                            return;
                        }
                      //  StringSearcher.Searchboi(value, searchtext);
                        //  textBox2.AppendText(Environment.NewLine + Environment.NewLine + value);
                    }
                    catch { }
                }*/
        //   public static void ChangeLabeldanked(string value)
        //   {
        // Form1 formee = new Form1();
        //   formee.ChangeLabeltoo(value);
        //ChangeLabeltoo(value);
        //   }
        /*    public static void changelabeltoostatic(string value)
                {
                    Form1 formee = new Form1();
                    formee.ChangeLabeltoo(value);
                }*/

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label10_Click(object sender, EventArgs e)
        {
            scanner.Add(3);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }

        private void label6_Click(object sender, EventArgs e)
        {
            scanner.Add(2);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            scanner.Add(4);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            scanner.Add(1);
        }

        private void label9_Click(object sender, EventArgs e)
        {
            scanner.Add(0);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 Formpages = new Form2();
            //    Hide();
            Formpages.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            messagesent = false;
            Changetimer("00:00");
            currentstat = "Searching for cheats";
            textBox2.Text = "";
            stringsearch[0, 0] = 0;
            stringsearch[0, 1] = 0;
            stringsearch[0, 2] = 0;
            stringsearch[1, 0] = 0;
            stringsearch[1, 1] = 0;
            stringsearch[1, 2] = 0;
            stringsearch[2, 0] = 0;
            stringsearch[2, 1] = 0;
            stringsearch[2, 2] = 0;
            explorersearched = false;
            javawsearched = false;
            currentplace = 0;
            ChangeLabeltwo("Searching for cheats");
            workertimer = new BackgroundWorker();
            workertimer.DoWork += new DoWorkEventHandler(worker_timer);
            workertimer.RunWorkerAsync();
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoSearch);
            worker.RunWorkerAsync();
            //  workertoo = new BackgroundWorker();
            //  workertoo.DoWork += new DoWorkEventHandler(worker_secondthreadsearch);
            //  workertoo.RunWorkerAsync();
            Thread workertoo = new Thread(worker_secondthreadsearch);
            workertoo.Start();
            workerthree = new BackgroundWorker();
            workerthree.DoWork += new DoWorkEventHandler(worker_too);
            workerthree.RunWorkerAsync();
            workerfour = new BackgroundWorker();
            workerfour.DoWork += new DoWorkEventHandler(worker_three);
            workerfour.RunWorkerAsync();
            workersearch = new BackgroundWorker();
            workersearch.DoWork += new DoWorkEventHandler(worker_search);
            workersearch.RunWorkerAsync();
            workersvchost = new BackgroundWorker();
            workersvchost.DoWork += new DoWorkEventHandler(worker_five);
            workersvchost.RunWorkerAsync();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {
                WindowState = FormWindowState.Minimized;
            }
            catch { }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            Close();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            if (String.Join("", scanner.ToArray()) == "43201")
            {
            }
            scanner.Clear();
        }

        /*        public static void explorerstringsearch(string[] strings)
                {
                }*/
<<<<<<< HEAD
        private void worker_DoSearch(object sender, DoWorkEventArgs e)
        {
            string ln;
            int counteer = 0;

            /*  try
              {
                  javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                  File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));
              }
              catch
              {
                  StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                  javawstrings = file.ReadToEnd().Split('\r');
                  file.Close();
              }*/
=======

        private void worker_DoSearch(object sender, DoWorkEventArgs e)
        {


            string ln;
            int counteer = 0;
            string[] javawstrings;
            try
            {
                javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));

            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                javawstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
            try
            {
                Searcher.Searchboi("javaw", "");

                //  if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw"))
                {
                    //   StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                    stringsearch[0, 0] = Convert.ToInt32(new FileInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw").Length);
                    //     while ((ln = filee.ReadLine()) != null)
                    for (int helpme = 0; helpme < searchstringsjavaw.Count; helpme++)
                    {
                        ln = searchstringsjavaw[helpme].ToLower();
                        for (int currentstringjavaw = 0; currentstringjavaw < javawstrings.Length; currentstringjavaw++)
                        {
                            if (ln.Contains(javawstrings[currentstringjavaw].Replace("\r\n", "")))
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£javawstrings$" + time + " Possible Javaw string found: " + searchstringsjavaw[helpme]);
                            }
                            ChangeLabeltoo(stringsearch[0, 1] + @"\" + stringsearch[0, 0]);
                            stringsearch[0, 1]++;
                            counteer++;
                        }
<<<<<<< HEAD
=======
                     
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                    }
                }
            }
            catch { }
            javawsearched = true;
            AddProgress("10");
        }

        private void worker_five(object sender, DoWorkEventArgs e)
        {
<<<<<<< HEAD
            try
            {
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.json", SearchOption.TopDirectoryOnly);
                /*    for (int i = 0; filePaths.Length > i; i++)
                    {
                        try
                        {
                            string[] text = File.ReadAllLines(filePaths[i]);
                            for (int ef = 0; text.Length > ef; ef++)
                            {
                                string texttolower = text[ef].ToLower();*/

                /*  try
                  {
                      javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                      File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));
                  }
                  catch
                  {
                      StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                      javawstrings = file.ReadToEnd().Split('\r');
                      file.Close();
                  }*/
=======

            try
            {
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.json", SearchOption.AllDirectories);
                /*    for (int i = 0; filePaths.Length > i; i++)
                    {
                        try
                        {
                            string[] text = File.ReadAllLines(filePaths[i]);
                            for (int ef = 0; text.Length > ef; ef++)
                            {
                                string texttolower = text[ef].ToLower();*/
                string[] javawstrings;
                try
                {
                    javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                    File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));


                }
                catch
                {
                    StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                    javawstrings = file.ReadToEnd().Split('\r');
                    file.Close();
                }
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                for (int i = 0; filePaths.Length > i; i++)
                {
                    try
                    {
                        string[] text = File.ReadAllLines(filePaths[i]);
                        for (int ef = 0; text.Length > ef; ef++)
                        {
                            string texttolower = text[ef].ToLower();
                            for (int currentstringjavaw = 0; currentstringjavaw < javawstrings.Length; currentstringjavaw++)
                            {
                                if (texttolower.Contains(javawstrings[currentstringjavaw].Replace("\r\n", "")))
                                {
                                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                    AppendTextBoxtoo("£versionsjson$" + time + " Possible Cheat in versions " + filePaths[i] + "  " + texttolower); //versionsjson$
                                }
                            }

                        }
                    }
                    catch { }
                }
            }
            catch { }
            AddProgress("20");
        }

        private void worker_search(object sender, DoWorkEventArgs e)
        {
            try
            {
                for (currentplace = currentplace; currentplace < searchplaces.Length; currentplace++)
                {
                    files = GetFiles();
                }
            }
            catch { }

            try
            {
                while (true)
                {
                    if (stringsearch[1, 1] >= stringsearch[1, 0] && stringsearch[0, 1] >= stringsearch[0, 0] && stringsearch[2, 1] >= stringsearch[2, 0] && javawsearched && explorersearched)
                    {
                        Parseresults();

<<<<<<< HEAD
=======
                        MainAsync();
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                        currentstat = "Search finished! Check the results";
                        break;
                    }
                }
                MainAsync();
                Thread.Sleep(20000);
                AddProgress("20");
                //   messagesent = true;
            }
            catch { }
        }

        private void worker_three(object sender, DoWorkEventArgs e)
        {
            try
            {
<<<<<<< HEAD
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\versions", "*.json", SearchOption.TopDirectoryOnly);

                /* try
                 {
                     explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                     File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));
                 }
                 catch
                 {
                     StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                     explorerstrings = file.ReadToEnd().Split('\r');
                     file.Close();
                 }*/
=======
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\versions", "*.json", SearchOption.AllDirectories);
                string[] explorerstrings;
                try
                {
                    explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                    File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));


                }
                catch
                {
                    StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                    explorerstrings = file.ReadToEnd().Split('\r');
                    file.Close();
                }
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                for (int i = 0; filePaths.Length > i; i++)
                {
                    try
                    {
                        string[] text = File.ReadAllLines(filePaths[i]);
                        for (int ef = 0; text.Length > ef; ef++)
                        {
                            string texttolower = text[ef].ToLower();
                            for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                            {
                                if (texttolower.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                                {
                                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                    AppendTextBoxtoo("£versionjson$" + time + " Possible Cheat in .minecraft " + filePaths[i] + "  " + texttolower);// versionjson
                                }
                            }

                        }
                    }
                    catch { }
                }
            }
            catch { }

            AddProgress("20");

            AddProgress("10");
        }

        private void worker_too(object sender, DoWorkEventArgs e)
        {
<<<<<<< HEAD
            string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.log", SearchOption.TopDirectoryOnly);

            /*   try
               {
                   explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                   File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));
               }
               catch
               {
                   StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                   explorerstrings = file.ReadToEnd().Split('\r');
                   file.Close();
               }*/
=======
            string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.log", SearchOption.AllDirectories);
            string[] explorerstrings;
            try
            {
                explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));


            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                explorerstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
            for (int i = 0; filePaths.Length > i; i++)
            {
                string[] text = File.ReadAllLines(filePaths[i]);
                for (int ef = 0; text.Length > ef; ef++)
                {
                    string texttolower = text[ef].ToLower();
                    for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                    {
                        if (texttolower.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                        {
                            string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                            AppendTextBoxtoo("£minecraft*.log$" + time + " Possible Cheat in .minecraft " + filePaths[i] + "  " + texttolower);// minecraft*.log
                        }
                    }
<<<<<<< HEAD
=======

>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                }
            }

            AddProgress("20");

            AddProgress("10");
        }
<<<<<<< HEAD
=======

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 Formpages = new Form2();
            //    Hide();
            Formpages.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

    
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
    }

    /// <summary>
    /// A base process memory searcher. All searchers should inherit from this class.
    /// </summary>
    public class Searcher : ISearcher
    {
        public static int currentnumber = 0;
        public static ProcessAccess MinProcessReadMemoryRights = ProcessAccess.VmRead;
        public static bool searchfinished = false;
        public static string searchprogress = "";
        public static string searchprogressjavaw = "";
        private Dictionary<string, object> _params;
        private int _pid;
        private List<string[]> _results;

        /// <summary>
        /// Creates a dummy searcher which does nothing. useless
        /// </summary>
        /// <param name="PID">This parameter has no effect.</param>
        public Searcher(int PID)
        {
            _pid = PID;
            _params = new Dictionary<string, object>();
            _results = new List<string[]>();
        }

        public event SearchError SearchError;

        public event SearchFinished SearchFinished;

        public event SearchProgressChanged SearchProgressChanged;

        /// <summary>
        /// The parameters of the search.
        /// </summary>
        public Dictionary<string, object> Params
        {
            get { return _params; }
        }

        /// <summary>
        /// The PID of the process to be searched.
        /// </summary>
        public int PID
        {
            get { return _pid; }
        }

        /// <summary>
        /// A <see cref="List"/> containing the search results.
        /// </summary>
        public List<string[]> Results
        {
            get { return _results; }
            set { _results = value; }
        }

<<<<<<< HEAD
        public static void CallSearchError(string message, string processname)
        {
            try
            {
                //   if (SearchFinished != null)
                //      searchfinished = true;

                if (processname == "explorer")
                {
                    searchprogress = message;
                    Form1.explorersearched = true;
                }
                else if (processname == "javaw")
                {
                    Form1.javawsearched = true;
                    searchprogressjavaw = message;
                }
            }
            catch { }
            searchprogress = message;
            //    SearchError(message);
        }

        public static void CallSearchFinished(string processname)
        {
            /* try
             {
                 //   if (SearchFinished != null)
                 try
                 {
                     //   if (SearchFinished != null)
                     //      searchfinished = true;
                     //   SearchFinished();
                     if (processname == "explorer")
                     {
                         //     Form1.explorersearched = true;
                     }
                     else if (processname == "javaw")
                     {
                         //       Form1.javawsearched = true;
                     }
                 }
                 catch { }
                 //    searchfinished = true;
                 //   SearchFinished();
             }
             catch { }*/
        }

=======
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
        public static void CallSearchProgressChanged(string progress, int i)
        {
            try
            {
                //    searchprogress = progress;
<<<<<<< HEAD
                switch (i)
                {
                    case 0:
                        searchprogress = progress;
=======
                switch(i)
                {
                    case 0:
                            searchprogress = progress;
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                        break;

                    case 1:
                        searchprogressjavaw = progress;
                        break;

                    default:
<<<<<<< HEAD
                        searchprogress = progress;
                        break;
                }
                // searchprogress = progress;
=======
                         searchprogress = progress;
                        break;
                }
               // searchprogress = progress;
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b

                //    Form1.ChangeLabel(progress);
            }
            catch { }
        }

        /// <summary>
        /// dumps process memory string and saves it in a file with the same name
        /// </summary>
        public static void Searchboi(string processname, string searchtext)
        {
            Searcher searcher = new Searcher(0);
            if (processname == "explorer")
            {
                searcher.Searchingexplorerstrings(processname, searchtext);
            }
            else if (processname == "javaw")
            {
                searcher.Searchingjavawstrings(processname, searchtext);
            }
            //  Searching(processname);
        }

        /// <summary>
        /// does jack shit
        /// </summary>
        public virtual void Search()
        {
        }

        public void Searchingexplorerstrings(string processname, string searchtext)
        {
<<<<<<< HEAD
=======
            string[] explorerstrings;

            try
            {
                explorerstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/explorerstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));


            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                explorerstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }

>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
            //Results.Clear();

            byte[] text = Encoding.Default.GetBytes(searchtext);//(byte[])Params["text"];
            ProcessHandle phandle;
            int count = 0;
            int totalstrings = 0;
            string explorercurrentstringsearch = "";
            int minsize = 4;//4;//
            bool unicode = true;// true;//
            bool opt_priv = true; //true;//
            bool opt_img = false;//true;//
            bool opt_map = true;//true;//

            try
            {
                phandle = new ProcessHandle(Process.GetProcessesByName(processname)[0].Id,
                    ProcessAccess.QueryInformation |
                    MinProcessReadMemoryRights);
            }
            catch
            {
                CallSearchError("Could not open " + processname, processname);
                return;
            }

            phandle.EnumMemory((info) =>
            {
                // skip unreadable areas
                if (info.Protect == MemoryProtection.AccessDenied)
                    return true;
                if (info.State != MemoryState.Commit)
                    return true;

                if ((!opt_priv) && (info.Type == MemoryType.Private))
                    return true;

                if ((!opt_img) && (info.Type == MemoryType.Image))
                    return true;

                if ((!opt_map) && (info.Type == MemoryType.Mapped))
                    return true;

                byte[] data = new byte[info.RegionSize.ToInt32()];
                int bytesRead = 0;
                totalstrings += info.RegionSize.ToInt32();
                CallSearchProgressChanged(
                       //Form1 form = new Form1();
                       String.Format("Searching 0x{0} ({1} / " + info.RegionSize.ToInt32() + ")", info.BaseAddress.ToString("x"), count), 0);

                try
                {
                    bytesRead = phandle.ReadMemory(info.BaseAddress, data, data.Length);

                    if (bytesRead == 0)
                        return true;
                }
                catch
                {
                    return true;
                }

                StringBuilder curstr = new StringBuilder();
                bool isUnicode = false;
                byte byte2 = 0;
                byte byte1 = 0;
                //  if ((processname == "explorer" && count < 500000) || (processname == "javaw" && count < 200000))//500000 352 13~ minutes // 100000 13 minutes, 380
                //    {
                for (int i = 0; i < bytesRead; i++)
                {
                    bool isChar = IsChar(data[i]);

                    if (unicode && isChar && isUnicode && byte1 > 0)
                    {
                        isUnicode = false;

                        if (curstr.Length > 0)
                            curstr.Remove(curstr.Length - 1, 1);

                        curstr.Append((char)data[i]);
                    }
                    else if (isChar)
                    {
                        curstr.Append((char)data[i]);
                    }
                    else if (unicode && data[i] == 0 && IsChar(byte1) && !IsChar(byte2))
                    {
                        // skip null byte
                        isUnicode = true;
                    }
                    else if (unicode &&
                        data[i] == 0 && IsChar(byte1) && IsChar(byte2) && curstr.Length < minsize)
                    {
                        // ... [char] [char] *[null]* ([char] [null] [char] [null]) ...
                        //                   ^ we are here
                        isUnicode = true;
                        curstr = new StringBuilder();
                        curstr.Append((char)byte1);
                    }
                    else
                    {
                        if (curstr.Length >= minsize)
                        {
                            int length = curstr.Length;

                            if (isUnicode)
                                length *= 2;

                            //      Results.Add(new string[] { Utils.FormatAddress(info.BaseAddress),
                            //String.Format("0x{0:x}", i - length), length.ToString(),
                            //       curstr.ToString() });
                            explorercurrentstringsearch = curstr.ToString().ToLower();
<<<<<<< HEAD
                            for (int currentstringexplorer = 0; currentstringexplorer < Form1.explorerstrings.Length; currentstringexplorer++)
                            {
                                if (explorercurrentstringsearch.Contains(Form1.explorerstrings[currentstringexplorer].Replace("\r\n", "")))
=======
                            for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                            {
                                if (explorercurrentstringsearch.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
                                {
                                    Form1.searchstringsexplorer.Add(curstr.ToString());
                                }
                            }
         
                            

                            count++;
                        }

                        isUnicode = false;
                        curstr = new StringBuilder();
                    }

                    byte2 = byte1;
                    byte1 = data[i];
                }
                //   }

                data = null;

                return true;
            });
            /*  for (int aaa = 0; aaa < filecontent.Count(); aaa++)
              {
                  file.WriteLine(filecontent[aaa]);
              }
              filecontent.Clear();*/
            // file.Close();
            phandle.Dispose();

            CallSearchFinished(processname);
        }
<<<<<<< HEAD
        public void Searchingjavawstrings(string processname, string searchtext)
        {
            /*     string[] javawstrings;
                 try
                 {
                     javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                     File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));
                 }
                 catch
                 {
                     StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                     javawstrings = file.ReadToEnd().Split('\r');
                     file.Close();
                 }*/
            //  StreamWriter file = new StreamWriter(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\" + processname);
            //Results.Clear();
            //file.WriteLine();
            byte[] text = Encoding.Default.GetBytes(searchtext);//(byte[])Params["text"];
            ProcessHandle phandle;
            int count = 0;
            int totalstrings = 0;
            string javawcurrentstringsearch = "";
            int minsize = 4;//4;//
            bool unicode = true;// true;//
            bool opt_priv = true; //true;//
            bool opt_img = false;//true;//
            bool opt_map = true;//true;//

            try
            {
                phandle = new ProcessHandle(Process.GetProcessesByName(processname)[0].Id,
                    ProcessAccess.QueryInformation |
                    MinProcessReadMemoryRights);
            }
            catch
            {
                CallSearchError("Could not open " + processname, processname);
                return;
=======

        public static void CallSearchError(string message, string processname)
        {
                
            try
            {
                //   if (SearchFinished != null)
                //      searchfinished = true;

                if (processname == "explorer")
                {
                    searchprogress = message;
                    Form1.explorersearched = true;
                }
                else if(processname == "javaw")
                {
                    Form1.javawsearched = true;
                    searchprogressjavaw = message;
                }
            }
            catch { }
            searchprogress = message;
            //    SearchError(message);
        }

        public static void CallSearchFinished(string processname)
        {
            try
            {
                //   if (SearchFinished != null)
                try
                {
                    //   if (SearchFinished != null)
                    //      searchfinished = true;
                    //   SearchFinished();
                    if (processname == "explorer")
                    {
                        Form1.explorersearched = true;
                    }
                    else if (processname == "javaw")
                    {
                        Form1.javawsearched = true;
                    }
                }
                catch { }
                searchfinished = true;
                //   SearchFinished();
>>>>>>> dcc406a4d0103099007d847496b1a46575b3cd8b
            }

            phandle.EnumMemory((info) =>
            {
                // skip unreadable areas
                if (info.Protect == MemoryProtection.AccessDenied)
                    return true;
                if (info.State != MemoryState.Commit)
                    return true;

                if ((!opt_priv) && (info.Type == MemoryType.Private))
                    return true;

                if ((!opt_img) && (info.Type == MemoryType.Image))
                    return true;

                if ((!opt_map) && (info.Type == MemoryType.Mapped))
                    return true;

                byte[] data = new byte[info.RegionSize.ToInt32()];
                int bytesRead = 0;
                totalstrings += info.RegionSize.ToInt32();
                CallSearchProgressChanged(
                       //Form1 form = new Form1();
                       String.Format("Searching 0x{0} ({1} / " + info.RegionSize.ToInt32() + ")", info.BaseAddress.ToString("x"), count), 1);

                try
                {
                    bytesRead = phandle.ReadMemory(info.BaseAddress, data, data.Length);

                    if (bytesRead == 0)
                        return true;
                }
                catch
                {
                    return true;
                }

                StringBuilder curstr = new StringBuilder();
                bool isUnicode = false;
                byte byte2 = 0;
                byte byte1 = 0;
                //    if ((processname == "explorer" && count < 500000) || (processname == "javaw" && count < 200000))//500000 352 13~ minutes // 100000 13 minutes, 380
                //   {
                for (int i = 0; i < bytesRead; i++)
                {
                    bool isChar = IsChar(data[i]);

                    if (unicode && isChar && isUnicode && byte1 > 0)
                    {
                        isUnicode = false;

                        if (curstr.Length > 0)
                            curstr.Remove(curstr.Length - 1, 1);

                        curstr.Append((char)data[i]);
                    }
                    else if (isChar)
                    {
                        curstr.Append((char)data[i]);
                    }
                    else if (unicode && data[i] == 0 && IsChar(byte1) && !IsChar(byte2))
                    {
                        // skip null byte
                        isUnicode = true;
                    }
                    else if (unicode &&
                        data[i] == 0 && IsChar(byte1) && IsChar(byte2) && curstr.Length < minsize)
                    {
                        // ... [char] [char] *[null]* ([char] [null] [char] [null]) ...
                        //                   ^ we are here
                        isUnicode = true;
                        curstr = new StringBuilder();
                        curstr.Append((char)byte1);
                    }
                    else
                    {
                        if (curstr.Length >= minsize)
                        {
                            int length = curstr.Length;

                            if (isUnicode)
                                length *= 2;

                            //      Results.Add(new string[] { Utils.FormatAddress(info.BaseAddress),
                            //String.Format("0x{0:x}", i - length), length.ToString(),
                            //       curstr.ToString() });
                            //    javawcurrentstringsearch = curstr.ToString().ToLower();

                            javawcurrentstringsearch = curstr.ToString().ToLower();
                            for (int currentstringjavaw = 0; currentstringjavaw < Form1.javawstrings.Length; currentstringjavaw++)
                            {
                                if (javawcurrentstringsearch.Contains(Form1.javawstrings[currentstringjavaw].Replace("\r\n", "")))
                                {
                                    Form1.searchstringsjavaw.Add(curstr.ToString());
                                }
                            }

                            {
                                //file.WriteLine(curstr.ToString());
                                Form1.searchstringsjavaw.Add(curstr.ToString().ToLower());
                            }

                            count++;
                        }

                        isUnicode = false;
                        curstr = new StringBuilder();
                    }

                    byte2 = byte1;
                    byte1 = data[i];
                }
                // }

                data = null;

                return true;
            });
            /*  for (int aaa = 0; aaa < filecontent.Count(); aaa++)
              {
                  file.WriteLine(filecontent[aaa]);
              }
              filecontent.Clear();*/
            //file.Close();
            phandle.Dispose();

            CallSearchFinished(processname);
        }

        private bool IsChar(byte b)
        {
            return (b >= 32 && b <= 126) || b == 10 || b == 13 || b == 9;
        }

        public void Searchingjavawstrings(string processname, string searchtext)
        {
            string[] javawstrings;
            try
            {
                javawstrings = new WebClient().DownloadString("https://settled-jurisdictio.000webhostapp.com/v289754p7n2898f234nm8077048gh9h9oythjwohwerhogrowhtwop/240c24t783v2098d21794d210987498132y4d8923yd872310821/javawstrings").Replace("\r", "").Split('\n');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));

            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                javawstrings = file.ReadToEnd().Split('\r');
                file.Close();
            }
            //  StreamWriter file = new StreamWriter(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\" + processname);
            //Results.Clear();
            //file.WriteLine();
            byte[] text = Encoding.Default.GetBytes(searchtext);//(byte[])Params["text"];
            ProcessHandle phandle;
            int count = 0;
            int totalstrings = 0;
            string javawcurrentstringsearch = "";
            int minsize = 4;//4;//
            bool unicode = true;// true;//
            bool opt_priv = true; //true;//
            bool opt_img = false;//true;//
            bool opt_map = true;//true;//

            try
            {
                phandle = new ProcessHandle(Process.GetProcessesByName(processname)[0].Id,
                    ProcessAccess.QueryInformation |
                    MinProcessReadMemoryRights);
            }
            catch
            {
                CallSearchError("Could not open " + processname, processname);
                return;
            }

            phandle.EnumMemory((info) =>
            {
                // skip unreadable areas
                if (info.Protect == MemoryProtection.AccessDenied)
                    return true;
                if (info.State != MemoryState.Commit)
                    return true;

                if ((!opt_priv) && (info.Type == MemoryType.Private))
                    return true;

                if ((!opt_img) && (info.Type == MemoryType.Image))
                    return true;

                if ((!opt_map) && (info.Type == MemoryType.Mapped))
                    return true;

                byte[] data = new byte[info.RegionSize.ToInt32()];
                int bytesRead = 0;
                totalstrings += info.RegionSize.ToInt32();
                CallSearchProgressChanged(
                       //Form1 form = new Form1();
                       String.Format("Searching 0x{0} ({1} / " + info.RegionSize.ToInt32() + ")", info.BaseAddress.ToString("x"), count), 1);

                try
                {
                    bytesRead = phandle.ReadMemory(info.BaseAddress, data, data.Length);

                    if (bytesRead == 0)
                        return true;
                }
                catch
                {
                    return true;
                }

                StringBuilder curstr = new StringBuilder();
                bool isUnicode = false;
                byte byte2 = 0;
                byte byte1 = 0;
                //    if ((processname == "explorer" && count < 500000) || (processname == "javaw" && count < 200000))//500000 352 13~ minutes // 100000 13 minutes, 380
                //   {
                for (int i = 0; i < bytesRead; i++)
                {
                    bool isChar = IsChar(data[i]);

                    if (unicode && isChar && isUnicode && byte1 > 0)
                    {
                        isUnicode = false;

                        if (curstr.Length > 0)
                            curstr.Remove(curstr.Length - 1, 1);

                        curstr.Append((char)data[i]);
                    }
                    else if (isChar)
                    {
                        curstr.Append((char)data[i]);
                    }
                    else if (unicode && data[i] == 0 && IsChar(byte1) && !IsChar(byte2))
                    {
                        // skip null byte
                        isUnicode = true;
                    }
                    else if (unicode &&
                        data[i] == 0 && IsChar(byte1) && IsChar(byte2) && curstr.Length < minsize)
                    {
                        // ... [char] [char] *[null]* ([char] [null] [char] [null]) ...
                        //                   ^ we are here
                        isUnicode = true;
                        curstr = new StringBuilder();
                        curstr.Append((char)byte1);
                    }
                    else
                    {
                        if (curstr.Length >= minsize)
                        {
                            int length = curstr.Length;

                            if (isUnicode)
                                length *= 2;

                            //      Results.Add(new string[] { Utils.FormatAddress(info.BaseAddress),
                            //String.Format("0x{0:x}", i - length), length.ToString(),
                            //       curstr.ToString() });
                            //    javawcurrentstringsearch = curstr.ToString().ToLower();

                            javawcurrentstringsearch = curstr.ToString().ToLower();
                            for (int currentstringjavaw = 0; currentstringjavaw < javawstrings.Length; currentstringjavaw++)
                            {
                                if (javawcurrentstringsearch.Contains(javawstrings[currentstringjavaw].Replace("\r\n", "")))
                                {
                                    Form1.searchstringsjavaw.Add(curstr.ToString());
                                }
                            }
                           
                            {
                                //file.WriteLine(curstr.ToString());
                                Form1.searchstringsjavaw.Add(curstr.ToString().ToLower());
                            }

                            count++;
                        }

                        isUnicode = false;
                        curstr = new StringBuilder();
                    }

                    byte2 = byte1;
                    byte1 = data[i];
                }
                // }

                data = null;

                return true;
            });
            /*  for (int aaa = 0; aaa < filecontent.Count(); aaa++)
              {
                  file.WriteLine(filecontent[aaa]);
              }
              filecontent.Clear();*/
            //file.Close();
            phandle.Dispose();

            CallSearchFinished(processname);
        }
    }
}

/*   string strTempFile = Path.GetTempFileName();
            File.WriteAllBytes(strTempFile, Properties.Resources.ProcessHacker);
            string newplace = strTempFile.Split('.')[0] + ".exe";
            File.Move(strTempFile, newplace);
`            System.Diagnostics.Process.Start(newplace);*/
