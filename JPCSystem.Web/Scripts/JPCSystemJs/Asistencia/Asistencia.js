$(document).ready(function () {
        $("#NivelId").change(function () {
            $("#GradoId").empty();
            $("#GradoId").append('<option value="0">Grado</option>');
            $.ajax({
                type: 'POST',
                url: '/Asistencia/GetNivel',
                dataType: 'json',
                data: { nivelId: $("#NivelId").val() },
                success: function (data) {
                    $.each(data, function (i, data) {
                        $("#GradoId").append('<option value="'
                            + data.Id + '">'
                            + data.NombreGrado + '</option>');
                    });
                },
                error: function (ex) {
                    alert('Error al cargar los Grados.' + ex);
                }
            });
            return false;
        });

    $("#GradoId").change(function () {
        $("#SeccionId").empty();
        $("#SeccionId").append('<option value="0">Seccion</option>');
        $.ajax({
            type: 'POST',
            url: '/Asistencia/GetSeccion',
            dataType: 'json',
            data: { GradoId: $("#GradoId").val() },
            success: function (data) {
                $.each(data, function (i, data) {
                    $("#SeccionId").append('<option value="'
                        + data.Id + '">'
                        + data.NombreSeccion + '</option>');
                });
            },
            error: function (ex) {
                alert('Error al cargar las Secciones.' + ex);
            }
        });
        return false;
    });

    $("#SeccionId").change(function () {
        $.ajax({
            type: 'Get',
            url: '/Asistencia/GetAlumnos/?seccionId=' + $("#SeccionId").val(),
            dataType: 'html',
            success: function (data) {
                $("#asistencia").html(data);
            },
            error: function (ex) {
                alert('Error al cargar las Secciones.' + ex);
            }
        });
    });
});

$("#success-alert2").fadeTo(3500, 500).slideUp(1000, function () {
    $("#success-alert2").slideUp(1000);
});

$("#success-alert3").fadeTo(3500, 500).slideUp(1000, function () {
    $("#success-alert3").slideUp(1000);
});

$("#btnBuscaMatricula").click(function () {
    var criterio = $("#criterioAlumno").val();
    var idNivel = $("#NivelId").val();
    var gradoId = $("#GradoId").val();
    var seccionId = $("#SeccionId").val();

    $.post("/Asistencia/ListaAsistencias/?criterio=" +
            criterio +
            "&idNivel=" +
            idNivel +
            "&gradoId=" +
            gradoId +
            "&seccionId=" +
            seccionId)
        .done(function (data) {
            
            $("#falso").text(data.msj["falso"]);
            $("#asiste").text(data.msj["asiste"]);

            $("#search_Asistencia").html(data.info);
        });
}); 



$("#criterioAlumno").keyup(function () {
    var criterio = $("#criterioAlumno").val();
    var idNivel = $("#NivelId").val();
    var gradoId = $("#GradoId").val();
    var seccionId = $("#SeccionId").val();

    $.post("/Asistencia/ListaAsistencias/?criterio=" +
            criterio +
            "&idNivel=" +
            idNivel +
            "&gradoId=" +
            gradoId +
            "&seccionId=" +
            seccionId)
        .done(function (data) {

            $("#falso").text(data.msj["falso"]);
            $("#asiste").text(data.msj["asiste"]);

            $("#search_Asistencia").html(data.info);
        });
});


$("#btnAsistenciaGuarda").click(function () {
    var formAsistencia = $("#formAsistencia").serializeArray();
    $.post("/Asistencia/Index/",
            formAsistencia
        )
        .done(function (data) {
            console.log(data);
            var info = "";
            if (data === "ok") {
                window.location = "/Asistencia/ListaAsistencias";
            }
            if (data === "error" || data === "exiten") {
                window.location = "/Asistencia";
            } else {
                for (var i = 0; i < data.length; i++) {
                    info = info + "<li>" + data[i].nombre + "</li>";
                    console.log(info);
                }
                toastr.warning("El/Los Alumno(s) <ul> " + info + " </ul>\n Aun no tiene asistencia");
            }

            
        });
});

$("#btnReposteAsistencia").click(function () {

    $('#myModalReporte').find('.modal-title').html("Resporte de Asistencia");
    $('#myModalReporte').modal('show');

    $.get("/Asistencia/GetReposteAsistenciaAlumnos/")
        .done(function (data) {
            $('#myModalReporte').find('.modal-body').html(data);
        });
});

$("#btnEditAsistencia").click(function () {
    var asistencia = $("#formAistenciaEdit").serialize();
    $.post("/Asistencia/EditAsistencia/", asistencia)
        .done(function (data) {
            $('#myModalEdit').modal('hide');
            $("#search_Asistencia").html(data);
        });
});




