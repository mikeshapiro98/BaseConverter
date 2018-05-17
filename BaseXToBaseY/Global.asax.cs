using System;

namespace BaseXToBaseY
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
        }

        void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            var innerException = ex.InnerException;
            Response.Write("The following error has occured: " + innerException.Message);
            Server.ClearError();
        }
    }
}