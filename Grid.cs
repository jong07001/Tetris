using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;


namespace Tetris
{
    public class Grid
    {
        public const int Xrad = 10;
        public const int Yrad = 20;
        private Color[,] cells;

        public Grid(){
           cells = new Color[Yrad,Xrad];
           ClearGrid();
        }

        public Color this[int x, int y]{
            get{return cells[y,x];}
            set{cells[y,x] = value;}
        }

        public bool UpptagenCell(int x, int y){
           if(x < 0 || x >= Xrad || y >= Yrad){return true;}
           if(y < 0){return false;}
           return cells[y,x] != Color.Transparent;
        }




        public void Blockadd(Block block){
            for(int row = 0; row < block.Shape.GetLength(0); row++){
                for(int col = 0; col < block.Shape.GetLength(1); col++){
                    if(block.Shape[row,col] == 1){
                        int gridX = block.X + col;
                        int gridY = block.Y + row;
                        if(gridY >= 0){cells[gridY, gridX] = block.PieceColor;}
                    }
                }
            }
        }


        public void ClearLines(){
            for(int y = Yrad - 1; y >= 0; y--){
                bool fullLine = true;
                for(int x = 0; x < Xrad; x++){
                    if(cells[y,x] == Color.Transparent){
                        fullLine = false;
                        break;
                    }
                }
                if(fullLine){
                    for(int rad = y; rad > 0; rad--){
                        for(int x = 0; x < Xrad; x++){
                            cells[rad,x] = cells[rad - 1, x];
                        }
                    }   
                    for(int x = 0; x < Xrad; x++){
                        cells[0, x] = Color.Transparent;
                    }
                    y++;
                }
            }
        }

        private void ClearGrid(){
            for(int y = 0; y < Yrad; y++){
                for(int x = 0; x < Xrad; x++){
                    cells[y,x] = Color.Transparent;
                }
            }
        }

        public void Draw(SpriteBatch spritebatch, Texture2D texture, int blockSize){
            for(int y = 0; y < Yrad; y++){
                for(int x = 0; x < Xrad; x++){
                    if(cells[y,x] != Color.Transparent){
                        spritebatch.Draw(texture, new Rectangle(x*blockSize, y*blockSize, blockSize, blockSize), cells[y,x]);
                    }
                }
            }
        }

    }
}
