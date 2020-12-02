using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace NativeApi.Domain
{
    class SabreCommandRequest
    {

       public static SabreCommand.SabreCommandLLSRQ getRequest(String token,String command)
        {
            SabreCommand.SabreCommandLLSRQ sabrecommand_request = new SabreCommand.SabreCommandLLSRQ();

            sabrecommand_request.Version = "1.8.1";
            SabreCommand.SabreCommandLLSRQRequest request = new SabreCommand.SabreCommandLLSRQRequest(); 

            request.Output = new SabreCommand.SabreCommandLLSRQRequestOutput();
            request.Output = SabreCommand.SabreCommandLLSRQRequestOutput.SCREEN; 
            request.CDATA = true;

            request.HostCommand = command;
            sabrecommand_request.Request = request; 
         

            return sabrecommand_request ; 
        }

        public static SabreCommand.MessageHeader returnMessageHeader()
        {


            SabreCommand.MessageHeader message_header = new SabreCommand.MessageHeader();

            DateTime now = DateTime.Now;

            // from 
            SabreCommand.From from = new SabreCommand.From();
            SabreCommand.PartyId fromPartyId = new SabreCommand.PartyId();
            SabreCommand.PartyId[] fromPartyIdArr = new SabreCommand.PartyId[1];
            fromPartyId.Value = "WebServiceClient";
            fromPartyIdArr[0] = fromPartyId;
            from.PartyId = fromPartyIdArr;
            message_header.From = from;

            // to 

            SabreCommand.To to = new SabreCommand.To();
            SabreCommand.PartyId toPartyId = new SabreCommand.PartyId();
            SabreCommand.PartyId[] toPartyIdArr = new SabreCommand.PartyId[1];
            toPartyId.Value = "WebServiceSupplier";
            toPartyIdArr[0] = toPartyId;
            to.PartyId = toPartyIdArr;
            message_header.To = to;

            message_header.Action = "SabreCommandLLSRQ";
            message_header.CPAId = "IPCC";
            SabreCommand.Service service = new SabreCommand.Service();
            service.Value = "SabreCommandLLSRQ";

            message_header.Service = service;
            SabreCommand.MessageData message_data = new SabreCommand.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 


            message_header.ConversationId = now.ToLongDateString();

            return message_header;
        }

        public static SabreCommand.Security returnSecurityHeader(string token)
        {

            SabreCommand.Security security = new SabreCommand.Security();

            security.BinarySecurityToken = token;


            return security;
        }


        public static SabreCommand.SabreCommandLLSPortTypeClient returnClient(string token)
        {

            SabreCommand.SabreCommandLLSPortTypeClient client = new SabreCommand.SabreCommandLLSPortTypeClient();

            string endpoint = Util.ReadToken(token);
            client.Endpoint.Address = new EndpointAddress(endpoint);


            return client;
        }


    }
}
