<?php
	$servicio="http://localhost:59754/MyWebService.svc?wsdl";

	$parametros = array();
	$user=$_POST['user'];
	$pass=$_POST['pass'];
	$usern=$_POST['newUser'];
	$passn=$_POST['newPass'];
	$client = new soapclient( $servicio, $parametros );

	if (strlen($passn)>=8 && preg_match('`[0-9]`', $passn)) {
		//$data = '"'.$passn.'"';
		$array=array($usern=>$passn);
		$jsonUs=json_encode($array);

		$result = $client->SetUser($user,$pass,$usern,$jsonUs);

		print_r(json_encode($result));
    }
    else {
    	echo "<div class='alert alert-warning' role='alert' id='alerta'>Contraseña inválida, intenta ingresar una nueva</div>";
    }

?>