(function () {
    'use strict'

    // Fetch all the forms we want to apply custom Bootstrap validation styles to
    var forms = document.querySelectorAll('.needs-validation')

    // Loop over them and prevent submission
    Array.prototype.slice.call(forms)
        .forEach(function (form) {
            form.addEventListener('submit', function (event) {
                if (!form.checkValidity()) {
                    event.preventDefault()
                    event.stopPropagation()
                }
                form.classList.add('was-validated')
            }, false)
        })
})()



    < script >

    $(document).ready(function () {
        obtenerTipoDcmmtoIdentidad();
    })

function obtenerTipoDcmmtoIdentidad() {

    $.ajax({
        url: "@Url.Action("getUbigeoDepartamento", "UbigeoViewGenerico")",
        type: "GET",
        dataType: "json",
        success: function (response) {
            $("#cbxTipoDcmtoIdentidad").append(
                $("<option>").attr({ "value": "" }).text("Seleccionar")
            )

            $.each(response, function (index, elemento) {

                $("#cbodepartamento").append(
                    $("<option>").attr({ "value": elemento.departamento }).text(elemento.departamento)
                )

            })

            $("#cbodepartamento").select2({ placeholder: "Seleccionar", width: "100%" });
        }
    })

}

                         
                        

                    </script >