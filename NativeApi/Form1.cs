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
using System.Diagnostics;



namespace NativeApi
{
    public partial class Form1 : Form
    {

        // url for local broker 
        static string FailOverUrl = "failover:(tcp://localhost:61616)"; 
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

        static IConnection con;
        static IMessageConsumer responseConsumer; 

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += Form1_FormClosing;
            try
            {
                InitializeBrokerConnection();
                SubscribeToEvents();
                SubscribeToSyncEvents();
            }
            catch (Exception e )
            {
                Debug.Write(e.Message);
                Debug.Write("### Error Starting the Connection ####"); 
            }
      
    

           // this.TopMost = true; 
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // close all connections
            producer.Close();
            session.Close();
            con.Close();

            Debug.Write("######### Form is closing ###########"); 
        }

        private void InitializeBrokerConnection()

        {

            string[] temp = WindowsIdentity.GetCurrent().Name.Split('\\');

            String userDomain = temp[0];
            String userName = temp[1];

            Uri uri = new Uri(FailOverUrl);

           // IConnectionFactory factory = new Apache.NMS.ActiveMQ.ConnectionFactory(uri);
            NMSConnectionFactory factory = new NMSConnectionFactory(URL);

            string RequestqueueName = redAppID + "_" + userDomain + "\\" + userName + "_request";
            string ResponsequeueName = redAppID + "_" + userDomain + "\\" + userName + "_response";

            con = factory.CreateConnection();

            con.ConnectionInterruptedListener += new ConnectionInterruptedListener(OnConnectionInterupted);
            con.ExceptionListener += new ExceptionListener(OnExceptionListener);
            con.ConnectionResumedListener += new ConnectionResumedListener(OnConnectionResumedListener);


            con.Start();

            txtResult.Text = "Connection Started !!! ";

            session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
          

            destinationRequest = session.GetQueue(RequestqueueName);
            destinationResponse = session.GetQueue(ResponsequeueName);
           

            // get the resposne and show it in the text box 
          //  consumer = session.CreateConsumer(destinationRequest);

            producer = session.CreateProducer(destinationRequest);
            producer.DeliveryMode = MsgDeliveryMode.NonPersistent;

           responseConsumer = session.CreateConsumer(destinationResponse);
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

        private void OnConnectionInterupted()
        {

            Debug.Write("Connecton was Interupted "); 

        }

        private void OnExceptionListener(Exception e )
        {
            Debug.Write(e.StackTrace);
            Debug.Write("##### There was an Exception ####### ");

            destinationRequest.Dispose();
            destinationResponse.Dispose();
          //  responseConsumer.Close(); 
            producer.Close();
            session.Close();
            con.Close();
        }

        private void OnConnectionResumedListener()
        {

            Debug.Write("Connection was Resumed");

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

        private void btnShowInEmu_Click(object sender, EventArgs e)
        {

            // Show a Mesage in the emulator 
            string text = txtCommand.Text.ToUpper();

            String Textmsg = "<?xml version='1.0' encoding='UTF-8'?><ns1:ShowInEmuRQ xmlns:ns1 ='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0' isCommand= 'false' >" +
   "<Message>" + text + "</Message>"+
   "</ns1:ShowInEmuRQ>";

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

            try
            {
                // send the message 
                producer.Send(session.CreateTextMessage(Textmsg));
            }
            catch (Apache.NMS.ActiveMQ.ConnectionClosedException e )
            {
                Debug.Write("######## Connection closed ########## ");
                Debug.Write(e);
              
            }
            finally{
             //   InitializeBrokerConnection();
            }
           

 
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

       //       txtResult.Text = Doc.Root.Name.LocalName;

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
                // ReadPNR(tokenval); 

                // SendSabreCommand(tokenval); 
                DesignatePrinter(tokenval); 
            }


            if (Doc.Root.Name.LocalName.Equals("EventSubscriptionRS"))
            {
                  txtResult.Text = xmlText;
                //txtResult.Text = "Command Intercepted ";

            
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

            TravelItineraryRead.TravelItineraryReadPortTypeClient client = TIR.returnClient(token);

            TravelItineraryRead.MessageHeader message_header = TIR.returnMessageHeader();
            TravelItineraryRead.Security1 security = TIR.returnSecurityHeader(token);

            // this line is to use TLS 1.2 otherwise it cannot connect to the end point 

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            TravelItineraryRead.TravelItineraryReadRS response; 
            try
            {
               response = client.TravelItineraryReadRQ(ref message_header, ref security, WSPayloads.buildRequestForTravelItinRead());
                // MessageBox.Show(response.ToString());
                SerializeAndShowWSResponse(response);
            }
            catch (Exception e )
            {

                MessageBox.Show(e.Message);

            }
            finally
            {
             
            }
       

            
        }


        private void SendSabreCommand(String token)
        {
            SabreCommand.SabreCommandLLSPortTypeClient client = SabreCommandRequest.returnClient(token);

            SabreCommand.MessageHeader message_header = SabreCommandRequest.returnMessageHeader();
            SabreCommand.Security security = SabreCommandRequest.returnSecurityHeader(token);

            // this line is to use TLS 1.2 otherwise it cannot connect to the end point 

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            SabreCommand.SabreCommandLLSRS response;
            //string command = txtCommand.Text.ToUpper();
            string command = "0HHTHIGK1JFKIN07JUL-OUT08JUL/HOLIDAY INN/DBLB/65.00USD/G/SI-" + Util.CHGKEY + "1900 VAN WYKE¥S OZONE PARK NY 10405¥FONE 212-555-1957¤REQUESTED SUITE/CF-62FE77";
            SabreCommand.SabreCommandLLSRQ req = SabreCommandRequest.getRequest(token, command);

            SerializeAndShowWSResponse(req);
            try
            {
                response = client.SabreCommandLLSRQ(ref message_header, ref security, req);

                SerializeAndShowWSResponse(response);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.StackTrace + "\"" + e.Message);
                Debug.Write(e.StackTrace);
                Debug.Write(e.Message);

            }
            finally
            {

            }


        }

       private void  DesignatePrinter(String token )
        {
            DesignatePrinter.DesignatePrinterPortTypeClient client = DesignatePrinterService.returnClient(token);

            DesignatePrinter.MessageHeader message_header = DesignatePrinterService.returnMessageHeader();
            DesignatePrinter.Security1 security = DesignatePrinterService.returnSecurityHeader(token);

            DesignatePrinter.DesignatePrinterRQ req = DesignatePrinterService.getRequest(token,"1");
            SerializeAndShowWSResponse(req);

            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            DesignatePrinter.DesignatePrinterRS response = null; 
            try
            {
                response = client.DesignatePrinterRQ(ref message_header, ref security, req);

                SerializeAndShowWSResponse(response);
            }
            catch (Exception e)
            {

                MessageBox.Show(e.StackTrace + "\"" + e.Message);
                Debug.Write(e.StackTrace);
                Debug.Write(e.Message);

            }
            finally
            {

            }

        }

        // This method shows the xml response from Travel Itinerary Red in a pop up message 

        private void SerializeAndShowWSResponse(Object response)
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


                Debug.Write(xmlDoc.InnerXml);
            }


        }

        /*
         * Designar Impresoras con DesignatePrinterLLSRQ 
        */
        private void button1_Click_1(object sender, EventArgs e)
        {
            String Textmsg = "<?xml version='1.0' encoding='UTF-8'?>" +
"<ns1:GetSessionSecurityTokenRQ xmlns:ns1='http://stl.sabre.com/POS/SRW/NextGen/nativeapi/v1.0'/>";

            SendMessage(Textmsg);
        }
    }
}
