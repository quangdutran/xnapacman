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
using Pacman.GameManager;
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

        MenuEntry board;
        MenuEntry fullScreen;
        MenuEntry soundsVolume;

        static int soundLevel = 2;

        public static float SoundLevel
        {
            get { return OptionsMenuScreen.soundLevel/10.0f; }
            
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

        static Dictionary<String, String> BoardsPaths = new Dictionary<string, string>();
        static bool isFullScreen;

        #endregion

        #region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public OptionsMenuScreen()
            : base("Options")
        {
            LoadBoardList();
            isFullScreen = GameStateManagementGame.FullScreen;

            board = new MenuEntry(string.Empty);
            fullScreen = new MenuEntry(string.Empty);
            soundsVolume = new MenuEntry(string.Empty);

            SetMenuEntryText();

            MenuEntry backMenuEntry = new MenuEntry("Back");

            // Hook up menu event handlers.

            board.Selected += BoardMenuEntrySelected;
            fullScreen.Selected += FullScreenSelected;
            soundsVolume.Selected += soundsVolumeSelected;

            backMenuEntry.Selected += OnSave;
            
            // Add entries to the menu.
            MenuEntries.Add(board);
            MenuEntries.Add(fullScreen);
            MenuEntries.Add(soundsVolume);
            MenuEntries.Add(backMenuEntry);
        }

        private static void LoadBoardList()
        {

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

            String key = String.Empty;
            if (loaded)
            {
                key = enumerator.Current.Key;
            }

            enumerator = BoardsPaths.GetEnumerator();
            enumerator.MoveNext();

            if (loaded)
            {
                bool endReached = false;
                while ((enumerator.Current.Key != key || endReached) && !(enumerator.Current.Key != key && endReached))
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
            board.Text          = "Board:               " + enumerator.Current.Key;
            fullScreen.Text     = "Full screen:          " + (isFullScreen ? "on" : "off");
            soundsVolume.Text   = "Sound:               " + ((soundLevel==0)?"off":(soundLevel.ToString()+"0%"));
            

        }


        #endregion

        #region Handle Input

        void BoardMenuEntrySelected(object sender, PlayerIndexEventArgs e)
        {
            MoveEnumerator();
            SetMenuEntryText();
        }

        static void MoveEnumerator()
        {
            if (!enumerator.MoveNext())
            {
                enumerator = BoardsPaths.GetEnumerator();
                enumerator.MoveNext();
            }

        }

        void FullScreenSelected(object sender, PlayerIndexEventArgs e)
        {
            isFullScreen = !isFullScreen;
            SetMenuEntryText();
        }

        void soundsVolumeSelected(object sender, PlayerIndexEventArgs e)
        {
            soundLevel = (soundLevel+1) % 11;
            SoundManager.UrgentUpdateVolume();
            SetMenuEntryText();
        }


        void OnSave(object sender, PlayerIndexEventArgs e)
        {
            if (isFullScreen != GameStateManagementGame.FullScreen)
                GameStateManagementGame.FullScreen = isFullScreen;

            OnCancel(sender, e);
            
        }

        internal static void SwitchToNextBoard()
        {
            if (!loaded)
                LoadBoardList();
            MoveEnumerator();

        }

        #endregion


    }
}
