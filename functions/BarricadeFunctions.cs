﻿using JetBrains.Annotations;
using Rocket.API;
using Rocket.API.Collections;
using Rocket.Core;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace Ocelot.BlueCrystalCooking.functions
{
    public static class BarricadeFunctions
    {
        public static void BarricadeDamaged(Transform barricadeTransform, ushort pendingTotalDamage)
        {
            if (barricadeTransform)
            {
                BarricadeData bData = BlueCrystalCookingPlugin.Instance.getBarricadeDataAtPosition(barricadeTransform.position);
                if (bData == null)
                    return;

                if (bData.barricade.health <= pendingTotalDamage)
                {
                    if (BlueCrystalCookingPlugin.Instance.Configuration.Instance.BarrelObjectId == bData.barricade.id)
                    {
                        BlueCrystalCookingPlugin.Instance.placedBarrelsTransformsIngredients.Remove(barricadeTransform);
                    }
                    else if (BlueCrystalCookingPlugin.Instance.Configuration.Instance.BlueCrystalTrayId == bData.barricade.id)
                    {
                        foreach(var tray in BlueCrystalCookingPlugin.Instance.freezingTrays.ToList())
                        {
                            if (tray.transform == null)
                                return;

                            if (tray.transform == barricadeTransform)
                            {
                                BlueCrystalCookingPlugin.Instance.freezingTrays.Remove(tray);
                            }
                        }
                    }
                }
            }
        }
    }
}
