﻿using System;

namespace GXPEngine
{
    public class Bullet : Sprite
    {
        readonly MyGame myGame = MyGame.current;

        Block bullet;

        public Bullet(float pRotation, Block pbullet) : base("Bullet_Small.png")
        {
            SetOrigin(width / 2, height / 2);
            SetScaleXY(1.5f, 1.5f);

            bullet = pbullet;
        }


        void Update()
        {
            TurnTowardsVelocity();
        }

        void TurnTowardsVelocity()
        {
            Vec2 velocity = bullet.velocity;
            rotation = velocity.GetAngleDeg() - parent.rotation + 90;
        }
    }
}