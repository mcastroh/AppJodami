 
let modeloBase = {
    idUsuario: 0,
    nombres: "",
    correo: "",
    telefono: "",
    idRol: 0,
    urlFoto: "",
    nombreFoto: "",
    clave: "",
    esActivo: 1,
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
                     if (data == 1)
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
    let inputConValor = $("input.input-validar").serializeArray(); 
    let inputSinValor = inputConValor.filter((item) => item.value.trim() == "");
     
    if (inputSinValor.length > 0) { 
        let mensaje = `Debe ingresar datos: "${inputSinValor[0].name}"`; 
        toastr.warning("", mensaje);
        $(`inputSinValor[name="${inputSinValor[0].name}"]`).focus();
        return;
    }

    let modelo = structuredClone(modeloBase);

    modelo["idUsuario"] = parseInt($("#txtId").val());
    modelo["nombres"] = $("#txtNombre").val();
    modelo["correo"] = $("#txtCorreo").val();
    modelo["telefono"] = $("#txtTelefono").val();
    modelo["idRol"] = $("#cboRol").val();
    modelo["esActivo"] = $("#cboEstado").val();

    let inputFoto = document.getElementById("txtFoto");

    let formData = new FormData();
    formData.append("foto", inputFoto.files[0]);
    formData.append("modelo", JSON.stringify(modelo));

    $("#modalData").find("div.modal-content").LoadingOverlay("show");

    if (modelo.idUsuario == 0) {

        fetch("/Usuario/Crear", {
            method: "POST",
            body: formData           
        })
            .then(response => {
                $("#modalData").find("div.modal-content").LoadingOverlay("hide");
                return response.ok ? response.json() : Promise.reject(response);
            })
            .then(responseJson => { 
                if (responseJson.estado) {
                    tablaData.row.add(responseJson.objeto).draw(false);
                    $("#modalData").modal("hide");
                    swal("Listo", "El usuario fue creado", "success");
                } else {                   
                    swal("", responseJson.mensaje, "error");
                }
            }) 
    } 
});

let filaSeleccionada;

$('#tbdata tbody').on("click", ".btn-editar", function () {    
    if ($(this).closest("tr").hasClass("child")) {
        filaSeleccionada = $(this).closest("tr").prev();
    } else {
        filaSeleccionada = $(this).closest("tr");
    }
   
    let modelo = tablaData.row(filaSeleccionada).data();
   
    console.log('modelo => ', modelo);
      
    cargarRoles();  

    //$("#cboRol").empty();
    //$.getJSON("listaRoles", {}, function (data) {
    //    $("#cboRol").append($("<option>").attr({ "value": "" }).text("Seleccionar"));
    //    $.each(data, function (i, obj) {
    //        $("#cboRol").append("<option value='" + obj.codigoKey + "'>" + obj.nameKey + "</option>");
    //    });
    //});  

    

    //Deseleccionar todas las opciones: 
/*    document.getElementById("cboRol").selectedIndex = "-1";*/

      //Devuelve la propiedad de índice seleccionado:
  /*  selectObject.selectedIndex*/

    //Seleccione el elemento < opción > con índice "2": 
    //document.getElementById("cboRol").selectedIndex = "2";
    //var dnd = document.querySelector('#cboRol'); 
    ////dnd.selectedIndex;
    //console.log('dnd', dnd.selectedIndex)

    var dnd = document.querySelector('#cboRol'); 
    console.log('dnd', dnd);

    Array.from(dnd.options).forEach(function (option, index) {

        console.log(option.id);

        // If the ID is "rogue", set this items index as the selectedIndex
        if (option.id === 'Empleado') {
            dnd.selectedIndex = index;
        }

    });


    //var x = document.getElementById("cboRol").selectedIndex;
    //var y = document.getElementById("cboRol").options;
    ////alert("Index: " + y[x].index + " is " + y[x].text);

    ////Establezca la propiedad de índice seleccionado: 
    //selectObject.selectedIndex = 3;

     
     

    mostarModal(modelo);
    $("#modalData").modal("show");
})
 