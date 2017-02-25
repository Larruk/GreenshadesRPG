using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenshadesRPG {
    public enum WeaponType {
        Mace,
        Sword,
        Axe,
        Spear,
        Dagger
    }

    public class Weapon {
        public string Name;
        public int Damage;
        public WeaponType weaponType;

        public Weapon(string name, int damage, WeaponType weaponType) {
            this.Name = name;
            this.Damage = damage;
            this.weaponType = weaponType;
        }
    }
}
