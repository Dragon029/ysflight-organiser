﻿using System;
using System.Collections.Generic;

namespace Ysfo.Core.Internal
{
    public class AddonCollection<T> : List<T> where T: Addon, new()
    {
        public string YsPath { get; protected set; }
        public string LstPath { get; protected set; }

        public AddonCollection(String ysPath) : this(ysPath, null)
        {
        }

        public AddonCollection(String ysPath, String lstPath)
        {
            YsPath = ysPath;
            LstPath = lstPath ?? DefaultLstPath();
        }

        public void Load()
        {
            // load aircraft
            ICollection<T> addons = LstLoader.Load<T>(YsPath, LstPath);

            // clear and add addons
            Clear();
            AddRange(addons);
        }

        private static String DefaultLstPath()
        {
            if (typeof(T) == typeof(Aircraft))
            {
                return "aircraft/aircraft.lst";
            }
            
            // eek
            throw new ArgumentException("Unknown type " + typeof(T).Name);
        }
    }
}
