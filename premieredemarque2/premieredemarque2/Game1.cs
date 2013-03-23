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
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private Texture2D SpriteMenu;

        Hero joueur;

        private Gestion _gestion;

        List<Perso> Ennemis;
        Gestionsons sons;
        Texture2D SpriteTexture;

        List<Rectangle> murs;

        Random rnd;

        Map map;

        int currentLevel = 1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 800;
            graphics.PreferredBackBufferWidth = 1280;
            Content.RootDirectory = "Content";
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
             SpriteMenu = Content.Load<Texture2D>("menu");
            // TODO : ajouter la logique d’initialisation ici
            joueur = new Hero(this, new Vector2(400, 400));

            map = new Map(graphics, currentLevel, murs);
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

            // Créer un SpriteBatch, qui peut être utilisé pour dessiner des textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            _gestion.loadSoldes(0);
            SpriteTexture = Content.Load<Texture2D>("sprite");
            sons.charger();
            joueur.charger();
            murs = map.charger();

          

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
            if (!_gestion.endLevel())
            {
                // Allows the game to exit
                KeyboardState keys = Keyboard.GetState();
                if (keys.IsKeyDown(Keys.Escape))
                    this.Exit();

                _gestion.instersectVetement(joueur.hitbox, false);
              

                foreach (Perso p in Ennemis)
                {
                    _gestion.instersectVetement(p.hitbox, true);
                    p.Maj(gameTime, murs);
                }
                _gestion.Update(gameTime);

                // TODO: Add your update logic here
                joueur.Maj(gameTime, Ennemis, murs);

                base.Update(gameTime);
            }
            else
            {
                currentLevel++;
                Initialize();
            }
        }

        /// <summary>
        /// Appelé quand le jeu doit se dessiner.
        /// </summary>
        /// <param name="gameTime">Fournit un aperçu des valeurs de temps.</param>
        protected override void Draw(GameTime gameTime)
        {
            if (!_gestion.endLevel())
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);

                // TODO : ajouter le code de dessin ici

                // Start drawing

                spriteBatch.Begin();

                map.afficher(spriteBatch, SpriteTexture);
                spriteBatch.Draw(SpriteMenu, new Rectangle(0, 0, 1280, 100), Color.White);


                _gestion.Draw(spriteBatch);

                foreach (Perso p in Ennemis)
                {
                    p.afficher(spriteBatch);
                }

                joueur.afficher(spriteBatch);



                spriteBatch.End();

                base.Draw(gameTime);
            }
        }

        private Vector2 findEmptyCase(Random rnd)
        {
            int poxX = rnd.Next(1, map.getLengthX() -1);
            int poxY = rnd.Next(1, map.getLengthY() -1 );
            while (map.isEmpty(poxX, poxY))
            {
                poxX = rnd.Next(1, map.getLengthX() -1);
                poxY = rnd.Next(1, map.getLengthY() -1);
            }
            return new Vector2((poxX * 50) -10 , poxY * 50 );
        }

    }
}
