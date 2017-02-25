using System;

namespace GreenshadesRPG {
    public class Monster : LivingEntity {
        public WeaponType weaponWeakness;
        Random randomWeakness;
        public int exp;
        int maxHP;

        public Monster(string name, int hp, int damage, int exp) {
            this.Name = name;
            this.HP = hp;
            this.maxHP = hp;
            this.AttackPower = damage;
            this.exp = exp;
            // Randomly select a weakness
            randomWeakness = new Random();
            this.weaponWeakness = (WeaponType)randomWeakness.Next(1, 3);
        }

        /// <summary>
        /// Monster receives damage, doubles damage if the weapon used is a weakness to the monster
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="usedWeapon"></param>
        /// <returns>True = Alive | False = Dead</returns>
        public bool TakeDamage(int damage) {                
            HP -= damage;
            Console.WriteLine("Dealt " + damage + " damage to the " + Name + ".");
            if (HP > 0) {
                return true;
            } else {
                Console.WriteLine("You've killed the " + Name + ".");
                // Reset the monster
                ResetMonster();
                return false;
            }
        }

        void ResetMonster() {
            this.HP = maxHP;
            // Randomly select a weakness
            Array weakness = Enum.GetValues(typeof(WeaponType));
            this.weaponWeakness = (WeaponType)randomWeakness.Next(weakness.Length);
        }
    }
}