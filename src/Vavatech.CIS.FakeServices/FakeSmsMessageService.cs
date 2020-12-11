using System;
using System.Collections.Generic;
using System.Text;
using Vavatech.CIS.IServices;

namespace Vavatech.CIS.FakeServices
{
    public class FakeSmsMessageService : IMessageService
    {
        public void Send(string message)
        {
            throw new NotImplementedException();
        }
    }
}
