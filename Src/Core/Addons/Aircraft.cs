﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ysfo.Core.Addons
{
    public class Aircraft
    {
        public Aircraft(String name)
        {
            Name = name;
        }

        public String Name { get; protected set; }
    }
}
