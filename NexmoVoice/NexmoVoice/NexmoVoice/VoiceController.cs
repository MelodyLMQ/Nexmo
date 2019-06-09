using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nexmo.Api;
using Nexmo.Api.Voice;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Web;
using System.Reflection;

namespace NexmoVoice
{
    public class VoiceController
    {
        private string uuid;
        private readonly Client client;
        public VoiceController()
        {
            //init dependencies
            string privateKey;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "NexmoVoice.Resources.private.key";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                privateKey = reader.ReadToEnd();
            }

            client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "a13c5956",
                ApiSecret = "It62MH4bBVU9k7BZ",
                ApplicationId = "62297d12-bacb-4f29-8db5-401910f07d79",
                ApplicationKey = privateKey
            });
        }

        public void PlayAudioStreamToCall()
        {
            var result = client.Call.BeginStream(uuid, new Call.StreamCommand
            {
                stream_url = new[] { "https://nexmo-community.github.io/ncco-examples/assets/voice_api_audio_streaming.mp3" }
            });
        }

        //public void MuteCall()
        //{
        //    client.Call.Edit(uuid, new Call.CallEditCommand { Action = "mute" });
        //}

        //public void TransferCall()
        //{
        //    var result = client.Call.Edit(uuid, new Call.CallEditCommand
        //    {
        //        Action = "transfer",
        //        Destination = new Call.Destination
        //        {
        //            Type = "ncco",
        //            Url = new[] { "https://developer.nexmo.com/ncco/transfer.json" }
        //        }
        //    });
        //}
        public void MakeCallWithNCCO()
        {
            var results = client.Call.Do(new Nexmo.Api.Voice.Call.CallCommand
            {
                to = new[]
                {
                    new Call.Endpoint { type = "phone", number = "6584531029" }
                },
                from = new Call.Endpoint { type = "phone", number = "12016441754" },
                Ncco = CreateNCCO()
            });

            uuid = results.uuid;
        }
        private JArray CreateNCCO()
        {
            dynamic CallNCCO = new JObject();
            CallNCCO.action = "talk";
            CallNCCO.voicname = "Melody";
            CallNCCO.text = "Here is a song for you, but I failed to deserialize the audio stream";

            //CallNCCO.action = "stream";
            //CallNCCO.streamUrl = "https://nexmo-community.github.io/ncco-examples/assets/voice_api_audio_streaming.mp3";

            var ncco = new JArray();
            ncco.Add(CallNCCO);

            return ncco;
        }
    }
}
