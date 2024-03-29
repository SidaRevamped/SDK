﻿using LeagueSharp;
using LeagueSharp.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swiftly_Teemo.Main;

namespace Swiftly_Teemo.Handler
{
    class Killsteal : Core
    {
        public static void KillSteal()
        {
            foreach (var target in GameObjects.EnemyHeroes.Where(x => x.IsValidTarget(Spells.Q.Range) && !x.IsDead && !x.IsZombie))
            {
                if (target != null && target.IsValidTarget())
                {
                    if (Spells.Q.IsReady() && target.Health < Spells.Q.GetDamage(target))
                    {
                        Spells.Q.Cast(target);
                    }
                    if (Spells.R.IsReady() && target.Health < Spells.R.GetDamage(target) && target.Distance(Player) <= Spells.R.Range && !Spells.Q.IsReady())
                    {
                        Spells.R.Cast(target);
                    }
                }
            }
            if (MenuConfig.KillStealSummoner)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(600f)))
                {
                    if (target.Health < Dmg.IgniteDmg && Spells.Ignite.IsReady() && !Spells.Q.IsReady() && target.Health > Spells.Q.GetDamage(target))
                    {
                        GameObjects.Player.Spellbook.CastSpell(Spells.Ignite, target);
                    }
                }
            }
        }
    }
}
