using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApi.Domain
{
    class WSPayloads
    {
        public static  GetReservation.GetReservationRQ buildPayloadforGetRes()
        {

            // build payload 

            GetReservation.GetReservationRQ payload_request = new GetReservation.GetReservationRQ();
            GetReservation.POS_TypePNRB pos_type = new GetReservation.POS_TypePNRB();
            GetReservation.SourceTypePNRB source_type = new GetReservation.SourceTypePNRB();

            // need PCC 
            source_type.PseudoCityCode = "F9UE";

            GetReservation.BookingChannelPNRB booking_chanel = new GetReservation.BookingChannelPNRB();

            booking_chanel.Type = "GDS";
            GetReservation.CompanyNameType company = new GetReservation.CompanyNameType();
            company.CompanyShortName = "Sabre";
            company.Code = "Sabre";

            booking_chanel.CompanyName = company;
            source_type.BookingChannel = booking_chanel;
            pos_type.Source = source_type;

            GetReservation.ReturnOptionsPNRB returnop = new GetReservation.ReturnOptionsPNRB();

            returnop.ResponseFormat = "STL";
            returnop.ViewName = "Simple";
            String[] subjAreasVec ={"PRICING_INFORMATION","ACCOUNTING_LINE","ADDRESS","AIR_CABIN","AFAX","ANCILLARY","BAS_EXTENSION","CORPORATE_ID","CUST_INSIGHT_PROFILE",
                "DK_NUMBER","DSS","EXT_FQTV","FARETYPE","FQTV","GFAX","HEADER","ITINERARY","NAME","PASSENGERDETAILS","PHONE","PRERESERVEDSEAT","RECEIVED","RECORD_LOCATOR",
                "REMARKS","TICKETING"
                };
            returnop.SubjectAreas = subjAreasVec;

            payload_request.Version = "1.19.0";

            payload_request.ReturnOptions = returnop;
            payload_request.POS = pos_type;

            return payload_request;
        }

     public static  TravelItineraryRead.TravelItineraryReadRQ buildRequestForTravelItinRead()
        {

            TravelItineraryRead.TravelItineraryReadRQ request = new TravelItineraryRead.TravelItineraryReadRQ();

            TravelItineraryRead.TravelItineraryReadRQMessagingDetails message_datails = new TravelItineraryRead.TravelItineraryReadRQMessagingDetails();

            string[] subjareas = { "FULL" };
            message_datails.SubjectAreas = subjareas;


            request.Version = "3.10.0";
            request.MessagingDetails = message_datails; 

            return request;
        }

        // method to build header 



    }
}
