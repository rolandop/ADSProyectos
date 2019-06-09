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
        public static ObjectResult ADOk(this ControllerBase services)
        {
            return services.ADSOk(null, RequestMessages.OK, 200);
        }

        public static ObjectResult ADSOk(this ControllerBase services, Object data)
        {
            try
            {
                return services.ADSOk(data, RequestMessages.OK, 200);
            }
            catch (Exception e)
            {
                return services.StatusCode(500, e.Message);
            }

        }


        public static ObjectResult ADSOk(this ControllerBase services, Object data, String mesagge)
        {
            return services.ADSOk(data, mesagge, 200);
        }
        public static ObjectResult ADSOk(this ControllerBase services, Object data, String mesagge, int code)
        {
            try
            {
                return services.Ok(new ResultModel
                {
                    Code = code,
                    Message = mesagge,
                    Data = data
                });
            }
            catch (Exception e)
            {
                return services.StatusCode(500, e.Message);
            }
        }
        #endregion

        #region NotFound
        public static NotFoundObjectResult ADSNotFound(this ControllerBase services)
        {
            return services.ADSNotFound(RequestMessages.NOT_FOUND, 404);
        }
        public static NotFoundObjectResult ADSNotFound(this ControllerBase services, String mesagge)
        {
            return services.ADSNotFound(mesagge, 404);
        }
        public static NotFoundObjectResult ADSNotFound(this ControllerBase services, String mesagge, int code)
        {
            return services.NotFound(new ResultModel
            {
                Code = code,
                Message = mesagge,
                Data = null
            });
        }
        #endregion

        #region BadRequest
        public static BadRequestResult ADSBadRequest(this ControllerBase services)
        {
            return services.BadRequest();
        }
        public static BadRequestObjectResult ADSBadRequest(this ControllerBase services, String mesagge)
        {
            return services.BadRequest(new ResultModel
            {
                Code = 55,
                Message = mesagge,
                Data = null
            });
        }

        #endregion
    }
}
