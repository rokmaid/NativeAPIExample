using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace NativeApi.Domain
{
    class DesignatePrinterService
    {


        public static DesignatePrinter.DesignatePrinterRQ getRequest(string token , String profilenum)
        {
            DesignatePrinter.DesignatePrinterRQ req = new DesignatePrinter.DesignatePrinterRQ();

            req.ReturnHostCommand = true; 
            req.Version= "2.0.1";

            DesignatePrinter.DesignatePrinterRQProfile profile = new DesignatePrinter.DesignatePrinterRQProfile() ;

            profile.Number = profilenum;

            req.Profile = profile; 

            return req; 
        }

        public static DesignatePrinter.MessageHeader returnMessageHeader()
        {
         DesignatePrinter.MessageHeader message_header = new DesignatePrinter.MessageHeader();

            DateTime now = DateTime.Now;

            // from 
            DesignatePrinter.From from = new DesignatePrinter.From();
            DesignatePrinter.PartyId fromPartyId = new DesignatePrinter.PartyId();
            DesignatePrinter.PartyId[] fromPartyIdArr = new DesignatePrinter.PartyId[1];
            fromPartyId.Value = "WebServiceClient";
            fromPartyIdArr[0] = fromPartyId;
            from.PartyId = fromPartyIdArr;
            message_header.From = from;

            // to 

            DesignatePrinter.To to = new DesignatePrinter.To();
            DesignatePrinter.PartyId toPartyId = new DesignatePrinter.PartyId();
            DesignatePrinter.PartyId[] toPartyIdArr = new DesignatePrinter.PartyId[1];
            toPartyId.Value = "WebServiceSupplier";
            toPartyIdArr[0] = toPartyId;
            to.PartyId = toPartyIdArr;
            message_header.To = to;

            message_header.Action = "DesignatePrinterLLSRQ";
            message_header.CPAId = "IPCC";
            DesignatePrinter.Service service = new DesignatePrinter.Service();
            service.Value = "DesignatePrinterLLSRQ";

             message_header.Service = service;
            DesignatePrinter.MessageData message_data = new DesignatePrinter.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 


            message_header.ConversationId = now.ToLongDateString();

            return message_header;


        }

        public static DesignatePrinter.Security1 returnSecurityHeader(string token)
        {

            DesignatePrinter.Security1 security = new DesignatePrinter.Security1();

           
            security.BinarySecurityToken = token;


            return security;
        }

        public static DesignatePrinter.DesignatePrinterPortTypeClient returnClient(string token)
        {

           DesignatePrinter.DesignatePrinterPortTypeClient client = new DesignatePrinter.DesignatePrinterPortTypeClient();

            string endpoint = Util.ReadToken(token);
            client.Endpoint.Address = new EndpointAddress(endpoint);


            return client;
        }

    }
}
