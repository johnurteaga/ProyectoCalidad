﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface IGradoRepository
    {
        IEnumerable<Grado> GetGrados();

        Grado GetGrado(Int32 id);

        void AddGrado(Grado grado);

        void UpdateGrado(Grado grado);

        void DeleteGrado(Int32 grado);
    }
}
