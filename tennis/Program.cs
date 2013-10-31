using System;

namespace tennis
{
	// В общем: нужно больше следить за тем, как твой код будет выполняться 
	// на машине. Я не говорю о том, что надо жертвовать удобством во имя
	// производительности, но очевидно не нужные вызовы стоит сокращать. 
	// Так же не стоит принебрегать эффективными методиками: есть ли 
	// возможность использовать векторую математику вместо тригонометрии, 
	// то нужно использовать. То же самое если есть возможность использовать
	// целочисленные вычисления вместо вычилений с плавающей точкой.
	class MainClass
	{
		public static void Main (string[] args)
		{
			Ball b = new Ball (0, 0, 0.5f);
			Rect field = new Rect (0, 0, 10, 20);
			Rect hand1 = new Rect (-9, 0, 1, 0.5f);
			Rect hand2 = new Rect (9, 0, 1, 0.5f);

			Controller c = new Controller ();
			//model.
			
			// Часть логики обработки состояния у тебя находиться 
			// непосредственно во входной точке приложения, а другая
			// часть находится в классе мячика. Это может быть 
			// оправдано если мы закладываемся на вариативность 
			// поведения объектов. На пример переопледеляя метод run()
		    // мы можем заставить шарик двигаться как-то иначе. Но почему 
			// тогда логика столконвений задается не в классе Rect или
			// его наследние?
			while (c.move (b, hand1, hand2, field)) {
				//b.print_log ();
				// определение нажатия на кнопку
			}
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

	interface IModelBall : IModel{
		float Angle {
			get;
			set;
		}
	}

	class Controller {
		IView view;
		public void move (Rect o, int v) {
			Point p = new Point ();
			p.y += v;

			IModel model;
			model = o;
			model.P = p;

			view = o;
			view.Render ();
		}

		public Boolean move (Ball o, Rect h1, Rect h2, Rect f) {
			IModelBall model;
			model = o;

			Point p = new Point ();
			p.x += (float) Math.Cos (model.Angle) * o.speed;
			float t = (float) Math.Tan (model.Angle * Math.PI / 180);
			if (t >= 0)
				p.y = t * p.x + (o.p0.y - o.p0.x);
			else
				p.y = t * p.x + (o.p0.y + o.p0.x);

			model.P = p;

			if (o.cross (h1, h2)) {
				model.Angle = 180 - model.Angle;
				return true;
			} else if (o.cross (f) == 1) {
				model.Angle = 360 - model.Angle;
				return true;
			} else if (o.cross (f) == -1) {
				Console.WriteLine ("Game over!"); // не пришей *** рукав!
				return false;
			}
			return true;
		}
	}

	class Point {
		public float x,y;
	}

	class Ball : IView, IModelBall {
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

		public float Angle {
			get {
				return angle;
			}
			set {
				angle = value;
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

		public void print_log () {
			Console.WriteLine ("X - " + this.p.x.ToString("R") + " Y - " + this.p.y.ToString("R") + "A - "+this.angle.ToString("R"));
		}

		public int cross (Rect f) {
			if ((cross (r, f.p0.y, p.y)) || (cross (r, f.p1.y, p.y))) 
				return 1;
			else if ((cross (r, f.p0.x, p.x)) || (cross (r, f.p1.x, p.x)))
				return -1;
			else
				return 0;
		} 

		public Boolean cross (Rect h1, Rect h2) {
			return ((cross (r, h1.p1.x, p.x)) && (p.y >= h1.p1.y) && (p.y <= h1.p0.y)
				|| (cross (r, h2.p0.x, p.x)) && (p.y >= h2.p1.y) && (p.y <= h2.p0.y));
		}

		public Boolean cross (float r, float c0, float c1) {
			return (r * r - Math.Pow ((c0 - c1), 2)) >= 0;
		}

		public void Render (){
			// закрашиваем по цвету фона, рисуем в новом месте
		}

	}

	class Rect : IView, IModel{
		public Point p0, p1, p;
		public float a, b;

		public Point P {
			get {
				return p;
			}
			set {
				p = value;
				reset_coord ();
			}
		}

		public Rect(float x, float y, float a, float  b) {
			p0 = new Point ();
			p1 = new Point ();
			p = new Point ();

			this.a = a;
			this.b = b;

			Point t = new Point ();
			t.x = x;
			t.y = y;
			P = t;
		}

		private void reset_coord () {
			p0.x = p.x - b / 2;
			p0.y = p.y + a / 2;

			p1.x = p.x + b / 2;
			p1.y = p.y - a / 2;
		}

		public void Render () {
			// закрашиваем по цвету фона, рисуем в новом месте
		}
	}
}
