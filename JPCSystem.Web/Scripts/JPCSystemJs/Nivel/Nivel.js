var id = $("#btnEliminaNivel").data("id");
var link = "/Nivel/Delete/" + id;
$(".nuevoNivel").click(function () {
    $("#myModalNivel").find('.modal-body').load("/Nivel/Create");
});


$("#btnGuardaNivel").click(function () {
    var formNivel = $("#formNivel").serialize();
    console.log(formNivel);
    $.post("/Nivel/Create/", formNivel)
        .done(function (data) {
            if (data.msj === "ok") {
                $("#search_Nivel").html(data.info);
                $("#myModalNivel").modal("hide");
            }
            $("#myModalNivel").find('.modal-body').html(data);
        });
});

$(".btnEditar").click(function (eve) {
    $("#myModalNivel").modal("show");
    $("#myModalNivel").find(".modal-title").html("Editar Nivel");
    $("#myModalNivel").find('.modal-body').load("/Nivel/Edit/" + $(this).data("id"));
});

function funcion(id) {
    //var id = id;
    console.log(id);
    var link = "/Nivel/Delete/" + id;
    bootbox.confirm("Esta seguro de Eliminar este Nivel?", function (result) {
        if (result) {
            var a = document.getElementById('btnEliminaNivel');
            a.href = link;
            $(".search_Nivel").html(data);
        }
    });
}