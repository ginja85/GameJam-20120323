﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Growth.Input;
using Growth.Rendering;
using Growth.GameObjects.Entities;

namespace Growth.GameObjects.Templates
{
    public class OreTemplate : ITemplate
    {
        private ContentManager content;
        private EntityConstructor entityConstructor;
        private Random rand = new Random();

        public OreTemplate(EntityConstructor entityConstructor, ContentManager content)
        {
            this.content = content;
            this.entityConstructor = entityConstructor;
        }

        public Entity Make()
        {
            Texture2D texture = content.Load<Texture2D>("Sprites\\Ore" + rand.Next(1, 4));
            Sprite oreSprite = new Sprite(texture, new Vector2(texture.Width / 2, texture.Height / 2));

            float collisionRadius = (Math.Max(texture.Width, texture.Height) / Renderer.TexturePixelsPerUnit) / 2;

            return new Ore(oreSprite)
            {
                CanCollide = false,
                IsPhysical = false
            };
        }
    }
}
