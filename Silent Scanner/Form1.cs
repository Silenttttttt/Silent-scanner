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
        public static string version = "1.4.5";
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
        public static HashSet<string> searchstringsexplorer = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static List<string> searchstringsexplorerlist = new List<string>();
        public static HashSet<string> searchstringsjavaw = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        public static List<string> searchstringsjavawlist = new List<string>();
        public int currentplace = 0;
        public bool finishedsearch;
        public static string[] allstringsjavaw;
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
        int totalpoints = 0;
        private string laineee;
        private string lastfile;
        private bool messagesent = false;
        private string[] minecraftnames;
        private string minecraftnamesjoin;
        private string possiblecheatsonlauncher;
        private string[] results = new string[1];
        private List<int> scanner = new List<int>();
        private string[] searchplaces = new string[] { @"C:\Users\" + Environment.UserName + @"\Desktop", @"C:\Users\" + Environment.UserName + @"\AppData\Roaming", @"C:\Users\" + Environment.UserName + @"\Documents", @"C:\Users\" + Environment.UserName + @"\Downloads", @"C:\Users\" + Environment.UserName + @"\OneDrive", @"C:\Users\" + Environment.UserName + @"\videos", @"C:\Users\" + Environment.UserName + @"\images", @"C:\$Recycle.Bin", @"C:\Users\" + Environment.UserName + @"\recent", @"C:\Windows\Prefetch", @"C:\Program Files\AutoHotkey", null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null };
        private int[,] stringsearch = new int[5, 5];
        private TimeSpan timing;
        public Form1()
        {
            InitializeComponent();
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
                explorerstringslist.AddRange(new WebClient().DownloadString("https:
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
            string[] javawstrings;
            List<string> javawstringslist = new List<string>();
            try
            {
                javawstringslist.AddRange(new WebClient().DownloadString("https:
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
                di = new DirectoryInfo(searchplaces[currentplace]);
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
                            results = list.ToArray();
                        }
                    }
                }
                var directories = di.GetDirectories();
                try
                {
                    if (results[results.Length - 1] != null && lastfile != results[results.Length - 1])
                    {
                        string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                        AppendTextBoxtoo("£filesearch$ Suspect file found(" + liststrings.ToArray()[results.Length - 1] + ") " + time + ": " + results[results.Length - 1]);
                        lastfile = results[results.Length - 1];
                    }
                }
                catch { }
                foreach (var directoryInfo in directories)
                {
                    try
                    {
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
        public void Parseresults()
        {
            try
            {
                Thread.Sleep(300);
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
                        }
                        try
                        {
                            List<string> filecont = new List<string>();
                            filecont.Add("Username: " + Environment.UserName + "  Nickname(s): " + String.Join(", ", minecraftnames));
                            filecont.Add(possiblecheatsonlauncher + DateTime.Now + " Result: ");
                            for (int a = 0; 10 > a; a++)
                            {
                                for (int e = 0; allstuff > e; e++)
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
        public void worker_secondthreadsearch()
        {
            try
            {
                string ln = "";
                List<string> possiblecheatslogfile = new List<string>();
                try
                {
                    Searcher.Searchboi("explorer", "");
                    AddProgress("10");
                    List<string> explorercontainstrings = new List<string>();
                    searchstringsexplorerlist.AddRange(searchstringsexplorer);
                    List<string> searchstringsexplorerlisttolower = new List<string>();
                    searchstringsexplorerlisttolower.AddRange(string.Join("\r", searchstringsexplorerlist).ToLower().Split('\r'));
                    int[] stringlimiter = new int[explorerstrings.Length];
                    string[] temp = new string[searchstringsexplorerlisttolower.Count / 1000];
                    List<string> linestringarray = new List<string>();
                    if (abortallthreads)
                    {
                        return;
                    }
                    AddProgress("10");
                    try
                    {
                        linestringarray = searchstringsexplorerlisttolower;
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
                            allactions++;
                            Searcher.searchprogress = "Checking Explorer strings: " + damn + @"\" + stringsearch[0, 0];
                            if (abortallthreads)
                            {
                                return;
                            }
                        }
                    }
                    catch { }
                }
                catch { }
            }
            catch { }
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
                if(timing.Minutes.ToString() == "3")
                {
                    ChangeLabeltwo("Scan reached 3 minutes, Check if scanner is stuck. End scan if you think so.");
                }
                if (timing.Minutes.ToString() == "5")
                {
                    ChangeLabeltwo("Scan reached 5 minutes, ending scan is recommended.");
                }
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
                    if (timing.Minutes.ToString() != "3" && timing.Minutes.ToString() != "5")
                    {
                        switch (currentnumbere)
                        {
                            case 0:
                                ChangeLabeltwo(currentstat);
                                break;
                            case 1:
                                ChangeLabeltwo(currentstat + ".");
                                break;
                            case 2:
                                ChangeLabeltwo(currentstat + "..");
                                break;
                            case 3:
                                ChangeLabeltwo(currentstat + "...");
                                break;
                        }
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
            utillitiespage.Show();
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
                string[] text = File.ReadAllLines(dnsdisplay);
                string time = "";
                try
                {
                    if (explorerstrings.Length >= 1)
                    {
                        for (int ef = 0; text.Length > ef; ef++)
                        {
                            for (int currentstringexplorer = 0; currentstringexplorer < explorerstrings.Length; currentstringexplorer++)
                            {
                                if (text[ef].Contains(explorerstrings[currentstringexplorer]))
                                {
                                    time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                    AppendTextBoxtoo("£dnssearch$" + time + " Possible cheat in dns logs(" + explorerstrings[currentstringexplorer] + ") " + dnsdisplay + "  " + text[ef]);
                                    if (abortallthreads)
                                    {
                                        return;
                                    }
                                }
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
            chart1.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.White;
            chart1.ChartAreas[0].AxisY.MinorGrid.Enabled = false;
            chart1.ChartAreas[0].AxisX.Maximum = 50;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
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
            Formpages.Show();
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (!explorersearched && !javawsearched)
            {
                scanbuttontext("Stop scan");
                SetProgress("0");
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
            Thread threadstarter = new Thread(worker_threadstarter);
            threadstarter.Start();
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
        public void worker_DoSearch()
        {
            string ln = "";
            try
            {
                Searcher.Searchboi("javaw", "");
                AddProgress("10");
                List<string> javawcontainstrings = new List<string>();
                searchstringsjavawlist.AddRange(searchstringsjavaw);
                List<string> searchstringsjavawlisttolower = new List<string>();
                searchstringsjavawlisttolower.AddRange(string.Join("\r", searchstringsjavawlist).ToLower().Split('\r'));
                int[] stringlimiter = new int[javawstrings.Length];
                List<string> linestringarray = new List<string>();
                stringsearch[1, 0] = searchstringsjavawlisttolower.Count;
                if (abortallthreads)
                {
                    return;
                }
                try
                {
                }
                catch { }
                AddProgress("10");
                try
                {
                    try
                    {
                        linestringarray = searchstringsjavawlisttolower;
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
                    }
                    catch { }
                }
                catch { }
            }
            catch { }
            javawsearched = true;
            AddProgress("10");
        }
        private void worker_five()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.json", SearchOption.AllDirectories);
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
                                            AppendTextBoxtoo("£.minecraftjson$" + time + " Possible cheat in .minecraft(" + javawstrings[currentstringjavaw] + ")" + filePaths[i] + "  " + texttolower[unusedyet]); 
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
                messagesent = true;
                SetProgress("100");
            }
            catch { }
        }
        private void worker_three(object sender, DoWorkEventArgs e)
        {
        }
        private void worker_too()
        {
            try
            {
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.log", SearchOption.TopDirectoryOnly);
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
                                    AppendTextBoxtoo("£minecraft*.log$" + time + " Possible cheat in .minecraft(" + explorerstrings[currentstringexplorer] + "): " + filePaths[i] + "  " + texttolower[unusedyet]);
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
        }
        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
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
        public Searcher(int PID)
        {
            _pid = PID;
            _params = new Dictionary<string, object>();
            _results = new List<string[]>();
        }
        public event SearchError SearchError;
        public event SearchFinished SearchFinished;
        public event SearchProgressChanged SearchProgressChanged;
        public Dictionary<string, object> Params
        {
            get { return _params; }
        }
        public int PID
        {
            get { return _pid; }
        }
        public List<string[]> Results
        {
            get { return _results; }
            set { _results = value; }
        }
        public static void CallSearchError(string message, string processname)
        {
            try
            {
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
        }
        public static void CallSearchFinished(string processname)
        {
        }
        public static void CallSearchProgressChanged(string progress, int i)
        {
            try
            {
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
            }
            catch { }
        }
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
        }
        public virtual void Search()
        {
        }
        public void Searchingexplorerstrings(string processname, string searchtext)
        {
            byte[] text = Encoding.Default.GetBytes(searchtext);
            ProcessHandle phandle;
            int count = 0;
            int totalstrings = 0;
            int minsize = 4;
            bool unicode = true;
            bool opt_priv = true; 
            bool opt_img = false;
            bool opt_map = true;
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
                        isUnicode = true;
                    }
                    else if (unicode &&
                        data[i] == 0 && IsChar(byte1) && IsChar(byte2) && curstr.Length < minsize)
                    {
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
                            Form1.allactions++;
                            Form1.searchstringsexplorer.Add(curstr.ToString());
                            count++;
                        }
                        isUnicode = false;
                        curstr = new StringBuilder();
                    }
                    byte2 = byte1;
                    byte1 = data[i];
                }
                data = null;
                return true;
            });
            phandle.Dispose();
            CallSearchFinished(processname);
        }
        public void Searchingjavawstrings(string processname, string searchtext)
        {
            try
            {
                byte[] text = Encoding.Default.GetBytes(searchtext);
                ProcessHandle phandle;
                int count = 0;
                int totalstrings = 0;
                int minsize = 4;
                bool unicode = true;
                bool opt_priv = true; 
                bool opt_img = false;
                bool opt_map = true;
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
                            isUnicode = true;
                        }
                        else if (unicode &&
                            data[i] == 0 && IsChar(byte1) && IsChar(byte2) && curstr.Length < minsize)
                        {
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
                    data = null;
                    return true;
                });
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
