  
let error = document.getElementById('error');
error.style.color = 'red';

let tipoDcmto = document.getElementById('cbxTipoDcmtoIdentidad');
let value = tipoDcmto.value;
let text = tipoDcmto.options[tipoDcmto.selectedIndex].text.substring(0, 3);
let numeroDcmtoIdentidad = document.getElementById('NumeroDcmtoIdentidad');
 
let form = document.getElementById('formProveedor');

form.addEventListener('submit', function (evt) {
    console.log('Guardar  ...');
    console.log('tipoDcmto  ...', value);
    console.log('numeroDcmtoIdentidad  ...', numeroDcmtoIdentidad);
    evt.preventDefault();
    
     

})

 