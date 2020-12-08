using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace kursovaya
{
    public partial class ColorForm : Form
    {
        Bitmap bit1, bit2; //Bitmap'ы для выбора цвета
        Color top, bottom, common; //цвета для рисования градиента - регуляции Saturation
        Pen penWhite = new Pen(Brushes.White); //кисти для рисования 

        public ColorForm()
        {
            MainForm main = this.Owner as MainForm;
            InitializeComponent();
            bit1 = new Bitmap(pictureBox1.Image, pictureBox1.Width, pictureBox1.Height); //преобразование картинки в Bitmap
            pictureBox2.Image = new Bitmap(pictureBox2.Width, pictureBox2.Height);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Refresh();
            Point p = pictureBox1.PointToClient(Cursor.Position); //чтобы можно было получать цвет под курсором
            textBox1.Text = ColorTranslator.ToHtml(bit1.GetPixel(p.X, p.Y)).ToString(); //вывод HEX кода цвета

            //рисование белого и черного кругов под курсором
            Graphics circle = pictureBox1.CreateGraphics();
            circle.DrawEllipse(penWhite, p.X, p.Y, 10, 10);
            circle.DrawEllipse(penBlack, p.X - 1, p.Y - 1, 12, 12);

            common = ColorTranslator.FromHtml(textBox1.Text); //текущий цвет
            int R = common.R, G = common.G, B = common.B; //получение RGB из текущего цвета
            double H1, S1, H2, S2;
            int R1, G1, B1, R2, G2, B2;
            ColorLogic.RGBToHSV(R, G, B, out H1, out S1, out double V1);
            H1 = Math.Round(H1);
            S1 = Math.Round(S1 * 100);
            H2 = H1; S2 = S1;
            ColorLogic.HSVToRGB(H1, S1, 0, out R1, out G1, out B1);
            ColorLogic.HSVToRGB(H2, S2, 100, out R2, out G2, out B2);
            top = Color.FromArgb(R1, G1, B1); //получение цвета сверху
            bottom = Color.FromArgb(R2, G2, B2); //и снизу по изменению насыщенности
            panel1.BackColor = common; //вывод текущего цвета на панель
            pictureBox2.Invalidate(); //нарисовать градиент на pictureBox2
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //если на textBox'е нажата клавиша Enter, то текущему цвету присвоится цвет из textBox'а
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    common = ColorTranslator.FromHtml(textBox1.Text);
                }
                catch (Exception) //если введен некорректный цвет
                {
                    return;
                }
                panel1.BackColor = common; //присвоение цвета текущему цвету в панели1
                pictureBox2.Invalidate(); //перерисовка pictureBox2
            }
        }

        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            pictureBox2.Refresh(); //обновить изображение (убрать круги)                 
            bit2 = new Bitmap(pictureBox2.ClientSize.Width, pictureBox2.Height); //создание шаблона Bitmap на основе pictureBox2
            pictureBox2.DrawToBitmap(bit2, pictureBox2.ClientRectangle); //создание Bitmap на pictureBox2
            common = bit2.GetPixel(e.X, e.Y); //получение текущего цвета из pictureBox2
            panel1.BackColor = common; //отображение текущего цвета в панели1
            string hex = common.R.ToString("X2") + common.G.ToString("X2") + common.B.ToString("X2");
            textBox1.Text = "#" + hex; //отображение HEX кода в textBox'е

            //рисование линий под текущим цветом
            Graphics graphics = pictureBox2.CreateGraphics();
            graphics.DrawLine(penBlack, e.X - 50, e.Y - 1, e.X + 50, e.Y - 1);
            graphics.DrawLine(penWhite, e.X - 50, e.Y, e.X + 50, e.Y);
            graphics.DrawLine(penBlack, e.X - 50, e.Y + 2, e.X + 50, e.Y + 2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void colorToButton_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            int R = common.R, G = common.G, B = common.B;
            main.setParticleColorTo(R, G, B);
        }

        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            //создание градиента на основе цветов top и bottom
            LinearGradientMode direction = LinearGradientMode.Vertical; //задание направления градиента
            LinearGradientBrush brush = new LinearGradientBrush(pictureBox2.ClientRectangle, top, bottom, direction); //создание градиентной кисти
            e.Graphics.FillRectangle(brush, pictureBox2.ClientRectangle); //само рисование
        }

        Pen penBlack = new Pen(Brushes.Black); //текущей позиции курсора

        private void button1_Click(object sender, EventArgs e)
        {
            MainForm main = this.Owner as MainForm;
            int R = common.R, G = common.G, B = common.B;
            main.setParticleColorFrom(R, G, B);
        }
    }
}
