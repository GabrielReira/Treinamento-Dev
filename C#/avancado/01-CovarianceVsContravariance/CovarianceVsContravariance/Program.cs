using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CovarianceVsContravariance
{
    class Program
    {
        static void Main(string[] args)
        {
            ManipuladorFTP<string> ftp = new ManipuladorFTP<string>();
            ftp.Armazenar("Item");
            Console.WriteLine(ftp.Recuperar(0));

            ManipuladorFTP<Nivel2> ftp2 = new ManipuladorFTP<Nivel2>();
            ftp2.Armazenar(new Nivel2());
            Console.WriteLine(ftp2.Recuperar(0));

            IArmazenador<Nivel3> armazenador3 = ftp2;
            armazenador3.Armazenar(new Nivel3());

            IRecuperador<Nivel1> recuperador1 = ftp2;
            Console.WriteLine(recuperador1.Recuperar(0));

            Console.ReadKey();
        }
    }
}
