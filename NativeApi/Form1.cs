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

namespace NativeApi
{
    public partial class Form1 : Form
    {

        // url for local broker 
        static string URL = "tcp://localhost:61616";

        // red app id for the plugin running in SRW 

        static string redAppID = "example-native-7lkkd5c";

        // this will be used to send messages to be broker 
        static IMessageProducer producer;

        // need these objects available in all methods 
        static ISession session;
        static IDestination destinationRequest;
        static IDestination destinationResponse;
        static IMessageConsumer consumer; 

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

          //  txtResult.Text = Doc.Root.Name.LocalName;
            var Hostrsp = from response in Doc.Descendants("Response") select response;

            foreach(var r in Hostrsp)
            {
                txtResult.Text += r.Value;

            }

            var Othrsp = from response in Doc.Descendants("ath") select response; 
            
            foreach(var oth in Othrsp)
            {
                txtResult.Text += oth.Value; 

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
            DialogResult dialogResult = MessageBox.Show("Do you want to Intercept the Command ? ", " ", MessageBoxButtons.YesNo);
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

    }
}
