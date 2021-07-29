<?php
	$servicio="http://localhost:59754/MyWebService.svc?wsdl";

	$parametros = array();
	$user=$_POST['user'];
	$pass=$_POST['pass'];
	$usern=$_POST['searchedUser'];
	$nameUser=$_POST['name'];
	$emailUser=$_POST['email'];
	$rolUser=$_POST['rol'];
	$telUser=$_POST['tel'];
	$client = new soapclient( $servicio, $parametros );

	$array=array("nombre"=>$nameUser,"correo"=>$emailUser,"rol"=>$rolUser,"telefono"=>$telUser);
	$jsonUs=json_encode($array);

	$result = $client->SetUserInfo($user,$pass,$usern,$jsonUs);

	print_r(json_encode($result));

?>