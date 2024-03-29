﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GXPEngine
{
    public class Crate : AnimationSprite
    {
        public Crate(float pX, float pY) : base("crate.png", 1, 1)
        {
            SetOrigin(width / 2, height / 2);
            SetXY(pX, pY);
            SetScaleXY(0.75f, 0.75f);
        }
    }
}