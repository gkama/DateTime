using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using System.Net;

using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace DateInfo
{
    public class Function
    {
        /// <summary>
        /// A simple function that takes a string and does a ToUpper
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<APIGatewayProxyResponse> FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                //Date info
                DateInfo di = new DateInfo(request.QueryStringParameters["date"], context);
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.OK,
                    Body = await di.getDateInfo(),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" }, { "Access-Control-Allow-Origin", "*" } }
                };

                //Return the response
                return response;
            }
            //If error
            catch (Exception e)
            {
                var response = new APIGatewayProxyResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Body = string.Format("Error getting date info. Correct date format is: yyyyMMdd ({0} for example).{1}Error Message: {2}", 
                           DateTime.Now.ToString("yyyyMMdd"), Environment.NewLine, e.Message),
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" }, { "Access-Control-Allow-Origin", "*" } }
                };

                //Return the response
                return response;
            }
        }
    }
}
