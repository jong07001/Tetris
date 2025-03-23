using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Tetris
{
    public class Grid
    {
        public const int Xrad = 20;
        public const int Yrad = 10;
        private readonly Color[,] cells = new Color[Yrad, Xrad];

        public Grid(){
            for(int x = 0; x < Yrad; x++){
                for(int y = 0; y < Xrad; y++){
                    cells[x,y] = Color.Transparent;
                }
            }
        }

        public bool UpptagenCell(int x, int y){
            return x < 0 || x >= Yrad || y >= Xrad || (y >= 0 && cells[x,y] != Color.Transparent);
        }




        public void Blockadd(Block block){
            for(int xrad = 0; xrad < block.Size; xrad++){
                for(int yrad = 0; yrad < block.Size; yrad++){
                    if(block.Shape[Xrad,Yrad] == 1){
                        int gridX = block.X + Yrad;
                        int gridY = block.Y + Xrad;
                        if(gridY >= 0){
                            cells[gridX, gridY] = block.Color;
                        }
                    }
                }
            }
        }


        public void ClearLine(){
            for(int y = Xrad -1; y >= 0; y--){
                bool fullLine = true;
                for(int x = 0; x < Yrad; x++){
                    if(cells[x,y] == Color.Transparent){
                        fullLine = false;
                        break;
                    }
                }
                if(fullLine){
                    for(int rad = y; rad > 0; rad--){
                        for(int x = 0; x < Yrad; x++){
                            cells[x,rad] = cells[x, rad - 1];
                        }
                    }
                    y++;
                }
            }
        }
    }
}
