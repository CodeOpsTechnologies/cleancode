using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TestSamples.Before
{
    public class Device
    {
        public const int MEDICAL = 0;
        public const int AGRICULTURAL = 1;
        public const int REFINARY = 2;

        public Device(int type, int deviceId)
        {
            this.Type = type;
            this.Device_Id = deviceId;
        }

        public void ToggleStatus()
        {
            Status = !Status;
        }

        public static string GetStatus(Device device, int language)
        {
            string on_Or_Off;
            string status_Message;
            //If not english, then dutch
            if (language == EN)
            {
                status_Message = "OnOff";
                on_Or_Off = device.Status ? "ON" : "OFF";
            }
            else
            {
                status_Message = "Toestand";
                on_Or_Off = device.Status ? "OP" : "UIT";
            }

            switch (device.Type)
            {
                case MEDICAL:
                    return string.Format("{0}: {1},  {2}", status_Message, on_Or_Off, device.HeartRate);
                case AGRICULTURAL:
                    return string.Format("{0}: {1},  {2}", status_Message, on_Or_Off, device.SoilQuality);
                case REFINARY:
                    return string.Format("{0}: {1},  {2}", status_Message, on_Or_Off, device.Temperature);
            }
            return "";
        }

        public static string StatusPrinter(List<Device> devices, int userLanguage)
        {
            var returnString = "";

            // test list is empty
            if (devices.Count == 0) {
                returnString = userLanguage == EN ? "<h1>Empty list of devices!</h1>" : "<h1>Lege lijst met apparaten!</h1>";
            }
            else {
                //we have devices
                //header
                if (userLanguage == EN) {
                    returnString += "<h1>Devices report</h1><br/>";
                }
                else {
                    // default is dutch
                    returnString += "<h1>apparaten Report</ h1><br/>";
                }

                int numMedical = 0;
                int numAgricultural = 0;
                int numRefinaries = 0;

                //Get all statuses
                for (int i = 0; i < devices.Count; i++) {
                    Device d = devices[i];
                    if (d.Type == MEDICAL) {
                        numMedical++;
                        returnString += string.Format("<BR/> Device ID: {0};{1}<BR/>", d.Device_Id,
                            GetStatus(d, userLanguage));
                    }

                    if (d.Type == AGRICULTURAL) {
                        numAgricultural++;
                        returnString += string.Format("<BR/> Device ID: {0};{1}<BR/>", d.Device_Id,
                            GetStatus(d, userLanguage)); 
                    }

                    if (d.Type == REFINARY) {
                        numRefinaries++;
                        returnString += string.Format("<BR/> Device ID: {0};{1}<BR/>", d.Device_Id,
                            GetStatus(d, userLanguage));
                        
                    }
                }

                //let`s print this
                returnString += GetLine(Device.MEDICAL, userLanguage, numMedical);
                returnString += GetLine(Device.AGRICULTURAL, userLanguage, numAgricultural);
                returnString += GetLine(Device.REFINARY, userLanguage, numRefinaries);

                //footer
                returnString += "TOTAL: ";
                returnString += (numAgricultural + numMedical + numRefinaries) + " " +
                                (userLanguage == EN ? "devices" : "apparaten");

            }
            return returnString;
        }

        public int Type;
        public bool Status = true;
        public int Device_Id;

        public int SoilQuality;
        public int HeartRate;
        public int Temperature;

        public const int EN = 1;
        public const int DU = 2;


        public static string GetLine(int type, int lang, int num)
        {
            if (num > 0)
            {
                if (lang == EN)
                {
                    return num + " " + Translate(type, lang) + " devices" + "<br/>";
                }
                return num + " " + Translate(type, lang) + " apparaten" + "<br/>";
            }
            return string.Empty;
        }

        private static string Translate(int type, int lang)
        {
            switch (type)
            {
                case MEDICAL:
                    return lang == EN ? "Medical" : "Medisch";
                case AGRICULTURAL:
                    return lang == EN ? "Agricultural" : "Agrarisch";
                case REFINARY:
                    return lang == EN ? "Refinary" : "Raffinaderij";
            }
            return "";
        }
    }
}
