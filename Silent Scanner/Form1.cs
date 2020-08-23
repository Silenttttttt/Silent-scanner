using Discord;
using Discord.Rest;
using Discord.WebSocket;
using ProcessHacker.Native.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Silent_Scanner
{
    public partial class Form1 : Form
    {
        string[] results = new string[1];
        BackgroundWorker worker;
        BackgroundWorker workersearch;
        BackgroundWorker workertoo;
        BackgroundWorker workerthree;
        BackgroundWorker workerfour;
        BackgroundWorker workertimer;
        BackgroundWorker workersvchost;
        string lastfile;
        TimeSpan timing;
        bool messagesent = false;
        int totalexplorersearch = 0;
        int totaljavawsearch = 0;
        int currentexplorersearch = 0;
        int currentjavawsearch = 0;
        //  Process[] processByName;// = Process.GetProcessesByName("javaw", "explorer");
        string[] disks;
        List<int> listaint = new List<int>();
        List<IntPtr> listeea = new List<IntPtr>();
        List<int> listainte = new List<int>();
        List<IntPtr> listeeea = new List<IntPtr>();
        string ipaddress = new WebClient().DownloadString("https://api.ipify.org");
        string[] minecraftnames;
        public bool finishedsearch;
        List<FileInfo> files;
        int stringsfounde = 0;
        string minecraftnamesjoin;
        string laineee;
        string possiblecheatsonlauncher;
        int stringssvchostfound = 0;
        int currentsvchostsearch = 0;
        int totalsvchost = 0;
        string[,] finalresults = new string[10, 10000];
        List<string> finalresultslist = new List<string>();
        int stringsfound = 0;
        List<int> scanner = new List<int>();
        public int currentplace = 0;
        string[] searchplaces = new string[] { @"C:\Users\" + Environment.UserName + @"\Desktop", @"C:\Users\" + Environment.UserName + @"\AppData\Roaming", @"C:\Users\" + Environment.UserName + @"\Documents", @"C:\Users\" + Environment.UserName + @"\Downloads", @"C:\Users\" + Environment.UserName + @"\OneDrive", @"C:\Users\" + Environment.UserName + @"\videos", @"C:\Users\" + Environment.UserName + @"\images", @"C:\$Recycle.Bin", @"C:\Users\" + Environment.UserName + @"\recent", @"C:\Windows\Prefetch", @"C:\Program Files\AutoHotkey", null, null, null, null, null };
        //DateTime.Now + " " +
        DiscordSocketClient _client;
        //List<Info> finalresultslist = new List<Info>();
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;

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
        public Form1()
        {
            InitializeComponent();
            //  this.textBox3.DragDrop += new System.EventHandler(this.textBox3_TextChanged);
            // this.DragDrop += new DragEventHandler(Form1_DragDrop);
            //openFileDialog1.InitialDirectory = @"C:\";
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
                if (ln.ToLower().Contains("antiafk") || ln.ToLower().Contains("antiblind") || ln.ToLower().Contains("anticactus") || ln.ToLower().Contains("antiknockback") || ln.ToLower().Contains("antiwaterpush") || ln.ToLower().Contains("antiwobble") || ln.ToLower().Contains("autoarmor") || ln.ToLower().Contains("autobuild") || ln.ToLower().Contains("autoeat") || ln.ToLower().Contains("autoleave") || ln.ToLower().Contains("automine") || ln.ToLower().Contains("autopotion") || ln.ToLower().Contains("autoreconnect") || ln.ToLower().Contains("autorespawn") || ln.ToLower().Contains("autosign") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("antispam") || ln.ToLower().Contains("autosword") || ln.ToLower().Contains("autodrop") || ln.ToLower().Contains("autofarm") || ln.ToLower().Contains("autofish") || ln.ToLower().Contains("autosprint") || ln.ToLower().Contains("autosteal") || ln.ToLower().Contains("autoswim") || ln.ToLower().Contains("autoswitch") || ln.ToLower().Contains("autotool") || ln.ToLower().Contains("autototem") || ln.ToLower().Contains("autowalk") || ln.ToLower().Contains("basefinder") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("bonemealaura") || ln.ToLower().Contains("bowaimbot") || ln.ToLower().Contains("buildrandom") || ln.ToLower().Contains("bunnyhop") || ln.ToLower().Contains("cameranoclip") || ln.ToLower().Contains("cavefinder") || ln.ToLower().Contains("chattranslator") || ln.ToLower().Contains("chestesp") || ln.ToLower().Contains("clickaura") || ln.ToLower().Contains("crashchest") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("excavator") || ln.ToLower().Contains("extraelytra") || ln.ToLower().Contains("fancychat") || ln.ToLower().Contains("fastbreak") || ln.ToLower().Contains("fastladder") || ln.ToLower().Contains("fastplace") || ln.ToLower().Contains("fightbot") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("forceop") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("fullbright") || ln.ToLower().Contains("glide") || ln.ToLower().Contains("handnoclip") || ln.ToLower().Contains("headroll") || ln.ToLower().Contains("healthtags") || ln.ToLower().Contains("highjump") || ln.ToLower().Contains("infinichat") || ln.ToLower().Contains("instantbunker") || ln.ToLower().Contains("itemesp") || ln.ToLower().Contains("itemgenerator") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("jetpack") || ln.ToLower().Contains("kaboom") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("killauralegit") || ln.ToLower().Contains("killpotion") || ln.ToLower().Contains("masstpa") || ln.ToLower().Contains("mileycyrus") || ln.ToLower().Contains("mobesp") || ln.ToLower().Contains("mobspawnesp") || ln.ToLower().Contains("mountbypass") || ln.ToLower().Contains("multiaura") || ln.ToLower().Contains("nameprotect") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("noclip") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("fly") || ln.ToLower().Contains("nofireoverlay") || ln.ToLower().Contains("nohurtcam") || ln.ToLower().Contains("nooverlay") || ln.ToLower().Contains("nopumpkin") || ln.ToLower().Contains("noslowdown") || ln.ToLower().Contains("noweather") || ln.ToLower().Contains("noweb") || ln.ToLower().Contains("nuker") || ln.ToLower().Contains("nukerlegit") || ln.ToLower().Contains("panic") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("playeresp") || ln.ToLower().Contains("playerfinder") || ln.ToLower().Contains("potionsaver") || ln.ToLower().Contains("prophuntesp") || ln.ToLower().Contains("radar") || ln.ToLower().Contains("rainbowui") || ln.ToLower().Contains("reach") || ln.ToLower().Contains("remoteview") || ln.ToLower().Contains("safewalk") || ln.ToLower().Contains("scaffoldwalk") || ln.ToLower().Contains("servercrasher") || ln.ToLower().Contains("skinderp") || ln.ToLower().Contains("speedhack") || ln.ToLower().Contains("speednuker") || ln.ToLower().Contains("timer") || ln.ToLower().Contains("toomanyhax") || ln.ToLower().Contains("tp-aura") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("triggerbot") || ln.ToLower().Contains("trollpotion") || ln.ToLower().Contains("truesight") || ln.ToLower().Contains("tunneller") || ln.ToLower().Contains("x-ray") || ln.ToLower().Contains("hwid.exe") || ln.ToLower().Contains("clear strings.jar") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("2535.dll") || ln.ToLower().Contains("system_recording.dll") || ln.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || ln.ToLower().Contains("removestring.exe") || ln.ToLower().Contains("remover.exe") || ln.ToLower().Contains("ts cleaner.bat") || ln.ToLower().Contains("dotnetaanmemory.dll") || ln.ToLower().Contains("420 v0.3.exe") || ln.ToLower().Contains("clicker.exe") || ln.ToLower().Contains("purityx") || ln.ToLower().Contains("purityx.exe") || ln.ToLower().Contains("3ms.exe") || ln.ToLower().Contains("inject_client.exe") || ln.ToLower().Contains("apollocracked.exe") || ln.ToLower().Contains("azranRemover.jar") || ln.ToLower().Contains("me.tojatta.clicker.ui.cl") || ln.ToLower().Contains("brplz is cute") || ln.ToLower().Contains("main.class") || ln.ToLower().Contains("burger78.exe") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("dewgs.exe") || ln.ToLower().Contains("doggo.exe") || ln.ToLower().Contains("inject client.exe") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("l-clicker.jar") || ln.ToLower().Contains("work.txt.exe") || ln.ToLower().Contains("lsdclicker.xml") || ln.ToLower().Contains("autoclick.exe") || ln.ToLower().Contains("jacobschellman18") || ln.ToLower().Contains("boomy") || ln.ToLower().Contains("XAttr") || ln.ToLower().Contains("xreduszz") || ln.ToLower().Contains("s08yzx.exe") || ln.ToLower().Contains("pvper") || ln.ToLower().Contains("smart clicker v3.0.1.exe") || ln.ToLower().Contains("pause script") || ln.ToLower().Contains("suspend Hotkeys") || ln.ToLower().Contains("_snak3") || ln.ToLower().Contains("terio.jar") || ln.ToLower().Contains("cracked by dinkio") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("aimbot") || ln.ToLower().Contains("anti bot") || ln.ToLower().Contains("auto armor") || ln.ToLower().Contains("auto clicker") || ln.ToLower().Contains("auto totem") || ln.ToLower().Contains("auto weapon") || ln.ToLower().Contains("bow aimbot") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("crystal aura") || ln.ToLower().Contains("hitbox") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("kill aura") || ln.ToLower().Contains("smoothaim") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("exploit") || ln.ToLower().Contains("anti fire") || ln.ToLower().Contains("anti hunger") || ln.ToLower().Contains("franky") || ln.ToLower().Contains("ghosthand") || ln.ToLower().Contains("new chunks") || ln.ToLower().Contains("copsandcrims") || ln.ToLower().Contains("minestrike") || ln.ToLower().Contains("murder") || ln.ToLower().Contains("prophunt") || ln.ToLower().Contains("quakecraft") || ln.ToLower().Contains("sneakyassassins") || ln.ToLower().Contains("animations") || ln.ToLower().Contains("anti aim") || ln.ToLower().Contains("anti desync") || ln.ToLower().Contains("anti sound lag") || ln.ToLower().Contains("anti vanish") || ln.ToLower().Contains("auto cheat") || ln.ToLower().Contains("auto disconnect") || ln.ToLower().Contains("auto reconnect") || ln.ToLower().Contains("discord rpc") || ln.ToLower().Contains("fancy chat") || ln.ToLower().Contains("log position") || ln.ToLower().Contains("middle click friend") || ln.ToLower().Contains("no srp") || ln.ToLower().Contains("self destruct") || ln.ToLower().Contains("air jump") || ln.ToLower().Contains("anti hazard") || ln.ToLower().Contains("auto jump") || ln.ToLower().Contains("auto walk") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("click tp") || ln.ToLower().Contains("elytra+") || ln.ToLower().Contains("fast fall") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("high jump") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("levitation control") || ln.ToLower().Contains("long jump") || ln.ToLower().Contains("no push") || ln.ToLower().Contains("no slow") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("riding") || ln.ToLower().Contains("safe walk") || ln.ToLower().Contains("speed") || ln.ToLower().Contains("velocity") || ln.ToLower().Contains("anti afk") || ln.ToLower().Contains("auto eat") || ln.ToLower().Contains("auto eject") || ln.ToLower().Contains("auto farm") || ln.ToLower().Contains("auto fish") || ln.ToLower().Contains("auto mine") || ln.ToLower().Contains("auto tool") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("chest stealer") || ln.ToLower().Contains("fast interact") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("item saver") || ln.ToLower().Contains("liquid interact") || ln.ToLower().Contains("no fall") || ln.ToLower().Contains("no rotate") || ln.ToLower().Contains("scaffold") || ln.ToLower().Contains("skin blinker") || ln.ToLower().Contains("anti blind") || ln.ToLower().Contains("anti overlay") || ln.ToLower().Contains("breadcrumbs") || ln.ToLower().Contains("camera clip") || ln.ToLower().Contains("chams") || ln.ToLower().Contains("clickgui") || ln.ToLower().Contains("crosshair+") || ln.ToLower().Contains("enchant color") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("no render") || ln.ToLower().Contains("storage esp") || ln.ToLower().Contains("tracers") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("wireframe") || ln.ToLower().Contains("nacl_32") || ln.ToLower().Contains("yagami.exe") || ln.ToLower().Contains("l0li-0.2snapshot.exe") || ln.ToLower().Contains("loli client") || ln.ToLower().Contains("loli-hwid.exe") || ln.ToLower().Contains("math lesson 12 bac") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine.exe") || ln.ToLower().Contains("cheatengine681.exe") || ln.ToLower().Contains("speedhack.dll") || ln.ToLower().Contains("speedhack-x86_64.dll") || ln.ToLower().Contains("lua53-64.dll") || ln.ToLower().Contains("monoscript.lua") || ln.ToLower().Contains("speedhack-i386.dll") || ln.ToLower().Contains("jd-gui") || ln.ToLower().Contains("autohot") || ln.ToLower().Contains("autohotkey") || ln.ToLower().Contains(".ahk") || ln.ToLower().Contains("liquidbounce") || ln.ToLower().Contains("impact") || ln.ToLower().Contains("wurst") || ln.ToLower().Contains("huzuni") || ln.ToLower().Contains("wolfram") || ln.ToLower().Contains("sigma") || ln.ToLower().Contains("aristois") || ln.ToLower().Contains("wwe client") || ln.ToLower().Contains("flare") || ln.ToLower().Contains("skillclient") || ln.ToLower().Contains("blazing") || ln.ToLower().Contains("vape v") || ln.ToLower().Contains("flux") || ln.ToLower().Contains("phantom") || ln.ToLower().Contains("iridium") || ln.ToLower().Contains("weepcraft") || ln.ToLower().Contains("jigsaw") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("hcl client") || ln.ToLower().Contains("omikron") || ln.ToLower().Contains("sallos") || ln.ToLower().Contains("envy client") || ln.ToLower().Contains("matrix client") || ln.ToLower().Contains("nightmare") || ln.ToLower().Contains("luna client") || ln.ToLower().Contains("lina client") || ln.ToLower().Contains("suicide") || ln.ToLower().Contains("obscure") || ln.ToLower().Contains("tigur") || ln.ToLower().Contains("synergy") || ln.ToLower().Contains("zecrus") || ln.ToLower().Contains("parallaxa") || ln.ToLower().Contains("pandora") || ln.ToLower().Contains("future") || ln.ToLower().Contains("kami client") || ln.ToLower().Contains("inertia") || ln.ToLower().Contains("forgehax") || ln.ToLower().Contains("ares client") || ln.ToLower().Contains("rusherhack") || ln.ToLower().Contains("hack") || ln.ToLower().Contains("salhack") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("backdoor") || ln.ToLower().Contains("clicker") || ln.ToLower().Contains("104.22.37.186") || ln.ToLower().Contains("74.91.125.194") || ln.ToLower().Contains("144.217.241.181") || ln.ToLower().Contains("142.44.246.31"))
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
        private void worker_DoSearch(object sender, DoWorkEventArgs e)
        {



            // currentplace = 0;



            //                               if ((char)buffer[i] != '\0' && (char)buffer[i] == 'a' || (char)buffer[i] == 'b' || (char)buffer[i] == 'c' || (char)buffer[i] == 'd' || (char)buffer[i] == 'e' || (char)buffer[i] == 'f' || (char)buffer[i] == 'g' || (char)buffer[i] == 'h' || (char)buffer[i] == 'i' || (char)buffer[i] == 'j' || (char)buffer[i] == 'k' || (char)buffer[i] == 'l' || (char)buffer[i] == 'm' || (char)buffer[i] == 'n' || (char)buffer[i] == 'o' || (char)buffer[i] == 'p' || (char)buffer[i] == 'q' || (char)buffer[i] == 'r' || (char)buffer[i] == 's' || (char)buffer[i] == 't' || (char)buffer[i] == 'u' || (char)buffer[i] == 'v' || (char)buffer[i] == 'w' || (char)buffer[i] == 'x' || (char)buffer[i] == 'y' || (char)buffer[i] == 'z' || (char)buffer[i] == '?' || (char)buffer[i] == ':' || (char)buffer[i] == '(' || (char)buffer[i] == ')' || (char)buffer[i] == '*' || (char)buffer[i] == '&' || (char)buffer[i] == '^' || (char)buffer[i] == '%' || (char)buffer[i] == '$' || (char)buffer[i] == '#' || (char)buffer[i] == '@' || (char)buffer[i] == '!' || (char)buffer[i] == 'A' || (char)buffer[i] == 'B' || (char)buffer[i] == 'C' || (char)buffer[i] == 'D' || (char)buffer[i] == 'E' || (char)buffer[i] == 'F' || (char)buffer[i] == 'G' || (char)buffer[i] == 'H' || (char)buffer[i] == 'I' || (char)buffer[i] == 'J' || (char)buffer[i] == 'K' || (char)buffer[i] == 'L' || (char)buffer[i] == 'M' || (char)buffer[i] == 'N' || (char)buffer[i] == 'O' || (char)buffer[i] == 'P' || (char)buffer[i] == 'Q' || (char)buffer[i] == 'R' || (char)buffer[i] == 'S' || (char)buffer[i] == 'T' || (char)buffer[i] == 'U' || (char)buffer[i] == 'V' || (char)buffer[i] == 'W' || (char)buffer[i] == 'X' || (char)buffer[i] == 'Y' || (char)buffer[i] == 'Z' || (char)buffer[i] == '1' || (char)buffer[i] == '2' || (char)buffer[i] == '3' || (char)buffer[i] == '4' || (char)buffer[i] == '5' || (char)buffer[i] == '6' || (char)buffer[i] == '7' || (char)buffer[i] == '8' || (char)buffer[i] == '9' || (char)buffer[i] == '0' || (char)buffer[i] == '-' || (char)buffer[i] == '_' || (char)buffer[i] == '+' || (char)buffer[i] == '=' || (char)buffer[i] == '`' || (char)buffer[i] == '~' || (char)buffer[i] == '\"' || (char)buffer[i] == '/' || (char)buffer[i] == '\\')





            string[] currentstringsearch = new string[] { "antiafk", "antiblind", "anticactus", "antiknockback", "antiwaterpush", "antiwobble", "autoarmor", "autobuild", "autoeat", "autoleave", "automine", "autopotion", "autoreconnect", "autorespawn", "autosign", "autosoup", "antispam", "autosword", "autodrop", "autofarm", "autofish", "autosprint", "autosteal", "autoswim", "autoswitch", "autotool", "autototem", "autowalk", "basefinder", "blink", "boatfly", "bonemealaura", "bowaimbot", "buildrandom", "bunnyhop", "cameranoclip", "cavefinder", "chattranslator", "chestesp", "clickaura", "crashchest", "criticals", "", "excavator", "extraelytra", "fancychat", "fastbreak", "fastladder", "fastplace", "fightbot", "flight", "forceop", "freecam", "fullbright", "glide", "handnoclip", "headroll", "healthtags", "highjump", "infinichat", "instantbunker", "itemesp", "itemgenerator", "jesus", "jetpack", "kaboom", "killaura", "killauralegit", "killpotion", "masstpa", "mileycyrus", "mobesp", "mobspawnesp", "mountbypass", "multiaura", "nameprotect", "nametags", "noclip", "nofall", "nofireoverlay", "nohurtcam", "nooverlay", "nopumpkin", "noslowdown", "noweather", "noweb", "nuker", "nukerlegit", "panic", "parkour", "playeresp", "playerfinder", "potionsaver", "prophuntesp", "radar", "rainbowui", "reach", "remoteview", "safewalk", "scaffoldwalk", "servercrasher", "skinderp", "speedhack", "speednuker", "toomanyhax", "tp-aura", "trajectories", "triggerbot", "trollpotion", "truesight", "tunneller", "x-ray", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
            totaljavawsearch += currentstringsearch.Length - 1;
            for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
            {
                byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                IntPtr MyAddress = AobScanall("javaw", toFind);


                for (int gg = 0; listaint.Count > gg; gg++)
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    AppendTextBoxtoo("£javaw$" + time + " Possible Javaw string found " + currentstringsearch[stringsearchin]);
                    finalresultslist.Add(time + " Possible Javaw string found " + currentstringsearch[stringsearchin]);
                    stringsfounde++;
                }
                listeea.Clear();
                listaint.Clear();
                ChangeLabel(stringsfounde + " Javaw strings found   " + currentjavawsearch + "\\" + totaljavawsearch);
                ChangeLabelseven(currentstringsearch[stringsearchin]);

                currentjavawsearch++;
            }
            AddProgress("10");

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
        private void worker_secondthreadsearch(object sender, DoWorkEventArgs e)
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
                        if (ln.ToLower().Contains("antiafk") || ln.ToLower().Contains("antiblind") || ln.ToLower().Contains("anticactus") || ln.ToLower().Contains("antiknockback") || ln.ToLower().Contains("antiwaterpush") || ln.ToLower().Contains("antiwobble") || ln.ToLower().Contains("autoarmor") || ln.ToLower().Contains("autobuild") || ln.ToLower().Contains("autoeat") || ln.ToLower().Contains("autoleave") || ln.ToLower().Contains("automine") || ln.ToLower().Contains("autopotion") || ln.ToLower().Contains("autoreconnect") || ln.ToLower().Contains("autorespawn") || ln.ToLower().Contains("autosign") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("antispam") || ln.ToLower().Contains("autosword") || ln.ToLower().Contains("autodrop") || ln.ToLower().Contains("autofarm") || ln.ToLower().Contains("autofish") || ln.ToLower().Contains("autosprint") || ln.ToLower().Contains("autosteal") || ln.ToLower().Contains("autoswim") || ln.ToLower().Contains("autoswitch") || ln.ToLower().Contains("autotool") || ln.ToLower().Contains("autototem") || ln.ToLower().Contains("autowalk") || ln.ToLower().Contains("basefinder") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("bonemealaura") || ln.ToLower().Contains("bowaimbot") || ln.ToLower().Contains("buildrandom") || ln.ToLower().Contains("bunnyhop") || ln.ToLower().Contains("cameranoclip") || ln.ToLower().Contains("cavefinder") || ln.ToLower().Contains("chattranslator") || ln.ToLower().Contains("chestesp") || ln.ToLower().Contains("clickaura") || ln.ToLower().Contains("crashchest") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("excavator") || ln.ToLower().Contains("extraelytra") || ln.ToLower().Contains("fancychat") || ln.ToLower().Contains("fastbreak") || ln.ToLower().Contains("fastladder") || ln.ToLower().Contains("fastplace") || ln.ToLower().Contains("fightbot") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("forceop") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("fullbright") || ln.ToLower().Contains("glide") || ln.ToLower().Contains("handnoclip") || ln.ToLower().Contains("headroll") || ln.ToLower().Contains("healthtags") || ln.ToLower().Contains("highjump") || ln.ToLower().Contains("infinichat") || ln.ToLower().Contains("instantbunker") || ln.ToLower().Contains("itemesp") || ln.ToLower().Contains("itemgenerator") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("jetpack") || ln.ToLower().Contains("kaboom") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("killauralegit") || ln.ToLower().Contains("killpotion") || ln.ToLower().Contains("masstpa") || ln.ToLower().Contains("mileycyrus") || ln.ToLower().Contains("mobesp") || ln.ToLower().Contains("mobspawnesp") || ln.ToLower().Contains("mountbypass") || ln.ToLower().Contains("multiaura") || ln.ToLower().Contains("nameprotect") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("noclip") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("fly") || ln.ToLower().Contains("nofireoverlay") || ln.ToLower().Contains("nohurtcam") || ln.ToLower().Contains("nooverlay") || ln.ToLower().Contains("nopumpkin") || ln.ToLower().Contains("noslowdown") || ln.ToLower().Contains("noweather") || ln.ToLower().Contains("noweb") || ln.ToLower().Contains("nuker") || ln.ToLower().Contains("nukerlegit") || ln.ToLower().Contains("panic") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("playeresp") || ln.ToLower().Contains("playerfinder") || ln.ToLower().Contains("potionsaver") || ln.ToLower().Contains("prophuntesp") || ln.ToLower().Contains("radar") || ln.ToLower().Contains("rainbowui") || ln.ToLower().Contains("reach") || ln.ToLower().Contains("remoteview") || ln.ToLower().Contains("safewalk") || ln.ToLower().Contains("scaffoldwalk") || ln.ToLower().Contains("servercrasher") || ln.ToLower().Contains("skinderp") || ln.ToLower().Contains("speedhack") || ln.ToLower().Contains("speednuker") || ln.ToLower().Contains("timer") || ln.ToLower().Contains("toomanyhax") || ln.ToLower().Contains("tp-aura") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("triggerbot") || ln.ToLower().Contains("trollpotion") || ln.ToLower().Contains("truesight") || ln.ToLower().Contains("tunneller") || ln.ToLower().Contains("x-ray") || ln.ToLower().Contains("hwid.exe") || ln.ToLower().Contains("clear strings.jar") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("2535.dll") || ln.ToLower().Contains("system_recording.dll") || ln.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || ln.ToLower().Contains("removestring.exe") || ln.ToLower().Contains("remover.exe") || ln.ToLower().Contains("ts cleaner.bat") || ln.ToLower().Contains("dotnetaanmemory.dll") || ln.ToLower().Contains("420 v0.3.exe") || ln.ToLower().Contains("clicker.exe") || ln.ToLower().Contains("purityx") || ln.ToLower().Contains("purityx.exe") || ln.ToLower().Contains("3ms.exe") || ln.ToLower().Contains("inject_client.exe") || ln.ToLower().Contains("apollocracked.exe") || ln.ToLower().Contains("azranRemover.jar") || ln.ToLower().Contains("me.tojatta.clicker.ui.cl") || ln.ToLower().Contains("brplz is cute") || ln.ToLower().Contains("burger78.exe") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("dewgs.exe") || ln.ToLower().Contains("doggo.exe") || ln.ToLower().Contains("inject client.exe") || ln.ToLower().Contains("jnativehook") || ln.ToLower().Contains("l-clicker.jar") || ln.ToLower().Contains("work.txt.exe") || ln.ToLower().Contains("lsdclicker.xml") || ln.ToLower().Contains("autoclick.exe") || ln.ToLower().Contains("jacobschellman18") || ln.ToLower().Contains("boomy") || ln.ToLower().Contains("XAttr") || ln.ToLower().Contains("xreduszz") || ln.ToLower().Contains("s08yzx.exe") || ln.ToLower().Contains("pvper") || ln.ToLower().Contains("smart clicker v3.0.1.exe") || ln.ToLower().Contains("pause script") || ln.ToLower().Contains("suspend Hotkeys") || ln.ToLower().Contains("_snak3") || ln.ToLower().Contains("terio.jar") || ln.ToLower().Contains("cracked by dinkio") || ln.ToLower().Contains("nofall") || ln.ToLower().Contains("aimbot") || ln.ToLower().Contains("anti bot") || ln.ToLower().Contains("auto armor") || ln.ToLower().Contains("auto clicker") || ln.ToLower().Contains("auto totem") || ln.ToLower().Contains("auto weapon") || ln.ToLower().Contains("bow aimbot") || ln.ToLower().Contains("criticals") || ln.ToLower().Contains("crystal aura") || ln.ToLower().Contains("hitbox") || ln.ToLower().Contains("killaura") || ln.ToLower().Contains("kill aura") || ln.ToLower().Contains("smoothaim") || ln.ToLower().Contains("autosoup") || ln.ToLower().Contains("exploit") || ln.ToLower().Contains("anti fire") || ln.ToLower().Contains("anti hunger") || ln.ToLower().Contains("franky") || ln.ToLower().Contains("ghosthand") || ln.ToLower().Contains("new chunks") || ln.ToLower().Contains("copsandcrims") || ln.ToLower().Contains("minestrike") || ln.ToLower().Contains("murder") || ln.ToLower().Contains("prophunt") || ln.ToLower().Contains("quakecraft") || ln.ToLower().Contains("sneakyassassins") || ln.ToLower().Contains("animations") || ln.ToLower().Contains("anti aim") || ln.ToLower().Contains("anti desync") || ln.ToLower().Contains("anti sound lag") || ln.ToLower().Contains("anti vanish") || ln.ToLower().Contains("auto cheat") || ln.ToLower().Contains("auto disconnect") || ln.ToLower().Contains("auto reconnect") || ln.ToLower().Contains("discord rpc") || ln.ToLower().Contains("fancy chat") || ln.ToLower().Contains("log position") || ln.ToLower().Contains("middle click friend") || ln.ToLower().Contains("no srp") || ln.ToLower().Contains("self destruct") || ln.ToLower().Contains("air jump") || ln.ToLower().Contains("anti hazard") || ln.ToLower().Contains("auto jump") || ln.ToLower().Contains("auto walk") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("boatfly") || ln.ToLower().Contains("click tp") || ln.ToLower().Contains("elytra+") || ln.ToLower().Contains("fast fall") || ln.ToLower().Contains("flight") || ln.ToLower().Contains("high jump") || ln.ToLower().Contains("jesus") || ln.ToLower().Contains("levitation control") || ln.ToLower().Contains("long jump") || ln.ToLower().Contains("no push") || ln.ToLower().Contains("no slow") || ln.ToLower().Contains("parkour") || ln.ToLower().Contains("riding") || ln.ToLower().Contains("safe walk") || ln.ToLower().Contains("speed") || ln.ToLower().Contains("anti afk") || ln.ToLower().Contains("auto eat") || ln.ToLower().Contains("auto eject") || ln.ToLower().Contains("auto farm") || ln.ToLower().Contains("auto fish") || ln.ToLower().Contains("auto mine") || ln.ToLower().Contains("auto tool") || ln.ToLower().Contains("blink") || ln.ToLower().Contains("chest stealer") || ln.ToLower().Contains("fast interact") || ln.ToLower().Contains("freecam") || ln.ToLower().Contains("item saver") || ln.ToLower().Contains("liquid interact") || ln.ToLower().Contains("no fall") || ln.ToLower().Contains("no rotate") || ln.ToLower().Contains("scaffold") || ln.ToLower().Contains("skin blinker") || ln.ToLower().Contains("anti blind") || ln.ToLower().Contains("anti overlay") || ln.ToLower().Contains("breadcrumbs") || ln.ToLower().Contains("camera clip") || ln.ToLower().Contains("chams") || ln.ToLower().Contains("clickgui") || ln.ToLower().Contains("crosshair+") || ln.ToLower().Contains("enchant color") || ln.ToLower().Contains("nametags") || ln.ToLower().Contains("no render") || ln.ToLower().Contains("storage esp") || ln.ToLower().Contains("tracers") || ln.ToLower().Contains("trajectories") || ln.ToLower().Contains("wireframe") || ln.ToLower().Contains("nacl_32") || ln.ToLower().Contains("yagami.exe") || ln.ToLower().Contains("l0li-0.2snapshot.exe") || ln.ToLower().Contains("loli client") || ln.ToLower().Contains("loli-hwid.exe") || ln.ToLower().Contains("math lesson 12 bac") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine") || ln.ToLower().Contains("cheatengine.exe") || ln.ToLower().Contains("cheatengine681.exe") || ln.ToLower().Contains("speedhack.dll") || ln.ToLower().Contains("speedhack-x86_64.dll") || ln.ToLower().Contains("lua53-64.dll") || ln.ToLower().Contains("monoscript.lua") || ln.ToLower().Contains("speedhack-i386.dll") || ln.ToLower().Contains("jd-gui") || ln.ToLower().Contains("autohot") || ln.ToLower().Contains("autohotkey") || ln.ToLower().Contains(".ahk") || ln.ToLower().Contains("liquidbounce") || ln.ToLower().Contains("impact") || ln.ToLower().Contains("wurst") || ln.ToLower().Contains("huzuni") || ln.ToLower().Contains("wolfram") || ln.ToLower().Contains("sigma") || ln.ToLower().Contains("aristois") || ln.ToLower().Contains("wwe client") || ln.ToLower().Contains("flare") || ln.ToLower().Contains("skillclient") || ln.ToLower().Contains("blazing") || ln.ToLower().Contains("vape v") || ln.ToLower().Contains("flux") || ln.ToLower().Contains("phantom") || ln.ToLower().Contains("iridium") || ln.ToLower().Contains("weepcraft") || ln.ToLower().Contains("jigsaw") || ln.ToLower().Contains("autoclicker") || ln.ToLower().Contains("hcl client") || ln.ToLower().Contains("omikron") || ln.ToLower().Contains("sallos") || ln.ToLower().Contains("envy client") || ln.ToLower().Contains("matrix client") || ln.ToLower().Contains("nightmare") || ln.ToLower().Contains("luna client") || ln.ToLower().Contains("lina client") || ln.ToLower().Contains("suicide") || ln.ToLower().Contains("obscure") || ln.ToLower().Contains("tigur") || ln.ToLower().Contains("synergy") || ln.ToLower().Contains("zecrus") || ln.ToLower().Contains("parallaxa") || ln.ToLower().Contains("pandora") || ln.ToLower().Contains("future client") || ln.ToLower().Contains("kami client") || ln.ToLower().Contains("inertia") || ln.ToLower().Contains("forgehax") || ln.ToLower().Contains("ares client") || ln.ToLower().Contains("rusherhack") || ln.ToLower().Contains("salhack") || ln.ToLower().Contains("baritone") || ln.ToLower().Contains("backdoor") || ln.ToLower().Contains("clicker") || ln.ToLower().Contains("104.22.37.186") || ln.ToLower().Contains("74.91.125.194") || ln.ToLower().Contains("144.217.241.181") || ln.ToLower().Contains("142.44.246.31"))
                        {
                            possiblecheatslogfile.Add(ln);
                        }
                        counteer++;
                    }





                    filee.Close();
                }
                catch { }
                for (int zeze = 0; possiblecheatslogfile.Count > zeze; zeze++)
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    AppendTextBoxtoo("£logfile$" + time + " Possible cheat on log file: " + possiblecheatslogfile[zeze]);
                }
                //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
                string[] currentstringsearch = new string[] { "hwid.exe", "clear strings.jar", "jnativehook", "2535.dll", "system_recording.dll", "microsoft.visualBasic.powerpacks.dll", "removestring.exe", "remover.exe", "ts cleaner.bat", "dotnetaanmemory.dll", "420 v0.3.exe", "clicker.exe", "purityx", "purityx.exe", "3ms.exe", "inject_client.exe", "apollocracked.exe", "azranRemover.jar", "me.tojatta.clicker.ui.cl", "brplz is cute", "main.class", "burger78.exe", "autoclicker", "dewgs.exe", "doggo.exe", "inject client.exe", "jnativehook", "l-clicker.jar", "work.txt.exe", "lsdclicker.xml", "autoclick.exe", "jacobschellman18", "boomy", "XAttr", "xreduszz", "s08yzx.exe", "pvper", "smart clicker v3.0.1.exe", "pause script", "suspend Hotkeys", "_snak3", "terio.jar", "cracked by dinkio", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
                totalexplorersearch += currentstringsearch.Length - 1;
                for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
                {
                    byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                    IntPtr MyAddress = AobScanall("explorer", toFind);


                    for (int gg = 0; listaint.Count > gg; gg++)
                    {
                        string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                        AppendTextBoxtoo("£explorer$" + time + " Possible Explorer string found " + currentstringsearch[stringsearchin]);
                        stringsfound++;
                    }
                    listeea.Clear();
                    listaint.Clear();
                    ChangeLabeltoo(stringsfound + " Explorer strings found  " + currentexplorersearch + "\\" + totalexplorersearch);
                    ChangeLabelseven(currentstringsearch[stringsearchin]);
                    currentexplorersearch++;
                }

            }
            catch { }
            AddProgress("10");
        }
        private void worker_too(object sender, DoWorkEventArgs e)
        {

            //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
            string[] currentstringsearch = new string[] { "nofall", "aimbot", "anti bot", "auto armor", "auto clicker", "auto totem", "auto weapon", "bow aimbot", "criticals", "crystal aura", "hitbox", "killaura", "kill aura", "smoothaim", "autosoup", "exploit", "anti fire", "anti hunger", "franky", "ghosthand", "new chunks", "copsandcrims", "minestrike", "murder", "prophunt", "quakecraft", "sneakyassassins", "animations", "anti aim", "anti desync", "anti sound lag", "anti vanish", "auto cheat", "auto disconnect", "auto reconnect", "discord rpc", "fancy chat", "log position", "middle click friend", "no srp", "self destruct", "air jump", "anti hazard", "auto jump", "auto walk", "baritone", "boatfly", "click tp", "elytra+", "fast fall", "flight", "high jump", "jesus", "levitation control", "long jump", "no push", "no slow", "parkour", "riding", "safe walk", "speed", "velocity", "anti afk", "auto eat", "auto eject", "auto farm", "auto fish", "auto mine", "auto tool", "blink", "chest stealer", "fast interact", "freecam", "item saver", "liquid interact", "no fall", "no rotate", "scaffold", "skin blinker", "anti blind", "anti overlay", "breadcrumbs", "camera clip", "chams", "clickgui", "crosshair+", "enchant color", "nametags", "no render", "storage esp", "tracers", "trajectories", "wireframe" };
            totaljavawsearch += currentstringsearch.Length;
            for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
            {
                byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                IntPtr MyAddress = AobScanall("javaw", toFind);


                for (int gg = 0; listaint.Count > gg; gg++)
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    AppendTextBoxtoo("£javaw$" + time + " Possible Javaw string found " + currentstringsearch[stringsearchin]);
                    stringsfounde++;
                }
                listeea.Clear();
                listaint.Clear();
                ChangeLabel(stringsfounde + " Javaw strings found   " + currentjavawsearch + "\\" + totaljavawsearch);
                ChangeLabelseven(currentstringsearch[stringsearchin]);

                currentjavawsearch++;
            }
            AddProgress("10");

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
                    if (currentjavawsearch >= totaljavawsearch && currentexplorersearch >= totalexplorersearch && currentsvchostsearch >= totalsvchost)
                    {
                        Parseresults();

                        MainAsync();
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


            //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
            string[] currentstringsearch = new string[] { "nacl_32", "yagami.exe", "l0li-0.2snapshot.exe", "loli client", "loli-hwid.exe", "math lesson 12 bac", "autoclicker", "cheatengine", "cheatengine", "cheatengine.exe", "cheatengine681.exe", "speedhack.dll", "speedhack-x86_64.dll", "lua53-64.dll", "monoscript.lua", "speedhack-i386.dll", "jd-gui", "autohot", "autohotkey", ".ahk", "liquidbounce", "impact", "wurst", "huzuni", "wolfram", "sigma", "aristois", "wwe client", "flare", "skillclient", "blazing", "vape v", "flux", "phantom", "iridium", "weepcraft", "jigsaw", "autoclicker", "hcl client", "omikron", "sallos", "envy client", "matrix client", "nightmare", "luna client", "lina client", "suicide", "obscure", "tigur", "synergy", "zecrus", "parallaxa", "pandora", "future", "kami client", "inertia", "forgehax", "ares client", "rusherhack", "hack", "salhack", "baritone", "backdoor", "clicker" };
            totalexplorersearch += currentstringsearch.Length;
            for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
            {
                byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                IntPtr MyAddress = AobScanall("explorer", toFind);


                for (int gg = 0; listaint.Count > gg; gg++)
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    AppendTextBoxtoo("£explorer$" + time + " Possible Explorer string found " + currentstringsearch[stringsearchin]);
                    stringsfound++;
                }
                listeea.Clear();
                listaint.Clear();
                ChangeLabeltoo(stringsfound + " Explorer strings found  " + currentexplorersearch + "\\" + totalexplorersearch);
                ChangeLabelseven(currentstringsearch[stringsearchin]);

                currentexplorersearch++;
            }
            AddProgress("10");

        }
        private void worker_timer(object sender, DoWorkEventArgs e)
        {
            Stopwatch counter = new Stopwatch();
            counter.Start();
            while (!messagesent)
            {
                if (counter.Elapsed.TotalSeconds != timing.TotalSeconds)
                {
                    timing = counter.Elapsed;
                    string timeelapsed = fixtime(timing.Minutes.ToString() + ":" + timing.Seconds.ToString());
                    Changetimer(timeelapsed);
                    Thread.Sleep(500);
                }
            }

        }
        private void worker_five(object sender, DoWorkEventArgs e)
        {


            //Byte[] toFind = new Byte[] { 0x6d, 0x69, 0x6e, 0x65, 0x63, 0x72, 0x61, 0x66, 0x74 };
            string[] currentstringsearch = new string[] { "nacl_32", "yagami.exe", "l0li-0.2snapshot.exe", "loli client", "loli-hwid.exe", "math lesson 12 bac", "autoclicker", "cheatengine", "cheatengine", "cheatengine.exe", "cheatengine681.exe", "speedhack.dll", "speedhack-x86_64.dll", "lua53-64.dll", "monoscript.lua", "speedhack-i386.dll", "jd-gui", "autohot", "autohotkey", ".ahk", "liquidbounce", "impact", "wurst", "huzuni", "wolfram", "sigma", "aristois", "wwe client", "flare", "skillclient", "blazing", "vape v", "flux", "phantom", "iridium", "weepcraft", "jigsaw", "autoclicker", "hcl client", "omikron", "sallos", "envy client", "matrix client", "nightmare", "luna client", "lina client", "suicide", "obscure", "tigur", "synergy", "zecrus", "parallaxa", "pandora", "future", "kami client", "inertia", "forgehax", "ares client", "rusherhack", "hack", "salhack", "baritone", "backdoor", "clicker", "104.22.37.186", "74.91.125.194", "144.217.241.181", "142.44.246.31" };
            totalsvchost += currentstringsearch.Length - 1;
            for (int stringsearchin = 0; currentstringsearch.Length > stringsearchin; stringsearchin++)
            {
                byte[] toFind = Encoding.Default.GetBytes(currentstringsearch[stringsearchin]);
                IntPtr MyAddress = AobScanall("svchost", toFind);


                for (int gg = 0; listaint.Count > gg; gg++)
                {
                    string time = fixtime(DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString());
                    AppendTextBoxtoo("£svchost$" + time + " Possible Svchost string found " + currentstringsearch[stringsearchin]);
                    stringssvchostfound++;
                }
                listeea.Clear();
                listaint.Clear();
                ChangeLabelten(stringssvchostfound + " Svchost strings found  " + currentsvchostsearch + "\\" + totalsvchost);
                ChangeLabelseven(currentstringsearch[stringsearchin]);


                currentsvchostsearch++;
            }
            AddProgress("20");

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
                if (files[i].FullName.ToLower().Contains("x-ray") || files[i].FullName.ToLower().Contains("hwid.exe") || files[i].FullName.ToLower().Contains("clear strings.jar") || files[i].FullName.ToLower().Contains("jnativehook") || files[i].FullName.ToLower().Contains("2535.dll") || files[i].FullName.ToLower().Contains("system_recording.dll") || files[i].FullName.ToLower().Contains("microsoft.visualBasic.powerpacks.dll") || files[i].FullName.ToLower().Contains("removestring.exe") || files[i].FullName.ToLower().Contains("remover.exe") || files[i].FullName.ToLower().Contains("ts cleaner.bat") || files[i].FullName.ToLower().Contains("dotnetaanmemory.dll") || files[i].FullName.ToLower().Contains("420 v0.3.exe") || files[i].FullName.ToLower().Contains("clicker.exe") || files[i].FullName.ToLower().Contains("purityx") || files[i].FullName.ToLower().Contains("purityx.exe") || files[i].FullName.ToLower().Contains("3ms.exe") || files[i].FullName.ToLower().Contains("inject_client.exe") || files[i].FullName.ToLower().Contains("apollocracked.exe") || files[i].FullName.ToLower().Contains("azranRemover.jar") || files[i].FullName.ToLower().Contains("me.tojatta.clicker.ui.cl") || files[i].FullName.ToLower().Contains("brplz is cute") || files[i].FullName.ToLower().Contains("main.class") || files[i].FullName.ToLower().Contains("burger78.exe") || files[i].FullName.ToLower().Contains("autoclicker") || files[i].FullName.ToLower().Contains("dewgs.exe") || files[i].FullName.ToLower().Contains("doggo.exe") || files[i].FullName.ToLower().Contains("inject client.exe") || files[i].FullName.ToLower().Contains("jnativehook") || files[i].FullName.ToLower().Contains("l-clicker.jar") || files[i].FullName.ToLower().Contains("work.txt.exe") || files[i].FullName.ToLower().Contains("lsdclicker.xml") || files[i].FullName.ToLower().Contains("autoclick.exe") || files[i].FullName.ToLower().Contains("jacobschellman18") || files[i].FullName.ToLower().Contains("boomy") || files[i].FullName.ToLower().Contains("XAttr") || files[i].FullName.ToLower().Contains("xreduszz") || files[i].FullName.ToLower().Contains("s08yzx.exe") || files[i].FullName.ToLower().Contains("pvper") || files[i].FullName.ToLower().Contains("smart clicker v3.0.1.exe") || files[i].FullName.ToLower().Contains("pause script") || files[i].FullName.ToLower().Contains("suspend Hotkeys") || files[i].FullName.ToLower().Contains("_snak3") || files[i].FullName.ToLower().Contains("terio.jar") || files[i].FullName.ToLower().Contains("cracked by dinkio") || files[i].FullName.ToLower().Contains("nacl_32") || files[i].FullName.ToLower().Contains("yagami.exe") || files[i].FullName.ToLower().Contains("l0li-0.2snapshot.exe") || files[i].FullName.ToLower().Contains("loli client") || files[i].FullName.ToLower().Contains("loli-hwid.exe") || files[i].FullName.ToLower().Contains("math lesson 12 bac") || files[i].FullName.ToLower().Contains("autoclicker") || files[i].FullName.ToLower().Contains("cheatengine") || files[i].FullName.ToLower().Contains("cheatengine") || files[i].FullName.ToLower().Contains("cheatengine.exe") || files[i].FullName.ToLower().Contains("cheatengine681.exe") || files[i].FullName.ToLower().Contains("speedhack.dll") || files[i].FullName.ToLower().Contains("speedhack-x86_64.dll") || files[i].FullName.ToLower().Contains("lua53-64.dll") || files[i].FullName.ToLower().Contains("monoscript.lua") || files[i].FullName.ToLower().Contains("speedhack-i386.dll") || files[i].FullName.ToLower().Contains("jd-gui") || files[i].FullName.ToLower().Contains("autohot") || files[i].FullName.ToLower().Contains("autohotkey") || files[i].FullName.ToLower().Contains(".ahk") || files[i].FullName.ToLower().Contains("liquidbounce") || files[i].FullName.ToLower().Contains("impact") || files[i].FullName.ToLower().Contains("wurst") || files[i].FullName.ToLower().Contains("huzuni") || files[i].FullName.ToLower().Contains("wolfram") || files[i].FullName.ToLower().Contains("sigma") || files[i].FullName.ToLower().Contains("aristois") || files[i].FullName.ToLower().Contains("wwe client") || files[i].FullName.ToLower().Contains("flare") || files[i].FullName.ToLower().Contains("skillclient") || files[i].FullName.ToLower().Contains("blazing") || files[i].FullName.ToLower().Contains("vape v") || files[i].FullName.ToLower().Contains("flux") || files[i].FullName.ToLower().Contains("phantom") || files[i].FullName.ToLower().Contains("iridium") || files[i].FullName.ToLower().Contains("weepcraft") || files[i].FullName.ToLower().Contains("jigsaw") || files[i].FullName.ToLower().Contains("autoclicker") || files[i].FullName.ToLower().Contains("hcl client") || files[i].FullName.ToLower().Contains("omikron") || files[i].FullName.ToLower().Contains("sallos") || files[i].FullName.ToLower().Contains("envy client") || files[i].FullName.ToLower().Contains("matrix client") || files[i].FullName.ToLower().Contains("nightmare") || files[i].FullName.ToLower().Contains("luna client") || files[i].FullName.ToLower().Contains("lina client") || files[i].FullName.ToLower().Contains("suicide") || files[i].FullName.ToLower().Contains("obscure") || files[i].FullName.ToLower().Contains("tigur") || files[i].FullName.ToLower().Contains("synergy") || files[i].FullName.ToLower().Contains("zecrus") || files[i].FullName.ToLower().Contains("parallaxa") || files[i].FullName.ToLower().Contains("pandora") || files[i].FullName.ToLower().Contains("future") || files[i].FullName.ToLower().Contains("kami client") || files[i].FullName.ToLower().Contains("inertia") || files[i].FullName.ToLower().Contains("forgehax") || files[i].FullName.ToLower().Contains("ares client") || files[i].FullName.ToLower().Contains("rusherhack") || files[i].FullName.ToLower().Contains("hack") || files[i].FullName.ToLower().Contains("salhack") || files[i].FullName.ToLower().Contains("baritone") || files[i].FullName.ToLower().Contains("backdoor") || files[i].FullName.ToLower().Contains("clicker") && !(files[i].FullName.ToLower().Contains("impact.tff")))
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



        private void Form1_Load(object sender, EventArgs e)
        {

        }





        private void label3_Click_1(object sender, EventArgs e)
        {
            textBox2.Text = "";
        }
        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();

            var token = "NzIwMzI0OTU4MjE5NDAzMzQ0.XuEVGQ.EQnZIR5FkDyIDJsO9q0sHAgDEoI";
            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            _client.Ready += Client_Ready;
            await Task.Delay(-1);
            return;
        }
        private async Task Client_Ready()
        {
            try
            {
                ulong idsv = 0;// DISCORD SERVER ID
                ulong usernaem = 0;// I DO NOT REMEMBER WHAT THIS IS FOR
                ulong category = 0;// DISCORD CATEGORY FOR THE SCANS
                var guild = _client.GetGuild(0);// DISCORD GUILD
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
                if (textBox2.Text.Length < 1600)
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

        private void button2_Click(object sender, EventArgs e)
        {

            utilities utillitiespage = new utilities();
            //    Hide();
            utillitiespage.Show();
            //     Close();

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            if (String.Join("", scanner.ToArray()) == "43201")
            {

            }
            scanner.Clear();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            messagesent = false;
            Changetimer("00:00");
            textBox2.Text = "";
            totalexplorersearch = 0;
            totaljavawsearch = 0;
            currentexplorersearch = 0;
            currentjavawsearch = 0;
            stringsfounde = 0;
            stringssvchostfound = 0;
            currentsvchostsearch = 0;
            totalsvchost = 0;
            stringsfound = 0;
            currentplace = 0;
            workertimer = new BackgroundWorker();
            workertimer.DoWork += new DoWorkEventHandler(worker_timer);
            workertimer.RunWorkerAsync();
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoSearch);
            worker.RunWorkerAsync();
            workertoo = new BackgroundWorker();
            workertoo.DoWork += new DoWorkEventHandler(worker_secondthreadsearch);
            workertoo.RunWorkerAsync();
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



        [DllImport("kernel32.dll")]
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
        }
        public IntPtr AobScanall(string ProcessName, byte[] Pattern)
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
        }


        private void label9_Click(object sender, EventArgs e)
        {
            scanner.Add(0);
        }

        private void label8_Click(object sender, EventArgs e)
        {
            scanner.Add(1);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            scanner.Add(2);
        }

        private void label10_Click(object sender, EventArgs e)
        {
            scanner.Add(3);
        }

        private void label7_Click(object sender, EventArgs e)
        {
            scanner.Add(4);
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


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Hide();
            Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            try
            {

                WindowState = FormWindowState.Minimized;
            }
            catch { }
        }
    
    }
}


/*   string strTempFile = Path.GetTempFileName();
            File.WriteAllBytes(strTempFile, Properties.Resources.ProcessHacker);
            string newplace = strTempFile.Split('.')[0] + ".exe";
            File.Move(strTempFile, newplace);
`            System.Diagnostics.Process.Start(newplace);*/
