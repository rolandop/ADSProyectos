using ADSUtilities.Models;
using Newtonsoft.Json;
using System;

namespace ADSUtilities
{
    public abstract class Response
    {
        public static object Code200(Object o,Boolean serializer =true)
        {
            var resul = new ResultModel{
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
        public static object Code200(Object o, String Message, Boolean serializer = true)
        {
            var resul = new ResultModel
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
        public static object Code200(Object o,int Code, String Message, Boolean serializer = true)
        {
            var resul = new ResultModel
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
        public static object Code201(Object o, Boolean serializer = true)
        {
            var resul = new ResultModel
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
        public static object Code201(Object o, String Message, Boolean serializer = true)
        {
            var resul = new ResultModel
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
        public static object Code201(Object o,int Code, String Message, Boolean serializer = true)
        {
            var resul = new ResultModel
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
        public static object Code400()
        {
            var resul = new ResultModel
            {
                Code = 400,
                Message = "Bad Request",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static object Code400(String Message)
        {
            var resul = new ResultModel
            {
                Code = 400,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static object Code400(int Code,String Message)
        {
            var resul = new ResultModel
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }


        public static object Code401()
        {
            var resul = new ResultModel
            {
                Code = 401,
                Message = "Unauthorized",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);

        }
        public static object Code401(String Message)
        {
            var resul = new ResultModel
            {
                Code = 401,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static object Code401(int Code,String Message)
        {
            var resul = new ResultModel
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }

        public static object Code403()
        {
            var resul = new ResultModel
            {
                Code = 403,
                Message = "Forbidden",
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }
        
        public static object Code403(String Message)
        {
            var resul = new ResultModel
            {
                Code = 403,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static object Code403(int Code,String Message)
        {
            var resul = new ResultModel
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }

        public static object Code404()
        {
            var resul = new ResultModel
            {
                Code = 404,
                Message = "Not Found",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static object Code404(String Message)
        {
            var resul = new ResultModel
            {
                Code = 404,
                Message = Message,
                Data = null
            };
            return resul;
            // return JsonConvert.SerializeObject(resul);
        }

        public static object Code404(int Code,String Message)
        {
            var resul = new ResultModel
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static object Code500()
        {
            var resul = new ResultModel
            {
                Code = 500,
                Message = "Internal Server Error",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static object Code500(String Message)
        {
            var resul = new ResultModel
            {
                Code = 500,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }
        public static object Code500(int Code, String Message)
        {
            var resul = new ResultModel
            {
                Code = Code,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static object Code504()
        {
            var resul = new ResultModel
            {
                Code = 504,
                Message = "Gateway Timeout",
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static object Code504(String Message)
        {
            var resul = new ResultModel
            {
                Code = 504,
                Message = Message,
                Data = null
            };
            return resul;
            //return JsonConvert.SerializeObject(resul);
        }

        public static object Code504(int Code ,String Message)
        {
            var resul = new ResultModel
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
