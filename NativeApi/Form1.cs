using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using NativeApi.Domain;
using System.Xml.Serialization;
using System.IO; 



namespace NativeApi
{
    public partial class Form1 : Form
    {

        // url for local broker 
        static string URL = "tcp://localhost:61616";
        static string ENDPOINT = "";
                
        // red app id for the plugin running in SRW 

        static string redAppID = "example-native-7lkkd5c";

        // this will be used to send messages to be broker 
        static IMessageProducer producer;

        // need these objects available in all methods 
        static ISession session;
        static IDestination destinationRequest;
        static IDestination destinationResponse;



        public Form1()
        {
            InitializeComponent();
            initializeBrokerConnection();
            SubscribeToEvents();
            SubscribeToSyncEvents();


        }

        private void initializeBrokerConnection()

        {

            string[] temp = WindowsIdentity.GetCurrent().Name.Split('\\');

            String userDomain = temp[0];
            String userName = temp[1];

            //  IConnectionFactory connection = 
            NMSConnectionFactory factory = new NMSConnectionFactory(URL);

            string RequestqueueName = redAppID + "_" + userDomain + "\\" + userName + "_request";
            string ResponsequeueName = redAppID + "_" + userDomain + "\\" + userName + "_response";

            IConnection con = factory.CreateConnection();


            con.Start();

            txtResult.Text = "Connection Started !!! ";

            session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);

            destinationRequest = session.GetQueue(RequestqueueName);
            destinationResponse = session.GetQueue(ResponsequeueName);
           

            // get the resposne and show it in the text box 
          //  consumer = session.CreateConsumer(destinationRequest);

            producer = session.CreateProducer(destinationRequest);
            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

            IMessageConsumer responseConsumer = session.CreateConsumer(destinationResponse);
            txtResult.Text = ""; 

            responseConsumer.Listener += (message) =>
            {

                // To Access GUI from a different thread 

                Invoke(new Action(() =>
                {
                    ITextMessage txtMsg = message as ITextMessage;
                    string body = txtMsg.Text;

                   //txtResult.Text = body; 
                    ParseXML(body); 
                }));
     

            };

        }



        private void btnSendToHost_Click(object sender, EventArgs e)
        {
            // send command to the Host 
            string command = txtCommand.Text.ToUpper();
            String textMsg = "<?xml version='1.0' encoding='UTF-8'?><ns1:SendHostCommandRQ xmlns:ns1='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0'  >" +
"<Command>" + command + "</Command>" +
"</ns1:SendHostCommandRQ>";

            SendMessage(textMsg);

        }

        private void btnSendToEmulator_Click(object sender, EventArgs e)
        {
            // send command to the emulator 
            string command = txtCommand.Text.ToUpper();

            String Textmsg = "<?xml version='1.0' encoding='UTF-8'?><ns1:ExecuteInEmuRQ xmlns:ns1='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0' showCommand='true' showResponse='true'>" +
"<Command>" + command + "</Command>" +
"</ns1:ExecuteInEmuRQ>";


            SendMessage(Textmsg); 


        }

        private void btnGetToken_Click(object sender, EventArgs e)
        {
            // get the session Token 

            String Textmsg = "<?xml version='1.0' encoding='UTF-8'?>" +
"<ns1:GetSessionSecurityTokenRQ xmlns:ns1='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0'/>";

            SendMessage(Textmsg);


        }

        private void SendMessage(string Textmsg)
        {

            // send the message 
            producer.Send(session.CreateTextMessage(Textmsg));

 
        }

        // will susbcribe to the commands declared in the redapp.xml 
        private void SubscribeToEvents()
        {
            String Textmsg = "<?xml version='1.0' encoding='UTF-8'?>" +
"<ns1:EventSubscriptionRQ xmlns:ns1='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0'/>";

            SendMessage(Textmsg); 

        }

        private void SubscribeToSyncEvents()
        {

            String Textmsg = "<?xml version='1.0' encoding='UTF-8'?>" +
"<ns1:CommandSubscriptionRQ xmlns:ns1='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0'/>";

            SendMessage(Textmsg); 

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlText"></param>
        private void ParseXML(string xmlText)
        {
            txtResult.Text = ""; 
            
            var Doc =  XDocument.Parse(xmlText);

            //   txtResult.Text = Doc.Root.Name.LocalName;

            if (Doc.Root.Name.LocalName.Equals("SendHostCommandRS"))
            {
                var Hostrsp = from response in Doc.Descendants("Response") select response;

                foreach (var r in Hostrsp)
                {
                    txtResult.Text += r.Value;

                }
            }
      
            if (Doc.Root.Name.LocalName.Equals("GetSessionSecurityTokenRS"))
            {
                var Othrsp = from response in Doc.Descendants("ath") select response;
                string tokenval = "";
                foreach (var oth in Othrsp)
                {
                    txtResult.Text += oth.Value;
                    tokenval += oth.Value;

                }
                 ReadPNR(tokenval); 

            }


            if (Doc.Root.Name.LocalName.Equals("EventSubscriptionRS"))
            {
                txtResult.Text = "Command Intercepted ";
            }

            if (Doc.Root.Name.LocalName.Equals("CommandInterceptionRQ"))
            {

                // addtional method to intercept the command or let it pass 

                string command = Doc.Root.Attribute("command").Value;
                interceptCommand(command);
            }
         
        

        }


        private void interceptCommand(string command)
        {
             string textmg;
            DialogResult dialogResult = MessageBox.Show("Do you want to Intercept the Command   "+command+"? ", " ", MessageBoxButtons.YesNo);
   
            if (dialogResult == DialogResult.Yes)
            {
                
           textmg  = "<?xml version='1.0' encoding='UTF-8'?>" +
       "<com.sabre.edge.dynamo.nativeapi:CommandInterceptionRS xmlns:com.sabre.edge.dynamo.nativeapi='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0' command='' />";

                SendMessage(textmg); 
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
               textmg = "<?xml version='1.0' encoding='UTF-8'?>" +
"<com.sabre.edge.dynamo.nativeapi:CommandInterceptionRS xmlns:com.sabre.edge.dynamo.nativeapi='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0' command='"+command+"' />";
                SendMessage(textmg);
            }
        }

        private void ReadPNR(string token )
        {

            ReadToken(token);

            TravelItineraryRead.TravelItineraryReadPortTypeClient client = new TravelItineraryRead.TravelItineraryReadPortTypeClient();

            TravelItineraryRead.MessageHeader message_header = new TravelItineraryRead.MessageHeader();
            TravelItineraryRead.Security1 security = new TravelItineraryRead.Security1();


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
            service.Value= "TravelItineraryReadRQ";
           


            message_header.Service = service;
            TravelItineraryRead.MessageData message_data = new TravelItineraryRead.MessageData();
            message_data.MessageId = "2529036674227040150";
            message_data.RefToMessageId = "mid:20001209-133003-2333@clientofsabre.com";
            message_data.Timestamp = now.ToString();

            message_header.MessageData = message_data;
            // conversation id seems to accept a timestamp 

           
            message_header.ConversationId= now.ToLongDateString();

            security.BinarySecurityToken = token;

            // this line is to use TLS 1.2 otherwise it cannot connect to the end point 

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            TravelItineraryRead.TravelItineraryReadRS response; 
            try
            {
               response = client.TravelItineraryReadRQ(ref message_header, ref security, WSPayloads.buildRequestForTravelItinRead());
                // MessageBox.Show(response.ToString());
                SerializeAndShowTIRResponse(response);
            }
            catch (Exception e )
            {

                MessageBox.Show(e.Message);

            }
            finally
            {
             
            }
       

            
        }

        // This method shows the xml response from Travel Itinerary Red in a pop up message 

        private void SerializeAndShowTIRResponse(TravelItineraryRead.TravelItineraryReadRS response)
        {

            XmlDocument xmlDoc = new XmlDocument();   //Represents an XML document, 
                                                      // Initializes a new instance of the XmlDocument class.          
            XmlSerializer xmlSerializer = new XmlSerializer(response.GetType());
            // Creates a stream whose backing store is memory. 
            using (MemoryStream xmlStream = new MemoryStream())
            {
                xmlSerializer.Serialize(xmlStream, response);
                xmlStream.Position = 0;
                //Loads the XML document from the specified string.
                xmlDoc.Load(xmlStream);
                //  MessageBox.Show(xmlDoc.InnerXml);
                txtResult.Text = "";
                txtResult.Text = xmlDoc.InnerXml; 
            }


        }

    
        // try to determine if the token is from CERT or Prod to see what SWS endpoint we point to 
        private void ReadToken(string token)
        {

            if (token.Contains("CRT.LB"))
            {
                Console.Write("CERT TOKEN !! ");
                ENDPOINT = "https://sws-crt.cert.havail.sabre.com";
            }else if (token.Contains("RES.LB"))
            {
                Console.Write("Prod token !!!");
                ENDPOINT = "https://webservices.havail.sabre.com";
            }

        }
    }
}
