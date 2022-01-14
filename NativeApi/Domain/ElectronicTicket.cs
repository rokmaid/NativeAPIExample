using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace NativeApi.Domain
{
    class ElectronicTicket
    {

        public static GetElectronicDocument.MessageHeader returnMessageHeader()
        {


            GetElectronicDocument.MessageHeader message_header = new GetElectronicDocument.MessageHeader();

            DateTime now = DateTime.Now;

            // from 
            GetElectronicDocument.From from = new GetElectronicDocument.From();
            GetElectronicDocument.PartyId fromPartyId = new GetElectronicDocument.PartyId();
            GetElectronicDocument.PartyId[] fromPartyIdArr = new GetElectronicDocument.PartyId[1];
            fromPartyId.Value = "WebServiceClient";
            fromPartyIdArr[0] = fromPartyId;
            from.PartyId = fromPartyIdArr;
            message_header.From = from;

            // to 

            GetElectronicDocument.To to = new GetElectronicDocument.To();
            GetElectronicDocument.PartyId toPartyId = new GetElectronicDocument.PartyId();
            GetElectronicDocument.PartyId[] toPartyIdArr = new GetElectronicDocument.PartyId[1];
            toPartyId.Value = "WebServiceSupplier";
            toPartyIdArr[0] = toPartyId;
            to.PartyId = toPartyIdArr;
            message_header.To = to;

            message_header.Action = "TravelItineraryReadRQ";
            message_header.CPAId = "IPCC";
            GetElectronicDocument.Service service = new GetElectronicDocument.Service();
            service.Value = "TravelItineraryReadRQ";



            message_header.Service = service;
            GetElectronicDocument.MessageData message_data = new GetElectronicDocument.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 


            message_header.ConversationId = now.ToLongDateString();

            return message_header;
        }

        public static GetElectronicDocument.GetElectronicDocumentRQ getPayload(string tktnum)
        {

            GetElectronicDocument.GetElectronicDocumentRQ RQ = new GetElectronicDocument.GetElectronicDocumentRQ();

            RQ.Version = "1.0.0"; 

            GetElectronicDocument.STL_HeaderRQ STL_Header_rq = new GetElectronicDocument.STL_HeaderRQ();

            RQ.STL_HeaderRQ = STL_Header_rq;

            GetElectronicDocument.POS pos = new GetElectronicDocument.POS() ;

            RQ.POS = pos;

            GetElectronicDocument.ElectronicDocumentSearchParameters search_parameters = new GetElectronicDocument.ElectronicDocumentSearchParameters();

            search_parameters.DocumentNumber = tktnum;
            RQ.SearchParameters = search_parameters; 

            return RQ; 

        }

        public static GetElectronicDocument.Security returnSecurityHeader(string token)
        {

            GetElectronicDocument.Security security = new GetElectronicDocument.Security();

            GetElectronicDocument.SecurityBinarySecurityToken security_token = new GetElectronicDocument.SecurityBinarySecurityToken();


            security_token.Value = token;
            security.BinarySecurityToken = security_token;



            return security;
        }


        public static GetElectronicDocument.GetElectronicDocumentPortTypeClient returnClient(string token)
        {

            GetElectronicDocument.GetElectronicDocumentPortTypeClient client = new GetElectronicDocument.GetElectronicDocumentPortTypeClient();

            string endpoint = Util.ReadToken(token);
            client.Endpoint.Address = new EndpointAddress(endpoint);


            return client;
        }
    }
}
