$("#btnNuevoHorario").click(function (eve) {
    $("#myModalHorarios").find('.modal-title').html("<h2>Gestion de Horario de Curso</h2>");
    $("#myModalHorarios").find('.modal-body').load("/DocenteCurso/Create/");
});

function editHorario(HoraId) {
    $("#myModalHorarios").modal("show");
    $("#myModalHorarios").find('.modal-title').html("Editar Horario de Curso");
    $("#myModalHorarios").find('.modal-body').load("/DocenteCurso/Edit/" + HoraId);
}

$('#IdDocente').selectize({
    create: false,
    sortField: 'text'
});

$("#btnGuardarHorario").click(function () {
    var formHorarios = $("#formHorarios").serialize();
    console.log(formHorarios);
    $.post("/DocenteCurso/Create", formHorarios)
        .done(function (data) {
            if (data.rpt === "Existe") {
                //$("#HorarioRegistrado").removeClass("hidden");
                $("#idErrorHorario").removeClass("hidden");
            }
            if (data.rpt === "false")
            {
                $("#myModalHorarios").find('.modal-body').html(data.info);
            } 
            if (data.rpt === "true") {
                $("#myModalHorarios").modal("hide");
                $("#search_result_Horario").html(data.info);
            }
        });
            //$("#myModalHorarios").modal("hide");
});

$("#criterioHora").keyup(function () {
    var criterio = $(this).val();
    var cursoId = $("#IdCurso").val();
    var nivelId = $("#IdNivel").val();
    var docenteId = $("#IdDocente").val();
    $.post("/DocenteCurso/Index/?",
            {
                criterio:criterio,
                cursoId:cursoId,
                nivelId:nivelId,
                docenteId:docenteId,
            }
        )
        .done(function(data) {
            $("#search_result_Horario").html(data);
        });

});

$("#IdCurso").change(function () {
    var criterio = $("#criterioHora").val();
    var cursoId = $("#IdCurso").val();
    var nivelId = $("#IdNivel").val();
    var docenteId = $("#IdDocente").val();
    $.post("/DocenteCurso/Index/?",
            {
                criterio: criterio,
                cursoId: cursoId,
                nivelId: nivelId,
                docenteId: docenteId
            }
        )
        .done(function (data) {
            $("#search_result_Horario").html(data);
        });

});

$("#IdNivel").change(function () {
    var criterio = $("#criterioHora").val();
    var cursoId = $("#IdCurso").val();
    var nivelId = $("#IdNivel").val();
    var docenteId = $("#IdDocente").val();
    $.post("/DocenteCurso/Index/?",
            {
                criterio: criterio,
                cursoId: cursoId,
                nivelId: nivelId,
                docenteId: docenteId
            }
        )
        .done(function (data) {
            $("#search_result_Horario").html(data);
        });

});

$("#IdDocente").change(function () {
    var criterio = $("#criterioHora").val();
    var cursoId = $("#IdCurso").val();
    var nivelId = $("#IdNivel").val();
    var docenteId = $("#IdDocente").val();
    $.post("/DocenteCurso/Index/?",
            {
                criterio: criterio,
                cursoId: cursoId,
                nivelId: nivelId,
                docenteId: docenteId
            }
        )
        .done(function (data) {
            $("#search_result_Horario").html(data);
        });
});

$("#btnCursoN").click(function (eve) {
    console.log("ASdasd")
    $("#myModalC").find('.modal-title').html("<h2>Agregar Crurso</h2>");
    $("#myModalC").find('.modal-body').load("/DocenteCurso//CreateCurso/");
});

$("#btnGuardarCurso").click(function () {
    var formCurso = $("#formCurso").serialize();
    console.log(formCurso);
    $.post("/DocenteCurso/CreateCurso", formCurso)
        .done(function (data) {
            //if (data.rpt === "Existe") {
            //    //$("#HorarioRegistrado").removeClass("hidden");
            //    $("#idErrorHorario").removeClass("hidden");
            //}
            if (data.rpt === "false") {
                $("#myModalC").find('.modal-body').html(data.info);
            }
            if (data.rpt === "true") {
                $("#myModalC").modal("hide");
                $("#search_result_Horario").html(data.info);
            }
        });
});



