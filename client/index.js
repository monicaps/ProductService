function modificaModal(signal) {
    if (signal==1) {
    //registro nuevo usuario
        $("#camposUsuario").show();
        $("#camposPassword").show();
        $("#camposNewUser").show();
        $("#camposNewPassword").show();

        $("#camposOldUser").hide();
        $("#camposSearchedUser").hide();
        $("#camposNameUser").hide();
        $("#camposEmailUser").hide();
        $("#camposRolUser").hide();
        $("#camposTelefonoUser").hide();
        var actButton=document.getElementById('operacion');
        actButton.setAttribute("onclick","registrarUsuario()");
    }
    if (signal==2) {
        $("#camposUsuario").show();
        $("#camposPassword").show();
        $("#camposSearchedUser").show();
        $("#camposNameUser").show();
        $("#camposEmailUser").show();
        $("#camposRolUser").show();
        $("#camposTelefonoUser").show();

        $("#camposOldUser").hide();
        $("#camposNewUser").hide();
        $("#camposNewPassword").hide();
        var actButton=document.getElementById('operacion');
        actButton.setAttribute("onclick","registrarInfoUsuario()");
    }
    if (signal==3) {
        $("#camposUsuario").show();
        $("#camposPassword").show();
        $("#camposOldUser").show();
        $("#camposNewUser").show();
        $("#camposNewPassword").show();

        $("#camposSearchedUser").hide();
        $("#camposNameUser").hide();
        $("#camposEmailUser").hide();
        $("#camposRolUser").hide();
        $("#camposTelefonoUser").hide();
        var actButton=document.getElementById('operacion');
        actButton.setAttribute("onclick","actualizarUsuario()");
    }
    if (signal==4) {
        $("#camposUsuario").show();
        $("#camposPassword").show();
        $("#camposSearchedUser").show();
        $("#camposNameUser").show();
        $("#camposEmailUser").show();
        $("#camposRolUser").show();
        $("#camposTelefonoUser").show();

        $("#camposOldUser").hide();
        $("#camposNewUser").hide();
        $("#camposNewPassword").hide();
        var actButton=document.getElementById('operacion');
        actButton.setAttribute("onclick","actualizarInfoUsuario()");
    }
}

function validar(band) {
    if (band==1) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var nuevoUsuario=document.getElementById('labelNewUser').value;
        var nuevoPassword=document.getElementById('labelNewPassword').value;

        if (user == ""||pass == ""||nuevoUsuario == ""||nuevoPassword == ""){
            return false;
        }else {
            return true;
        }
    }
    if (band==2) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var usuarioBuscado=document.getElementById('labelSearchedUser').value;
        var nombre=document.getElementById('labelNameUser').value;
        var correo=document.getElementById('labelEmailUser').value;
        //var rol=document.getElementById('rolSelect').value;
        var telefono=document.getElementById('labelTelefonoUser').value;
        if (user == ""||pass == ""||usuarioBuscado == ""||nombre == ""||correo==""||telefono==""){
            return false;
        }else {
            return true;
        }
    }
    if (band==3) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var oldUsuario=document.getElementById('labelOldUser').value;
        var nuevoUsuario=document.getElementById('labelNewUser').value;
        var nuevoPassword=document.getElementById('labelNewPassword').value;
        if (user == ""||pass == ""||oldUsuario==""||nuevoUsuario == ""||nuevoPassword == ""){
            return false;
        }else {
            return true;
        }
    }
    if (band==4) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var usuarioBuscado=document.getElementById('labelSearchedUser').value;
        var nombre=document.getElementById('labelNameUser').value;
        var correo=document.getElementById('labelEmailUser').value;
        //var rol=document.getElementById('rolSelect').value;
        var telefono=document.getElementById('labelTelefonoUser').value;
        if (user == ""||pass == ""||usuarioBuscado == ""||nombre == ""||correo==""||telefono==""){
            return false;
        }else {
            return true;
        }
    }

}

function registrarUsuario(){
    // Evita el refresh de la página
    event.preventDefault();
    var valido=validar(1);
    if (valido==true) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var nuevoUsuario=document.getElementById('labelNewUser').value;
        var nuevoPassword=document.getElementById('labelNewPassword').value;

        console.log(user);
        console.log(pass);
        console.log(nuevoUsuario);
        console.log(nuevoPassword);

        $.ajax({
            url:'SetUser.php',
            type:'POST',
            data:{"user":user,"pass":pass,"newUser":nuevoUsuario,"newPass":nuevoPassword},
            success:function (response) {
                console.log("Success\n"+response);
                var jsonRespuesta=JSON.parse(response);
                if(jsonRespuesta.Status=="Error"){
                    var mensaje1=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-danger");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje1);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                 }else{
                    var mensaje2=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message+" Realizado el "+jsonRespuesta.Data;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-success");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje2);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                }
            },
            error:function() {
                console.log("Error\n");
                var formulario = document.getElementsByClassName("formulario");
                var alerta = document.createElement("div");
                alerta.setAttribute("id", "alerta");
                alerta.setAttribute("class", "alert alert-danger");
                alerta.setAttribute("role", "alert");
                var texto = document.createTextNode("Error desconocido, intente más tarde.");
                alerta.appendChild(texto);
                formulario[0].appendChild(alerta);
            }
        });
    } else {
        var formulario = document.getElementsByClassName("formulario");
        var alerta = document.createElement("div");
        alerta.setAttribute("id", "alerta");
        alerta.setAttribute("class", "alert alert-warning");
        alerta.setAttribute("role", "alert");
        var texto = document.createTextNode("Llena todos los campos");
        alerta.appendChild(texto);
        formulario[0].appendChild(alerta);
    }

}

function registrarInfoUsuario(){
    // Evita el refresh de la página
    event.preventDefault();
    var valido=validar(2);

    if (valido==true) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var usuarioBuscado=document.getElementById('labelSearchedUser').value;
        var nombre=document.getElementById('labelNameUser').value;
        var correo=document.getElementById('labelEmailUser').value;
        var rol=document.getElementById('rolSelect').value;
        var telefono=document.getElementById('labelTelefonoUser').value;
        $.ajax({
            url:'SetUserInfo.php',
            type:'POST',
            data:{"user":user,"pass":pass,"searchedUser":usuarioBuscado,"name":nombre,"email":correo,
                "rol":rol,"tel":telefono},
            success:function (response) {
                console.log(response);
                console.log("Success\n"+response);
                var jsonRespuesta=JSON.parse(response);
                if(jsonRespuesta.Status=="Error"){
                    var mensaje1=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-danger");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje1);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                 }else{
                    var mensaje2=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message+" Realizado el "+jsonRespuesta.Data;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-success");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje2);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                }
            },
            error:function() {
                console.log("Error\n");
                var formulario = document.getElementsByClassName("formulario");
                var alerta = document.createElement("div");
                alerta.setAttribute("id", "alerta");
                alerta.setAttribute("class", "alert alert-danger");
                alerta.setAttribute("role", "alert");
                var texto = document.createTextNode("Error desconocido, intente más tarde.");
                alerta.appendChild(texto);
                formulario[0].appendChild(alerta);
            }
        });
    } else {
        var formulario = document.getElementsByClassName("formulario");
        var alerta = document.createElement("div");
        alerta.setAttribute("id", "alerta");
        alerta.setAttribute("class", "alert alert-warning");
        alerta.setAttribute("role", "alert");
        var texto = document.createTextNode("Llena todos los campos");
        alerta.appendChild(texto);
        formulario[0].appendChild(alerta);
    }
}

function actualizarUsuario(){
    // Evita el refresh de la página
    event.preventDefault();
    var valido=validar(3);

    if (valido==true) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var oldUsuario=document.getElementById('labelOldUser').value;
        var nuevoUsuario=document.getElementById('labelNewUser').value;
        var nuevoPassword=document.getElementById('labelNewPassword').value;

        $.ajax({
            url:'UpdateUser.php',
            type:'POST',
            data:{"us":user,"passw":pass,"oldUser":oldUsuario,"nUser":nuevoUsuario,
                "nPassword":nuevoPassword},
            success:function (response) {
                console.log(response);
                console.log("Success\n"+response);
                var jsonRespuesta=JSON.parse(response);
                if(jsonRespuesta.Status=="Error"){
                    var mensaje1=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-danger");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje1);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                 }else{
                    var mensaje2=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message+" Realizado el "+jsonRespuesta.Data;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-success");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje2);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                }
            },
            error:function() {
                console.log("Error\n");
                var formulario = document.getElementsByClassName("formulario");
                var alerta = document.createElement("div");
                alerta.setAttribute("id", "alerta");
                alerta.setAttribute("class", "alert alert-danger");
                alerta.setAttribute("role", "alert");
                var texto = document.createTextNode("Error desconocido, intente más tarde.");
                alerta.appendChild(texto);
                formulario[0].appendChild(alerta);
            }
        });
    } else {
        var formulario = document.getElementsByClassName("formulario");
        var alerta = document.createElement("div");
        alerta.setAttribute("id", "alerta");
        alerta.setAttribute("class", "alert alert-warning");
        alerta.setAttribute("role", "alert");
        var texto = document.createTextNode("Llena todos los campos");
        alerta.appendChild(texto);
        formulario[0].appendChild(alerta);
    }
}

function actualizarInfoUsuario(){
    // Evita el refresh de la página
    event.preventDefault();
    var valido=validar(4);

    if (valido==true) {
        var user=document.getElementById('labelUser').value;
        var pass=document.getElementById('labelPassword').value;
        var usuarioBuscado=document.getElementById('labelSearchedUser').value;
        var nombre=document.getElementById('labelNameUser').value;
        var correo=document.getElementById('labelEmailUser').value;
        var rol=document.getElementById('rolSelect').value;
        var telefono=document.getElementById('labelTelefonoUser').value;
        $.ajax({
            url:'UpdateUserInfo.php',
            type:'POST',
            data:{"user":user,"pass":pass,"searchedUser":usuarioBuscado,"name":nombre,"email":correo,
                "rol":rol,"tel":telefono},
            success:function (response) {
                console.log(response);
                console.log("Success\n"+response);
                var jsonRespuesta=JSON.parse(response);
                if(jsonRespuesta.Status=="Error"){
                    var mensaje1=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-danger");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje1);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                 }else{
                    var mensaje2=jsonRespuesta.Status+": (Código "+jsonRespuesta.Code+") "
                    +jsonRespuesta.Message+" Realizado el "+jsonRespuesta.Data;

                    var formulario = document.getElementsByClassName("formulario");
                    var alerta = document.createElement("div");
                    alerta.setAttribute("id", "alerta");
                    alerta.setAttribute("class", "alert alert-success");
                    alerta.setAttribute("role", "alert");
                    var texto = document.createTextNode(mensaje2);
                    alerta.appendChild(texto);
                    formulario[0].appendChild(alerta);
                }
            },
            error:function() {
                console.log("Error\n");
                var formulario = document.getElementsByClassName("formulario");
                var alerta = document.createElement("div");
                alerta.setAttribute("id", "alerta");
                alerta.setAttribute("class", "alert alert-danger");
                alerta.setAttribute("role", "alert");
                var texto = document.createTextNode("Error desconocido, intente más tarde.");
                alerta.appendChild(texto);
                formulario[0].appendChild(alerta);
            }
        });
    } else {
        var formulario = document.getElementsByClassName("formulario");
        var alerta = document.createElement("div");
        alerta.setAttribute("id", "alerta");
        alerta.setAttribute("class", "alert alert-warning");
        alerta.setAttribute("role", "alert");
        var texto = document.createTextNode("Llena todos los campos");
        alerta.appendChild(texto);
        formulario[0].appendChild(alerta);
    }
}
