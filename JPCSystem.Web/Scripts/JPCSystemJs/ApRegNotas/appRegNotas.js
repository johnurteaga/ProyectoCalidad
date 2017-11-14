$("#btnAgregarRegistro").click(function() {
    $.get("/ApRegNotas/Create/")
        .done(function (data) {
            console.log(data);
            if (data.rpt === "ok") {
                toastr.options = {
                    "positionClass": "toast-top-center",
                    "preventDuplicates": true
                }

                toastr.info('Los Timestres para el año <strong>' + data.fecha + " </strong> ya fueron configurados.");
            } else {
                $("#myModalApRegistro").modal("show");
                $("#myModalApRegistro").find(".modal-title").html("<h2>Aperturar Registro Notas </h2>");
                $("#myModalApRegistro").find(".modal-body").html(data);

                console.log(data);
            }
        });
});

$("#btnGuardaRegistro").click(function () {
    var ApRegNotas = $("#ApRegNotas").serialize();
    $.post("/ApRegNotas/Create", ApRegNotas)
        .done(function (data) {
            if (data.rpt === "Existe") {
                toastr.options = {
                    "positionClass": "toast-top-center",
                    "preventDuplicates": true
                }

                toastr.warning('Este Registro ya exite !');
            }
            if (data.rpt === "false") {
                $("#myModalApRegistro").find('.modal-body').html(data.info);
            }
            if (data.rpt === "true") {
                $("#myModalApRegistro").modal("hide");
                $("#search_result_Registro").html(data.info);
            }
        });
    //$("#myModalHorarios").modal("hide");
});

$("#criterioRegistro").keyup(function () {
    var criterio = $(this).val();
    $.post("/ApRegNotas/Index/",
            {
                criterio: criterio
            }
        )
        .done(function (data) {
            $("#search_result_Registro").html(data);
        });
});

function editRegistro(id)
{
    $("#myModalApRegistro").modal("show");
    $("#myModalApRegistro").find(".modal-title").html("<h2>Editar Registro Notas </h2>");
    $("#myModalApRegistro").find(".modal-body").load("/ApRegNotas/Edit/?id="+id);
}