using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenshadesRPG {
    public class Character : LivingEntity {
        public int level = 1;
        public Weapon leftHand;
        public Weapon rightHand;
        public int currExp = 0;
        int maxExp = 6;

        public Character(string name, Weapon left, Weapon right) {
            this.Name = name;
            this.leftHand = left;
            this.rightHand = right;

            // More HP for player
            HP = 20;
            MaxHP = 20;
        }

        /// <summary>
        /// Monster receives damage, doubles damage if the weapon used is a weakness to the monster
        /// </summary>
        /// <param name="damage"></param>
        /// <param name="usedWeapon"></param>
        /// <returns>True = Alive | False = Dead</returns>
        public bool TakeDamage(Monster monster) {
            HP -= monster.AttackPower;
            Console.WriteLine("Received " + monster.AttackPower + " damage, ouch!");
            if (HP > 0) {
                return true;
            } else {
                Console.WriteLine("You've been killed by a " + monster.Name + "...\n\n\nGAME OVER\n\nPress any key to quit...");
                return false;
            }
        }

        public bool Attack(Monster monster, bool usingLeftHand) {
            Weapon usedWeapon = usingLeftHand ? leftHand : rightHand;
            int damage = usedWeapon.Damage + AttackPower;

            if (usedWeapon.weaponType == monster.weaponWeakness) {
                damage *= 2;
                Console.WriteLine("WEAPON WEAKNESS - DOUBLE DAMAGE");
            }

            return monster.TakeDamage(damage);
        }

        public void AddExp(int experience) {
            currExp += experience;
            if (currExp >= maxExp)
                LevelUp();
        }

        /// <summary>
        /// Arbitrary leveling system!
        /// </summary>
        void LevelUp() {
            level++;
            MaxHP += 5;
            HP = MaxHP;
            AttackPower += level;
            currExp = 0;
            maxExp += level * 2;
            Console.WriteLine("\n!!!\t\tLEVEL UP\t\t!!!\nCongratulations!");
            PrintCharacterSheet();
            System.Threading.Thread.Sleep(3000);
        }

        public void PrintCharacterSheet() {
            Console.WriteLine(Name + " reached level " + level
                + ".\nMax HP: " + MaxHP
                + ".\nAttack Power: " + AttackPower);
            System.Threading.Thread.Sleep(3000);
        }
    }
}
