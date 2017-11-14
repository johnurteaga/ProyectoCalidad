namespace JPCSystem.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alumno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroDocumento = c.Int(nullable: false),
                        ApPaterno = c.String(),
                        ApMaterno = c.String(),
                        Nombre = c.String(),
                        IdUbigeo = c.String(maxLength: 255),
                        Direccion = c.String(),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Correo = c.String(),
                        Genero = c.String(),
                        Telefono = c.Int(nullable: false),
                        DocumentoId = c.Int(nullable: false),
                        Estado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documento", t => t.DocumentoId)
                .ForeignKey("dbo.Ubigeo", t => t.IdUbigeo)
                .Index(t => t.IdUbigeo)
                .Index(t => t.DocumentoId);
            
            CreateTable(
                "dbo.Documento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomDocumento = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ubigeo",
                c => new
                    {
                        IdUbigeo = c.String(nullable: false, maxLength: 255),
                        Departamento = c.String(maxLength: 255),
                        Provincia = c.String(maxLength: 255),
                        Distrito = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.IdUbigeo);
            
            CreateTable(
                "dbo.AnioAcademico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Anio = c.String(),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaFin = c.DateTime(nullable: false),
                        FechaInicioMatricula = c.DateTime(nullable: false),
                        FechaFinMatricula = c.DateTime(nullable: false),
                        FechaMatriculaExtemporanea = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AperturarRegistroNotas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaInicioPrimTrim = c.DateTime(nullable: false),
                        FechaFinPrimTrim = c.DateTime(nullable: false),
                        AñoAcademicoId = c.Int(nullable: false),
                        estado = c.Boolean(nullable: false),
                        TrimestreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AnioAcademico", t => t.AñoAcademicoId)
                .ForeignKey("dbo.Trimestre", t => t.TrimestreId)
                .Index(t => t.AñoAcademicoId)
                .Index(t => t.TrimestreId);
            
            CreateTable(
                "dbo.Trimestre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Apoderado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroDocumento = c.Int(nullable: false),
                        ApPaterno = c.String(nullable: false),
                        ApMaterno = c.String(nullable: false),
                        Nombres = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Correo = c.String(),
                        Genero = c.String(nullable: false),
                        Telefono = c.Int(nullable: false),
                        LugarTrabajo = c.String(nullable: false),
                        DocumentoId = c.Int(nullable: false),
                        IdUbigeo = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documento", t => t.DocumentoId)
                .ForeignKey("dbo.Ubigeo", t => t.IdUbigeo)
                .Index(t => t.DocumentoId)
                .Index(t => t.IdUbigeo);
            
            CreateTable(
                "dbo.Asistencia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Estado = c.Boolean(nullable: false),
                        FechaAsistencia = c.DateTime(nullable: false),
                        SeccionId = c.Int(nullable: false),
                        AlumnoId = c.Int(nullable: false),
                        AnioAcademicoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alumno", t => t.AlumnoId)
                .ForeignKey("dbo.AnioAcademico", t => t.AnioAcademicoId)
                .ForeignKey("dbo.Seccion", t => t.SeccionId)
                .Index(t => t.SeccionId)
                .Index(t => t.AlumnoId)
                .Index(t => t.AnioAcademicoId);
            
            CreateTable(
                "dbo.Seccion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreSeccion = c.String(),
                        GradoId = c.Int(nullable: false),
                        Capasida = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grado", t => t.GradoId)
                .Index(t => t.GradoId);
            
            CreateTable(
                "dbo.Grado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreGrado = c.String(nullable: false),
                        NivelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nivel", t => t.NivelId)
                .Index(t => t.NivelId);
            
            CreateTable(
                "dbo.Nivel",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreNivel = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreCurso = c.String(nullable: false),
                        GradoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Grado", t => t.GradoId)
                .Index(t => t.GradoId);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreDepartamento = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Distrito",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreDistrito = c.String(nullable: false),
                        ProvinciaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Provincia", t => t.ProvinciaId)
                .Index(t => t.ProvinciaId);
            
            CreateTable(
                "dbo.Provincia",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreProvincia = c.String(nullable: false),
                        DepartamentoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Departamento", t => t.DepartamentoId)
                .Index(t => t.DepartamentoId);
            
            CreateTable(
                "dbo.Docente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NroDocumento = c.Int(nullable: false),
                        ApPaterno = c.String(nullable: false),
                        ApMaterno = c.String(nullable: false),
                        Nombres = c.String(nullable: false),
                        Direccion = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Genero = c.String(nullable: false),
                        Telefono = c.Int(nullable: false),
                        AniosExperiencia = c.Int(nullable: false),
                        EstadoCivil = c.String(nullable: false),
                        DocumentoId = c.Int(nullable: false),
                        IdUbigeo = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Documento", t => t.DocumentoId)
                .ForeignKey("dbo.Ubigeo", t => t.IdUbigeo)
                .Index(t => t.DocumentoId)
                .Index(t => t.IdUbigeo);
            
            CreateTable(
                "dbo.DocenteCurso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocenteId = c.Int(nullable: false),
                        CursoId = c.Int(nullable: false),
                        SeccionId = c.Int(nullable: false),
                        HoraInicio = c.String(),
                        HoraFin = c.String(),
                        Dia = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.CursoId)
                .ForeignKey("dbo.Docente", t => t.DocenteId)
                .ForeignKey("dbo.Seccion", t => t.SeccionId)
                .Index(t => t.DocenteId)
                .Index(t => t.CursoId)
                .Index(t => t.SeccionId);
            
            CreateTable(
                "dbo.Matricula",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaMatricula = c.DateTime(nullable: false),
                        AnioAcademicoId = c.Int(nullable: false),
                        AlumnoId = c.Int(nullable: false),
                        ApoderadoId = c.Int(nullable: false),
                        SeccionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alumno", t => t.AlumnoId)
                .ForeignKey("dbo.AnioAcademico", t => t.AnioAcademicoId)
                .ForeignKey("dbo.Apoderado", t => t.ApoderadoId)
                .ForeignKey("dbo.Seccion", t => t.SeccionId)
                .Index(t => t.AnioAcademicoId)
                .Index(t => t.AlumnoId)
                .Index(t => t.ApoderadoId)
                .Index(t => t.SeccionId);
            
            CreateTable(
                "dbo.Nota",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cualitativa = c.String(nullable: false),
                        Vigecimal = c.Int(nullable: false),
                        Nota1 = c.Int(nullable: false),
                        Nota2 = c.Int(nullable: false),
                        Nota3 = c.Int(nullable: false),
                        Nota4 = c.Int(nullable: false),
                        TrimestreId = c.Int(nullable: false),
                        CursoId = c.Int(nullable: false),
                        AlumnoId = c.Int(nullable: false),
                        SeccionId = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alumno", t => t.AlumnoId)
                .ForeignKey("dbo.Curso", t => t.CursoId)
                .ForeignKey("dbo.Seccion", t => t.SeccionId)
                .ForeignKey("dbo.Trimestre", t => t.TrimestreId)
                .Index(t => t.TrimestreId)
                .Index(t => t.CursoId)
                .Index(t => t.AlumnoId)
                .Index(t => t.SeccionId);
            
            CreateTable(
                "dbo.Pago",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FechaPago = c.DateTime(nullable: false),
                        Monto = c.Int(nullable: false),
                        Descripccion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Parentesco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoParentesco = c.String(),
                        AlumnoId = c.Int(),
                        ApoderadoId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alumno", t => t.AlumnoId)
                .ForeignKey("dbo.Apoderado", t => t.ApoderadoId)
                .Index(t => t.AlumnoId)
                .Index(t => t.ApoderadoId);
            
            CreateTable(
                "dbo.Promedio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PrimerTrimPromedio = c.Int(nullable: false),
                        PromedioFinal = c.Int(nullable: false),
                        NotaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Nota", t => t.NotaId)
                .Index(t => t.NotaId);
            
            CreateTable(
                "dbo.Traslado",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AlumnoId = c.Int(nullable: false),
                        FechaTraslado = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Alumno", t => t.AlumnoId)
                .Index(t => t.AlumnoId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DocenteId = c.Int(),
                        ApoderadoId = c.Int(),
                        AlumnoId = c.Int(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Traslado", "AlumnoId", "dbo.Alumno");
            DropForeignKey("dbo.Promedio", "NotaId", "dbo.Nota");
            DropForeignKey("dbo.Parentesco", "ApoderadoId", "dbo.Apoderado");
            DropForeignKey("dbo.Parentesco", "AlumnoId", "dbo.Alumno");
            DropForeignKey("dbo.Nota", "TrimestreId", "dbo.Trimestre");
            DropForeignKey("dbo.Nota", "SeccionId", "dbo.Seccion");
            DropForeignKey("dbo.Nota", "CursoId", "dbo.Curso");
            DropForeignKey("dbo.Nota", "AlumnoId", "dbo.Alumno");
            DropForeignKey("dbo.Matricula", "SeccionId", "dbo.Seccion");
            DropForeignKey("dbo.Matricula", "ApoderadoId", "dbo.Apoderado");
            DropForeignKey("dbo.Matricula", "AnioAcademicoId", "dbo.AnioAcademico");
            DropForeignKey("dbo.Matricula", "AlumnoId", "dbo.Alumno");
            DropForeignKey("dbo.DocenteCurso", "SeccionId", "dbo.Seccion");
            DropForeignKey("dbo.DocenteCurso", "DocenteId", "dbo.Docente");
            DropForeignKey("dbo.DocenteCurso", "CursoId", "dbo.Curso");
            DropForeignKey("dbo.Docente", "IdUbigeo", "dbo.Ubigeo");
            DropForeignKey("dbo.Docente", "DocumentoId", "dbo.Documento");
            DropForeignKey("dbo.Distrito", "ProvinciaId", "dbo.Provincia");
            DropForeignKey("dbo.Provincia", "DepartamentoId", "dbo.Departamento");
            DropForeignKey("dbo.Curso", "GradoId", "dbo.Grado");
            DropForeignKey("dbo.Asistencia", "SeccionId", "dbo.Seccion");
            DropForeignKey("dbo.Seccion", "GradoId", "dbo.Grado");
            DropForeignKey("dbo.Grado", "NivelId", "dbo.Nivel");
            DropForeignKey("dbo.Asistencia", "AnioAcademicoId", "dbo.AnioAcademico");
            DropForeignKey("dbo.Asistencia", "AlumnoId", "dbo.Alumno");
            DropForeignKey("dbo.Apoderado", "IdUbigeo", "dbo.Ubigeo");
            DropForeignKey("dbo.Apoderado", "DocumentoId", "dbo.Documento");
            DropForeignKey("dbo.AperturarRegistroNotas", "TrimestreId", "dbo.Trimestre");
            DropForeignKey("dbo.AperturarRegistroNotas", "AñoAcademicoId", "dbo.AnioAcademico");
            DropForeignKey("dbo.Alumno", "IdUbigeo", "dbo.Ubigeo");
            DropForeignKey("dbo.Alumno", "DocumentoId", "dbo.Documento");
            DropIndex("dbo.Traslado", new[] { "AlumnoId" });
            DropIndex("dbo.Promedio", new[] { "NotaId" });
            DropIndex("dbo.Parentesco", new[] { "ApoderadoId" });
            DropIndex("dbo.Parentesco", new[] { "AlumnoId" });
            DropIndex("dbo.Nota", new[] { "SeccionId" });
            DropIndex("dbo.Nota", new[] { "AlumnoId" });
            DropIndex("dbo.Nota", new[] { "CursoId" });
            DropIndex("dbo.Nota", new[] { "TrimestreId" });
            DropIndex("dbo.Matricula", new[] { "SeccionId" });
            DropIndex("dbo.Matricula", new[] { "ApoderadoId" });
            DropIndex("dbo.Matricula", new[] { "AlumnoId" });
            DropIndex("dbo.Matricula", new[] { "AnioAcademicoId" });
            DropIndex("dbo.DocenteCurso", new[] { "SeccionId" });
            DropIndex("dbo.DocenteCurso", new[] { "CursoId" });
            DropIndex("dbo.DocenteCurso", new[] { "DocenteId" });
            DropIndex("dbo.Docente", new[] { "IdUbigeo" });
            DropIndex("dbo.Docente", new[] { "DocumentoId" });
            DropIndex("dbo.Provincia", new[] { "DepartamentoId" });
            DropIndex("dbo.Distrito", new[] { "ProvinciaId" });
            DropIndex("dbo.Curso", new[] { "GradoId" });
            DropIndex("dbo.Grado", new[] { "NivelId" });
            DropIndex("dbo.Seccion", new[] { "GradoId" });
            DropIndex("dbo.Asistencia", new[] { "AnioAcademicoId" });
            DropIndex("dbo.Asistencia", new[] { "AlumnoId" });
            DropIndex("dbo.Asistencia", new[] { "SeccionId" });
            DropIndex("dbo.Apoderado", new[] { "IdUbigeo" });
            DropIndex("dbo.Apoderado", new[] { "DocumentoId" });
            DropIndex("dbo.AperturarRegistroNotas", new[] { "TrimestreId" });
            DropIndex("dbo.AperturarRegistroNotas", new[] { "AñoAcademicoId" });
            DropIndex("dbo.Alumno", new[] { "DocumentoId" });
            DropIndex("dbo.Alumno", new[] { "IdUbigeo" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Traslado");
            DropTable("dbo.Promedio");
            DropTable("dbo.Parentesco");
            DropTable("dbo.Pago");
            DropTable("dbo.Nota");
            DropTable("dbo.Matricula");
            DropTable("dbo.DocenteCurso");
            DropTable("dbo.Docente");
            DropTable("dbo.Provincia");
            DropTable("dbo.Distrito");
            DropTable("dbo.Departamento");
            DropTable("dbo.Curso");
            DropTable("dbo.Nivel");
            DropTable("dbo.Grado");
            DropTable("dbo.Seccion");
            DropTable("dbo.Asistencia");
            DropTable("dbo.Apoderado");
            DropTable("dbo.Trimestre");
            DropTable("dbo.AperturarRegistroNotas");
            DropTable("dbo.AnioAcademico");
            DropTable("dbo.Ubigeo");
            DropTable("dbo.Documento");
            DropTable("dbo.Alumno");
        }
    }
}
