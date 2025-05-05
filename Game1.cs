using System;
using System.Drawing;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D11;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace Tetris;

public class Game1 : Game
{
    private GraphicsDeviceManager graphics;
    private SpriteBatch spriteBatch;
    private Grid grid;
    private Block currentBlock;
    private Texture2D blockTexture;
    private double dropTimer;
    private KeyboardState state1;
    private double dropInterval = 0.5;
    private int blockSize = 24;
    private bool gameover = false;
    private SpriteFont gofont;
    private Song gamemusic;
    private Texture2D tetrisgrid;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        grid = new Grid();
        SpawnNewBlock();
        base.Initialize();
    }
    private static Random random = new Random();
    private void SpawnNewBlock(){
        int type = random.Next(0,7);
        currentBlock = new Block(type);
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        blockTexture = new Texture2D(GraphicsDevice,1,1);
        blockTexture.SetData(new[]{Color.White});
        gamemusic = Content.Load<Song>("03. A-Type Music (Korobeiniki)");
        MediaPlayer.Play(gamemusic);
        tetrisgrid = Content.Load<Texture2D>("Tetris grid-Photoroom");
        gofont = Content.Load<SpriteFont>("Game over screen");



        // TODO: use this.Content to load your game content here
    }

    protected override void Update(GameTime gameTime)
    {
        if(gameover){return;}

        KeyboardState state2 = Keyboard.GetState();
        
        if(state2.IsKeyUp(Keys.Left) && state1.IsKeyDown(Keys.Left)){
            currentBlock.Move(-1,0);
            if(IsCollision(grid)){
                currentBlock.Move(1,0);
            }
        }
        if(state1.IsKeyDown(Keys.Right)){
            if(state2.IsKeyUp(Keys.Right) && state1.IsKeyDown(Keys.Right)){
                currentBlock.Move(1,0);
                if(IsCollision(grid)){
                    currentBlock.Move(-1,0);
                }
            }
        }
        
        if(state2.IsKeyDown(Keys.Down)){
            currentBlock.Move(0,1);
            if(IsCollision(grid)){
                currentBlock.Move(0,-1);
            }
            }
        if(state2.IsKeyUp(Keys.Up) && state1.IsKeyDown(Keys.Up)){
            currentBlock.Rotate(grid);
            if(IsCollision(grid)){
                currentBlock.UndoRotate();
            }
        }
       
        

        dropTimer += gameTime.ElapsedGameTime.TotalSeconds;
        if(dropTimer >= dropInterval){
            currentBlock.Move(0,1);
        
            if(IsCollision(grid)){
                currentBlock.Move(0,-1);
                grid.Blockadd(currentBlock);
                grid.ClearLines();

                bool abovegrid = false;
                for(int x = 0; x < Grid.Xrad; x++){
                    if(grid[x,0] != Color.Transparent){
                        abovegrid = true;
                        break;
                    }
                }
                if(abovegrid){
                    gameover=true;
                }else

                SpawnNewBlock();
            }
            dropTimer = 0;
        }
        state1 = state2;
        base.Update(gameTime);   
    }
    private bool IsCollision(Grid grid){
        for(int row = 0; row < currentBlock.Size; row++){
            for(int col = 0; col < currentBlock.Size; col++){
                if(currentBlock.Shape[row,col] == 1){
                    int gridX = currentBlock.X + col;
                    int gridY = currentBlock.Y + row;
                    if(gridX < 0 || gridX >= Grid.Xrad || gridY >= Grid.Yrad){
                        return true;
                    }
                    if(gridY >= 0 && grid.UpptagenCell(gridX, gridY)){
                        return true;
                    }
                }
            }
        }
        return false;
    }
    

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        spriteBatch.Begin();
        grid.Draw(spriteBatch, blockTexture, blockSize);
       
        if(gameover){
            string go = "GAME OVER";
            Vector2 size = gofont.MeasureString(go);
            var mitten = new Vector2((GraphicsDevice.PresentationParameters.BackBufferWidth - size.X)/2, (GraphicsDevice.PresentationParameters.BackBufferHeight - size.Y)/2);
            spriteBatch.DrawString(gofont, go, mitten, Color.MonoGameOrange);
        }
        else

        for(int row = 0; row < currentBlock.Size; row++){
            for(int col = 0; col < currentBlock.Size; col++){
                if(currentBlock.Shape[row,col] == 1){
                    int drawX = (currentBlock.X + col)*blockSize;
                    int drawY = (currentBlock.Y + row)*blockSize;
                    if(drawX >= 0 && drawX < Grid.Xrad * blockSize && drawY >= 0 && drawY < Grid.Yrad * blockSize){
                    spriteBatch.Draw(blockTexture, new Rectangle(drawX, drawY, blockSize, blockSize), currentBlock.PieceColor);
                    }
                }
            } 
        }
        spriteBatch.Draw(tetrisgrid, new Vector2(0,0), Color.DarkSlateGray);
        

        // TODO: Add your drawing code here
        spriteBatch.End();
        base.Draw(gameTime);
    }
}
