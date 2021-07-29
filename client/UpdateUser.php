<?php
	$servicio="http://localhost:59754/MyWebService.svc?wsdl";

	$parametros = array();
	$user=$_POST['us'];
	$pass=$_POST['passw'];
	$oldUser=$_POST['oldUser'];
	$usern=$_POST['nUser'];
	$passn=$_POST['nPassword'];
	$client = new soapclient( $servicio, $parametros );

	$result = $client->UpdateUser($user,$pass,$oldUser,$usern,$passn);

	print_r(json_encode($result));
?>