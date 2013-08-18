using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using ReadWriteMemory;
using PostMessageFuncs;

namespace AHBot
{
    class D3Interface
    {
        static int proc_id;
        static int handle;
        private int currentClass = 0;
        private int currentSubType = 0;
        private int currentSupType = 0;
        private int currentQuality = 0;
        private int[] currentStats = new int[3] { 0, 0, 0 };
        private int numCharacters = 1;
        private int defaultClass;
        private double optionsPerScroll = 1.65;
        private int optionHeight = 18;
        private int statsPerPage = 10;
        private int shortPauseDuration;
        private int queryResultsDelay;
        private int buyoutVariance;
        private bool safeMode;
        private bool testMode = false;
        private PostMessage send = new PostMessage();
        private SQF SQF = new SQF();
        private AHBotForm frm = AHBotForm.ActiveForm as AHBotForm;
        private int boughtCount = 0;

        public D3Interface(int id, int pauseDuration, int querySearchDelay, int numCharacters, int defaultClass, bool testMode, int buyoutVariance, bool safeMode)
        {
            proc_id = id;
            handle = (int)Process.GetProcessById(proc_id).MainWindowHandle;
            shortPauseDuration = pauseDuration;
            this.numCharacters = numCharacters;
            this.defaultClass = defaultClass;
            currentClass = defaultClass;
            this.testMode = testMode;
            this.queryResultsDelay = querySearchDelay;
            this.buyoutVariance = buyoutVariance;
            this.safeMode = safeMode;
        }

        public int GetAHValue(List<int> offsets)
        {
            ProcessMemory memory = new ProcessMemory(proc_id);
            memory.StartProcess();
            int result = memory.ReadInt(memory.ImageAddress() + 0x00FC75C0);
            foreach (int item in offsets)
            {
                result = memory.ReadInt(result += item);
            }

            return result;
        }

        public void SearchAH()
        {
            int X = new Random().Next(118, 204);
            int Y = new Random().Next(458, 472);
            send.MouseClick(handle, "left", X, Y);
        }

        public void BuyItems(string name, int[] query, string itemLabel)
        {
            int itemCount = GetAHValue(new List<int>() { 0, 0x4, 0x64 });
            frm.lbActivityConsole("Number of results: " + itemCount);

            if (itemCount > 2 || safeMode)
            {
                frm.lbActivityConsole("Potentially incorrect search. Resubmitting.");
                ResetSearchStats();
                LoadAHQuery(name, query, false);
                Thread.Sleep(shortPauseDuration);
                SearchAH();
                frm.lbActivityConsole("Waiting " + queryResultsDelay + "ms for search results");
                Thread.Sleep(queryResultsDelay);
                itemCount = GetAHValue(new List<int>() { 0, 0x4, 0x64 });
                frm.lbActivityConsole("Resubmitted search found " + itemCount + " results");
            }

            //Only attempt to buy things if there are exactly 1 item OR exactly 2 items
            if (itemCount == 1 || itemCount == 2)
            {
                int buyout1 = GetAHValue(new List<int>() { 0, 0xC, 0xD8 });
                int buyout2 = GetAHValue(new List<int>() { 0, 0xC, 0xD8 + (1 * 0x118) });
                frm.lbActivityConsole("Row 1 buyout price is: " + buyout1);
                frm.lbActivityConsole("Row 2 buyout price is: " + buyout2);

                int currentBuyout = query[8] + buyoutVariance;

                if (buyout1 > 0 && buyout1 <= currentBuyout)
                {
                    boughtCount++;
                    BuyItem(1);
                    frm.lbActivityConsole("Buying item 1");
                    frm.lbSuccessConsole(boughtCount + ": Bought " + itemLabel + " for " + buyout1.ToString() + ".");
                }
                if (itemCount == 2 && buyout2 > 0 && buyout2 <= currentBuyout) //row2 moves up to row1, so just buy row1 again
                {
                    boughtCount++;
                    frm.lbActivityConsole("Buying item 2");
                    frm.lbSuccessConsole(boughtCount + ": Bought " + itemLabel + " for " + buyout2.ToString() + ".");
                    BuyItem(1);
                }
            }
        }

        private void BuyItem(int row)
        {
            if (testMode)
            {
                Thread.Sleep(10000);
                return;
            }
            int Y;
            switch (row)
            {
                case 1:
                    //Row 1 coords
                    Y = new Random().Next(173, 182);
                    break;
                case 2:
                    //Row 2 coords
                    Y = new Random().Next(195, 210);
                    break;
                case 3:
                    //Row 3 coords
                    Y = new Random().Next(220, 232);
                    break;
                default:
                    throw new ArgumentNullException("Can't find given row number");
            }

            /////////////////////////////////////////////////////////////
            int X = new Random().Next(293, 653); //Row X-coords
            send.MouseClick(handle, "left", X, Y);
            Thread.Sleep(new Random().Next(10, 90));
            X = new Random().Next(640, 715); //Lower-right buyout button X
            Y = new Random().Next(481, 492); //Lower-right buyout button Y
            send.MouseClick(handle, "left", X, Y);
            Thread.Sleep(new Random().Next(10, 90));
            X = new Random().Next(309, 380); //Middle buyout button X
            Y = new Random().Next(392, 409); //Middle buyout button Y
            send.MouseClick(handle, "left", X, Y);
            Thread.Sleep(2500);
            X = new Random().Next(367, 438);
            Y = new Random().Next(247, 264);
            send.MouseClick(handle, "left", X, Y);
            Thread.Sleep(new Random().Next(10, 90));
        }

        public void SelectOption(int option, int optionIndex, int totalOptionCount, int optionsPerPage)
        {
            int X;
            int Y;
            int firstOptionY;

            //Start by getting the X/Y values of the ComboBoxes and the Y value of the first selectable option
            switch (option)
            {
                case 0: //Class
                    X = new Random().Next(115, 200);
                    Y = new Random().Next(163, 178);
                    firstOptionY = 189;
                    break;
                case 1: //SupType
                    X = new Random().Next(115, 200);
                    Y = new Random().Next(205, 218);
                    firstOptionY = 230;
                    break;
                case 2: //SubType
                    X = new Random().Next(115, 200);
                    Y = new Random().Next(231, 245);
                    firstOptionY = 256;
                    break;
                case 3: //Rarity
                    X = new Random().Next(178, 207);
                    Y = new Random().Next(275, 290);
                    firstOptionY = 301;
                    break;
                case 4: //Preferred Stat 1
                    X = new Random().Next(110, 175);
                    Y = new Random().Next(330, 340);
                    firstOptionY = 348;
                    break;
                case 5: //Preferred Stat 2
                    X = new Random().Next(110, 175);
                    Y = new Random().Next(346, 358);
                    firstOptionY = 370;
                    break;
                case 6: //Preferred Stat 3
                    X = new Random().Next(110, 175);
                    Y = new Random().Next(372, 386);
                    firstOptionY = 392;
                    break;
                default:
                    throw new ArgumentException("Unknown option index in SelectOption");
            }

            send.MouseClick(handle, "left", X, Y);
            double yOffset;

            if (optionIndex < optionsPerPage) //Stat on first page
            {
                for (int i = 0; i < 30; i++)
                    send.MouseScroll(handle, "up", X, firstOptionY);
                yOffset = (optionIndex * optionHeight) + (optionHeight / 2); //Position of stat on first page
            }
            else if (optionIndex > (totalOptionCount - 10)) //Stat on last page
            {
                for (int i = 0; i < 30; i++)
                    send.MouseScroll(handle, "down", X, firstOptionY);
                yOffset = ((optionsPerPage - (totalOptionCount - optionIndex)) * optionHeight) + (optionHeight / 2); //Position of stat on lat page
            }
            else //Stat somewhere in the middle
            {
                //Scroll to top first
                for (int i = 0; i < 30; i++)
                    send.MouseScroll(handle, "up", X, firstOptionY);
                Thread.Sleep(shortPauseDuration);

                yOffset = (optionsPerPage * optionHeight) - (optionHeight / 2); //Last position of page
                double scrollsRequired = (optionIndex - (optionsPerPage - 1)) / optionsPerScroll;
                for (int i = 0; i < Math.Floor(scrollsRequired) + 1; i++)
                {
                    send.MouseScroll(handle, "down", X, firstOptionY);
                    Thread.Sleep(shortPauseDuration);
                }

                double offset = scrollsRequired % 1;
                if (offset > 0.5)
                    yOffset -= optionHeight * (optionsPerScroll * (1 - offset));
                else
                    yOffset = yOffset + ((optionHeight * optionsPerScroll * offset) - (optionsPerScroll * optionHeight));
            }

            Y = (int)(firstOptionY + yOffset);
            Thread.Sleep(shortPauseDuration);
            send.MouseClick(handle, "left", X, Y);
        }

        public void LoadAHQuery(string name, int[] query)
        {
            LoadAHQuery(name, query, true);
        }

        public void LoadAHQuery(string name, int[] query, bool changeBuyout)
        {
            if (name.StartsWith("None"))
            {
                frm.lbActivityConsole("Item type: " + SQF.ItemSubTypes[query[0]]);
                if (GetCurrentClass() != defaultClass)
                    SetSearchClass(defaultClass);

                if (query[0] != GetCurrentSubType())
                    SetSearchSubType(query[0]);
                if (query[1] != GetCurrentQuality())
                    SetSearchQuality(query[1]);

                frm.lbActivityConsole("Stat 1: " + SQF.Stats[query[2]] + " (" + query[3] + ")");
                SetSearchStat(0, query[2], query[0]);
                if (query[3] != -1)
                    SetSearchStatValue(0, query[3]);

                frm.lbActivityConsole("Stat 2: " + SQF.Stats[query[4]] + " (" + query[5] + ")");
                SetSearchStat(1, query[4], query[0]);
                if (query[5] != -1)
                    SetSearchStatValue(1, query[5]);

                frm.lbActivityConsole("Stat 3: " + SQF.Stats[query[6]] + " (" + query[7] + ")");
                SetSearchStat(2, query[6], query[0]);
                if (query[7] != -1)
                    SetSearchStatValue(2, query[7]);
            }
            else
            {
                SetSearchName(name);
                Thread.Sleep(shortPauseDuration);
            }

            if (changeBuyout)
                SetBuyout(query[8] + (new Random().Next(0, buyoutVariance)));
        }

        //Sets everything to AH defaults
        public void ResetQuery()
        {
            SetSearchClass(1);
            SetSearchSupType(0);
            SetSearchSubType(0);
            SetSearchQuality(0);
            SelectOption(4, 0, 30, 10);
            SelectOption(5, 0, 30, 10);
            SelectOption(6, 0, 30, 10);
            Thread.Sleep(250);
            ResetSearchName();
        }

        public void ResetSearchStats()
        {
            SelectOption(4, 0, 30, 10);
            SelectOption(5, 0, 30, 10);
            SelectOption(6, 0, 30, 10);
            currentStats[0] = 0;
            currentStats[1] = 0;
            currentStats[2] = 0;

            Thread.Sleep(shortPauseDuration);
        }

        public void ResetSearchName()
        {
            ResetSearchName("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
        }

        //name is the name we're clearing out -- longer names take longer to delete
        public void ResetSearchName(string name)
        {
            //TODO: Find x/y values
            int X = 90;
            int Y = 422;

            int nameLength = name.Length;

            send.MouseClick(handle, "left", X, Y);
            send.SendKeys(handle, "{HOME}");
            
            for (int i = 0; i < nameLength; i++)
                send.SendKeys(handle, "{DEL}");
        }

        private void SetSearchSubType(int subTypeIndex)
        {
            //Check to see if current class selected is okay to use for sub type
            if (!SQF.CanClassUseSubType(GetCurrentClass(), subTypeIndex))
                SetSearchClass(SQF.GetClassToUseSubType(subTypeIndex));

            //Check to see if current sup type is correct, if not then select the correct one
            int supTypeIndex = SQF.GetSupTypeIndex(subTypeIndex);
            if (GetCurrentSupType() != supTypeIndex)
            {
                SetCurrentSupType(supTypeIndex);
                SetSearchSupType(supTypeIndex);
            }

            SetCurrentSubType(subTypeIndex);
            SelectOption(SQF.GetSearchOptionByName("SubType"), SQF.GetSubTypeIndex(subTypeIndex), SQF.GetNumSubTypes(supTypeIndex), SQF.GetNumOptionsPerPage(supTypeIndex));

        }

        private void SetSearchSupType(int supTypeIndex)
        {
            SelectOption(SQF.GetSearchOptionByName("SupType"), supTypeIndex, SQF.GetNumSupTypes(), 10);
        }

        private void SetSearchClass(int classIndex)
        {
            SelectOption(SQF.GetSearchOptionByName("Class"), classIndex + numCharacters, numCharacters + SQF.GetNumClasses(), 10);
            SetCurrentClass(classIndex);
        }

        private void SetSearchQuality(int qualityIndex)
        {
            SetCurrentQuality(qualityIndex);
            SelectOption(SQF.GetSearchOptionByName("Quality"), qualityIndex, SQF.GetNumQualities(), 10);
        }

        private void SetSearchStat(int preferredStatIndex, int statIndex, int subTypeIndex)
        {
            //If stat is not set, set it
            if (GetCurrentStats()[preferredStatIndex] != statIndex)
            {
                int omit1;
                int omit2;

                //Find out which stats are omitted
                switch (preferredStatIndex)
                {
                    case 0:
                        omit1 = GetCurrentStats()[1];
                        omit2 = GetCurrentStats()[2];
                        break;
                    case 1:
                        omit1 = GetCurrentStats()[0];
                        omit2 = GetCurrentStats()[2];
                        break;
                    case 2:
                        omit1 = GetCurrentStats()[0];
                        omit2 = GetCurrentStats()[1];
                        break;
                    default:
                        throw new ArgumentException("Invalid preferredStatIndex in SetSearchStat");
                }


                //Build a new stat list with the omitted stats removed
                int[] statList = SQF.GetStatList(subTypeIndex, omit1, omit2);
                //Find the index of our stat in the new list and call SelectOption()
                int option = SQF.GetSearchOptionByName("preferredStat" + (preferredStatIndex));
                for (int i = 0; i < statList.Length; i++)
                {
                    if (statList[i] == statIndex)
                    {
                        SelectOption(option, i, statList.Length, statsPerPage);
                        SetCurrentStats(preferredStatIndex, statIndex);
                    }
                }
            }
        }

        private void SetSearchStatValue(int preferredStatIndex, int value)
        {
            
            int X = 205;
            int Y;
            switch (preferredStatIndex)
            {
                case 0:
                    Y = 330;
                    break;
                case 1:
                    Y = 350;
                    break;
                case 2:
                    Y = 375;
                    break;
                default:
                    throw new ArgumentException("Invalid preferredStatIndex in SetSearchStatValue");
            }
            send.MouseClick(handle, "left", X, Y);
            EditTextField(value);
        }

        public void SetSearchName(string value)
        {
            int X = 90;
            int Y = 422;

            send.MouseClick(handle, "left", X, Y);
            send.SendChars(handle, value);

        }

        public void SetBuyout(int value)
        {
            send.MouseClick(handle, "left", 188, 424);
            EditTextField(value);
        }

        public void EditTextField(int value)
        {
            send.SendKeys(handle, "{DEL}{DEL}{DEL}{DEL}{DEL}{DEL}{DEL}{DEL}{DEL}{DEL}");
            Thread.Sleep(shortPauseDuration);
            send.SendChars(handle, value.ToString());
            Thread.Sleep(shortPauseDuration);
        }

        public int GetCurrentClass()
        {
            return currentClass;
        }

        public void SetCurrentClass(int classIndex)
        {
            currentClass = classIndex;
        }

        public int GetCurrentSupType()
        {
            return currentSupType;
        }

        public void SetCurrentSupType(int supTypeIndex)
        {
            currentSupType = supTypeIndex;
        }

        public int GetCurrentSubType()
        {
            return currentSubType;
        }

        public void SetCurrentSubType(int subTypeIndex)
        {
            currentSubType = subTypeIndex;
        }

        public int GetCurrentQuality()
        {
            return currentQuality;
        }

        public void SetCurrentQuality(int qualityIndex)
        {
            currentQuality = qualityIndex;
        }

        public int[] GetCurrentStats()
        {
            return currentStats;
        }

        public void SetCurrentStats(int preferredStatIndex, int stat)
        {
            currentStats[preferredStatIndex] = stat;
        }

        public bool StatIsMatch(int preferredStatIndex, int statIndex)
        {
            frm.lbActivityConsole("StatIsMatch? Preferred Stat: " + preferredStatIndex + " Stat: " + SQF.Stats[statIndex]);
            if (GetCurrentStats()[preferredStatIndex] == statIndex)
                return true;
            return false;
        }

        public bool StatIsMatch(int stat)
        {
            for (int i = 0; i < GetCurrentStats().Length; i++)
            {
                if (GetCurrentStats()[i] == stat)
                    return true;
            }
            return false;
        }

        
        
    }
}
