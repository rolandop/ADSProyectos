using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADSConsultaPla.Services
{
    public interface IConsultaPlaService
    {
        string ConsultaSisprevService(string identificacion, string nombre, string app);
    }
}
