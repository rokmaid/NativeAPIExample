using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel; 

namespace NativeApi.Domain
{
    class TIR
    {


        public static TravelItineraryRead.TravelItineraryReadRQ buildRequestForTravelItinRead()
        {

            TravelItineraryRead.TravelItineraryReadRQ request = new TravelItineraryRead.TravelItineraryReadRQ();

            TravelItineraryRead.TravelItineraryReadRQMessagingDetails message_datails = new TravelItineraryRead.TravelItineraryReadRQMessagingDetails();

            string[] subjareas = { "FULL" };
            message_datails.SubjectAreas = subjareas;


            request.Version = "3.10.0";
            request.MessagingDetails = message_datails;

            return request;
        }


        public static TravelItineraryRead.MessageHeader returnMessageHeader()
        {


            TravelItineraryRead.MessageHeader message_header = new TravelItineraryRead.MessageHeader();

            DateTime now = DateTime.Now;

            // from 
            TravelItineraryRead.From from = new TravelItineraryRead.From();
            TravelItineraryRead.PartyId fromPartyId = new TravelItineraryRead.PartyId();
            TravelItineraryRead.PartyId[] fromPartyIdArr = new TravelItineraryRead.PartyId[1];
            fromPartyId.Value = "WebServiceClient";
            fromPartyIdArr[0] = fromPartyId;
            from.PartyId = fromPartyIdArr;
            message_header.From = from;

            // to 

            TravelItineraryRead.To to = new TravelItineraryRead.To();
            TravelItineraryRead.PartyId toPartyId = new TravelItineraryRead.PartyId();
            TravelItineraryRead.PartyId[] toPartyIdArr = new TravelItineraryRead.PartyId[1];
            toPartyId.Value = "WebServiceSupplier";
            toPartyIdArr[0] = toPartyId;
            to.PartyId = toPartyIdArr;
            message_header.To = to;

            message_header.Action = "TravelItineraryReadRQ";
            message_header.CPAId = "IPCC";
            TravelItineraryRead.Service service = new TravelItineraryRead.Service();
            service.Value = "TravelItineraryReadRQ";



            message_header.Service = service;
            TravelItineraryRead.MessageData message_data = new TravelItineraryRead.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 


            message_header.ConversationId = now.ToLongDateString();

            return message_header; 
        }

        public static TravelItineraryRead.Security1 returnSecurityHeader(string token )
        {

            TravelItineraryRead.Security1 security = new TravelItineraryRead.Security1();

            security.BinarySecurityToken = token; 

            
            return security; 
        }

        public static TravelItineraryRead.TravelItineraryReadPortTypeClient returnClient(string token)
        {

            TravelItineraryRead.TravelItineraryReadPortTypeClient client = new TravelItineraryRead.TravelItineraryReadPortTypeClient();

            string endpoint = Util.ReadToken(token);
            client.Endpoint.Address = new EndpointAddress(endpoint); 


            return client; 
        }



    }
}
