$(".nuevaSeccion").click(function () {
    $("#myModalSeccion").find('.modal-title').html("<h2>Agregar Sección</h2>");
    $("#myModalSeccion").find('.modal-body').load("/Secciones/Create/");
});

function EliminarSeccion(id) {
    bootbox.confirm("Esta seguro de Eliminar esta Sección?",
        function (result) {
            if (result) {

                $.get("/Secciones/Delete/" + id, function (data) {
                    $("#search_result_Seccion").html(data);
                });
            }
        }
    );
};
$('.btnEditarSeccion').click(function () {
    var id = $(this).attr('data-id');
    $.get("/Secciones/Edit/" + id, function (data) {
        $("#myModalSeccion").find('.modal-title').html("<h2>Editar Sección</h2>");
        $("#myModalSeccion").find('.modal-body').html(data);
        $('#myModalSeccion').modal('show');
    });
});

$("#btnGuardaSecc").click(function () {
    var formSecc = $("#formSeccion").serialize();
    $.post("/Secciones/Create/",
            formSecc
        )
        .done(function (data) {
            if (data.rpt === "error") {
                $("#myModalSeccion").find('.modal-body').html(data.info);
            }
            if (data.rpt === "existe") {
                $(".alertaSecc").removeClass("hidden");
            }
            $('#myModalSeccion').modal('hide');
            
            
        });
});
