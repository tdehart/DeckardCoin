using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ReadWriteMemory;
using PostMessageFuncs;

namespace AHBot
{
    public partial class AHBotForm : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(int hWnd, ref RECT lpRect);
        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        private PostMessage send = new PostMessage();
        private D3Interface D3Interface;
        private SQF SearchQueryFilters = new SQF();
        private int sleep_time_1;
        private int sleep_time_2;
        private int clickDelay;
        private int buyoutVariance;
        private int queryResultsDelay;
        private int numberOfChars;
        static bool is_running = false;
        static bool is_testing = false;
        static bool is_buying = false;
        static bool testModeEnabled = false;
        static bool safeModeEnabled = false;
        static int proc_id;
        static bool haltSearch = false;
        bool lastQueryWasLegendary = false;
        public Dictionary<string, int[]> queries = new Dictionary<string, int[]>();
        public List<string> qnames = new List<string>();
        int handle;

        public AHBotForm()
        {
            InitializeComponent();
        }

        static string ExtractNumbers(string expr) { return string.Join(null, System.Text.RegularExpressions.Regex.Split(expr, "[^\\d]")); }

        private void AHBotForm_Load(object sender, EventArgs e)
        {
            string version = "v0.3.2";
            versionLabel.Text = version;
            Process[] p = Process.GetProcessesByName("Diablo III");
            foreach (Process proc in p) { processIdsInput.Items.Add("Proc ID:" + proc.Id); }
            if (p.Length > 0)
                processIdsInput.SelectedIndex = 0;
            string path = Directory.GetCurrentDirectory();
            string[] filePaths = Directory.GetFiles(path);
            bool fileExists = false;
            foreach (string file in filePaths)
            {
                if (Path.GetExtension(file) == ".xml")
                {
                    itemFilesInput.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    fileExists = true;
                }
            }
            if (fileExists)
                itemFilesInput.SelectedIndex = 0;
            classSelectionInput.SelectedIndex = 1;
            queries.Clear();
            qnames.Clear();
            System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();
            ToolTip1.SetToolTip(this.testModeCheckbox, "The bot will search for but not buy items in test mode. Useful for testing new item configs.");
            stopButton.Enabled = false;
        }

        private void loadQueriesFromFile(string file)
        {
            queries.Clear();
            qnames.Clear();

            XDocument xdoc = XDocument.Load(file);

            int buyoutValue;

            var items = from i in XElement.Load(file).Elements("item")
                        select i;

            int queryIndex = 0;
            foreach (var item in items)
            {
                if (item.Element("name") == null)
                {
                    string itemLabel;
                    int typeIndex;
                    int qualityIndex;
                    int stat1Index;
                    int stat1Value;
                    int stat2Index;
                    int stat2Value;
                    int stat3Index;
                    int stat3Value;

                    if (item.Attribute("label") != null) itemLabel = item.Attribute("label").Value;
                    else itemLabel = "Unknown Item";
                    
                    if (item.Element("type") != null) typeIndex = SQF.GetSubTypeByName(item.Element("type").Value);
                    else throw new ArgumentNullException("Check your item file -- one of the item types is missing");

                    if (item.Element("quality") != null) qualityIndex = SQF.GetQualityByName(item.Element("quality").Value);
                    else throw new ArgumentNullException("Check your item file -- one of the item qualities is missing");

                    if (item.Element("stat1Name") != null) stat1Index = SQF.GetStatByName(item.Element("stat1Name").Value);
                    else throw new ArgumentNullException("Check your item file -- one of the stat1Names is missing or you didn't provide a stat1Name for an item");

                    if (item.Element("stat1Value") != null) stat1Value = int.Parse(item.Element("stat1Value").Value);
                    else throw new ArgumentNullException("Check your item file -- one of the Stat1 values is missing or you didn't provide a stat1Value value for an item");

                    if (item.Element("stat2Name") != null) stat2Index = SQF.GetStatByName(item.Element("stat2Name").Value);
                    else stat2Index = 0;

                    if (item.Element("stat2Value") != null) stat2Value = int.Parse(item.Element("stat2Value").Value);
                    else stat2Value = -1;

                    if (item.Element("stat3Name") != null) stat3Index = SQF.GetStatByName(item.Element("stat3Name").Value);
                    else stat3Index = 0;

                    if (item.Element("stat3Value") != null) stat3Value = int.Parse(item.Element("stat3Value").Value);
                    else stat3Value = -1;

                    if (item.Element("buyoutValue") != null) buyoutValue = int.Parse(item.Element("buyoutValue").Value);
                    else throw new ArgumentNullException("Check your item file -- one of the buyout values is incorrect or missing");

                    queries.Add("None"+queryIndex, new int[] { typeIndex, qualityIndex, stat1Index, stat1Value, stat2Index, stat2Value, stat3Index, stat3Value, buyoutValue });
                    qnames.Add(itemLabel);
                    queryIndex++;
                }
                else
                {
                    string legendaryName = item.Element("name").Value;

                    if (item.Element("buyoutValue") != null) buyoutValue = int.Parse(item.Element("buyoutValue").Value);
                    else throw new ArgumentNullException("Check your item file -- one of the buyout values is incorrect or missing");

                    queries.Add(legendaryName, new int[] { 0, 0, 0, 0, 0, 0, 0, 0, buyoutValue });
                    qnames.Add(legendaryName);
                }
                
            }
            lbActivityConsole("Items in config: " + queryIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Get process ID
            proc_id = Convert.ToInt32(ExtractNumbers(processIdsInput.SelectedItem.ToString()));
            lbActivityConsole("Starting Bot with process id: " + proc_id);

            //Grab variables from the Form GUI
            if (testModeCheckbox.Checked)
            {
                lbActivityConsole("Test Mode is enabled.");
                testModeEnabled = true;
            }
            if (safeModeCheckbox.Checked)
            {
                lbActivityConsole("Safe Mode is enabled.");
                safeModeEnabled = true;
            }
            queryResultsDelay = Convert.ToInt32(sResultsDelayTextbox.Text);
            clickDelay = Convert.ToInt32(delayTextBox.Text);
            numberOfChars = Convert.ToInt32(numCharsInput.Text);
            buyoutVariance = Convert.ToInt32(buyoutVarianceInput.Text);
            D3Interface = new D3Interface(proc_id, clickDelay, queryResultsDelay, numberOfChars, classSelectionInput.SelectedIndex, testModeEnabled, buyoutVariance, safeModeEnabled);
            handle = (int)Process.GetProcessById(proc_id).MainWindowHandle;            
            sleep_time_1 = Convert.ToInt32(searchLowerBoundInput.Text);
            sleep_time_2 = Convert.ToInt32(searchUpperBoundInput.Text);

            //Resize the window if needed
            var rect = new RECT();
            GetWindowRect(handle, ref rect);
            var result = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
            if (result.Width == 816 && result.Height == 638)
                lbActivityConsole("Window size correct.");
            else
            {
                SetWindowPos(Process.GetProcessById(Convert.ToInt32(ExtractNumbers(processIdsInput.SelectedItem.ToString()))).MainWindowHandle, (IntPtr)null, 100, 100, 816, 632, 0u);
                Thread.Sleep(500);
                //Clicks the border of the window to resize the in-game UI
                int X = 911;
                int Y = 702;
                Cursor.Position = new System.Drawing.Point(X, Y);
                mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                Thread.Sleep(2000);
                lbActivityConsole("Window resized.");
            }
            
            //Load the item config that is selected
            lbActivityConsole("Loading item config...");
            loadQueriesFromFile(itemFilesInput.Text + ".xml");

            //Grey out the Form GUI controls
            safeModeCheckbox.Enabled = false;
            testModeCheckbox.Enabled = false;
            startButton.Enabled = false;
            classSelectionInput.Enabled = false;
            numCharsInput.Enabled = false;
            delayTextBox.Enabled = false;
            itemFilesInput.Enabled = false;
            searchUpperBoundInput.Enabled = false;
            searchLowerBoundInput.Enabled = false;
            processIdsInput.Enabled = false;
            sResultsDelayTextbox.Enabled = false;
            buyoutVarianceInput.Enabled = false;
            itemFileRefreshButton.Enabled = false;
            pidRefreshButton.Enabled = false;
            stopButton.Enabled = true;

            //is_testing = true;
            is_running = true;
            haltSearch = false;

            //Start the thread
            startButton.Text = "Running...";
            lbActivityConsole("Bot Running...");
            if (is_running)
            {
                D3Interface.ResetQuery();
                Thread.Sleep(500);
                is_buying = false;
            }
            new Thread(RefreshThread).Start();
        }

        void RefreshThread()
        {
            while (!haltSearch)
            {
                while (is_running && !is_buying && !haltSearch)
                {
                    int qnamesCount = 0; 
                    foreach (var query in queries)
                    {
                        string itemLabel = qnames[qnamesCount];
                        qnamesCount++;
                        string queryKey = query.Key;
                        int[] queryValue = query.Value;

                        lbActivityConsole("");
                        lbActivityConsole("Searching for: " + itemLabel + " (" + queryValue[8].ToString("N0") + ")");

                        //Workaround for the slowness of sending delete keys
                        //Without this extra delay the next search will be messed up
                        if (lastQueryWasLegendary && queryKey.StartsWith("None"))
                        {
                            Thread.Sleep(1500);
                            lastQueryWasLegendary = false;
                        }
                            

                        D3Interface.LoadAHQuery(queryKey, queryValue);
                        Thread.Sleep(clickDelay);
                        D3Interface.SearchAH();
                        lbActivityConsole("Waiting " + queryResultsDelay + "ms for search results");
                        Thread.Sleep(queryResultsDelay);
                        is_buying = true;
                        D3Interface.BuyItems(queryKey, queryValue, itemLabel);
                        is_buying = false;

                        int pauseDuration = new Random().Next(sleep_time_1, sleep_time_2);
                        lbActivityConsole("Waiting " + pauseDuration + "ms before next search");
                        Thread.Sleep(pauseDuration);

                        if (queryKey.StartsWith("None"))
                            D3Interface.ResetSearchStats();
                        else
                        {
                            D3Interface.ResetSearchName(queryKey);
                            lastQueryWasLegendary = true;
                        }

                        if (haltSearch)
                        {
                            lbActivityConsole("");
                            lbActivityConsole("Bot stopped");
                            lbActivityConsole("");
                            break;
                        }
                    }
                }

                while (is_testing && !haltSearch)
                {
                    //int buyout1 = D3Interface.GetAHValue(new List<int>() { 0, 0xC, 0xD8 });
                    //int buyout2 = D3Interface.GetAHValue(new List<int>() { 0, 0xC, 0x1F0 });
                    //int buyout3 = D3Interface.GetAHValue(new List<int>() { 0, 0xC, 0x54, 0, 0x2B4 });

                    //int itemCount = D3Interface.GetAHValue(new List<int>() { 0, 0x4, 0x64 });

                    //lbActivityConsole("Item count:" + itemCount);

                    //lbActivityConsole("" + buyout1);
                    //lbActivityConsole("" + buyout2);
                    //lbActivityConsole("" + buyout3);

                    var rect = new RECT();
                    GetWindowRect(handle, ref rect);
                    var result = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);
                    lbActivityConsole("height:" + result.Height + " width: " + result.Width);

                    Thread.Sleep(5000);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e) { Environment.Exit(0); }
        private void AHBotForm_FormClosed_1(object sender, FormClosedEventArgs e) { Environment.Exit(0); }

        private void stopButton_Click(object sender, EventArgs e)
        {
            haltSearch = true;
            safeModeCheckbox.Enabled = true;
            testModeCheckbox.Enabled = true;
            startButton.Enabled = true;
            startButton.Text = "Start";
            stopButton.Enabled = false;
            classSelectionInput.Enabled = true;
            numCharsInput.Enabled = true;
            delayTextBox.Enabled = true;
            itemFilesInput.Enabled = true;
            searchUpperBoundInput.Enabled = true;
            searchLowerBoundInput.Enabled = true;
            processIdsInput.Enabled = true;
            sResultsDelayTextbox.Enabled = true;
            buyoutVarianceInput.Enabled = true;
            itemFileRefreshButton.Enabled = true;
            pidRefreshButton.Enabled = true;
        }
        

        public void lbActivityConsole(string comment)
        {
            activityLogBox.Invoke(new MethodInvoker(delegate
            {
                activityLogBox.Items.Add(comment);
                int NumberOfItems = activityLogBox.ClientSize.Height / activityLogBox.ItemHeight;
                if (activityLogBox.TopIndex == activityLogBox.Items.Count - NumberOfItems - 1)
                {
                    activityLogBox.TopIndex = activityLogBox.Items.Count - NumberOfItems + 1;
                }
            }));
            logActivityConsole(comment);
        }

        public void lbSuccessConsole(string comment)
        {
            boughtItemsBox.Invoke(new MethodInvoker(delegate
            {
                boughtItemsBox.Items.Add(comment);
                int NumberOfItems = boughtItemsBox.ClientSize.Height / boughtItemsBox.ItemHeight;
                if (boughtItemsBox.TopIndex == boughtItemsBox.Items.Count - NumberOfItems - 1)
                {
                    boughtItemsBox.TopIndex = boughtItemsBox.Items.Count - NumberOfItems + 1;
                }
            }));
            logSuccessConsole(comment);
        }

        private void itemConfigRefreshButton_Click(object sender, EventArgs e)
        {
            itemFilesInput.Items.Clear();
            string path = Directory.GetCurrentDirectory();
            string[] filePaths = Directory.GetFiles(path);
            bool fileExists = false;
            foreach (string file in filePaths)
            {
                if (Path.GetExtension(file) == ".xml")
                {
                    itemFilesInput.Items.Add(System.IO.Path.GetFileNameWithoutExtension(file));
                    fileExists = true;
                }
            }
            if (fileExists)
                itemFilesInput.SelectedIndex = 0;
            lbActivityConsole("XML list refreshed!");
        }

        private void pidRefreshButton_Click(object sender, EventArgs e)
        {
            processIdsInput.Items.Clear();
            Process[] p = Process.GetProcessesByName("Diablo III");
            foreach (Process proc in p) { processIdsInput.Items.Add("Proc ID:" + proc.Id); }
            if (p.Length > 0)
                processIdsInput.SelectedIndex = 0;
            lbActivityConsole("Process list refreshed!");
        }
        public void logActivityConsole(String message)
        {
            DateTime datet = DateTime.Now;
            String filePath = "pid" + proc_id + "_" + datet.ToString("MM-dd") + "_activity.log";
            if (!File.Exists(filePath))
            {
                FileStream files = File.Create(filePath);
                files.Close();
            }
            try
            {
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine("pid" + proc_id + "_" + datet.ToString("MM/dd hh:mm") + "> " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                lbActivityConsole(e.Message.ToString());
            }
        }
        public void logSuccessConsole(String message)
        {
            DateTime datet = DateTime.Now;
            String filePath = "pid" + proc_id + "_" + datet.ToString("MM-dd") + "_bought.log";
            if (!File.Exists(filePath))
            {
                FileStream files = File.Create(filePath);
                files.Close();
            }
            try
            {
                StreamWriter sw = File.AppendText(filePath);
                sw.WriteLine("pid" + proc_id + "-" + datet.ToString("MM/dd hh:mm") + "> " + message);
                sw.Flush();
                sw.Close();
            }
            catch (Exception e)
            {
                lbActivityConsole(e.Message.ToString());
            }
        }
    }
}
