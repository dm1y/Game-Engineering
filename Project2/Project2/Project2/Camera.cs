using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project2
{
    public class Camera
    {
        public Matrix transform;
        public Viewport view { get; set; }
        public Vector2 center { get; set; }

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Player player)
        {
            center = new Vector2(view.X +player.position.X/2, view.Y + player.position.Y/2);  //not correct. Will fix tomorrow.
            transform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0) );
        }
    }
}
