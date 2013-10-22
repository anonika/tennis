using System;

namespace tennis
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Ball b = new Ball (0, 0, 0.5f);
			Rect field = new Rect (0, 0, 10, 20);
			Rect hand1 = new Rect (-9, 0, 1, 0.5f);
			Rect hand2 = new Rect (9, 0, 1, 0.5f);

			while (true) {
				b.run ();
			}
		}
	}

	class Point {
		public float x,y;
	}

	class Ball {
		public Point p0, p;
		public float angle, speed, r;

		public Ball (float x, float y, float r) {
			p0 = new Point ();
			p = new Point ();

			p.x = x;
			p.y = y;
			this.r = r;
			this.set_base ();

			Random rand = new Random ();
			angle = rand.Next (-89, 89);
		}

		public void set_base () {
			p0.x = p.x;
			p0.y = p.y;
		}

		public void inverse_speed () {
			speed *= -1; 
		}

		public void run () {
			p.x += (float) Math.Cos (angle) * speed;
			float t = (float) Math.Tan (angle * Math.PI / 180);
			if (t >= 0)
				p.y = t * p.x + (p0.y - p0.x);
			else
				p.y = t * p.x + (p0.y + p0.x);
		}
	}

	class Rect {
		public Point p0, p1, p;
		public float a, b;

		public Rect(float x, float y, float a, float  b) {
			p0 = new Point ();
			p1 = new Point ();
			p = new Point ();

			this.a = a;
			this.b = b;

			p.x = x;
			p.y = y;

			p0.x = x - b / 2;
			p0.y = y + a / 2;

			p1.x = x + b / 2;
			p1.y = y - a / 2;
		}
	}
}
