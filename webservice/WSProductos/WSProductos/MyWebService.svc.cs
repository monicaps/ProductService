using System;
using System.Text;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

namespace WSProductos
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "MyWebService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione MyWebService.svc o MyWebService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class MyWebService : IMyWebService
    {
        public Respuesta SetUser(string User, string Pass, string SearchedUser, string UserJson)
        {
            IFirebaseConfig con = new FirebaseConfig
            {
                AuthSecret = "6HAUTILDmOI3ouaqCZyXf5E3QTxvZkXlBED79vr0",
                BasePath = "https://productws-dcfb7.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(con);

            Respuesta respuesta=null;
            string passUser = '"' + GetMD5(Pass) + '"';
            string passUs = GetMD5(Pass);

            FirebaseResponse responseUsuarios = client.Get(@"usuarios/" + User);
                FirebaseResponse r = client.Get(@"usuarios/" + SearchedUser);
                if (responseUsuarios != null)
                {
                    string pUser = responseUsuarios.Body;
                    FirebaseResponse responseInfoUser = client.Get(@"usuarios_info/" + User);
                    dynamic jsonInfoUser = JsonConvert.DeserializeObject(responseInfoUser.Body);
                    string rol = jsonInfoUser["rol"];
                    if (string.Equals(pUser, passUser) || string.Equals(pUser, passUs))
                    {
                        if (string.Equals(rol, "rh"))
                        {
                            if (string.Equals(r.Body, "null"))
                            {
                                string esquema = @"{" + '"' + SearchedUser + '"' + ":{'type':'string'}}";
                                JSchema schema = JSchema.Parse(esquema);
                                try
                                {
                                    JObject ObjUser = JObject.Parse(UserJson);
                                    if (ObjUser.IsValid(schema))
                                    {
                                        string data = "";
                                        JsonTextReader reader = new JsonTextReader(new StringReader(UserJson));
                                        while (reader.Read())
                                        {
                                            if (reader.Value != null)
                                            {
                                                data = reader.Value.ToString();
                                            }
                                            else
                                            {
                                                FirebaseResponse r304 = client.Get(@"respuestas/" + 304);
                                                respuesta = new Respuesta() { Code = 304, Message = r304.Body, Data = "", Status = "Error" };
                                            }
                                        }
                                        DateTime time = DateTime.Now;
                                        var post = client.Set(@"usuarios/"+SearchedUser,GetMD5(data));
                                        FirebaseResponse r404 = client.Get(@"respuestas/" + 404);
                                        respuesta = new Respuesta() { Code = 404, Message = r404.Body, Data =time.ToString(), Status = "Success" };
                                    }

                                }
                                catch
                                {
                                    FirebaseResponse r305 = client.Get(@"respuestas/" + 305);
                                    respuesta = new Respuesta() { Code = 305, Message = r305.Body, Data = "", Status = "Error" };
                                }
                            }
                            else
                            {
                                FirebaseResponse r508 = client.Get(@"respuestas/" + 508);
                                respuesta = new Respuesta() { Code = 508, Message = r508.Body, Data = "", Status = "Error" };
                            }
                            
                        }
                        else
                        {
                            FirebaseResponse r504 = client.Get(@"respuestas/" + 504);
                            respuesta = new Respuesta() { Code = 504, Message = r504.Body, Data = "", Status = "Error" };
                        }

                    }
                    else
                    {
                        FirebaseResponse r501 = client.Get(@"respuestas/" + 501);
                        respuesta = new Respuesta() { Code = 501, Message = r501.Body, Data = "", Status = "Error" };
                    }
                }
                else
                {
                    FirebaseResponse r500 = client.Get(@"respuestas/" + 500);
                    respuesta = new Respuesta() { Code = 500, Message = r500.Body, Data = "", Status = "Error" };
                }
            
            return respuesta;
        }

        public Respuesta SetUserInfo(string User, string Pass, string SearchedUser, string UserInfoJson)
        {

            IFirebaseConfig con = new FirebaseConfig
            {
                AuthSecret = "6HAUTILDmOI3ouaqCZyXf5E3QTxvZkXlBED79vr0",
                BasePath = "https://productws-dcfb7.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(con);

            Respuesta respuesta = null;
            string passUser = '"' + GetMD5(Pass) + '"';
            string passUs = GetMD5(Pass);

            FirebaseResponse responseUsuarios = client.Get(@"usuarios/" + User);
            FirebaseResponse r1 = client.Get(@"usuarios/" + SearchedUser);
            FirebaseResponse r = client.Get(@"usuarios_info/" + SearchedUser);

            if (responseUsuarios != null)
            {
                string pUser = responseUsuarios.Body;
                FirebaseResponse responseInfoUser = client.Get(@"usuarios_info/" + User);
                dynamic jsonInfoUser = JsonConvert.DeserializeObject(responseInfoUser.Body);
                string rol = jsonInfoUser["rol"];

                if (string.Equals(pUser, passUser) || string.Equals(pUser, passUs))
                {
                    if (string.Equals(rol, "rh"))
                    {
                        if (string.Equals(r1.Body, "null"))
                        {
                            FirebaseResponse r505 = client.Get(@"respuestas/" + 505);
                            respuesta = new Respuesta() { Code = 505, Message = r505.Body, Data = "", Status = "Error" };
                        }
                        else
                        {
                            if (string.Equals(r.Body, "null"))
                            {
                                string esquema = @"{'nombre':{'type':'string'},
                                                'correo':{'type':'string'},
                                                'rol':{'type':'string'},
                                                'telefono':{'type':'string'}
                                            }";

                                JSchema schema = JSchema.Parse(esquema);
                                try
                                {
                                    JObject ObjUser = JObject.Parse(UserInfoJson);

                                    if (ObjUser.IsValid(schema))
                                    {
                                        dynamic jsonInfo = JsonConvert.DeserializeObject(UserInfoJson);
                                        string nombre, correo, rolN, telefono;
                                        nombre = jsonInfo["nombre"];
                                        correo = jsonInfo["correo"];
                                        rolN = jsonInfo["rol"];
                                        telefono = jsonInfo["telefono"];

                                        if (string.Equals(nombre, "") || string.Equals(correo, "") || string.Equals(rolN, "") || string.Equals(telefono, ""))
                                        {
                                            FirebaseResponse r304 = client.Get(@"respuestas/" + 304);
                                            respuesta = new Respuesta() { Code = 304, Message = r304.Body, Data = "", Status = "Error" };
                                        }
                                        else
                                        {
                                            UsuarioInfo info = new UsuarioInfo() { nombre = nombre, correo = correo, rol = rolN, telefono = telefono };
                                            DateTime time = DateTime.Now;
                                            var post = client.Set(@"usuarios_info/" + SearchedUser, info);
                                            FirebaseResponse r402 = client.Get(@"respuestas/" + 402);
                                            respuesta = new Respuesta() { Code = 402, Message = r402.Body, Data = time.ToString(), Status = "Success" };
                                            
                                        }
                                    }

                                }
                                catch
                                {
                                    FirebaseResponse r305 = client.Get(@"respuestas/" + 305);
                                    respuesta = new Respuesta() { Code = 305, Message = r305.Body, Data = "", Status = "Error" };
                                }
                            }
                            else
                            {
                                FirebaseResponse r506 = client.Get(@"respuestas/" + 506);
                                respuesta = new Respuesta() { Code = 506, Message = r506.Body, Data = "", Status = "Error" };
                            }
                        }

                    }
                    else
                    {
                        FirebaseResponse r504 = client.Get(@"respuestas/" + 504);
                        respuesta = new Respuesta() { Code = 504, Message = r504.Body, Data = "", Status = "Error" };
                    }

                }
                else
                {
                    FirebaseResponse r501 = client.Get(@"respuestas/" + 501);
                    respuesta = new Respuesta() { Code = 501, Message = r501.Body, Data = "", Status = "Error" };
                }
            }
            else
            {
                FirebaseResponse r500 = client.Get(@"respuestas/" + 500);
                respuesta = new Respuesta() { Code = 500, Message = r500.Body, Data = "", Status = "Error" };
            }

            return respuesta;
        }

        public Respuesta UpdateUser(string User, string Pass, string oldUser, string newUser, string newPass)
        {
            IFirebaseConfig con = new FirebaseConfig
            {
                AuthSecret = "6HAUTILDmOI3ouaqCZyXf5E3QTxvZkXlBED79vr0",
                BasePath = "https://productws-dcfb7.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(con);

            Respuesta respuesta = null;
            string passUser = '"' + GetMD5(Pass) + '"';
            string passUs = GetMD5(Pass);

            FirebaseResponse responseUsuarios = client.Get(@"usuarios/" + User);
            FirebaseResponse r = client.Get(@"usuarios/" + oldUser);
            if (responseUsuarios != null)
            {
                string pUser = responseUsuarios.Body;
                FirebaseResponse responseInfoUser = client.Get(@"usuarios_info/" + User);
                dynamic jsonInfoUser = JsonConvert.DeserializeObject(responseInfoUser.Body);
                string rol = jsonInfoUser["rol"];
                if (string.Equals(pUser, passUser) || string.Equals(pUser, passUs))
                {
                    if (string.Equals(rol, "rh"))
                    {
                        if (string.Equals(r.Body, "null"))
                        {
                            FirebaseResponse r505 = client.Get(@"respuestas/" + 505);
                            respuesta = new Respuesta() { Code = 505, Message = r505.Body, Data = "", Status = "Error" };
                            
                        }
                        else
                        {
                            bool valid = newUser.Contains(" ");
                            if (valid==true)
                            {
                                FirebaseResponse r503 = client.Get(@"respuestas/" + 503);
                                respuesta = new Respuesta() { Code = 503, Message = r503.Body, Data = "", Status = "Error" };
                            }
                            else
                            {
                                //expresion regular para que haya por lo menos 1 digito, una letra y el tamaño seade 8 a 16 caracteres
                                
                                Regex regex = new Regex(@"^(?=\w*\d)(?=\w*[a-z])\S{8,16}$");
                                bool match = regex.IsMatch(newPass);
                                if (match)
                                {
                                    FirebaseResponse respaldo = client.Get(@"usuarios_info/"+oldUser);
                                    if (respaldo!=null)
                                    {
                                        dynamic inf=JsonConvert.DeserializeObject(respaldo.Body);
                                        string nombre, correo, rolN, telefono;
                                        nombre = inf["nombre"];
                                        correo = inf["correo"];
                                        rolN = inf["rol"];
                                        telefono = inf["telefono"];
                                        UsuarioInfo UserRespaldo = new UsuarioInfo(){ nombre=nombre,correo=correo, rol=rolN,telefono=telefono };
                                        var infoNewUser = client.Set(@"usuarios_info/"+newUser,UserRespaldo);
                                        var deleteInfo = client.Delete(@"usuarios_info/" + oldUser);
                                    }
                                    var delete = client.Delete(@"usuarios/" + oldUser);
                                    DateTime time = DateTime.Now;
                                    var update = client.Set(@"usuarios/" + newUser, GetMD5(newPass)); 
                                    FirebaseResponse r401 = client.Get(@"respuestas/" + 401);
                                    respuesta = new Respuesta() { Code = 404, Message = r401.Body, Data = time.ToString(), Status = "Success" };
                                }
                                else
                                {
                                    FirebaseResponse r502 = client.Get(@"respuestas/" + 502);
                                    respuesta = new Respuesta() { Code = 502, Message = r502.Body, Data = "", Status = "Error" };
                                }
                            }
                        }

                    }
                    else
                    {
                        FirebaseResponse r504 = client.Get(@"respuestas/" + 504);
                        respuesta = new Respuesta() { Code = 504, Message = r504.Body, Data = "", Status = "Error" };
                    }

                }
                else
                {
                    FirebaseResponse r501 = client.Get(@"respuestas/" + 501);
                    respuesta = new Respuesta() { Code = 501, Message = r501.Body, Data = "", Status = "Error" };
                }
            }
            else
            {
                FirebaseResponse r500 = client.Get(@"respuestas/" + 500);
                respuesta = new Respuesta() { Code = 500, Message = r500.Body, Data = "", Status = "Error" };
            }

            return respuesta;
        }

        public Respuesta UpdateUserInfo(string User, string Pass, string SearchedUser, string UserInfoJson)
        {

            IFirebaseConfig con = new FirebaseConfig
            {
                AuthSecret = "6HAUTILDmOI3ouaqCZyXf5E3QTxvZkXlBED79vr0",
                BasePath = "https://productws-dcfb7.firebaseio.com/"
            };
            IFirebaseClient client = new FireSharp.FirebaseClient(con);

            Respuesta respuesta = null;
            string passUser = '"' + GetMD5(Pass) + '"';
            string passUs = GetMD5(Pass);

            FirebaseResponse responseUsuarios = client.Get(@"usuarios/" + User);
            FirebaseResponse r = client.Get(@"usuarios/" + SearchedUser);
            FirebaseResponse r1 = client.Get(@"usuarios_info/" + SearchedUser);

            if (responseUsuarios != null)
            {
                string pUser = responseUsuarios.Body;
                FirebaseResponse responseInfoUser = client.Get(@"usuarios_info/" + User);
                dynamic jsonInfoUser = JsonConvert.DeserializeObject(responseInfoUser.Body);
                string rol = jsonInfoUser["rol"];

                if (string.Equals(pUser, passUser) || string.Equals(pUser, passUs))
                {
                    if (string.Equals(rol, "rh"))
                    {
                        if (string.Equals(r.Body, "null"))
                        {
                            FirebaseResponse r505 = client.Get(@"respuestas/" + 505);
                            respuesta = new Respuesta() { Code = 505, Message = r505.Body, Data = "", Status = "Error" };
                        }
                        else
                        {
                            if (string.Equals(r1.Body, "null"))
                            {
                                FirebaseResponse r507 = client.Get(@"respuestas/" + 507);
                                respuesta = new Respuesta() { Code = 507, Message = r507.Body, Data = "", Status = "Error" };
                            }
                            else
                            {
                                string esquema = @"{'nombre':{'type':'string'},
                                                'correo':{'type':'string'},
                                                'rol':{'type':'string'},
                                                'telefono':{'type':'string'}
                                            }";

                                JSchema schema = JSchema.Parse(esquema);
                                try
                                {
                                    JObject ObjUser = JObject.Parse(UserInfoJson);

                                    if (ObjUser.IsValid(schema))
                                    {
                                        dynamic jsonInfo = JsonConvert.DeserializeObject(UserInfoJson);
                                        string nombre, correo, rolN, telefono;
                                        nombre = jsonInfo["nombre"];
                                        correo = jsonInfo["correo"];
                                        rolN = jsonInfo["rol"];
                                        telefono = jsonInfo["telefono"];

                                        if (string.Equals(nombre, "") || string.Equals(correo, "") || string.Equals(rolN, "") || string.Equals(telefono, ""))
                                        {
                                            FirebaseResponse r304 = client.Get(@"respuestas/" + 304);
                                            respuesta = new Respuesta() { Code = 304, Message = r304.Body, Data = "", Status = "Error" };
                                        }
                                        else
                                        {
                                            UsuarioInfo info = new UsuarioInfo() { nombre = nombre, correo = correo, rol = rolN, telefono = telefono };
                                            DateTime time = DateTime.Now;
                                            var update = client.Update(@"usuarios_info/" + SearchedUser, info);
                                            FirebaseResponse r403 = client.Get(@"respuestas/" + 403);
                                            respuesta = new Respuesta() { Code = 403, Message = r403.Body, Data = time.ToString(), Status = "Success" };
                                        }
                                    }

                                }
                                catch
                                {
                                    FirebaseResponse r305 = client.Get(@"respuestas/" + 305);
                                    respuesta = new Respuesta() { Code = 305, Message = r305.Body, Data = "", Status = "Error" };
                                }
                            }

                        }

                    }
                    else
                    {
                        FirebaseResponse r504 = client.Get(@"respuestas/" + 504);
                        respuesta = new Respuesta() { Code = 504, Message = r504.Body, Data = "", Status = "Error" };
                    }

                }
                else
                {
                    FirebaseResponse r501 = client.Get(@"respuestas/" + 501);
                    respuesta = new Respuesta() { Code = 501, Message = r501.Body, Data = "", Status = "Error" };
                }
            }
            else
            {
                FirebaseResponse r500 = client.Get(@"respuestas/" + 500);
                respuesta = new Respuesta() { Code = 500, Message = r500.Body, Data = "", Status = "Error" };
            }

            return respuesta;
        }

        private static string GetMD5(string str)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}
