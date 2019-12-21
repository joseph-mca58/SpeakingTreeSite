using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeakingTree.Helpers
{
    public interface IEmailHelper
    {
        void SendEmailToUser(string userEmail, string userId, string password, bool success, string instructions, string failureReason);

    }
}
