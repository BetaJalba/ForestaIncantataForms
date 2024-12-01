using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvventuraForestaIncantataVerifica
{
    public class CCasella
    {
        static int count = 0;
        static string path = @"../../caselle.jpg";

        int                                             id,
                                                        offset;
        string                                          nome;
        public event EventHandler<CasellaEventArgs>     OnEffetto;
        
        public string Nome 
        { 
            get { return nome; } 
        }

        public CCasella() 
        {
            id = count;
            offset = 0;
            count++;
            nome = "Casella Normale";
        }

        protected CCasella(string nome, int offset)
        {
            id = count;
            this.offset = offset;
            count++;
            this.nome = nome;
        }

        protected void EventoEffetto() 
        {
            CasellaEventArgs args = new CasellaEventArgs();
            args.Casella = id;
            OnEffetto?.Invoke(this, args);
        }

        public virtual int Effetto() 
        {
            return 0;
        }

        public Bitmap getImage() 
        {
            Bitmap image = new Bitmap(path);
            return image.Clone(new Rectangle(offset * 100, 0, 100, 100), image.PixelFormat);
        }
    }

    public class CCasellaMagica : CCasella 
    {
        public CCasellaMagica() : base("Casella Sorgente Magica", 1) 
        {

        }

        public override int Effetto() 
        {
            return 3;
        }
    }

    public abstract class CCasellaTrappola : CCasella 
    {
        public CCasellaTrappola(string nome, int offset) : base($"Casella Trappola: {nome}", offset) 
        {

        }
    }

    public class CCasellaRagnatela : CCasellaTrappola  
    {
        public CCasellaRagnatela() : base("RagnatelaGigante", 2) 
        {

        }

        public override int Effetto() // if sender is CCasellaRagnatela salta
        {
            EventoEffetto();
            return 0;
        }
    }

    public class CCasellaPalude : CCasellaTrappola 
    {
        public CCasellaPalude() : base("Palude Viscosa", 3) 
        {

        }

        public override int Effetto()
        {
            return -7;
        }
    }

    public class CCasellaAmico : CCasella 
    {
        public CCasellaAmico() : base("Casella Amico della Foresta", 4) 
        {

        }

        public override int Effetto()
        {
            return 5;
        }
    }

    public class CCasellaAlbero : CCasella
    {
        public CCasellaAlbero() : base("Casella Albero del Destino", 5) 
        {

        }

        public override int Effetto() // if sender is CCasellaAlbero ritira
        {
            EventoEffetto();
            return 0;
        }
    }

    public class CasellaEventArgs : EventArgs 
    {
        public int Casella;
    }
}
