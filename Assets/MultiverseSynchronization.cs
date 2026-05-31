/* Hi dear curious person. You looking for how I did something?
Well here's your warning: this code is SO FUCKING BAD AND SCUFFED.
This code is being held up by my hopes and dreams, if this actually helps you in your own module,
count that as a MIRACLE. */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using KModkit;
using Rnd = UnityEngine.Random;
using Math = ExMath;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
public class MultiverseSynchronization : MonoBehaviour {

   public KMBombInfo Bomb;
   public KMAudio Audio;

   public KMSelectable AddMinutesButton;
   public KMSelectable SubMinutesButton;
   public KMSelectable AddDozenSecondsButton;
   public KMSelectable SubDozenSecondsButton;
   public KMSelectable AddSecondsButton;
   public KMSelectable SubSecondsButton;
   public KMSelectable SyncButton;
   public TextMesh DozenMinutesDisplay;
   public TextMesh MinutesDisplay;
   public TextMesh DozenSecondDisplay;
   public TextMesh SecondsDisplay;
   public TextMesh DivisorDisplay;
   public Material DisplayMaterial;
   public Material BGScreenMaterial;
   public Material Button1Material;
   public Material Button2Material;
   public Material OutlineMaterial;

   int DozenMinuteTime = 0;
   int MinuteTime = 0;
   int DozenSecondTime = 0;
   int SecondTime = 0;
   bool StopTimer = false;

   bool Kaboosh = false;
   bool TimerRunning = false;

   // Step 1 Vars
   string SerialNumber;
   int ModuleCount;
   int IndicatorsAndBatteries;
   int AlphanumericSNLetters;
   int BombTotalTime;
   int CurrentStrikes;
   int SNDigitSum;
   string SolveKeyPartTwo = "";
   int CorrectSolveAmount;
   bool LessThan11Modules = false;

   int rowT1;
   int columnT1;

   string SolveKeyString = "";
   int SolveKey;

   // Step 2 Vars
   int TimestampSeconds = 0;
   int TimestampMinutes = 0;
   string FormattedCorrectTime;

   bool Has2EmptyPortPlate = false;
   bool Has1EmptyPortPlate = false;
   int LastSNDigit;

   bool Rule04 = false;
   bool Rule10 = true;
   bool SkipAll = false;
   string AppliedRulesS2 = "";

   // Step 3 Vars
   string[][] TableStep3Modules = new string[][]
   {
      "Listening, Unordered Keys, Plumbing, Safety Safe, Misordered Keys, Piano Keys, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "T-Words, Word Scramble, Anagrams, Recorded Keys, Simon States, Round Keypad, Morse Code".Split(new string[] { ", " }, StringSplitOptions.None),
      "Bordered Keys, Follow The Leader, Blind Alley, Hexamaze, Battleship, LED Encryption, Who’s on First".Split(new string[] { ", " }, StringSplitOptions.None),
      "Sink, Morse-a-Maze, Algebra, Color Flash, Turn The Keys, 3D Maze, Simon Says".Split(new string[] { ", " }, StringSplitOptions.None),
      "Mastermind Cruel, Extended Password, Human Resourses, Poker, Mashematics, Skyrim, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "Cruel Piano Keys, Button Sequence, Game of LIfe Cruel, Mortal Kombat, Poetry, Mafia, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "Led Grid, Polyhedral Maze, Symbolic Coordinates, Faulty Backgrounds, Radiator, Colored Switches, Memory".Split(new string[] { ", " }, StringSplitOptions.None),
      "Identity Parade, Visual Impairment, The Swan, The Iphone, X-ray, Waste Management, Who’s on First".Split(new string[] { ", " }, StringSplitOptions.None),
      "Maintenance, Color Generator, S.E.T., Painting, Monsplode Trading Cards, Flags, Morse Code".Split(new string[] { ", " }, StringSplitOptions.None),
      "Curriculum, Braille, Gridlock, Blind Maze, The Jukebox, Perplexing Wires, Wire Sequence".Split(new string[] { ", " }, StringSplitOptions.None),
      "Symbol Cycle, Modern Cipher, Timezone, Color Morse, Sonic the Hedgehog, Nonogram, Wires".Split(new string[] { ", " }, StringSplitOptions.None),
      "Alphabet, The Screw, Mastermind Simple, Game of Life Simple, Hunting, Big Circle, Complicated Wires".Split(new string[] { ", " }, StringSplitOptions.None),
      "Reordered Keys, Connection Check, Bitwise Operations, Chord Qualities, Minesweeper, Letter Keys, Maze".Split(new string[] { ", " }, StringSplitOptions.None),
      "Perspective Pegs, Mystic Square, Adjacent Letters, Text Field, Microcontroller, Only Connect, Password".Split(new string[] { ", " }, StringSplitOptions.None),
      "Shape Shift, English Test, Cheap Checkout, Ordered Keys, Creation, The Clock, Wires".Split(new string[] { ", " }, StringSplitOptions.None),
      "101 Dalmatians, Zoo, Point of Order, Switches, Orientation Cube, Astrology, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "Crazy Talk, Chord Progressions, Resistors, Friendship, Colored Squares, FizzBuzz, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "The Bulb, Wire Placement, Color Math, Web Design, Boolean Venn Diagram, Ice Cream, Simon Says".Split(new string[] { ", " }, StringSplitOptions.None),
      "Sea Shells, Translated Morse Code, Translated Password, Semaphore, Combination Lock, Tic Tac Toe, Memory".Split(new string[] { ", " }, StringSplitOptions.None),
      "Binary LEDs, Translated Who’s On First, Simon Screams, Modules Against Humanity, Complicated Buttons, Symbolic Password, Who’s on First".Split(new string[] { ", " }, StringSplitOptions.None),
      "The Gamepad, Monsplode Fight!, Sea Shells, Rock Paper Scissors Lizard Spock, Coordinates, Rubiks Cube, Morse Code".Split(new string[] { ", " }, StringSplitOptions.None),
      "Broken Buttons, Double-Oh, Neutralization, Emoji Math, Mouse In The Maze, Number Pad, Complicated Wires".Split(new string[] { ", " }, StringSplitOptions.None),
      "Laundry, Logic, Cryptography, Caesar Cipher, Bitmaps, Word Search, Wire Sequence".Split(new string[] { ", " }, StringSplitOptions.None),
      "Two Bits, Foreign Exchange Rates, Adventure Game, Chess, Murder, Third Base, Complicated Wires".Split(new string[] { ", " }, StringSplitOptions.None),
      "Listening, Unordered Keys, Plumbing, Safety Safe, Misordered Keys, Piano Keys, Password".Split(new string[] { ", " }, StringSplitOptions.None),
      "T-Words, Word Scramble, Anagrams, Recorded Keys, Simon States, Round Keypad, Maze".Split(new string[] { ", " }, StringSplitOptions.None),
      "Bordered Keys, Follow The Leader, Blind Alley, Hexamaze, Battleship, LED Encryption, Password".Split(new string[] { ", " }, StringSplitOptions.None),
      "Sink, Morse-a-Maze, Algebra, Color Flash, 3D Tunnels, 3D Maze, Wire Sequence".Split(new string[] { ", " }, StringSplitOptions.None),
      "Mastermind Cruel, Extended Password, Human Resourses, Poker, Mashematics, Skyrim, Complicated Wires".Split(new string[] { ", " }, StringSplitOptions.None),
      "Cruel Piano Keys, Button Sequence, Game of LIfe Cruel, Mortal Kombat, Poetry, Mafia, Morse Code".Split(new string[] { ", " }, StringSplitOptions.None),
      "Led Grid, Polyhedral Maze, Symbolic Coordinates, Faulty Backgrounds, Radiator, Colored Switches, Memory".Split(new string[] { ", " }, StringSplitOptions.None),
      "Identity Parade, Visual Impairment, The Swan, The Iphone, X-ray, Waste Management, Who’s on First".Split(new string[] { ", " }, StringSplitOptions.None),
      "Maintenance, Color Generator, S.E.T., Painting, Monsplode Trading Cards, Flags, Simon Says".Split(new string[] { ", " }, StringSplitOptions.None),
      "Curriculum, Braille, Gridlock, Blind Maze, The Jukebox, Perplexing Wires, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "Symbol Cycle, Modern Cipher, Timezone, Color Morse, Sonic the Hedgehog, Nonogram, Keypad".Split(new string[] { ", " }, StringSplitOptions.None),
      "Backgrounds, Festive Piano Keys, Mastermind Simple, Game of Life Simple, Hunting, Big Circle, Wires".Split(new string[] { ", " }, StringSplitOptions.None)
   };
   List<int> SNLookupList = new List<int>() {4, 3, 6, 1, 5, 2, 4, 6, 1, 4, 3, 6, 1, 2, 4, 5};
   int LookupSNDigit;
   int LookupSNDTableDigit;
   char SNCharacterLookup;
   int Table3LookupDigit;
   string CorrectModuleToSolve = "[none]";
   List<string> IgnoringModulesList = new List<string>() {"Multiverse Synchronization"};
   List<string> AllSolvedModules = new List<string>();
   bool hasValidModule = false;
   bool ValidStartingAtoM = false;

   char[,] TableStep1 = 
   {
      {'1',    '2',  '3',  '4',  '5',  '6',  '7',	'8', 	'9', 	'0', 	'1',	'2', 	'3', 	'4', 	'5', 	'6', 	'7', 	'8', 	'9', 	'0', 	'1', 	'2', 	'3', 	'4', 	'5', 	'6'},
      {'1',	   '3',	'2', 	'4', 	'3',	'5', 	'4', 	'6', 	'5', 	'7',	'6',	'8', 	'7', 	'9', 	'8', 	'0', 	'9', 	'1', 	'0', 	'2', 	'1', 	'3', 	'2', 	'4', 	'3', 	'5'},
      {'8', 	'7', 	'6', 	'5', 	'4', 	'3',	'2' ,	'1' ,	'0', 	'9', 	'8', 	'7',	'6' ,	'5' ,	'4' ,	'3' ,	'2' ,	'1' ,	'0' ,	'8',  '6', 	'3', 	'1', 	'5', 	'2', 	'9'},
      {'6', 	'7' ,	'8', 	'9', 	'0', 	'2', 	'3', 	'5', 	'2', 	'5', 	'2', 	'1', 	'7', 	'9', 	'3', 	'2', 	'1', 	'6', 	'8',	'5',	'2' ,	'8' ,	'5', 	'6', 	'7', 	'0'},
      {'1', 	'4', 	'6', 	'9', 	'2', 	'4', 	'6', 	'8', 	'1', 	'5', 	'7', 	'2', 	'1', 	'3', 	'5', 	'7', 	'9',	'0' ,	'2', 	'4', 	'6', 	'8', 	'7', 	'2', 	'8', 	'3'},
      {'8', 	'1', 	'4', 	'2', 	'6', 	'7', 	'8', 	'8', 	'7', 	'4', 	'5', 	'1', 	'3', 	'7', 	'8', 	'1', 	'2', 	'6', 	'3', 	'7', 	'1', 	'2', 	'7', 	'2', 	'7', 	'2'},
      {'0', 	'2', 	'6', 	'2', 	'5', 	'4', 	'7', 	'2', 	'4', 	'1', 	'7', 	'7', 	'2', 	'5', 	'3', 	'7', 	'3', 	'9', 	'6', 	'1', 	'3', 	'6', 	'5', 	'4', 	'1', 	'8'},
      {'1', 	'4', 	'6', 	'2', 	'6',	'3', 	'1', 	'1', 	'8', 	'6', 	'7', 	'4', 	'7', 	'2', 	'8', 	'4', 	'2', 	'8', 	'5', 	'3', 	'1', 	'9', 	'7', 	'4', 	'2', 	'4'},
      {'6', 	'5', 	'4', 	'3', 	'2', 	'1', 	'0', 	'9', 	'8', 	'7', 	'6', 	'5', 	'4', 	'3', 	'3', 	'1', 	'2',	'9', 	'7', 	'5', 	'4', 	'1', 	'2', 	'5', 	'6', 	'8'},
      {'1',    '2',  '3',  '4',  '5',  '6',  '7',	'8', 	'9', 	'0', 	'1',	'2', 	'3', 	'4', 	'5', 	'6', 	'7', 	'8', 	'9', 	'0', 	'1', 	'2', 	'3', 	'4', 	'5', 	'6'},
      {'1',	   '3',	'2', 	'4', 	'3',	'5', 	'4', 	'6', 	'5', 	'7',	'6',	'8', 	'7', 	'9', 	'8', 	'0', 	'9', 	'1', 	'0', 	'2', 	'1', 	'3', 	'2', 	'4', 	'3', 	'5'},
      {'8', 	'7', 	'6', 	'5', 	'4', 	'3',	'2' ,	'1' ,	'0', 	'9', 	'8', 	'7',	'6' ,	'5' ,	'4' ,	'3' ,	'2' ,	'1' ,	'0' ,	'8',  '6', 	'3', 	'1', 	'5', 	'2', 	'9'},
      {'6', 	'7' ,	'8', 	'9', 	'0', 	'2', 	'3', 	'5', 	'2', 	'5', 	'2', 	'1', 	'7', 	'9', 	'3', 	'2', 	'1', 	'6', 	'8',	'5',	'2' ,	'8' ,	'5', 	'6', 	'7', 	'0'},
      {'1', 	'4', 	'6', 	'9', 	'2', 	'4', 	'6', 	'8', 	'1', 	'5', 	'7', 	'2', 	'1', 	'3', 	'5', 	'7', 	'9',	'0' ,	'2', 	'4', 	'6', 	'8', 	'7', 	'2', 	'8', 	'3'},
      {'8', 	'1', 	'4', 	'2', 	'6', 	'7', 	'8', 	'8', 	'7', 	'4', 	'5', 	'1', 	'3', 	'7', 	'8', 	'1', 	'2', 	'6', 	'3', 	'7', 	'1', 	'2', 	'7', 	'2', 	'7', 	'2'},
      {'0', 	'2', 	'6', 	'2', 	'5', 	'4', 	'7', 	'2', 	'4', 	'1', 	'7', 	'7', 	'2', 	'5', 	'3', 	'7', 	'3', 	'9', 	'6', 	'1', 	'3', 	'6', 	'5', 	'4', 	'1', 	'8'},
      {'1', 	'4', 	'6', 	'2', 	'6',	'3', 	'1', 	'1', 	'8', 	'6', 	'7', 	'4', 	'7', 	'2', 	'8', 	'4', 	'2', 	'8', 	'5', 	'3', 	'1', 	'9', 	'7', 	'4', 	'2', 	'4'},
      {'6', 	'5', 	'4', 	'3', 	'2', 	'1', 	'0', 	'9', 	'8', 	'7', 	'6', 	'5', 	'4', 	'3', 	'3', 	'1', 	'2',	'9', 	'7', 	'5', 	'4', 	'1', 	'2', 	'5', 	'6', 	'8'},
      {'1',    '2',  '3',  '4',  '5',  '6',  '7',	'8', 	'9', 	'0', 	'1',	'2', 	'3', 	'4', 	'5', 	'6', 	'7', 	'8', 	'9', 	'0', 	'1', 	'2', 	'3', 	'4', 	'5', 	'6'},
      {'1',	   '3',	'2', 	'4', 	'3',	'5', 	'4', 	'6', 	'5', 	'7',	'6',	'8', 	'7', 	'9', 	'8', 	'0', 	'9', 	'1', 	'0', 	'2', 	'1', 	'3', 	'2', 	'4', 	'3', 	'5'},
      {'8', 	'7', 	'6', 	'5', 	'4', 	'3',	'2' ,	'1' ,	'0', 	'9', 	'8', 	'7',	'6' ,	'5' ,	'4' ,	'3' ,	'2' ,	'1' ,	'0' ,	'8',  '6', 	'3', 	'1', 	'5', 	'2', 	'9'},
      {'6', 	'7' ,	'8', 	'9', 	'0', 	'2', 	'3', 	'5', 	'2', 	'5', 	'2', 	'1', 	'7', 	'9', 	'3', 	'2', 	'1', 	'6', 	'8',	'5',	'2' ,	'8' ,	'5', 	'6', 	'7', 	'0'},
      {'1', 	'4', 	'6', 	'9', 	'2', 	'4', 	'6', 	'8', 	'1', 	'5', 	'7', 	'2', 	'1', 	'3', 	'5', 	'7', 	'9',	'0' ,	'2', 	'4', 	'6', 	'8', 	'7', 	'2', 	'8', 	'3'},
      {'8', 	'1', 	'4', 	'2', 	'6', 	'7', 	'8', 	'8', 	'7', 	'4', 	'5', 	'1', 	'3', 	'7', 	'8', 	'1', 	'2', 	'6', 	'3', 	'7', 	'1', 	'2', 	'7', 	'2', 	'7', 	'2'},
      {'0', 	'2', 	'6', 	'2', 	'5', 	'4', 	'7', 	'2', 	'4', 	'1', 	'7', 	'7', 	'2', 	'5', 	'3', 	'7', 	'3', 	'9', 	'6', 	'1', 	'3', 	'6', 	'5', 	'4', 	'1', 	'8'},
      {'1', 	'4', 	'6', 	'2', 	'6',	'3', 	'1', 	'1', 	'8', 	'6', 	'7', 	'4', 	'7', 	'2', 	'8', 	'4', 	'2', 	'8', 	'5', 	'3', 	'1', 	'9', 	'7', 	'4', 	'2', 	'4'},
      {'6', 	'5', 	'4', 	'3', 	'2', 	'1', 	'0', 	'9', 	'8', 	'7', 	'6', 	'5', 	'4', 	'3', 	'3', 	'1', 	'2',	'9', 	'7', 	'5', 	'4', 	'1', 	'2', 	'5', 	'6', 	'8'},
      {'1',    '2',  '3',  '4',  '5',  '6',  '7',	'8', 	'9', 	'0', 	'1',	'2', 	'3', 	'4', 	'5', 	'6', 	'7', 	'8', 	'9', 	'0', 	'1', 	'2', 	'3', 	'4', 	'5', 	'6'},
      {'1',	   '3',	'2', 	'4', 	'3',	'5', 	'4', 	'6', 	'5', 	'7',	'6',	'8', 	'7', 	'9', 	'8', 	'0', 	'9', 	'1', 	'0', 	'2', 	'1', 	'3', 	'2', 	'4', 	'3', 	'5'},
      {'8', 	'7', 	'6', 	'5', 	'4', 	'3',	'2' ,	'1' ,	'0', 	'9', 	'8', 	'7',	'6' ,	'5' ,	'4' ,	'3' ,	'2' ,	'1' ,	'0' ,	'8',  '6', 	'3', 	'1', 	'5', 	'2', 	'9'},
      {'6', 	'7' ,	'8', 	'9', 	'0', 	'2', 	'3', 	'5', 	'2', 	'5', 	'2', 	'1', 	'7', 	'9', 	'3', 	'2', 	'1', 	'6', 	'8',	'5',	'2' ,	'8' ,	'5', 	'6', 	'7', 	'0'},
      {'1', 	'4', 	'6', 	'9', 	'2', 	'4', 	'6', 	'8', 	'1', 	'5', 	'7', 	'2', 	'1', 	'3', 	'5', 	'7', 	'9',	'0' ,	'2', 	'4', 	'6', 	'8', 	'7', 	'2', 	'8', 	'3'},
      {'8', 	'1', 	'4', 	'2', 	'6', 	'7', 	'8', 	'8', 	'7', 	'4', 	'5', 	'1', 	'3', 	'7', 	'8', 	'1', 	'2', 	'6', 	'3', 	'7', 	'1', 	'2', 	'7', 	'2', 	'7', 	'2'},
      {'0', 	'2', 	'6', 	'2', 	'5', 	'4', 	'7', 	'2', 	'4', 	'1', 	'7', 	'7', 	'2', 	'5', 	'3', 	'7', 	'3', 	'9', 	'6', 	'1', 	'3', 	'6', 	'5', 	'4', 	'1', 	'8'},
      {'1', 	'4', 	'6', 	'2', 	'6',	'3', 	'1', 	'1', 	'8', 	'6', 	'7', 	'4', 	'7', 	'2', 	'8', 	'4', 	'2', 	'8', 	'5', 	'3', 	'1', 	'9', 	'7', 	'4', 	'2', 	'4'},
      {'6', 	'5', 	'4', 	'3', 	'2', 	'1', 	'0', 	'9', 	'8', 	'7', 	'6', 	'5', 	'4', 	'3', 	'3', 	'1', 	'2',	'9', 	'7', 	'5', 	'4', 	'1', 	'2', 	'5', 	'6', 	'8'}
   };

   bool AllowedToSolve = false;
   bool ZeroSecondsLeft = false;
   bool AMinuteLeft = false;
   string ModuleThatWasJustSolved;
   int FailedToSolve = 0;
   string FormattedTime = "";
   string[] Timestamps = new string[] {"0", "0", "0", "0"};

   static int ModuleIdCounter = 1;
   int ModuleId;
   private bool ModuleSolved;

   void Awake () { //Avoid doing calculations in here regarding edgework. Just use this for setting up buttons for simplicity.
      ModuleId = ModuleIdCounter++;
      GetComponent<KMBombModule>().OnActivate += Activate;

      //button.OnInteract += delegate () { buttonPress(); return false; };
      AddMinutesButton.OnInteract += delegate () { AddMinutesButtonPress(); return false; };
      SubMinutesButton.OnInteract += delegate () { SubMinutesButtonPress(); return false; };
      AddDozenSecondsButton.OnInteract += delegate () { AddDozenSecondsButtonPress(); return false; };
      SubDozenSecondsButton.OnInteract += delegate () { SubDozenSecondsButtonPress(); return false; };
      AddSecondsButton.OnInteract += delegate () { AddSecondsButtonPress(); return false; };
      SubSecondsButton.OnInteract += delegate () { SubSecondsButtonPress(); return false; };
      SyncButton.OnInteract += delegate () { SyncButtonPress(); return false; };

   }

   void Step1Solving() {
      SerialNumber = Bomb.GetSerialNumber();
      ModuleCount = Bomb.GetModuleNames().Count();

      if (ModuleCount <= 11)
      {
         CorrectSolveAmount = ModuleCount / 2;
         LessThan11Modules = true;
      }

      IndicatorsAndBatteries = Bomb.GetIndicators().Count<string>() + Bomb.GetBatteryCount();
      
      //Alphanumeric shit
      AlphanumericSNLetters = 0;
      foreach (char c in SerialNumber.ToUpper())
      {
         if (char.IsLetter(c))
         {
            AlphanumericSNLetters += (c - 'A' + 1);
         }
      }

      SNDigitSum = SerialNumber.Where(char.IsDigit).Sum(c => c - '0');

      Debug.LogFormat("[Multiverse Synchronization #{0}] Step 1 Unconverted digits: {1} modules, {2} indicators + batteries, {3} alphanumeric letter sum, {4} seconds on bomb, {5} strikes, {6} SN digits sum", ModuleId, ModuleCount, IndicatorsAndBatteries, AlphanumericSNLetters, BombTotalTime, CurrentStrikes, SNDigitSum);
   
      SolveKeyPartTwo = "";
      SolveKeyPartTwo = SolveKeyPartTwo + (char)('A' + ((ModuleCount % 26) + 1) - 1);
      SolveKeyPartTwo = SolveKeyPartTwo + (char)('A' + ((IndicatorsAndBatteries % 26) + 1) - 1);
      SolveKeyPartTwo = SolveKeyPartTwo + (char)('A' + ((AlphanumericSNLetters % 26) + 1) - 1);
      SolveKeyPartTwo = SolveKeyPartTwo + (char)('A' + ((BombTotalTime % 26) + 1) - 1);
      SolveKeyPartTwo = SolveKeyPartTwo + (char)('A' + ((CurrentStrikes % 26) + 1) - 1);
      SolveKeyPartTwo = SolveKeyPartTwo + (char)('A' + ((SNDigitSum % 26) + 1) - 1);

      Debug.LogFormat("[Multiverse Synchronization #{0}] Step 1 Second Part of Solve key: {1}", ModuleId, SolveKeyPartTwo);

      //Solve Key Solving
      SolveKeyString = "";
      for (int i = 0; i < 6; i++)
      {
         rowT1 = 0;
         columnT1 = 0;

         if (char.IsLetter(SerialNumber[i]))
         {
            rowT1 = (SerialNumber[i] - 'A' + 1) - 1;
         }
         else
         {
            rowT1 = SerialNumber[i] - '0' + 26;
         }

         columnT1 = (SolveKeyPartTwo[i] - 'A' + 1) - 1;

         SolveKeyString += TableStep1[rowT1, columnT1];

      }

      Debug.LogFormat("[Multiverse Synchronization #{0}] Solve Key: {1}", ModuleId, SolveKeyString);

      SolveKey = int.Parse(SolveKeyString);
      if (LessThan11Modules)
      {
         Debug.LogFormat("[Multiverse Synchronization #{0}] Bomb has <= 11 modules. Correct Count of Solved Modules: {1}", ModuleId, CorrectSolveAmount);
      }
      else
      {
         CorrectSolveAmount = SolveKey % (ModuleCount - 7);

         if (Bomb.GetSolvedModuleNames().Count() > CorrectSolveAmount)
         {
            while (Bomb.GetSolvedModuleNames().Count() > CorrectSolveAmount)
            {
               CorrectSolveAmount += 4;
            }
         }

         Debug.LogFormat("[Multiverse Synchronization #{0}] Correct Count of Solved Modules: {1}", ModuleId, CorrectSolveAmount);
      }    
   }

   void Step2Solving() {

      Rule04 = false;
      Rule10 = true;
      SkipAll = false;
      TimestampSeconds = 0;
      TimestampMinutes = 0;
      AppliedRulesS2 = "";
      Has1EmptyPortPlate = false;
      Has2EmptyPortPlate = false;

      if (Bomb.GetBatteryCount() == 0)
      {
         Rule04 = true;
         TimestampSeconds += 167;
         AppliedRulesS2 += "19, ";
      }
      if (Bomb.IsPortPresent(Port.Parallel))
      {
         TimestampSeconds += 120;
         AppliedRulesS2 += "1, ";
      }
      if (Bomb.IsIndicatorPresent(Indicator.CLR))
      {
         TimestampSeconds += 47;
         AppliedRulesS2 += "2, ";
      }
      if (Bomb.GetBatteryCount() % 2 == 0)
      {
         TimestampSeconds -= 89;
         AppliedRulesS2 += "3, ";
      }
      if (Rule04)
      {
         TimestampSeconds = TimestampSeconds - 9;
         AppliedRulesS2 += "4, ";
      }
      if (Bomb.IsIndicatorPresent(Indicator.NSA))
      {
         TimestampSeconds = TimestampSeconds + 193;
         AppliedRulesS2 += "5, ";
      }
      if (Bomb.IsPortPresent(Port.DVI))
      {
         TimestampSeconds = TimestampSeconds - 180;
         AppliedRulesS2 += "6, ";
      }
      else if (Bomb.IsPortPresent(Port.StereoRCA))
      {
         TimestampSeconds = TimestampSeconds + 120;
         AppliedRulesS2 += "6, ";
      }
      if (!Bomb.IsPortPresent(Port.RJ45))
      {
         TimestampSeconds = TimestampSeconds - 17;
         AppliedRulesS2 += "7, ";
      }
      if (Bomb.GetPortCount(Port.Parallel) == 1 && Bomb.GetPortCount(Port.Serial) == 1)
      {
         Rule10 = false;
         AppliedRulesS2 += "8, ";
      }
      if (Bomb.IsIndicatorPresent(Indicator.BOB))
      {
         TimestampSeconds = TimestampSeconds - 67;
         AppliedRulesS2 += "9, ";
      }
      foreach (object[] plate in Bomb.GetPortPlates())      
      {
         if (plate.Length == 0)
         {
            if (Has1EmptyPortPlate)
            {
               Has2EmptyPortPlate = true;
               break;
            }
            else
            {
               Has1EmptyPortPlate = true;
            }
         }
      }
      if (Rule10 == true && Has2EmptyPortPlate == true)
      {
         TimestampSeconds = TimestampSeconds + 600;
         SkipAll = true;
         AppliedRulesS2 += "10, ";
      }
      foreach (int digit in Bomb.GetSerialNumberNumbers())
      {
         LastSNDigit = digit;
      }
      if (!SkipAll && LastSNDigit % 2 == 0)
      {
         TimestampSeconds = TimestampSeconds + 79;
         AppliedRulesS2 += "13, 11, ";
      }
      if (!SkipAll && Bomb.IsIndicatorPresent(Indicator.SIG))
      {
         TimestampSeconds += 98;
         AppliedRulesS2 += "12, ";
      }
      if (!SkipAll && Bomb.IsIndicatorPresent(Indicator.TRN))
      {
         TimestampSeconds += 422;
         AppliedRulesS2 += "14, ";
      }
      if (!SkipAll && Bomb.IsPortPresent(Port.Serial))
      {
         TimestampSeconds = TimestampSeconds - 315;
         AppliedRulesS2 += "15, ";
      }
      if (!SkipAll && !Rule04)
      {
         TimestampSeconds += 402;
         AppliedRulesS2 += "16, ";
      }
      if (!SkipAll && Bomb.GetSerialNumber().Any(ch => "AEIOU".Contains(ch)))
      {
         TimestampSeconds += 240;
         AppliedRulesS2 += "17, ";
      }
      if (!SkipAll && TimestampSeconds % 2 == 0)
      {
         TimestampSeconds = TimestampSeconds - 147;
         AppliedRulesS2 += "18, ";
      }
      if (TimestampSeconds < 0)
      {
         TimestampSeconds += 720;
         AppliedRulesS2 += "20, ";
      }

      TimestampMinutes = TimestampSeconds / 60;
      TimestampSeconds = TimestampSeconds % 60;

      FormattedCorrectTime = TimestampMinutes.ToString("00") + ":" + TimestampSeconds.ToString("00");
      Debug.LogFormat("[Multiverse Synchronization #{0}] Step 2: Timer must be set to {1}. Rules {2}were applied.", ModuleId, FormattedCorrectTime, AppliedRulesS2);
   }

   void Step3Solving() {
      LookupSNDTableDigit = SolveKey % 16;
      LookupSNDigit = SNLookupList[LookupSNDTableDigit];
      SNCharacterLookup = SerialNumber[LookupSNDigit-1];
      Debug.LogFormat("[Multiverse Synchronization #{0}] Step 3: Solve Key modulo 16 is {1}, so looking up {2}º digit, which is {3}.", ModuleId, LookupSNDTableDigit, LookupSNDigit, SNCharacterLookup);

      if (char.IsLetter(SNCharacterLookup))
      {
         Table3LookupDigit = (SNCharacterLookup - 'A' + 1) - 1;
      }
      else
      {
         Table3LookupDigit = SNCharacterLookup - '0' + 26;
      }

      hasValidModule = false;
      foreach (string module in TableStep3Modules[Table3LookupDigit])
      {
         if (Bomb.GetSolvableModuleNames().Contains(module))
         {
            if (IgnoringModulesList.Contains(module))
            {
               continue;
            }
            else
            {
               hasValidModule = true;
               CorrectModuleToSolve = module;
               Debug.LogFormat("[Multiverse Synchronization #{0}] Step 3 - Bomb has a valid module: {1}", ModuleId,  CorrectModuleToSolve);
               break;
            }
         }
      }

      ValidStartingAtoM = false;
      if (!hasValidModule)
      {
         foreach (string module in Bomb.GetSolvableModuleNames())
         {
            if (module.Length > 0 && char.ToUpper(module[0]) >= 'A' && char.ToUpper(module[0]) <= 'M' && module != "Multiverse Synchronization")
            {
               Debug.LogFormat("[Multiverse Synchronization #{0}] Step 3 - Bomb doesn't has a valid module, but has module(s) starting with the first half of the alphabet.", ModuleId);
               CorrectModuleToSolve = "##firstHalf";
               ValidStartingAtoM = true;
               break;
            }
         }
         if (!ValidStartingAtoM)
         {
            CorrectModuleToSolve = "##secondHalf";
            Debug.LogFormat("[Multiverse Synchronization #{0}] Step 3 - Bomb doesn't has a valid module or module(s) starting with the first half of the alphabet. Any module on the second half of the alphabet is correct.", ModuleId);
         }
         ValidStartingAtoM = true;
      }
   }

   void UpdateDisplay() {
      if (!TimerRunning || !ModuleSolved)
      {
         SecondsDisplay.text = SecondTime.ToString();
         DozenSecondDisplay.text = DozenSecondTime.ToString();
         MinutesDisplay.text = MinuteTime.ToString();
         DozenMinutesDisplay.text = DozenMinuteTime.ToString();

         FormattedTime = "";

         Timestamps[0] = DozenMinuteTime.ToString();
         Timestamps[1] = MinuteTime.ToString();
         Timestamps[2] = DozenSecondTime.ToString();
         Timestamps[3] = SecondTime.ToString();

      FormattedTime = Timestamps[0] + Timestamps[1] + ":" + Timestamps[2] + Timestamps[3]; 
      }     
   }

   void SyncButtonPress() {
      GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
      AddSecondsButton.AddInteractionPunch();
      if (!TimerRunning)
      {
         if (Bomb.GetSolvedModuleNames().Count() == CorrectSolveAmount)
         {
            if (FormattedCorrectTime == FormattedTime)
            {
               StartCoroutine(Countdown());
               Debug.LogFormat("[Multiverse Synchronization #{0}] Defuser asked to sync multiverse at {1}{2}:{3}{4}, which is correct. Starting countdown...", ModuleId, DozenMinuteTime, MinuteTime, DozenSecondTime, SecondTime);
               Audio.PlaySoundAtTransform("TimerStart", transform);
               TimerRunning = true;
               StopTimer = false;
               AllSolvedModules = Bomb.GetSolvedModuleNames();
            }
            else
            {
               if (!ModuleSolved)
               {
                  Strike();
                  Debug.LogFormat("[Multiverse Synchronization #{0}] Strike! Defuser asked to sync at {1}, expected {2}.", ModuleId, FormattedTime, FormattedCorrectTime);
               }
            }
         }
         else
         {
            if (!ModuleSolved)
            {
               Strike();
               Debug.LogFormat("[Multiverse Synchronization #{0}] Strike! Defuser asked to sync at {1} solves, expected {2}.", ModuleId, Bomb.GetSolvedModuleNames().Count(), CorrectSolveAmount);
            }
         } 
      }
   }

   void AddSecondsButtonPress() {
      if (!TimerRunning)
      {
         if (SecondTime == 9)
         {
            SecondTime = 0;
            if (DozenSecondTime == 5)
            {
               DozenSecondTime = 0;
               if (MinuteTime == 9)
               {
                  MinuteTime = 0;
                  if (DozenMinuteTime == 9)
                  {
                     DozenMinuteTime = 0;
                     MinuteTime = 0;
                     DozenSecondTime = 0;
                     SecondTime = 0;
                  }
                  else
                  {
                     DozenMinuteTime++;
                  }
               }
               else
               {
                  MinuteTime++;
               }
            }
            else
            {
               DozenSecondTime++;
            }
         }
         else
         {
            SecondTime++;
         }
         GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
         AddSecondsButton.AddInteractionPunch();
         UpdateDisplay();
      }
   }

   void AddDozenSecondsButtonPress() {
      if (!TimerRunning)
      {
         if (DozenSecondTime == 5)
         {
            DozenSecondTime = 0;
            if (MinuteTime == 9)
            {
               MinuteTime = 0;
               if (DozenMinuteTime == 9)
               {
                  DozenMinuteTime = 0;
                  MinuteTime = 0;
                  DozenSecondTime = 0;
                  SecondTime = 0;
               }
               else
               {
                  DozenMinuteTime++;
               }
            }
            else
            {
               MinuteTime++;
            }
         }
         else
         {
            DozenSecondTime++;
         }
         GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
         AddSecondsButton.AddInteractionPunch();
         UpdateDisplay();
      }
   }

   void AddMinutesButtonPress() {
      if (!TimerRunning)
      {
         if (MinuteTime == 9)
         {
            MinuteTime = 0;
            if (DozenMinuteTime == 9)
            {
               DozenMinuteTime = 0;
               MinuteTime = 0;
               DozenSecondTime = 0;
               SecondTime = 0;
            }
            else
            {
               DozenMinuteTime++;
            }
         }
         else
         {
            MinuteTime++;
         }
         GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
         AddSecondsButton.AddInteractionPunch();
         UpdateDisplay();
      }
   }

   void SubSecondsButtonPress() {
      if (!TimerRunning)
      {
         if (SecondTime == 0)
         {
            SecondTime = 9;
            if (DozenSecondTime == 0)
            {
               DozenSecondTime = 5;
               if (MinuteTime == 0)
               {
                  MinuteTime = 9;
                  if (DozenMinuteTime == 0)
                  {
                     DozenMinuteTime = 9;
                     MinuteTime = 9;
                     DozenSecondTime = 5;
                     SecondTime = 9;
                  }
                  else
                  {
                     DozenMinuteTime--;
                  }
               }
               else
               {
                  MinuteTime--;
               }
            }
            else
            {
               DozenSecondTime--;
            }
         }
         else
         {
            SecondTime--;
         }
         GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
         AddSecondsButton.AddInteractionPunch();
         UpdateDisplay();
      }
   }

   void SubDozenSecondsButtonPress() {
      if (!TimerRunning)
      {
         if (DozenSecondTime == 0)
         {
            DozenSecondTime = 5;
            if (MinuteTime == 0)
            {
               MinuteTime = 9;
               if (DozenMinuteTime == 0)
               {
                  DozenMinuteTime = 9;
                  MinuteTime = 9;
                  DozenSecondTime = 5;
                  SecondTime = 9;
               }
               else
               {
                  DozenMinuteTime--;
               }
            }
            else
            {
               MinuteTime--;
            }
         }
         else
         {
            DozenSecondTime--;
         }
         GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
         AddSecondsButton.AddInteractionPunch();
         UpdateDisplay();
      }
   }

   void SubMinutesButtonPress() {
      if (!TimerRunning)
      {
         if (MinuteTime == 0)
         {
            MinuteTime = 9;
            if (DozenMinuteTime == 0)
            {
               DozenMinuteTime = 9;
               MinuteTime = 9;
               DozenSecondTime = 5;
               SecondTime = 9;
            }
            else
            {
               DozenMinuteTime--;
            }
         }
         else
         {
            MinuteTime--;
         }
         GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
         AddSecondsButton.AddInteractionPunch();
         UpdateDisplay();
      }
   }
   
   IEnumerator StrikeToExplode() {
      if (!ModuleSolved)
      {
         while (true)
         {
            yield return new WaitForSeconds(2f);
            Strike();
         }
      }
   }

   IEnumerator Countdown() {
      UpdateDisplay();
      DozenMinutesDisplay.color = new Color32(255, 255, 0, 255);
      MinutesDisplay.color = new Color32(255, 255, 0, 255);
      DozenSecondDisplay.color = new Color32(255, 255, 0, 255);
      SecondsDisplay.color = new Color32(255, 255, 0, 255);
      DivisorDisplay.color = new Color32(255, 255, 0, 255);
      while (FormattedTime != "00:00")
      {
         if (FormattedTime == "01:00" || (Timestamps[0] == "0" && Timestamps[1] == "0"))
         {
            yield return new WaitForSeconds(1f);
            if (SecondTime == 0)
            {
               SecondTime = 9;
               if (DozenSecondTime == 0)
               {
                  DozenSecondTime = 5;
                  if (MinuteTime == 0)
                  {
                     MinuteTime = 9;
                     if (DozenMinuteTime == 0)
                     {
                        DozenMinuteTime = 9;
                        MinuteTime = 9;
                        DozenSecondTime = 5;
                        SecondTime = 9;
                     }
                     else
                     {
                        DozenMinuteTime--;
                     }
                  }
                  else
                  {
                     MinuteTime--;
                  }
               }
               else
               {
                  DozenSecondTime--;
               }
            }
            else
            {
               SecondTime--;
            }
            DivisorDisplay.text = ".";
            DozenMinuteTime = DozenSecondTime;
            MinuteTime = SecondTime;
            DozenSecondTime = 9;
            SecondTime = 0;
            StartCoroutine(LastDigitDancing());
            UpdateDisplay();
            AMinuteLeft = true;
            Audio.PlaySoundAtTransform("OneMinuteLeft", transform);
            Audio.PlaySoundAtTransform("TickingSound", transform);
            DozenMinutesDisplay.color = new Color32(255, 113, 0, 255);
            MinutesDisplay.color = new Color32(255, 113, 0, 255);
            DozenSecondDisplay.color = new Color32(255, 113, 0, 255);
            SecondsDisplay.color = new Color32(255, 113, 0, 255);
            DivisorDisplay.color = new Color32(255, 113, 0, 255);
            while (FormattedTime != "00:00")
            {
               if (Timestamps[0] == "0")
               {
                  DozenMinutesDisplay.color = new Color32(255, 0, 0, 255);
                  MinutesDisplay.color = new Color32(255, 0, 0, 255);
                  DozenSecondDisplay.color = new Color32(255, 0, 0, 255);
                  SecondsDisplay.color = new Color32(255, 0, 0, 255);
                  DivisorDisplay.color = new Color32(255, 0, 0, 255);
               }

               yield return new WaitForSeconds(0.09f);
               if (DozenSecondTime == 0)
               {
                  DozenSecondTime = 9;
                  if (MinuteTime == 0)
                  {
                     MinuteTime = 9;
                     DozenMinuteTime--;
                     Audio.PlaySoundAtTransform("TickingSound", transform);

                     if (Timestamps[0] == "1" && Timestamps[1] == "1")
                     {
                        Audio.PlaySoundAtTransform("Count10", transform);
                     }

                     if (Timestamps[0] == "1" && Timestamps[1] == "0")
                     {
                        Audio.PlaySoundAtTransform("Count09", transform);
                     }

                     if (Timestamps[0] == "3" && Timestamps[1] == "1")
                     {
                        Audio.PlaySoundAtTransform("30SecondsLeft", transform);
                     }

                     if (Timestamps[0] == "0")
                     {
                        if (Timestamps[1] == "9")
                        {
                           Audio.PlaySoundAtTransform("Count08", transform);
                        }
                        if (Timestamps[1] == "8")
                        {
                           Audio.PlaySoundAtTransform("Count07", transform);
                        }
                        if (Timestamps[1] == "7")
                        {
                           Audio.PlaySoundAtTransform("Count06", transform);
                        }
                        if (Timestamps[1] == "6")
                        {
                           Audio.PlaySoundAtTransform("Count05", transform);
                        }
                        if (Timestamps[1] == "5")
                        {
                           Audio.PlaySoundAtTransform("Count04", transform);
                        }
                        if (Timestamps[1] == "4")
                        {
                           Audio.PlaySoundAtTransform("Count03", transform);
                        }
                        if (Timestamps[1] == "3")
                        {
                           Audio.PlaySoundAtTransform("Count02", transform);
                        }
                        if (Timestamps[1] == "2")
                        {
                           Audio.PlaySoundAtTransform("Count01", transform);
                        }
                        if (Timestamps[1] == "1")
                        {
                           ZeroSecondsLeft = true;
                           Audio.PlaySoundAtTransform("Count00", transform);
                        }
                     }
                  }
                  else
                  {
                     MinuteTime--;
                     Audio.PlaySoundAtTransform("TickingSound", transform);

                     if (Timestamps[0] == "1" && Timestamps[1] == "1")
                     {
                        Audio.PlaySoundAtTransform("Count10", transform);
                     }

                     if (Timestamps[0] == "1" && Timestamps[1] == "0")
                     {
                        Audio.PlaySoundAtTransform("Count09", transform);
                     }

                     if (Timestamps[0] == "3" && Timestamps[1] == "1")
                     {
                        Audio.PlaySoundAtTransform("30SecondsLeft", transform);
                     }

                     if (Timestamps[0] == "0")
                     {
                        if (Timestamps[1] == "9")
                        {
                           Audio.PlaySoundAtTransform("Count08", transform);
                        }
                        if (Timestamps[1] == "8")
                        {
                           Audio.PlaySoundAtTransform("Count07", transform);
                        }
                        if (Timestamps[1] == "7")
                        {
                           Audio.PlaySoundAtTransform("Count06", transform);
                        }
                        if (Timestamps[1] == "6")
                        {
                           Audio.PlaySoundAtTransform("Count05", transform);
                        }
                        if (Timestamps[1] == "5")
                        {
                           Audio.PlaySoundAtTransform("Count04", transform);
                        }
                        if (Timestamps[1] == "4")
                        {
                           Audio.PlaySoundAtTransform("Count03", transform);
                        }
                        if (Timestamps[1] == "3")
                        {
                           Audio.PlaySoundAtTransform("Count02", transform);
                        }
                        if (Timestamps[1] == "2")
                        {
                           Audio.PlaySoundAtTransform("Count01", transform);
                        }
                        if (Timestamps[1] == "1")
                        {
                           ZeroSecondsLeft = true;
                           Audio.PlaySoundAtTransform("Count00", transform);
                        }
                     }
                  }
               }
               else
               {
                  DozenSecondTime--;
               }

               UpdateDisplay();
            }
         }
         else
         {
            yield return new WaitForSeconds(1f);
            Audio.PlaySoundAtTransform("TickingSound", transform);
            if (SecondTime == 0)
            {
               SecondTime = 9;
               if (DozenSecondTime == 0)
               {
                  DozenSecondTime = 5;
                  if (MinuteTime == 0)
                  {
                     MinuteTime = 9;
                     if (DozenMinuteTime == 0)
                     {
                        DozenMinuteTime = 9;
                        MinuteTime = 9;
                        DozenSecondTime = 5;
                        SecondTime = 9;
                     }
                     else
                     {
                        DozenMinuteTime--;
                     }
                  }
                  else
                  {
                     MinuteTime--;
                  }
               }
               else
               {
                  DozenSecondTime--;
               }
            }
            else
            {
               SecondTime--;
            }
            UpdateDisplay();
         }
      }
      Debug.LogFormat("[Multiverse Synchronization #{0}] Countdown reached 00:00!", ModuleId);
      StopTimer = true;
      ZeroSecondsLeft = true;

      if (ValidStartingAtoM)
      { 
         if ((Bomb.GetSolvedModuleNames().Count() - CorrectSolveAmount) == 1)
         {
            List<string> solvedmodulelist = new List<string>(Bomb.GetSolvedModuleNames());
            foreach (string module in AllSolvedModules)
            {
               if (Bomb.GetSolvedModuleNames().Contains(module))
               {
                  Debug.LogFormat("[Multiverse Synchronization #{0}] module = {1}", ModuleId, module);
                  solvedmodulelist.Remove(module);
               }
            } 
            ModuleThatWasJustSolved = solvedmodulelist[0];
            Debug.LogFormat("[Multiverse Synchronization #{0}] Just solved module: {1}, First letter: {2}", ModuleId, ModuleThatWasJustSolved, ModuleThatWasJustSolved[0]);
            
            if (CorrectModuleToSolve == "##firstHalf")
            {
               if (char.ToUpper(ModuleThatWasJustSolved[0]) >= 'A' && char.ToUpper(ModuleThatWasJustSolved[0]) <= 'M')
               {
                  AllowedToSolve = true;
               }
            }
            if (CorrectModuleToSolve == "##secondHalf")
            {
               if (char.ToUpper(ModuleThatWasJustSolved[0]) >= 'N' && char.ToUpper(ModuleThatWasJustSolved[0]) <= 'Z')
               {
                  AllowedToSolve = true;
               }
            }
         }
      }

      if (AllowedToSolve)
      {
         Solve();
      }
      else
      {
         DozenMinuteTime = 0;
         MinuteTime = 0;
         DozenSecondTime = 0;
         SecondTime = 0;
         StopTimer = true;
         TimerRunning = false;
         ZeroSecondsLeft = false;
         DozenMinutesDisplay.color = new Color32(0, 255, 0, 255);
         MinutesDisplay.color = new Color32(0, 255, 0, 255);
         DozenSecondDisplay.color = new Color32(0, 255, 0, 255);
         SecondsDisplay.color = new Color32(0, 255, 0, 255);
         DivisorDisplay.color = new Color32(0, 255, 0, 255);
         DivisorDisplay.text = ":";
         UpdateDisplay();
         FailedToSolve++;
         if (FailedToSolve >= 3)
         {
            StartCoroutine(StrikeToExplode());
            Debug.LogFormat("[Multiverse Synchronization #{0}] Exploding! Failed to sync multiverse 3 times.", ModuleId);
         }
         else
         {
            Strike();
            Debug.LogFormat("[Multiverse Synchronization #{0}] Strike! Failed to solve the module when timer hit 0 (Strike {1}/3). Resetting timer to initial state...", ModuleId, FailedToSolve);
         }
      }
   }

   IEnumerator LastDigitDancing() {
      while (!StopTimer)
      {  yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "9";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "8";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "7";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "6";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "5";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "4";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "3";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "2";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "1";
         yield return new WaitForSeconds(0.000001f);
         SecondsDisplay.text =  "0";
      }
   }

   void OnDestroy () { //Shit you need to do when the bomb ends
      
   }

   void Activate () { //Shit that should happen when the bomb arrives (factory)/Lights turn on
      BombTotalTime = Convert.ToInt32(Bomb.GetTime());
      UpdateDisplay();
      Step1Solving();
      Step2Solving();
      Step3Solving();
   }

   void Start () { //Shit that you calculate, usually a majority if not all of the module

   }

   void Update () { //Shit that happens at any point after initialization,
      // CHECK STRIKE COUNT FOR RECALC
      if (CurrentStrikes != Bomb.GetStrikes() && !Kaboosh)
      {
         Debug.LogFormat("[Multiverse Synchronization #{0}] A strike was caused by a module on the bomb. Recalculating multiverse collapse...", ModuleId);
         CurrentStrikes = Bomb.GetStrikes();
         Step1Solving();
         Step2Solving();
         Step3Solving();
      }
      else
      {
         CurrentStrikes = Bomb.GetStrikes();
      }

      // STEP 1 - EXPLODE IF OVERFLOW OF SOLVED MODULES
      if (Bomb.GetSolvedModuleNames().Count() > CorrectSolveAmount && Kaboosh == false && ModuleSolved == false && AllowedToSolve == false)
      {
         if (Kaboosh == false && ZeroSecondsLeft == false)
         {
            Kaboosh = true;
            StartCoroutine(StrikeToExplode());
            Debug.LogFormat("[Multiverse Synchronization #{0}] Exploding! Defuser solved more than {1} modules!", ModuleId, CorrectSolveAmount);
         }
      }

      // STEP 3 - EXPLODE IF MODULE SOLVED EARLY
      if (Bomb.GetSolvedModuleNames().Contains(CorrectModuleToSolve) && ModuleSolved == false)
      {
         if (ZeroSecondsLeft == true)
         {
            AllowedToSolve = true;
         }
         else
         {
            if (Kaboosh == false)
            {
               Kaboosh = true;
               StartCoroutine(StrikeToExplode());
               Debug.LogFormat("[Multiverse Synchronization #{0}] Exploding! Defuser solved {1} earlier than needed.", ModuleId, CorrectModuleToSolve);
            }  
         }
      }

      if (!ZeroSecondsLeft)
      {
         AllSolvedModules = Bomb.GetSolvedModuleNames();
      }
   }

   void Solve () {
      GetComponent<KMBombModule>().HandlePass();
      DozenMinutesDisplay.color = new Color32(0, 255, 255, 255);
      MinutesDisplay.color = new Color32(0, 255, 255, 255);
      DozenSecondDisplay.color = new Color32(0, 255, 255, 255);
      SecondsDisplay.color = new Color32(0, 255, 255, 255);
      DivisorDisplay.color = new Color32(0, 255, 255, 255);
      Debug.LogFormat("[Multiverse Synchronization #{0}] Sucess! The Keep Talking and Nobody Explodes Multiverse has been saved! Module Solved! Good luck on the rest of your bomb. Thank you for your cooperation.", ModuleId);
      Audio.PlaySoundAtTransform("SolvedModule", transform);
      ModuleSolved = true;
   }

   void Strike () {
      if (!ModuleSolved)
      {
         GetComponent<KMBombModule>().HandleStrike();
      }
   }

#pragma warning disable 414
   private readonly string TwitchHelpMessage = @"Use [!{0} sync <##:##>] to start the timer at the specified timestamp.";
#pragma warning restore 414

   IEnumerator ProcessTwitchCommand (string Command) {

      Command = Command.ToLowerInvariant().Trim();

      Match match = Regex.Match(Command, @"^sync\s+([0-9]{2}):([0-5][0-9])$");
      if (!match.Success)
      {
         yield break;
      }
            
      int T_TimestampDozenMinutes = int.Parse(match.Groups[1].Value[0].ToString());
      int T_TimestampMinutes = int.Parse(match.Groups[1].Value[1].ToString());
      int T_TimestampDozenSeconds = int.Parse(match.Groups[2].Value[0].ToString());
      int T_TimestampSeconds = int.Parse(match.Groups[2].Value[1].ToString());

      DozenMinuteTime = T_TimestampDozenMinutes;
      MinuteTime = T_TimestampMinutes;
      DozenSecondTime = T_TimestampDozenSeconds;
      SecondTime = T_TimestampSeconds;
      UpdateDisplay();
      GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);
      SyncButtonPress();
      GetComponent<KMAudio>().PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);

      yield return null;
   }

   IEnumerator TwitchHandleForcedSolve () {

      DozenMinuteTime = 0;
      MinuteTime = 0;
      DozenSecondTime = 0;
      SecondTime = 0;
      Debug.LogFormat("[Multiverse Synchronization #{0}] Twitch Team got too lazy or busy and can't solve the multiverse?! Forcing a multiverse switch!!!", ModuleId);
      Solve();

      yield return null;
   }
}