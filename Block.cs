using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Tetris
{
    public class Block
    {
        private List<int[,]> rotations;
        private int RotationI;
        private int x;
        private int y;
        private Color pieceColor;
        public int[,] Shape => rotations[RotationI];
        public int Size => Shape.GetLength(0);
        public int X{
            get{return x;}
            set{x = value;}
        }
        public int Y{
            get{return y;}
            set{y = value;}
        }
        public Color PieceColor => pieceColor;
        private readonly List<int[,]>[] blockShape = new List<int[,]>[]{
            new List<int[,]>{
                new int[,]{
                    {1,1,1,1},
                    {0,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0,}},
                new int[,]{
                    {1,0,0,0},
                    {1,0,0,0},
                    {1,0,0,0},
                    {1,0,0,0},
                    }
            },
            new List<int[,]>{
                new int[,]{
                    {1,1,0,0},
                    {1,1,0,0},
                    {0,0,0,0},
                    {0,0,0,0}},
            },
            new List<int[,]>{
                new int[,]{
                    {1,0,0,0},
                    {1,1,1,0},
                    {0,0,0,0},
                    {0,0,0,0},},

                new int[,]{
                    {1,1,0,0},
                    {1,0,0,0},
                    {1,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,1,1,0},
                    {0,0,1,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {0,1,0,0},
                    {0,1,0,0},
                    {1,1,0,0},
                    {0,0,0,0}
                },
            },
            new List<int[,]>{
                new int[,]{
                    {0,0,1,0},
                    {1,1,1,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,0,0,0},
                    {1,0,0,0},
                    {1,1,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,1,1,0},
                    {1,0,0,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,1,0,0},
                    {0,1,0,0},
                    {0,1,0,0},
                    {0,0,0,0}
                }
            },
            new List<int[,]>{
                new int[,]{
                    {0,1,1,0},
                    {1,1,0,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,0,0,0},
                    {1,1,0,0},
                    {0,1,0,0},
                    {0,0,0,0}
                },
            },
            new List<int[,]>{
                new int[,]{
                    {1,1,0,0},
                    {0,1,1,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {0,1,0,0},
                    {1,1,0,0},
                    {1,0,0,0},
                    {0,0,0,0}
                }
            },
            new List<int[,]>{
                new int[,]{
                    {0,1,0,0},
                    {1,1,1,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,0,0,0},
                    {1,1,0,0},
                    {1,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {1,1,1,0},
                    {0,1,0,0},
                    {0,0,0,0},
                    {0,0,0,0}
                },
                new int[,]{
                    {0,1,0,0},
                    {1,1,0,0},
                    {0,1,0,0},
                    {0,0,0,0}
                }
            }
            
        };
        private readonly Color[] pieceColors = new Color[]{
            Color.Cyan,
            Color.Yellow,
            Color.Blue,
            Color.Orange,
            Color.Green,
            Color.Red,
            Color.Purple
        };
        public Block(int type){
            rotations = blockShape[type];
            RotationI = 0;
            pieceColor = pieceColors[type];
            x = 3;
            y = -2;
        }

        public void Move(int ax, int by){
            x += ax;
            y += by;
        }
        public void Rotate(Grid grid){
            int prevRotation = RotationI;
            RotationI = (RotationI+1) % rotations.Count;
       
        }        
        public void UndoRotate(){
        RotationI = (RotationI - 1 + rotations.Count) % rotations.Count;
        }

    }
}