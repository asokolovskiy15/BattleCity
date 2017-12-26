using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kefir_BattleCity
{
   public class WarObject: MoveObject
    {

       public int HealthPoint { get; set;}
       public Weapon Weapon { get; set; }
       public override void ContactObject(MoveObject mobject, out bool needRemove)
       {
           if (mobject is Bullet)
               HealthPoint--;
           if (HealthPoint <= 0)
               needRemove = true;
           else
               needRemove = false;
       }

    }
}
