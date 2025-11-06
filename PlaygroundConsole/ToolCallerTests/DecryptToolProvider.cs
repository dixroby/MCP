using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlaygroundConsole.ToolCallerTests
{
    internal class DecryptToolProvider : CryptToolProviderBase
    {
        public DecryptToolProvider() : base(
            "decryptor_tool",
            "Tool for decrypting text",
            "Text to decrypt")
        {
        }


        protected override string ExecuteTool(string text)
        {
            byte[] bytes = Convert.FromBase64String(text);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}
