﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GXPEngine
{
    public class LevelUI : Canvas
    {
        readonly MyGame myGame = MyGame.current;
        readonly LevelManager levelManager = LevelManager.current;

        public int hWallsAmount;
        public int vWallsAmount;

        int mouseX = Input.mouseX;
        int mouseY = Input.mouseY;

        public bool holdingObject = false;

        Sprite inventorySlot1;
        Sprite inventorySlot2;
        Sprite WallIconV;
        Sprite WallIconH;

        PlacableWall wallh;
        PlacableWall wallv;

        readonly Font cowboyFont = new Font(FontFamily.GenericSerif, 20, FontStyle.Regular);

        public LevelUI(int pVWallsAmount, int pHWallsAmount) : base(1280, 720, false)
        {
            vWallsAmount = pVWallsAmount;
            hWallsAmount = pHWallsAmount;

            AddChild(new AmmoDisplay());

            inventorySlot1 = new InventoryBar(myGame.width / 2 - 52.5f, myGame.height - 50);
            inventorySlot2 = new InventoryBar(myGame.width / 2 + 52.5f, myGame.height - 50);

            WallIconV = new StaticWall(myGame.width / 2 - 52.5f, myGame.height - 50, new Vec2(0.25f, 0.25f), 0, false);
            WallIconH = new StaticWall(myGame.width / 2 + 52.5f, myGame.height - 50, new Vec2(0.25f, 0.25f), 90, false);
        }

        bool once = false;

        void Update()
        {
            mouseX = Input.mouseX;
            mouseY = Input.mouseY;

            graphics.Clear(Color.Empty);

            if (Input.GetKey(Key.TAB)) DrawMouseCoords();

            if (levelManager.levelComplete == false) graphics.DrawString("Score: " + levelManager.score.ToString(), cowboyFont, Brushes.Black, 30, 110);

            DrawInventory();
            InventoryFunctionality();

            if (once == false && levelManager.levelComplete)
            {
                once = true;
                levelManager.score += 50 * hWallsAmount;
                levelManager.score += 50 * vWallsAmount;
            }

            levelManager.holding = holdingObject;
        }

        void InventoryFunctionality()
        {
            if (levelManager.levelComplete == true) return;

            if (mouseY < height &&
                mouseY > height - 100 &&
                Input.GetMouseButtonDown(0) &&
                holdingObject == false)
            {
                if (mouseX < width / 2 &&
                    mouseX > width / 2 - 105 &&
                    vWallsAmount > 0)
                {
                    wallh = new PlacableWall(mouseX, mouseY, 0, this);
                    levelManager.LateAddChild(wallh);
                    --vWallsAmount;
                    holdingObject = true;
                }
                else if (mouseX > width / 2 &&
                         mouseX < width / 2 + 105 &&
                         hWallsAmount > 0)
                {
                    wallv = new PlacableWall(mouseX, mouseY, 90, this);
                    levelManager.LateAddChild(wallv);
                    --hWallsAmount;
                    holdingObject = true;
                }
            }
        }

        void DrawInventory()
        {
            DrawSprite(inventorySlot1);
            DrawSprite(inventorySlot2);

            DrawSprite(WallIconV);
            DrawSprite(WallIconH);

            graphics.DrawString(vWallsAmount.ToString(), SystemFonts.DefaultFont, Brushes.Black, width / 2 - 20, height - 20);
            graphics.DrawString(hWallsAmount.ToString(), SystemFonts.DefaultFont, Brushes.Black, width / 2 + 85, height - 20);

            levelManager.hwallsamount = hWallsAmount;
            levelManager.vwallsamount = vWallsAmount;
        }

        void DrawMouseCoords()
        {
            graphics.DrawString(mouseX.ToString() + " " + mouseY.ToString(), SystemFonts.DefaultFont, Brushes.White, 100, 100);
        }
    }
}