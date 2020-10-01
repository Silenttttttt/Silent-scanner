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
        private bool explorersearched = false;
        private List<FileInfo> files;
        private string[,] finalresults = new string[10, 10000];

        //  List<int> listainte = new List<int>();
        //   List<IntPtr> listeeea = new List<IntPtr>();
        private string ipaddress;

        private bool javawsearched = false;
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
        //  int stringsexplorerfound = 0; 0 2
        //    int currentexplorersearch = 0; 0 1
        //    int currentjavawsearch = 0;  1 1
        //    int totaljavawsearch = 0;  1 0
        //    int stringsjavawfound = 0; 1 2
        //    int stringssvchostfound = 0; 2 2
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
                if (ln.ToLower().Contains("antiafk") || ln.ToLower().Contains("antiblind") || ln.ToLower().Contains("anticactus") || ln.ToLower().Contains("antiknockback") || ln.ToLower().Contains("antiwaterpush") || ln.ToLower().Contains("antiwobble") || ln.ToLower().Contains("autoarmor") || ln.ToLower().Contains("autobuild") || ln.ToLower().Contains("autoeat") || ln.ToLower().Contains("autoleave") || ln.ToLower().Contains("automine") || ln.ToLower().Contains("autopotion") || ln.ToLower().Contains("autoreconnect") || ln.ToLower().Contains("autorespawn") || ln.ToLower().Contains("autosign") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("antispam") || ln.ToLower().Contains("autosword") || ln.ToLower().Contains("autodrop") || ln.ToLower().Contains("autofarm") || ln.ToLower().Contains("autofish") || ln.ToLower().Contains("autosprint") || ln.ToLower().Contains("autosteal") || ln.ToLower().Contains("autoswim") || ln.ToLower().Contains("autoswitch") || ln.ToLower().Contains("autotool") || ln.ToLower().Contains("autototem") || ln.ToLower().Contains("autowalk") || ln.ToLower().Contains("basefinder") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("bonemealaura") || ln.ToLower().Contains("bowaimbot") || ln.ToLower().Contains("buildrandom") || ln.ToLower().Contains("bunnyhop") || ln.ToLower().Contains("cameranoclip") || ln.ToLower().Contains("cavefinder") || ln.ToLower().Contains("chattranslator") || ln.ToLower().Contains("chestesp") || ln.ToLower().Contains("clickaura") || ln.ToLower().Contains("crashchest") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("excavator") || ln.ToLower().Contains("extraelytra") || ln.ToLower().Contains("fancychat") || ln.ToLower().Contains("fastbreak") || ln.ToLower().Contains("fastladder") || ln.ToLower().Contains("fastplace") || ln.ToLower().Contains("fightbot") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("forceop") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("fullbright") || ln.ToLower().Contains("glide") || ln.ToLower().Contains("handnoclip") || ln.ToLower().Contains("headroll") || ln.ToLower().Contains("healthtags") || ln.ToLower().Contains("highjump") || ln.ToLower().Contains("infinichat") || ln.ToLower().Contains("instantbunker") || ln.ToLower().Contains("itemesp") || ln.ToLower().Contains("itemgenerator") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("jetpack") || ln.ToLower().Contains("kaboom") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("killauralegit") || ln.ToLower().Contains("killpotion") || ln.ToLower().Contains("masstpa") || ln.ToLower().Contains("mileycyrus") || ln.ToLower().Contains("mobesp") || ln.ToLower().Contains("mobspawnesp") || ln.ToLower().Contains("mountbypass") || ln.ToLower().Contains("multiaura") || ln.ToLower().Contains("nameprotect") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("noclip") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("fly") || ln.ToLower().Contains("nofireoverlay") || ln.ToLower().Contains("nohurtcam") || ln.ToLower().Contains("nooverlay") || ln.ToLower().Contains("nopumpkin") || ln.ToLower().Contains("noslowdown") || ln.ToLower().Contains("noweather") || ln.ToLower().Contains("noweb") || ln.ToLower().Contains("nuker") || ln.ToLower().Contains("nukerlegit") || ln.ToLower().Contains("panic") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("playeresp") || ln.ToLower().Contains("playerfinder") || ln.ToLower().Contains("potionsaver") || ln.ToLower().Contains("prophuntesp") || ln.ToLower().Contains("radar") || ln.ToLower().Contains("rainbowui") || ln.ToLower().Contains("reach") || ln.ToLower().Contains("remoteview") || ln.ToLower().Contains("safewalk") || ln.ToLower().Contains("scaffoldwalk") || ln.ToLower().Contains("servercrasher") || ln.ToLower().Contains("skinderp") || ln.ToLower().Contains("speedhack") || ln.ToLower().Contains("speednuker") || ln.ToLower().Contains("timer") || ln.ToLower().Contains("toomanyhax") || ln.ToLower().Contains("tp-aura") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("triggerbot") || ln.ToLower().Contains("trollpotion") || ln.ToLower().Contains("truesight") || ln.ToLower().Contains("tunneller") || ln.ToLower().Contains("x-ray") || ln.ToLower().Contains("hwid.exe") || ln.ToLower().Contains("clear strings.jar") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("2535.dll") || ln.ToLower().Contains("system_recording.dll") || ln.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || ln.ToLower().Contains("removestring.exe") || ln.ToLower().Contains("remover.exe") || ln.ToLower().Contains("ts cleaner.bat") || ln.ToLower().Contains("dotnetaanmemory.dll") || ln.ToLower().Contains("420 v0.3.exe") || ln.ToLower().Contains("clicker.exe") || ln.ToLower().Contains("purityx") || ln.ToLower().Contains("purityx.exe") || ln.ToLower().Contains("3ms.exe") || ln.ToLower().Contains("inject_client.exe") || ln.ToLower().Contains("apollocracked.exe") || ln.ToLower().Contains("azranRemover.jar") || ln.ToLower().Contains("me.tojatta.clicker.ui.cl") || ln.ToLower().Contains("brplz is cute") || ln.ToLower().Contains("main.class") || ln.ToLower().Contains("burger78.exe") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("dewgs.exe") || ln.ToLower().Contains("doggo.exe") || ln.ToLower().Contains("inject client.exe") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("l-clicker.jar") || ln.ToLower().Contains("work.txt.exe") || ln.ToLower().Contains("lsdclicker.xml") || ln.ToLower().Contains("autoclick.exe") || ln.ToLower().Contains("jacobschellman18") || ln.ToLower().Contains("boomy") || ln.ToLower().Contains("XAttr") || ln.ToLower().Contains("xreduszz") || ln.ToLower().Contains("s08yzx.exe") || ln.ToLower().Contains("pvper") || ln.ToLower().Contains("smart clicker v3.0.1.exe") || ln.ToLower().Contains("pause script") || ln.ToLower().Contains("suspend Hotkeys") || ln.ToLower().Contains("_snak3") || ln.ToLower().Contains("terio.jar") || ln.ToLower().Contains("cracked by dinkio") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("aimbot") || ln.ToLower().Contains("anti bot") || ln.ToLower().Contains("auto armor") || ln.ToLower().Contains("auto clicker") || ln.ToLower().Contains("auto totem") || ln.ToLower().Contains("auto weapon") || ln.ToLower().Contains("bow aimbot") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("crystal aura") || ln.ToLower().Contains("hitbox") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("kill aura") || ln.ToLower().Contains("smoothaim") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("exploit") || ln.ToLower().Contains("anti fire") || ln.ToLower().Contains("anti hunger") || ln.ToLower().Contains("franky") || ln.ToLower().Contains("ghosthand") || ln.ToLower().Contains("new chunks") || ln.ToLower().Contains("copsandcrims") || ln.ToLower().Contains("minestrike") || ln.ToLower().Contains("murder") || ln.ToLower().Contains("prophunt") || ln.ToLower().Contains("quakecraft") || ln.ToLower().Contains("sneakyassassins") || ln.ToLower().Contains("animations") || ln.ToLower().Contains("anti aim") || ln.ToLower().Contains("anti desync") || ln.ToLower().Contains("anti sound lag") || ln.ToLower().Contains("anti vanish") || ln.ToLower().Contains("auto cheat") || ln.ToLower().Contains("auto disconnect") || ln.ToLower().Contains("auto reconnect") || ln.ToLower().Contains("discord rpc") || ln.ToLower().Contains("fancy chat") || ln.ToLower().Contains("log position") || ln.ToLower().Contains("middle click friend") || ln.ToLower().Contains("no srp") || ln.ToLower().Contains("self destruct") || ln.ToLower().Contains("air jump") || ln.ToLower().Contains("anti hazard") || ln.ToLower().Contains("auto jump") || ln.ToLower().Contains("auto walk") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("click tp") || ln.ToLower().Contains("elytra+") || ln.ToLower().Contains("fast fall") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("high jump") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("levitation control") || ln.ToLower().Contains("long jump") || ln.ToLower().Contains("no push") || ln.ToLower().Contains("no slow") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("riding") || ln.ToLower().Contains("safe walk") || ln.ToLower().Contains("speed") || ln.ToLower().Contains("velocity") || ln.ToLower().Contains("anti afk") || ln.ToLower().Contains("auto eat") || ln.ToLower().Contains("auto eject") || ln.ToLower().Contains("auto farm") || ln.ToLower().Contains("auto fish") || ln.ToLower().Contains("auto mine") || ln.ToLower().Contains("auto tool") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("chest stealer") || ln.ToLower().Contains("fast interact") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("item saver") || ln.ToLower().Contains("liquid interact") || ln.ToLower().Contains("no fall") || ln.ToLower().Contains("no rotate") || ln.ToLower().Contains("scaffold") || ln.ToLower().Contains("skin blinker") || ln.ToLower().Contains("anti blind") || ln.ToLower().Contains("anti overlay") || ln.ToLower().Contains("breadcrumbs") || ln.ToLower().Contains("camera clip") || ln.ToLower().Contains("chams") || ln.ToLower().Contains("clickgui") || ln.ToLower().Contains("crosshair+") || ln.ToLower().Contains("enchant color") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("no render") || ln.ToLower().Contains("storage esp") || ln.ToLower().Contains("tracers") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("wireframe") || ln.ToLower().Contains("nacl_32") || ln.ToLower().Contains("yagami.exe") || ln.ToLower().Contains("l0li-0.2snapshot.exe") || ln.ToLower().Contains("loli client") || ln.ToLower().Contains("loli-hwid.exe") || ln.ToLower().Contains("math lesson 12 bac") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine.exe") || ln.ToLower().Contains("cheatengine681.exe") || ln.ToLower().Contains("speedhack.dll") || ln.ToLower().Contains("speedhack-x86_64.dll") || ln.ToLower().Contains("lua53-64.dll") || ln.ToLower().Contains("monoscript.lua") || ln.ToLower().Contains("speedhack-i386.dll") || ln.ToLower().Contains("jd-gui") || ln.ToLower().Contains("autohot") || ln.ToLower().Contains("autohotkey") || ln.ToLower().Contains(".ahk") || ln.ToLower().Contains("liquidbounce") || ln.ToLower().Contains("impact") || ln.ToLower().Contains("wurst") || ln.ToLower().Contains("huzuni") || ln.ToLower().Contains("wolfram") || ln.ToLower().Contains("sigma") || ln.ToLower().Contains("aristois") || ln.ToLower().Contains("wwe client") || ln.ToLower().Contains("flare") || ln.ToLower().Contains("skillclient") || ln.ToLower().Contains("blazing") || ln.ToLower().Contains("vape v") || ln.ToLower().Contains("flux") || ln.ToLower().Contains("phantom client") || ln.ToLower().Contains("iridium") || ln.ToLower().Contains("weepcraft") || ln.ToLower().Contains("jigsaw") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("hcl client") || ln.ToLower().Contains("omikron") || ln.ToLower().Contains("sallos") || ln.ToLower().Contains("envy client") || ln.ToLower().Contains("matrix client") || ln.ToLower().Contains("nightmare") || ln.ToLower().Contains("luna client") || ln.ToLower().Contains("lina client") || ln.ToLower().Contains("suicide client") || ln.ToLower().Contains("obscure") || ln.ToLower().Contains("tigur") || ln.ToLower().Contains("synergy") || ln.ToLower().Contains("zecrus") || ln.ToLower().Contains("parallaxa") || ln.ToLower().Contains("pandora") || ln.ToLower().Contains("future") || ln.ToLower().Contains("kami client") || ln.ToLower().Contains("inertia") || ln.ToLower().Contains("forgehax") || ln.ToLower().Contains("ares client") || ln.ToLower().Contains("rusherhack") || ln.ToLower().Contains("hack") || ln.ToLower().Contains("salhack") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("backdoor") || ln.ToLower().Contains("clicker") || ln.ToLower().Contains("104.22.37.186") || ln.ToLower().Contains("74.91.125.194") || ln.ToLower().Contains("144.217.241.181") || ln.ToLower().Contains("142.44.246.31"))
                {
                    ln.Replace("name", "");
                    ln.Replace("selectedProfile", "");
                    ln.Replace("lastVersionId", "");
                    ln.Replace("type", "");
                    ln.Replace("javaDir", "");
                    possiblecheatslauncherlist.Add(" Possible cheat on launcher: " + ln + " ");
                }
                counter++;
            }

            file.Close();

            minecraftnames = liste.ToArray();
            possiblecheatsonlauncher = String.Join(", ", possiblecheatslauncherlist.ToArray());
            minecraftnamesjoin = String.Join("-", minecraftnames);

            ChangeLabelnine("Usernames: " + String.Join(", ", minecraftnames));
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
            var di = new DirectoryInfo(directory);
            var fs = di.EnumerateFiles("*.*", SearchOption.TopDirectoryOnly);

            List<string> list = new List<string>();
            files.AddRange(fs);
            for (int i = 0; files.Count > i; i++)
            {
                if (!files[i].FullName.ToLower().Contains("ProcessHacker") && files[i].FullName.ToLower().Contains("x-ray") || files[i].FullName.ToLower().Contains("hwid.exe") || files[i].FullName.ToLower().Contains("clear strings.jar") || files[i].FullName.ToLower().Contains("jnativehook") || files[i].FullName.ToLower().Contains("2535.dll") || files[i].FullName.ToLower().Contains("system_recording.dll") || files[i].FullName.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || files[i].FullName.ToLower().Contains("removestring.exe") || files[i].FullName.ToLower().Contains("remover.exe") || files[i].FullName.ToLower().Contains("ts cleaner.bat") || files[i].FullName.ToLower().Contains("dotnetaanmemory.dll") || files[i].FullName.ToLower().Contains("420 v0.3.exe") || files[i].FullName.ToLower().Contains("clicker.exe") || files[i].FullName.ToLower().Contains("purityx") || files[i].FullName.ToLower().Contains("purityx.exe") || files[i].FullName.ToLower().Contains("3ms.exe") || files[i].FullName.ToLower().Contains("inject_client.exe") || files[i].FullName.ToLower().Contains("apollocracked.exe") || files[i].FullName.ToLower().Contains("azranRemover.jar") || files[i].FullName.ToLower().Contains("me.tojatta.clicker.ui.cl") || files[i].FullName.ToLower().Contains("brplz is cute") || files[i].FullName.ToLower().Contains("main.class") || files[i].FullName.ToLower().Contains("burger78.exe") || files[i].FullName.ToLower().Contains("autoclicker") || files[i].FullName.ToLower().Contains("dewgs.exe") || files[i].FullName.ToLower().Contains("doggo.exe") || files[i].FullName.ToLower().Contains("inject client.exe") || files[i].FullName.ToLower().Contains("jnativehook") || files[i].FullName.ToLower().Contains("l-clicker.jar") || files[i].FullName.ToLower().Contains("work.txt.exe") || files[i].FullName.ToLower().Contains("lsdclicker.xml") || files[i].FullName.ToLower().Contains("autoclick.exe") || files[i].FullName.ToLower().Contains("jacobschellman18") || files[i].FullName.ToLower().Contains("boomy") || files[i].FullName.ToLower().Contains("XAttr") || files[i].FullName.ToLower().Contains("xreduszz") || files[i].FullName.ToLower().Contains("s08yzx.exe") || files[i].FullName.ToLower().Contains("pvper") || files[i].FullName.ToLower().Contains("smart clicker v3.0.1.exe") || files[i].FullName.ToLower().Contains("pause script") || files[i].FullName.ToLower().Contains("suspend Hotkeys") || files[i].FullName.ToLower().Contains("_snak3") || files[i].FullName.ToLower().Contains("terio.jar") || files[i].FullName.ToLower().Contains("cracked by dinkio") || files[i].FullName.ToLower().Contains("nacl_32") || files[i].FullName.ToLower().Contains("yagami.exe") || files[i].FullName.ToLower().Contains("l0li-0.2snapshot.exe") || files[i].FullName.ToLower().Contains("loli client") || files[i].FullName.ToLower().Contains("loli-hwid.exe") || files[i].FullName.ToLower().Contains("math lesson 12 bac") || files[i].FullName.ToLower().Contains("autoclicker") || files[i].FullName.ToLower().Contains("cheatengine") || files[i].FullName.ToLower().Contains("cheatengine") || files[i].FullName.ToLower().Contains("cheatengine.exe") || files[i].FullName.ToLower().Contains("cheatengine681.exe") || files[i].FullName.ToLower().Contains("speedhack.dll") || files[i].FullName.ToLower().Contains("speedhack-x86_64.dll") || files[i].FullName.ToLower().Contains("lua53-64.dll") || files[i].FullName.ToLower().Contains("monoscript.lua") || files[i].FullName.ToLower().Contains("speedhack-i386.dll") || files[i].FullName.ToLower().Contains("jd-gui") || files[i].FullName.ToLower().Contains("autohot") || files[i].FullName.ToLower().Contains("autohotkey") || files[i].FullName.ToLower().Contains(".ahk") || files[i].FullName.ToLower().Contains("liquidbounce") || files[i].FullName.ToLower().Contains("impact client") || files[i].FullName.ToLower().Contains("wurst") || files[i].FullName.ToLower().Contains("huzuni") || files[i].FullName.ToLower().Contains("wolfram") || files[i].FullName.ToLower().Contains("sigma") || files[i].FullName.ToLower().Contains("aristois") || files[i].FullName.ToLower().Contains("wwe client") || files[i].FullName.ToLower().Contains("flare") || files[i].FullName.ToLower().Contains("skillclient") || files[i].FullName.ToLower().Contains("blazing") || files[i].FullName.ToLower().Contains("vape v") || files[i].FullName.ToLower().Contains("flux") || files[i].FullName.ToLower().Contains("phantom client") || files[i].FullName.ToLower().Contains("iridium") || files[i].FullName.ToLower().Contains("weepcraft") || files[i].FullName.ToLower().Contains("jigsaw") || files[i].FullName.ToLower().Contains("autoclicker") || files[i].FullName.ToLower().Contains("hcl client") || files[i].FullName.ToLower().Contains("omikron") || files[i].FullName.ToLower().Contains("sallos") || files[i].FullName.ToLower().Contains("envy client") || files[i].FullName.ToLower().Contains("matrix client") || files[i].FullName.ToLower().Contains("nightmare") || files[i].FullName.ToLower().Contains("luna client") || files[i].FullName.ToLower().Contains("lina client") || files[i].FullName.ToLower().Contains("suicide client") || files[i].FullName.ToLower().Contains("obscure") || files[i].FullName.ToLower().Contains("tigur") || files[i].FullName.ToLower().Contains("synergy") || files[i].FullName.ToLower().Contains("zecrus") || files[i].FullName.ToLower().Contains("parallaxa") || files[i].FullName.ToLower().Contains("pandora") || files[i].FullName.ToLower().Contains("future") || files[i].FullName.ToLower().Contains("kami client") || files[i].FullName.ToLower().Contains("inertia") || files[i].FullName.ToLower().Contains("forgehax") || files[i].FullName.ToLower().Contains("ares client") || files[i].FullName.ToLower().Contains("rusherhack") || files[i].FullName.ToLower().Contains("hack") || files[i].FullName.ToLower().Contains("salhack") || files[i].FullName.ToLower().Contains("baritone") || files[i].FullName.ToLower().Contains("backdoor") || files[i].FullName.ToLower().Contains("clicker"))
                {
                    list.Add(files[i].FullName);
                    results = list.ToArray();
                }
            }
            var directories = di.GetDirectories();
            try
            {
                if (results[results.Length - 1] != null && lastfile != results[results.Length - 1])
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    //       AppendTextBoxtoo("£filesearch$" + time + " " + results[results.Length - 1]);// + @"\" + files[files.Count - 1].ToString(), 2);
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
                    if (results[i].Contains("javaw$"))
                    {
                        results[i] = results[i].Replace("javaw$", "");
                        finalresults[0, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("explorer$"))
                    {
                        results[i] = results[i].Replace("explorer$", "");
                        finalresults[1, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("svchost$"))
                    {
                        results[i] = results[i].Replace("svchost$", "");
                        finalresults[2, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("filesearch$"))
                    {
                        results[i] = results[i].Replace("filesearch$", "");
                        finalresults[3, allstuff] = results[i];
                        allstuff++;
                    }
                    else if (results[i].Contains("logfile$"))
                    {
                        results[i] = results[i].Replace("logfile$", "");
                        finalresults[4, allstuff] = results[i];
                        allstuff++;
                    }
                    else
                    {
                        finalresults[5, allstuff] = results[i];
                        allstuff++;
                    }
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
                try
                {
                    StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\logs\latest.log");
                    while ((ln = filee.ReadLine()) != null)
                    {
                        if (ln.ToLower().Contains("antiafk") || ln.ToLower().Contains("antiblind") || ln.ToLower().Contains("anticactus") || ln.ToLower().Contains("antiknockback") || ln.ToLower().Contains("antiwaterpush") || ln.ToLower().Contains("antiwobble") || ln.ToLower().Contains("autoarmor") || ln.ToLower().Contains("autobuild") || ln.ToLower().Contains("autoeat") || ln.ToLower().Contains("autoleave") || ln.ToLower().Contains("automine") || ln.ToLower().Contains("autopotion") || ln.ToLower().Contains("autoreconnect") || ln.ToLower().Contains("autorespawn") || ln.ToLower().Contains("autosign") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("antispam") || ln.ToLower().Contains("autosword") || ln.ToLower().Contains("autodrop") || ln.ToLower().Contains("autofarm") || ln.ToLower().Contains("autofish") || ln.ToLower().Contains("autosprint") || ln.ToLower().Contains("autosteal") || ln.ToLower().Contains("autoswim") || ln.ToLower().Contains("autoswitch") || ln.ToLower().Contains("autotool") || ln.ToLower().Contains("autototem") || ln.ToLower().Contains("autowalk") || ln.ToLower().Contains("basefinder") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("bonemealaura") || ln.ToLower().Contains("bowaimbot") || ln.ToLower().Contains("buildrandom") || ln.ToLower().Contains("bunnyhop") || ln.ToLower().Contains("cameranoclip") || ln.ToLower().Contains("cavefinder") || ln.ToLower().Contains("chattranslator") || ln.ToLower().Contains("chestesp") || ln.ToLower().Contains("clickaura") || ln.ToLower().Contains("crashchest") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("excavator") || ln.ToLower().Contains("extraelytra") || ln.ToLower().Contains("fancychat") || ln.ToLower().Contains("fastbreak") || ln.ToLower().Contains("fastladder") || ln.ToLower().Contains("fastplace") || ln.ToLower().Contains("fightbot") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("forceop") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("fullbright") || ln.ToLower().Contains("glide") || ln.ToLower().Contains("handnoclip") || ln.ToLower().Contains("headroll") || ln.ToLower().Contains("healthtags") || ln.ToLower().Contains("highjump") || ln.ToLower().Contains("infinichat") || ln.ToLower().Contains("instantbunker") || ln.ToLower().Contains("itemesp") || ln.ToLower().Contains("itemgenerator") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("jetpack") || ln.ToLower().Contains("kaboom") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("killauralegit") || ln.ToLower().Contains("killpotion") || ln.ToLower().Contains("masstpa") || ln.ToLower().Contains("mileycyrus") || ln.ToLower().Contains("mobesp") || ln.ToLower().Contains("mobspawnesp") || ln.ToLower().Contains("mountbypass") || ln.ToLower().Contains("multiaura") || ln.ToLower().Contains("nameprotect") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("noclip") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("fly") || ln.ToLower().Contains("nofireoverlay") || ln.ToLower().Contains("nohurtcam") || ln.ToLower().Contains("nooverlay") || ln.ToLower().Contains("nopumpkin") || ln.ToLower().Contains("noslowdown") || ln.ToLower().Contains("noweather") || ln.ToLower().Contains("noweb") || ln.ToLower().Contains("nuker") || ln.ToLower().Contains("nukerlegit") || ln.ToLower().Contains("panic") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("playeresp") || ln.ToLower().Contains("playerfinder") || ln.ToLower().Contains("potionsaver") || ln.ToLower().Contains("prophuntesp") || ln.ToLower().Contains("radar") || ln.ToLower().Contains("rainbowui") || ln.ToLower().Contains("reach") || ln.ToLower().Contains("remoteview") || ln.ToLower().Contains("safewalk") || ln.ToLower().Contains("scaffoldwalk") || ln.ToLower().Contains("servercrasher") || ln.ToLower().Contains("skinderp") || ln.ToLower().Contains("speedhack") || ln.ToLower().Contains("speednuker") || ln.ToLower().Contains("timer") || ln.ToLower().Contains("toomanyhax") || ln.ToLower().Contains("tp-aura") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("triggerbot") || ln.ToLower().Contains("trollpotion") || ln.ToLower().Contains("truesight") || ln.ToLower().Contains("tunneller") || ln.ToLower().Contains("x-ray") || ln.ToLower().Contains("hwid.exe") || ln.ToLower().Contains("clear strings.jar") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("2535.dll") || ln.ToLower().Contains("system_recording.dll") || ln.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || ln.ToLower().Contains("removestring.exe") || ln.ToLower().Contains("remover.exe") || ln.ToLower().Contains("ts cleaner.bat") || ln.ToLower().Contains("dotnetaanmemory.dll") || ln.ToLower().Contains("420 v0.3.exe") || ln.ToLower().Contains("clicker.exe") || ln.ToLower().Contains("purityx") || ln.ToLower().Contains("purityx.exe") || ln.ToLower().Contains("3ms.exe") || ln.ToLower().Contains("inject_client.exe") || ln.ToLower().Contains("apollocracked.exe") || ln.ToLower().Contains("azranRemover.jar") || ln.ToLower().Contains("me.tojatta.clicker.ui.cl") || ln.ToLower().Contains("brplz is cute") || ln.ToLower().Contains("burger78.exe") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("dewgs.exe") || ln.ToLower().Contains("doggo.exe") || ln.ToLower().Contains("inject client.exe") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("l-clicker.jar") || ln.ToLower().Contains("work.txt.exe") || ln.ToLower().Contains("lsdclicker.xml") || ln.ToLower().Contains("autoclick.exe") || ln.ToLower().Contains("jacobschellman18") || ln.ToLower().Contains("boomy") || ln.ToLower().Contains("XAttr") || ln.ToLower().Contains("xreduszz") || ln.ToLower().Contains("s08yzx.exe") || ln.ToLower().Contains("pvper") || ln.ToLower().Contains("smart clicker v3.0.1.exe") || ln.ToLower().Contains("pause script") || ln.ToLower().Contains("suspend Hotkeys") || ln.ToLower().Contains("_snak3") || ln.ToLower().Contains("terio.jar") || ln.ToLower().Contains("cracked by dinkio") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("aimbot") || ln.ToLower().Contains("anti bot") || ln.ToLower().Contains("auto armor") || ln.ToLower().Contains("auto clicker") || ln.ToLower().Contains("auto totem") || ln.ToLower().Contains("auto weapon") || ln.ToLower().Contains("bow aimbot") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("crystal aura") || ln.ToLower().Contains("hitbox") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("kill aura") || ln.ToLower().Contains("smoothaim") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("exploit") || ln.ToLower().Contains("anti fire") || ln.ToLower().Contains("anti hunger") || ln.ToLower().Contains("franky") || ln.ToLower().Contains("ghosthand") || ln.ToLower().Contains("new chunks") || ln.ToLower().Contains("copsandcrims") || ln.ToLower().Contains("minestrike") || ln.ToLower().Contains("murder") || ln.ToLower().Contains("prophunt") || ln.ToLower().Contains("quakecraft") || ln.ToLower().Contains("sneakyassassins") || ln.ToLower().Contains("animations") || ln.ToLower().Contains("anti aim") || ln.ToLower().Contains("anti desync") || ln.ToLower().Contains("anti sound lag") || ln.ToLower().Contains("anti vanish") || ln.ToLower().Contains("auto cheat") || ln.ToLower().Contains("auto disconnect") || ln.ToLower().Contains("auto reconnect") || ln.ToLower().Contains("discord rpc") || ln.ToLower().Contains("fancy chat") || ln.ToLower().Contains("log position") || ln.ToLower().Contains("middle click friend") || ln.ToLower().Contains("no srp") || ln.ToLower().Contains("self destruct") || ln.ToLower().Contains("air jump") || ln.ToLower().Contains("anti hazard") || ln.ToLower().Contains("auto jump") || ln.ToLower().Contains("auto walk") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("click tp") || ln.ToLower().Contains("elytra+") || ln.ToLower().Contains("fast fall") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("high jump") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("levitation control") || ln.ToLower().Contains("long jump") || ln.ToLower().Contains("no push") || ln.ToLower().Contains("no slow") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("riding") || ln.ToLower().Contains("safe walk") || ln.ToLower().Contains("speed") || ln.ToLower().Contains("anti afk") || ln.ToLower().Contains("auto eat") || ln.ToLower().Contains("auto eject") || ln.ToLower().Contains("auto farm") || ln.ToLower().Contains("auto fish") || ln.ToLower().Contains("auto mine") || ln.ToLower().Contains("auto tool") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("chest stealer") || ln.ToLower().Contains("fast interact") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("item saver") || ln.ToLower().Contains("liquid interact") || ln.ToLower().Contains("no fall") || ln.ToLower().Contains("no rotate") || ln.ToLower().Contains("scaffold") || ln.ToLower().Contains("skin blinker") || ln.ToLower().Contains("anti blind") || ln.ToLower().Contains("anti overlay") || ln.ToLower().Contains("breadcrumbs") || ln.ToLower().Contains("camera clip") || ln.ToLower().Contains("chams") || ln.ToLower().Contains("clickgui") || ln.ToLower().Contains("crosshair+") || ln.ToLower().Contains("enchant color") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("no render") || ln.ToLower().Contains("storage esp") || ln.ToLower().Contains("tracers") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("wireframe") || ln.ToLower().Contains("nacl_32") || ln.ToLower().Contains("yagami.exe") || ln.ToLower().Contains("l0li-0.2snapshot.exe") || ln.ToLower().Contains("loli client") || ln.ToLower().Contains("loli-hwid.exe") || ln.ToLower().Contains("math lesson 12 bac") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine.exe") || ln.ToLower().Contains("cheatengine681.exe") || ln.ToLower().Contains("speedhack.dll") || ln.ToLower().Contains("speedhack-x86_64.dll") || ln.ToLower().Contains("lua53-64.dll") || ln.ToLower().Contains("monoscript.lua") || ln.ToLower().Contains("speedhack-i386.dll") || ln.ToLower().Contains("jd-gui") || ln.ToLower().Contains("autohot") || ln.ToLower().Contains("autohotkey") || ln.ToLower().Contains(".ahk") || ln.ToLower().Contains("liquidbounce") || ln.ToLower().Contains("impact") || ln.ToLower().Contains("wurst") || ln.ToLower().Contains("huzuni") || ln.ToLower().Contains("wolfram") || ln.ToLower().Contains("sigma") || ln.ToLower().Contains("aristois") || ln.ToLower().Contains("wwe client") || ln.ToLower().Contains("flare") || ln.ToLower().Contains("skillclient") || ln.ToLower().Contains("blazing") || ln.ToLower().Contains("vape v") || ln.ToLower().Contains("flux") || ln.ToLower().Contains("phantom client") || ln.ToLower().Contains("iridium") || ln.ToLower().Contains("weepcraft") || ln.ToLower().Contains("jigsaw") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("hcl client") || ln.ToLower().Contains("omikron") || ln.ToLower().Contains("sallos") || ln.ToLower().Contains("envy client") || ln.ToLower().Contains("matrix client") || ln.ToLower().Contains("nightmare") || ln.ToLower().Contains("luna client") || ln.ToLower().Contains("lina client") || ln.ToLower().Contains("suicide client") || ln.ToLower().Contains("obscure") || ln.ToLower().Contains("tigur") || ln.ToLower().Contains("synergy") || ln.ToLower().Contains("zecrus") || ln.ToLower().Contains("parallaxa") || ln.ToLower().Contains("pandora") || ln.ToLower().Contains("future client") || ln.ToLower().Contains("kami client") || ln.ToLower().Contains("inertia") || ln.ToLower().Contains("forgehax") || ln.ToLower().Contains("ares client") || ln.ToLower().Contains("rusherhack") || ln.ToLower().Contains("salhack") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("backdoor") || ln.ToLower().Contains("clicker") || ln.ToLower().Contains("104.22.37.186") || ln.ToLower().Contains("74.91.125.194") || ln.ToLower().Contains("144.217.241.181") || ln.ToLower().Contains("142.44.246.31"))
                        {
                            possiblecheatslogfile.Add(ln);
                        }
                        counteer++;
                    }

                    filee.Close();
                }
                catch { }
                /*
                                for (int zeze = 0; possiblecheatslogfile.Count > zeze; zeze++)
                                {
                                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                    AppendTextBoxtoo("£logfile$" + time + " Possible cheat on log file: " + possiblecheatslogfile[zeze]);
                                }
                                //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
                                string[] currentstringsearch = new string[] { "hwid.exe", "clear strings.jar", "jnativehook", "2535.dll", "system_recording.dll", "microsoft.visualBasic.powerpacks.dll", "removestring.exe", "remover.exe", "ts cleaner.bat", "dotnetaanmemory.dll", "420 v0.3.exe", "clicker.exe", "purityx", "purityx.exe", "3ms.exe", "inject_client.exe", "apollocracked.exe", "azranRemover.jar", "me.tojatta.clicker.ui.cl", "brplz is cute", "main.class", "burger78.exe", "autoclicker", "dewgs.exe", "doggo.exe", "inject client.exe", "jnativehook", "l-clicker.jar", "work.txt.exe", "lsdclicker.xml", "autoclick.exe", "jacobschellman18", "boomy", "XAttr", "xreduszz", "s08yzx.exe", "pvper", "smart clicker v3.0.1.exe", "pause script", "suspend Hotkeys", "_snak3", "terio.jar", "cracked by dinkio", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
                                stringsearch[0,2] += currentstringsearch.Length - 1;
                                for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
                                {
                                    byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                                    DoSearch("explorer", currentstringsearch[stringsearchin]);

                                    for (int gg = 0; listaint.Count > gg; gg++)
                                    {
                                        string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                        AppendTextBoxtoo("£explorer$" + time + " Possible Explorer string found " + currentstringsearch[stringsearchin]);
                                        stringsearch[0,2]++;
                                    }
                                    listeea.Clear();
                                    listaint.Clear();
                                    ChangeLabeltoo(stringsearch[0,2] + " Explorer strings found  " + stringsearch[0,1] + "\\" + stringsearch[0,2]);
                                    ChangeLabelseven(currentstringsearch[stringsearchin]);
                                    stringsearch[0,1]++;
                                }*/

                try
                {
                    Searcher.Searchboi("explorer", "");

                    //   if (File.Exists(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer"))
                    //      {
                    //           StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                    stringsearch[0, 0] = searchstringsexplorer.Count;//Convert.ToInt32(new FileInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Length);
                                                                     // while ((ln = filee.ReadLine()) != null)
                    string[] danked = new string[searchstringsexplorer.Count];
                    for (int eeed = 0; eeed < searchstringsexplorer.Count; eeed++)
                    {
                        danked[eeed] = searchstringsexplorer.ToArray()[eeed].ToLower();
                    }
                    for (int helpme = 0; helpme < searchstringsexplorer.Count; helpme++)
                    {
                        ln = danked[helpme];

                        if ((ln.Length > 4 && ln.Length < 300) && ln.Contains("toomanyhax") || ln.Contains("kill-aura") || ln.Contains("triggerbot") || ln.Contains("x-ray") || ln.Contains("hwid.exe") || ln.Contains("clear strings.jar") || ln.Contains("jnativehook") || ln.Contains("2535.dll") || ln.Contains("system_recording.dll") || ln.Contains("microsoft.visualBasic.powerpacks.dll") || ln.Contains("removestring.exe") || ln.Contains("remover.exe") || ln.Contains("ts cleaner.bat") || ln.Contains("dotnetaanmemory.dll") || ln.Contains("420 v0.3.exe") || ln.Contains("clicker.exe") || ln.Contains("purityx") || ln.Contains("purityx.exe") || ln.Contains("3ms.exe") || ln.Contains("inject_client.exe") || ln.Contains("apollocracked.exe") || ln.Contains("azranRemover.jar") || ln.Contains("me.tojatta.clicker.ui.cl") || ln.Contains("brplz is cute") || ln.Contains("burger78.exe") || ln.Contains("autoclicker") || ln.Contains("dewgs.exe") || ln.Contains("doggo.exe") || ln.Contains("inject client.exe") || ln.Contains("jnativehook") || ln.Contains("l-clicker.jar") || ln.Contains("work.txt.exe") || ln.Contains("lsdclicker.xml") || ln.Contains("autoclick.exe") || ln.Contains("jacobschellman18") || ln.Contains("boomy") || ln.Contains("XAttr") || ln.Contains("xreduszz") || ln.Contains("s08yzx.exe") || ln.Contains("pvper") || ln.Contains("smart clicker v3.0.1.exe") || ln.Contains("pause script") || ln.Contains("suspend Hotkeys") || ln.Contains("_snak3") || ln.Contains("terio.jar") || ln.Contains("cracked by dinkio") || ln.Contains("nofall") || ln.Contains("aimbot") || ln.Contains("killaura") || ln.Contains("nacl_32") || ln.Contains("yagami.exe") || ln.Contains("l0li-0.2snapshot.exe") || ln.Contains("loli client") || ln.Contains("loli-hwid.exe") || ln.Contains("math lesson 12 bac") || ln.Contains("autoclicker") || ln.Contains("cheatengine") || ln.Contains("cheatengine") || ln.Contains("cheatengine.exe") || ln.Contains("cheatengine681.exe") || ln.Contains("speedhack.dll") || ln.Contains("speedhack-x86_64.dll") || ln.Contains("lua53-64.dll") || ln.Contains("monoscript.lua") || ln.Contains("speedhack-i386.dll") || ln.Contains("jd-gui") || ln.Contains("autohot") || ln.Contains("autohotkey") || ln.Contains(".ahk") || ln.Contains("liquidbounce") || ln.Contains("impact client") || ln.Contains("wurst") || ln.Contains("huzuni") || ln.Contains("wolfram") || ln.Contains("sigma") || ln.Contains("aristois") || ln.Contains("wwe client") || ln.Contains("flare") || ln.Contains("skillclient") || ln.Contains("blazing") || ln.Contains("vape v") || ln.Contains("flux") || ln.Contains("phantom client") || ln.Contains("iridium") || ln.Contains("weepcraft") || ln.Contains("jigsaw") || ln.Contains("autoclicker") || ln.Contains("hcl client") || ln.Contains("omikron") || ln.Contains("sallos") || ln.Contains("envy client") || ln.Contains("matrix client") || ln.Contains("nightmare") || ln.Contains("luna client") || ln.Contains("lina client") || ln.Contains("suicide client") || ln.Contains("obscure") || ln.Contains("tigur") || ln.Contains("synergy") || ln.Contains("zecrus") || ln.Contains("parallaxa") || ln.Contains("pandora") || ln.Contains("future client") || ln.Contains("kami client") || ln.Contains("inertia") || ln.Contains("forgehax") || ln.Contains("ares client") || ln.Contains("rusherhack") || ln.Contains("salhack") || ln.Contains("baritone") || ln.Contains("backdoor") || ln.Contains("clicker") || ln.Contains("104.22.37.186") || ln.Contains("74.91.125.194") || ln.Contains("144.217.241.181") || ln.Contains("142.44.246.31"))
                        {
                            if ((ln.ToLower().Length > 4 && ln.ToLower().Length < 300) && ln.ToLower().Contains("toomanyhax") || ln.ToLower().Contains("kill-aura") || ln.ToLower().Contains("triggerbot") || ln.ToLower().Contains("x-ray") || ln.ToLower().Contains("hwid.exe") || ln.ToLower().Contains("clear strings.jar") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("2535.dll") || ln.ToLower().Contains("system_recording.dll") || ln.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || ln.ToLower().Contains("removestring.exe") || ln.ToLower().Contains("remover.exe") || ln.ToLower().Contains("ts cleaner.bat") || ln.ToLower().Contains("dotnetaanmemory.dll") || ln.ToLower().Contains("420 v0.3.exe") || ln.ToLower().Contains("clicker.exe") || ln.ToLower().Contains("purityx") || ln.ToLower().Contains("purityx.exe") || ln.ToLower().Contains("3ms.exe") || ln.ToLower().Contains("inject_client.exe") || ln.ToLower().Contains("apollocracked.exe") || ln.ToLower().Contains("azranRemover.jar") || ln.ToLower().Contains("me.tojatta.clicker.ui.cl") || ln.ToLower().Contains("brplz is cute") || ln.ToLower().Contains("burger78.exe") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("dewgs.exe") || ln.ToLower().Contains("doggo.exe") || ln.ToLower().Contains("inject client.exe") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("l-clicker.jar") || ln.ToLower().Contains("work.txt.exe") || ln.ToLower().Contains("lsdclicker.xml") || ln.ToLower().Contains("autoclick.exe") || ln.ToLower().Contains("jacobschellman18") || ln.ToLower().Contains("boomy") || ln.ToLower().Contains("XAttr") || ln.ToLower().Contains("xreduszz") || ln.ToLower().Contains("s08yzx.exe") || ln.ToLower().Contains("pvper") || ln.ToLower().Contains("smart clicker v3.0.1.exe") || ln.ToLower().Contains("pause script") || ln.ToLower().Contains("suspend Hotkeys") || ln.ToLower().Contains("_snak3") || ln.ToLower().Contains("terio.jar") || ln.ToLower().Contains("cracked by dinkio") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("aimbot") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("nacl_32") || ln.ToLower().Contains("yagami.exe") || ln.ToLower().Contains("l0li-0.2snapshot.exe") || ln.ToLower().Contains("loli client") || ln.ToLower().Contains("loli-hwid.exe") || ln.ToLower().Contains("math lesson 12 bac") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine.exe") || ln.ToLower().Contains("cheatengine681.exe") || ln.ToLower().Contains("speedhack.dll") || ln.ToLower().Contains("speedhack-x86_64.dll") || ln.ToLower().Contains("lua53-64.dll") || ln.ToLower().Contains("monoscript.lua") || ln.ToLower().Contains("speedhack-i386.dll") || ln.ToLower().Contains("jd-gui") || ln.ToLower().Contains("autohot") || ln.ToLower().Contains("autohotkey") || ln.ToLower().Contains(".ahk") || ln.ToLower().Contains("liquidbounce") || ln.ToLower().Contains("impact client") || ln.ToLower().Contains("wurst") || ln.ToLower().Contains("huzuni") || ln.ToLower().Contains("wolfram") || ln.ToLower().Contains("sigma") || ln.ToLower().Contains("aristois") || ln.ToLower().Contains("wwe client") || ln.ToLower().Contains("flare") || ln.ToLower().Contains("skillclient") || ln.ToLower().Contains("blazing") || ln.ToLower().Contains("vape v") || ln.ToLower().Contains("flux") || ln.ToLower().Contains("phantom client") || ln.ToLower().Contains("iridium") || ln.ToLower().Contains("weepcraft") || ln.ToLower().Contains("jigsaw") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("hcl client") || ln.ToLower().Contains("omikron") || ln.ToLower().Contains("sallos") || ln.ToLower().Contains("envy client") || ln.ToLower().Contains("matrix client") || ln.ToLower().Contains("nightmare") || ln.ToLower().Contains("luna client") || ln.ToLower().Contains("lina client") || ln.ToLower().Contains("suicide client") || ln.ToLower().Contains("obscure") || ln.ToLower().Contains("tigur") || ln.ToLower().Contains("synergy") || ln.ToLower().Contains("zecrus") || ln.ToLower().Contains("parallaxa") || ln.ToLower().Contains("pandora") || ln.ToLower().Contains("future client") || ln.ToLower().Contains("kami client") || ln.ToLower().Contains("inertia client") || ln.ToLower().Contains("forgehax") || ln.ToLower().Contains("ares client") || ln.ToLower().Contains("rusherhack") || ln.ToLower().Contains("salhack") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("backdoor") || ln.ToLower().Contains("clicker") || ln.ToLower().Contains("104.22.37.186") || ln.ToLower().Contains("74.91.125.194") || ln.ToLower().Contains("144.217.241.181") || ln.ToLower().Contains("142.44.246.31"))
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£explorer$" + time + " Possible Explorer string found " + ln);
                            }
                        }
                        ChangeLabeltoo(stringsearch[0, 1] + @"\" + stringsearch[0, 0]);
                        stringsearch[0, 1]++;
                        // counteer++;// HERE BOI
                        /*                List<int> intarrayer = new List<int>();
                                        List<string> stringarrayer = new List<string>();
                                        string[] currentstringsearch = new string[] { "antiafk", "antiblind", "anticactus", "antiknockback", "antiwaterpush", "antiwobble", "autoarmor", "autobuild", "autoeat", "autoleave", "automine", "autopotion", "autoreconnect", "autorespawn", "autosign", "autosoup", "antispam", "autosword", "autodrop", "autofarm", "autofish", "autosprint", "autosteal", "autoswim", "autoswitch", "autotool", "autototem", "autowalk", "basefinder", "blink", "boatfly", "bonemealaura", "bowaimbot", "buildrandom", "bunnyhop", "cameranoclip", "cavefinder", "chattranslator", "chestesp", "clickaura", "crashchest", "criticals", "", "excavator", "extraelytra", "fancychat", "fastbreak", "fastladder", "fastplace", "fightbot", "flight", "forceop", "freecam", "fullbright", "glide", "handnoclip", "headroll", "healthtags", "highjump", "infinichat", "instantbunker", "itemesp", "itemgenerator", "jesus", "jetpack", "kaboom", "killaura", "killauralegit", "killpotion", "masstpa", "mileycyrus", "mobesp", "mobspawnesp", "mountbypass", "multiaura", "nameprotect", "nametags", "noclip", "nofall", "nofireoverlay", "nohurtcam", "nooverlay", "nopumpkin", "noslowdown", "noweather", "noweb", "nuker", "nukerlegit", "panic", "parkour", "playeresp", "playerfinder", "potionsaver", "prophuntesp", "radar", "rainbowui", "reach", "remoteview", "safewalk", "scaffoldwalk", "servercrasher", "skinderp", "speedhack", "speednuker", "toomanyhax", "tp-aura", "trajectories", "triggerbot", "trollpotion", "truesight", "tunneller", "x-ray", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
                                        for (int dankdank = 0; dankdank < currentstringsearch.Length; dankdank++)
                                        {
                                         int curvalue = FindInFile(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", currentstringsearch[dankdank]);
                                            if (curvalue != 0)
                                            {
                                                intarrayer.Add(curvalue);
                                            }
                                        }
                                    //    StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                                        //   stringsearch[0, 0] = Convert.ToInt32(new FileInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Length);
                                        for (int d = 0; d < intarrayer.Count; d++)
                                        {
                                            //    string line =
                                            var lineNumber = File.ReadLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Take(intarrayer[d]).Count(c => c.ToCharArray()[0] == '\n') + 1;//File.ReadLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Take(intarrayer[d]).Count(c => c == '\n') + 1;
                                            stringarrayer.Add(File.OpenRead(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Read(null,lineNumber, 200).ToString());//(filee.Read(intarrayer[d]).ToString);//.Skip(intarrayer[d]).ToString());
                                        }
                                        for (int h = 0; h < stringarrayer.Count; h++)
                                        {
                                            string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                            AppendTextBoxtoo("£explorer$" + time + " " + stringarrayer[h]);
                                        }
                                        //   filee.Close();
                                        // }
                                    }*/
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
                if (!Searcher.searchfinished)
                {
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

        private void worker_DoSearch(object sender, DoWorkEventArgs e)
        {
            /*

                        // currentplace = 0;

                        //                               if ((char)buffer[i] != '\0' && (char)buffer[i] == 'a' || (char)buffer[i] == 'b' || (char)buffer[i] == 'c' || (char)buffer[i] == 'd' || (char)buffer[i] == 'e' || (char)buffer[i] == 'f' || (char)buffer[i] == 'g' || (char)buffer[i] == 'h' || (char)buffer[i] == 'i' || (char)buffer[i] == 'j' || (char)buffer[i] == 'k' || (char)buffer[i] == 'l' || (char)buffer[i] == 'm' || (char)buffer[i] == 'n' || (char)buffer[i] == 'o' || (char)buffer[i] == 'p' || (char)buffer[i] == 'q' || (char)buffer[i] == 'r' || (char)buffer[i] == 's' || (char)buffer[i] == 't' || (char)buffer[i] == 'u' || (char)buffer[i] == 'v' || (char)buffer[i] == 'w' || (char)buffer[i] == 'x' || (char)buffer[i] == 'y' || (char)buffer[i] == 'z' || (char)buffer[i] == '?' || (char)buffer[i] == ':' || (char)buffer[i] == '(' || (char)buffer[i] == ')' || (char)buffer[i] == '*' || (char)buffer[i] == '&' || (char)buffer[i] == '^' || (char)buffer[i] == '%' || (char)buffer[i] == '$' || (char)buffer[i] == '#' || (char)buffer[i] == '@' || (char)buffer[i] == '!' || (char)buffer[i] == 'A' || (char)buffer[i] == 'B' || (char)buffer[i] == 'C' || (char)buffer[i] == 'D' || (char)buffer[i] == 'E' || (char)buffer[i] == 'F' || (char)buffer[i] == 'G' || (char)buffer[i] == 'H' || (char)buffer[i] == 'I' || (char)buffer[i] == 'J' || (char)buffer[i] == 'K' || (char)buffer[i] == 'L' || (char)buffer[i] == 'M' || (char)buffer[i] == 'N' || (char)buffer[i] == 'O' || (char)buffer[i] == 'P' || (char)buffer[i] == 'Q' || (char)buffer[i] == 'R' || (char)buffer[i] == 'S' || (char)buffer[i] == 'T' || (char)buffer[i] == 'U' || (char)buffer[i] == 'V' || (char)buffer[i] == 'W' || (char)buffer[i] == 'X' || (char)buffer[i] == 'Y' || (char)buffer[i] == 'Z' || (char)buffer[i] == '1' || (char)buffer[i] == '2' || (char)buffer[i] == '3' || (char)buffer[i] == '4' || (char)buffer[i] == '5' || (char)buffer[i] == '6' || (char)buffer[i] == '7' || (char)buffer[i] == '8' || (char)buffer[i] == '9' || (char)buffer[i] == '0' || (char)buffer[i] == '-' || (char)buffer[i] == '_' || (char)buffer[i] == '+' || (char)buffer[i] == '=' || (char)buffer[i] == '`' || (char)buffer[i] == '~' || (char)buffer[i] == '\"' || (char)buffer[i] == '/' || (char)buffer[i] == '\\')

                        string[] currentstringsearch = new string[] { "antiafk", "antiblind", "anticactus", "antiknockback", "antiwaterpush", "antiwobble", "autoarmor", "autobuild", "autoeat", "autoleave", "automine", "autopotion", "autoreconnect", "autorespawn", "autosign", "autosoup", "antispam", "autosword", "autodrop", "autofarm", "autofish", "autosprint", "autosteal", "autoswim", "autoswitch", "autotool", "autototem", "autowalk", "basefinder", "blink", "boatfly", "bonemealaura", "bowaimbot", "buildrandom", "bunnyhop", "cameranoclip", "cavefinder", "chattranslator", "chestesp", "clickaura", "crashchest", "criticals", "", "excavator", "extraelytra", "fancychat", "fastbreak", "fastladder", "fastplace", "fightbot", "flight", "forceop", "freecam", "fullbright", "glide", "handnoclip", "headroll", "healthtags", "highjump", "infinichat", "instantbunker", "itemesp", "itemgenerator", "jesus", "jetpack", "kaboom", "killaura", "killauralegit", "killpotion", "masstpa", "mileycyrus", "mobesp", "mobspawnesp", "mountbypass", "multiaura", "nameprotect", "nametags", "noclip", "nofall", "nofireoverlay", "nohurtcam", "nooverlay", "nopumpkin", "noslowdown", "noweather", "noweb", "nuker", "nukerlegit", "panic", "parkour", "playeresp", "playerfinder", "potionsaver", "prophuntesp", "radar", "rainbowui", "reach", "remoteview", "safewalk", "scaffoldwalk", "servercrasher", "skinderp", "speedhack", "speednuker", "toomanyhax", "tp-aura", "trajectories", "triggerbot", "trollpotion", "truesight", "tunneller", "x-ray", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
                        stringsearch[1, 0] += currentstringsearch.Length - 1;
                        for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
                        {
                            byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                            //    DoSearch("javaw", currentstringsearch[stringsearchin]);

                            for (int gg = 0; listaint.Count > gg; gg++)
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£javaw$" + time + " Possible Javaw string found " + currentstringsearch[stringsearchin]);
                                finalresultslist.Add(time + " Possible Javaw string found " + currentstringsearch[stringsearchin]);
                                stringsearch[0, 2]++;
                            }
                            listeea.Clear();
                            listaint.Clear();
                            ChangeLabel(stringsearch[0, 2] + " Javaw strings found   " + stringsearch[1, 1] + "\\" + stringsearch[1, 0]);
                            ChangeLabelseven(currentstringsearch[stringsearchin]);

                            stringsearch[1, 1]++;
                        }*/
            string ln;
            int counteer = 0;
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
                        ln = searchstringsjavaw[helpme];
                        if (ln.ToLower().Contains("antiafk") || ln.ToLower().Contains("antiblind") || ln.ToLower().Contains("anticactus") || ln.ToLower().Contains("antiknockback") || ln.ToLower().Contains("antiwaterpush") || ln.ToLower().Contains("antiwobble") || ln.ToLower().Contains("autoarmor") || ln.ToLower().Contains("autobuild") || ln.ToLower().Contains("autoeat") || ln.ToLower().Contains("autoleave") || ln.ToLower().Contains("automine") || ln.ToLower().Contains("autopotion") || ln.ToLower().Contains("autoreconnect") || ln.ToLower().Contains("autorespawn") || ln.ToLower().Contains("autosign") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("antispam") || ln.ToLower().Contains("autosword") || ln.ToLower().Contains("autodrop") || ln.ToLower().Contains("autofarm") || ln.ToLower().Contains("autofish") || ln.ToLower().Contains("autosprint") || ln.ToLower().Contains("autosteal") || ln.ToLower().Contains("autoswim") || ln.ToLower().Contains("autoswitch") || ln.ToLower().Contains("autotool") || ln.ToLower().Contains("autototem") || ln.ToLower().Contains("autowalk") || ln.ToLower().Contains("basefinder") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("bonemealaura") || ln.ToLower().Contains("bowaimbot") || ln.ToLower().Contains("buildrandom") || ln.ToLower().Contains("bunnyhop") || ln.ToLower().Contains("cameranoclip") || ln.ToLower().Contains("cavefinder") || ln.ToLower().Contains("chattranslator") || ln.ToLower().Contains("chestesp") || ln.ToLower().Contains("clickaura") || ln.ToLower().Contains("crashchest") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("excavator") || ln.ToLower().Contains("extraelytra") || ln.ToLower().Contains("fancychat") || ln.ToLower().Contains("fastbreak") || ln.ToLower().Contains("fastladder") || ln.ToLower().Contains("fastplace") || ln.ToLower().Contains("fightbot") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("forceop") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("fullbright") || ln.ToLower().Contains("glide") || ln.ToLower().Contains("handnoclip") || ln.ToLower().Contains("headroll") || ln.ToLower().Contains("healthtags") || ln.ToLower().Contains("highjump") || ln.ToLower().Contains("infinichat") || ln.ToLower().Contains("instantbunker") || ln.ToLower().Contains("itemesp") || ln.ToLower().Contains("itemgenerator") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("jetpack") || ln.ToLower().Contains("kaboom") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("killauralegit") || ln.ToLower().Contains("killpotion") || ln.ToLower().Contains("masstpa") || ln.ToLower().Contains("mileycyrus") || ln.ToLower().Contains("mobesp") || ln.ToLower().Contains("mobspawnesp") || ln.ToLower().Contains("mountbypass") || ln.ToLower().Contains("multiaura") || ln.ToLower().Contains("nameprotect") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("noclip") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("fly") || ln.ToLower().Contains("nofireoverlay") || ln.ToLower().Contains("nohurtcam") || ln.ToLower().Contains("nooverlay") || ln.ToLower().Contains("nopumpkin") || ln.ToLower().Contains("noslowdown") || ln.ToLower().Contains("noweather") || ln.ToLower().Contains("noweb") || ln.ToLower().Contains("nuker") || ln.ToLower().Contains("nukerlegit") || ln.ToLower().Contains("panic") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("playeresp") || ln.ToLower().Contains("playerfinder") || ln.ToLower().Contains("potionsaver") || ln.ToLower().Contains("prophuntesp") || ln.ToLower().Contains("radar") || ln.ToLower().Contains("rainbowui") || ln.ToLower().Contains("remoteview") || ln.ToLower().Contains("safewalk") || ln.ToLower().Contains("scaffoldwalk") || ln.ToLower().Contains("servercrasher") || ln.ToLower().Contains("skinderp") || ln.ToLower().Contains("speedhack") || ln.ToLower().Contains("speednuker") || ln.ToLower().Contains("toomanyhax") || ln.ToLower().Contains("tp-aura") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("triggerbot") || ln.ToLower().Contains("trollpotion") || ln.ToLower().Contains("truesight") || ln.ToLower().Contains("tunneller") || ln.ToLower().Contains("x-ray") || ln.ToLower().Contains("hwid.exe") || ln.ToLower().Contains("clear strings.jar") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("2535.dll") || ln.ToLower().Contains("system_recording.dll") || ln.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || ln.ToLower().Contains("removestring.exe") || ln.ToLower().Contains("remover.exe") || ln.ToLower().Contains("ts cleaner.bat") || ln.ToLower().Contains("dotnetaanmemory.dll") || ln.ToLower().Contains("420 v0.3.exe") || ln.ToLower().Contains("clicker.exe") || ln.ToLower().Contains("purityx") || ln.ToLower().Contains("purityx.exe") || ln.ToLower().Contains("3ms.exe") || ln.ToLower().Contains("inject_client.exe") || ln.ToLower().Contains("apollocracked.exe") || ln.ToLower().Contains("azranRemover.jar") || ln.ToLower().Contains("me.tojatta.clicker.ui.cl") || ln.ToLower().Contains("brplz is cute") || ln.ToLower().Contains("burger78.exe") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("dewgs.exe") || ln.ToLower().Contains("doggo.exe") || ln.ToLower().Contains("inject client.exe") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("l-clicker.jar") || ln.ToLower().Contains("work.txt.exe") || ln.ToLower().Contains("lsdclicker.xml") || ln.ToLower().Contains("autoclick.exe") || ln.ToLower().Contains("jacobschellman18") || ln.ToLower().Contains("boomy") || ln.ToLower().Contains("XAttr") || ln.ToLower().Contains("xreduszz") || ln.ToLower().Contains("s08yzx.exe") || ln.ToLower().Contains("pvper") || ln.ToLower().Contains("smart clicker v3.0.1.exe") || ln.ToLower().Contains("pause script") || ln.ToLower().Contains("suspend Hotkeys") || ln.ToLower().Contains("_snak3") || ln.ToLower().Contains("terio.jar") || ln.ToLower().Contains("cracked by dinkio") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("aimbot") || ln.ToLower().Contains("anti bot") || ln.ToLower().Contains("auto armor") || ln.ToLower().Contains("auto clicker") || ln.ToLower().Contains("auto totem") || ln.ToLower().Contains("auto weapon") || ln.ToLower().Contains("bow aimbot") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("crystal aura") || ln.ToLower().Contains("hitbox") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("kill aura") || ln.ToLower().Contains("smoothaim") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("exploit") || ln.ToLower().Contains("anti fire") || ln.ToLower().Contains("anti hunger") || ln.ToLower().Contains("franky") || ln.ToLower().Contains("ghosthand") || ln.ToLower().Contains("new chunks") || ln.ToLower().Contains("copsandcrims") || ln.ToLower().Contains("minestrike") || ln.ToLower().Contains("murder") || ln.ToLower().Contains("prophunt") || ln.ToLower().Contains("quakecraft") || ln.ToLower().Contains("sneakyassassins") || ln.ToLower().Contains("anti aim") || ln.ToLower().Contains("anti desync") || ln.ToLower().Contains("anti sound lag") || ln.ToLower().Contains("anti vanish") || ln.ToLower().Contains("auto cheat") || ln.ToLower().Contains("auto disconnect") || ln.ToLower().Contains("auto reconnect") || ln.ToLower().Contains("discord rpc") || ln.ToLower().Contains("fancy chat") || ln.ToLower().Contains("log position") || ln.ToLower().Contains("middle click friend") || ln.ToLower().Contains("no srp") || ln.ToLower().Contains("self destruct") || ln.ToLower().Contains("air jump") || ln.ToLower().Contains("anti hazard") || ln.ToLower().Contains("auto jump") || ln.ToLower().Contains("auto walk") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("click tp") || ln.ToLower().Contains("elytra+") || ln.ToLower().Contains("fast fall") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("high jump") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("levitation control") || ln.ToLower().Contains("long jump") || ln.ToLower().Contains("no push") || ln.ToLower().Contains("no slow") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("riding") || ln.ToLower().Contains("safe walk") || ln.ToLower().Contains("anti afk") || ln.ToLower().Contains("auto eat") || ln.ToLower().Contains("auto eject") || ln.ToLower().Contains("auto farm") || ln.ToLower().Contains("auto fish") || ln.ToLower().Contains("auto mine") || ln.ToLower().Contains("auto tool") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("chest stealer") || ln.ToLower().Contains("fast interact") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("item saver") || ln.ToLower().Contains("liquid interact") || ln.ToLower().Contains("no fall") || ln.ToLower().Contains("no rotate") || ln.ToLower().Contains("scaffold") || ln.ToLower().Contains("skin blinker") || ln.ToLower().Contains("anti blind") || ln.ToLower().Contains("anti overlay") || ln.ToLower().Contains("breadcrumbs") || ln.ToLower().Contains("camera clip") || ln.ToLower().Contains("chams") || ln.ToLower().Contains("clickgui") || ln.ToLower().Contains("crosshair+") || ln.ToLower().Contains("enchant color") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("no render") || ln.ToLower().Contains("storage esp") || ln.ToLower().Contains("tracers") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("wireframe") || ln.ToLower().Contains("nacl_32") || ln.ToLower().Contains("yagami.exe") || ln.ToLower().Contains("l0li-0.2snapshot.exe") || ln.ToLower().Contains("loli client") || ln.ToLower().Contains("loli-hwid.exe") || ln.ToLower().Contains("math lesson 12 bac") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine.exe") || ln.ToLower().Contains("cheatengine681.exe") || ln.ToLower().Contains("speedhack.dll") || ln.ToLower().Contains("speedhack-x86_64.dll") || ln.ToLower().Contains("lua53-64.dll") || ln.ToLower().Contains("monoscript.lua") || ln.ToLower().Contains("speedhack-i386.dll") || ln.ToLower().Contains("jd-gui") || ln.ToLower().Contains("autohot") || ln.ToLower().Contains("autohotkey") || ln.ToLower().Contains(".ahk") || ln.ToLower().Contains("liquidbounce") || ln.ToLower().Contains("impact") || ln.ToLower().Contains("wurst") || ln.ToLower().Contains("huzuni") || ln.ToLower().Contains("wolfram") || ln.ToLower().Contains("sigma client") || ln.ToLower().Contains("aristois") || ln.ToLower().Contains("wwe client") || ln.ToLower().Contains("flare") || ln.ToLower().Contains("skillclient") || ln.ToLower().Contains("blazing") || ln.ToLower().Contains("vape v") || ln.ToLower().Contains("flux") || ln.ToLower().Contains("phantom client") || ln.ToLower().Contains("iridium") || ln.ToLower().Contains("weepcraft") || ln.ToLower().Contains("jigsaw") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("hcl client") || ln.ToLower().Contains("omikron") || ln.ToLower().Contains("sallos") || ln.ToLower().Contains("envy client") || ln.ToLower().Contains("matrix client") || ln.ToLower().Contains("nightmare") || ln.ToLower().Contains("luna client") || ln.ToLower().Contains("lina client") || ln.ToLower().Contains("suicide client") || ln.ToLower().Contains("obscure") || ln.ToLower().Contains("tigur") || ln.ToLower().Contains("synergy") || ln.ToLower().Contains("zecrus") || ln.ToLower().Contains("parallaxa") || ln.ToLower().Contains("pandora") || ln.ToLower().Contains("future client") || ln.ToLower().Contains("kami client") || ln.ToLower().Contains("inertia") || ln.ToLower().Contains("forgehax") || ln.ToLower().Contains("ares client") || ln.ToLower().Contains("rusherhack") || ln.ToLower().Contains("salhack") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("backdoor") || ln.ToLower().Contains("clicker") || ln.ToLower().Contains("104.22.37.186") || ln.ToLower().Contains("74.91.125.194") || ln.ToLower().Contains("144.217.241.181") || ln.ToLower().Contains("142.44.246.31"))
                        {
                            string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                            AppendTextBoxtoo("£javaw$" + time + " Possible Javaw string found " + ln);
                        }
                        ChangeLabeltoo(stringsearch[0, 1] + @"\" + stringsearch[0, 0]);
                        stringsearch[0, 1]++;
                        counteer++;// HERE BOI
                        /*                List<int> intarrayer = new List<int>();
                                        List<string> stringarrayer = new List<string>();
                                        string[] currentstringsearch = new string[] { "antiafk", "antiblind", "anticactus", "antiknockback", "antiwaterpush", "antiwobble", "autoarmor", "autobuild", "autoeat", "autoleave", "automine", "autopotion", "autoreconnect", "autorespawn", "autosign", "autosoup", "antispam", "autosword", "autodrop", "autofarm", "autofish", "autosprint", "autosteal", "autoswim", "autoswitch", "autotool", "autototem", "autowalk", "basefinder", "blink", "boatfly", "bonemealaura", "bowaimbot", "buildrandom", "bunnyhop", "cameranoclip", "cavefinder", "chattranslator", "chestesp", "clickaura", "crashchest", "criticals", "", "excavator", "extraelytra", "fancychat", "fastbreak", "fastladder", "fastplace", "fightbot", "flight", "forceop", "freecam", "fullbright", "glide", "handnoclip", "headroll", "healthtags", "highjump", "infinichat", "instantbunker", "itemesp", "itemgenerator", "jesus", "jetpack", "kaboom", "killaura", "killauralegit", "killpotion", "masstpa", "mileycyrus", "mobesp", "mobspawnesp", "mountbypass", "multiaura", "nameprotect", "nametags", "noclip", "nofall", "nofireoverlay", "nohurtcam", "nooverlay", "nopumpkin", "noslowdown", "noweather", "noweb", "nuker", "nukerlegit", "panic", "parkour", "playeresp", "playerfinder", "potionsaver", "prophuntesp", "radar", "rainbowui", "reach", "remoteview", "safewalk", "scaffoldwalk", "servercrasher", "skinderp", "speedhack", "speednuker", "toomanyhax", "tp-aura", "trajectories", "triggerbot", "trollpotion", "truesight", "tunneller", "x-ray", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
                                        for (int dankdank = 0; dankdank < currentstringsearch.Length; dankdank++)
                                        {
                                         int curvalue = FindInFile(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer", currentstringsearch[dankdank]);
                                            if (curvalue != 0)
                                            {
                                                intarrayer.Add(curvalue);
                                            }
                                        }
                                    //    StreamReader filee = new StreamReader(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer");
                                        //   stringsearch[0, 0] = Convert.ToInt32(new FileInfo(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Length);
                                        for (int d = 0; d < intarrayer.Count; d++)
                                        {
                                            //    string line =
                                            var lineNumber = File.ReadLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Take(intarrayer[d]).Count(c => c.ToCharArray()[0] == '\n') + 1;//File.ReadLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Take(intarrayer[d]).Count(c => c == '\n') + 1;
                                            stringarrayer.Add(File.OpenRead(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\explorer").Read(null,lineNumber, 200).ToString());//(filee.Read(intarrayer[d]).ToString);//.Skip(intarrayer[d]).ToString());
                                        }
                                        for (int h = 0; h < stringarrayer.Count; h++)
                                        {
                                            string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                            AppendTextBoxtoo("£explorer$" + time + " " + stringarrayer[h]);
                                        }
                                        //   filee.Close();
                                        // }
                                    }*/
                    }
                }
            }
            catch { }
            javawsearched = true;
            AddProgress("10");
        }

        private void worker_five(object sender, DoWorkEventArgs e)
        {
            /*
                        //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
                        string[] currentstringsearch = new string[] { "nacl_32", "yagami.exe", "l0li-0.2snapshot.exe", "loli client", "loli-hwid.exe", "math lesson 12 bac", "autoclicker", "cheatengine", "cheatengine", "cheatengine.exe", "cheatengine681.exe", "speedhack.dll", "speedhack-x86_64.dll", "lua53-64.dll", "monoscript.lua", "speedhack-i386.dll", "jd-gui", "autohot", "autohotkey", ".ahk", "liquidbounce", "impact", "wurst", "huzuni", "wolfram", "sigma", "aristois", "wwe client", "flare", "skillclient", "blazing", "vape v", "flux", "phantom client", "iridium", "weepcraft", "jigsaw", "autoclicker", "hcl client", "omikron", "sallos", "envy client", "matrix client", "nightmare", "luna client", "lina client", "suicide client", "obscure", "tigur", "synergy", "zecrus", "parallaxa", "pandora", "future", "kami client", "inertia", "forgehax", "ares client", "rusherhack", "hack", "salhack", "baritone", "backdoor", "clicker", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
                        stringsearch[2, 0] += currentstringsearch.Length - 1;
                        for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
                        {
                            byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                            //      DoSearch("svchost", currentstringsearch[stringsearchin]);

                            for (int gg = 0; listaint.Count > gg; gg++)
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£svchost$" + time + " Possible Svchost string found " + currentstringsearch[stringsearchin]);
                                stringsearch[2, 2]++;
                            }
                            listeea.Clear();
                            listaint.Clear();
                            ChangeLabelten(stringsearch[2, 2] + " Svchost strings found  " + stringsearch[2, 1] + "\\" + stringsearch[2, 0]);
                            ChangeLabelseven(currentstringsearch[stringsearchin]);

                            stringsearch[2, 1]++;
                        }*/
            try
            {
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.json", SearchOption.AllDirectories);
                for (int i = 0; filePaths.Length > i; i++)
                {
                    try
                    {
                        string[] text = File.ReadAllLines(filePaths[i]);
                        for (int ef = 0; text.Length > ef; ef++)
                        {
                            string texttolower = text[ef].ToLower();
                            if (texttolower.Contains("x-ray") || texttolower.Contains("hwid.exe") || texttolower.Contains("clear strings.jar") || texttolower.Contains("jnativehook") || texttolower.Contains("2535.dll") || texttolower.Contains("system_recording.dll") || texttolower.Contains("microsoft.visualBasic.powerpacks.dll") || texttolower.Contains("removestring.exe") || texttolower.Contains("remover.exe") || texttolower.Contains("ts cleaner.bat") || texttolower.Contains("dotnetaanmemory.dll") || texttolower.Contains("420 v0.3.exe") || texttolower.Contains("clicker.exe") || texttolower.Contains("purityx") || texttolower.Contains("purityx.exe") || texttolower.Contains("3ms.exe") || texttolower.Contains("inject_client.exe") || texttolower.Contains("apollocracked.exe") || texttolower.Contains("azranRemover.jar") || texttolower.Contains("me.tojatta.clicker.ui.cl") || texttolower.Contains("brplz is cute") || texttolower.Contains("burger78.exe") || texttolower.Contains("autoclicker") || texttolower.Contains("dewgs.exe") || texttolower.Contains("doggo.exe") || texttolower.Contains("inject client.exe") || texttolower.Contains("jnativehook") || texttolower.Contains("l-clicker.jar") || texttolower.Contains("work.txt.exe") || texttolower.Contains("lsdclicker.xml") || texttolower.Contains("autoclick.exe") || texttolower.Contains("jacobschellman18") || texttolower.Contains("boomy") || texttolower.Contains("XAttr") || texttolower.Contains("xreduszz") || texttolower.Contains("s08yzx.exe") || texttolower.Contains("pvper") || texttolower.Contains("smart clicker v3.0.1.exe") || texttolower.Contains("pause script") || texttolower.Contains("suspend Hotkeys") || texttolower.Contains("_snak3") || texttolower.Contains("terio.jar") || texttolower.Contains("cracked by dinkio") || texttolower.Contains("nofall") || texttolower.Contains("aimbot") || texttolower.Contains("anti bot") || texttolower.Contains("auto armor") || texttolower.Contains("auto clicker") || texttolower.Contains("nacl_32") || texttolower.Contains("yagami.exe") || texttolower.Contains("l0li-0.2snapshot.exe") || texttolower.Contains("loli client") || texttolower.Contains("loli-hwid.exe") || texttolower.Contains("math lesson 12 bac") || texttolower.Contains("autoclicker") || texttolower.Contains("cheatengine") || texttolower.Contains("cheatengine") || texttolower.Contains("cheatengine.exe") || texttolower.Contains("cheatengine681.exe") || texttolower.Contains("speedhack.dll") || texttolower.Contains("speedhack-x86_64.dll") || texttolower.Contains("lua53-64.dll") || texttolower.Contains("monoscript.lua") || texttolower.Contains("speedhack-i386.dll") || texttolower.Contains("jd-gui") || texttolower.Contains("autohot") || texttolower.Contains("autohotkey") || texttolower.Contains(".ahk") || texttolower.Contains("liquidbounce") || texttolower.Contains("impact client") || texttolower.Contains("wurst") || texttolower.Contains("huzuni") || texttolower.Contains("wolfram") || texttolower.Contains("sigma client") || texttolower.Contains("aristois") || texttolower.Contains("wwe client") || texttolower.Contains("flare") || texttolower.Contains("skillclient") || texttolower.Contains("blazing") || texttolower.Contains("vape v") || texttolower.Contains("flux") || texttolower.Contains("phantom client") || texttolower.Contains("iridium") || texttolower.Contains("weepcraft") || texttolower.Contains("jigsaw") || texttolower.Contains("autoclicker") || texttolower.Contains("hcl client") || texttolower.Contains("omikron") || texttolower.Contains("sallos") || texttolower.Contains("envy client") || texttolower.Contains("matrix client") || texttolower.Contains("nightmare") || texttolower.Contains("luna client") || texttolower.Contains("lina client") || texttolower.Contains("suicide client") || texttolower.Contains("obscure") || texttolower.Contains("tigur") || texttolower.Contains("synergy") || texttolower.Contains("zecrus") || texttolower.Contains("parallaxa") || texttolower.Contains("pandora") || texttolower.Contains("future client") || texttolower.Contains("kami client") || texttolower.Contains("inertia") || texttolower.Contains("forgehax") || texttolower.Contains("ares client") || texttolower.Contains("rusherhack") || texttolower.Contains("salhack") || texttolower.Contains("baritone") || texttolower.Contains("clicker") || texttolower.Contains("104.22.37.186") || texttolower.Contains("74.91.125.194") || texttolower.Contains("144.217.241.181") || texttolower.Contains("142.44.246.31"))
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£javaw$" + time + " Possible Cheat in versions " + filePaths[i] + "  " + texttolower);
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

                          MainAsync();
                        currentstat = "Search finished! Check the results";
                        break;
                    }
                }
                //     MainAsync();
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
                string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\versions", "*.json", SearchOption.AllDirectories);
                for (int i = 0; filePaths.Length > i; i++)
                {
                    try
                    { 
                    string[] text = File.ReadAllLines(filePaths[i]);
                        for (int ef = 0; text.Length > ef; ef++)
                        {
                            string texttolower = text[ef].ToLower();
                            if (texttolower.Contains("x-ray") || texttolower.Contains("hwid.exe") || texttolower.Contains("clear strings.jar") || texttolower.Contains("jnativehook") || texttolower.Contains("2535.dll") || texttolower.Contains("system_recording.dll") || texttolower.Contains("microsoft.visualBasic.powerpacks.dll") || texttolower.Contains("removestring.exe") || texttolower.Contains("remover.exe") || texttolower.Contains("ts cleaner.bat") || texttolower.Contains("dotnetaanmemory.dll") || texttolower.Contains("420 v0.3.exe") || texttolower.Contains("clicker.exe") || texttolower.Contains("purityx") || texttolower.Contains("purityx.exe") || texttolower.Contains("3ms.exe") || texttolower.Contains("inject_client.exe") || texttolower.Contains("apollocracked.exe") || texttolower.Contains("azranRemover.jar") || texttolower.Contains("me.tojatta.clicker.ui.cl") || texttolower.Contains("brplz is cute") || texttolower.Contains("burger78.exe") || texttolower.Contains("autoclicker") || texttolower.Contains("dewgs.exe") || texttolower.Contains("doggo.exe") || texttolower.Contains("inject client.exe") || texttolower.Contains("jnativehook") || texttolower.Contains("l-clicker.jar") || texttolower.Contains("work.txt.exe") || texttolower.Contains("lsdclicker.xml") || texttolower.Contains("autoclick.exe") || texttolower.Contains("jacobschellman18") || texttolower.Contains("boomy") || texttolower.Contains("XAttr") || texttolower.Contains("xreduszz") || texttolower.Contains("s08yzx.exe") || texttolower.Contains("pvper") || texttolower.Contains("smart clicker v3.0.1.exe") || texttolower.Contains("pause script") || texttolower.Contains("suspend Hotkeys") || texttolower.Contains("_snak3") || texttolower.Contains("terio.jar") || texttolower.Contains("cracked by dinkio") || texttolower.Contains("nofall") || texttolower.Contains("aimbot") || texttolower.Contains("wireframe") || texttolower.Contains("nacl_32") || texttolower.Contains("yagami.exe") || texttolower.Contains("l0li-0.2snapshot.exe") || texttolower.Contains("loli client") || texttolower.Contains("loli-hwid.exe") || texttolower.Contains("math lesson 12 bac") || texttolower.Contains("autoclicker") || texttolower.Contains("cheatengine") || texttolower.Contains("cheatengine") || texttolower.Contains("cheatengine.exe") || texttolower.Contains("cheatengine681.exe") || texttolower.Contains("speedhack.dll") || texttolower.Contains("speedhack-x86_64.dll") || texttolower.Contains("lua53-64.dll") || texttolower.Contains("monoscript.lua") || texttolower.Contains("speedhack-i386.dll") || texttolower.Contains("jd-gui") || texttolower.Contains("autohot") || texttolower.Contains("autohotkey") || texttolower.Contains(".ahk") || texttolower.Contains("liquidbounce") || texttolower.Contains("impact client") || texttolower.Contains("wurst") || texttolower.Contains("huzuni") || texttolower.Contains("wolfram") || texttolower.Contains("sigma client") || texttolower.Contains("aristois") || texttolower.Contains("wwe client") || texttolower.Contains("flare") || texttolower.Contains("skillclient") || texttolower.Contains("blazing") || texttolower.Contains("vape v") || texttolower.Contains("flux") || texttolower.Contains("phantom client") || texttolower.Contains("iridium") || texttolower.Contains("weepcraft") || texttolower.Contains("jigsaw") || texttolower.Contains("autoclicker") || texttolower.Contains("hcl client") || texttolower.Contains("omikron") || texttolower.Contains("sallos") || texttolower.Contains("envy client") || texttolower.Contains("matrix client") || texttolower.Contains("nightmare") || texttolower.Contains("luna client") || texttolower.Contains("lina client") || texttolower.Contains("suicide client") || texttolower.Contains("obscure") || texttolower.Contains("tigur") || texttolower.Contains("synergy") || texttolower.Contains("zecrus") || texttolower.Contains("parallaxa") || texttolower.Contains("pandora") || texttolower.Contains("future client") || texttolower.Contains("kami client") || texttolower.Contains("inertia") || texttolower.Contains("forgehax") || texttolower.Contains("ares client") || texttolower.Contains("rusherhack") || texttolower.Contains("salhack") || texttolower.Contains("baritone") || texttolower.Contains("clicker") || texttolower.Contains("104.22.37.186") || texttolower.Contains("74.91.125.194") || texttolower.Contains("144.217.241.181") || texttolower.Contains("142.44.246.31"))
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£javaw$" + time + " Possible Cheat in .minecraft " + filePaths[i] + "  " + texttolower);
                            }
                        }
                        
                    }
                    catch{ }
                }
            }
            catch { }

            AddProgress("20");
            /*
                        //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
                        string[] currentstringsearch = new string[] { "nacl_32", "yagami.exe", "l0li-0.2snapshot.exe", "loli client", "loli-hwid.exe", "math lesson 12 bac", "autoclicker", "cheatengine", "cheatengine", "cheatengine.exe", "cheatengine681.exe", "speedhack.dll", "speedhack-x86_64.dll", "lua53-64.dll", "monoscript.lua", "speedhack-i386.dll", "jd-gui", "autohot", "autohotkey", ".ahk", "liquidbounce", "impact", "wurst", "huzuni", "wolfram", "sigma", "aristois", "wwe client", "flare", "skillclient", "blazing", "vape v", "flux", "phantom client", "iridium", "weepcraft", "jigsaw", "autoclicker", "hcl client", "omikron", "sallos", "envy client", "matrix client", "nightmare", "luna client", "lina client", "suicide client", "obscure", "tigur", "synergy", "zecrus", "parallaxa", "pandora", "future", "kami client", "inertia", "forgehax", "ares client", "rusherhack", "hack", "salhack", "baritone", "backdoor", "clicker" };
                        stringsearch[0, 0] += currentstringsearch.Length;
                        for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
                        {
                            byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                            //     DoSearch("explorer", currentstringsearch[stringsearchin]);

                            for (int gg = 0; listaint.Count > gg; gg++)
                            {
                                string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                                AppendTextBoxtoo("£explorer$" + time + " Possible Explorer string found " + currentstringsearch[stringsearchin]);
                                stringsearch[1, 2]++;
                            }
                            listeea.Clear();
                            listaint.Clear();
                            //  ChangeLabeltoo(stringsearch[1, 2] + " Explorer strings found  " + stringsearch[0, 1] + "\\" + stringsearch[0, 0]);
                            //     ChangeLabelseven(currentstringsearch[stringsearchin]);

                            stringsearch[0, 1]++;
                        }*/
            AddProgress("10");
        }

        private void worker_too(object sender, DoWorkEventArgs e)
        {
            string[] filePaths = Directory.GetFiles(@"C:\Users\" + Environment.UserName + @"\AppData\Roaming\.minecraft\", "*.log", SearchOption.AllDirectories);
            for (int i = 0; filePaths.Length > i; i++)
            {
                string[] text = File.ReadAllLines(filePaths[i]);
                for (int ef = 0; text.Length > ef; ef++)
                {
                    string texttolower = text[ef].ToLower();
                    if (texttolower.Contains("x-ray") || texttolower.Contains("hwid.exe") || texttolower.Contains("clear strings.jar") || texttolower.Contains("jnativehook") || texttolower.Contains("2535.dll") || texttolower.Contains("system_recording.dll") || texttolower.Contains("microsoft.visualBasic.powerpacks.dll") || texttolower.Contains("removestring.exe") || texttolower.Contains("remover.exe") || texttolower.Contains("ts cleaner.bat") || texttolower.Contains("dotnetaanmemory.dll") || texttolower.Contains("420 v0.3.exe") || texttolower.Contains("clicker.exe") || texttolower.Contains("purityx") || texttolower.Contains("purityx.exe") || texttolower.Contains("3ms.exe") || texttolower.Contains("inject_client.exe") || texttolower.Contains("apollocracked.exe") || texttolower.Contains("azranRemover.jar") || texttolower.Contains("me.tojatta.clicker.ui.cl") || texttolower.Contains("brplz is cute") || texttolower.Contains("burger78.exe") || texttolower.Contains("autoclicker") || texttolower.Contains("dewgs.exe") || texttolower.Contains("doggo.exe") || texttolower.Contains("inject client.exe") || texttolower.Contains("jnativehook") || texttolower.Contains("l-clicker.jar") || texttolower.Contains("work.txt.exe") || texttolower.Contains("lsdclicker.xml") || texttolower.Contains("autoclick.exe") || texttolower.Contains("jacobschellman18") || texttolower.Contains("boomy") || texttolower.Contains("XAttr") || texttolower.Contains("xreduszz") || texttolower.Contains("s08yzx.exe") || texttolower.Contains("pvper") || texttolower.Contains("smart clicker v3.0.1.exe") || texttolower.Contains("pause script") || texttolower.Contains("suspend Hotkeys") || texttolower.Contains("_snak3") || texttolower.Contains("terio.jar") || texttolower.Contains("cracked by dinkio") || texttolower.Contains("nofall") || texttolower.Contains("aimbot") || texttolower.Contains("wireframe") || texttolower.Contains("nacl_32") || texttolower.Contains("yagami.exe") || texttolower.Contains("l0li-0.2snapshot.exe") || texttolower.Contains("loli client") || texttolower.Contains("loli-hwid.exe") || texttolower.Contains("math lesson 12 bac") || texttolower.Contains("autoclicker") || texttolower.Contains("cheatengine") || texttolower.Contains("cheatengine") || texttolower.Contains("cheatengine.exe") || texttolower.Contains("cheatengine681.exe") || texttolower.Contains("speedhack.dll") || texttolower.Contains("speedhack-x86_64.dll") || texttolower.Contains("lua53-64.dll") || texttolower.Contains("monoscript.lua") || texttolower.Contains("speedhack-i386.dll") || texttolower.Contains("jd-gui") || texttolower.Contains("autohot") || texttolower.Contains("autohotkey") || texttolower.Contains(".ahk") || texttolower.Contains("liquidbounce") || texttolower.Contains("impact client") || texttolower.Contains("wurst") || texttolower.Contains("huzuni") || texttolower.Contains("wolfram") || texttolower.Contains("sigma client") || texttolower.Contains("aristois") || texttolower.Contains("wwe client") || texttolower.Contains("flare") || texttolower.Contains("skillclient") || texttolower.Contains("blazing") || texttolower.Contains("vape v") || texttolower.Contains("flux") || texttolower.Contains("phantom client") || texttolower.Contains("iridium") || texttolower.Contains("weepcraft") || texttolower.Contains("jigsaw") || texttolower.Contains("autoclicker") || texttolower.Contains("hcl client") || texttolower.Contains("omikron") || texttolower.Contains("sallos") || texttolower.Contains("envy client") || texttolower.Contains("matrix client") || texttolower.Contains("nightmare") || texttolower.Contains("luna client") || texttolower.Contains("lina client") || texttolower.Contains("suicide client") || texttolower.Contains("obscure") || texttolower.Contains("tigur") || texttolower.Contains("synergy") || texttolower.Contains("zecrus") || texttolower.Contains("parallaxa") || texttolower.Contains("pandora") || texttolower.Contains("future client") || texttolower.Contains("kami client") || texttolower.Contains("inertia") || texttolower.Contains("forgehax") || texttolower.Contains("ares client") || texttolower.Contains("rusherhack") || texttolower.Contains("salhack") || texttolower.Contains("baritone") || texttolower.Contains("clicker") || texttolower.Contains("104.22.37.186") || texttolower.Contains("74.91.125.194") || texttolower.Contains("144.217.241.181") || texttolower.Contains("142.44.246.31"))
                    {
                        string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                        AppendTextBoxtoo("£javaw$" + time + " Possible Cheat in .minecraft " + filePaths[i] + "  " + texttolower);
                    }
                }
            }

            AddProgress("20");
            /*   //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
               string[] currentstringsearch = new string[] { "nofall", "aimbot", "anti bot", "auto armor", "auto clicker", "auto totem", "auto weapon", "bow aimbot", "criticals", "crystal aura", "hitbox", "killaura", "kill aura", "smoothaim", "autosoup", "exploit", "anti fire", "anti hunger", "franky", "ghosthand", "new chunks", "copsandcrims", "minestrike", "murder", "prophunt", "quakecraft", "sneakyassassins", "animations", "anti aim", "anti desync", "anti sound lag", "anti vanish", "auto cheat", "auto disconnect", "auto reconnect", "discord rpc", "fancy chat", "log position", "middle click friend", "no srp", "self destruct", "air jump", "anti hazard", "auto jump", "auto walk", "baritone", "boatfly", "click tp", "elytra+", "fast fall", "flight", "high jump", "jesus", "levitation control", "long jump", "no push", "no slow", "parkour", "riding", "safe walk", "speed", "velocity", "anti afk", "auto eat", "auto eject", "auto farm", "auto fish", "auto mine", "auto tool", "blink", "chest stealer", "fast interact", "freecam", "item saver", "liquid interact", "no fall", "no rotate", "scaffold", "skin blinker", "anti blind", "anti overlay", "breadcrumbs", "camera clip", "chams", "clickgui", "crosshair+", "enchant color", "nametags", "no render", "storage esp", "tracers", "trajectories", "wireframe" };
               stringsearch[1, 0] += currentstringsearch.Length;
               for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
               {
                   byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                   //      DoSearch("javaw", currentstringsearch[stringsearchin]);

                   for (int gg = 0; listaint.Count > gg; gg++)
                   {
                       string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                       AppendTextBoxtoo("£javaw$" + time + " Possible Javaw string found " + currentstringsearch[stringsearchin]);
                       stringsearch[0, 2]++;
                   }
                   listeea.Clear();
                   listaint.Clear();
                   ChangeLabel(stringsearch[0, 2] + " Javaw strings found   " + stringsearch[1, 1] + "\\" + stringsearch[1, 0]);
                   ChangeLabelseven(currentstringsearch[stringsearchin]);

                   stringsearch[1, 1]++;
               }*/
            AddProgress("10");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form2 Formpages = new Form2();
            //    Hide();
            Formpages.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        /*  [DllImport("kernel32.dll")]
          public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] buffer, uint size, int lpNumberOfBytesRead);
          [DllImport("kernel32.dll")]
          protected static extern int VirtualQueryEx(IntPtr hProcess, IntPtr lpAddress, out MEMORY_BASIC_INFORMATION lpBuffer, int dwLength);

          [StructLayout(LayoutKind.Sequential)]
          protected struct MEMORY_BASIC_INFORMATION
          {
              public IntPtr BaseAddress;
              public IntPtr AllocationBase;
              public uint AllocationProtect;
              public uint RegionSize;
              public uint State;
              public uint Protect;
              public uint Type;
          }
          List<MEMORY_BASIC_INFORMATION> MemReg { get; set; }

          public void MemInfo(IntPtr pHandle)
          {
              try
              {
                  IntPtr Addy = new IntPtr();
                  while (true)
                  {
                      MEMORY_BASIC_INFORMATION MemInfo = new MEMORY_BASIC_INFORMATION();
                      int MemDump = VirtualQueryEx(pHandle, Addy, out MemInfo, Marshal.SizeOf(MemInfo));
                      if (MemDump == 0) break;
                      if ((MemInfo.State & 0x1000) != 0 && (MemInfo.Protect & 0x100) == 0)
                          MemReg.Add(MemInfo);
                      Addy = new IntPtr(MemInfo.BaseAddress.ToInt32() + (int)MemInfo.RegionSize);
                  }
              }
              catch { }
          }
          public IntPtr _Scan(byte[] sIn, byte[] sFor)
          {
              try
              {
                  int[] sBytes = new int[256]; int Pool = 0;
                  int End = sFor.Length - 1;
                  for (int i = 0; i < 256; i++)
                      sBytes[i] = sFor.Length;
                  for (int i = 0; i < End; i++)
                      sBytes[sFor[i]] = End - i;
                  while (Pool <= sIn.Length - sFor.Length)
                  {
                      for (int i = End; sIn[Pool + i] == sFor[i]; i--)
                          if (i == 0) return new IntPtr(Pool);
                      Pool += sBytes[sIn[Pool + End]];
                  }
              }
              catch { }
              return IntPtr.Zero;
          }
          public IntPtr AobScan(string ProcessName, byte[] Pattern)
          {
              try
              {
                  Process[] P = Process.GetProcessesByName(ProcessName);
                  ProcessHandle phandle = null;
                  try
                  {
                      phandle = new ProcessHandle(P[0].Id);
                  }
                  catch { }
                  if (P.Length == 0) return IntPtr.Zero;
                  MemReg = new List<MEMORY_BASIC_INFORMATION>();
                  MemInfo(phandle);
                  for (int i = 0; i < MemReg.Count; i++)
                  {
                      byte[] buff = new byte[MemReg[i].RegionSize];
                      ReadProcessMemory(phandle, MemReg[i].BaseAddress, buff, MemReg[i].RegionSize, 0);

                      IntPtr Result = _Scan(buff, Pattern);
                      if (Result != IntPtr.Zero)
                      {
                          listeea.Add(Result);
                          listaint.Add(MemReg[i].BaseAddress.ToInt32());
                      }
                      //IntPtr[] Resulte = Result;
                      //
                  }
                  //return new IntPtr(MemReg[i].BaseAddress.ToInt32() + Result.ToInt32());
              }
              catch { }
              return IntPtr.Zero;
          }*/
        /*  public IntPtr AobScanall(string ProcessName, byte[] Pattern)
          {
              try
              {
                  //    List<ProcessHandle> processlist = new List<ProcessHandle>();
                  Process[] P = Process.GetProcessesByName(ProcessName);

                  for (int gae = 0; P.Length > gae; gae++)
                  {
                      //     processlist.Add(P[gae].Id);
                      if (P.Length == 0) return IntPtr.Zero;
                      MemReg = new List<MEMORY_BASIC_INFORMATION>();
                      MemInfo(P[gae].Handle);
                      for (int i = 0; i < MemReg.Count; i++)
                      {
                          byte[] buff = new byte[MemReg[i].RegionSize];
                          ReadProcessMemory(P[gae].Handle, MemReg[i].BaseAddress, buff, MemReg[i].RegionSize, 0);

                          IntPtr Result = _Scan(buff, Pattern);
                          if (Result != IntPtr.Zero)
                          {
                              listeeea.Add(Result);
                              listainte.Add(MemReg[i].BaseAddress.ToInt32());
                          }
                          //IntPtr[] Resulte = Result;
                          //
                      }
                  }
                  //return new IntPtr(MemReg[i].BaseAddress.ToInt32() + Result.ToInt32());
              }
              catch { }
              return IntPtr.Zero;
          }*/
        /*  public void DoSearch(string processname, string value)
          {
              try
              {
                  List<string> filecont = new List<string>();

                  //  SearchValues.Add(value);
                  IntPtr baseAddress, lastAddress;
                  Process[] process = Process.GetProcessesByName(processname);
                  baseAddress = process[0].MainModule.BaseAddress;
                  lastAddress = baseAddress + process[0].MainModule.ModuleMemorySize;
                 // processPointer = OpenProcess((uint)(0x0010), 1, (uint)process[0].Id);
                  string[] filecontent;
                  int iVal;
                  double dVal;
                  string currentstring;
                  int.TryParse(value, out iVal);
                  double.TryParse(value, out dVal);
                  for (Int64 addr = (Int64)baseAddress; addr + value.Length < (Int64)lastAddress; addr = +8)
                  {
                      try
                      {
                          *//*       // Match numbers
                                 if (dVal > 0 && MemoryContainsNumber((IntPtr)addr, dVal, ((IntPtr)addr)))
                                 {
                                     PossibleAddresses.Add((IntPtr)addr);
                                     listaint.Add(1);
                                 }
                                 else if (iVal > 0 && MemoryContainsNumber((IntPtr)addr, iVal, ((IntPtr)addr)))
                                 {
                                     PossibleAddresses.Add((IntPtr)addr);
                                     listaint.Add(1);
                                 }*//*
                          currentstring = ReadMemory((IntPtr)addr, (uint)value.Length, (IntPtr)addr).Trim().ToLower();

                          if (currentstring != "\0\0\0\0\0\0\0\0" && currentstring.Contains('a') || currentstring.Contains('b') || currentstring.Contains('c') || currentstring.Contains('d') || currentstring.Contains('e') || currentstring.Contains('f') || currentstring.Contains('g') || currentstring.Contains('h') || currentstring.Contains('i') || currentstring.Contains('j') || currentstring.Contains('k') || currentstring.Contains('l') || currentstring.Contains('m') || currentstring.Contains('n') || currentstring.Contains('o') || currentstring.Contains('p') || currentstring.Contains('q') || currentstring.Contains('r') || currentstring.Contains('s') || currentstring.Contains('t') || currentstring.Contains('u') || currentstring.Contains('v') || currentstring.Contains('w') || currentstring.Contains('x') || currentstring.Contains('y') || currentstring.Contains('z') || currentstring.Contains('?') || currentstring.Contains(':') || currentstring.Contains('(') || currentstring.Contains(')') || currentstring.Contains('*') || currentstring.Contains('&') || currentstring.Contains('^') || currentstring.Contains('%') || currentstring.Contains('$') || currentstring.Contains('#') || currentstring.Contains('@') || currentstring.Contains('!') || currentstring.Contains('A') || currentstring.Contains('B') || currentstring.Contains('C') || currentstring.Contains('D') || currentstring.Contains('E') || currentstring.Contains('F') || currentstring.Contains('G') || currentstring.Contains('H') || currentstring.Contains('I') || currentstring.Contains('J') || currentstring.Contains('K') || currentstring.Contains('L') || currentstring.Contains('M') || currentstring.Contains('N') || currentstring.Contains('O') || currentstring.Contains('P') || currentstring.Contains('Q') || currentstring.Contains('R') || currentstring.Contains('S') || currentstring.Contains('T') || currentstring.Contains('U') || currentstring.Contains('V') || currentstring.Contains('W') || currentstring.Contains('X') || currentstring.Contains('Y') || currentstring.Contains('Z') || currentstring.Contains('1') || currentstring.Contains('2') || currentstring.Contains('3') || currentstring.Contains('4') || currentstring.Contains('5') || currentstring.Contains('6') || currentstring.Contains('7') || currentstring.Contains('8') || currentstring.Contains('9') || currentstring.Contains('0') || currentstring.Contains('-') || currentstring.Contains('_') || currentstring.Contains('+') || currentstring.Contains('=') || currentstring.Contains('`') || currentstring.Contains('~') || currentstring.Contains('\"') || currentstring.Contains('/') || currentstring.Contains('\\'))
                          {
                              filecont.Add(currentstring);
                          }

                          // Match strings
                          *//*
                                                  if (ReadMemory((IntPtr)addr, (uint)value.Length, (IntPtr)addr).Trim().ToLower() == value.Trim().ToLower())
                                                  {
                                                      PossibleAddresses.Add((IntPtr)addr);
                                                      listaint.Add(1);
                                                  }*//*
                      }
                      catch { }
                  }

                  filecontent = filecont.ToArray();
                  File.WriteAllLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\" + processname, filecontent);
                  hasLockedOn = PossibleAddresses.Count == 1;

                //  CloseHandle(processPointer);
              }
              catch { }
          }*/
        /* private string ReadMemory(IntPtr memAddress, uint size, IntPtr BaseAddress)
         {
             byte[] buffer = new byte[size];
             IntPtr bytesRead;

             ReadProcessMemory(processPointer, BaseAddress, buffer, size, out bytesRead);
             return Encoding.Default.GetString(buffer);
             //string result = "";
             //foreach (string c in BitConverter.ToString(buffer).Split('-'))
             //    result += char.ConvertFromUtf32(Int32.Parse(c, System.Globalization.NumberStyles.HexNumber));
             //return result;
         }*/
        /*        private bool MemoryContainsNumber(IntPtr memAddress, double number, IntPtr BaseAddress)
                {
                    byte[] numberBytes = BitConverter.GetBytes(number);
                    byte[] buffer = new byte[numberBytes.Length];
                    IntPtr bytesRead;

                    ReadProcessMemory(processPointer, BaseAddress, buffer, (uint)numberBytes.Length, out bytesRead);

                    for (int i = 0; i < buffer.Length; i++)
                        if (buffer[i] != numberBytes[i])
                            return false;
                    return true;
                }*/
        /*  public void Dumpmemory(string processname)
          {
              try
              {
                  List<string> filecont = new List<string>();

                  //  SearchValues.Add(value);
                  IntPtr baseAddress, lastAddress;
                  Process[] process = Process.GetProcessesByName(processname);
                  baseAddress = process[0].MainModule.BaseAddress;
                  lastAddress = baseAddress + process[0].MainModule.ModuleMemorySize;
                  processPointer = OpenProcess((uint)(0x0010), 1, (uint)process[0].Id);
                  //    string[] filecontent;
                  string currentstring;

                  // for (Int64 addr = (Int64)baseAddress; addr < (Int64)lastAddress; addr =+ 8)
                  {
                      try
                      {
                         currentprocess = process[0];

                            Int64 memorysize = (Int64)lastAddress - (Int64)baseAddress;
                          *//*       // Match numbers
                                 if (dVal > 0 && MemoryContainsNumber((IntPtr)addr, dVal, ((IntPtr)addr)))
                                 {
                                     PossibleAddresses.Add((IntPtr)addr);
                                     listaint.Add(1);
                                 }
                                 else if (iVal > 0 && MemoryContainsNumber((IntPtr)addr, iVal, ((IntPtr)addr)))
                                 {
                                     PossibleAddresses.Add((IntPtr)addr);
                                     listaint.Add(1);
                                 }*//*
                          currentstring = ReadMemory(baseAddress, (uint)memorysize, baseAddress).Trim().ToLower();

                          //   if (currentstring != "\0\0\0\0\0\0\0\0" && currentstring.Contains('a') || currentstring.Contains('b') || currentstring.Contains('c') || currentstring.Contains('d') || currentstring.Contains('e') || currentstring.Contains('f') || currentstring.Contains('g') || currentstring.Contains('h') || currentstring.Contains('i') || currentstring.Contains('j') || currentstring.Contains('k') || currentstring.Contains('l') || currentstring.Contains('m') || currentstring.Contains('n') || currentstring.Contains('o') || currentstring.Contains('p') || currentstring.Contains('q') || currentstring.Contains('r') || currentstring.Contains('s') || currentstring.Contains('t') || currentstring.Contains('u') || currentstring.Contains('v') || currentstring.Contains('w') || currentstring.Contains('x') || currentstring.Contains('y') || currentstring.Contains('z') || currentstring.Contains('?') || currentstring.Contains(':') || currentstring.Contains('(') || currentstring.Contains(')') || currentstring.Contains('*') || currentstring.Contains('&') || currentstring.Contains('^') || currentstring.Contains('%') || currentstring.Contains('$') || currentstring.Contains('#') || currentstring.Contains('@') || currentstring.Contains('!') || currentstring.Contains('A') || currentstring.Contains('B') || currentstring.Contains('C') || currentstring.Contains('D') || currentstring.Contains('E') || currentstring.Contains('F') || currentstring.Contains('G') || currentstring.Contains('H') || currentstring.Contains('I') || currentstring.Contains('J') || currentstring.Contains('K') || currentstring.Contains('L') || currentstring.Contains('M') || currentstring.Contains('N') || currentstring.Contains('O') || currentstring.Contains('P') || currentstring.Contains('Q') || currentstring.Contains('R') || currentstring.Contains('S') || currentstring.Contains('T') || currentstring.Contains('U') || currentstring.Contains('V') || currentstring.Contains('W') || currentstring.Contains('X') || currentstring.Contains('Y') || currentstring.Contains('Z') || currentstring.Contains('1') || currentstring.Contains('2') || currentstring.Contains('3') || currentstring.Contains('4') || currentstring.Contains('5') || currentstring.Contains('6') || currentstring.Contains('7') || currentstring.Contains('8') || currentstring.Contains('9') || currentstring.Contains('0') || currentstring.Contains('-') || currentstring.Contains('_') || currentstring.Contains('+') || currentstring.Contains('=') || currentstring.Contains('`') || currentstring.Contains('~') || currentstring.Contains('\"') || currentstring.Contains('/') || currentstring.Contains('\\'))
                          //   {
                          //      filecont.Add(currentstring);

                          //  }

                   //       File.WriteAllLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\" + processname, currentstring.Split());

                          // Match strings
                          *//*
                                                  if (ReadMemory((IntPtr)addr, (uint)value.Length, (IntPtr)addr).Trim().ToLower() == value.Trim().ToLower())
                                                  {
                                                      PossibleAddresses.Add((IntPtr)addr);
                                                      listaint.Add(1);
                                                  }*//*
                      }
                      catch { }
                  }

                  //   filecontent = filecont.ToArray();
                  //     File.WriteAllLines(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\" + processname, filecontent);
                  hasLockedOn = PossibleAddresses.Count == 1;

                  CloseHandle(processPointer);
              }
              catch { }
          }

          [DllImport("kernel32.dll")]
          public static extern IntPtr OpenProcess(UInt32 dwDesiredAccess, Int32 bInheritHandle, UInt32 dwProcessId);
          [DllImport("kernel32.dll")]
          public static extern Int32 CloseHandle(IntPtr hObject);
          [DllImport("kernel32.dll")]
          public static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesRead);
          [DllImport("kernel32.dll")]
          public static extern Int32 WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, UInt32 size, out IntPtr lpNumberOfBytesWritten);*/
        /*    public static int FindInFile(string fileName, string value)
            {   // returns complement of number of characters in file if not found
                // else returns index where value found
                int index = 0;
                using (StreamReader reader = new StreamReader(fileName))
                {
                    if (String.IsNullOrEmpty(value))
                        return 0;
                    StringSearch valueSearch = new StringSearch(value);
                    int readChar;
                    while ((readChar = reader.Read()) >= 0)
                    {
                        ++index;
                        if (valueSearch.Found(readChar))
                            return index - value.Length;
                    }
                }
                return 0;
            }
            public class StringSearch
            {   // Call Found one character at a time until string found
                private readonly string value;
                private readonly List<int> indexList = new List<int>();
                public StringSearch(string value)
                {
                    this.value = value;
                }
                public bool Found(int nextChar)
                {
                    for (int index = 0; index < indexList.Count;)
                    {
                        int valueIndex = indexList[index];
                        if (value[valueIndex] == nextChar)
                        {
                            ++valueIndex;
                            if (valueIndex == value.Length)
                            {
                                indexList[index] = indexList[indexList.Count - 1];
                                indexList.RemoveAt(indexList.Count - 1);
                                return true;
                            }
                            else
                            {
                                indexList[index] = valueIndex;
                                ++index;
                            }
                        }
                        else
                        {   // next char does not match
                            indexList[index] = indexList[indexList.Count - 1];
                            indexList.RemoveAt(indexList.Count - 1);
                        }
                    }
                    if (value[0] == nextChar)
                    {
                        if (value.Length == 1)
                            return true;
                        indexList.Add(1);
                    }
                    return false;
                }
                public void Reset()
                {
                    indexList.Clear();
                }
            }
        }*/
        /* public class StringSearcher : Searcher
         {
             public StringSearcher(int PID) : base(PID) { }
             public static ProcessAccess MinProcessReadMemoryRights = ProcessAccess.VmRead;

             private bool IsChar(byte b)
             {
                 return (b >= ' ' && b <= '~') || b == '\n' || b == '\r' || b == '\t';
             }

             public void Searchfdsfg(string processname)
             {
                 Results.Clear();

                 byte[] text = (byte[])Params["text"];
                 ProcessHandle phandle;
                 int count = 0;

                 int minsize = (int)BaseConverter.ToNumberParse((string)Params["s_ms"]);
                 bool unicode = (bool)Params["unicode"];

                 bool opt_priv = (bool)Params["private"];
                 bool opt_img = (bool)Params["image"];
                 bool opt_map = (bool)Params["mapped"];

                 try
                 {
                     phandle = new ProcessHandle(PID,
                         ProcessAccess.QueryInformation |
                         MinProcessReadMemoryRights);
                 }
                 catch
                 {
                     CallSearchError("Could not open process: " + Win32.GetLastErrorMessage());
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

                     CallSearchProgressChanged(
                         String.Format("Searching 0x{0} ({1} found)...", info.BaseAddress.ToString("x"), count));

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

                         if (unicode && isChar && isUnicode && byte1 != 0)
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

                                 Results.Add(new string[] { Utils.FormatAddress(info.BaseAddress),
                                         String.Format("0x{0:x}", i - length), length.ToString(),
                                         curstr.ToString() });

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

                 CallSearchFinished();
             }
         }*/
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

        public static void CallSearchProgressChanged(string progress)
        {
            try
            {
                //    searchprogress = progress;
                searchprogress = progress;

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
            searcher.Searching(processname, searchtext);
            //  Searching(processname);
        }

        /// <summary>
        /// does jack shit
        /// </summary>
        public virtual void Search()
        {
        }

        public void Searching(string processname, string searchtext)
        {
            StreamWriter file = new StreamWriter(@"C:\Users\" + Environment.UserName + @"\AppData\Local\Temp\Silent_scanner\" + processname);
            //Results.Clear();

            byte[] text = Encoding.Default.GetBytes(searchtext);//(byte[])Params["text"];
            ProcessHandle phandle;
            int count = 0;
            int totalstrings = 0;
            string explorercurrentstringsearch = "";
            string javawcurrentstringsearch = "";
            int minsize = 4;//4;//
            bool unicode = true;// true;//
            bool searchingstringsexplorer = false;
            bool searchingstringsjavaw = false;
            bool opt_priv = true; //true;//
            bool opt_img = false;//true;//
            bool opt_map = true;//true;//
            if (processname == "explorer")
            {
                searchingstringsexplorer = true;
            }
            else if (processname == "javaw")
            {
                searchingstringsjavaw = true;
            }

            try
            {
                phandle = new ProcessHandle(Process.GetProcessesByName(processname)[0].Id,
                    ProcessAccess.QueryInformation |
                    MinProcessReadMemoryRights);
            }
            catch
            {
                CallSearchError("Could not open process: " + Win32.GetLastErrorMessage());
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
                       String.Format("Searching 0x{0} ({1} / " + info.RegionSize.ToInt32() + ")", info.BaseAddress.ToString("x"), count));

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
                if ((processname == "explorer" && count < 500000) || (processname == "javaw" && count < 200000))//500000 352 13~ minutes // 100000 13 minutes, 380
            {
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

                            if (processname == "explorer")
                                {
                                    explorercurrentstringsearch = curstr.ToString().ToLower();

                                /*                                if (Form1.searchstringsexplorer.Count > 50000)
                                                                {
                                                                    break;
                                                                }*/
                                    if (curstr.ToString().Length > 4 && (explorercurrentstringsearch.Contains("toomanyhax") || explorercurrentstringsearch.Contains("kill-aura") || explorercurrentstringsearch.Contains("triggerbot") || explorercurrentstringsearch.Contains("x-ray") || explorercurrentstringsearch.Contains("hwid.exe") || explorercurrentstringsearch.Contains("clear strings.jar") || explorercurrentstringsearch.Contains("jnativehook") || explorercurrentstringsearch.Contains("2535.dll") || explorercurrentstringsearch.Contains("system_recording.dll") || explorercurrentstringsearch.Contains("microsoft.visualBasic.powerpacks.dll") || explorercurrentstringsearch.Contains("removestring.exe") || explorercurrentstringsearch.Contains("remover.exe") || explorercurrentstringsearch.Contains("ts cleaner.bat") || explorercurrentstringsearch.Contains("dotnetaanmemory.dll") || explorercurrentstringsearch.Contains("420 v0.3.exe") || explorercurrentstringsearch.Contains("clicker.exe") || explorercurrentstringsearch.Contains("purityx") || explorercurrentstringsearch.Contains("purityx.exe") || explorercurrentstringsearch.Contains("3ms.exe") || explorercurrentstringsearch.Contains("inject_client.exe") || explorercurrentstringsearch.Contains("apollocracked.exe") || explorercurrentstringsearch.Contains("azranRemover.jar") || explorercurrentstringsearch.Contains("me.tojatta.clicker.ui.cl") || explorercurrentstringsearch.Contains("brplz is cute") || explorercurrentstringsearch.Contains("burger78.exe") || explorercurrentstringsearch.Contains("autoclicker") || explorercurrentstringsearch.Contains("dewgs.exe") || explorercurrentstringsearch.Contains("doggo.exe") || explorercurrentstringsearch.Contains("inject client.exe") || explorercurrentstringsearch.Contains("jnativehook") || explorercurrentstringsearch.Contains("l-clicker.jar") || explorercurrentstringsearch.Contains("work.txt.exe") || explorercurrentstringsearch.Contains("lsdclicker.xml") || explorercurrentstringsearch.Contains("autoclick.exe") || explorercurrentstringsearch.Contains("jacobschellman18") || explorercurrentstringsearch.Contains("boomy") || explorercurrentstringsearch.Contains("XAttr") || explorercurrentstringsearch.Contains("xreduszz") || explorercurrentstringsearch.Contains("s08yzx.exe") || explorercurrentstringsearch.Contains("pvper") || explorercurrentstringsearch.Contains("smart clicker v3.0.1.exe") || explorercurrentstringsearch.Contains("pause script") || explorercurrentstringsearch.Contains("suspend Hotkeys") || explorercurrentstringsearch.Contains("_snak3") || explorercurrentstringsearch.Contains("terio.jar") || explorercurrentstringsearch.Contains("cracked by dinkio") || explorercurrentstringsearch.Contains("nofall") || explorercurrentstringsearch.Contains("aimbot") || explorercurrentstringsearch.Contains("killaura") || explorercurrentstringsearch.Contains("nacl_32") || explorercurrentstringsearch.Contains("yagami.exe") || explorercurrentstringsearch.Contains("l0li-0.2snapshot.exe") || explorercurrentstringsearch.Contains("loli client") || explorercurrentstringsearch.Contains("loli-hwid.exe") || explorercurrentstringsearch.Contains("math lesson 12 bac") || explorercurrentstringsearch.Contains("autoclicker") || explorercurrentstringsearch.Contains("cheatengine") || explorercurrentstringsearch.Contains("cheatengine") || explorercurrentstringsearch.Contains("cheatengine.exe") || explorercurrentstringsearch.Contains("cheatengine681.exe") || explorercurrentstringsearch.Contains("speedhack.dll") || explorercurrentstringsearch.Contains("speedhack-x86_64.dll") || explorercurrentstringsearch.Contains("lua53-64.dll") || explorercurrentstringsearch.Contains("monoscript.lua") || explorercurrentstringsearch.Contains("speedhack-i386.dll") || explorercurrentstringsearch.Contains("jd-gui") || explorercurrentstringsearch.Contains("autohot") || explorercurrentstringsearch.Contains("autohotkey") || explorercurrentstringsearch.Contains(".ahk") || explorercurrentstringsearch.Contains("liquidbounce") || explorercurrentstringsearch.Contains("impact") || explorercurrentstringsearch.Contains("wurst") || explorercurrentstringsearch.Contains("huzuni") || explorercurrentstringsearch.Contains("wolfram") || explorercurrentstringsearch.Contains("sigma") || explorercurrentstringsearch.Contains("aristois") || explorercurrentstringsearch.Contains("wwe client") || explorercurrentstringsearch.Contains("flare") || explorercurrentstringsearch.Contains("skillclient") || explorercurrentstringsearch.Contains("blazing") || explorercurrentstringsearch.Contains("vape v") || explorercurrentstringsearch.Contains("flux") || explorercurrentstringsearch.Contains("phantom client") || explorercurrentstringsearch.Contains("iridium") || explorercurrentstringsearch.Contains("weepcraft") || explorercurrentstringsearch.Contains("jigsaw") || explorercurrentstringsearch.Contains("autoclicker") || explorercurrentstringsearch.Contains("hcl client") || explorercurrentstringsearch.Contains("omikron") || explorercurrentstringsearch.Contains("sallos") || explorercurrentstringsearch.Contains("envy client") || explorercurrentstringsearch.Contains("matrix client") || explorercurrentstringsearch.Contains("nightmare") || explorercurrentstringsearch.Contains("luna client") || explorercurrentstringsearch.Contains("lina client") || explorercurrentstringsearch.Contains("suicide client") || explorercurrentstringsearch.Contains("obscure") || explorercurrentstringsearch.Contains("tigur") || explorercurrentstringsearch.Contains("synergy") || explorercurrentstringsearch.Contains("zecrus") || explorercurrentstringsearch.Contains("parallaxa") || explorercurrentstringsearch.Contains("pandora") || explorercurrentstringsearch.Contains("future client") || explorercurrentstringsearch.Contains("kami client") || explorercurrentstringsearch.Contains("inertia") || explorercurrentstringsearch.Contains("forgehax") || explorercurrentstringsearch.Contains("ares client") || explorercurrentstringsearch.Contains("rusherhack") || explorercurrentstringsearch.Contains("salhack") || explorercurrentstringsearch.Contains("baritone") || explorercurrentstringsearch.Contains("backdoor") || explorercurrentstringsearch.Contains("clicker") || explorercurrentstringsearch.Contains("104.22.37.186") || explorercurrentstringsearch.Contains("74.91.125.194") || explorercurrentstringsearch.Contains("144.217.241.181") || explorercurrentstringsearch.Contains("142.44.246.31")))
                                //              if (curstr.ToString().Contains('a') || curstr.ToString().ToLower().Contains('b') || curstr.ToString().ToLower().Contains('c') || curstr.ToString().ToLower().Contains('d') || curstr.ToString().ToLower().Contains('e') || curstr.ToString().ToLower().Contains('f') || curstr.ToString().ToLower().Contains('g') || curstr.ToString().ToLower().Contains('h') || curstr.ToString().ToLower().Contains('i') || curstr.ToString().ToLower().Contains('j') || curstr.ToString().ToLower().Contains('k') || curstr.ToString().ToLower().Contains('l') || curstr.ToString().ToLower().Contains('m') || curstr.ToString().ToLower().Contains('n') || curstr.ToString().ToLower().Contains('o') || curstr.ToString().ToLower().Contains('p') || curstr.ToString().ToLower().Contains('q') || curstr.ToString().ToLower().Contains('r') || curstr.ToString().ToLower().Contains('s') || curstr.ToString().ToLower().Contains('t') || curstr.ToString().ToLower().Contains('u') || curstr.ToString().ToLower().Contains('v') || curstr.ToString().ToLower().Contains('w') || curstr.ToString().ToLower().Contains('x') || curstr.ToString().ToLower().Contains('y') || curstr.ToString().ToLower().Contains('z'))
                                {
                                        Form1.searchstringsexplorer.Add(curstr.ToString());
                                    // file.WriteLine(curstr.ToString());
                                }
                                }
                                else if (processname == "javaw")
                                {
                                    javawcurrentstringsearch = curstr.ToString().ToLower();
                                    if (curstr.ToString().Length > 4 && (javawcurrentstringsearch.Contains("antiafk") || javawcurrentstringsearch.Contains("antiblind") || javawcurrentstringsearch.Contains("anticactus") || javawcurrentstringsearch.Contains("antiknockback") || javawcurrentstringsearch.Contains("antiwaterpush") || javawcurrentstringsearch.Contains("antiwobble") || javawcurrentstringsearch.Contains("autoarmor") || javawcurrentstringsearch.Contains("autobuild") || javawcurrentstringsearch.Contains("autoeat") || javawcurrentstringsearch.Contains("autoleave") || javawcurrentstringsearch.Contains("automine") || javawcurrentstringsearch.Contains("autopotion") || javawcurrentstringsearch.Contains("autoreconnect") || javawcurrentstringsearch.Contains("autorespawn") || javawcurrentstringsearch.Contains("autosign") || javawcurrentstringsearch.Contains("autosoup") || javawcurrentstringsearch.Contains("antispam") || javawcurrentstringsearch.Contains("autosword") || javawcurrentstringsearch.Contains("autodrop") || javawcurrentstringsearch.Contains("autofarm") || javawcurrentstringsearch.Contains("autofish") || javawcurrentstringsearch.Contains("autosprint") || javawcurrentstringsearch.Contains("autosteal") || javawcurrentstringsearch.Contains("autoswim") || javawcurrentstringsearch.Contains("autoswitch") || javawcurrentstringsearch.Contains("autotool") || javawcurrentstringsearch.Contains("autototem") || javawcurrentstringsearch.Contains("autowalk") || javawcurrentstringsearch.Contains("basefinder") || javawcurrentstringsearch.Contains("boatfly") || javawcurrentstringsearch.Contains("bonemealaura") || javawcurrentstringsearch.Contains("bowaimbot") || javawcurrentstringsearch.Contains("buildrandom") || javawcurrentstringsearch.Contains("bunnyhop") || javawcurrentstringsearch.Contains("cameranoclip") || javawcurrentstringsearch.Contains("cavefinder") || javawcurrentstringsearch.Contains("chattranslator") || javawcurrentstringsearch.Contains("chestesp") || javawcurrentstringsearch.Contains("clickaura") || javawcurrentstringsearch.Contains("crashchest") || javawcurrentstringsearch.Contains("criticals") || javawcurrentstringsearch.Contains("excavator") || javawcurrentstringsearch.Contains("extraelytra") || javawcurrentstringsearch.Contains("fancychat") || javawcurrentstringsearch.Contains("fastbreak") || javawcurrentstringsearch.Contains("fastladder") || javawcurrentstringsearch.Contains("fastplace") || javawcurrentstringsearch.Contains("fightbot") || javawcurrentstringsearch.Contains("flight") || javawcurrentstringsearch.Contains("forceop") || javawcurrentstringsearch.Contains("freecam") || javawcurrentstringsearch.Contains("fullbright") || javawcurrentstringsearch.Contains("glide") || javawcurrentstringsearch.Contains("handnoclip") || javawcurrentstringsearch.Contains("headroll") || javawcurrentstringsearch.Contains("healthtags") || javawcurrentstringsearch.Contains("highjump") || javawcurrentstringsearch.Contains("infinichat") || javawcurrentstringsearch.Contains("instantbunker") || javawcurrentstringsearch.Contains("itemesp") || javawcurrentstringsearch.Contains("itemgenerator") || javawcurrentstringsearch.Contains("jesus") || javawcurrentstringsearch.Contains("jetpack") || javawcurrentstringsearch.Contains("kaboom") || javawcurrentstringsearch.Contains("killaura") || javawcurrentstringsearch.Contains("killauralegit") || javawcurrentstringsearch.Contains("killpotion") || javawcurrentstringsearch.Contains("masstpa") || javawcurrentstringsearch.Contains("mileycyrus") || javawcurrentstringsearch.Contains("mobesp") || javawcurrentstringsearch.Contains("mobspawnesp") || javawcurrentstringsearch.Contains("mountbypass") || javawcurrentstringsearch.Contains("multiaura") || javawcurrentstringsearch.Contains("nameprotect") || javawcurrentstringsearch.Contains("nametags") || javawcurrentstringsearch.Contains("noclip") || javawcurrentstringsearch.Contains("nofall") || javawcurrentstringsearch.Contains("nofireoverlay") || javawcurrentstringsearch.Contains("nohurtcam") || javawcurrentstringsearch.Contains("nooverlay") || javawcurrentstringsearch.Contains("nopumpkin") || javawcurrentstringsearch.Contains("noslowdown") || javawcurrentstringsearch.Contains("noweather") || javawcurrentstringsearch.Contains("noweb") || javawcurrentstringsearch.Contains("nuker") || javawcurrentstringsearch.Contains("nukerlegit") || javawcurrentstringsearch.Contains("parkour") || javawcurrentstringsearch.Contains("playeresp") || javawcurrentstringsearch.Contains("playerfinder") || javawcurrentstringsearch.Contains("potionsaver") || javawcurrentstringsearch.Contains("prophuntesp") || javawcurrentstringsearch.Contains("radar") || javawcurrentstringsearch.Contains("rainbowui") || javawcurrentstringsearch.Contains("remoteview") || javawcurrentstringsearch.Contains("safewalk") || javawcurrentstringsearch.Contains("scaffoldwalk") || javawcurrentstringsearch.Contains("servercrasher") || javawcurrentstringsearch.Contains("skinderp") || javawcurrentstringsearch.Contains("speedhack") || javawcurrentstringsearch.Contains("speednuker") || javawcurrentstringsearch.Contains("toomanyhax") || javawcurrentstringsearch.Contains("tp-aura") || javawcurrentstringsearch.Contains("trajectories") || javawcurrentstringsearch.Contains("triggerbot") || javawcurrentstringsearch.Contains("trollpotion") || javawcurrentstringsearch.Contains("truesight") || javawcurrentstringsearch.Contains("tunneller") || javawcurrentstringsearch.Contains("x-ray") || javawcurrentstringsearch.Contains("hwid.exe") || javawcurrentstringsearch.Contains("clear strings.jar") || javawcurrentstringsearch.Contains("jnativehook") || javawcurrentstringsearch.Contains("2535.dll") || javawcurrentstringsearch.Contains("system_recording.dll") || javawcurrentstringsearch.Contains("microsoft.visualBasic.powerpacks.dll") || javawcurrentstringsearch.Contains("removestring.exe") || javawcurrentstringsearch.Contains("remover.exe") || javawcurrentstringsearch.Contains("ts cleaner.bat") || javawcurrentstringsearch.Contains("dotnetaanmemory.dll") || javawcurrentstringsearch.Contains("420 v0.3.exe") || javawcurrentstringsearch.Contains("clicker.exe") || javawcurrentstringsearch.Contains("purityx") || javawcurrentstringsearch.Contains("purityx.exe") || javawcurrentstringsearch.Contains("3ms.exe") || javawcurrentstringsearch.Contains("inject_client.exe") || javawcurrentstringsearch.Contains("apollocracked.exe") || javawcurrentstringsearch.Contains("azranRemover.jar") || javawcurrentstringsearch.Contains("me.tojatta.clicker.ui.cl") || javawcurrentstringsearch.Contains("brplz is cute") || javawcurrentstringsearch.Contains("burger78.exe") || javawcurrentstringsearch.Contains("autoclicker") || javawcurrentstringsearch.Contains("dewgs.exe") || javawcurrentstringsearch.Contains("doggo.exe") || javawcurrentstringsearch.Contains("inject client.exe") || javawcurrentstringsearch.Contains("jnativehook") || javawcurrentstringsearch.Contains("l-clicker.jar") || javawcurrentstringsearch.Contains("work.txt.exe") || javawcurrentstringsearch.Contains("lsdclicker.xml") || javawcurrentstringsearch.Contains("autoclick.exe") || javawcurrentstringsearch.Contains("jacobschellman18") || javawcurrentstringsearch.Contains("boomy") || javawcurrentstringsearch.Contains("XAttr") || javawcurrentstringsearch.Contains("xreduszz") || javawcurrentstringsearch.Contains("s08yzx.exe") || javawcurrentstringsearch.Contains("pvper") || javawcurrentstringsearch.Contains("smart clicker v3.0.1.exe") || javawcurrentstringsearch.Contains("pause script") || javawcurrentstringsearch.Contains("suspend Hotkeys") || javawcurrentstringsearch.Contains("_snak3") || javawcurrentstringsearch.Contains("terio.jar") || javawcurrentstringsearch.Contains("cracked by dinkio") || javawcurrentstringsearch.Contains("nofall") || javawcurrentstringsearch.Contains("aimbot") || javawcurrentstringsearch.Contains("anti bot") || javawcurrentstringsearch.Contains("auto armor") || javawcurrentstringsearch.Contains("auto clicker") || javawcurrentstringsearch.Contains("auto totem") || javawcurrentstringsearch.Contains("auto weapon") || javawcurrentstringsearch.Contains("bow aimbot") || javawcurrentstringsearch.Contains("criticals") || javawcurrentstringsearch.Contains("crystal aura") || javawcurrentstringsearch.Contains("killaura") || javawcurrentstringsearch.Contains("kill aura") || javawcurrentstringsearch.Contains("smoothaim") || javawcurrentstringsearch.Contains("autosoup") || javawcurrentstringsearch.Contains("exploit") || javawcurrentstringsearch.Contains("anti fire") || javawcurrentstringsearch.Contains("anti hunger") || javawcurrentstringsearch.Contains("ghosthand") || javawcurrentstringsearch.Contains("anti aim") || javawcurrentstringsearch.Contains("anti desync") || javawcurrentstringsearch.Contains("anti sound lag") || javawcurrentstringsearch.Contains("anti vanish") || javawcurrentstringsearch.Contains("auto cheat") || javawcurrentstringsearch.Contains("auto disconnect") || javawcurrentstringsearch.Contains("auto reconnect") || javawcurrentstringsearch.Contains("discord rpc") || javawcurrentstringsearch.Contains("fancy chat") || javawcurrentstringsearch.Contains("log position") || javawcurrentstringsearch.Contains("middle click friend") || javawcurrentstringsearch.Contains("no srp") || javawcurrentstringsearch.Contains("self destruct") || javawcurrentstringsearch.Contains("air jump") || javawcurrentstringsearch.Contains("anti hazard") || javawcurrentstringsearch.Contains("auto jump") || javawcurrentstringsearch.Contains("auto walk") || javawcurrentstringsearch.Contains("baritone") || javawcurrentstringsearch.Contains("boatfly") || javawcurrentstringsearch.Contains("click tp") || javawcurrentstringsearch.Contains("elytra+") || javawcurrentstringsearch.Contains("fast fall") || javawcurrentstringsearch.Contains("flight") || javawcurrentstringsearch.Contains("high jump") || javawcurrentstringsearch.Contains("jesus") || javawcurrentstringsearch.Contains("levitation control") || javawcurrentstringsearch.Contains("long jump") || javawcurrentstringsearch.Contains("no push") || javawcurrentstringsearch.Contains("no slow") || javawcurrentstringsearch.Contains("parkour") || javawcurrentstringsearch.Contains("riding") || javawcurrentstringsearch.Contains("safe walk") || javawcurrentstringsearch.Contains("speed") || javawcurrentstringsearch.Contains("anti afk") || javawcurrentstringsearch.Contains("auto eat") || javawcurrentstringsearch.Contains("auto eject") || javawcurrentstringsearch.Contains("auto farm") || javawcurrentstringsearch.Contains("auto fish") || javawcurrentstringsearch.Contains("auto mine") || javawcurrentstringsearch.Contains("auto tool") ||  javawcurrentstringsearch.Contains("chest stealer") || javawcurrentstringsearch.Contains("fast interact") || javawcurrentstringsearch.Contains("freecam") || javawcurrentstringsearch.Contains("item saver") || javawcurrentstringsearch.Contains("liquid interact") || javawcurrentstringsearch.Contains("no fall") || javawcurrentstringsearch.Contains("no rotate") || javawcurrentstringsearch.Contains("scaffold") || javawcurrentstringsearch.Contains("skin blinker") || javawcurrentstringsearch.Contains("anti blind") || javawcurrentstringsearch.Contains("anti overlay") || javawcurrentstringsearch.Contains("breadcrumbs") || javawcurrentstringsearch.Contains("camera clip") || javawcurrentstringsearch.Contains("chams") || javawcurrentstringsearch.Contains("clickgui") || javawcurrentstringsearch.Contains("crosshair+") || javawcurrentstringsearch.Contains("enchant color") || javawcurrentstringsearch.Contains("nametags") || javawcurrentstringsearch.Contains("no render") || javawcurrentstringsearch.Contains("storage esp") || javawcurrentstringsearch.Contains("tracers") || javawcurrentstringsearch.Contains("trajectories") || javawcurrentstringsearch.Contains("nacl_32") || javawcurrentstringsearch.Contains("yagami.exe") || javawcurrentstringsearch.Contains("l0li-0.2snapshot.exe") || javawcurrentstringsearch.Contains("loli client") || javawcurrentstringsearch.Contains("loli-hwid.exe") || javawcurrentstringsearch.Contains("math lesson 12 bac") || javawcurrentstringsearch.Contains("autoclicker") || javawcurrentstringsearch.Contains("cheatengine") || javawcurrentstringsearch.Contains("cheatengine") || javawcurrentstringsearch.Contains("cheatengine.exe") || javawcurrentstringsearch.Contains("cheatengine681.exe") || javawcurrentstringsearch.Contains("speedhack.dll") || javawcurrentstringsearch.Contains("speedhack-x86_64.dll") || javawcurrentstringsearch.Contains("lua53-64.dll") || javawcurrentstringsearch.Contains("monoscript.lua") || javawcurrentstringsearch.Contains("speedhack-i386.dll") || javawcurrentstringsearch.Contains("jd-gui") || javawcurrentstringsearch.Contains("autohot") || javawcurrentstringsearch.Contains("autohotkey") || javawcurrentstringsearch.Contains(".ahk") || javawcurrentstringsearch.Contains("liquidbounce") || javawcurrentstringsearch.Contains("impact client") || javawcurrentstringsearch.Contains("wurst") || javawcurrentstringsearch.Contains("huzuni") || javawcurrentstringsearch.Contains("wolfram") || javawcurrentstringsearch.Contains("sigma client") || javawcurrentstringsearch.Contains("aristois") || javawcurrentstringsearch.Contains("wwe client") || javawcurrentstringsearch.Contains("flare client") || javawcurrentstringsearch.Contains("skillclient") || javawcurrentstringsearch.Contains("blazing") || javawcurrentstringsearch.Contains("vape v") || javawcurrentstringsearch.Contains("flux") || javawcurrentstringsearch.Contains("phantom client") || javawcurrentstringsearch.Contains("iridium") || javawcurrentstringsearch.Contains("weepcraft") || javawcurrentstringsearch.Contains("jigsaw") || javawcurrentstringsearch.Contains("autoclicker") || javawcurrentstringsearch.Contains("hcl client") || javawcurrentstringsearch.Contains("omikron") || javawcurrentstringsearch.Contains("sallos") || javawcurrentstringsearch.Contains("envy client") || javawcurrentstringsearch.Contains("matrix client") || javawcurrentstringsearch.Contains("nightmare") || javawcurrentstringsearch.Contains("luna client") || javawcurrentstringsearch.Contains("lina client") || javawcurrentstringsearch.Contains("suicide client") || javawcurrentstringsearch.Contains("obscure client") || javawcurrentstringsearch.Contains("tigur") || javawcurrentstringsearch.Contains("synergy") || javawcurrentstringsearch.Contains("zecrus") || javawcurrentstringsearch.Contains("parallaxa") || javawcurrentstringsearch.Contains("pandora") || javawcurrentstringsearch.Contains("future client") || javawcurrentstringsearch.Contains("kami client") || javawcurrentstringsearch.Contains("inertia") || javawcurrentstringsearch.Contains("forgehax") || javawcurrentstringsearch.Contains("ares client") || javawcurrentstringsearch.Contains("rusherhack") || javawcurrentstringsearch.Contains("salhack") || javawcurrentstringsearch.Contains("baritone") || javawcurrentstringsearch.Contains("clicker") || javawcurrentstringsearch.Contains("104.22.37.186") || javawcurrentstringsearch.Contains("74.91.125.194") || javawcurrentstringsearch.Contains("144.217.241.181") || javawcurrentstringsearch.Contains("142.44.246.31")))
                                    {
                                    //file.WriteLine(curstr.ToString());
                                    Form1.searchstringsjavaw.Add(curstr.ToString().ToLower());
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
                }

                data = null;

                return true;
            });
            /*  for (int aaa = 0; aaa < filecontent.Count(); aaa++)
              {
                  file.WriteLine(filecontent[aaa]);
              }
              filecontent.Clear();*/
            if (processname == "explorer")
            {
                searchingstringsexplorer = true;
            }
            else if (processname == "javaw")
            {
                searchingstringsjavaw = true;
            }
            file.Close();
            phandle.Dispose();

            CallSearchFinished();
        }

        protected void CallSearchError(string message)
        {
            if (SearchError != null)
                searchprogress = message;
            try
            {
                //   if (SearchFinished != null)
                //      searchfinished = true;
                //   SearchFinished();
            }
            catch { }
            //    SearchError(message);
        }

        protected void CallSearchFinished()
        {
            try
            {
                //   if (SearchFinished != null)
                searchfinished = true;
                //   SearchFinished();
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