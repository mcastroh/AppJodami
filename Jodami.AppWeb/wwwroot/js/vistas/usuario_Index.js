 
let modeloBase = {
    idUsuario: 0,
    nombres: "",
    correo: "",
    telefono: "",
    idRol: 0,
    urlFoto: "",
    nombreFoto: "",
    clave: "",
    esActivo: true,
    nameRolAsignado: "",
};
 
let tablaData;

$(document).ready(function () { 

    tablaData = $('#tbdata').DataTable({
        responsive: true,
         "ajax": {
             "url": '/Usuario/ListaUsuarios',
             "type": "GET",
             "datatype": "json"
         },
         "columns": [
             { "data": "idUsuario", "visible": false, "searchable": false },
             { "data": "nombres" },
             { "data": "correo" },
             { "data": "telefono" },     
             { "data": "nameRolAsignado" },   
             //{
             //    "data": "urlFoto", render: function (data) {
             //        return `<img style="height:60px" src=${data} class="rounded mx-auto d-block"/>` 
             //    }
             //},   
             {
                 "data": "esActivo", render: function (data) {
                     if (data == true)
                         return `<span class="badge badge-info">Activo</span>`
                     else
                         return `<span class="badge badge-danger">De Baja</span>`
                 }
             }, 
             {
                 "defaultContent": '<button class="btn btn-primary btn-editar btn-sm mr-2"><i class="fas fa-pencil-alt"></i></button>' +
                     '<button class="btn btn-danger btn-eliminar btn-sm"><i class="fas fa-trash-alt"></i></button>',
                 "orderable": false,
                 "searchable": false,
                 "width": "100px"
             }
         ],
        order: [[1, "asc"]],
        dom: "Bfrtip",
        buttons: [
            {
                text: 'Exportar Excel',
                extend: 'excelHtml5',
                title: '',
                filename: 'Reporte Usuarios',
                exportOptions: {
                    columns: [1, 2, 3, 4, 5]
                }
            }, 'pageLength'
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        },
    }); 

});
   
function cargarRoles() {

    $("#cboRol").empty();

    $.getJSON("listaRoles", {}, function (data) {
        $("#cboRol").append($("<option>").attr({ "value": "" }).text("Seleccionar"));
        $.each(data, function (i, obj) {
            $("#cboRol").append("<option value='" + obj.codigoKey + "'>" + obj.nameKey + "</option>");
        });
    });  
}

function mostarModal(modelo = modeloBase) {  
    $("#txtId").val(modelo.idUsuario)
    $("#txtNombre").val(modelo.nombres)
    $("#txtCorreo").val(modelo.correo)
    $("#txtTelefono").val(modelo.telefono)
    $("#cboRol").val(modelo.idRol == 0 ? $("#cboRol option:first").val() : modelo.idRol)
    $("#cboEstado").val(modelo.esActivo)
    $("#txtFoto").val("")
    $("#imgUsuario").attr("src", modelo.urlFoto)   
}
 
$("#btnNuevo").click(function () { 
    cargarRoles();  
    mostarModal();
    $("#modalData").modal("show"); 

});

$("#btnGuardar").click(function () {
    
    console.log('function => Guardar Datos...')

});

