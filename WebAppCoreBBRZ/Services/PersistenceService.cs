using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCoreBBRZ.Services
{
    public static class PersistenceService
    {
        public static void Init()
        {
            Debug.WriteLine("*** Hello from the other side: PersistenceService");
        }

        public static bool SaveJSON()
        {
            try
            {
                JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
                var ourJSONData = javascriptSerializer.Serialize(WebAppCoreBBRZ.Controllers.AdminController.adminListe);

                using (StreamWriter sw = new StreamWriter(
                    WebAppCoreBBRZ.Controllers.PersistenceController.filename))
                {
                    sw.WriteLine(ourJSONData);
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            
        }

        public static bool LoadJSON()
        {
            try
            {
                JavaScriptSerializer javascriptSerializer = new JavaScriptSerializer();
                string ourJSONData = null;

                if (!System.IO.File.Exists(WebAppCoreBBRZ.Controllers.PersistenceController.filename))
                    return false;

                using (StreamReader sw = new StreamReader(
                    WebAppCoreBBRZ.Controllers.PersistenceController.filename))
                {
                    ourJSONData = sw.ReadToEnd();
                }

                dynamic listeVonAdmins = javascriptSerializer.
                    Deserialize<List<WebAppCoreBBRZ.Models.Admin>>(ourJSONData);
                WebAppCoreBBRZ.Controllers.AdminController.adminListe = listeVonAdmins;

                return true;

            } 
            catch (Exception e)
            {
                // throw new FieldAccessException();
                return false;
                
            }
            
        }

    }
}
