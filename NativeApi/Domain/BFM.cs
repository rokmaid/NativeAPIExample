using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using NativeApi.BargainFinderMax;

namespace NativeApi.Domain
{
    class BFM
    {
        private static OTA_AirLowFareSearchRQ request;

        public static BargainFinderMax.OTA_AirLowFareSearchRQ buildPayloadRequest(String pcc )
        {

            request = new OTA_AirLowFareSearchRQ()
            {
                Version = "5.2.0",

                POS = new SourceType[]
            {
                new SourceType()
                {
                    PseudoCityCode = pcc,
                    RequestorID = new UniqueID_Type()
                    {
                        ID = "1",
                        Type = "1",
                        CompanyName = new CompanyNameType()
                        {
                            Code = "TN",
                            Value = "TN"
                        }
                    }
                }
            },
                OriginDestinationInformation = new OTA_AirLowFareSearchRQOriginDestinationInformation[]
            {
                new OTA_AirLowFareSearchRQOriginDestinationInformation()
                {
                    RPH = "0",
                    Item = "2020-04-14T00:00:00",
                    ItemElementName = ItemChoiceType.DepartureDateTime,
                    OriginLocation = new OriginDestinationInformationTypeOriginLocation()
                    {
                        LocationCode = "AEP"
                    },
                    DestinationLocation = new OriginDestinationInformationTypeDestinationLocation()
                    {
                        LocationCode = "COR"
                    }, TPA_Extensions = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions()
                    {
                        SegmentType = new ExchangeOriginDestinationInformationTypeSegmentType()
                        {
                            Code = 0
                        },
                        Baggage = new BaggageType()
                        {
                            FreePieceRequired = false
                        }
                    }
                },
                new OTA_AirLowFareSearchRQOriginDestinationInformation()
                {
                    RPH = "1",
                    Item = "2020-04-27T00:00:00",
                    ItemElementName = ItemChoiceType.DepartureDateTime,
                    OriginLocation = new OriginDestinationInformationTypeOriginLocation()
                    {
                        LocationCode = "COR"
                    },
                    DestinationLocation = new OriginDestinationInformationTypeDestinationLocation()
                    {
                        LocationCode = "AEP"
                    }
                    , TPA_Extensions = new OTA_AirLowFareSearchRQOriginDestinationInformationTPA_Extensions()
                    {
                        SegmentType = new ExchangeOriginDestinationInformationTypeSegmentType()
                        {
                            Code = 0
                        },
                        Baggage = new BaggageType()
                        {
                            FreePieceRequired = false
                        }
                    }
                }
            }, TravelPreferences = new AirSearchPrefsType()
            {
                // traveler prefs node 
                AncillaryFees = new AirSearchPrefsTypeAncillaryFees()
                {
                    Enable=true,
                    Summary=true,
                    // Air Extas 
                    AncillaryFeeGroup = new AirSearchPrefsTypeAncillaryFeesAncillaryFeeGroup[]
                    {
                        new AirSearchPrefsTypeAncillaryFeesAncillaryFeeGroup()
                        {
                          
                         Code = "BG",
                        Count = "1"
                    }
                        }
                    
                }
               
                

            },
               TravelerInfoSummary = new TravelerInfoSummaryType()
                {
                    AirTravelerAvail = new TravelerInformationType[]
                {
                    new TravelerInformationType()
                    {
                        PassengerTypeQuantity = new PassengerTypeQuantityType[]
                        {
                            new PassengerTypeQuantityType()
                            {
                                Code = "ADT",
                                Quantity = "1"
                            }
                        }
                    }
                }
                },
                TPA_Extensions = new OTA_AirLowFareSearchRQTPA_Extensions()
                {
                    IntelliSellTransaction = new TransactionType()
                    {
                        RequestType = new TransactionTypeRequestType()
                        {
                            Name = "100ITINS"
                        }

                    }
                   
                }
            };



            return request; 

        }

        public static BargainFinderMax.MessageHeader buildMessageHeader()
        {
            BargainFinderMax.MessageHeader message_header = new BargainFinderMax.MessageHeader();

            DateTime now = DateTime.Now;

            // from 
            BargainFinderMax.From from = new BargainFinderMax.From();
            BargainFinderMax.PartyId fromPartyId = new BargainFinderMax.PartyId();
            BargainFinderMax.PartyId[] fromPartyIdArr = new BargainFinderMax.PartyId[1];
            fromPartyId.Value = "WebServiceClient";
            fromPartyIdArr[0] = fromPartyId;
            from.PartyId = fromPartyIdArr;
            message_header.From = from;

            // to 

            BargainFinderMax.To to = new BargainFinderMax.To();
            BargainFinderMax.PartyId toPartyId = new BargainFinderMax.PartyId();
            BargainFinderMax.PartyId[] toPartyIdArr = new BargainFinderMax.PartyId[1];
            toPartyId.Value = "WebServiceSupplier";
            toPartyIdArr[0] = toPartyId;
            to.PartyId = toPartyIdArr;
            message_header.To = to;

            message_header.Action = "BargainFinderMaxRQ";
            message_header.CPAId = "IPCC";
            BargainFinderMax.Service service = new BargainFinderMax.Service();
            service.Value = "BargainFinderMaxRQ";



            message_header.Service = service;
            BargainFinderMax.MessageData message_data = new BargainFinderMax.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 


            message_header.ConversationId = now.ToLongDateString();

            return message_header;
        }



        public static BargainFinderMax.Security   returnSecurityHeader(string token)
        {

            BargainFinderMax.Security security = new BargainFinderMax.Security();

            security.BinarySecurityToken = token;


            return security;
        }

        public static BargainFinderMax.BargainFinderMaxPortTypeClient returnClient(string token)
        {

            BargainFinderMaxPortTypeClient client = new BargainFinderMaxPortTypeClient();

            string endpoint = Util.ReadToken(token);
           client.Endpoint.Address = new EndpointAddress(endpoint);

            /*
             * This is to handle the size of the response 
             */
            Binding binding = new BasicHttpsBinding()
            {
                MaxReceivedMessageSize = Int32.MaxValue,
                MaxBufferSize = Int32.MaxValue
            };

         
            client.Endpoint.Binding = binding; 

            return client;
        }

    }
}
