using AvventuraForestaIncantataVerifica;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ForestaIncantataForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            dataGridView1.ColumnCount = 10;
            dataGridView1.RowCount = 5;
            dataGridView1.ScrollBars = ScrollBars.None;
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.SelectionChanged += (sender, e) => (sender as DataGridView).ClearSelection();

            foreach (DataGridViewRow row in dataGridView1.Rows)
                row.Height = 100;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
                column.Width = 100;

            gioco = new CGioco();
            corrispondenze = new int[50][];
            playerIcons = new List<PictureBox>();

            fillNumbers(dataGridView1, 0, 0, dataGridView1.ColumnCount, dataGridView1.RowCount, 0);

            dataGridView1[corrispondenze[0][0], corrispondenze[0][1]].Style.BackColor = Color.GreenYellow; // inzio
            dataGridView1[corrispondenze[0][0], corrispondenze[0][1]].Value = "INIZIO";
            dataGridView1[corrispondenze[49][0], corrispondenze[49][1]].Style.BackColor = Color.GreenYellow; // fine
            dataGridView1[corrispondenze[49][0], corrispondenze[49][1]].Value = "FINE";

            FillCaselleImages();
            drawPlayers();
        }

        static string playerPath = @"../../player_sprites.png";

        CGioco gioco;
        int[][] corrispondenze; //potevo tranquillamente usare una lista
        List<PictureBox> playerIcons;

        private void drawPlayers() 
        {
            disposeOfPlayers();
            int[] pos = gioco.GetPlayers();

            if (pos[0] == pos[1]) 
            {
                Bitmap bmp = new Bitmap(playerPath);
                PictureBox pic = new PictureBox();
                pic.Height = 100;
                pic.Width = 100;
                pic.BackColor = Color.Transparent;
                pic.Image = bmp.Clone(new Rectangle(200, 0, 100, 100), bmp.PixelFormat);
                pic.Parent = dataGridView1;
                pic.Location = new Point(100 * corrispondenze[pos[0]][0], 100 * corrispondenze[pos[0]][1]);
                playerIcons.Add(pic);
                pic.BringToFront();
            } else 
            {
                Bitmap bmp = new Bitmap(playerPath);
                PictureBox pic = new PictureBox();
                pic.Height = 100;
                pic.Width = 100;
                pic.BackColor = Color.Transparent;
                pic.Image = bmp.Clone(new Rectangle(0, 0, 100, 100), bmp.PixelFormat);
                pic.Parent = dataGridView1;
                pic.Location = new Point(100 * corrispondenze[pos[0]][0], 100 * corrispondenze[pos[0]][1]);
                playerIcons.Add(pic);
                pic.BringToFront();

                PictureBox pic2 = new PictureBox();
                pic2.Height = 100;
                pic2.Width = 100;
                pic2.BackColor = Color.Transparent;
                pic2.Image = bmp.Clone(new Rectangle(100, 0, 100, 100), bmp.PixelFormat);
                pic2.Parent = dataGridView1;
                pic2.Location = new Point(100 * corrispondenze[pos[1]][0], 100 * corrispondenze[pos[1]][1]);
                playerIcons.Add(pic2);
                pic2.BringToFront();
            }
        }

        private void FillCaselleImages() // consiglio di non guardare
        {
            CCreatoreMappa mappa = new CCreatoreMappa();
            for (int i = 1; i < 49; i++) 
            {
                PictureBox pic = new PictureBox();
                pic.Height = 100;
                pic.Width = 100;
                pic.BackColor = Color.Transparent;
                pic.Image = mappa.Mappa[i].getImage();
                pic.Parent = dataGridView1;
                pic.Location = new Point(100 * corrispondenze[i][0], 100 * corrispondenze[i][1]);
            }
        }

        private void disposeOfPlayers() 
        {
            foreach (PictureBox player in playerIcons)
                player.Dispose();
        }

        // riempie i lati di una matrice sempre più piccola fino a quando la completa
        private void fillNumbers(DataGridView dg, int xStart, int yStart, int width, int height, int count) 
        {
            if (width - xStart < 1 || height - yStart < 1)
                return;

            if (width - xStart > 0)
                for (int i = xStart; i < width; i++)
                {
                    dg[i, height - 1].Value = "→";
                    corrispondenze[count] = [i, height - 1];
                    count++;
                }

            if (height - yStart > 1)
            {
                for (int i = height - 2; i >= yStart; i--)
                {
                    dg[width - 1, i].Value = "↑";
                    corrispondenze[count] = [width - 1, i];
                    count++;
                }

                if (width - xStart > 1)
                {
                    for (int i = width - 2; i >= xStart; i--)
                    {
                        dg[i, yStart].Value = "←";
                        corrispondenze[count] = [i, yStart];
                        count++;
                    }

                    if (height - yStart > 2)
                        for (int i = yStart + 1; i < height - 1; i++)
                        {
                            dg[xStart, i].Value = "↓";
                            corrispondenze[count] = [xStart, i];
                            count++;
                        }
                }
            }

            fillNumbers(dg, xStart + 1, yStart + 1, width - 1, height - 1, count);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text.EndsWith('1') || (sender as Button).Text.EndsWith('A'))
                (sender as Button).Text = "TIRA G2";
            else
                (sender as Button).Text = "TIRA G1";

            bool continua = gioco.Gioco();

            drawPlayers();
            MessageBox.Show(gioco.GetRisultato());

            if (!continua) 
            {
                this.Close();
            }
        }
    }
}
