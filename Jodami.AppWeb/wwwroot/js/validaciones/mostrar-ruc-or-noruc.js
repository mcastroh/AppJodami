function tipoDcmtoIdentidadSelect() {

    let tipoDcmto = document.getElementById('cbxTipoDcmtoIdentidad');
    var value = tipoDcmto.value;
    var text = tipoDcmto.options[tipoDcmto.selectedIndex].text.substring(0, 3);

    if (text == 'RUC') {
        hideApellidosAndNombre();
        showRazonSocial();
    } else {
        hideRazonSocial();
        showApellidosAndNombre();
    }
}

function hideApellidosAndNombre() {
    var elem = document.getElementById('tipoDcmtoNoRUC');
    elem.style.display = 'none';
}

function showApellidosAndNombre() {
    var elem = document.getElementById('tipoDcmtoNoRUC');
    elem.style.display = 'inline-flex';
}

function hideRazonSocial() {
    var elem = document.getElementById('tipoDcmtoRUC');
    elem.style.display = 'none';
}

function showRazonSocial() {
    var elem = document.getElementById('tipoDcmtoRUC');
    elem.style.display = 'inline';
}

/*window.onload = hideApellidosAndNombre;*/