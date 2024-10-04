﻿using HarmonyLib;
using StardewModdingAPI;
using System;

namespace StardewPortChanger
{
    public class ModEntry : Mod
    {
        private ModConfig Config;
        public static int ServerPort;

        public override void Entry(IModHelper helper)
        {
            this.Config = this.Helper.ReadConfig<ModConfig>();
            ServerPort = this.Config.ServerPort;
            var harmony = new Harmony("tokerin.stardewportchanger");
            var original = Type.GetType("Lidgren.Network.NetPeerConfiguration, Lidgren.Network").GetProperty("Port").GetSetMethod();
            var prefix = typeof(NetPeerConfigPatch).GetMethod("Prefix");
            harmony.Patch(original, new HarmonyMethod(prefix));
        }
    }

    internal class ModConfig
    {
        public int ServerPort { get; set; } = 36207;
    }

    public class NetPeerConfigPatch
    {
        public static void Prefix(ref int value)
        {
            value = ModEntry.ServerPort;
        }
    }
}