using Microsoft.AspNetCore.Http;
using MvcCoreSession.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreSession.Extensions
{
    /*IMPORTANTE QUE LA CLASE SEA STATIC*/
    public static class SessionExtension
    {
        //QUE NECESITAMOS PARA PODER CREAR UN METODO SETOBJECT EN SESSION
        //HttpContext.Session.SetString("KEY", value);
        //NECESITAMOS EN EL METODO EL TIPO DE OBJETO QUE DESEAMOS EXTENDER
        //HttpContext.Session.SetObject("KEY",objeto);
        /*importante el ISession*/
        public static void SetObject(this ISession session, string key,object value) 
        {
            string data = HelperSession.SerializeObject(value);
            session.SetString(key, data);
        }
        //HttpContext.Session.GetObject<T>("Key");
        public static T GetObject<T>(this ISession session, string key) 
        {
            string data = session.GetString(key);
            //QUE SUCEDE EN NUESTRA APP CUANDO BUSCAMOS UNA CLAVE QUE NO EXISTE?  
            //SI NO ENCUENTRA LA KEY DEVOLVEMOS UN NULL
            if (data == null)
            {
                //SE DEVUELVE EL VALOR POR DEFECTO DEL GENERICO
                return default(T);
            }
            else 
            {
                return HelperSession.DeserializeObject<T>(data);
            }
        }
    }
}
