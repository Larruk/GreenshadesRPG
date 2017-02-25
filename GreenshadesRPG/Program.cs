using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenshadesRPG {
    class Program {
        static Character myChar;
        static bool inCombat = false;
        static List<Monster> possibleEnemies;
        static List<Weapon> possibleWeapons;
        static int counter = 50;
        static Random randomEnemy;

        static void Main(string[] args) {
            // Several diff monsters, added extras to increase their
            // likeliness of showing up
            possibleEnemies = new List<Monster>() {
                new Monster("Skeleton", 1, 1, 1),
                new Monster("Skeleton", 1, 1, 1),
                new Monster("Goblin Archer", 3, 1, 2),
                new Monster("Goblin Archer", 3, 1, 2),
                new Monster("Goblin Grunt", 5, 1, 3),
                new Monster("Goblin Grunt", 5, 1, 3),
                new Monster("Goblin Commander", 7, 2, 5),
                new Monster("Dwarf Miner", 13, 1, 4),
                new Monster("Dwarf Hunter", 8, 2, 6),
                new Monster("Dwarf Mage", 6, 5, 2),
                new Monster("Stack of Dwarves", 15, 4, 10)
            };
            randomEnemy = new Random();

            possibleWeapons = new List<Weapon>() {
                new Weapon("Training Dagger", 0, WeaponType.Dagger),
                new Weapon("Dagger", 1, WeaponType.Dagger),
                new Weapon("Sword", 1, WeaponType.Sword),
                new Weapon("Mace", 2, WeaponType.Mace),                
                new Weapon("Axe", 2, WeaponType.Axe),
                new Weapon("Spear", 3, WeaponType.Spear)
            };

            Intro();
            Explore();
        }        

        static void Intro() {
            Console.WriteLine("Welcome to the Greenshades RPG!");
            PrintUnderline();
            Console.WriteLine("Please enter your name:");
            string name = Console.ReadLine();
            Weapon left = LoadWeapon();
            Weapon right = LoadWeapon();

            myChar = new Character(name, left, right);

            Console.WriteLine("\n\nThank you " + 
                myChar.Name + " for accepting the Greenshades Quest. \nThis quest is dangerous, keep your trusty " + 
                myChar.leftHand.Name + " and " +
                myChar.rightHand.Name + " close... \nContinue down the trail until you reach the Dragon's Den.\n");
        }

        static void Explore() {
            Console.WriteLine("\nYou continue on your quest...");
            while (!inCombat) {
                Console.WriteLine(counter + " meters remaining...");

                // If the player made it to the end, finish the game!
                counter--;
                if (counter <= 0) {
                    // Final boss, win if completed
                    Combat();
                    WinGame();
                } else {
                    System.Threading.Thread.Sleep(500);

                    int chance = new Random().Next(0, 100);
                    if (chance <= 10) {
                        // Enter combat 10% of the time, every .5 seconds
                        Combat();
                    }
                }        
            }
        }

        static void Combat() {
            inCombat = true;

            // Random monster encounter
            Monster enemy = possibleEnemies[randomEnemy.Next(0, possibleEnemies.Count)];
            if (counter <= 0) {
                // Boss fight!
                Console.WriteLine("... You hear a loud roar ...");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("The air feels dry... you feel as if your quest is coming to an end...");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Up above! You brace yourself as a gust of wind hits you... What is that!?");
                System.Threading.Thread.Sleep(3000);
                enemy = new Monster("Dragon", 70, 3, 0);
            }
            Console.WriteLine();
            PrintUnderline();
            Console.WriteLine("You run into a " + enemy.Name + "!");
            bool usingLeftHand = false;

            while (inCombat) {
                // Handle Player combat
                Console.WriteLine("> Press L to attack with your " + myChar.leftHand.Name + " or R for your " + myChar.rightHand.Name + ".");
                
                // Read key presses for either L or just assume R if NOT L to improve user experience
                usingLeftHand = Console.ReadKey(true).KeyChar == 'l';
                if (myChar.Attack(enemy, usingLeftHand)) {
                    // Monster remains alive so handle Monster combat
                    if (!myChar.TakeDamage(enemy)) {
                        GameOver();
                    }

                    // Print both combatants status
                    myChar.PrintStatus();
                    enemy.PrintStatus();
                    Console.WriteLine();
                } else {
                    // Monster is dead, combat ends
                    inCombat = false;
                }
            }

            // If out of combat and still alive continue exploring
            PrintUnderline();
            myChar.AddExp(enemy.exp);
            if (counter > 0)
                Explore();
        }

        static Weapon LoadWeapon() {
            // Display weapons to the user
            Console.WriteLine("\nSelect a weapon:");
            for (int i = 0; i < possibleWeapons.Count; i++) {
                Console.WriteLine(i + ". " + possibleWeapons[i].Name + " - " + possibleWeapons[i].Damage + " damage");
            }

            // Receive key input and ensure (recursively) that user entered a valid key
            int x = 0;
            try {
                x = int.Parse(Console.ReadKey(true).KeyChar.ToString());
                if (x < 0 || x > possibleWeapons.Count - 1) {
                    // Invalid input
                    throw new Exception();
                }
            } catch (Exception e) {                
                Console.WriteLine("Error: Please use a valid number!");
                return LoadWeapon();
            }

            // Remove the weapon so you can ask the player again
            Weapon selectedWeapon = possibleWeapons.ElementAt(x);
            possibleWeapons.RemoveAt(x);
            return selectedWeapon;        
        }

        public static void PrintUnderline() {
            Console.WriteLine("===============================");
        }

        static void GameOver() {
            // Wait on any key press to quit
            Console.ReadKey();
            Environment.Exit(0);      
        }

        static void WinGame() {
            Console.WriteLine("CONGRATULATIONS!\nYOU FINISHED YOUR QUEST!\n");
            myChar.PrintCharacterSheet();
            Console.WriteLine("\nPress any key to quit...");        
            GameOver();
        }
    }
}
