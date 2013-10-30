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
				b.print_log ();
				if ((cross (b.r, field.p0.y, b.p.y)) || (cross (b.r, field.p1.y, b.p.y))) {
					b.angle = 360 - b.angle; 
				}

				if ((cross (b.r, hand1.p1.x, b.p.x)) && (b.p.y >= hand1.p1.y) && (b.p.y <= hand1.p0.y)
				    || (cross (b.r, hand2.p0.x, b.p.x)) && (b.p.y >= hand2.p1.y) && (b.p.y <= hand2.p0.y)) {
					b.angle = 180 - b.angle;
				}

				if ((cross (b.r, field.p0.x, b.p.x)) || (cross (b.r, field.p1.x, b.p.x))) {
					Console.WriteLine ("Game over!");
					break;
				}
			}
		}

		public static Boolean cross (float r, float c0, float c1) {
			return (r * r - Math.Pow ((c0 - c1), 2)) >= 0;
		}
	}

	interface IView {
		void Render ();
	}

	interface IModel {
		Point P {
			get;
			set;
		}
	}

	class Point {
		public float x,y;
	}

	class Ball : IView, IModel {
		public Point p0, p;
		public float angle, speed, r;

		public Point P {
			get {
				return p;
			}
			set {
				p = value;
			}
		}

		public Ball (float x, float y, float r) {
			p0 = new Point ();
			p = new Point ();

			p.x = x;
			p.y = y;
			this.r = r;
			this.set_base ();

			this.speed = 1;

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

		public void print_log () {
			Console.WriteLine ("X - " + this.p.x.ToString("R") + " Y - " + this.p.y.ToString("R") + "A - "+this.angle.ToString("R"));
		}
		public void Render (){
		}
	}

	class Rect {
		public Point p0, p1, p;
		public float a, b;

		public Point P {
			get {
				return p;
			}
			set {
				p = value;
			}
		}

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
		public void Render () {

		}
	}
}
