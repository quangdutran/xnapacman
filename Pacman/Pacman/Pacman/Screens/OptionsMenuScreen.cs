#region File Description
//-----------------------------------------------------------------------------
// OptionsMenuScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using System.IO;
using System.Collections.Generic;
using System;
using System.Xml;
#endregion

namespace GameStateManagement
{
    /// <summary>
    /// The options screen is brought up over the top of the main menu
    /// screen, and gives the user a chance to configure the game
    /// in various hopefully useful ways.
    /// </summary>
    class OptionsMenuScreen : MenuScreen
    {
        #region Fields
        /*
        MenuEntry ungulateMenuEntry;
        MenuEntry languageMenuEntry;
        MenuEntry frobnicateMenuEntry;
        MenuEntry elfMenuEntry;
        */

        MenuEntry board;

        enum Ungulate
        {
            BactrianCamel,
            Dromedary,
            Llama,
        }

        static Dictionary<String, String>.Enumerator enumerator;

        public static String PathToBoard
        {
            get {
                if (!loaded)
                    LoadBoardList();

                return enumerator.Current.Value; 
            }
        }

        static bool loaded = false;

        static Ungulate currentUngulate = Ungulate.Dromedary;

        static string[] languages = { "C#", "French", "Deoxyribonucleic acid" };
        static int currentLanguage = 0;

        static bool frobnicate = true;

        static int elf = 23;

        static Dictionary<String, String> BoardsPaths = new Dictionary<string, string>();
        List<String> listOfBoards;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            // Create our menu entries.
            /*
            ungulateMenuEntry = new MenuEntry(string.Empty);
            languageMenuEntry = new MenuEntry(string.Empty);
            frobnicateMenuEntry = new MenuEntry(string.Empty);
            elfMenuEntry = new MenuEntry(string.Empty);
            */

            LoadBoardList();

            board = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry backMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.


            /*
            ungulateMenuEntry.Selected += UngulateMenuEntrySelected;
            languageMenuEntry.Selected += LanguageMenuEntrySelected;
            frobnicateMenuEntry.Selected += FrobnicateMenuEntrySelected;
            elfMenuEntry.Selected += ElfMenuEntrySelected;
             */
            board.Selected += BoardMenuEntrySelected;
            backMenuEntry.Selected += OnCancel;
            
            // Add entries to the menu.
            /*
            MenuEntries.Add(ungulateMenuEntry);
            MenuEntries.Add(languageMenuEntry);
            MenuEntries.Add(frobnicateMenuEntry);
            MenuEntries.Add(elfMenuEntry);
            */
            MenuEntries.Add(board);
            MenuEntries.Add(backMenuEntry);
        }

        private static void LoadBoardList()
        {
            String key = String.Empty;
            if (loaded)
            {
               key = enumerator.Current.Key;
            }

            BoardsPaths = new Dictionary<string, string>(); 

            DirectoryInfo di = new DirectoryInfo("Maps");
            FileInfo[] rgFiles = di.GetFiles("*.xml");

            XmlDocument document;
            foreach (FileInfo fi in rgFiles)
            {
                document = new XmlDocument();
                document.Load(fi.FullName);

                String name = document.DocumentElement.GetAttribute("name");

                BoardsPaths.Add(name, fi.FullName);

            }

            enumerator = BoardsPaths.GetEnumerator();
            enumerator.MoveNext();

            if (loaded)
            {
                bool endReached = false;
                while ((enumerator.Current.Key != key || endReached) &&  !(enumerator.Current.Key != key && endReached))
                {
                    endReached = !enumerator.MoveNext();
                }

                if (enumerator.Current.Key != key && endReached)
                {
                    enumerator = BoardsPaths.GetEnumerator();
                    enumerator.MoveNext();
                }
            }

            loaded = true;
        }


        /// <summary>
        /// Fills in the latest values for the options screen menu text.
        /// </summary>
        void SetMenuEntryText()
        {
            board.Text = "Board: " + enumerator.Current.Key;

            /*
            ungulateMenuEntry.Text = "Preferred ungulate: " + currentUngulate;
            languageMenuEntry.Text = "Language: " + languages[currentLanguage];
            frobnicateMenuEntry.Text = "Frobnicate: " + (frobnicate ? "on" : "off");
            elfMenuEntry.Text = "elf: " + elf;
             */ 
        }


        #endregion

        #region Handle Input

/*
        /// <summary>
        /// Event handler for when the Ungulate menu entry is selected.
        /// </summary>
        void UngulateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentUngulate++;

            if (currentUngulate > Ungulate.Llama)
                currentUngulate = 0;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Language menu entry is selected.
        /// </summary>
        void LanguageMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            currentLanguage = (currentLanguage + 1) % languages.Length;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Frobnicate menu entry is selected.
        /// </summary>
        void FrobnicateMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            frobnicate = !frobnicate;

            SetMenuEntryText();
        }


        /// <summary>
        /// Event handler for when the Elf menu entry is selected.
        /// </summary>
        void ElfMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            elf++;

            SetMenuEntryText();
        }
*/


        void BoardMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            if (!enumerator.MoveNext())
            {
                enumerator = BoardsPaths.GetEnumerator();
                enumerator.MoveNext();
            }

            SetMenuEntryText();
        }

        #endregion
    }
}
