using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Microsoft.Xrm.Client;
using Microsoft.Xrm.Client.Services;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Crm.Sdk.Messages;
namespace DiscoveryServiceWebAPI.Controllers
{
    public class TestContactController : ApiController
    {
        public string Get()
        {

            //Might need to call login fucntion first
            CrmConnection con = new CrmConnection("CRM");
            IOrganizationService serviceTest = new OrganizationService(con);


            QueryExpression query = new QueryExpression("contact");
            query.ColumnSet = new ColumnSet(new string[] { "contactid", "firstname", "lastname", "emailaddress1", "address1_line1", "address1_stateorprovince", "address1_postalcode" });

            EntityCollection result = serviceTest.RetrieveMultiple(query);

            XmlSerializer serializer = new XmlSerializer(typeof(EntityCollection));
            StringBuilder stringBuilder = new StringBuilder();

            XElement xmlElements;
            foreach (Entity test in result.Entities)
            {
                xmlElements = new XElement("Contact", test.Attributes.Select(i => new XElement(i.Key.ToString(), i.Value.ToString())));
                stringBuilder.Append(xmlElements.ToString());
                stringBuilder.Append("\n\n");

            }

            return stringBuilder.ToString();
        }
    }
}
