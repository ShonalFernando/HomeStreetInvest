using MongoDB.Bson;

namespace HomeStreetInvest.UserAccounts.Services
{
    public class SessionService
    {
        //To generatea session id -> Name, DateTime, and Users ObjectID
        public string GenerateSessionID(string Username, ObjectId UserID)
        {
            int NameLength = Username.Length;
            int DayAsInt = DateTime.Now.Day;
            int HourAsInt = DateTime.Now.Hour;
            string UserIDAsString = UserID.ToString();

            Random Random = new();
            int RandomCheck = Random.Next(111, 999);

            string SessionID = NameLength.ToString() + DayAsInt.ToString() + HourAsInt.ToString() + UserIDAsString + RandomCheck.ToString();

            int sum = SessionID.Sum(c => Char.IsDigit(c) ? (c - '0') : 0);
            int checkDigit = sum % 10;

            SessionID += checkDigit.ToString();

            return SessionID;
        }

        public bool ValidateSessionID(string receivedSessionID)
        {
            if (string.IsNullOrEmpty(receivedSessionID) || receivedSessionID.Length < 2)
            {
                // Invalid SessionID (must have at least 2 characters for checksum)
                return false;
            }

            // Extract the SessionID without the check digit
            string sessionIDWithoutCheckDigit = receivedSessionID.Substring(0, receivedSessionID.Length - 1);

            // Extract the check digit
            char receivedCheckDigit = receivedSessionID[receivedSessionID.Length - 1];

            // Calculate the check digit for the extracted sessionIDWithoutCheckDigit
            int sum = sessionIDWithoutCheckDigit.Sum(c => Char.IsDigit(c) ? (c - '0') : 0);
            int calculatedCheckDigit = sum % 10;

            // Compare the received check digit with the calculated one
            if (receivedCheckDigit == (calculatedCheckDigit + '0'))
            {
                // Checksum matches, SessionID is valid
                return true;
            }

            // Checksum does not match, SessionID is invalid
            return false;
        }

    }
}
