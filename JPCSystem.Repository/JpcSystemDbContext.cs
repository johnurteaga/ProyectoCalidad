using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JPCSystem.Domain;

namespace JPCSystem.Repository
{
    public class JpcSystemDbContext : DbContext
    {
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<AnioAcademico> AnioAcademicos { get; set; }
        public DbSet<AperturarRegistroNotas> AperturarRegistroNotas { get; set; }
        public DbSet<Apoderado> Apoderados { get; set; }
        public DbSet<Asistencia> Asistencias { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Docente> Docentes { get; set; }
        public DbSet<DocenteCurso> DocentesCursos { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Matricula> Matriculas { get; set; }
        public DbSet<Nivel> Niveles { get; set; }
        public DbSet<Nota> Nota { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Parentesco> Parentescos { get; set; }
        public DbSet<Promedio> Promedios { get; set; }
        public DbSet<Seccion> Secciones { get; set; }
        public DbSet<Traslado> Traslados { get; set; }
        public DbSet<Trimestre> Trimestres { get; set; }
        public DbSet<Distrito> Distritos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Ubigeo> Ubigeos { get; set; }


        protected override void OnModelCreating(DbModelBuilder dbModelBuilder)
        {
            dbModelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            dbModelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            dbModelBuilder.Entity<DocenteCurso>().ToTable("DocenteCurso");
            //dbModelBuilder.Entity<NivelDocentes>().ToTable("Apoderado");
            //dbModelBuilder.Entity<CursoDocentes>().ToTable("Docente");


        }
    }
}
