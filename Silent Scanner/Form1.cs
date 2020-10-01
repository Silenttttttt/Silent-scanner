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
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

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



        public static string version = "1.4.0";






        public static bool abortallthreads = false;
        [DllImport("kernel32.dll")]
        static extern uint GetLastError();
        static Stopwatch gamerstopwatch = Stopwatch.StartNew();
        public static string[] alldrives = GetAllDrives();
        public static string[] disks = GetDrives();
        public static bool explorersearched = false;
        public static bool istimerrunning = false;
        public static string[] explorerstrings = GetExplorerStrings();
        public static string[] javawstrings = GetJavawStrings();
        public static bool javawsearched = false;
        public string[] resultsfg;
        public static bool javawstringsearcherror = false;
        public static bool explorerstringsearcherror = false;
        public static bool javawfinishedstringsearch = false;
        public static bool explorerfinishedstringsearch = false;
        public static int allactions = 0;
        public static HashSet<string> searchstringsexplorer = new HashSet<string>(StringComparer.OrdinalIgnoreCase);// List<string> searchstringsexplorer = new List<string>();
        public static List<string> searchstringsexplorerlist = new List<string>();
        public static HashSet<string> searchstringsjavaw = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static List<string> searchstringsjavawlist = new List<string>();
        public int currentplace = 0;
        public bool finishedsearch;
        public static string[] allstringsjavaw;
        // private DiscordSocketClient _client;
        public static List<string> stringscontainedjavaw = new List<string>();
        public static bool containsstringsfinishedjavaw = false;
        public static HashSet<string> hashset = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        private int currentnumbere = 0;
        private string currentstat = "Currently not searching";
        private Point dragCursorPoint;
        private Point dragFormPoint;
        private bool dragging = false;
        private List<FileInfo> files;
        private string[,] finalresults = new string[10, 10000];
        public static string[] allstringsexplorer;
        public static List<string> stringscontainedexplorer = new List<string>();
        public static bool containsstringsfinishedexplorer = false;
        //  List<int> listainte = new List<int>();
        //   List<IntPtr> listeeea = new List<IntPtr>();
        int totalpoints = 0;
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
        private string[] searchplaces = new string[] { @"C:\Users\" + Environment.UserName + @"\Desktop", @"C:\Users\" + Environment.UserName + @"\AppData\Roaming", @"C:\Users\" + Environment.UserName + @"\Documents", @"C:\Users\" + Environment.UserName + @"\Downloads", @"C:\Users\" + Environment.UserName + @"\OneDrive", @"C:\Users\" + Environment.UserName + @"\videos", @"C:\Users\" + Environment.UserName + @"\images", @"C:\$Recycle.Bin", @"C:\Users\" + Environment.UserName + @"\recent", @"C:\Windows\Prefetch", @"C:\Program Files\AutoHotkey", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };

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
        //  private BackgroundWorker worker;
        //  private BackgroundWorker workerfour;
        //private BackgroundWorker workersearch;
        //   private BackgroundWorker workersvchost;
        //private BackgroundWorker workerthree;
        //  private BackgroundWorker workertimer;
        //  private BackgroundWorker workertoo;
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
                for (int f = 0; f < disks.Length; f++)
                {
                    searchplaces[f + 11] = disks[f];
                }
            }
            catch { }
            string ln;
            try
            {
                label1.Text = "Silent scanner " + version;
                //  if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version"))
                //  {
                //  label1.Text = "Silent scanner " + File.ReadAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version");
                //  }(
                File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version");
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\version", version);
            }
            catch { }
            possiblecheatsonlauncher = null;
            List<string> liste = new List<string>();
            List<string> lista = new List<string>();
            List<string> possiblecheatslauncherlist = new List<string>();
            int counter = 0;
            try
            {
                if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\launcher_profiles.json"))
                {
                    StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\launcher_profiles.json");

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
                            }
                        }
                        counter++;
                    }

                    file.Close();
                }
            }
            catch { }
            //   minecraftnames = liste.ToArray();
            //   possiblecheatsonlauncher = String.Join(", ", possiblecheatslauncherlist.ToArray());
            //  minecraftnamesjoin = String.Join("-", minecraftnames);
            try
            {
                if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\launcher_profiles.json"))
                {
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
                }
            }
            catch { }
        }
        Series series1 = new Series
        {
            Name = "Series1",
            Color = System.Drawing.Color.Orange,
            IsVisibleInLegend = false,
            IsXValueIndexed = true,
            ChartType = SeriesChartType.Line
        };
        public static string[] GetAllDrives()
        {
            List<string> list = new List<string>();
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    try
                    {
                        list.Add(drive.Name);
                    }
                    catch { }
                }
            }
            catch { }
            return list.ToArray();
        }

        public static string[] GetDrives()
        {
            List<string> list = new List<string>();
            try
            {
                foreach (var drive in DriveInfo.GetDrives())
                {
                    try
                    {
                        if (drive.TotalSize <= 17179860387)
                        {
                            list.Add(drive.Name);
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return list.ToArray();
        }
        public static string[] GetExplorerStrings()
        {
            string[] allthedrives = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            bool cansearch = false;
            List<string> explorerstringslist = new List<string>();
            string[] explorerstrings;
            for (int eeeff = 0; eeeff < allthedrives.Length; eeeff++)
            {
                cansearch = true;
                for (int alldrivesint = 0; alldrivesint < alldrives.Length; alldrivesint++)
                {
                    if (alldrives[alldrivesint].ToLower().Contains(allthedrives[eeeff].ToLower()))
                    {
                        cansearch = false;
                        break;
                    }
                }
                if (cansearch)
                {
                    explorerstringslist.Add(allthedrives[eeeff] + @":\");
                }
            }

            try
            {
                explorerstringslist.AddRange(new WebClient().DownloadString("https://silentscanner.000webhostapp.com/Silent_scanner/explorerstrings").Replace("\n", "").Split('\r'));
                explorerstrings = string.Join("\r", explorerstringslist).ToLower().Split('\r');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", string.Join("\r\n", explorerstrings));
            }
            catch
            {
                if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer"))
                {
                    StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                    explorerstrings = file.ReadToEnd().ToLower().Replace("\n", "").Split('\r');
                    file.Close();
                }
                else
                {
                    explorerstrings = new string[0];
                }
            }
            return explorerstrings;
        }

        public static string[] GetJavawStrings()
        {
            //string[] allthedrives = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
            //bool cansearch = false;
            string[] javawstrings;
            List<string> javawstringslist = new List<string>();
            //for (int eeeff = 0; eeeff < allthedrives.Length; eeeff++)
            //{
            //    cansearch = true;
            //    for (int alldrivesint = 0; alldrivesint < alldrives.Length; alldrivesint++)
            //    {
            //        if (alldrives[alldrivesint].ToLower().Contains(allthedrives[eeeff].ToLower()))
            //        {
            //            cansearch = false;
            //            break;
            //        }
            //    }
            //    if (cansearch)
            //    {
            //        javawstringslist.Add(allthedrives[eeeff] + @":\");
            //    }
            //}

            try
            {
                javawstringslist.AddRange(new WebClient().DownloadString("https://silentscanner.000webhostapp.com/Silent_scanner/javawstrings").Replace("\n", "").ToLower().Split('\r'));
                javawstrings = string.Join("\r", javawstringslist).ToLower().Split('\r');
                File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw", string.Join("\r\n", javawstrings));
            }
            catch
            {
                StreamReader file = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");
                javawstrings = file.ReadToEnd().ToLower().Replace("\n", "").Split('\r');
                file.Close();
            }
            return javawstrings;
        }


        public void dankchart(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(dankchart), new object[] { value });
                    return;
                }
                allactions++;
                // if (gamerstopwatch.Elapsed.TotalSeconds > 1)
                {
                    if (chart1.ChartAreas[0].AxisX.Maximum - 10 < totalpoints)
                    {
                        chart1.ChartAreas[0].AxisX.Maximum += 25;
                        chart1.ChartAreas[0].AxisX.Minimum += 25;
                    }
                    series1.Points.AddXY(totalpoints + 1, (Convert.ToDouble(value) / 10000) / gamerstopwatch.Elapsed.TotalSeconds);
                    gamerstopwatch.Restart();
                    allactions = 0;
                    totalpoints++;
                }
            }
            catch { }
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
                allactions++;
                progressBar1.Value += Convert.ToInt32(value);
            }
            catch { }
        }
        public void SetProgress(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(SetProgress), new object[] { value });
                    return;
                }
                allactions++;
                progressBar1.Value = Convert.ToInt32(value);
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
                allactions++;
                richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + value);
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
                allactions++;
                label6.Text = value;
            }
            catch { }
        }

        public void scanbuttontext(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(scanbuttontext), new object[] { value });
                    return;
                }
                button1.Text = value;
            }
            catch { }
        }
        public void putdankresults(string value)
        {
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new Action<string>(putdankresults), new object[] { value });
                    return;
                }
                allactions++;
                resultsfg = richTextBox1.Text.Split('£');
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
                allactions++;
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
                allactions++;
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
                allactions++;
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
                allactions++;
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
                allactions++;
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
                allactions++;
                label11.Text = value;
            }
            catch { }
        }

        public string fixtime(string value)
        {
            try
            {
                Convert.ToDateTime(value);
                string[] time = value.Split(':');
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
            return value;
        }

        public List<FileInfo> GetFiles()
        {
            try
            {

                DirectoryInfo di;
                DirectoryInfo[] directories;
                List<FileInfo> files;


                di = new DirectoryInfo(searchplaces[currentplace]);//"C:\");
                directories = di.GetDirectories();

                files = new List<FileInfo>();

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
            //     string[] resultstring;
            // finishedsearch = false;
            if (abortallthreads)
            {
                return;
            }
            try
            {
                var di = new DirectoryInfo(directory);
                var fs = di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);

                List<string> list = new List<string>();
                List<string> liststrings = new List<string>();
                files.AddRange(fs);
                for (int i = 0; files.Count > i; i++)
                {
                    for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                    {
                        string ln = files[i].FullName.ToLower();
                        if (ln.Contains(explorerstrings[currentstringexplorer]))
                        {

                            list.Add(files[i].FullName);
                            liststrings.Add(explorerstrings[currentstringexplorer]);
                            //    liststrings.Add((explorerstrings[currentstringexplorer]));
                            results = list.ToArray();
                        }
                    }

                }


                //     resultstring = liststrings.ToArray();

                var directories = di.GetDirectories();
                try
                {
                    if (results[results.Length - 1] != null && lastfile != results[results.Length - 1])
                    {
                        string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                        AppendTextBoxtoo("£filesearch$ Suspect file found(" + liststrings.ToArray()[results.Length - 1] + ") " + time + ": " + results[results.Length - 1]);// + @"\" + files[files.Count - 1].ToString(), 2);
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
            catch { }
        }

        //public async Task MainAsync()
        //{
        //    _client = new DiscordSocketClient();

        //    var token = "";
        //    await _client.LoginAsync(Discord.TokenType.Bot, token);
        //    await _client.StartAsync();

        //    _client.Ready += Client_Ready;
        //    await Task.Delay(-1);
        //    return;
        //}

        public void Parseresults()
        {
            try
            {
                Thread.Sleep(300);
                //  List<string> thelisttoarray = richTextBox1.Text.Split('£').ToList();
                //string[] resultsfg = thelisttoarray.ToArray();
                int allstuff = 0;
                File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\result.txt");
                try
                {

                    if (resultsfg.Length > 0)
                    {
                        for (int i = 0; resultsfg.Length > i; i++)
                        {
                            if (resultsfg[i].Contains("javawstrings$"))
                            {
                                resultsfg[i] = resultsfg[i].Replace("javawstrings$", "");
                                finalresults[0, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            else if (resultsfg[i].Contains("explorerstrings$"))
                            {
                                resultsfg[i] = resultsfg[i].Replace("explorerstrings$", "");
                                finalresults[1, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            else if (resultsfg[i].Contains(".minecraftjson$"))
                            {
                                resultsfg[i] = resultsfg[i].Replace(".minecraftjson$", "");
                                finalresults[2, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            else if (resultsfg[i].Contains("filesearch$"))
                            {
                                resultsfg[i] = resultsfg[i].Replace("filesearch$", "");
                                finalresults[3, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            else if (resultsfg[i].Contains("minecraft*.log$"))
                            {
                                resultsfg[i] = resultsfg[i].Replace("minecraft*.log$", "");
                                finalresults[4, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            else if (resultsfg[i].Contains("dnssearch$"))
                            {
                                resultsfg[i] = resultsfg[i].Replace("dnssearch$", "");
                                finalresults[4, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            else
                            {
                                finalresults[5, allstuff] = resultsfg[i];
                                allstuff++;
                            }
                            //minecraft *.log
                            //.minecraftjson
                            //javawstrings
                            //explorerstrings
                            //filesearch
                        }
                        try
                        {
                            List<string> filecont = new List<string>();
                            filecont.Add("Username: " + Environment.UserName + "  Nickname(s): " + String.Join(", ", minecraftnames));
                            filecont.Add(possiblecheatsonlauncher + DateTime.Now + " Result: ");
                            for (int a = 0; 10 > a; a++)
                            {
                                for (int e = 0; allstuff > e; e++)// length of finalresult string[]
                                {
                                    if (finalresults[a, e] != null)
                                    {
                                        filecont.Add(finalresults[a, e]);
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
                }
                catch { }
            }
            catch { }
        }

        public void worker_secondthreadsearch()//object sender, DoWorkEventArgs e)
        {
            try
            {
                string ln = "";
                //  int counteer = 0;
                List<string> possiblecheatslogfile = new List<string>();

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

                    //   if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer"))
                    //      {
                    //           StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                    //Convert.ToInt32(new FileInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Length);
                    // while ((ln = filee.ReadLine()) != null)
                    Searcher.Searchboi("explorer", "");
                    AddProgress("10");

                    List<string> explorercontainstrings = new List<string>();
                    // HashSet<string> chunkcompare = new HashSet<string>();
                    searchstringsexplorerlist.AddRange(searchstringsexplorer);

                    List<string> searchstringsexplorerlisttolower = new List<string>();
                    //  List<string> noidea = new List<string>();
                    searchstringsexplorerlisttolower.AddRange(string.Join("\r", searchstringsexplorerlist).ToLower().Split('\r'));
                    // string time = "";
                    //    HashSet<string> chunkcompare;
                    int[] stringlimiter = new int[explorerstrings.Length];
                    //List<string> chunkcomparetemp = new List<string>();
                    string[] temp = new string[searchstringsexplorerlisttolower.Count / 1000];
                    List<string> linestringarray = new List<string>();
                    //  int helpme = 0;
                    //string[] danked = new string[searchstringsexplorer.Count];
                    /*  for (int eeed = 0; eeed < searchstringsexplorer.Count; eeed++)
                       {
                           danked[eeed] = searchstringsexplorer.ToArray()[eeed];
                       }*/

                    //    for (int notsure = 0; notsure < explorerstrings.Length; notsure++)
                    //       {
                    //            if (searchstringsexplorer.Any(l => l.Contains(explorerstrings[notsure])))
                    //           {
                    //                explorercontainstrings.Add(explorerstrings[notsure]);
                    //               ChangeLabelten(explorerstrings[notsure]);
                    //          }
                    //            Searcher.searchprogress = notsure + @"\" + explorerstrings.Length;
                    //  ChangeLabeltoo(notsure + @"\" + explorerstrings.Length);
                    //  explorercontainstrings = containsstrings(explorerstrings, searchstringsexplorer, 1);
                    if (abortallthreads)
                    {
                        return;
                    }
                    AddProgress("10");
                    try
                    {


                        /*     while (helpme < searchstringsexplorerlisttolower.Count)
                             {
                                 try
                                 {
                                     //  chunkcompare.Clear();
                                     if (searchstringsexplorerlisttolower.Count + helpme / 1000 > searchstringsexplorerlisttolower.Count)
                                     {
                                         Array.Copy(searchstringsexplorerlisttolower.ToArray(), helpme, temp, 0, searchstringsexplorerlisttolower.Count - helpme);
                                         chunkcompare = new HashSet<string>(temp);
                                     }
                                     else
                                     {
                                         Array.Copy(searchstringsexplorerlisttolower.ToArray(), helpme, temp, 0, searchstringsexplorerlisttolower.Count / 1000);//6 minutes 1000// 1:30~ 1k
                                         chunkcompare = new HashSet<string>(temp);
                                         //  chunkcompare.Add();//chunkcomparetemp;
                                     }
                                     for (int currentstringexplorere = 0; currentstringexplorere < explorerstrings.Length; currentstringexplorere++)
                                     {
                                         if (chunkcompare.Any(k => k.Contains(explorerstrings[currentstringexplorere])))
                                         {*/
                        linestringarray = searchstringsexplorerlisttolower;//chunkcompare.ToList();
                        stringsearch[0, 0] = linestringarray.Count;
                        for (int damn = 0; damn < linestringarray.Count(); damn++)
                        {
                            ln = linestringarray[damn];
                            for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                            {
                                if (ln.Length < 4)
                                {
                                    break;
                                }
                                if (ln.Contains(explorerstrings[currentstringexplorer]) && stringlimiter[currentstringexplorer] < 20)
                                {
                                    ChangeLabelten(explorerstrings[currentstringexplorer]);
                                    AppendTextBoxtoo("£explorerstrings$" + fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString()) + " Possible Explorer string found (" + explorerstrings[currentstringexplorer] + "): " + linestringarray[damn]);
                                    stringlimiter[currentstringexplorer]++;
                                    if (abortallthreads)
                                    {
                                        return;
                                    }
                                }
                                allactions++;
                            }
                            //    ChangeLabeltoo(stringsearch[0, 1] + @"\" + stringsearch[0, 0]);
                            allactions++;
                            Searcher.searchprogress = "Checking Explorer strings: " + damn + @"\" + stringsearch[0, 0];
                            if (abortallthreads)
                            {
                                return;
                            }
                        }
                        //break;
                        // counteer++;// HERE BOI 

                    }

                    //    }
                    // }  

                    catch { }

                    //  }
                }
                catch { }
                //   }
            }
            catch { }
            //  }
            //   catch { }
            explorersearched = true;
        }
        public void worker_Dochart()
        {
            int timespersecond = 5;
            while (!messagesent)
            {
                Thread.Sleep(10 / timespersecond * 100);
                dankchart((allactions * timespersecond).ToString());

                if (abortallthreads)
                {
                    return;
                }
            }
        }
        public void worker_timer()
        {
            Stopwatch counter = new Stopwatch();
            counter.Start();
            string timeelapsed = "";
            while (!messagesent)
            {
                Thread.Sleep(500);
                if (!explorersearched && Searcher.searchprogress != "" && !explorerfinishedstringsearch && !explorerfinishedstringsearch)
                {
                    switch (currentnumbere)
                    {
                        case 0:

                            ChangeLabeltoo(Searcher.searchprogress);
                            currentnumbere++;
                            break;

                        case 1:

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
                else if (explorersearched && !explorerstringsearcherror && !explorerfinishedstringsearch && !Searcher.searchprogress.ToLower().Contains("search finished"))
                {
                    Searcher.searchprogress = "Explorer string search finished";
                    ChangeLabeltoo("Explorer string search finished");
                    explorerfinishedstringsearch = true;
                }
                else if (explorerstringsearcherror && !explorerfinishedstringsearch && Searcher.searchprogress.Length > 5)
                {
                    ChangeLabeltoo(Searcher.searchprogress);
                    explorerfinishedstringsearch = true;
                }

                if (!javawsearched && Searcher.searchprogressjavaw != "" && !javawfinishedstringsearch && !javawstringsearcherror)
                {
                    switch (currentnumbere)
                    {
                        case 0:

                            ChangeLabel(Searcher.searchprogressjavaw);
                            break;

                        case 1:

                            ChangeLabel(Searcher.searchprogressjavaw + ".");
                            break;

                        case 2:
                            //       currentstat = currentstatarray[0];
                            ChangeLabel(Searcher.searchprogressjavaw + "..");
                            break;

                        case 3:
                            ChangeLabel(Searcher.searchprogressjavaw + "...");
                            break;
                    }
                }

                else if (javawsearched && !javawstringsearcherror && !javawfinishedstringsearch)
                {
                    Searcher.searchprogressjavaw = "Javaw string search finished";
                    ChangeLabel("Javaw string search finished");
                    javawfinishedstringsearch = true;
                }
                else if (javawstringsearcherror && !javawfinishedstringsearch && Searcher.searchprogressjavaw.Length > 5)
                {
                    ChangeLabel(Searcher.searchprogressjavaw);
                    javawfinishedstringsearch = true;
                }
                if (counter.Elapsed.TotalSeconds != timing.TotalSeconds)
                {
                    timing = counter.Elapsed;
                    timeelapsed = fixtime(timing.Minutes.ToString() + ":" + timing.Seconds.ToString());
                    Changetimer(timeelapsed);
                    putdankresults("");


                    switch (currentnumbere)
                    {
                        case 0:

                            ChangeLabeltwo(currentstat);
                            break;

                        case 1:

                            ChangeLabeltwo(currentstat + ".");
                            break;

                        case 2:
                            //       currentstat = currentstatarray[0];
                            ChangeLabeltwo(currentstat + "..");
                            break;

                        case 3:
                            //  currentstat = currentstatarray[0];
                            ChangeLabeltwo(currentstat + "...");
                            break;
                    }
                }




                if (abortallthreads)
                {
                    return;
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
        private void worker_displaydnssearch()
        {

            string dnsdisplay = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\displaydns.txt";
            File.Delete(dnsdisplay);
            File.Delete(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\dnsdisplay.bat");
            File.WriteAllText(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\dnsdisplay.bat", @"cd %temp%\Silent_scanner & ipconfig /displaydns>displaydns.txt");
            Process.Start(new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = @"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\dnsdisplay.bat"
            });

            Thread.Sleep(15000);
            try
            {
                //   string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.json", SearchOption.AllDirectories);

                string[] text = File.ReadAllLines(dnsdisplay);
                string time = "";

                try
                {
                    if (explorerstrings.Length >= 1)
                    {
                        for (int ef = 0; text.Length > ef; ef++)
                        {
                            //       string[] texttolower = text[ef].ToLower().Split('\"');

                            for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                            {
                                //  for (int unusedyet = 0; unusedyet < texttolower.Length; unusedyet++)
                                //    {
                                if (text[ef].Contains(explorerstrings[currentstringexplorer]))
                                {
                                    time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                    AppendTextBoxtoo("£dnssearch$" + time + " Possible cheat in dns logs(" + explorerstrings[currentstringexplorer] + ") " + dnsdisplay + "  " + text[ef]);
                                    if (abortallthreads)
                                    {
                                        return;
                                    }
                                }
                                // }
                                allactions++;
                            }
                        }
                    }
                }
                catch { }

            }
            catch { }
            AddProgress("10");


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            chart1.Series.Clear();


            chart1.Series.Add(series1);

            chart1.Invalidate();
            ChartArea CA = chart1.ChartAreas[0];
            CA.Position = new ElementPosition(0, 0, 100, 100);
            chart1.ChartAreas[0].BackColor = Color.Transparent;
            chart1.ChartAreas[0].BorderColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisY.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorTickMark.LineColor = Color.White;
            chart1.ChartAreas[0].AxisX.MajorTickMark.LineColor = Color.Transparent;
            chart1.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            // chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            //    chart1.ChartAreas[0].AxisY.MajorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Maximum = 50;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            //    chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
            series1.Color = Color.FromArgb(255, 0, 164, 204);
        }

        public void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = System.Windows.Forms.Cursor.Position;
            dragFormPoint = this.Location;
        }

        public void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(System.Windows.Forms.Cursor.Position, new Size(dragCursorPoint));
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
                        //  richTextBox1.AppendText(Environment.NewLine + Environment.NewLine + value);
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




        private void label10_Click(object sender, EventArgs e)
        {
            scanner.Add(3);
        }

        private void label3_Click_1(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
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






            // make one time use?



            //dns saearch
            //makelookgood

            //  if (progressBar1.Value > 0)
            //    {
            if (!explorersearched && !javawsearched)
            {
                scanbuttontext("Stop scan");
                SetProgress("0");
                // clearchart("");

                gamerstopwatch.Reset();
                gamerstopwatch.Start();
                messagesent = false;
                Changetimer("00:00");
                richTextBox1.Text = "";
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
                alldrives = GetAllDrives();
                disks = GetDrives();
                explorerstrings = GetExplorerStrings();
                javawstrings = GetJavawStrings();
                searchstringsjavaw.Clear();
                searchstringsexplorer.Clear();
                currentstat = "Searching for cheats";
            }
            //   workertimer = new BackgroundWorker();
            //  workertimer.DoWork += new DoWorkEventHandler(worker_timer);
            //    workertimer.RunWorkerAsync();
            Thread threadstarter = new Thread(worker_threadstarter);

            threadstarter.Start();

            //  workertoo = new BackgroundWorker();
            //  workertoo.DoWork += new DoWorkEventHandler(worker_secondthreadsearch);
            //  workertoo.RunWorkerAsync();
            //   }

        }
        [SecurityPermissionAttribute(SecurityAction.Demand, ControlThread = true)]
        public void worker_threadstarter()
        {
            abortallthreads = false;
            Thread danktimer = new Thread(worker_timer);
            Thread workerexplorerstrings = new Thread(worker_secondthreadsearch);
            Thread workerfilesearcher = new Thread(worker_search);
            Thread workerjavawstrings = new Thread(worker_DoSearch);
            Thread chartworker = new Thread(worker_Dochart);
            Thread workertoo = new Thread(worker_too);
            Thread workerfive = new Thread(worker_five);
            Thread displaydnssearch = new Thread(worker_displaydnssearch);

            if (istimerrunning == false && !explorersearched && !javawsearched && !abortallthreads)
            {

                try
                {
                    chartworker.Start();
                }
                catch { }
                try
                {
                    danktimer.Start();
                }
                catch { }
                try
                {
                    workertoo.Start();
                }
                catch { }
                try
                {
                    workerfive.Start();
                }
                catch { }
                try
                {
                    workerexplorerstrings.Start();
                }
                catch { }
                try
                {
                    workerfilesearcher.Start();
                }
                catch { }
                try
                {
                    workerjavawstrings.Start();
                }
                catch { }
                try
                {
                    displaydnssearch.Start();
                }
                catch { }
                istimerrunning = true;
            }
            else if (istimerrunning == true)
            {
                Parseresults();
                scanbuttontext("Restart program");
                abortallthreads = true;


                //try
                //{
                //    danktimer.Abort();
                //}
                //catch { }
                //try
                //{
                //    workertoo.Abort();
                //}
                //catch { }
                //try
                //{
                //    workerfive.Abort();
                //}
                //catch { }
                //try
                //{
                //    workerexplorerstrings.Abort();
                //}
                //catch { }
                //try
                //{
                //    workerfilesearcher.Abort();
                //}
                //catch { }
                //try
                //{
                //    workerjavawstrings.Abort();
                //} 251 255
                //catch { }
                //try220 220
                //{
                //    chartworker.Abort();
                //}
                //catch { }
                istimerrunning = false;



                while (true)
                {

                    ChangeLabelten("");
                    ChangeLabel("");
                    ChangeLabelnine("");
                    ChangeLabelseven("");
                    ChangeLabeltoo("");
                    ChangeLabeltwo("Scan stopped early. Restart program to scan again.");
                    Thread.Sleep(1000);
                }

            }
            else
            {
                Parseresults();
                Process.Start(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\Silent_scanner.exe");
                Thread.Sleep(1000);
                Application.Exit();
                Environment.Exit(69);
            }

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
            Application.Exit();
            Environment.Exit(420);
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            if (String.Join("", scanner.ToArray()) == "43201")
            {
            }
            scanner.Clear();
        }
        //2:45
        //public List<string> containsstrings(string[] allstringse, HashSet<string> hashsete, int label)
        //{
        //    List<string> containstrings = new List<string>();
        //    switch (label)
        //    {
        //        case 2:

        //            stringscontainedjavaw.Clear();
        //            containsstringsfinishedjavaw = false;
        //            allstringsjavaw = allstringse;
        //            hashset = hashsete;
        //            try
        //            {
        //                Thread containsstringstwo = new Thread(worker_containsstringstwojavaw);
        //                containsstringstwo.Start();
        //                for (int notsure = 0; notsure < allstringsjavaw.Length / 2; notsure++)
        //                {
        //                    try
        //                    {
        //                        if (hashset.Any(l => l.Contains(allstringsjavaw[notsure])))
        //                        {
        //                            containstrings.Add(allstringsjavaw[notsure]);
        //                            ChangeLabelten(allstringsjavaw[notsure]);
        //                        }
        //                        try
        //                        {

        //                            Searcher.searchprogressjavaw = notsure + @"\" + javawstrings.Length / 2;
        //                        }
        //                        catch { }

        //                    }
        //                    catch { }
        //                    //  ChangeLabeltoo(notsure + @"\" + javawstrings.Length);
        //                }

        //            }
        //            catch { }

        //            while (true)
        //            {
        //                if (containsstringsfinishedjavaw)
        //                {
        //                    containstrings.AddRange(stringscontainedjavaw);
        //                    return containstrings;
        //                }
        //            }
        //        //break;
        //        case 1:

        //            stringscontainedexplorer.Clear();
        //            containsstringsfinishedexplorer = false;
        //            allstringsexplorer = allstringse;
        //            hashset = hashsete;
        //            try
        //            {
        //                Thread containsstringstwo = new Thread(worker_containsstringstwoexplorer);
        //                containsstringstwo.Start();
        //                for (int notsure = 0; notsure < allstringsexplorer.Length / 2; notsure++)
        //                {
        //                    try
        //                    {
        //                        if (hashset.Any(l => l.Contains(allstringsexplorer[notsure])))
        //                        {
        //                            containstrings.Add(allstringsexplorer[notsure]);
        //                            ChangeLabelten(allstringsexplorer[notsure]);
        //                        }
        //                        try
        //                        {

        //                            Searcher.searchprogress = notsure + @"\" + explorerstrings.Length / 2;

        //                        }
        //                        catch { }

        //                    }
        //                    catch { }
        //                    //  ChangeLabeltoo(notsure + @"\" + explorerstrings.Length);
        //                }

        //            }
        //            catch { }

        //            while (true)
        //            {
        //                if (containsstringsfinishedexplorer)
        //                {
        //                    containstrings.AddRange(stringscontainedexplorer);
        //                    return containstrings;
        //                }
        //            }


        //            //break;







        //    }
        //    return containstrings;
        //}
        //public void worker_containsstringstwojavaw()// PASS HASHSET AND STRING[] ON WORKER, SEARCH ON GOOGLE WHEN U GOT WIFI LMAO, FUG DIS SHIT GOOD NIGHT AND GOOD MORNING MA BOI
        //{
        //    List<string> containstrings = new List<string>();
        //    try
        //    {
        //        for (int notsure = allstringsjavaw.Length / 2; notsure < allstringsjavaw.Length; notsure++)
        //        {
        //            try
        //            {
        //                if (hashset.Any(l => l.Contains(allstringsjavaw[notsure])))
        //                {
        //                    stringscontainedjavaw.Add(allstringsjavaw[notsure]);
        //                    ChangeLabelten(allstringsjavaw[notsure]);
        //                }

        //            }
        //            catch { }
        //            //   ChangeLabeltoo(notsure - javawstrings.Length / 2 + @"\" + javawstrings.Length / 2);
        //            Searcher.searchprogressjavaw = notsure - javawstrings.Length / 2 + @"\" + javawstrings.Length / 2;
        //        }

        //    }
        //    catch { }
        //    containsstringsfinishedjavaw = true;
        //}

        //public void worker_containsstringstwoexplorer()
        //{
        //    List<string> containstrings = new List<string>();
        //    try
        //    {
        //        for (int notsure = allstringsexplorer.Length / 2; notsure < allstringsexplorer.Length; notsure++)
        //        {
        //            try
        //            {
        //                if (hashset.Any(l => l.Contains(allstringsexplorer[notsure])))
        //                {
        //                    stringscontainedexplorer.Add(allstringsexplorer[notsure]);
        //                    ChangeLabelten(allstringsexplorer[notsure]);
        //                }

        //            }
        //            catch { }
        //            Searcher.searchprogress = notsure - explorerstrings.Length / 2 + @"\" + explorerstrings.Length / 2;

        //            //  ChangeLabeltoo(notsure - explorerstrings.Length / 2 + @"\" + explorerstrings.Length / 2);
        //        }

        //    }
        //    catch { }
        //    containsstringsfinishedexplorer = true;
        //}
        public void worker_DoSearch()
        {
            string ln = "";
            //  int counteer = 0;

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
            try
            {
                Searcher.Searchboi("javaw", "");
                AddProgress("10");
                //  if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw"))

                //   StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\javaw");

                List<string> javawcontainstrings = new List<string>();
                // HashSet<string> chunkcompare = new HashSet<string>();
                searchstringsjavawlist.AddRange(searchstringsjavaw);
                List<string> searchstringsjavawlisttolower = new List<string>();
                // List<string> noidea = new List<string>();
                searchstringsjavawlisttolower.AddRange(string.Join("\r", searchstringsjavawlist).ToLower().Split('\r'));
                // string time = "";
                //HashSet<string> chunkcompare;
                int[] stringlimiter = new int[javawstrings.Length];
                //  stringlimiter
                //List<string> chunkcomparetemp = new List<string>();
                //  string[] temp = new string[searchstringsjavawlisttolower.Count / 1000];
                List<string> linestringarray = new List<string>();
                stringsearch[1, 0] = searchstringsjavawlisttolower.Count;
                //string[] danked = new string[searchstringsjavaw.Count];
                /*  for (int eeed = 0; eeed < searchstringsjavaw.Count; eeed++)
                   {
                       danked[eeed] = searchstringsjavaw.ToArray()[eeed];
                   }*/
                /*  try
                  {
                      for (int notsure = 0; notsure < javawstrings.Length; notsure++)
                      {
                          try
                          {
                              if (searchstringsjavaw.Any(l => l.Contains(javawstrings[notsure])))
                              {
                                  javawcontainstrings.Add(javawstrings[notsure]);
                                  ChangeLabelten("Comparing strings");
                              }
                              Searcher.searchprogressjavaw = notsure + @"\" + javawstrings.Length;
                          }
                          catch { }
                          //  ChangeLabeltoo(notsure + @"\" + javawstrings.Length);
                      }
                  }*/
                if (abortallthreads)
                {
                    return;
                }
                try
                {
                    //javawcontainstrings = containsstrings(javawstrings, searchstringsjavaw, 2);
                }
                catch { }
                AddProgress("10");
                try
                {
                    //     int helpme = 0;
                    //     while (helpme < searchstringsjavawlisttolower.Count)
                    //     {
                    try
                    {
                        //if (helpme + searchstringsjavawlisttolower.Count / 1000 > searchstringsjavawlisttolower.Count)
                        //{
                        //    Array.Copy(searchstringsjavawlisttolower.ToArray(), helpme, temp, 0, searchstringsjavawlisttolower.Count - helpme);
                        //    chunkcompare = new HashSet<string>(temp);
                        //}
                        //else
                        //{
                        //    Array.Copy(searchstringsjavawlisttolower.ToArray(), helpme, temp, 0, searchstringsjavawlisttolower.Count / 1000);
                        //    chunkcompare = new HashSet<string>(temp);
                        //}
                        //for (int currentstringjavawe = 0; currentstringjavawe < javawstrings.Length; currentstringjavawe++)
                        //{
                        //    try
                        //    {
                        //        if (chunkcompare.Any(k => k.Contains(javawstrings[currentstringjavawe])))
                        //        {
                        linestringarray = searchstringsjavawlisttolower;//chunkcompare.ToList();
                        for (int damn = 0; damn < linestringarray.Count(); damn++)
                        {
                            ln = linestringarray[damn];
                            for (int currentstringjavaw = 0; currentstringjavaw < javawstrings.Length; currentstringjavaw++)
                            {
                                if (ln.Length < 4)
                                {
                                    break;
                                }
                                if (ln.Contains(javawstrings[currentstringjavaw]) && stringlimiter[currentstringjavaw] < 20)
                                {
                                    ChangeLabelten(javawstrings[currentstringjavaw]);
                                    AppendTextBoxtoo("£javawstrings$" + fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString()) + " Possible Javaw string found(" + javawstrings[currentstringjavaw] + "): " + linestringarray[damn]);
                                    stringlimiter[currentstringjavaw]++;
                                    if (abortallthreads)
                                    {
                                        return;
                                    }
                                }
                                totalpoints++;
                            }

                            Searcher.searchprogressjavaw = damn + @"\" + stringsearch[1, 0];
                            totalpoints++;
                            if (abortallthreads)
                            {
                                return;
                            }
                        }



                        //   break;
                    }
                    catch { }

                    //    stringsearch[1, 1] = helpme - 1;
                }
                catch { }
            }
            // }
            catch { }
            // helpme += searchstringsjavawlisttolower.Count / 1000;

            //  }
            //  catch { }
            // }
            //  catch { }
            javawsearched = true;
            AddProgress("10");
        }

        private void worker_five()
        {
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
                // List<string> javawcontainstrings = new List<string>();
                //  HashSet<string> javawhashset = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                string[] text;
                for (int i = 0; filePaths.Length > i; i++)
                {
                    if (abortallthreads)
                    {
                        return;
                    }
                    text = File.ReadAllLines(filePaths[i]);
                    try
                    {
                        if (javawstrings.Length >= 1)
                        {
                            for (int ef = 0; text.Length > ef; ef++)
                            {
                                string[] texttolower = text[ef].ToLower().Split('\"');

                                for (int currentstringjavaw = 0; currentstringjavaw < javawstrings.Length; currentstringjavaw++)
                                {
                                    for (int unusedyet = 0; unusedyet < texttolower.Length; unusedyet++)
                                    {
                                        if (texttolower[unusedyet].Contains(javawstrings[currentstringjavaw]))
                                        {
                                            string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                            AppendTextBoxtoo("£.minecraftjson$" + time + " Possible cheat in .minecraft(" + javawstrings[currentstringjavaw] + ")" + filePaths[i] + "  " + texttolower[unusedyet]); //minecraftjson$
                                            if (abortallthreads)
                                            {
                                                return;
                                            }
                                        }
                                    }
                                    allactions++;
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

        private void worker_search()
        {
            try
            {
                while (currentplace < searchplaces.Length)
                {
                    if (abortallthreads)
                    {
                        return;
                    }
                    files = GetFiles();
                    allactions++;
                    currentplace++;
                }
            }
            catch { }
            AddProgress("20");
            try
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    if (javawsearched && explorersearched || progressBar1.Value == 100)
                    {
                        AddProgress("10");

                        Parseresults();

                        currentstat = "Search finished! Check the results";
                        break;
                    }
                }

                //     MainAsync();
                // Thread.Sleep(10000);

                messagesent = true;
                SetProgress("100");
            }
            catch { }
        }

        private void worker_three(object sender, DoWorkEventArgs e)
        {
            /*   try
               {
                   string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\versions", "*.json", SearchOption.TopDirectoryOnly);

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
                   List<string> explorercontainstrings = new List<string>();
                   HashSet<string> explorerhashset = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                   string[] text;
                   for (int i = 0; filePaths.Length > i; i++)
                   {
                       try
                       {
                           text = File.ReadAllLines(filePaths[i]);
                           explorerhashset = new HashSet<string>(text.ToList());

                           explorercontainstrings.Clear();
                           try
                           {
                               for (int notsure = 0; notsure < explorerstrings.Length; notsure++)
                               {
                                   try
                                   {
                                       if (explorerhashset.Any(l => l.Contains(explorerstrings[notsure])))
                                       {
                                           explorercontainstrings.Add(explorerstrings[notsure]);
                                       }
                                   }
                                   catch { }
                               }
                           }
                           catch { }
                           if (explorercontainstrings.Count >= 1)
                           {
                               for (int ef = 0; text.Length > ef; ef++)
                               {
                                   string texttolower = text[ef].ToLower();
                                   for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                                   {
                                       if (texttolower.Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                                       {
                                           string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                           AppendTextBoxtoo("£.minecraftjson$" + time + " Possible cheat in .minecraft(" + explorerstrings[currentstringexplorer] + "): " + filePaths[i] + "  " + texttolower);// .minecraftjson
                                       }
                                   }
                               }
                           }
                       }
                       catch { }
                   }

                   AddProgress("20");
               }
               catch { }*/
        }

        private void worker_too()
        {
            try
            {
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
                for (int i = 0; filePaths.Length > i; i++)
                {
                    string[] text = File.ReadAllLines(filePaths[i]);
                    for (int ef = 0; text.Length > ef; ef++)
                    {
                        string[] texttolower = text[ef].ToLower().Split('\"');

                        for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                        {
                            for (int unusedyet = 0; unusedyet < texttolower.Length; unusedyet++)
                            {
                                if (texttolower[unusedyet].Contains(explorerstrings[currentstringexplorer].Replace("\r\n", "")))
                                {
                                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                    AppendTextBoxtoo("£minecraft*.log$" + time + " Possible cheat in .minecraft(" + explorerstrings[currentstringexplorer] + "): " + filePaths[i] + "  " + texttolower[unusedyet]);// minecraft*.log
                                    if (abortallthreads)
                                    {
                                        return;
                                    }
                                }

                            }
                            allactions++;
                        }

                    }
                    if (abortallthreads)
                    {
                        return;
                    }
                }
            }
            catch { }
            AddProgress("10");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
            allactions++;
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // messagesent = true;
            //  Parseresults();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
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

        public static void CallSearchError(string message, string processname)
        {
            try
            {
                //   if (SearchFinished != null)
                //      searchfinished = true;

                if (processname == "explorer")
                {
                    Form1.explorerstringsearcherror = true;
                    searchprogress = message;
                    Form1.explorersearched = true;
                }
                else if (processname == "javaw")
                {
                    Form1.javawstringsearcherror = true;
                    Form1.javawsearched = true;
                    searchprogressjavaw = message;
                }
            }
            catch { }
            //    searchprogress = message;
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

        public static void CallSearchProgressChanged(string progress, int i)
        {
            try
            {
                //    searchprogress = progress;
                switch (i)
                {
                    case 0:

                        searchprogress = progress;

                        break;
                    case 1:

                        searchprogressjavaw = progress;

                        break;

                    default:

                        searchprogress = progress;

                        break;
                }
                // searchprogress = progress;

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
            //Results.Clear();

            byte[] text = Encoding.Default.GetBytes(searchtext);//(byte[])Params["text"];
            ProcessHandle phandle;
            int count = 0;
            int totalstrings = 0;
            // string explorercurrentstringsearch = "";
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
                CallSearchError("Could not open " + processname + ", Error: " + new Win32Exception(Marshal.GetLastWin32Error()).Message, processname);
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
                       String.Format("Searching Explorer:  0x{0} ({1} / " + info.RegionSize.ToInt32() + ")", info.BaseAddress.ToString("x"), count), 0);

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
                        if (curstr.Length >= minsize && curstr.Length <= 1000)
                        {
                            int length = curstr.Length;

                            if (isUnicode)
                            {
                                length *= 2;
                            }

                            //      Results.Add(new string[] { Utils.FormatAddress(info.BaseAddress),
                            //String.Format("0x{0:x}", i - length), length.ToString(),
                            //       curstr.ToString() });
                            //      explorercurrentstringsearch = curstr.ToString().ToLower();
                            //    for (int currentstringexplorer = 0; currentstringexplorer < Form1.explorerstrings.Length; currentstringexplorer++)
                            //   {
                            //     if (new List<String>(explorerstrings)).Contains(explorercurrentstringsearch[currentstringexplorer]))//(explorercurrentstringsearch.Contains(Form1.explorerstrings[currentstringexplorer].ToLower()))
                            //   {
                            //                 if (currentstringexplorer > 26 - Form1.alldrives.Length)
                            // {
                            //       Form1.searchstringsexplorer.Add(curstr.ToString());
                            //  Form1.searchstringsexplorerlist.Contains()
                            Form1.allactions++;
                            Form1.searchstringsexplorer.Add(curstr.ToString());
                            //     }
                            //      else
                            //   {
                            //  }
                            //   }
                            //   }

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
            //Form1.searchstringsexplorerlist = Form1.searchstringsexplorer.ToList();
            phandle.Dispose();

            CallSearchFinished(processname);
        }

        public void Searchingjavawstrings(string processname, string searchtext)
        {
            try
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
                // string javawcurrentstringsearch = "";
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
                    CallSearchError("Could not open " + processname + ", Error: " + new Win32Exception(Marshal.GetLastWin32Error()).Message, processname);
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
                           String.Format("Searching Javaw: 0x{0} ({1} / " + info.RegionSize.ToInt32() + ")", info.BaseAddress.ToString("x"), count), 1);

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
                            if (curstr.Length >= minsize && curstr.Length <= 1000)
                            {
                                int length = curstr.Length;

                                if (isUnicode)
                                    length *= 2;

                                //      Results.Add(new string[] { Utils.FormatAddress(info.BaseAddress),
                                //String.Format("0x{0:x}", i - length), length.ToString(),
                                //       curstr.ToString() });
                                //    javawcurrentstringsearch = curstr.ToString().ToLower();

                                /*  javawcurrentstringsearch = curstr.ToString().ToLower();
                                  for (int currentstringjavaw = 0; currentstringjavaw < Form1.javawstrings.Length; currentstringjavaw++)
                                  {
                                      if (javawcurrentstringsearch.Contains(Form1.javawstrings[currentstringjavaw].ToLower()))
                                      {
                                          Form1.searchstringsjavaw.Add(curstr.ToString());
                                      }
                                  }

                                  {
                                      //file.WriteLine(curstr.ToString());
                                      Form1.searchstringsjavaw.Add(curstr.ToString().ToLower());
                                  }
    */
                                Form1.allactions++;
                                Form1.searchstringsjavaw.Add(curstr.ToString());
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
            catch { }
        }

        private bool IsChar(byte b)
        {
            return (b >= 32 && b <= 126) || b == 10 || b == 13 || b == 9;
        }
    }
}

/*   string strTempFile = Path.GetTempFileName();
            File.WriteAllBytes(strTempFile, Properties.Resources.ProcessHacker);
            string newplace = strTempFile.Split('.')[0] + ".exe";
            File.Move(strTempFile, newplace);
`            System.Diagnostics.Process.Start(newplace);*/
