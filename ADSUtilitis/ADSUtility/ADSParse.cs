using ADSUtilities.Util;
using Newtonsoft.Json;
using System;

namespace ADSUtilities
{
    public abstract class Response
    {
        public static Object Code200(Object o,Boolean serializer =true)
        {
            var resul = new Structure{
                Code=200,
                Message="Ok",
                Data = o
            };
            if(serializer == false)
            {
                return resul;
            }
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code200(Object o, String Message, Boolean serializer = true)
        {
            var resul = new Structure
            {
                Code = 200,
                Message = Message,
                Data = o
            };
            if (serializer == false)
            {
                return resul;
            }
            return JsonConvert.SerializeObject(resul); ;
        }
        public static Object Code200(Object o,int Code, String Message, Boolean serializer = true)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = o
            };
            if (serializer == false)
            {
                return resul;
            }
            return resul;
            //return JsonConvert.SerializeObject(resul); ;
        }
        public static Object Code201(Object o, Boolean serializer = true)
        {
            var resul = new Structure
            {
                Code = 201,
                Message = "Created",
                Data = o
            };
            if (serializer == false)
            {
                return resul;
            }
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code201(Object o, String Message, Boolean serializer = true)
        {
            var resul = new Structure
            {
                Code = 201,
                Message = Message,
                Data = o
            };
            if (serializer == false)
            {
                return resul;
            }
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code201(Object o,int Code, String Message, Boolean serializer = true)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = o
            };
            if (serializer == false)
            {
                return resul;
            }
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code400()
        {
            var resul = new Structure
            {
                Code = 400,
                Message = "Bad Request",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static Object Code400(String Message)
        {
            var resul = new Structure
            {
                Code = 400,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code400(int Code,String Message)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }


        public static Object Code401()
        {
            var resul = new Structure
            {
                Code = 401,
                Message = "Unauthorized",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);

        }
        public static Object Code401(String Message)
        {
            var resul = new Structure
            {
                Code = 401,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code401(int Code,String Message)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }

        public static Object Code403()
        {
            var resul = new Structure
            {
                Code = 403,
                Message = "Forbidden",
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }
        
        public static Object Code403(String Message)
        {
            var resul = new Structure
            {
                Code = 403,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code403(int Code,String Message)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }

        public static Object Code404()
        {
            var resul = new Structure
            {
                Code = 404,
                Message = "Not Found",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static Object Code404(String Message)
        {
            var resul = new Structure
            {
                Code = 404,
                Message = Message,
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }

        public static Object Code404(int Code,String Message)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static Object Code500()
        {
            var resul = new Structure
            {
                Code = 500,
                Message = "Internal Server Error",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code500(String Message)
        {
            var resul = new Structure
            {
                Code = 500,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static Object Code500(int Code, String Message)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static Object Code504()
        {
            var resul = new Structure
            {
                Code = 504,
                Message = "Gateway Timeout",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static Object Code504(String Message)
        {
            var resul = new Structure
            {
                Code = 504,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static Object Code504(int Code ,String Message)
        {
            var resul = new Structure
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
    }
}
