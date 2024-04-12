
// --------------------
// Validar s�lo letras
// --------------------
function validateSoloLetras(e) {
    key = e.keyCode || e.which;
    tecla = String.fromCharCode(key).toString();
    letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ�����abcdefghijklmnopqrstuvwxyz�����";

    especiales = [8, 13];
    tecla_especial = false

    for (var i in especiales) {
        if (key == especiales[i]) {
            tecla_especial = true;
            break;
        }
    }

    if (letras.indexOf(tecla) == -1 && !tecla_especial) {
        alert("Ingresar s�lo letras");
        return false;
    }
}
