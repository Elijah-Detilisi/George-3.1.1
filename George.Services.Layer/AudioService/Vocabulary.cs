using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace George.Services.Layer.AudioService
{
    public static class Vocabulary
    {
        #region Data fields
        private readonly static Dictionary<string, string> _promptMessages = new Dictionary<string, string>(){
            {
                "Sign-up: Introduction", 
                    "Welcome to the sign-up window."
            },
            {
                "Sign-up: Email_Request", 
                    "Please provide your email address."
            },
            {
                "Sign-up: Password_Request", 
                    "Please provide your email password."
            },
            {
                "Sign-up: Authentication_Verification", 
                    "Please wait while I verify your login info."
            },
            {
                "Sign-up: Greeting", 
                    "Hello, my name is George, I am your voice operated emailing system."
            },
            {
                "Sign-up: Authentication_Error", 
                    "The authentication details you provided are invalid, please try again."
            }
        };

        private readonly static Dictionary<string, List<string>> _registeredCommands = new Dictionary<string, List<string>>()
        {
            {
                "Sign-up: Exit", 
                    new List<string>(){
                        "close", "sleep", 
                        "stop", "shut up",  
                        "good bye", "shut down", "turn off"
                    } 
            },
            {
                "Sign-up: Login",
                    new List<string>(){
                        "login", "sign in"
                    }
            },

        };
        #endregion

        #region Class methods
        public static string GetPromptMessage(string promptKey)
        {
            string message = _promptMessages[promptKey];
            
            return message;
        }

        public static List<string> GetCommands(string commandKey)
        {
            var commands = _registeredCommands[commandKey];
            return commands;
        }
        #endregion

    }
}
