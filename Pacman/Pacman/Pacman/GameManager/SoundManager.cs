using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using GameStateManagement;

namespace Pacman.GameManager
{
    class SoundManager
    {
        static SoundManager instance = new SoundManager();
        static ContentManager content;

        static float Volume
        {
            get {
                return OptionsMenuScreen.SoundLevel;
            }
        }

        #region SoundsEffects

        static SoundEffect dotSound;
        static SoundEffect pillSound;
        static SoundEffect pacmanDeadSound;
        static SoundEffect mainMenuSound;
        static SoundEffect clickSound;
        
        static SoundEffectInstance dotSoundInstance;
        static SoundEffectInstance pillSoundInstance;
        static SoundEffectInstance pacmanDeadSoundInstance;
        static SoundEffectInstance mainMenuSoundInstance;
        static SoundEffectInstance clickSoundInstance;

        #endregion



        public static ContentManager contentManager
        {
            get { return SoundManager.content; }
            set { 

                SoundManager.content = value; 
            
            }
        }

        public static void UrgentUpdateVolume()
        {
            mainMenuSoundInstance.Volume = Volume;
        }


        public static void LoadContent()
        {
            
            dotSound = content.Load<SoundEffect>("pac_dot");
            dotSoundInstance = dotSound.CreateInstance();

            pillSound = content.Load<SoundEffect>("pac_pill");
            pillSoundInstance = pillSound.CreateInstance();


            pacmanDeadSound = content.Load<SoundEffect>("pac_dead");
            pacmanDeadSoundInstance = pacmanDeadSound.CreateInstance();

            mainMenuSound = content.Load<SoundEffect>("menu");
            mainMenuSoundInstance = mainMenuSound.CreateInstance();

            clickSound = content.Load<SoundEffect>("click");
            clickSoundInstance = clickSound.CreateInstance();
        
        }


        internal static void UnloadContent()
        {
            content.Unload();
        }

        public static void PlayDotSound()
        {

            if (dotSoundInstance.State == SoundState.Playing)
                dotSoundInstance.Stop();

                dotSoundInstance = dotSound.CreateInstance();
                dotSoundInstance.Volume = Volume;
                dotSoundInstance.IsLooped = false;

                dotSoundInstance.Play();
        }

        public static void PlayPillSound()
        {

            if (pillSoundInstance.State == SoundState.Playing)
                pillSoundInstance.Stop();

            pillSoundInstance = pillSound.CreateInstance();
            pillSoundInstance.Volume = Volume;
            pillSoundInstance.IsLooped = false;

            dotSoundInstance.Pause();
            pillSoundInstance.Play();
        }

        public static void PlayPacmanDeadSound()
        {

            if (pacmanDeadSoundInstance.State == SoundState.Playing)
                pacmanDeadSoundInstance.Stop();

            pacmanDeadSoundInstance = pacmanDeadSound.CreateInstance();
            pacmanDeadSoundInstance.Volume = Volume;
            pacmanDeadSoundInstance.IsLooped = false;

            pacmanDeadSoundInstance.Play();
        }

        public static void PauseMainMenuSound()
        {
            if (!mainMenuSoundInstance.IsDisposed && mainMenuSoundInstance.State == SoundState.Playing)
            {
                mainMenuSoundInstance.Pause();
                //mainMenuSoundInstance = mainMenuSound.CreateInstance();
            }

            if (mainMenuSoundInstance.IsDisposed)
                LoadContent();
        }

        public static void PlayMainMenuSound()
        {
            mainMenuSoundInstance = mainMenuSound.CreateInstance();
            mainMenuSoundInstance.Volume = Volume;
            mainMenuSoundInstance.IsLooped = true;

            mainMenuSoundInstance.Play();
            
        }

        public static void PlayClickSound()
        {

            if (clickSoundInstance.State == SoundState.Playing)
                clickSoundInstance.Stop();

            clickSoundInstance = clickSound.CreateInstance();
            clickSoundInstance.Volume = Volume / 2;
            clickSoundInstance.IsLooped = false;

            clickSoundInstance.Play();
        }


    }
}
