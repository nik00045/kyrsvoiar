using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Security.Cryptography;
using System.Text;


namespace kyrsvoiar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RSAController : ControllerBase
    {
        string pubKeyPath = "public.key";//change as needed
        string priKeyPath = "private.key";//change as needed
        RSACryptoServiceProvider cspp = new RSACryptoServiceProvider(2048);
        public void MakeKey()
        {
            //lets take a new CSP with a new 2048 bit rsa key pair
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);

            //how to get the private key
            RSAParameters privKey = csp.ExportParameters(true);

            //and the public key ...
            RSAParameters pubKey = csp.ExportParameters(false);
            //converting the public key into a string representation
            string pubKeyString;
            {
                //we need some buffer
                var sw = new StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, pubKey);
                //get the string from the stream
                pubKeyString = sw.ToString();
                pubKeyPath = pubKeyString;
                Program.pubKey = pubKeyString;
            }
            string privKeyString;
            {
                //we need some buffer
                var sw = new StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, privKey);
                //get the string from the stream
                privKeyString = sw.ToString();
                priKeyPath = privKeyString;
                Program.priKey = privKeyString;
            }
        }
        public string MakePubKey()
        {
            //lets take a new CSP with a new 2048 bit rsa key pair


            //and the public key ...
            RSAParameters pubKey = cspp.ExportParameters(false);
            //converting the public key into a string representation
            string pubKeyString;
            {
                //we need some buffer
                var sw = new StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, pubKey);
                //get the string from the stream
                pubKeyString = sw.ToString();
                Program.pubKey = pubKeyString;
            }

            return pubKeyString;
        }
        public string MakePriKey()
        {
            //lets take a new CSP with a new 2048 bit rsa key pair

            //how to get the private key
            RSAParameters privKey = cspp.ExportParameters(true);

            string privKeyString;
            {
                //we need some buffer
                var sw = new StringWriter();
                //we need a serializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //serialize the key into the stream
                xs.Serialize(sw, privKey);
                //get the string from the stream
                privKeyString = sw.ToString();
                Program.priKey = privKeyString;
            }
            return privKeyString;
        }

        public string EncryptFile(string data, string pubKeyPathXML)
        {
            //converting the public key into a string representation
            string pubKeyString = pubKeyPathXML;

            //get a stream from the string
            var sr = new StringReader(pubKeyString);

            //we need a deserializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));

            //get the object back from the stream
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();
            csp.ImportParameters((RSAParameters)xs.Deserialize(sr));

            byte[] bytesPlainTextData = Convert.FromBase64String(data);

            //apply pkcs#1.5 padding and encrypt our data 
            var bytesCipherText = csp.Encrypt(bytesPlainTextData, false);
            //we might want a string representation of our cypher text... base64 will do
            string encryptedText = Convert.ToBase64String(bytesCipherText);
            return encryptedText;
        }
        public string DecryptFile(string data, string priKeyPathXML)
        {
            //we want to decrypt, therefore we need a csp and load our private key
            RSACryptoServiceProvider csp = new RSACryptoServiceProvider();

            string privKeyString;
            {
                privKeyString = priKeyPathXML;
                //get a stream from the string
                var sr = new StringReader(privKeyString);
                //we need a deserializer
                var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
                //get the object back from the stream
                RSAParameters privKey = (RSAParameters)xs.Deserialize(sr);
                csp.ImportParameters(privKey);
            }
            string encryptedText = data;

            byte[] bytesCipherText = Convert.FromBase64String(encryptedText);

            //decrypt and strip pkcs#1.5 padding
            byte[] bytesPlainTextData = csp.Decrypt(bytesCipherText, false);

            //get our original plainText back...
            return Convert.ToBase64String(bytesPlainTextData);
        }

        [HttpGet]
        public string Get()
        {
            //  pubKeyPath = MakePubKey();
            // priKeyPath = MakePriKey();
            MakeKey();
            return " pubKey ---" + pubKeyPath + "\n " + "  priKey ---" + priKeyPath + "\n " + EncryptFile("aaaaaaaa", pubKeyPath) + "\n " + DecryptFile(EncryptFile("aaaaaaaa", pubKeyPath), priKeyPath);
        }
        [HttpGet("Getpubkey")]
        public string Getpubkey()
        {
            MakeKey();
            return Program.pubKey;
        }

        [HttpPost]
        [Produces("text/plain")]
        [Consumes("text/plain")]
        public async Task<string> GetAnchorsByiot()
        {
            string data;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body, Encoding.UTF8))
            {
                data = await reader.ReadToEndAsync();
            }
            //Request.Body.ToString();
            return DecryptFile(data, Program.priKey);
        }

    }
}
