$("#btnNuevoCurso").click(function () {
    $("#myModalCurso").modal('show')
    $("#myModalCurso").find('.modal-title').html("Agregar Curso");
    $("#myModalCurso").find('.modal-body').load("/Curso/Create/");
});

function EditCurso(id) {
    $("#myModalCurso").modal("show");
    $("#myModalCurso").find('.modal-title').html("Editar Curso");
    $("#myModalCurso").find('.modal-body').load("/Curso/Edit/" + id);
}

$("#btnGuardarCurso").click(function () {
    var formCurso1 = $("#formCurso1").serialize();
    $.post("/Curso/Create/", formCurso1)
        .done(function (data) {
            if (data.rpt === "Existe") {
                toastr.warning("El curso ya se encuentra registrado");
            }
            if (data.rpt === "false") {
                $("#myModalCurso").find(".modal-body").html(data.info);
            }
            if (data.rpt === "true") {
                $("#myModalCurso").modal("hide");
                toastr.success("Curso Regitrado con exito");
                $("#search_result_Curso").html(data.info);
            }
            if (data.rpt === "edit") {
                $("#myModalCurso").modal("hide");
                toastr.success("Curso Editado con exito");
                $("#search_result_Curso").html(data.info);
            }
            
        });
});
