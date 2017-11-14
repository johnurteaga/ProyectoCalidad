$("#seccionId").change(function () {
    //var criterio = $("#criterioAlumnoNotas").val();
    //var idTrime = $("#Trimestre").val();
    //var nivelId = $("#GradoId").val();
    //var gradoId = $("#SeccionId").val();
    var seccionId = $("#SeccionId").val();
    var cursoId = $("#SeccionId").val();

    $.post("/Notas/ListaNotas/?&seccionId=" +
            seccionId +
            "&cursoId=" +
            cursoId )
        .done(function (data) {
            console.log(data);
            //$("#search_Asistencia").html(data.info);
        });
});
