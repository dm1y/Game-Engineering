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
        public Matrix transform { get; private set; }
        public Viewport view { get; set; }
        public Vector2 center { get; set; }
        public Vector2 Position { get; set; }
        Vector2 playerPositionInWorldSpace;

        public Camera(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Player player)
        {
            playerPositionInWorldSpace = player.position;
            if (playerPositionInWorldSpace.X >= view.Width)
            {
               
                //need to update camera position to boundary so it doesn't continue
            }
            else if (playerPositionInWorldSpace.X <= -view.Width )
            {
                //need to update camera position to negative boundary so it doesn't continue
            }
            if (playerPositionInWorldSpace.Y >= view.Height)
            {
                //need to update camera position to boundary so it doesn't continue
            }
            else if (playerPositionInWorldSpace.Y <= -view.Height)
            {
                //need to update camera position to negative boundary so it doesn't continue
            }
            //Find the center and create the transform matrix
            center = new Vector2(playerPositionInWorldSpace.X - view.Width/2, playerPositionInWorldSpace.Y - view.Height/2);  
            transform = Matrix.CreateScale(new Vector3(1,1,0)) * Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0) );
        }
    }
}
