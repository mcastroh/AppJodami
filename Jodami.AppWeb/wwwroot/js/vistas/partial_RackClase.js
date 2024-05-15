$(document).ready(function () {

    console.log('ingresa a document.ready');

    var Accion = $('#hdnAccion').val();

    switch (Accion) {
        case "Nuevo": LimpiarControles(); break;
        case "Actualizar": BloquearControlesEdicion(); break;
        case "Consultar": BloquearControlesConsulta(); break;
    }
})

function LimpiarControles() {
    $('#RackClase_IdClaseRack').val("");
    $('#RackClase_DescClaseRack').val("");
}

function BloquearControlesEdicion() {
    $('#RackClase_IdClaseRack').attr('readonly', true);
}

function BloquearControlesConsulta() {
    $('#RackClase_IdClaseRack').attr('readonly', true);
    $('#RackClase_DescClaseRack').attr('readonly', true);
    $('#btnGrabar').attr('disabled', true);

}

function Eliminar(IdClaseRack) {
    var rowindex = this.document.activeElement.parentElement.parentElement.rowIndex;
    var table = document.getElementById("idClaseRack");

    $.confirm({
        title: 'Eliminar',
        content: 'Esta seguro de eliminar el registro ?',
        icon: 'fa fa-question',
        theme: 'bootstrap',
        closeIcon: true,
        animation: 'scale',
        type: 'orange',
        buttons: {
            Confirmar: function () {
                console.log(rowindex);
                var xhttp = new XMLHttpRequest();
                xhttp.onreadystatechange = function () {
                    if (this.readyState == 4 && this.status == 200) {

                        var data = xhttp.responseText;
                        var jsonResponse = JSON.parse(data);
                        if (jsonResponse.Success == true) {
                            location.reload();
                        }
                    }
                };
                var URL = $("#UrlEliminar").val();
                xhttp.open("POST", URL);
                xhttp.setRequestHeader("Content-Type", "application/json");

                var Data = JSON.stringify({ pIdClaseRack: IdClaseRack });

                xhttp.send(Data);

            },
            Cancelar: function () {
            }
        }
    });
};
