using System;
using System.Windows.Controls;
using System.Collections.Generic;
using Game.Engine.Items;
using Game.Engine.Skills;
using Game.Engine.Monsters;

namespace Game.Engine
{
    // publicly available methods for GameSession class
    // this is how other classes should interact with the gameplay
    partial class GameSession
    {
        
        public void SendText(string text)
        {
            // sends a string to the game console
            // no need to put an enter at the end/beginning
            parentPage.AddConsoleText(text);
        }


        /***************************        KEYBOARD      ***************************/
        public Tuple<string,int> GetKeyResponse()
        {
            // usage: GetKeyResponse().Item1 will return the capital letter corresponding to the next key pressed by the user
            // GetKeyResponse().Item2 will return time before getting the response (in milliseconds)
            // do not use for keys other than A-Z or 0-9 (unless you know their windows codes)
            parentPage.Movable = false;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            CurrentKey = "";
            while (CurrentKey == "")
            {
                Wait(100);
            }
            watch.Stop();
            if (CurrentKey.Length == 2 && CurrentKey[0] == 'D') CurrentKey = CurrentKey.Remove(0, 1); // remove windows code for digits
            parentPage.Movable = true;
            SendText(CurrentKey);
            return new Tuple<string, int>(CurrentKey, (int)watch.ElapsedMilliseconds);
        }
        public Tuple<string,int> GetValidKeyResponse(List<string> validKeys)
        {
            // same as above, but only keys from the validKeys list will be accepted
            // program will not proceed until a correct key is pressed so make sure the user knows what to do
            if (validKeys.Count == 0) return null;
            parentPage.Movable = false;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            CurrentKey = "";
            bool stillTesting = true;
            while (stillTesting)
            {
                Wait(100);
                if (CurrentKey.Length == 2 && CurrentKey[0] == 'D') CurrentKey = CurrentKey.Remove(0, 1);
                foreach (string vk in validKeys)
                {
                    if (CurrentKey == vk)
                    {
                        stillTesting = false;
                        break;
                    }
                }
            }
            watch.Stop();
            parentPage.Movable = true;
            return new Tuple<string, int>(CurrentKey, (int)watch.ElapsedMilliseconds);
        }


        /***************************        ITEMS      ***************************/
        public List<string> GetActiveItemNames()
        {
            // return names of all currently active items (max 5) as List<string>
            List<string> ans = new List<string>();
            for (int i = 0; i < 5; i++)
            {
                Image img = parentPage.GetImageFromGrid(i, 0);
                if (img != null)
                {
                    if (img.Name != "") ans.Add(img.Name);
                }
            }
            return ans;
        }
        public bool TestForItem(string name)
        {
            // check if a particular item is currently equipped as active
            List<string> actives = GetActiveItemNames();
            foreach (string s in actives)
            {
                if (s == name) return true;
            }
            return false;
        }
        public bool TestForItemClass(string name)
        {
            // check if ANY item from a given special class (staff,axe,spear,sword) is currently equipped as active
            switch(name)
            {
                case "Axe":
                    foreach (Item item in items) if (item.IsAxe) return true;
                    break;
                case "Spear":
                    foreach (Item item in items) if (item.IsSpear) return true;
                    break;
                case "Staff":
                    foreach (Item item in items) if (item.IsStaff) return true;
                    break;
                case "Sword":
                    foreach (Item item in items) if (item.IsSword) return true;
                    break;
            }
            return false;
        }
        public void AddRandomClassItem()
        {
            // player receives a random item that is guaranteed to fit their class
            // (e.g. a warrior will not get magic staffs and a mage will not get axes, swords or spears)
            Item it = Index.RandomClassItem(currentPlayer);
            for (int i = 0; i < 30; i++)
            {
                if (!itemPositions.Contains(i))
                {
                    InsertItemToGrid(it, i);
                    break;
                }
            }
        }
        public void AddRandomItem()
        {
            // player receives a random item (no class restrictions)
            Item it = Index.RandomItem();
            for (int i = 0; i < 30; i++)
            {
                if (!itemPositions.Contains(i))
                {
                    InsertItemToGrid(it, i);
                    break;
                }
            }
        }
        public void AddThisItem(Item it)
        {
            // player receives a particular item
            // you may want to use this method together with Index methods for item production:
            // Index.RandomItem() for random item
            // Index.RandomClassItem() for random class-suitable item
            for (int i = 0; i < 30; i++)
            {
                if (!itemPositions.Contains(i))
                {
                    InsertItemToGrid(it, i);
                    break;
                }
            }
        }


        /***************************        SKILLS      ***************************/
        public void LearnThisSkill(Skill sk)
        {
            // player learns a particular skill
            // if you use this method, you should know exactly what skill you want the player to learn
            currentPlayer.Learn(sk);
        }

        /***************************        FIGHTS      ***************************/
        public void FightRandomMonster()
        {
            // player will fight against a random monster
            // xp can be gained here, but gold/items cannot (you can do this separately inside your interaction)
            try
            {
                Monster monster = Index.RandomMonsterFactory().Clone().Create(currentPlayer.Level);
                if (monster != null)
                {
                    DefaultStrategy strat0 = new DefaultStrategy();
                    Display.BattleScene newBattleScene = new Display.BattleScene(parentPage, currentPlayer, monster);
                    Battle newBattle = new Battle(strat0, this, newBattleScene, monster, newCounter);
                    newBattle.Run();
                    //if (newBattle.battleResult) UpdateStat(7, monster.XPValue);
                }
            }
            catch (IndexOutOfRangeException e)
            {
                parentPage.AddConsoleText("An attempt was made to create a monster but something went wrong. Did you remember to update the Index class?");
                parentPage.AddConsoleText(e.Message);
            }
        }

        public void FightThisMonster(Monster monster)
        {
            // player will fight against a particular monster
            // xp can be gained here, but gold/items cannot (you can do this separately inside your interaction)
            if (monster != null)
            {
                
                Display.BattleScene newBattleScene = new Display.BattleScene(parentPage, currentPlayer, monster);
                Battle newBattle = new Battle(this, newBattleScene, monster, false);
                newBattle.Run();
                if (newBattle.battleResult) UpdateStat(7, monster.XPValue);
            }
        }


        /***************************        PLAYER STATISTICS      ***************************/
        public void UpdateStat(int number, int value)
        {
            // use this method to change player statistics
            // important: this method is for PERMANENT changes, do not use it to apply conditional buffs (e.g. from items)
            // if you are writing an item class and you want to apply a conditional buff, see Item.ApplyBuffs for an example of how to do that

            // usage: UpdateStat(statCode, changeValue)
            // statCodes for each statistic:
            // 1 - health
            // 2 - strength
            // 3 - armor
            // 4 - precision
            // 5 - magic power
            // 6 - stamina
            // 7 - experience points (character level is updated automatically and NOT meant to be changed directly)
            // 8 - gold
            // the method itself allows for any change (negative stat values will default to zero), but external balance guidelines may apply
            switch (number)
            {
                case 1:
                    currentPlayer.Health += value - currentPlayer.HealthBuff;
                    break;
                case 2:
                    currentPlayer.Strength += value - currentPlayer.StrengthBuff;
                    break;
                case 3:
                    currentPlayer.Armor += value - currentPlayer.ArmorBuff;
                    break;
                case 4:
                    currentPlayer.Precision += value - currentPlayer.PrecisionBuff;
                    break;
                case 5:
                    currentPlayer.MagicPower += value - currentPlayer.MagicPowerBuff;
                    break;
                case 6:
                    currentPlayer.Stamina += value - currentPlayer.StaminaBuff;
                    break;
                case 7:
                    currentPlayer.XP += value;
                    break;
                case 8:
                    currentPlayer.Gold += value;
                    break;
            }
            RefreshStats();
        }
        public void UpdateStat(StatPackage pack) 
        {
            // another version, can be useful during battle-type interactions
            currentPlayer.Health -= pack.HealthDmg - currentPlayer.HealthBuff;
            currentPlayer.Strength -= pack.StrengthDmg - currentPlayer.StrengthBuff;
            currentPlayer.Armor -= pack.ArmorDmg - currentPlayer.ArmorBuff;
            currentPlayer.Precision -= pack.PrecisionDmg - currentPlayer.PrecisionBuff;
            currentPlayer.MagicPower -= pack.MagicPowerDmg - currentPlayer.MagicPowerBuff;
            RefreshStats();
        }

    }
}
