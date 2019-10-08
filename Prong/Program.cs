/**
 *  Este jogo foi desenvolvido por Bruno Vinícius.
 **/

using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace testekkk
{
    class Program : GameWindow
    {

        float xDaBola = 0;
        float yDaBola = 0;
        float tamanhoDaBola = 20;
        float velocidadeDaBolaEmX = 3;
        float velocidadeDaBolaEmY = 3;

        float yDoJogador = -200;
        float xDoJogador = 0;
        float tamanhoDoJogador = 40;

        float novoTamanhoDaBola = -2;

        float yDoInimigo = 0;
        float xDoInimigo = 0;
        float tamanhoDoInimigo = 60;
        float velocidadeDoInimigoEmX = 7;
        float velocidadeDoInimigoEmY = 7;

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            xDaBola = xDaBola + velocidadeDaBolaEmX;
            yDaBola = yDaBola + velocidadeDaBolaEmY;
            if (yDaBola + tamanhoDaBola / 2 > ClientSize.Height / 2 || yDaBola - tamanhoDaBola / 2 < -ClientSize.Height / 2)
            {
                velocidadeDaBolaEmY = -velocidadeDaBolaEmY;
            }
            if (xDaBola + tamanhoDaBola / 2 > ClientSize.Width / 2 || xDaBola - tamanhoDaBola / 2 < -ClientSize.Width / 2)
            {
                velocidadeDaBolaEmX = -velocidadeDaBolaEmX;
            }

            if (xDaBola + tamanhoDaBola / 2 > xDoJogador - tamanhoDoJogador / 2
                && xDaBola - tamanhoDaBola / 2 < xDoJogador + tamanhoDoJogador / 2
                && yDaBola + tamanhoDaBola / 2 > yDoJogador - tamanhoDoJogador / 2
                && yDaBola - tamanhoDaBola / 2 < yDoJogador + tamanhoDoJogador / 2)
            {
                xDaBola = 0;
                yDaBola = 0;
                tamanhoDoJogador = tamanhoDoJogador + 8.5f;
                velocidadeDoInimigoEmX = velocidadeDoInimigoEmX + 2.5f;
                velocidadeDoInimigoEmY = velocidadeDoInimigoEmY + 2.5f;

                if (tamanhoDoJogador >= 200)
                {
                    tamanhoDoJogador = 200;
                }
            }

            xDoInimigo = xDoInimigo + velocidadeDoInimigoEmX;
            yDoInimigo = yDoInimigo + velocidadeDoInimigoEmY;
            if (xDoInimigo + tamanhoDoInimigo / 2 > ClientSize.Width / 2 || xDoInimigo - tamanhoDoInimigo / 2 < -ClientSize.Width / 2)
            {
                velocidadeDoInimigoEmX = -velocidadeDoInimigoEmX;
            }
            if (yDoInimigo + tamanhoDoInimigo / 2 > ClientSize.Height / 2 || yDoInimigo - tamanhoDoInimigo / 2 < -ClientSize.Height / 2)
            {
                velocidadeDoInimigoEmY = -velocidadeDoInimigoEmY;
            }

            if (xDoJogador + tamanhoDoJogador / 2 > xDoInimigo - tamanhoDoInimigo / 2
                && xDoJogador - tamanhoDoJogador / 2 < xDoInimigo + tamanhoDoInimigo / 2
                && yDoJogador + tamanhoDoJogador / 2 > yDoInimigo - tamanhoDoInimigo / 2
                && yDoJogador - tamanhoDoJogador / 2 < yDoInimigo + tamanhoDoInimigo / 2)
            {
                tamanhoDoJogador = tamanhoDoJogador + novoTamanhoDaBola;
                if (tamanhoDoJogador <= 10)
                {
                    tamanhoDoJogador = 10;
                }
            }


            if (xDoJogador - tamanhoDoJogador / 2 < -ClientSize.Width / 2
                || xDoJogador + tamanhoDoJogador / 2 > ClientSize.Width / 2)
            {
                xDoJogador = -xDoJogador;
            }
            if (yDoJogador - tamanhoDoJogador / 2 < -ClientSize.Height / 2
                || yDoJogador + tamanhoDoJogador / 2 > ClientSize.Height / 2)
            {
                yDoJogador = -yDoJogador;
            }


            if (Keyboard.GetState().IsKeyDown(Key.Up))
            {
                yDoJogador = yDoJogador + 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Down))
            {
                yDoJogador = yDoJogador - 5;
            }

            if (Keyboard.GetState().IsKeyDown(Key.Right))
            {
                xDoJogador = xDoJogador + 5;
            }
            if (Keyboard.GetState().IsKeyDown(Key.Left))
            {
                xDoJogador = xDoJogador - 5;
            }


        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Viewport(0, 0, ClientSize.Width, ClientSize.Height);

            Matrix4 projection = Matrix4.CreateOrthographic(ClientSize.Width, ClientSize.Height, 0.0f, 1.0f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref projection);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DesenharRetangulo(xDaBola, yDaBola, tamanhoDaBola, tamanhoDaBola, 0.0f, 1.0f, 0.0f);
            DesenharRetangulo(xDoJogador, yDoJogador, tamanhoDoJogador, tamanhoDoJogador, 0.0f, 0.0f, 1.0f);
            DesenharRetangulo(xDoInimigo, yDoInimigo, tamanhoDoInimigo, tamanhoDoInimigo, 1.0f, 0.0f, 0.0f);

            SwapBuffers();
        }

        void DesenharRetangulo(float x, float y, float largura, float altura, float r, float g, float b)
        {

            GL.Color3(r, g, b);

            GL.Begin(PrimitiveType.Quads);
            GL.Vertex2(-0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, -0.5f * altura + y);
            GL.Vertex2(0.5f * largura + x, 0.5f * altura + y);
            GL.Vertex2(-0.5f * largura + x, 0.5f * altura + y);
            GL.End();
        }

        static void Main()
        {
            new Program().Run();
        }
    }
}