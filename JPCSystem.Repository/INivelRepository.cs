﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public interface INivelRepository
    {
        IEnumerable<Nivel> GetNiveles();

        Nivel GetNivel(Int32 id);

        void AddNivel(Nivel nivel);

        void UpdateNivel(Nivel nivel);

        void DeleteNivel(Int32 nivelId);
    }
}