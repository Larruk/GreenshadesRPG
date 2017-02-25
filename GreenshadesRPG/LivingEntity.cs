using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenshadesRPG {
    public abstract class LivingEntity {
        public string Name;
        public int HP = 10;
        public int MaxHP;
        public int AttackPower = 1;
        public void PrintStatus() {
            Console.WriteLine("Status: " + Name + " has " + HP + " HP remaining.");
        }
    }
}
