using System;
using System.Collections.Generic;
using Game.Engine.Items;
using System.Windows;
using System.Windows.Threading;
using Game.Display;
using Game.Engine.Skills;
using System.Windows.Controls;
using Game.Engine.Interactions;
using Game.Engine.Monsters;
using Game.Engine.Interactions.Built_In;

namespace Game.Engine
{
    // internal logic and utility methods for GameSession class
    // some of the methods may be public (mostly for Game.Display calls), but they are generally not meant for routine usage by game content classes
    partial class GameSession
    {
        KilledMonstersCounter newCounter = KilledMonstersCounter.GetInstance();
        private void UpdateLocations()
        {
            // execute events for the current location
            LocationEvents(mapMatrix.Matrix[playerPosTop, playerPosLeft]);
            // update available moves
            AvailableMoves[0] = AvailableMoves[1] = AvailableMoves[2] = AvailableMoves[3] = false;
            if (playerPosLeft > 0 && mapMatrix.Matrix[playerPosTop, playerPosLeft - 1] > 0) AvailableMoves[2] = true;
            if (playerPosLeft < mapMatrix.Width && mapMatrix.Matrix[playerPosTop, playerPosLeft + 1] > 0) AvailableMoves[3] = true;
            if (playerPosTop > 0 && mapMatrix.Matrix[playerPosTop - 1, playerPosLeft] > 0) AvailableMoves[0] = true;
            if (playerPosTop < mapMatrix.Height && mapMatrix.Matrix[playerPosTop + 1, playerPosLeft] > 0) AvailableMoves[1] = true;
        }
        public void Wait(int milliseconds)
        {
            // wait a specific amount of time (e.g. for a keyboard response from the user)
            // for most practical purposes it will be used with a small argument value in a loop
            if (milliseconds == 0 || milliseconds < 0) return;
            timer.Interval = milliseconds;
            timer.Enabled = true;
            timer.Start();
            timer.Tick += (s, e) =>
            {
                timer.Enabled = false;
                timer.Stop();
            };
            while (timer.Enabled)
            {
                // do not freeze the rest of application
                if (Application.Current != null) Application.Current.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate { }));
            }
        }
        public void RemoveItemPosition(int x, int y)
        {
            // update item grid
            itemPositions.Remove(5 * y + x);
            RefreshItems();
        }
        public void AddItemPosition(int x, int y)
        {
            // update item grid
            itemPositions.Add(5 * y + x);
            RefreshItems();
        }
        public bool ProduceItem(string name)
        {
            // add an item and return true OR
            // if the item bag is full, do not add and return false
            for (int i = 0; i < 30; i++)
            {
                if (!itemPositions.Contains(i))
                {
                    Item it = Index.ProduceSpecificItem(name);
                    InsertItemToGrid(it, i);
                    return true;
                }
            }
            return false;
        }
        public void InsertItemToGrid(Item it, int i)
        {
            // inform the display that a new item has been inserted
            Image img = it.GetImage();
            parentPage.ItemGrid.Children.Add(img);
            Grid.SetRow(img, (i - i % 5) / 5);
            Grid.SetColumn(img, i % 5);
            itemPositions.Add(i);
            RefreshItems();
        }
        private void RefreshItems()
        {
            // re-check for active items - both the game logic and the user may have changed them
            List<string> itemNames = GetActiveItemNames();
            List<Item> newItems = new List<Item>();
            foreach (string itemName in itemNames)
            {
                Item tmp = Index.ProduceSpecificItem(itemName);
                newItems.Add(tmp);
            }
            items = newItems;
            RefreshStats();
        }
        public List<StatPackage> ModifyOffensive(List<StatPackage> packs)
        {
            // apply offensive buffs from all active items to a StatPackage
            if (items.Count > 0)
            {
                foreach (Item item in items)
                {
                    for (int i = 0; i < packs.Count; i++) packs[i] = item.ModifyOffensive(packs[i], GetActiveItemNames());
                }
            }
            return packs;
        }
        public List<StatPackage> ModifyDefensive(List<StatPackage> packs)
        {
            // apply defensive buffs from all active items to a StatPackage
            if (items.Count > 0)
            {
                foreach (Item item in items)
                {
                    for (int i = 0; i < packs.Count; i++) packs[i] = item.ModifyDefensive(packs[i], GetActiveItemNames());
                }
            }
            return packs;
        }
        public void RefreshStats()
        {
            // refresh active item buffs to statstics
            currentPlayer.ResetBuffs();
            List<string> itemNames = GetActiveItemNames();
            foreach (string itemName in itemNames)
            {
                List<string> tmp = new List<string>(itemNames);
                tmp.Remove(itemName); // all active items except this one
                Index.ProduceSpecificItem(itemName).ApplyBuffs(currentPlayer, tmp);
            }
            // refresh statistics display
            parentPage.Stat1.Document.Blocks.Clear();
            parentPage.Stat1.AppendText("Health: " + currentPlayer.Health);
            parentPage.Stat2.Document.Blocks.Clear();
            parentPage.Stat2.AppendText("Strength: " + currentPlayer.Strength);
            parentPage.Stat3.Document.Blocks.Clear();
            parentPage.Stat3.AppendText("Armor: " + currentPlayer.Armor);
            parentPage.Stat4.Document.Blocks.Clear();
            parentPage.Stat4.AppendText("Precision: " + currentPlayer.Precision);
            parentPage.Stat5.Document.Blocks.Clear();
            parentPage.Stat5.AppendText("Power: " + currentPlayer.MagicPower);
            parentPage.Stat6.Document.Blocks.Clear();
            parentPage.Stat6.AppendText("Stamina: " + currentPlayer.Stamina);
            parentPage.StatLevel.Document.Blocks.Clear();
            parentPage.StatLevel.AppendText(currentPlayer.ClassName + " Level " + currentPlayer.Level);
            parentPage.StatGold.Document.Blocks.Clear();
            parentPage.StatGold.AppendText("Gold: " + currentPlayer.Gold);
            AdjustStatDisplay();
        }
        private void AdjustStatDisplay()
        {
            // adjustments (don't ask me why this can't be done only once in xaml... )
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.Stat1);
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.Stat2);
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.Stat3);
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.Stat4);
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.Stat5);
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.Stat6);

            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.StatLevel);
            System.Windows.Documents.EditingCommands.AlignCenter.Execute(null, parentPage.StatGold);
        }
        public Skill GetListBoxResponse()
        {
            // for BattleScene.cs purposes
            CurrentSelection = null;
            while (CurrentSelection == null)
            {
                Wait(100);
            }
            return CurrentSelection;
        }
        public int ListBoxInteractionChoice(List<string> choices)
        {
            // for ListBoxInteractions
            if (choices.Count == 0) return 0;
            ListBoxInteractionDisplay inter = new ListBoxInteractionDisplay(parentPage);
            inter.SetChoices(choices);
            while (inter.ChosenNumber < 0)
            {
                Wait(100);
            }
            inter.Finish();
            return inter.ChosenNumber;
        }

        // for ImageInteractions
        public ImageInteractionScene SetTmpImage(Image img)
        {
            ImageInteractionScene scene = new ImageInteractionScene(parentPage, img);
            scene.SetupDisplay();
            return scene;
        }
        public void RemoveTmpImage(ImageInteractionScene scene)
        {
            scene.EndDisplay();
        }
        public void ListAllItemsCost()
        {
            // for selling items
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Image img = parentPage.GetImageFromGrid(i, j);
                    if (img != null)
                    {
                        if (img.Name != "")
                        {
                            Item tmp = Index.ProduceSpecificItem(img.Name);
                            SendText(tmp.PublicName + ": " + tmp.GoldValue + " gold");
                        }
                    }
                }
            }
        }
        public void ListAllItemsTips()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Image img = parentPage.GetImageFromGrid(i, j);
                    if (img != null)
                    {
                        if (img.Name != "")
                        {
                            Item tmp = Index.ProduceSpecificItem(img.Name);
                            string txt = tmp.PublicName + ": ";
                            if (tmp.HpMod > 0) txt += "Health(+" + tmp.HpMod + ") ";
                            if (tmp.HpMod < 0) txt += "Health(" + tmp.HpMod + ") ";
                            if (tmp.StrMod > 0) txt += "Strength(+" + tmp.StrMod + ") ";
                            if (tmp.StrMod < 0) txt += "Strength(" + tmp.StrMod + ") ";
                            if (tmp.ArMod > 0) txt += "Armor(+" + tmp.ArMod + ") ";
                            if (tmp.ArMod < 0) txt += "Armor(" + tmp.ArMod + ") ";
                            if (tmp.PrMod > 0) txt += "Precision(+" + tmp.PrMod + ") ";
                            if (tmp.PrMod < 0) txt += "Precision(" + tmp.PrMod + ") ";
                            if (tmp.MgcMod > 0) txt += "Power(+" + tmp.MgcMod + ") ";
                            if (tmp.MgcMod < 0) txt += "Power(" + tmp.MgcMod + ") ";
                            if (tmp.StaMod > 0) txt += "Health(+" + tmp.StaMod + ") ";
                            if (tmp.StaMod < 0) txt += "Health(" + tmp.StaMod + ") ";
                            if (tmp.PublicTip != null) txt += "Bonus: " + tmp.PublicTip;
                            SendText(txt);
                        }
                    }
                }
            }
        }
        public void RefreshMonstersDisplay()
        {
            // refresh display of all monsters
            Image img;
            for (int y = 0; y < mapMatrix.Height; y++)
            {
                for (int x = 0; x < mapMatrix.Width; x++)
                {
                    img = mapMatrix.HintMonsterImage(x, y);
                    if (img != null)
                    {
                        parentPage.UpdateMonster(mapMatrix.Width * y + x, img, mapMatrix.Width);
                    }
                }
            }
        }
        // two methods for interactions
        public void StopMoving() { parentPage.Movable = false; }
        public void StartMoving() { parentPage.Movable = true; }
        private void LocationEvents(int code)
        {

            // events and interactions that happen at special map locations

            /* Note: there is a weird display issue where KeyDown event fires twice when a game interaction occurs
             * I have made several attempts to trace it, but it remains elusive
             * The internet offers contradictory opinions, some people claim it's caused by an underlying WPF bug
             * In any case, IgnoreNextKey serves as a workaround
             * Be careful, though, since it means the movement will not be smooth around interactions by default
             * If you want smooth movement for some specific interaction, you need to overwrite IgnoreNextKey inside the relevant if-clause
             */
            if (code > 1) parentPage.IgnoreNextKey = true;
            //
            if (code == 1000)
            {
                Battle newBattle = null;
                try
                {
                    DefaultStrategy strat0 = new DefaultStrategy();
                    Q1Strategy strat1 = new Q1Strategy();
                    Q2Strategy strat2 = new Q2Strategy();
                    

                    Monster monster = mapMatrix.CreateMonster(playerPosLeft, playerPosTop, currentPlayer.Level);
                    if (monster != null)
                    {
                        BattleScene newBattleScene = new BattleScene(parentPage, currentPlayer, monster);
                        if (newCounter.BattleStrategy == 1)
                        {
                            newBattle = new Battle(strat1, this, newBattleScene, monster, newCounter);
                        }
                        else if (newCounter.BattleStrategy == 2)
                        {
                            newBattle = new Battle(strat2, this, newBattleScene, monster, newCounter);
                        }
                        else
                        {
                            newBattle = new Battle(strat0, this, newBattleScene, monster, newCounter);
                        }

                        newBattle.Run();
                        if (newBattle.battleResult)
                        {
                            if (monster.Name == "monster2141")
                            {
                                newCounter.GreenDragonCounter += 1;
                                parentPage.UpdateMonster(mapMatrix.Width * PlayerPosTop + PlayerPosLeft, mapMatrix.HintMonsterImage(playerPosLeft, playerPosTop), mapMatrix.Width);
                                UpdateStat(7, monster.XPValue);
                                mapMatrix.Monsters[mapMatrix.Width * PlayerPosTop + PlayerPosLeft] = null; // this monster was defeated
                            }
                            else if (monster.Name == "monster2140")
                            {
                                newCounter.GhostCounter += 1;
                                parentPage.UpdateMonster(mapMatrix.Width * PlayerPosTop + PlayerPosLeft, mapMatrix.HintMonsterImage(playerPosLeft, playerPosTop), mapMatrix.Width);
                                UpdateStat(7, monster.XPValue);
                                mapMatrix.Monsters[mapMatrix.Width * PlayerPosTop + PlayerPosLeft] = null; // this monster was defeated
                            }
                            else if (monster.Name == "monster0001")
                            {
                                newCounter.RatsCounter += 1;
                                parentPage.UpdateMonster(mapMatrix.Width * PlayerPosTop + PlayerPosLeft, mapMatrix.HintMonsterImage(playerPosLeft, playerPosTop), mapMatrix.Width);
                                UpdateStat(7, monster.XPValue);
                                mapMatrix.Monsters[mapMatrix.Width * PlayerPosTop + PlayerPosLeft] = null; // this monster was defeated
                            }
                            else
                            {
                                parentPage.UpdateMonster(mapMatrix.Width * PlayerPosTop + PlayerPosLeft, mapMatrix.HintMonsterImage(playerPosLeft, playerPosTop), mapMatrix.Width);
                                UpdateStat(7, monster.XPValue);
                                mapMatrix.Monsters[mapMatrix.Width * PlayerPosTop + PlayerPosLeft] = null; // this monster was defeated
                            }
                        }
                        else mapMatrix.Monsters[mapMatrix.Width * PlayerPosTop + PlayerPosLeft] = monster; // remember this monster until the next time
                        // restore position from before the battle
                        parentPage.MovePlayer("reverse");
                    }
                    else parentPage.IgnoreNextKey = false;
                }
                catch (IndexOutOfRangeException e)
                {
                    parentPage.AddConsoleText("An attempt was made to create a monster but something went wrong. Did you remember to update the Index class?");
                    parentPage.AddConsoleText(e.Message);
                }
            }
            else if (code >= 2000 && code < 3000)
            {
                mapMatrix = metaMapMatrix.GetCurrentMatrix(code - 2000);
                InitializeMapDisplay(metaMapMatrix.GetPreviousMatrixCode() + 2000);
            }
            else if (code > 3000)
            {
                mapMatrix.Interactions[mapMatrix.Width * PlayerPosTop + PlayerPosLeft].Run();
                if (mapMatrix.Interactions[mapMatrix.Width * PlayerPosTop + PlayerPosLeft].Enterable == false) parentPage.MovePlayer("reverse");
            }
        }

    }
}
