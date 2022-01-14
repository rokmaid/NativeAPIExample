using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;


namespace NativeApi.Domain
{
    class SalesReport
    {


  
        public static DailySalesSummary.MessageHeader returnMessageHeader()
        {


            DailySalesSummary.MessageHeader message_header = new DailySalesSummary.MessageHeader();

            DateTime now = DateTime.Now;

            // from 
            DailySalesSummary.From from = new DailySalesSummary.From();
            DailySalesSummary.PartyId fromPartyId = new DailySalesSummary.PartyId();
            DailySalesSummary.PartyId[] fromPartyIdArr = new DailySalesSummary.PartyId[1];
            fromPartyId.Value = "WebServiceClient";
            fromPartyIdArr[0] = fromPartyId;
            from.PartyId = fromPartyIdArr;
            message_header.From = from;

            // to 

            DailySalesSummary.To to = new  DailySalesSummary.To();
            DailySalesSummary.PartyId toPartyId = new DailySalesSummary.PartyId();
            DailySalesSummary.PartyId[] toPartyIdArr = new DailySalesSummary.PartyId[1];
            toPartyId.Value = "WebServiceSupplier";
            toPartyIdArr[0] = toPartyId;
            to.PartyId = toPartyIdArr;
            message_header.To = to;

            message_header.Action = "TKT_TravelAgencyReportsRQ";
            message_header.CPAId = "IPCC";
            DailySalesSummary.Service service = new DailySalesSummary.Service();
            service.Value = "TKT_TravelAgencyReportsRQ";



            message_header.Service = service;
            DailySalesSummary.MessageData message_data = new DailySalesSummary.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 


            message_header.ConversationId = now.ToLongDateString();

            return message_header;
        }



        public static DailySalesSummary.Security returnSecurityHeader(string token)
        {

            DailySalesSummary.Security security = new DailySalesSummary.Security();

            DailySalesSummary.SecurityBinarySecurityToken security_token = new DailySalesSummary.SecurityBinarySecurityToken();

            security_token.Value = token;
            security.BinarySecurityToken = security_token;


            return security;
        }
        public static DailySalesSummary.DailySalesSummaryPortTypeClient returnClient(string token)
        {

            DailySalesSummary.DailySalesSummaryPortTypeClient client = new DailySalesSummary.DailySalesSummaryPortTypeClient();

            string endpoint = Util.ReadToken(token);
            client.Endpoint.Address = new EndpointAddress(endpoint);


            return client;
        }


        public static DailySalesSummary._DailySalesSummaryRQ buildRequest()
        {

            DailySalesSummary._DailySalesSummaryRQ RQ = new DailySalesSummary._DailySalesSummaryRQ();

            RQ.version = "1.2.2";
            DailySalesSummary._DailySSSelectionCriteria selection_criteria = new DailySalesSummary._DailySSSelectionCriteria();

            RQ.SelectionCriteria = selection_criteria;
            RQ.Header = new DailySalesSummary.STL_HeaderRQ();

            DateTime report_date = Convert.ToDateTime("2021-01-01");

            //System.Windows.Forms.MessageBox.Show(report_date.ToString());
         

            selection_criteria.PseudoCityCode = "AK47";
            selection_criteria.ReportDate = report_date;

            return RQ; 
        }


    }
}
