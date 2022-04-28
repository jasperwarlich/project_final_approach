﻿using System;

namespace GXPEngine
{
    public class AimLine : Sprite
    {
        LevelManager levelManager;

        public AimLine(float pX, float pY, LevelManager pLevelManager) : base("triangle.png", false)
        {
            SetOrigin(width / 2, 1.5f*height);
            SetXY(pX, pY);
            levelManager = pLevelManager;
        }

        void Update()
        {
            TurnTowardsMouse();
            if (Input.GetKeyDown(Key.S)) Shoot();
        }

        void TurnTowardsMouse()
        {
            Vec2 _relative_Mouse_Position = Vec2.GetMousePosition() - new Vec2(parent.x, parent.y);
            rotation = _relative_Mouse_Position.GetAngleDeg() - parent.rotation + 90;
        }

        void Shoot()
        {
            Vec2 position = new Vec2(x + parent.x, y + parent.y);
            Vec2 nozzle_Pos = new Vec2(position.x + 75, position.y);
            nozzle_Pos.RotateAroundVecDeg(position, rotation + parent.rotation - 90);

            Vec2 velocity = new Vec2(10, 0);
            velocity.SetAngleDeg(rotation + parent.rotation - 90);

            //levelManager.LateAddChild(new Bullet(nozzle_Pos.x, nozzle_Pos.y, rotation + parent.rotation, velocity));
            Block bullet = new Block(5, nozzle_Pos, velocity, levelManager);
            levelManager._movers.Add(bullet);
            levelManager.AddChild(bullet);
        }
    }
}