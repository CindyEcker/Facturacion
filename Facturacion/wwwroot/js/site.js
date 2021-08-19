
function updateArticulo() {
    $.ajax({
        data: { id: $("#ID_Articulo").val() },
        type: "GET",
        url: '@Url.Action("detallesArticulo", "Facturas")',
        success: function (result) {
            $("#precioUnit").val(result);
        }
    });

    $.ajax({
        data: { cantidad: $("#Cantidad").val(), id: $("#ID_Articulo").val() },
        type: "GET",
        url: '@Url.Action("montoTotal", "Facturas")',
        success: function (resultado) {
            $("#Monto").val(resultado);
        }
    });
}