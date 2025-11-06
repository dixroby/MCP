using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole.ToolCallerTests
{
    internal class EncryptToolProvider : CryptToolProviderBase
    {
        public EncryptToolProvider() : 
            base("encryptor_tool", "tool for encrypting text", "text to encrypt")
        {
        }

        protected override string ExecuteTool(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            string base64 = Convert.ToBase64String(bytes);
            return base64;
        }
    }
}
