using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvventuraForestaIncantataVerifica
{
    

    public class CGioco
    {
        bool        turnoP1,
                    vincitore;
        string?     messaggioRiportato;
        int[]       posizioni;
        CGiocatore  p1,
                    p2;
        CDado       dado;

        public CGioco() 
        {
            turnoP1 = true;
            vincitore = false;
            messaggioRiportato = "Inizio Gioco!";
            posizioni = new int[2];
            Array.Fill(posizioni, 0); // entrambi i giocatori iniziano a 0
            CCreatoreMappa mappa = new CCreatoreMappa();
            p1 = new CGiocatore(mappa.Mappa);
            p2 = new CGiocatore(mappa.Mappa);
            p1.OnShouldThrow += (sender, e) => turnoP1 = !turnoP1;
            p2.OnShouldThrow += (sender, e) => turnoP1 = !turnoP1;
            p1.OnWin += (messaggio) => { vincitore = true; messaggioRiportato = messaggio; };
            p2.OnWin += (messaggio) => { vincitore = true; messaggioRiportato = messaggio; };
            dado = new CDado();
        }

        public bool Gioco() 
        {
            string? toAdd = string.Empty;

            if (turnoP1) 
            {
                toAdd = p1.Avanza(dado.Lancia());
                posizioni[0] = p1.GetPos();
                
            } else
            {
                toAdd = p2.Avanza(dado.Lancia());
                posizioni[1] = p2.GetPos();
            }

            if (toAdd != null)
                messaggioRiportato = toAdd;

            turnoP1 = !turnoP1;
            return !vincitore; // true indica che può continuare, metto vincitore a true in caso di vincita perché è più facile da gestire
        }

        public int[] GetPlayers() 
        {
            return posizioni;
        }

        public string GetRisultato() 
        {
            return messaggioRiportato;
        }
    }
}
