using System;
using System.Drawing;
using System.Windows.Forms;

public class Game
{
    private Player _player = new Player(); 
    private Ball[] _balls = new Ball[3];
    public int _points = 0;
    public int _startTime = DateTime.Now.TimeOfDay.Seconds;
    public float _nameBoxWidth = 0.3f * Window.width;

    public void Setup()
    {
        Random rnd = new Random();
        for (int i = 0; i < _balls.Length; i++)
        {
            _balls[i] = new Ball(rnd);
        }

    }

    public void Update(float dt)
    {
        _player.Update(dt);
        for (int i = 0; i < _balls.Length; i++)
        {
            // drop a ball every 2 seconds
            if ((DateTime.Now.TimeOfDay.Seconds  - _startTime > 
                i * 2 && !_balls[i]._done)) 
            {
               _balls[i]._show = true;
            }
            if (!_balls[i]._done) 
            {
                // ball is caught!
                if (_balls[i]._yPosition >= 0.75f * Window.height && 
                    (_player._xPosition > _balls[i]._xPosition - (float)0.025 * 
                    Window.width && _player._xPosition < _balls[i]._xPosition + 
                    (float)0.075 * Window.width))
                {
                    _points += 1;
                    _balls[i].BallCaught();
                }
                // ball hits ground!
                else if (_balls[i]._yPosition >= 0.80f * Window.height)
                {
                    _balls[i].BallSplat();
                }
                else 
                {
                    _balls[i].Update(dt);
                }
            }
        }
    }

    public void Draw(Graphics g)
    {
        Color ground = ColorTranslator.FromHtml("#142F98");
        Brush groundBrush = new SolidBrush(ground);
        SolidBrush groundFontBrush = new SolidBrush(Color.Pink);
        g.FillRectangle(groundBrush, 0, 0.9f * Window.height, 
            Window.width, Window.height);

        Font nameFont = new Font("Arial", 10);
        StringFormat nameFormat = new StringFormat();
        nameFormat.LineAlignment = StringAlignment.Near;
        nameFormat.Alignment = StringAlignment.Near;
        g.FillRectangle(new SolidBrush(Color.Black), 0, 0.9f * Window.height,
            _nameBoxWidth, Window.height);
        g.DrawString("Bridge Schaad '25\r\nCandy Catcher", nameFont, 
            groundFontBrush, 0, 0.9f * Window.height, nameFormat);

        Font pointFont = new Font("Arial", 20);

        StringFormat pointFormat = new StringFormat();
        pointFormat.LineAlignment = StringAlignment.Near;
        pointFormat.Alignment = StringAlignment.Far;
        g.DrawString("Points: " + _points, pointFont, groundFontBrush,
            0.9f * Window.width, 0.9f * Window.height,
            pointFormat);

        _player.Draw(g);
        foreach (Ball ball in _balls)
        {
            if (ball._show) 
            {
                ball.Draw(g);
            }
        }
        // if last ball is done, Game over!
        if (_balls[_balls.Length - 1]._done) 
        {
            SolidBrush overFontBrush = new SolidBrush(Color.Purple);
            StringFormat overFormat = new StringFormat();
            overFormat.LineAlignment = StringAlignment.Center;
            overFormat.Alignment = StringAlignment.Center;
            g.DrawString("GAME OVER!", pointFont, overFontBrush,
            0.5f * Window.width, 0.5f * Window.height,
            overFormat);
        }
    }

    public void MouseClick(MouseEventArgs mouse)
    {
        if (mouse.Button == MouseButtons.Left)
        {
            System.Console.WriteLine(mouse.Location.X + ", " + mouse.Location.Y);
        }
    }

    public void KeyDown(KeyEventArgs key)
    {
        if (key.KeyCode == Keys.D || key.KeyCode == Keys.Right)
        {
            _player.MoveRight();
        }
        else if (key.KeyCode== Keys.S || key.KeyCode == Keys.Down)
        {
        }
        else if (key.KeyCode == Keys.A || key.KeyCode == Keys.Left)
        {
            _player.MoveLeft();
        }
        else if (key.KeyCode == Keys.W || key.KeyCode == Keys.Up)
        {
        }
        else if (key.KeyCode == Keys.Oemplus)
        {
            _nameBoxWidth *= 1.1f;
        }
    }
}
