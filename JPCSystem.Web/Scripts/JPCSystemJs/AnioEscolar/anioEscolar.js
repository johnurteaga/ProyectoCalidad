$("#btnAgregarAnio").click(function () {
    $.get("/AnioEscolar/Create/")
        .done(function (data) {
            if (data.msg === "ok") {
                toastr.options = {
                    "positionClass": "toast-top-center",
                    "preventDuplicates": true
                }

                toastr.info('El año academico <strong> ' + data.fecha + ' </strong> ya se encuentra registrado, regrese el proximo año');
            } else {
                $("#myModalAnio").modal("show");
                $("#myModalAnio").find(".modal-title").html("<h2>Aperturar Nuevo Año </h2>");
                $("#myModalAnio").find(".modal-body").html(data);

            }
        });
});

$("#btnGuardaAnio").click(function () {
    var formAnio = $("#formAnio").serialize();
    console.log(formAnio);
    $.post("/AnioEscolar/Create", formAnio)
        .done(function (data) {
            if (data.rpt === "Existe") {
                toastr.options = {
                    "positionClass": "toast-top-center",
                    "preventDuplicates": true
                }

                toastr.warning('Este Registro ya exite!');
            }
            if (data.rpt === "Error") {
                toastr.warning(data.info);
            }
            if (data.rpt === "false") {
                $("#myModalAnio").find('.modal-body').html(data.info);
            }
            if (data.rpt === "true") {
                $("#myModalAnio").modal("hide");
                $("#search_result_anio").html(data.info);
            }
        });
});

$("#criterioAnio").keyup(function () {
    var criterio = $(this).val();
    $.post("/AnioEscolar/Index/",
            {
                criterio: criterio
            }
        )
        .done(function (data) {
            $("#search_result_anio").html(data);
        });
});

function editAnio(id) {
    $("#myModalAnio").modal("show");
    $("#myModalAnio").find(".modal-title").html("<h2>Editar Año Academico </h2>");
    $("#myModalAnio").find(".modal-body").load("/AnioEscolar/Edit/?id=" + id);
}