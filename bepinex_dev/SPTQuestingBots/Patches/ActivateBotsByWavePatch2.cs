﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Aki.Reflection.Patching;
using EFT;

namespace SPTQuestingBots.Patches
{
    public class ActivateBotsByWavePatch2 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return typeof(BotsController).GetMethod(
                "ActivateBotsByWave",
                BindingFlags.Public | BindingFlags.Instance,
                null,
                new Type[] { typeof(BotWaveDataClass) },
                null);
        }

        [PatchPrefix]
        private static bool PatchPrefix(BotWaveDataClass wave)
        {
            if (!GameStartPatch.IsDelayingGameStart)
            {
                //LoggingController.LogInfo("Allowing spawn of " + wave.WildSpawnType + " bot wave...");
                return true;
            }

            GameStartPatch.AddMissedBotWave(wave);
            //LoggingController.LogInfo("Delaying spawn of " + wave.WildSpawnType + " bot wave...");

            return false;
        }
    }
}
