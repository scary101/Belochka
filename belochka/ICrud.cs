using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belochka
{
    internal interface ICrud
    {
        void Create();
        int Read(int poz);
        void Update(int poz, int poz1);
        void Delete(int poz);

    }
}
