﻿using System;
using System.Collections.Generic;

namespace ROLib
{
    public class SolarTechLimit
    {
        [Persistent] public string name;
        [Persistent] public int level;
        [Persistent] public string description;
        [Persistent] public string techRequired;
        [Persistent] public float kwPerM2;
        [Persistent] public float kgPerM2;
        [Persistent] public float costPerM2;
        [Persistent] public float massMultHinged;
        [Persistent] public float massMultFolded;
        [Persistent] public float massMultTrack;
        [Persistent] public float costMultHinged;
        [Persistent] public float costMultFolded;
        [Persistent] public float costMultTrack;
        [Persistent] public string key1;
        [Persistent] public string key20;
        [Persistent] public string key80;
        [Persistent] public string key99;

        public static bool isInitialized = false;

        private static readonly Dictionary<int, SolarTechLimit> allTL = new Dictionary<int, SolarTechLimit>();
        private static int maxTL = -1;
        private static readonly string modTag = "[ModuleROSolar.SolarTechLimit]";

        public SolarTechLimit() { }

        public override string ToString()
        {
            return $"{name};Level:{level};Description:{description};Tech:{techRequired};kwPerM2:{kwPerM2};kgPerM2:{kgPerM2};CostPerM2:{costPerM2}";
        }

        public static void Init(ConfigNode config)
        {
            ROLLog.debug($"{modTag}: Init() Started");
            allTL.Clear();
            foreach (ConfigNode node in config.GetNodes("ROS_TECH"))
            {
                SolarTechLimit obj = ConfigNode.CreateObjectFromConfig<SolarTechLimit>(node);
                ROLLog.debug($"{modTag}: Adding ROSTL {obj}");
                allTL.Add(obj.level, obj);
                maxTL = Math.Max(maxTL, obj.level);
            }
            isInitialized = true;
        }

        public static SolarTechLimit GetTechLevel(int lvl)
        {
            if (!isInitialized)
            {
                Init(GameDatabase.Instance.GetConfigNode("ROSOLAR_CONFIG"));
            }
            lvl = (lvl < 0) ? 0 : lvl;
            lvl = (lvl > maxTL) ? maxTL : lvl;
            if (allTL.TryGetValue(lvl, out SolarTechLimit info))
            {
                return info;
            }
            return allTL[0];
        }
    }
}
