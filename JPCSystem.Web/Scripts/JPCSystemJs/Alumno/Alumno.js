$("#btnNuevo").click(function (eve) {
    //$("#myModalAlumno").find('.modal-body').html("");
    $("#myModalAlumno").find('.modal-title').html("Agregar Alumno");
    $("#myModalAlumno").find('.modal-body').load("/Alumno/Create/");
});

function editAlumno(AlumnoId) {
    $("#myModalAlumno").modal("show");
    $("#myModalAlumno").find('.modal-title').html("Editar Alumno");
    $("#myModalAlumno").find('.modal-body').load("/Alumno/Edit/"+AlumnoId);
}

function DetailsAlumno(AlumnoId) {
    $("#myModalAlumno").modal("show");
    $("#myModalAlumno").find('.modal-title').html("Detalles de Alumno");
    $("#btnGuardarAlumno").addClass("hidden");
    $("#myModalAlumno").find('.modal-body').load("/Alumno/Details/" + AlumnoId);
}

$("#btnGuardarAlumno").click(function () {
    var formAlumno = $("#formAlumno").serialize();
    $.post("/Alumno/Create/", formAlumno)
        .done(function (data) {
            if (data === "Error") {
                $("#idAlertRepetido").removeClass("hidden");
            }
            if (data.data === "ok") {
                $("#myModalAlumno").modal("hide");
                toastr.success('Alumno agregado Correctamente');
            }
            else {
                $("#myModalAlumno").find('.modal-body').html(data);
            }
        });
});

$("#criterio").keyup(function () {
    $.post("/Alumno/Index/?criterio="+$(this).val())
        .done(function (data) {
            $("#search_result_Alumno").html(data);
        });
});

