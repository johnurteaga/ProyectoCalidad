$("#success-alert").fadeTo(2500, 500).slideUp(1000, function () {
    $("#success-alert").slideUp(1000);
});

$("#btnAgregarMatricula").click(function () {
    $.get("/Matricula/Create")
    .done(function (data) {
        if (data === "ok") {
            toastr.info("El Periodo de Matriculas a terminado.");
        }
    });
});

//$('#IdAlumno').selectize({
//    create: false,
//    sortField: 'text',
//});

$("#nivel").change(function () {
    $("#grado").empty();
    $("#grado").html("<option value='0'>Grado</option>");
    $("#seccion").empty();
    $("#seccion").html("<option value='0'>Seccion</option>");
    var idNivel = $(this).val();
    $.post("/Matricula/GetNivel/?nivelId=" + idNivel)
        .done(function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#grado").append(
                    "<option value='" + data[i].Id + "'>" + data[i].NombreGrado + "</option>'"
                );


            }
        });
});

$("#grado").change(function () {
    var gradoId = $(this).val();
    $("#seccion").empty();
    $("#seccion").html("<option value='0'>Seccion</option>");
    $.post("/Matricula/GetSeccion/?gradoId=" + gradoId)
        .done(function (data) {
            for (var i = 0; i < data.length; i++) {
                $("#seccion").append(
                    "<option value='" + data[i].Id + "'>" + data[i].NombreSeccion + "</option>'"
                );


            }
        });

    $.post("/Matricula/Index/?gradoId=" + gradoId)
        .done(function (data) {
            console.log(data);
        });

});

$("#btnBuscaMatricula").click(function() {
    var criterioAlumno = $("#criterioAlumno").val();
    var grado = $("#grado").val();
    var seccion = $("#seccion").val();
    var nivel = $("#nivel").val();
    var anio = $("#anioEscolar").val();

    //var anioEscolar = $("#anioEscolar").val();
    $.post("/Matricula/Index/?criterio=" +
            criterioAlumno +
            "&nivelId=" +
            nivel +
            "&gradoId=" +
            grado +
            "&seccionId=" +
            seccion +
            "&anioId=" +
            anio)
        .done(function(data) {
            console.log(data);
            if (data === "ok") {
                toastr.success("No existen alumnos matriculdos en este Año escolar para esta Sección.");
            }

            $("#search_Matricula").html(data);
        });
});

$("#criterioAlumno").keyup(function () {
    var criterioAlumno = $("#criterioAlumno").val();
    var grado = $("#grado").val();
    var seccion = $("#seccion").val();
    var nivel = $("#nivel").val();
    var anio = $("#anioEscolar").val();

    //var anioEscolar = $("#anioEscolar").val();
    $.post("/Matricula/Index/?criterio=" +
            criterioAlumno +
            "&nivelId=" +
            nivel +
            "&gradoId=" +
            grado +
            "&seccionId=" +
            seccion +
            "&anioId=" +
            anio)
        .done(function (data) {
            console.log(data);
            if (data === "ok") {
                toastr.success("No existen alumnos matriculdos en este Año escolar para esta Sección.");
            }

            $("#search_Matricula").html(data);
        });
});