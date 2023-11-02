using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace oyun
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Panel parca;
        Panel elma = new Panel();
        List<Panel> yilan = new List<Panel>();

        string yon = "right";

        
        private void label4_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
            oyunreset();

            parca = new Panel();
            parca.Location = new Point(360, 180);
            parca.Size = new Size(20, 20);
            parca.BackColor = Color.Black;
            yilan.Add(parca);
            panel1.Controls.Add(yilan[0]);
            label3.Visible = false;
            timer1.Interval = 100;



            timer1.Start();
            elmalar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int locX = yilan[0].Location.X;
            int locY = yilan[0].Location.Y;

            elmayedi();
            hareket();
            kaza();

                

            if (yon == "right")
            {
                if (locX < 680)
                {
                    locX += 20;
                }
                else
                {
                    locX = 0;
                }
            }
            if (yon == "left")
            {
                if(locX > 0)
                {
                    locX -= 20;
                }
                else
                {
                    locX = 680;
                }
            }
            if (yon == "up")
            {
                if(locY > 0)
                {
                    locY -= 20;
                }
                else
                {
                    locY = 340;
                }
            }
            if(yon == "down")
            {
                if(locY < 340)
                {
                    locY += 20;
                }
                else
                {
                    locY = 0;
                }
            }
            yilan[0].Location = new Point(locX, locY);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right && yon != "left")
                yon = "right";
            if (e.KeyCode == Keys.Left && yon != "right")
                yon = "left";
            if (e.KeyCode == Keys.Up && yon != "down")
                yon = "up";
            if (e.KeyCode == Keys.Down && yon != "up")
                yon = "down";
            
        }

        void elmalar()
        {
            Random rnd = new Random();
            int elmax, elmay;
            elmax = rnd.Next(700);
            elmay = rnd.Next(360);

            elmax -= elmax % 20;
            elmay -= elmay % 20;

            elma.Size = new Size(20,20);
            elma.BackColor = Color.Yellow;
            elma.Location = new Point(elmax,elmay); 
            panel1.Controls.Add(elma);
        }

        void elmayedi()
        {
            int puan = int.Parse(label2.Text);
            if (yilan[0].Location == elma.Location)
            {
                panel1.Controls.Remove(elma);
                puan += 1;
                label2.Text = puan.ToString();
                elmalar(); // yeni elma rasgele bir yere atar   
                parcaekle();  // yılanın kuyruğuna parça ekler
                timer1.Interval -= 2;
            }
        }


        void parcaekle() /// yılanın arkasına parçanın eklenmesini sağlar
        {
            Panel ekparca = new Panel();
            ekparca.Size = new Size(20, 20);
            ekparca.BackColor = Color.Gray;
            yilan.Add(ekparca);
            panel1.Controls.Add(ekparca);
        } 
        
        void hareket() // yılanın arkasına eklenen kuyruğun hareketini sağlar
        {
            for (int i = yilan.Count - 1; i > 0; i--)
            {
                yilan[i].Location = yilan[i - 1].Location;
            }
        }

        void kaza()   //yilanın başı ile herhangi bir parçasının çarpışması sonucu oyunu durdurur
        {
            for (int i = 2; i < yilan.Count; i++)
            {
                if (yilan[0].Location == yilan[i].Location)
                {
                    timer1.Stop(); // yılanı durdurur
                    label3.Visible = true;
                    label3.Text = "GAME OVER";
                }
            }
        }

        void oyunreset() // oyun kaybettiğin zaman tekrar başlat butonuna tıkladığında paneldeki yılanın tekrar 1 den başlamasını sağlar ve muzuda yeni bir yere koyar
        {
            panel1.Controls.Clear();
            yilan.Clear();
            
            
        }

       
    }
}
