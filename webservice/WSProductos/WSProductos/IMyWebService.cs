using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WSProductos
{
    [DataContract]
    public class UsuarioInfo
    {
        [DataMember]
        public String nombre { set; get; }

        [DataMember]
        public String correo { set; get; }

        [DataMember]
        public String rol { set; get; }

        [DataMember]
        public String telefono { set; get; }
    };

    [DataContract]
    public class Respuesta
    {
        [DataMember]
        public int Code { set; get; }

        [DataMember]
        public String Message { set; get; }

        [DataMember]
        public String Data { set; get; }

        [DataMember]
        public String Status { set; get; }
    }
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IMyWebService" en el código y en el archivo de configuración a la vez.
    [ServiceContract,XmlSerializerFormat(Style =OperationFormatStyle.Rpc,
                                            Use =OperationFormatUse.Encoded)]
    public interface IMyWebService
    {

        [OperationContract]
        Respuesta UpdateUser(String User, String Pass, String oldUser, String newUser, String newPass);

        [OperationContract]
        Respuesta SetUserInfo(String User, String Pass, String SearchedUser, String UserInfoJson);

        [OperationContract]
        Respuesta UpdateUserInfo(String User, String Pass, String SearchedUser, String UserInfoJson);

        [OperationContract]
        Respuesta SetUser(String User, String Pass, String SearchedUser, String UserJson);

    }
}
