using ADSUtilities.Logger;
using ADSUtilities.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADSUtilities
{
    public static class ResponseExtensions
    {

        #region ok
        public static ObjectResult ADSOk(this ControllerBase services)
        {
            return services.ADSOk(null);
        }

        public static ObjectResult ADSOk(this ControllerBase services, object data)
        {
            return services.ADSOk(data, ResponseMessages.OK);
        }

        public static ObjectResult ADSOk(this ControllerBase services, object data, string message)
        {
            return services.ADSOk(data, message, 0);
        }

        public static ObjectResult ADSOk(this ControllerBase services, object data, string message, int code)
        {
            try
            {
                return services.Ok(new ResultModel
                {
                    Error = code,
                    Msg = message,
                    Data = data,
                    Request = RequestModel.GetRequest(services)
                });
            }
            catch (Exception e)
            {
                return services.ADSInternalError(e.Message);
            }
        }

        #endregion

        #region NotFound

        public static ObjectResult ADSNotFound(this ControllerBase services)
        {
            return services.ADSNotFound(ResponseMessages.NOT_FOUND);
        }
        public static ObjectResult ADSNotFound(this ControllerBase services, string mesagge)
        {
            return services.ADSNotFound(mesagge, null);
        }

        public static ObjectResult ADSNotFound(this ControllerBase services, string mesagge, object data)
        {
            return services.ADSNotFound(mesagge, data, 404);
        }

        public static ObjectResult ADSNotFound(this ControllerBase services, string message, object data, int code)
        {
            try
            {
                return services.NotFound(new ResultModel
                {
                    Error = code,
                    Msg = message,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return services.ADSInternalError(e.Message);
            }
        }
        #endregion

        #region BadRequest
        public static ObjectResult ADSBadRequest(this ControllerBase services)
        {
            return services.ADSBadRequest(ResponseMessages.BAD_REQUEST);
        }
        public static ObjectResult ADSBadRequest(this ControllerBase services, string mesagge)
        {
            return services.ADSBadRequest(mesagge, null);
        }

        public static ObjectResult ADSBadRequest(this ControllerBase services, string mesagge, object data)
        {
            return services.ADSBadRequest(mesagge, data, 400);
        }

        public static ObjectResult ADSBadRequest(this ControllerBase services, string message, object data, int code)
        {
            try
            {
                return services.BadRequest(new ResultModel
                {
                    Error = code,
                    Msg = message,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return services.ADSInternalError(e.Message);
            }
        }

        #endregion

        #region IntrnalError
        public static ObjectResult ADSInternalError(this ControllerBase services)
        {
            return services.ADSInternalError(ResponseMessages.INTERNAL_ERROR);
        }

        public static ObjectResult ADSInternalError(this ControllerBase services, string message)
        {
            return services.ADSInternalError(message, null);
        }

        public static ObjectResult ADSInternalError(this ControllerBase services, string message, object data)
        {
            return services.ADSInternalError(message, data, 500);
        }

        public static ObjectResult ADSInternalError(this ControllerBase services, string message, int code)
        {
            return services.ADSInternalError(message, null, code);
        }

        public static ObjectResult ADSInternalError(this ControllerBase services, string message, object data, int code)
        {
            try
            {
                return services.StatusCode(code, new ResultModel
                {
                    Error = code,
                    Msg = message,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return services.StatusCode(code, new ResultModel
                {
                    Error = code,
                    Msg = e.Message,
                    Data = null
                }); ;
            }
        }

        #endregion

        #region StatusCode
        public static ObjectResult ADSStatusCode(this ControllerBase services)
        {
            return services.ADSStatusCode(0);
        }
        public static ObjectResult ADSStatusCode(this ControllerBase services, int code)
        {
            return services.ADSStatusCode(code, "");
        }

        public static ObjectResult ADSStatusCode(this ControllerBase services, int code, string message)
        {
            return services.ADSStatusCode(code, message, null);
        }

        public static ObjectResult ADSStatusCode(this ControllerBase services, int code, string message, object data)
        {
            try
            {
                return services.StatusCode(code, new ResultModel
                {
                    Error = code,
                    Msg = message,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return services.StatusCode(code, new ResultModel
                {
                    Error = code,
                    Msg = e.Message,
                    Data = null
                }); ;
            }
        }

        #endregion
    }
}
