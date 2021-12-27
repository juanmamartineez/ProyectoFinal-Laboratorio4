$("#selectImg").change(function () { //Selecciono el elemento con el id(#)
    //obtiene el nombre del archivo y el tamaño
    var fileName = this.files[0].name;
    var fileSize = this.files[0].size;

    if (fileSize > 3000000) {
        //Validamos que no supere los 3MB, lanzamos una alerta si supera y dejamos vacios los campos
        alert('El archivo no debe superar los 3MB');
        this.value = '';
        this.files[0].name = '';
    } else {
        //Recuperamos la extension del archivo
        var extension = fileName.split('.').pop();

        //Convertimos la extension en minuscula
        extension.toLowerCase();

        switch (extension) {
            case 'jpeg':
            case 'jpg':
            case 'png':
            break;
            default:
                alert('El archivo no tiene la extension correcta');
                this.value = '';
                this.files[0].name = '';
        }
    }
    readURL(this)
})

function readURL(input) {
    if (input.files && input.files[0]) { //Si existen archivos
        var reader = new FileReader(); //lee el archivo

        reader.onload = function(e) { 
            $("#image").attr("src", e.target.result); //Cuando carga la imagen accede al atributo src y le asigna el result de la imagen
            $("#image").attr("display", "block");
        }

        reader.readAsDataURL(input.files[0]); //Lee el archivo en string base 64
    }
}