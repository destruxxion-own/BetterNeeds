using MSCLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace BetterNeeds
{
    internal class NeedsCore
    {
        public class ObjectsInstances
        {
            public GameObject Needs, Thirst, Hunger, Stress, Urine, Fatigue, Dirtiness;
            public GameObject ThirstBG, HungerBG, StressBG, UrineBG, FatigueBG, DirtinessBG;
        }
        public static ObjectsInstances Text = new ObjectsInstances();
        public class FSMInstances
        {
            public PlayMakerFSM ThirstValue, HungerValue, StressValue, UrineValue, FatigueValue, DirtinessValue;
        }
        public static FSMInstances PlayMaker = new FSMInstances();
        public class KeybindInstances
        {
            public Keybind Thirst, Hunger, Stress, Urine, Fatigue, Dirtiness, Background;
        }
        public static KeybindInstances Keybinds = new KeybindInstances();
        public class BooleanInstances
        {
            public bool ThirstOn = true, HungerOn = true, StressOn = true, UrineOn  = true, FatigueOn = true, DirtinessOn = true, BackgroundOn = true;
        }
        public static BooleanInstances Booleans = new BooleanInstances();
    }
}
