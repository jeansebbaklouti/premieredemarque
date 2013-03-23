﻿

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace premieredemarque2
{
    class Gestionsons
    {
        public Game1 jeu;
        public List<SoundEffect> Lpas;
        public List<SoundEffect> Lcri;
        public Song ambiance, theme;
        public List<SoundEffect> Lroule;
        public Random rand;
        public int indicerand, indicerand2;
        public float espacesoundpas, espacesoundcart;

        public Gestionsons(Game1 g)
        {
            jeu = g;
            Lpas = new List<SoundEffect>();
            Lroule = new List<SoundEffect>();
            Lcri = new List<SoundEffect>();
            rand = new Random();
            indicerand = 6;
            indicerand2 = 4;
            espacesoundpas = 0;
            espacesoundcart = 0;
        }

        public void charger()
        {
            Lpas.Add(jeu.Content.Load<SoundEffect>("walk_1"));
            Lpas.Add(jeu.Content.Load<SoundEffect>("walk_2"));
            Lpas.Add(jeu.Content.Load<SoundEffect>("walk_3"));
            Lpas.Add(jeu.Content.Load<SoundEffect>("walk_4"));
            Lpas.Add(jeu.Content.Load<SoundEffect>("walk_5"));
            Lpas.Add(jeu.Content.Load<SoundEffect>("walk_6"));

            Lroule.Add(jeu.Content.Load<SoundEffect>("cart_1"));
            Lroule.Add(jeu.Content.Load<SoundEffect>("cart_2"));
            Lroule.Add(jeu.Content.Load<SoundEffect>("cart_3"));
            Lroule.Add(jeu.Content.Load<SoundEffect>("cart_4"));

            Lcri.Add(jeu.Content.Load<SoundEffect>("scream_01"));
            Lcri.Add(jeu.Content.Load<SoundEffect>("scream_02"));
            Lcri.Add(jeu.Content.Load<SoundEffect>("scream_03"));

            ambiance = jeu.Content.Load<Song>("ambient_shop");
            theme = jeu.Content.Load<Song>("theme");
        }

        public void Maj(Boolean marche, Boolean taper, Boolean tuer)
        {
            if (MediaPlayer.State == MediaState.Stopped)
            {
                MediaPlayer.Volume = 2.0f;
                MediaPlayer.Play(ambiance);
                MediaPlayer.Play(theme);
            }


            int tmp, tmp2, tmp3;
            if (marche == true)
            {
                tmp = (int)espacesoundpas;

                espacesoundpas = (espacesoundpas + 0.07f) % 1001;
                if (tmp != (int)espacesoundpas)
                {
                    nextpas();
                }

                tmp2 = (int)espacesoundcart;
                espacesoundcart = (espacesoundcart + 0.07f) % 1001;
                if (tmp2 != (int)espacesoundcart)
                {
                    nextcart();
                }
            }
            if (tuer == true)
            {
                choicri();
            }

        }

        public void nextpas()
        {
            int tmp = indicerand;
            do
            {
                indicerand = rand.Next(Lpas.Count);
            } while (tmp == indicerand);


            Lpas[indicerand].Play();
        }

        public void nextcart()
        {
            int tmp = indicerand;
            do
            {
                indicerand = rand.Next(Lroule.Count);
            } while (tmp == indicerand);

            Lroule[indicerand].Play();
        }

        public void choicri()
        {
            int tmp = indicerand;
            do
            {
                indicerand = rand.Next(Lcri.Count);
            } while (tmp == indicerand);

            Lcri[indicerand].Play();
        }
    }
}