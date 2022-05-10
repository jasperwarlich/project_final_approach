﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class PlacableWall : Sprite
    {
        readonly MyGame myGame = MyGame.current;
        readonly LevelUI levelUI;

        bool followMouse = true;

        Block BlockA;
        Block BlockB;

        bool obstructed = false;
        bool obstructedlastFrame = false;

        Sound placeWall = new Sound("place metal wall.mp3");

        public PlacableWall(float pX, float pY, int pRotation, LevelUI pGameUI) : base("WallTexture.png", false, true)
        {
            SetOrigin(width / 2, height / 2);
            SetXY(pX, pY);
            rotation = pRotation;
            levelUI = pGameUI;

            AddRigidBody();
        }

        void Update()
        {
            if (followMouse == true)
            {
                FollowMouse();

                if (CheckIfObstructed() == false && Input.GetMouseButtonDown(0))
                {
                    followMouse = false;
                    placeWall.Play();
                    levelUI.holdingObject = false;
                }
            }
            else
            {
                CheckReturnToInventory();
            }
        }

        void CheckReturnToInventory()
        {
            int mouseX = Input.mouseX;
            int mouseY = Input.mouseY;
            bool mouseOverlaps = false;

            if (rotation == 0)
            {
                if (mouseX < x + width/2 &&
                    mouseX > x - width/2 &&
                    mouseY > y - height/2 &&
                    mouseY < y + height/2)
                {
                    mouseOverlaps = true;
                }
            }
            else if (rotation == 90)
            {
                if (mouseX < x + height / 2 &&
                    mouseX > x - height / 2 &&
                    mouseY > y - width / 2 &&
                    mouseY < y + width / 2)
                {
                    mouseOverlaps = true;
                }
            }

            if (mouseOverlaps == true)
            {
                SetColor(255, 255, 0);
                if (Input.GetMouseButtonDown(0)) ReturnToInventory();
            }
            else
            {
                SetColor(255, 255, 255);
            }
        }

        bool CheckIfObstructed()
        {
            obstructed = false;

            if (obstructedlastFrame)
            {
                obstructedlastFrame = false;
                return true;
            }

            if (rotation == 0)
            {
                if (myGame.LeftXBoundary > x - width / 2 ||
                    myGame.RightXBoundary < x + width / 2 ||
                    myGame.TopYBoundary > y - height / 2 ||
                    myGame.BottomYBoundary < y + height / 2)
                {
                    obstructed = true;
                }
            }
            else if (rotation == 90)
            {
                if (myGame.LeftXBoundary > x - height/2 ||
                    myGame.RightXBoundary < x + height/2 ||
                    myGame.TopYBoundary > y - width/2 ||
                    myGame.BottomYBoundary < y + height/2)
                {
                    obstructed = true;
                }
            }

            if (obstructed == true)
            {
                SetColor(255, 0, 0);
                return true;
            }
            else
            {
                SetColor(255, 255, 255);
                return false;
            }
        }

        void FollowMouse()
        {
            if (rotation == 0)
            {
                SetXY(Input.mouseX, Input.mouseY);
                BlockA._position = new Vec2(x + 1, y - 37.5f);
                BlockB._position = new Vec2(x + 1, y + 39.5f);
            }
            else if (rotation == 90)
            {
                SetXY(Input.mouseX, Input.mouseY);
                BlockA._position = new Vec2(x - 38.5f, y);
                BlockB._position = new Vec2(x + 39f, y);
            }

            if (Input.GetKeyDown(Key.BACKSPACE))
            {
                ReturnToInventory();
            }
        }

        void AddRigidBody()
        {
            BlockA = new Block(37.5f, new Vec2(100, 150), new Vec2(), false, false, 999);
            BlockB = new Block(37.5f, new Vec2(100, 200), new Vec2(), false, false, 999);

            myGame.LateAddChild(BlockA);
            myGame.LateAddChild(BlockB);

            myGame._movers.Add(BlockA);
            myGame._movers.Add(BlockB);
        }

        void ReturnToInventory()
        {
            if (rotation == 0) levelUI.hWallsAmount++;
            else if (rotation == 90) levelUI.vWallsAmount++;

            LateDestroy();
        }

        void OnCollision(GameObject other)
        {
            Console.WriteLine("collided");

            if (other is PlacableWall || other is StaticWall || other is Enemy || other is Player || other is AimLine)
            {
                obstructed = true;
                obstructedlastFrame = true;
                SetColor(255, 0, 0);
            }
        }
    }
}