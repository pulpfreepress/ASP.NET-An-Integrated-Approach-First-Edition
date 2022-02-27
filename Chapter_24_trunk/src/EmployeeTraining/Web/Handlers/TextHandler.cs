using System;
using System.Web;
using System.IO;

namespace Handlers {
    public class TextHandler : IHttpHandler {
        /// <summary>
        /// You will need to configure this handler in the web.config file of your 
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: http://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return false; }
        }

        public void ProcessRequest(HttpContext context) {
            HttpRequest request = context.Request;
            HttpResponse response = context.Response;
            
            FileStream fs = null;
            StreamReader reader = null;
            try {

                fs = new FileStream(request.PhysicalPath, FileMode.Open);
                reader = new StreamReader(fs);
                string line;
                response.Write("<html>");
                response.Write("<body>");
                response.Write("<h1> " + request.PhysicalPath + "</h1><br>");
                while ((line = reader.ReadLine()) != null) {
                    response.Write("<p>" + line + "</p>");
                }
                response.Write("</body>");
                response.Write("</html>");
                response.Flush();

            }
            catch (Exception ) {
                response.Write("<html>");
                response.Write("<body>");
                response.Write("<h1>Cannot open the requested file!</h1>");
                response.Write("</body>");
                response.Write("</html>");
                response.Flush();
            }
            finally {
                if (fs != null) fs.Close();
                if (reader != null) reader.Close();
            }


        }

        #endregion
    }
}
