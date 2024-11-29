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

            fillNumbers(dataGridView1, 0, dataGridView1.RowCount - 1, 0, 0, dataGridView1.ColumnCount, dataGridView1.RowCount, 0, 0);
        } 

        private void fillNumbers (DataGridView dg, int x, int y, int minX, int minY, int maxX, int maxY, int sense, int count) 
        {
            if (y < 0 || x < 0 || x >= dg.ColumnCount || y >= dg.RowCount)
                return;

            if (sense == 0) 
            {
                for (int i = 0; i < maxX; i++, x++)  // primo 0, 9
                {
                    dg[i, y].Value = count.ToString();
                    count++;
                }
                x--;
                y--;
                for (int i = maxY - 2; i >= minY; i--, y--)
                {
                    dg[x, i].Value = count.ToString();
                    count++;
                }
                y++;

                if (count > 30) return;

                fillNumbers(dg, x--, y, maxX, maxY, minX--, minY--, 1, count);
            }
            else if (sense == 1) 
            {
                for (int i = minX - 1; i >= maxX; i--, x--) 
                {
                    dg[i, y].Value = count.ToString();
                    count++;
                }
                x++;
                y++;
                for (int i = y; i < minY - 1; i++, y++) 
                {
                    dg[x, i].Value = count.ToString();
                    count++;
                }

                fillNumbers(dg, x++, y, minX, minY, maxX--, maxY--, 0, count);
            }


            
        }
    }
}