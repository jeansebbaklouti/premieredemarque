using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using premieredemarque2.vetement;

namespace premieredemarque2
{
    /// <summary>
    /// Type principal pour votre jeu
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public int GameState = 0;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D SpriteMenu;
        SpriteFont police;
        Hero joueur;
        private Texture2D SpriteSplash;
        private Texture2D SpriteGameOver;

        private Gestion _gestion;

        List<Perso> Ennemis;
        Gestionsons sons;
        Texture2D SpriteTexture;

        List<Rectangle> murs;

        static Random rnd;

        Map map;

        Texture2D SpriteOthers;

        int currentLevel = 1;
        public int startTime = 0;
        public int stopTime = 0;

        private Boolean couponActif = false;
        private Coupon cp;

        public Boolean loadGame = false;
        public Boolean endGame = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Permet au jeu de s’initialiser avant le démarrage.
        /// Emplacement pour la demande de services nécessaires et le chargement de contenu
        /// non graphique. Calling base.Initialize passe en revue les composants
        /// et les initialise.
        /// </summary>
        protected override void Initialize()
        {
            rnd = new Random();
            murs = new List<Rectangle>();
            sons = new Gestionsons(this);

            map = new Map(graphics, currentLevel, murs);
            joueur = new Hero(this, map.getStart());
            _gestion = new Gestion(this, 4, map, joueur);
            _gestion.charger();
            Ennemis = new List<Perso>();
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));

            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));
            Ennemis.Add(new Perso(this, findEmptyCase(rnd)));


            base.Initialize();


        }


        /// <summary>
        /// LoadContent est appelé une fois par partie. Emplacement de chargement
        /// de tout votre contenu.
        /// </summary>
        protected override void LoadContent()
        {
            police = Content.Load<SpriteFont>("SpriteFont1");
            // Créer un SpriteBatch, qui peut être utilisé pour dessiner des textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _gestion.loadSoldes(0);
            SpriteTexture = Content.Load<Texture2D>("sprite");
            sons.charger();
            joueur.charger();
            murs = map.charger();
            SpriteOthers = Content.Load<Texture2D>("concurrentes");
            SpriteMenu = Content.Load<Texture2D>("menu");
            SpriteSplash = Content.Load<Texture2D>("splash");
            SpriteGameOver = Content.Load<Texture2D>("game-over");


            foreach (Perso p in Ennemis)
            {
                p.Charger();
            }

            // TODO : utiliser this.Content pour charger le contenu de jeu ici
        }



        /// <summary>
        /// UnloadContent est appelé une fois par partie. Emplacement de déchargement
        /// de tout votre contenu.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO : décharger tout le contenu non-ContentManager ici
        }

        /// <summary>
        /// Permet au jeu d’exécuter la logique de mise à jour du monde,
        /// de vérifier les collisions, de gérer les entrées et de lire l’audio.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Update(GameTime gameTime)
        {
            KeyboardState keys = Keyboard.GetState();
            if (this.GameState != 1)
            {
                if (this.GameState == 0 && !loadGame)
                {
                    MediaPlayer.Play(sons.SplashSound);
                    loadGame = true;
                }
                if (this.GameState == -1 && !endGame)
                {
                    sons.OverSound.Play();
                    endGame = true;
                }
                if (keys.GetPressedKeys().Length > 0 
                    && (this.stopTime == 0 || ((int)gameTime.TotalGameTime.TotalSeconds-this.stopTime) > 2))
                {
                    loadGame = false;
                    endGame = false;
                    this.GameState = 1;
                    this.startTime = (int)gameTime.TotalGameTime.TotalSeconds;
                }
            }
            else {
                if (!_gestion.endLevel())
                {
                    // Allows the game to exit
                    if (keys.IsKeyDown(Keys.Escape))
                        this.Exit();

                    if (keys.IsKeyDown(Keys.P))
                    {
                        currentLevel++;
                        Initialize();
                        LoadContent();
                    }
                    if (keys.IsKeyDown(Keys.M))
                    {
                        currentLevel--;
                        Initialize();
                        LoadContent();
                    }

                    _gestion.instersectVetement(joueur.hitbox, false);
                    _gestion.Update(gameTime, this.GameState);


                    foreach (Perso p in Ennemis)
                    {
                        _gestion.instersectVetement(p.hitbox, true);
                        p.Maj(gameTime, murs, _gestion.isFury);
                    }


                    // TODO: Add your update logic here
                    joueur.isFury = _gestion.isFury;
                    joueur.Maj(gameTime, Ennemis, murs);

                    sons.Maj(joueur.marche, joueur.taper, joueur.tuer, joueur.toucher, _gestion._time);
                    base.Update(gameTime);
                }
                else if (_gestion.getStatus() == 1)
                {
                    currentLevel++;
                    Initialize();
                    LoadContent();
                }
                else
                {
                    stopTime = (int)gameTime.TotalGameTime.TotalSeconds;
                    GameState = -1;
                    currentLevel = 1;
                    sons.stopTheme();
                    Initialize();
                    LoadContent();
                }
            }
        }

        /// <summary>
        /// Appelé quand le jeu doit se dessiner.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            if (this.GameState == 0)
            {
                // Splascreen
                spriteBatch.Draw(SpriteSplash, new Rectangle(0, 0, 1280, 800), Color.White);
            }
            else if (this.GameState == -1)
            {
                // Game Over
                spriteBatch.Draw(SpriteGameOver, new Rectangle(0, 0, 1280, 800), Color.White);
            }
            else
            {
                if (!_gestion.endLevel())
                {

                    map.afficher(spriteBatch, SpriteTexture, _gestion.isFury);
                    spriteBatch.Draw(SpriteMenu, new Rectangle(0, 0, 1280, 100), Color.White);

                    // Affichage Bonus coupon
                    // if (_gestion.isFury && !couponActif)
                    // {
                    //     cp = new Coupon(this, 15, gameTime.TotalGameTime.Seconds, map.getBonus());
                    //     cp.Initialize();
                    //     couponActif = true;
                    //     _gestion._currentVetements.Add(cp);
                    // }


                    // ORDRE AFFICHAGE
                    if (joueur.invincible == true && _gestion.isFury == true)
                    {
                        foreach (Perso p in Ennemis)
                        {
                            if (p.dead != 0)
                            {
                                p.afficher(spriteBatch, SpriteOthers, _gestion.isFury);
                            }
                        }
                        _gestion.Draw(spriteBatch);
                        joueur.afficher(spriteBatch);
                        foreach (Perso p in Ennemis)
                        {
                            if (p.dead == 0)
                            {
                                p.afficher(spriteBatch, SpriteOthers, _gestion.isFury);
                            }
                        }


                    }
                    else
                    {
                        foreach (Perso p in Ennemis)
                        {
                            if (p.dead != 0)
                            {
                                p.afficher(spriteBatch, SpriteOthers, _gestion.isFury);
                            }
                        }
                        _gestion.Draw(spriteBatch);
                        foreach (Perso p in Ennemis)
                        {
                            if (p.dead == 0)
                            {
                                p.afficher(spriteBatch, SpriteOthers, _gestion.isFury);
                            }
                        }
                        joueur.afficher(spriteBatch);
                    }
                }
            }

            // spriteBatch.DrawString(police, _gestion._time.ToString(), Vector2.Zero, Color.Black);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Vector2 findEmptyCase(Random rnd)
        {
            int poxX = rnd.Next(1, 23);
            int poxY = rnd.Next(1, 15);
            while (map.isEmpty(poxX, poxY))
            {
                poxX = rnd.Next(1, 23);
                poxY = rnd.Next(1, 15);
            }
            return new Vector2(poxX * 50f - 10, poxY * 50f);
        }

    }
}
