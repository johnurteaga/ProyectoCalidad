$("#btnNuevoDocente").click(function (eve) {
    //$("#myModalAlumno").find('.modal-body').html("");
    $("#myModalDocente").find('.modal-title').html("Agregar Docente");
    $("#myModalDocente").find('.modal-body').load("/Docente/Create/");
});

function editDocente(DocenteId) {
    $("#myModalDocente").modal("show");
    $("#myModalDocente").find('.modal-title').html("Editar Docente");
    $("#myModalDocente").find('.modal-body').load("/Docente/Edit/" + DocenteId);
}

function DetailsDocente(DocenteId) {
    $("#myModalDocente").modal("show");
    $("#myModalDocente").find('.modal-title').html("Detalles de Docente");
    $("#btnGuardarDocente").addClass("hidden");
    $("#myModalDocente").find('.modal-body').load("/Docente/Details/" + DocenteId);
}

$("#btnGuardarDocente").click(function () {
    var formDocente = $("#formDocente").serialize();
    $.post("/Docente/Create/", formDocente)
        .done( function (data) {
            if (data.rpt === "Exite") {
                $("#idAlertRepetidoDocente").removeClass("hidden");
            }
            if (data.rpt === "false")
            {
                $("#myModalDocente").find('.modal-body').html(data.info);
            } 
            if (data.rpt === "true") {
                $("#myModalDocente").modal("hide");
                $("#search_result_Docente").html(data.info);
            }
        });
});

$("#criterioDocente").keyup(function () {
    $.post("/Docente/Index/?criterio=" + $(this).val())
        .done(function (data) {
            $("#search_result_Docente").html(data);
        });
});

