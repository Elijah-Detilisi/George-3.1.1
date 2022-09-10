using System.Linq;

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

        private readonly static Dictionary<string, string[]> _registeredCommands = new Dictionary<string, string[]>()
        {
            {
                "Sign-up: Exit", 
                    new string[]{
                        "close", "sleep", 
                        "stop", "shut up", "thunderstruck", 
                        "good bye", "shut down", "turn off"
                    } 
            },
            {
                "Sign-up: Login",
                    new string[]{
                        "login", "sign in"
                    }
            },

        };

        private readonly static Dictionary<string, string[]> _dictationInputs = new Dictionary<string, string[]>()
        {
            {
                "Spelling: AlphaNumerics",
                    "a-b-c-d-e-f-g-h-i-j-k-l-m-n-o-p-q-r-s-t-u-v-w-x-y-z-1-2-3-4-5-6-7-8-9-0".Split("-")
            },
            {
                "Email: Domains",
                    new string[]{
                        "@gmail.com", "@yahoo.com", "@hotmail.com",
                        "@outlook.com", "@office365.com"
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

        public static string[] GetCommands(string commandKey)
        {
            var commands = _registeredCommands[commandKey];
            return commands;
        }

        public static string[] GetDictationInputs(string dictationKey)
        {
            var commands = _dictationInputs[dictationKey];
            return commands;
        }
        #endregion
    }
}
